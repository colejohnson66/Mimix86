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
 *   Mimix86. If not, see <http://www.gnu.org/licenses/>.
 * =============================================================================
 */

using System;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents an x86 instruction decoder.
/// </summary>
[PublicAPI]
public sealed partial class Decoder
{
    private readonly OneBytePrefix?[] _oneBytePrefixes = new OneBytePrefix?[256];
    // private readonly TwoBytePrefix?[] _twoBytePrefixes = new TwoBytePrefix?[256];

    private readonly DecoderStoreByInstructionMap<Entry?> _instructions = new();


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
    public void RegisterOneBytePrefix(byte b, OneBytePrefix prefix)
    {
        if (prefix is not (OneBytePrefix.SegmentES or OneBytePrefix.SegmentCS or OneBytePrefix.SegmentDS or OneBytePrefix.SegmentSS) &&
            prefix is not (OneBytePrefix.Lock or OneBytePrefix.Repne or OneBytePrefix.RepRepe))
            throw new ArgumentOutOfRangeException(nameof(prefix), prefix, "Prefix is not supported yet.");

        if (_oneBytePrefixes[b] is not null)
            throw new InvalidOperationException($"One-byte prefix at [{b:X2}] is already registered as {_oneBytePrefixes[b]}.");

        _oneBytePrefixes[b] = prefix;
    }


    /// <summary>
    /// Register an instruction map/byte combination.
    /// </summary>
    /// <param name="map">The instruction map to register into.</param>
    /// <param name="b">The byte to register.</param>
    /// <param name="hasModRM"><c>true</c> if this instruction map/byte combination has a ModR/M byte.</param>
    /// <exception cref="InvalidOperationException">
    /// If the instruction map/byte combination is already registered.
    /// </exception>
    public void RegisterInstructionMapByte(InstructionMap map, byte b, bool hasModRM)
    {
        if (_instructions[map, b] is not null)
            throw new InvalidOperationException("Instruction map/byte combination is already registered.");

        _instructions[map, b] = new()
        {
            HasModRM = hasModRM,
        };
    }

    /// <summary>
    /// Register multiple instruction map/byte combinations.
    /// </summary>
    /// <param name="map">The instruction map to register into.</param>
    /// <param name="bytes">The bytes to register.</param>
    /// <param name="hasModRM"><c>true</c> if these instruction map/byte combinations all have a ModR/M byte.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes" /> is <c>null</c>.</exception>
    /// <exception cref="InvalidOperationException">
    /// If any instruction map/bytes combination are already registered.
    /// </exception>
    public void RegisterInstructionMapBytes(InstructionMap map, byte[] bytes, bool hasModRM)
    {
        ArgumentNullException.ThrowIfNull(bytes);

        RegisterInstructionMapBytes(map, bytes.AsSpan(), hasModRM);
    }

    /// <summary>
    /// Register multiple instruction map/byte combinations.
    /// </summary>
    /// <param name="map">The instruction map to register into.</param>
    /// <param name="bytes">The bytes to register.</param>
    /// <param name="hasModRM"><c>true</c> if these instruction map/byte combinations all have a ModR/M byte.</param>
    /// <exception cref="InvalidOperationException">
    /// If any instruction map/bytes combination are already registered.
    /// </exception>
    public void RegisterInstructionMapBytes(InstructionMap map, ReadOnlySpan<byte> bytes, bool hasModRM)
    {
        Span<Entry?> span = _instructions[map];
        foreach (byte b in bytes)
        {
            if (span[b] is not null)
                throw new InvalidOperationException($"Instruction map/byte combination ({map}:{b:x2}) is already registered.");
        }

        foreach (byte b in bytes)
        {
            span[b] = new()
            {
                HasModRM = hasModRM,
            };
        }
    }


    /// <summary>
    /// Register a single instruction entry in a specified opcode map/byte combination.
    /// </summary>
    /// <param name="map">The instruction map to register into.</param>
    /// <param name="b">The byte to register into.</param>
    /// <param name="entry">The entry to register</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="map" /> is unsupported.</exception>
    /// <exception cref="InvalidOperationException">
    /// If the instruction map/byte combination is not registered.
    /// </exception>
    public void RegisterInstructions(InstructionMap map, byte b, DecoderInstructionEntry entry)
    {
        Entry? mapEntry = _instructions[map, b];
        if (mapEntry is null)
            throw new InvalidOperationException("Instruction map/byte combination is not registered.");

        mapEntry.Instructions.Add(entry);
    }
}
