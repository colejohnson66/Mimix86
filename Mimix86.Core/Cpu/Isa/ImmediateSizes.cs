/* =============================================================================
 * File:   ImmediateSizes.cs
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

namespace Mimix86.Core.Cpu.Isa;

/// <summary>
/// Contains the various immediate sizes for use by the decoder.
/// </summary>
public enum ImmediateSizes
{
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
    /// <remarks>This exists solely for <see cref="Opcodes.EnterIwIb" />.</remarks>
    WordByte,

    // add with 80386+ support
    // /// <summary>
    // /// Indicates an immediate that is a double-word (four bytes).
    // /// </summary>
    // Dword,

    // add with x86-64 support
    // /// <summary>
    // /// Indicates an immediate that is a quad-word (eight bytes).
    // /// </summary>
    // Qword,

    /// <summary>
    /// Indicates an immediate that is a pointer with a word offset (two byte segment plus two byte offset).
    /// </summary>
    PointerWordWord,

    // add with 80386+ support
    // /// <summary>
    // /// Indicates an immediate that is a pointer with a double-word offset (two byte segment plus four byte offset).
    // /// </summary>
    // PointerWordDword,

    // add with x86-64 support
    // /// <summary>
    // /// Indicates an immediate that is a pointer with a quad-word offset (two byte segment plus eight byte offset).
    // /// </summary>
    // PointerWordQword,
}
