/* =============================================================================
 * File:   TwoBytePrefix.cs
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

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Contains the various two-byte prefix bytes that can occur when decoding x86 instructions.
/// </summary>
public enum TwoBytePrefix
{
    // sorted by their byte values

    /// <summary>
    /// The AMD 3D Now! prefix, typically located at <c>[0F 0F]</c>.
    /// The eight-bit immediate is the opcode byte.
    /// </summary>
    _3DNow,

    /// <summary>
    /// The <c>DREX</c> opcode map escape prefix, located at <c>[0F 24]</c>.
    /// The <c>DREX</c> byte itself is located between the ModR/M and SIB byte and the displacement (or immediate if no
    ///   displacement is present).
    /// This is the <c>MOV Ry,Ty</c> instruction on the 80386 and 80486.
    /// </summary>
    Drex0F24,

    /// <summary>
    /// The <c>DREX</c> opcode map escape prefix, located at <c>[0F 25]</c>.
    /// The <c>DREX</c> byte itself is located between the ModR/M and SIB byte and the displacement (or immediate if no
    ///   displacement is present).
    /// </summary>
    Drex0F25,

    /// <summary>
    /// The three byte opcode map escape prefix, located at <c>[0F 38]</c>.
    /// </summary>
    ThreeByteEscape0F38,

    /// <summary>
    /// The three byte opcode map escape prefix, located at <c>[0F 3A]</c>.
    /// </summary>
    ThreeByteEscape0F3A,

    /// <summary>
    /// The <c>DREX</c> opcode map escape prefix, located at <c>[0F 7A]</c>.
    /// These opcodes do not actually contain a <c>DREX</c> byte, and are more similar to a normal three-byte map.
    /// </summary>
    Drex0F7A,

    /// <summary>
    /// The <c>DREX</c> opcode map escape prefix, located at <c>[0F 7B]</c>.
    /// These opcodes do not actually contain a <c>DREX</c> byte, and are more similar to a normal three-byte map.
    /// </summary>
    Drex0F7B,
}
