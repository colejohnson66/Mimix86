/* =============================================================================
 * File:   MemorySystem.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Represents the whole memory system.
 * Provides access to read/write/DMA through registered handlers.
 * =============================================================================
 * Copyright (c) 2022 Cole Tobin
 *
 * This file is part of Mimix86.
 *
 * Mimix86 is free software: you can redistribute it and/or modify it under the
 *   terms of the GNU General Public License as published by the Free Software
 *   Foundation, either version 3 of the License, or (at your option) any later
 *   version.
 *
 * Mimix86 is distributed in the hope that it will be useful, but WITHOUT ANY
 *   WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 *   FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
 *   details.
 *
 * You should have received a copy of the GNU General Public License along with
 *   Mimix86. If not, see <http://www.gnu.org/licenses/>.
 * =============================================================================
 */

using Mimix86.Core.Cpu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#pragma warning disable CS1591

namespace Mimix86.Core.Memory;

/// <summary>
/// Represents the entirety of the memory system.
/// </summary>
[PublicAPI]
public static class MemorySystem
{
    private const int BITS_PER_CHUNK_INDEX = 20; // 1M
    // each array element is a list of chunk handlers for the whole 1M
    private static readonly List<MemoryChunk>?[] Handlers =
        new List<MemoryChunk>?[Config.PHYSICAL_ADDRESS_BITS - BITS_PER_CHUNK_INDEX + 1];


    // future: 80386+
    // private static bool _smRamAvailable;
    // private static bool _smRamEnabled;
    // private static bool _smRamRestricted;


    #region Handlers

    /// <summary>
    /// Register a memory chunk handler for a specified range of addresses with specified read and optional write and
    ///   DMA delegates.
    /// </summary>
    /// <param name="handler">The handler object to register.</param>
    /// <exception cref="InvalidOperationException">
    /// If an existing handler already claimed ownership over the start/end rage.
    /// </exception>
    public static void RegisterMemoryHandler(MemoryChunk handler)
    {
        ulong firstIndex = handler.StartAddress.Value >> BITS_PER_CHUNK_INDEX;
        ulong lastIndex = handler.EndAddress.Value >> BITS_PER_CHUNK_INDEX;

        // check for overlapping ranges
        for (ulong i = firstIndex; i <= lastIndex; i++)
        {
            List<MemoryChunk>? handlers = Handlers[i];
            if (handlers is null)
                continue;

            if (handlers.Any(chunk => handler.StartAddress <= chunk.EndAddress && handler.EndAddress >= chunk.StartAddress))
                throw new InvalidOperationException("Cannot register a chunk at this address; Another has already claimed it.");
        }

        // register it
        for (ulong i = firstIndex; i <= lastIndex; i++)
            GetOrCreateChunk(i).Add(handler);
    }

    private static List<MemoryChunk> GetOrCreateChunk(ulong index)
    {
        List<MemoryChunk>? list = Handlers[index];

        if (list is not null)
            return list;

        list = new();
        Handlers[index] = list;
        return list;
    }

    #endregion


    #region Read/Write

    public static void Read(CpuCore core, PhysicalAddress address, Span<byte> data)
    {
        if (address >> BITS_PER_CHUNK_INDEX != (address + (ulong)data.Length - 1) >> BITS_PER_CHUNK_INDEX)
            throw new ArgumentException("Crossing page.");

        PhysicalAddress a20Address = new(address.Value & 0xF_FFFFul);                    // TODO: this is prob incorrect
        PhysicalAddress a20AddressPlusData = new(a20Address.Value + (ulong)data.Length); // TODO: and this

        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (MemoryChunk handler in GetOrCreateChunk(address))
        {
            if (a20Address < handler.StartAddress || a20AddressPlusData > handler.EndAddress)
                continue;
            if (handler.Read(address, data))
                return;
        }

        // TODO: handle the rest
        throw new NotImplementedException();
    }

    public static void Write(CpuCore core, PhysicalAddress address, ReadOnlySpan<byte> data)
    {
        if (address >> BITS_PER_CHUNK_INDEX != (address + (ulong)data.Length - 1) >> BITS_PER_CHUNK_INDEX)
            throw new ArgumentException("Cannot write; Data would cross a page boundary.");

        PhysicalAddress a20Address = new(address.Value & 0xF_FFFFul);                    // TODO: this is prob incorrect
        PhysicalAddress a20AddressPlusData = new(a20Address.Value + (ulong)data.Length); // TODO: and this

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (MemoryChunk handler in GetOrCreateChunk(address))
        {
            if (a20Address < handler.StartAddress || a20AddressPlusData > handler.EndAddress)
                continue;
            if (!handler.CanWrite)
                throw new NotSupportedException("Cannot write; Writing to this memory chunk is unsupported.");
            if (handler.Write(address, data))
                return;
        }

        // TODO: handle the rest
        throw new NotImplementedException();
    }

    #endregion


    public static void LoadRom(RomType type, Stream input, PhysicalAddress address)
    {
        ArgumentNullException.ThrowIfNull(input);
        if (type is not RomType.Bios)
            throw new ArgumentOutOfRangeException(nameof(type), type, "ROM type must be BIOS.");
        if (!input.CanRead)
            throw new ArgumentException("Must be able to read the BIOS ROM stream.", nameof(input));
        if (!input.CanSeek)
            throw new ArgumentException("Must be able to seek the BIOS ROM stream.", nameof(input));
        if ((ulong)input.Length + address.Value != 0x10_0000)
            throw new ArgumentException("BIOS ROM must end at 0xF'FFFF.", nameof(input));

        RegisterMemoryHandler(new RomChunk(address, input));
    }
}
