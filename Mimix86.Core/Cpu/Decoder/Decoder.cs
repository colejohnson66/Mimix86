/* =============================================================================
 * File:   Decoder.cs
 * Author: Cole Tobin
 * =============================================================================
 * Copyright (c) 2023 Cole Tobin
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
 *   Mimix86. If not, see <https://www.gnu.org/licenses/>.
 * =============================================================================
 */

using Mimix86.Core.Cpu.Decoder.Map;
using System;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents an x86 instruction decoder.
/// </summary>
[PublicAPI]
public sealed partial class Decoder
{
    private readonly OneBytePrefixes?[] _oneBytePrefixes = new OneBytePrefixes?[256];
    // private readonly TwoBytePrefix?[] _twoBytePrefixes = new TwoBytePrefix?[256];

    private readonly OpcodeMapDictionary<EntrySet?> _instructions = new();


    /// <summary>
    /// Register a single one-byte prefix to a specified byte value.
    /// </summary>
    /// <param name="b">The byte value to register the prefix to.</param>
    /// <param name="prefix">The prefix to register.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If any of the following are <c>true</c>:
    /// <list type="bullet">
    ///   <item>if <paramref name="prefix" /> is unsupported</item>
    /// </list>
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// If the specified one-byte prefix is already registered at the specified byte value.
    /// </exception>
    public void RegisterOneBytePrefix(byte b, OneBytePrefixes prefix)
    {
        if (prefix is not (OneBytePrefixes.SegmentES or OneBytePrefixes.SegmentCS or OneBytePrefixes.SegmentDS or OneBytePrefixes.SegmentSS) &&
            prefix is not (OneBytePrefixes.Lock or OneBytePrefixes.Repne or OneBytePrefixes.RepRepe))
            throw new ArgumentOutOfRangeException(nameof(prefix), prefix, "Prefix is not supported yet.");

        if (_oneBytePrefixes[b] is not null)
            throw new InvalidOperationException($"One-byte prefix at [{b:X2}] is already registered as {_oneBytePrefixes[b]}.");

        _oneBytePrefixes[b] = prefix;
    }


    /// <summary>
    /// Register an opcode map/byte combination.
    /// </summary>
    /// <param name="map">The opcode map to register into.</param>
    /// <param name="b">The byte to register.</param>
    /// <param name="flags">The flags for this opcode map/byte combination.</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="map" /> is unsupported.</exception>
    /// <exception cref="InvalidOperationException">
    /// If the opcode map/byte combination is already registered.
    /// </exception>
    public void RegisterOpcodeMapByte(OpcodeMaps map, byte b, OpcodeMapIndexFlags flags)
    {
        if (_instructions[map, b] is not null)
            throw new InvalidOperationException("Instruction map/byte combination is already registered.");

        _instructions[map, b] = new()
        {
            Flags = flags,
        };
    }

    /// <summary>
    /// Register multiple opcode map/byte combinations.
    /// </summary>
    /// <param name="map">The opcode map to register into.</param>
    /// <param name="bytes">The bytes to register.</param>
    /// <param name="flags">The flags for all of these opcode map/byte combinations.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="map" /> is unsupported.</exception>
    /// <exception cref="InvalidOperationException">
    /// If any opcode map/bytes combination are already registered.
    /// </exception>
    public void RegisterOpcodeMapBytes(OpcodeMaps map, byte[] bytes, OpcodeMapIndexFlags flags)
    {
        ArgumentNullException.ThrowIfNull(bytes);

        RegisterOpcodeMapBytes(map, bytes.AsSpan(), flags);
    }

    /// <summary>
    /// Register multiple opcode map/byte combinations.
    /// </summary>
    /// <param name="map">The opcode map to register into.</param>
    /// <param name="bytes">The bytes to register.</param>
    /// <param name="flags">The flags for all of these opcode map/byte combinations.</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="map" /> is unsupported.</exception>
    /// <exception cref="InvalidOperationException">
    /// If any opcode map/bytes combination is already registered.
    /// </exception>
    public void RegisterOpcodeMapBytes(OpcodeMaps map, ReadOnlySpan<byte> bytes, OpcodeMapIndexFlags flags)
    {
        Span<EntrySet?> span = _instructions[map];
        foreach (byte b in bytes)
        {
            if (span[b] is not null)
                throw new InvalidOperationException($"Instruction map/byte combination ({map}:{b:x2}) is already registered.");
        }

        foreach (byte b in bytes)
        {
            span[b] = new()
            {
                Flags = flags,
            };
        }
    }


    /// <summary>
    /// Register a single opcode entry in a specified opcode map/byte combination.
    /// </summary>
    /// <param name="map">The opcode map to register into.</param>
    /// <param name="b">The byte to register into.</param>
    /// <param name="entry">The entry to register</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="map" /> is unsupported.</exception>
    /// <exception cref="InvalidOperationException">
    /// If the opcode map/byte combination is not registered.
    /// </exception>
    public void RegisterInstruction(OpcodeMaps map, byte b, OpcodeMapOpcodeEntry entry)
    {
        EntrySet? mapEntry = _instructions[map, b];
        if (mapEntry is null)
            throw new InvalidOperationException("Instruction map/byte combination is not registered.");

        mapEntry.Instructions.Add(entry);
    }
}
