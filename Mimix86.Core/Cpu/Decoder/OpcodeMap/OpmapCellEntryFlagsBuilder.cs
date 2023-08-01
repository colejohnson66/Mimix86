/* =============================================================================
 * File:   OpcodeDecodeFlagsBuilder.cs
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
 *   Mimix86. If not, see <https://www.gnu.org/licenses/>.
 * =============================================================================
 */

using System.Diagnostics;

namespace Mimix86.Core.Cpu.Decoder.OpcodeMap;

/// <summary>
/// Contains a "builder" class to assist in creation of a <see cref="OpmapCellEntryFlags" /> object during the decode stage.
/// </summary>
/// <remarks>
/// After all the flags are extracted from the instruction stream, <see cref="Matches" /> can be used to see if another
///   <see cref="OpmapCellEntryFlags" /> object matches them.
/// </remarks>
public class OpmapCellEntryFlagsBuilder
{
    private OpmapCellEntryFlags _value;

    /// <summary>
    /// Construct a new, empty, <see cref="OpmapCellEntryFlagsBuilder" />.
    /// </summary>
    public OpmapCellEntryFlagsBuilder()
    { }


    /// <summary>
    /// Set the value of the ModR/M byte's mod, reg, and R/M fields.
    /// </summary>
    /// <param name="b">The value of the ModR/M byte.</param>
    public void ModRM(byte b)
    {
        /* ┌───┬───┬───┬───┬───┬───┬───┬───┐
         * │ 7 │ 6 │ 5 │ 4 │ 3 │ 2 │ 1 │ 0 │
         * │  Mod  │    Reg    │    R/M    │
         * └───────┴───────────┴───────────┘
         */

        // mod
        _value |= (b & 0xC0) is 0xC0
            ? OpmapCellEntryFlags.ModReg
            : OpmapCellEntryFlags.ModMem;

        // reg
        _value |= ((b >> 3) & 7) switch
        {
            0 => OpmapCellEntryFlags.Reg0,
            1 => OpmapCellEntryFlags.Reg1,
            2 => OpmapCellEntryFlags.Reg2,
            3 => OpmapCellEntryFlags.Reg3,
            4 => OpmapCellEntryFlags.Reg4,
            5 => OpmapCellEntryFlags.Reg5,
            6 => OpmapCellEntryFlags.Reg6,
            7 => OpmapCellEntryFlags.Reg7,
            _ => throw new UnreachableException(),
        };

        // r/m
        _value |= (b & 7) switch
        {
            0 => OpmapCellEntryFlags.RM0,
            1 => OpmapCellEntryFlags.RM1,
            2 => OpmapCellEntryFlags.RM2,
            3 => OpmapCellEntryFlags.RM3,
            4 => OpmapCellEntryFlags.RM4,
            5 => OpmapCellEntryFlags.RM5,
            6 => OpmapCellEntryFlags.RM6,
            7 => OpmapCellEntryFlags.RM7,
            _ => throw new UnreachableException(),
        };
    }


    // future: instruction set (real, v8086, 32, 64)

    // future: OSIZE (16, 32, 64)

    // future: ASIZE (16, 32, 64)

    // future: legacy SSE prefixes (NP, 66, F3, F2)

    // future: VLEN (128, 256, 512)

    // future: (E)VEX.W bit


    /// <summary>
    /// Determine if the compiled assortment of flags matches those from a given opcode's flags.
    /// </summary>
    /// <param name="opcodeFlags">The flags required by the opcode.</param>
    /// <returns>
    /// A boolean indicating if the provided opcode flags are a match to the ones extracted from the instruction stream.
    /// </returns>
    public bool Matches(OpmapCellEntryFlags opcodeFlags) =>
        (_value.Values & opcodeFlags.Masks) == opcodeFlags.Values;
}
