/* =============================================================================
 * File:   OneBytePrefix.cs
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
/// Contains the various one-byte prefix bytes that can occur when decoding x86 instructions.
/// </summary>
public enum OneBytePrefix
{
    // sorted by their byte values

    /// <summary>
    /// The two byte opcode map escape prefix, typically located at <c>[0F]</c>.
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
    Rex,

    /// <summary>
    /// The <c>L1OM</c> prefix for scalar instructions, typically located at <c>[62]</c>.
    /// This is normally the <c>BOUND</c> instruction.
    /// </summary>
    L1OMScalar,

    /// <summary>
    /// The <c>MVEX/EVEX</c> prefix, typically located at <c>[62]</c>.
    /// Which prefix is used depends on the "U" bit (bit 2) in byte 2 of the 3 byte payload.
    /// This is normally the <c>BOUND</c> instruction.
    /// </summary>
    MvexEvex,

    /// <summary>
    /// The <c>FS</c> segment prefix, typically located at <c>[64]</c>.
    /// This is also overloaded by the Pentium P4 as a "hint alternating branch taken/not taken" for <c>Jcc</c>.
    /// </summary>
    SegmentFS,

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
    /// The <c>L1OM</c> vector for scalar instructions, typically located at <c>[D6]</c>.
    /// This is normally the <c>SALC</c> instruction.
    /// </summary>
    L1OMVector,

    /// <summary>
    /// The <c>LOCK</c> prefix, typically located at <c>[F0]</c>.
    /// </summary>
    Lock,

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
}
