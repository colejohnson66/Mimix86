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
using Mimix86.Core.Cpu.Isa;
using System;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents an x86 instruction decoder.
/// </summary>
[PublicAPI]
public sealed partial class Decoder
{
    private readonly OneBytePrefixes?[] _oneBytePrefixes = new OneBytePrefixes?[256];
    private readonly TwoBytePrefixes?[] _twoBytePrefixes = new TwoBytePrefixes?[256];

    private readonly OpcodeMapDictionary<EntrySet?> _instructions = new();


    /// <summary>
    /// Register an ISA extension set.
    /// </summary>
    /// <param name="extension">The extension set to register.</param>
    public void RegisterIsaExtension(IsaExtension extension)
    {
        ArgumentNullException.ThrowIfNull(extension);

        if (extension.OneBytePrefixes is not null)
        {
            foreach ((byte b, OneBytePrefixes prefix) in extension.OneBytePrefixes)
            {
                ref OneBytePrefixes? old = ref _oneBytePrefixes[b];
                if (old is not null)
                {
                    // throw if different; otherwise, ignore
                    if (old == prefix)
                        throw new InvalidOperationException($"One-byte prefix {prefix} at [{b:X2}] is already registered as {old}.");
                    continue;
                }
                old = prefix;
            }
        }

        if (extension.TwoBytePrefixes is not null)
        {
            foreach ((byte b, TwoBytePrefixes prefix) in extension.TwoBytePrefixes)
            {
                ref TwoBytePrefixes? old = ref _twoBytePrefixes[b];
                if (old is not null)
                {
                    // throw if different; otherwise, ignore
                    if (old != prefix)
                        throw new InvalidOperationException($"Two-byte prefix {prefix} at [{b:X2}] is already registered as {old}.");
                    continue;
                }
                old = prefix;
            }
        }

        if (extension.OpcodeMapFlags is not null)
        {
            foreach ((OpcodeMapIndex index, OpcodeMapIndexFlags flags) in extension.OpcodeMapFlags)
            {
                ref EntrySet? set = ref _instructions[index];

                if (set is not null)
                {
                    if (set.Flags != flags)
                        throw new InvalidOperationException("Cannot register an opcode map index with different flags than already registered as.");
                    continue;
                }
                set = new(flags);
            }
        }

        // must register entries after flags, otherwise sets could be `null` prematurely
        if (extension.OpcodeMapEntries is not null)
        {
            foreach ((OpcodeMapIndex index, OpcodeMapEntry entry) in extension.OpcodeMapEntries)
            {
                ref EntrySet? set = ref _instructions[index];
                if (set is null)
                    throw new InvalidOperationException("Cannot register an opcode map entry before the index is registered.");
                set.Instructions.Add(entry);
            }
        }
    }
}
