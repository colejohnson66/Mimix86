/* =============================================================================
 * File:   Prefixes.cs
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

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Contains the various x86 instruction prefixes that are supported.
/// </summary>
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public enum Prefixes
{
    // sorted by their (typical) byte values

    #region Prefix in the One-Byte Opcode Map

    /// <summary>
    /// The two-byte opcode map escape prefix, typically located at <c>[0F]</c>.
    /// </summary>
    TwoByteEscape,

    /// <summary>
    /// The <c>ES</c> segment prefix, typically located at <c>[26]</c>.
    /// </summary>
    SegmentES,

    /// <summary>
    /// The <c>CS</c> segment prefix, typically located at <c>[2E]</c>.
    /// This is also overloaded by the Pentium P4 as a "hint branch not taken" for <c>Jcc</c>.
    /// </summary>
    SegmentCS,

    /// <summary>
    /// The <c>DS</c> segment prefix, typically located at <c>[36]</c>.
    /// </summary>
    SegmentDS,

    /// <summary>
    /// The <c>SS</c> segment prefix, typically located at <c>[3E]</c>.
    /// This is also overloaded by the Pentium P4 as a "hint branch taken" for <c>Jcc</c>, and for Intel CET to suppress
    ///   the check for <c>ENDBR32</c> or <c>ENDBR64</c> at an indirect branch target.
    /// </summary>
    SegmentSS,

    /// <summary>
    /// The <c>REX</c> set of 16 prefixes, typically located at <c>[40]..[4F]</c>.
    /// This is normally the <c>INC Zv</c> and <c>DEC Zv</c> instructions.
    /// </summary>
    /// <remarks>
    /// When bound to a byte in the <see cref="Cpu.Decoder.Prefixes.OneByte" /> opmap, the four least-significant bits
    ///   will be interpreted as the payload.
    /// Therefore, all sixteen possible values should be bound to this prefix for proper operation.
    /// </remarks>
    Rex,

    /// <summary>
    /// The <c>L1OM</c> prefix for scalar instructions, typically located at <c>[62]</c>.
    /// This is normally the <c>BOUND</c> instruction.
    /// </summary>
    L1OMScalar,

    /// <summary>
    /// The four-byte <c>MVEX</c> prefix, typically located at <c>[62]</c>.
    /// This is normally the <c>BOUND</c> instruction.
    /// </summary>
    /// <remarks>
    /// This was the original prefix used in Intel's Xeon Phi line of coprocessors.
    /// Once AVX-512 was released on client processors, the three-byte payload was changed.
    /// In the MVEX prefix, the "U" bit (bit 2 of byte 2 of the three-byte payload) is clear, but is set for
    ///   <see cref="Evex" />.
    /// Since the introduction of Intel APX in late July 2023, this is no longer true, with the "U" bit being overloaded
    ///   as another payload bit.
    /// </remarks>
    Mvex,

    /// <summary>
    /// The four-byte <c>EVEX</c> prefix, typically located at <c>[62]</c>.
    /// This is normally the <c>BOUND</c> instruction.
    /// </summary>
    /// <remarks>
    /// Despite originally being the same as the <see cref="Mvex" /> prefix, but with the "U" bit being set, Intel APX
    ///   has changed this;
    /// The interpretation of the 3-byte payload is (now) dependent on the opcode byte that follows this prefix.
    /// </remarks>
    Evex,

    /// <summary>
    /// The <c>DS2</c> segment prefix, typically located at <c>[63]</c>.
    /// </summary>
    SegmentDS2,

    /// <summary>
    /// The <c>REPC</c> prefix, typically located at <c>[64]</c>.
    /// </summary>
    Repc,

    /// <summary>
    /// The <c>FS</c> segment prefix, typically located at <c>[64]</c>.
    /// This is also overloaded by the Pentium P4 as a "hint alternating branch taken/not taken" for <c>Jcc</c>.
    /// </summary>
    SegmentFS,

    /// <summary>
    /// The <c>REPNC</c> prefix, typically located at <c>[65]</c>.
    /// </summary>
    Repnc,

    /// <summary>
    /// The <c>GS</c> segment prefix, typically located at <c>[65]</c>.
    /// </summary>
    SegmentGS,

    /// <summary>
    /// The <c>OSIZE</c> prefix, typically located at <c>[66]</c>.
    /// </summary>
    OperandSize,

    /// <summary>
    /// The <c>ASIZE</c> prefix, typically located at <c>[67]</c>.
    /// </summary>
    AddressSize,

    /// <summary>
    /// The <c>XOP</c> prefix, typically located at <c>[8F]</c>.
    /// </summary>
    Xop,

    /// <summary>
    /// The three-byte <c>VEX</c> prefix, typically located at <c>[C4]</c>.
    /// This is normally the <c>LES</c> instruction.
    /// </summary>
    Vex3,

    /// <summary>
    /// The two-byte <c>VEX</c> prefix, typically located at <c>[C5]</c>.
    /// This is normally the <c>LDS</c> instruction.
    /// </summary>
    Vex2,

    /// <summary>
    /// The two-byte <c>REX2</c>, typically located at <c>[D5]</c>.
    /// This is normally the <c>AAD Ib</c> instruction..
    /// </summary>
    Rex2,

    /// <summary>
    /// The <c>DS3</c> segment prefix, typically located at <c>[D6]</c>.
    /// </summary>
    SegmentDS3,

    /// <summary>
    /// The three-byte <c>L1OM</c> vector for scalar instructions, typically located at <c>[D6]</c>.
    /// This is normally the <c>SALC</c> instruction.
    /// </summary>
    L1OMVector,

    /// <summary>
    /// The <c>LOCK</c> prefix, typically located at <c>[F0]</c>.
    /// </summary>
    Lock,

    /// <summary>
    /// The <c>IRAM</c> segment prefix, typically located at <c>[F1]</c>.
    /// </summary>
    SegmentIRam,

    /// <summary>
    /// The <c>REPNE</c> prefix, typically located at <c>[F2]</c>.
    /// This is also overloaded by Intel MPX as the <c>BND</c> prefix, and Intel HLE as the <c>XACQUIRE</c> prefix.
    /// </summary>
    Repne,

    /// <summary>
    /// The <c>REP/REPE</c> prefix, typically located at <c>[F3]</c>.
    /// This is also overloaded by Intel HLE as the <c>XRELEASE</c> prefix.
    /// </summary>
    RepRepe,

    #endregion

    #region Prefix in the Two-Byte Opcode Map

    /// <summary>
    /// The AMD 3D Now! prefix, typically located at <c>[0F 0F]</c>.
    /// The eight-bit immediate is the opcode byte.
    /// </summary>
    _3DNow,

    /// <summary>
    /// The <c>DREX</c> opcode map escape prefix, located at <c>[0F 24]</c>.
    /// The <c>DREX</c> payload byte itself is located after the SIB byte (or ModR/M byte if the SIB byte is not
    ///   present), and before the displacement and immediate (if present).
    /// This is the <c>MOV Ry,Ty</c> instruction on the 80386 and 80486.
    /// </summary>
    Drex0F24,

    /// <summary>
    /// The <c>DREX</c> opcode map escape prefix, located at <c>[0F 25]</c>.
    /// The <c>DREX</c> payload byte itself is located after the SIB byte (or ModR/M byte if the SIB byte is not
    ///   present), and before the displacement and immediate (if present).
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

    #endregion
}
