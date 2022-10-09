/* =============================================================================
 * File:   DecodeFlagsBuilder.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * A "builder" helper for DecodeFlags.
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

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Contains a "builder" class to assist in creation of a <see cref="DecodeFlags" /> object during the decode stage.
/// </summary>
/// <remarks>
/// After all the flags are extracted from the instruction stream, <see cref="Matches" /> can be used to see if another
///   <see cref="DecodeFlags" /> object matches them.
/// </remarks>
public class DecodeFlagsBuilder
{
    private ulong _value;

    /// <summary>
    /// Construct a new, empty, <see cref="DecodeFlagsBuilder" />.
    /// </summary>
    public DecodeFlagsBuilder()
    { }


    /// <summary>
    /// Set the value of the ModR/M byte's R/M field.
    /// </summary>
    /// <param name="rm">The value of the ModR/M byte's R/M field.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="rm" /> is less than zero or greater than 7.
    /// </exception>
    public void RM(int rm)
    {
        if (rm is < 0 or > 7)
            throw new ArgumentOutOfRangeException(nameof(rm), rm, "Required ModR/M R/M value must be between zero and seven, inclusive.");

        _value |= rm switch
        {
            0 => DecodeFlags.RM0,
            1 => DecodeFlags.RM1,
            2 => DecodeFlags.RM2,
            3 => DecodeFlags.RM3,
            4 => DecodeFlags.RM4,
            5 => DecodeFlags.RM5,
            6 => DecodeFlags.RM6,
            7 => DecodeFlags.RM7,
            _ => throw new UnreachableException(),
        };
    }

    /// <summary>
    /// Set the value of the ModR/M byte's register field.
    /// </summary>
    /// <param name="reg">The value of the ModR/M byte's register field.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="reg" /> is less than zero or greater than 7.
    /// </exception>
    public void Reg(int reg)
    {
        if (reg is < 0 or > 7)
            throw new ArgumentOutOfRangeException(nameof(reg), reg, "Required ModR/M register value must be between zero and seven, inclusive.");

        _value |= reg switch
        {
            0 => DecodeFlags.REG0,
            1 => DecodeFlags.REG1,
            2 => DecodeFlags.REG2,
            3 => DecodeFlags.REG3,
            4 => DecodeFlags.REG4,
            5 => DecodeFlags.REG5,
            6 => DecodeFlags.REG6,
            7 => DecodeFlags.REG7,
            _ => throw new UnreachableException(),
        };
    }

    /// <summary>
    /// Set the value of the ModR/M byte's mod field to indicate register form (i.e. <c>0b11</c>).
    /// </summary>
    public void ModRegister() =>
        _value |= DecodeFlags.MOD_REG;

    /// <summary>
    /// Set the value of the ModR/M byte's mod field to indicate memory form (i.e. not <c>0b11</c>).
    /// </summary>
    public void ModMemory() =>
        _value |= DecodeFlags.MOD_MEM;


    // future: instruction set (16, 32, 64)

    // future: OSIZE (16, 32, 64)

    // future: ASIZE (16, 32, 64)

    // future: legacy SSE prefixes (NP, 66, F3, F2)

    // future: VLEN (128, 256, 512)

    // future: VEX.W bit


    /// <summary>
    /// Determine if the compiled assortment of flags matches those from a given opcode's flags.
    /// </summary>
    /// <param name="opcodeFlags">The flags required by the opcode.</param>
    /// <returns>
    /// A boolean indicating if the provided opcode flags are a match to the ones extracted from the instruction stream.
    /// </returns>
    public bool Matches(DecodeFlags opcodeFlags)
    {
        uint extractedValues = (uint)(_value & 0xFFFF_FFFFu);
        return (extractedValues & opcodeFlags.Masks) == opcodeFlags.Values;
    }
}
