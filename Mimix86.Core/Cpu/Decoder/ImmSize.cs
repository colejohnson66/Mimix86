/* =============================================================================
 * File:   ImmSize.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Contains the various immediate sizes.
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

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents the various immediate sizes for use with <see cref="DecodeDescriptors.ImmediateDescriptor" />.
/// </summary>
public enum ImmSize
{
    /// <summary>
    /// Indicates a lack of an immediate.
    /// </summary>
    None,

    /// <summary>
    /// Indicates an immediate that is a single byte.
    /// </summary>
    Byte,

    /// <summary>
    /// Indicates an immediate that is a single word (two bytes).
    /// </summary>
    Word,

    /// <summary>
    /// Indicates an immediate that is a word followed by a byte (three bytes).
    /// </summary>
    /// <remarks>This exists solely for <see cref="Opcode.EnterIwIb" />.</remarks>
    WordByte,

    /// <summary>
    /// Indicates an immediate with a <c>'v'</c> suffix.
    /// </summary>
    /// <remarks>
    /// Immediates of this form have the following mapping to the number of bits in the immediate:
    /// <list type="table">
    ///   <listheader>
    ///     <term>Effective OSIZE</term>
    ///     <description>Immediate size</description>
    ///   </listheader>
    ///   <item>
    ///     <term>16-bit</term>
    ///     <description>16 bits</description>
    ///   </item>
    ///   <item>
    ///     <term>32-bit</term>
    ///     <description>32 bits</description>
    ///   </item>
    ///   <item>
    ///     <term>64-bit</term>
    ///     <description>64 bits</description>
    ///   </item>
    /// </list>
    /// </remarks>
    ImmV,

    /// <summary>
    /// Indicates an immediate with a <c>'z'</c> suffix.
    /// </summary>
    /// <remarks>
    /// Immediates of this form have the following mapping to the number of bits in the immediate:
    /// <list type="table">
    ///   <listheader>
    ///     <term>Effective OSIZE</term>
    ///     <description>Immediate size</description>
    ///   </listheader>
    ///   <item>
    ///     <term>16-bit</term>
    ///     <description>16 bits</description>
    ///   </item>
    ///   <item>
    ///     <term>32-bit</term>
    ///     <description>32 bits</description>
    ///   </item>
    ///   <item>
    ///     <term>64-bit</term>
    ///     <description>32 bits</description>
    ///   </item>
    /// </list>
    /// </remarks>
    ImmZ,

    /// <summary>
    /// Indicates a far pointer immediate (those with a <c>'p'</c> suffix).
    /// </summary>
    /// <remarks>
    /// Immediates of this form have the following mapping to the number of bits in the immediate:
    /// <list type="table">
    ///   <listheader>
    ///     <term>Effective OSIZE</term>
    ///     <description>Immediate size</description>
    ///   </listheader>
    ///   <item>
    ///     <term>16-bit</term>
    ///     <description>16+16 (32) bits</description>
    ///   </item>
    ///   <item>
    ///     <term>32-bit</term>
    ///     <description>16+32 (48) bits</description>
    ///   </item>
    ///   <item>
    ///     <term>64-bit</term>
    ///     <description>16+64 (80) bits</description>
    ///   </item>
    /// </list>
    /// </remarks>
    Pointer,
}
