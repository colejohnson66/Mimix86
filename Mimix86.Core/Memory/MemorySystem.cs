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
using System.Linq;

#pragma warning disable CS1591

namespace Mimix86.Core.Memory;

/// <summary>
/// Represents the entirety of the memory system.
/// </summary>
[PublicAPI]
public static class MemorySystem
{
    #region Delegates

    /// <summary>
    /// A delegate to invoke when reading from a memory chunk.
    /// </summary>
    /// <param name="address">The address marking the beginning of the memory being read from.</param>
    /// <param name="buffer">The buffer to place the read bytes.</param>
    /// <returns><c>true</c> if the data was read successfully; <c>false</c> otherwise.</returns>
    public delegate bool ReadDelegate(PhysicalAddress address, Span<byte> buffer);

    /// <summary>
    /// A delegate to invoke when writing to a memory chunk.
    /// </summary>
    /// <param name="address">The address marking the beginning of the memory being written to.</param>
    /// <param name="data">The bytes to write.</param>
    /// <returns><c>true</c> if the data was written successfully; <c>false</c> otherwise.</returns>
    public delegate bool WriteDelegate(PhysicalAddress address, ReadOnlySpan<byte> data);

    /// <summary>
    /// A delegate to invoke when performing DMA access to a memory chunk.
    /// </summary>
    /// <param name="address">The address marking the beginning of the memory being read/written to.</param>
    /// <param name="span">
    /// If the DMA request was successful, a <see cref="Span{T}">Span&lt;byte&gt;</see> to the backing memory array,
    ///   beginning at <paramref name="address" />.
    /// </param>
    /// <returns><c>true</c> if the DMA request was successful; <c>false</c> otherwise.</returns>
    public delegate bool DmaDelegate(PhysicalAddress address, out Span<byte> span);

    #endregion


    private const int BITS_PER_CHUNK_INDEX = 20; // 1M
    // each array element is a list of chunk handlers for the whole 1M
    private static readonly List<ChunkHandler>?[] Handlers =
        new List<ChunkHandler>?[Config.PHYSICAL_ADDRESS_BITS - BITS_PER_CHUNK_INDEX + 1];


    // future: 80386+
    // private static bool _smRamAvailable;
    // private static bool _smRamEnabled;
    // private static bool _smRamRestricted;


    private static PhysicalAddress _biosRomAddress;


    #region Handlers

    /// <summary>
    /// Register a memory chunk handler for a specified range of addresses with specified read and optional write and
    ///   DMA delegates.
    /// </summary>
    /// <param name="start">The starting address this chunk will handle.</param>
    /// <param name="end">The (inclusive) ending address this chunk will handle.</param>
    /// <param name="read">The delegate to invoke when reading from this chunk.</param>
    /// <param name="write">
    /// The delegate to invoke when writing to this chunk, or <c>null</c> if this chunk is read-only.
    /// </param>
    /// <param name="dma">
    /// The delegate to invoke for DMA access to this chunk, or <c>null</c> if this chunk does not support DMA.
    /// </param>
    /// <exception cref="ArgumentException">
    /// If <paramref name="end" /> is less than <paramref name="start" />.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If <paramref name="end" /> is greater than the maximum supported physical address.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// If an existing handler already claimed ownership over the start/end rage.
    /// </exception>
    public static void RegisterMemoryHandler(PhysicalAddress start, PhysicalAddress end, ReadDelegate read, WriteDelegate? write, DmaDelegate? dma)
    {
        if (start > end)
            throw new ArgumentException("Starting address of a handler must be less than or equal to the ending address.");
        if (end > Config.MAXIMUM_PHYSICAL_ADDRESS)
            throw new ArgumentException("End address must be lower than the maximum supported memory.");

        ulong firstIndex = start >> BITS_PER_CHUNK_INDEX;
        ulong lastIndex = end >> BITS_PER_CHUNK_INDEX;

        // check for overlapping ranges
        for (ulong i = firstIndex; i <= lastIndex; i++)
        {
            List<ChunkHandler>? handlers = Handlers[i];
            if (handlers is null)
                continue;

            if (handlers.Any(chunk => start <= chunk.End && end >= chunk.Start))
                throw new InvalidOperationException("Overlapping chunk.");
        }

        // register it
        ChunkHandler newHandler = new(start, end, read, write, dma);
        for (ulong i = firstIndex; i <= lastIndex; i++)
            GetOrCreateChunk(i).Add(newHandler);
    }

    private static List<ChunkHandler> GetOrCreateChunk(ulong index)
    {
        List<ChunkHandler>? list = Handlers[index];

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

        PhysicalAddress a20Address = new(address & 0xF_FFFFul); // TODO: this is prob incorrect
        // bool isBios = a20Address >= _biosRomAddress;

        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (ChunkHandler handler in GetOrCreateChunk(address))
        {
            if (a20Address < handler.Start || a20Address + (ulong)data.Length > handler.End)
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
            throw new ArgumentException("Crossing page.");

        PhysicalAddress a20Address = new(address & 0xF_FFFFul); // TODO: this is prob incorrect
        // bool isBios = a20Address >= _biosRomAddress;

        // ReSharper disable once ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
        foreach (ChunkHandler handler in GetOrCreateChunk(address))
        {
            if (a20Address < handler.Start || a20Address + (ulong)data.Length > handler.End)
                continue;
            if (handler.Write is null)
                break;
            if (handler.Write(address, data))
                return;
        }

        // TODO: handle the rest
        throw new NotImplementedException();
    }

    #endregion

    private record ChunkHandler(
        PhysicalAddress Start,
        PhysicalAddress End,
        ReadDelegate Read,
        WriteDelegate? Write,
        DmaDelegate? Dma);
}
