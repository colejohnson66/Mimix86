/* =============================================================================
 * File:   SegmentNames.cs
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

namespace Mimix86.Core.Cpu;

/// <summary>
/// Represents the four architectural segment registers, defined by their prefix byte.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum SegmentNames : byte
{
    /// <summary>
    /// Indicates the <c>ES</c> (extra) segment, with the prefix byte, <c>[26]</c>.
    /// </summary>
    [UsedImplicitly] ES = 0x26,

    /// <summary>
    /// Indicates the <c>CS</c> (code) segment, with the prefix byte, <c>[2E]</c>.
    /// </summary>
    [UsedImplicitly] CS = 0x2E,

    /// <summary>
    /// Indicates the <c>SS</c> (stack) segment, with the prefix byte, <c>[36]</c>.
    /// </summary>
    [UsedImplicitly] SS = 0x36,

    /// <summary>
    /// Indicates the <c>DS</c> (data) segment, with the prefix byte, <c>[3E]</c>.
    /// </summary>
    [UsedImplicitly] DS = 0x3E,

    // add with 80386+ support
    // /// <summary>
    // /// Indicates the <c>FS</c> segment, with the prefix byte, <c>[64]</c>.
    // /// </summary>
    // FS = 0x64,

    // add with 80386+ support
    // /// <summary>
    // /// Indicates the <c>GS</c> segment, with the prefix byte, <c>[65]</c>.
    // /// </summary>
    // GS = 0x65,
}
