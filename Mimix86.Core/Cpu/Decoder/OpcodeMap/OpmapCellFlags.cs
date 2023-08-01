/* =============================================================================
 * File:   OpcodeMapFlags.cs
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

using System;

namespace Mimix86.Core.Cpu.Decoder.OpcodeMap;

/// <summary>
/// Contains the various flags for an opcode map/byte index.
/// </summary>
[Flags]
public enum OpmapCellFlags
{
    /// <summary>
    /// Indicates that the relevant opcode map entry has a ModR/M byte that must be decoded immediately after the opcode
    ///   byte.
    /// </summary>
    HasModRM = 1 << 0,
}
