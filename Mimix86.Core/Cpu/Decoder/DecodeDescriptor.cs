/* =============================================================================
 * File:   DecodeDescriptor.cs
 * Author: Cole Tobin
 * =============================================================================
 * Copyright (c) 2022-2023 Cole Tobin
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
using System.Diagnostics;
using static Mimix86.Core.Cpu.Decoder.Decoder;
using static Mimix86.Core.Cpu.Decoder.OpcodeMap;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Contains the mappings between an array of <see cref="OpcodeMapEntry" /> objects and their associated
///   <see cref="DecodeHandler" />.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class DecodeDescriptor
{
    /// <summary>
    /// Construct a new <see cref="Mimix86.Core.Cpu.Decoder.Decoder" /> object with a given CPU level.
    /// </summary>
    /// <param name="cpuLevel">The CPU level for this set of descriptors.</param>
    public DecodeDescriptor(int cpuLevel)
    {
        Debug.Assert(cpuLevel is 0);

        InitDecodeCalls(cpuLevel);
    }

    private void InitDecodeCalls(int cpuLevel)
    {
        // ReSharper disable once RedundantExplicitArraySize - ensure it's accurate
        OpcodeMapEntry[][] _6xBlock = new OpcodeMapEntry[16][]
        {
            Opcode60, Opcode61, Opcode62, Opcode63,
            Opcode64, Opcode65, Opcode66, Opcode67,
            Opcode68, Opcode69, Opcode6A, Opcode6B,
            Opcode6C, Opcode6D, Opcode6E, Opcode6F,
        };

        if (cpuLevel is 0)
        {
            for (int i = 0; i < 16; i++)
                Entries[0x60 + i] = new(_6xBlock[i], DecodeImmediate);
        }
        else
        {
            throw new NotImplementedException();
        }
    }


    /// <summary>
    /// Represents a single mapping entry between an array of <see cref="OpcodeMapEntry" /> objects and their associated
    ///   <see cref="DecodeHandler" />.
    /// </summary>
    /// <param name="OpcodeMapEntries">
    /// The array of <see cref="OpcodeMapEntry" /> objects.
    /// These will be passed in the <c>opmapEntries</c> argument of <see cref="Handler" />.
    /// </param>
    /// <param name="Handler">The handler for <see cref="OpcodeMapEntries" />.</param>
    public record Entry(
        OpcodeMapEntry[]? OpcodeMapEntries,
        DecodeHandler Handler);
}
