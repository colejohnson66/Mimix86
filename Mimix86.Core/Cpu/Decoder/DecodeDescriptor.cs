/* =============================================================================
 * File:   DecodeDescriptor.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * <TODO>
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

using System;
using System.Diagnostics;
using static Mimix86.Core.Cpu.Decoder.Decoder;
using static Mimix86.Core.Cpu.Decoder.OpcodeMap;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Contains the mappings between an array of <see cref="OpcodeMapEntry" /> objects and their associated
///   <see cref="DecodeHandler" />.
/// </summary>
public class DecodeDescriptor
{
    /// <summary>
    /// Get the actual mappings.
    /// </summary>
    // ReSharper disable once RedundantExplicitArraySize - ensure it's accurate
    public Entry[] Entries { get; } = new Entry[256]
    {
        /* [00] */ new(Opcode00, DecodeModRM),
        /* [01] */ new(Opcode01, DecodeModRM),
        /* [02] */ new(Opcode02, DecodeModRM),
        /* [03] */ new(Opcode03, DecodeModRM),
        /* [04] */ new(Opcode04, DecodeImmediate),
        /* [05] */ new(Opcode05, DecodeImmediate),
        /* [06] */ new(Opcode06, DecodeSimple),
        /* [07] */ new(Opcode07, DecodeSimple),
        /* [08] */ new(Opcode08, DecodeModRM),
        /* [09] */ new(Opcode09, DecodeModRM),
        /* [0A] */ new(Opcode0A, DecodeModRM),
        /* [0B] */ new(Opcode0B, DecodeModRM),
        /* [0C] */ new(Opcode0C, DecodeImmediate),
        /* [0D] */ new(Opcode0D, DecodeImmediate),
        /* [0E] */ new(Opcode0E, DecodeSimple),
        /* [0F] */ new(Opcode0F, DecodeSimple),
        /* [10] */ new(Opcode10, DecodeModRM),
        /* [11] */ new(Opcode11, DecodeModRM),
        /* [12] */ new(Opcode12, DecodeModRM),
        /* [13] */ new(Opcode13, DecodeModRM),
        /* [14] */ new(Opcode14, DecodeImmediate),
        /* [15] */ new(Opcode15, DecodeImmediate),
        /* [16] */ new(Opcode16, DecodeSimple),
        /* [17] */ new(Opcode17, DecodeSimple),
        /* [18] */ new(Opcode18, DecodeModRM),
        /* [19] */ new(Opcode19, DecodeModRM),
        /* [1A] */ new(Opcode1A, DecodeModRM),
        /* [1B] */ new(Opcode1B, DecodeModRM),
        /* [1C] */ new(Opcode1C, DecodeImmediate),
        /* [1D] */ new(Opcode1D, DecodeImmediate),
        /* [1E] */ new(Opcode1E, DecodeSimple),
        /* [1F] */ new(Opcode1F, DecodeSimple),
        /* [20] */ new(Opcode20, DecodeModRM),
        /* [21] */ new(Opcode21, DecodeModRM),
        /* [22] */ new(Opcode22, DecodeModRM),
        /* [23] */ new(Opcode23, DecodeModRM),
        /* [24] */ new(Opcode24, DecodeImmediate),
        /* [25] */ new(Opcode25, DecodeImmediate),
        /* [26] */ new(null, DecodeUD),
        /* [27] */ new(Opcode27, DecodeSimple),
        /* [28] */ new(Opcode28, DecodeModRM),
        /* [29] */ new(Opcode29, DecodeModRM),
        /* [2A] */ new(Opcode2A, DecodeModRM),
        /* [2B] */ new(Opcode2B, DecodeModRM),
        /* [2C] */ new(Opcode2C, DecodeImmediate),
        /* [2D] */ new(Opcode2D, DecodeImmediate),
        /* [2E] */ new(null, DecodeUD),
        /* [2F] */ new(Opcode2F, DecodeSimple),
        /* [30] */ new(Opcode30, DecodeModRM),
        /* [31] */ new(Opcode31, DecodeModRM),
        /* [32] */ new(Opcode32, DecodeModRM),
        /* [33] */ new(Opcode33, DecodeModRM),
        /* [34] */ new(Opcode34, DecodeImmediate),
        /* [35] */ new(Opcode35, DecodeImmediate),
        /* [36] */ new(null, DecodeUD),
        /* [37] */ new(Opcode37, DecodeSimple),
        /* [38] */ new(Opcode38, DecodeModRM),
        /* [39] */ new(Opcode39, DecodeModRM),
        /* [3A] */ new(Opcode3A, DecodeModRM),
        /* [3B] */ new(Opcode3B, DecodeModRM),
        /* [3C] */ new(Opcode3C, DecodeImmediate),
        /* [3D] */ new(Opcode3D, DecodeImmediate),
        /* [3E] */ new(null, DecodeUD),
        /* [3F] */ new(Opcode3F, DecodeSimple),
        /* [40] */ new(Opcode40x47, DecodeSimple),
        /* [41] */ new(Opcode40x47, DecodeSimple),
        /* [42] */ new(Opcode40x47, DecodeSimple),
        /* [43] */ new(Opcode40x47, DecodeSimple),
        /* [44] */ new(Opcode40x47, DecodeSimple),
        /* [45] */ new(Opcode40x47, DecodeSimple),
        /* [46] */ new(Opcode40x47, DecodeSimple),
        /* [47] */ new(Opcode40x47, DecodeSimple),
        /* [48] */ new(Opcode48x4F, DecodeSimple),
        /* [49] */ new(Opcode48x4F, DecodeSimple),
        /* [4A] */ new(Opcode48x4F, DecodeSimple),
        /* [4B] */ new(Opcode48x4F, DecodeSimple),
        /* [4C] */ new(Opcode48x4F, DecodeSimple),
        /* [4D] */ new(Opcode48x4F, DecodeSimple),
        /* [4E] */ new(Opcode48x4F, DecodeSimple),
        /* [4F] */ new(Opcode48x4F, DecodeSimple),
        /* [50] */ new(Opcode50x57, DecodeSimple),
        /* [51] */ new(Opcode50x57, DecodeSimple),
        /* [52] */ new(Opcode50x57, DecodeSimple),
        /* [53] */ new(Opcode50x57, DecodeSimple),
        /* [54] */ new(Opcode50x57, DecodeSimple),
        /* [55] */ new(Opcode50x57, DecodeSimple),
        /* [56] */ new(Opcode50x57, DecodeSimple),
        /* [57] */ new(Opcode50x57, DecodeSimple),
        /* [58] */ new(Opcode58x5F, DecodeSimple),
        /* [59] */ new(Opcode58x5F, DecodeSimple),
        /* [5A] */ new(Opcode58x5F, DecodeSimple),
        /* [5B] */ new(Opcode58x5F, DecodeSimple),
        /* [5C] */ new(Opcode58x5F, DecodeSimple),
        /* [5D] */ new(Opcode58x5F, DecodeSimple),
        /* [5E] */ new(Opcode58x5F, DecodeSimple),
        /* [5F] */ new(Opcode58x5F, DecodeSimple),
        /* [60] */ null!, // ┌───────────────────┐
        /* [61] */ null!, // │        Set        │
        /* [62] */ null!, // │        in         │
        /* [63] */ null!, // │ `InitDecodeCalls` │
        /* [64] */ null!, // │       based       │
        /* [65] */ null!, // │        on         │
        /* [66] */ null!, // │        CPU        │
        /* [67] */ null!, // │       level       │
        /* [68] */ null!, // │                   │
        /* [69] */ null!, // │ Level 0:          │
        /* [6A] */ null!, // │   DecodeImmediate │
        /* [6B] */ null!, // │ Levels 1+:        │
        /* [6C] */ null!, // │   unimplemented   │
        /* [6D] */ null!, // │                   │
        /* [6E] */ null!, // │                   │
        /* [6F] */ null!, // └───────────────────┘
        /* [70] */ new(Opcode70x7F, DecodeImmediate),
        /* [71] */ new(Opcode70x7F, DecodeImmediate),
        /* [72] */ new(Opcode70x7F, DecodeImmediate),
        /* [73] */ new(Opcode70x7F, DecodeImmediate),
        /* [74] */ new(Opcode70x7F, DecodeImmediate),
        /* [75] */ new(Opcode70x7F, DecodeImmediate),
        /* [76] */ new(Opcode70x7F, DecodeImmediate),
        /* [77] */ new(Opcode70x7F, DecodeImmediate),
        /* [78] */ new(Opcode70x7F, DecodeImmediate),
        /* [79] */ new(Opcode70x7F, DecodeImmediate),
        /* [7A] */ new(Opcode70x7F, DecodeImmediate),
        /* [7B] */ new(Opcode70x7F, DecodeImmediate),
        /* [7C] */ new(Opcode70x7F, DecodeImmediate),
        /* [7D] */ new(Opcode70x7F, DecodeImmediate),
        /* [7E] */ new(Opcode70x7F, DecodeImmediate),
        /* [7F] */ new(Opcode70x7F, DecodeImmediate),
        /* [80] */ new(Opcode80, DecodeModRM),
        /* [81] */ new(Opcode81, DecodeModRM),
        /* [82] */ new(Opcode80, DecodeModRM), // [82] is a undefined copy of [80]
        /* [83] */ new(Opcode83, DecodeModRM),
        /* [84] */ new(Opcode84, DecodeModRM),
        /* [85] */ new(Opcode85, DecodeModRM),
        /* [86] */ new(Opcode86, DecodeModRM),
        /* [87] */ new(Opcode87, DecodeModRM),
        /* [88] */ new(Opcode88, DecodeModRM),
        /* [89] */ new(Opcode89, DecodeModRM),
        /* [8A] */ new(Opcode8A, DecodeModRM),
        /* [8B] */ new(Opcode8B, DecodeModRM),
        /* [8C] */ new(Opcode8C, DecodeModRM),
        /* [8D] */ new(Opcode8D, DecodeModRM),
        /* [8E] */ new(Opcode8E, DecodeModRM),
        /* [8F] */ new(Opcode8F, DecodeModRM),
        /* [90] */ new(Opcode90, DecodeSimple),
        /* [91] */ new(Opcode91x97, DecodeSimple),
        /* [92] */ new(Opcode91x97, DecodeSimple),
        /* [93] */ new(Opcode91x97, DecodeSimple),
        /* [94] */ new(Opcode91x97, DecodeSimple),
        /* [95] */ new(Opcode91x97, DecodeSimple),
        /* [96] */ new(Opcode91x97, DecodeSimple),
        /* [97] */ new(Opcode91x97, DecodeSimple),
        /* [98] */ new(Opcode98, DecodeSimple),
        /* [99] */ new(Opcode99, DecodeSimple),
        /* [9A] */ new(Opcode9A, DecodeModRM),
        /* [9B] */ new(Opcode9B, DecodeSimple),
        /* [9C] */ new(Opcode9C, DecodeSimple),
        /* [9D] */ new(Opcode9D, DecodeSimple),
        /* [9E] */ new(Opcode9E, DecodeSimple),
        /* [9F] */ new(Opcode9F, DecodeSimple),
        /* [A0] */ new(OpcodeA0, DecodeImmediate),
        /* [A1] */ new(OpcodeA1, DecodeImmediate),
        /* [A2] */ new(OpcodeA2, DecodeImmediate),
        /* [A3] */ new(OpcodeA3, DecodeImmediate),
        /* [A4] */ new(OpcodeA4, DecodeSimple),
        /* [A5] */ new(OpcodeA5, DecodeSimple),
        /* [A6] */ new(OpcodeA6, DecodeSimple),
        /* [A7] */ new(OpcodeA7, DecodeSimple),
        /* [A8] */ new(OpcodeA8, DecodeImmediate),
        /* [A9] */ new(OpcodeA9, DecodeImmediate),
        /* [AA] */ new(OpcodeAA, DecodeSimple),
        /* [AB] */ new(OpcodeAB, DecodeSimple),
        /* [AC] */ new(OpcodeAC, DecodeSimple),
        /* [AD] */ new(OpcodeAD, DecodeSimple),
        /* [AE] */ new(OpcodeAE, DecodeSimple),
        /* [AF] */ new(OpcodeAF, DecodeSimple),
        /* [B0] */ new(OpcodeB0xB7, DecodeImmediate),
        /* [B1] */ new(OpcodeB0xB7, DecodeImmediate),
        /* [B2] */ new(OpcodeB0xB7, DecodeImmediate),
        /* [B3] */ new(OpcodeB0xB7, DecodeImmediate),
        /* [B4] */ new(OpcodeB0xB7, DecodeImmediate),
        /* [B5] */ new(OpcodeB0xB7, DecodeImmediate),
        /* [B6] */ new(OpcodeB0xB7, DecodeImmediate),
        /* [B7] */ new(OpcodeB0xB7, DecodeImmediate),
        /* [B8] */ new(OpcodeB8xBF, DecodeImmediate),
        /* [B9] */ new(OpcodeB8xBF, DecodeImmediate),
        /* [BA] */ new(OpcodeB8xBF, DecodeImmediate),
        /* [BB] */ new(OpcodeB8xBF, DecodeImmediate),
        /* [BC] */ new(OpcodeB8xBF, DecodeImmediate),
        /* [BD] */ new(OpcodeB8xBF, DecodeImmediate),
        /* [BE] */ new(OpcodeB8xBF, DecodeImmediate),
        /* [BF] */ new(OpcodeB8xBF, DecodeImmediate),
        /* [C0] */ new(OpcodeC0, DecodeModRM),
        /* [C1] */ new(OpcodeC1, DecodeModRM),
        /* [C2] */ new(OpcodeC2, DecodeImmediate),
        /* [C3] */ new(OpcodeC3, DecodeImmediate),
        /* [C4] */ new(OpcodeC4, DecodeModRM),
        /* [C5] */ new(OpcodeC5, DecodeModRM),
        /* [C6] */ new(OpcodeC6, DecodeModRM),
        /* [C7] */ new(OpcodeC7, DecodeModRM),
        /* [C8] */ new(OpcodeC8, DecodeImmediate),
        /* [C9] */ new(OpcodeC9, DecodeSimple),
        /* [CA] */ new(OpcodeCA, DecodeImmediate),
        /* [CB] */ new(OpcodeCB, DecodeSimple),
        /* [CC] */ new(OpcodeCC, DecodeSimple),
        /* [CD] */ new(OpcodeCD, DecodeImmediate),
        /* [CE] */ new(OpcodeCE, DecodeSimple),
        /* [CF] */ new(OpcodeCF, DecodeSimple),
        /* [D0] */ new(OpcodeD0, DecodeModRM),
        /* [D1] */ new(OpcodeD1, DecodeModRM),
        /* [D2] */ new(OpcodeD2, DecodeModRM),
        /* [D3] */ new(OpcodeD3, DecodeModRM),
        /* [D4] */ new(OpcodeD4, DecodeImmediate),
        /* [D5] */ new(OpcodeD5, DecodeImmediate),
        /* [D6] */ new(OpcodeD6, DecodeSimple),
        /* [D7] */ new(OpcodeD7, DecodeSimple),
        /* [D8] */ new(OpcodeD8, DecodeModRM),
        /* [D9] */ new(OpcodeD9, DecodeModRM),
        /* [DA] */ new(OpcodeDA, DecodeModRM),
        /* [DB] */ new(OpcodeDB, DecodeModRM),
        /* [DC] */ new(OpcodeDC, DecodeModRM),
        /* [DD] */ new(OpcodeDD, DecodeModRM),
        /* [DE] */ new(OpcodeDE, DecodeModRM),
        /* [DF] */ new(OpcodeDF, DecodeModRM),
        /* [E0] */ new(OpcodeE0, DecodeImmediate),
        /* [E1] */ new(OpcodeE1, DecodeImmediate),
        /* [E2] */ new(OpcodeE2, DecodeImmediate),
        /* [E3] */ new(OpcodeE3, DecodeImmediate),
        /* [E4] */ new(OpcodeE4, DecodeImmediate),
        /* [E5] */ new(OpcodeE5, DecodeImmediate),
        /* [E6] */ new(OpcodeE6, DecodeImmediate),
        /* [E7] */ new(OpcodeE7, DecodeImmediate),
        /* [E8] */ new(OpcodeE8, DecodeImmediate),
        /* [E9] */ new(OpcodeE9, DecodeImmediate),
        /* [EA] */ new(OpcodeEA, DecodeModRM),
        /* [EB] */ new(OpcodeEB, DecodeImmediate),
        /* [EC] */ new(OpcodeEC, DecodeSimple),
        /* [ED] */ new(OpcodeED, DecodeSimple),
        /* [EE] */ new(OpcodeEE, DecodeSimple),
        /* [EF] */ new(OpcodeEF, DecodeSimple),
        /* [F0] */ new(null, DecodeUD),
        /* [F1] */ new(OpcodeF1, DecodeSimple),
        /* [F2] */ new(null, DecodeUD),
        /* [F3] */ new(null, DecodeUD),
        /* [F4] */ new(OpcodeF4, DecodeSimple),
        /* [F5] */ new(OpcodeF5, DecodeSimple),
        /* [F6] */ new(OpcodeF6, DecodeModRM),
        /* [F7] */ new(OpcodeF7, DecodeModRM),
        /* [F8] */ new(OpcodeF8, DecodeSimple),
        /* [F9] */ new(OpcodeF9, DecodeSimple),
        /* [FA] */ new(OpcodeFA, DecodeSimple),
        /* [FB] */ new(OpcodeFB, DecodeSimple),
        /* [FC] */ new(OpcodeFC, DecodeSimple),
        /* [FD] */ new(OpcodeFD, DecodeSimple),
        /* [FE] */ new(OpcodeFE, DecodeModRM),
        /* [FF] */ new(OpcodeFF, DecodeModRM),
    };

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
