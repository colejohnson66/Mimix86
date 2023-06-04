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

    private readonly DecoderStoreByOpcodeMap<Entry?> _opcodes = new();


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
    /// Register an opcode map/byte combination.
    /// </summary>
    /// <param name="map">The opcode map to register into.</param>
    /// <param name="b">The byte to register.</param>
    /// <param name="hasModRM"><c>true</c> if this map/byte combination has a ModR/M byte.</param>
    /// <param name="immediateSize">
    /// The size of the immediate for this opcode map/byte combination, or <c>null</c> if there isn't one.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// If the opcode map/byte combination is already registered.
    /// </exception>
    public void RegisterOpcodeMapByte(DecoderMap map, byte b, bool hasModRM, ImmediateSizes? immediateSize)
    {
        if (_opcodes[map, b] is not null)
            throw new InvalidOperationException("Opcode map/byte combination is already registered.");

        _opcodes[map, b] = new()
        {
            HasModRM = hasModRM,
            ImmediateSize = immediateSize,
        };
    }

    /// <summary>
    /// Register multiple opcode map/byte combinations.
    /// </summary>
    /// <param name="map">The map to register into.</param>
    /// <param name="bytes">The bytes to register.</param>
    /// <param name="hasModRM"><c>true</c> if these map/byte combinations all have a ModR/M byte.</param>
    /// <param name="immediateSize">
    /// The size of the immediate for these opcode map/byte combinations, or <c>null</c> if there isn't one.
    /// </param>
    /// <exception cref="ArgumentNullException">If <paramref name="bytes" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    /// <exception cref="InvalidOperationException">
    /// If any opcode map/bytes combination are already registered.
    /// </exception>
    public void RegisterOpcodeMapBytes(DecoderMap map, byte[] bytes, bool hasModRM, ImmediateSizes? immediateSize)
    {
        ArgumentNullException.ThrowIfNull(bytes);
        if (immediateSize.HasValue && !Enum.IsDefined(immediateSize.GetValueOrDefault()))
            throw new ArgumentOutOfRangeException(nameof(immediateSize), immediateSize, "Unknown immediate size.");

        Span<Entry?> span = _opcodes[map];
        // ReSharper disable once LoopCanBeConvertedToQuery - can't because Span<T>
        foreach (byte b in bytes)
        {
            Entry? entry = span[b];
            if (entry is not null)
                throw new InvalidOperationException("Opcode map/byte combination is already registered.");
        }

        foreach (byte b in bytes)
        {
            span[b] = new()
            {
                HasModRM = hasModRM,
                ImmediateSize = immediateSize,
            };
        }
    }


    /// <summary>
    /// Register a single opcode entry in a specified opcode map/byte combination.
    /// </summary>
    /// <param name="map">The map to register into.</param>
    /// <param name="b">The byte to register into.</param>
    /// <param name="entry">The entry to register</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="map" /> is unsupported.</exception>
    /// <exception cref="InvalidOperationException">If the opcode map/byte combination is not registered.</exception>
    public void RegisterOpcodes(DecoderMap map, byte b, DecoderOpcodeEntry entry)
    {
        Entry? mapEntry = _opcodes[map, b];
        if (mapEntry is null)
            throw new InvalidOperationException("Opcode map/byte combination is not registered.");

        mapEntry.Opcodes.Add(entry);
    }
}
