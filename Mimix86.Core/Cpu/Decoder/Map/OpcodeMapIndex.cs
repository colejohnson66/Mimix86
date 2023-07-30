/* =============================================================================
 * File:   OpcodeMapIndex.cs
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

namespace Mimix86.Core.Cpu.Decoder.Map;

/// <summary>
/// Represents an index into the opcode map, as a tuple of the map itself, and the byte value.
/// </summary>
/// <param name="Map">The map <see cref="Byte" /> indexes into.</param>
/// <param name="Byte">The index into the map (<see cref="Map" />).</param>
public readonly record struct OpcodeMapIndex(OpcodeMaps Map, byte Byte)
{
    /// <inheritdoc />
    public override string ToString() =>
        $"{Map}[{Byte:X2}]";
}
