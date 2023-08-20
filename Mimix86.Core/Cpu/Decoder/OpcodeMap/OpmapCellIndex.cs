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

using System;

namespace Mimix86.Core.Cpu.Decoder.OpcodeMap;

/// <summary>
/// Represents an index into the overarching opcode map, as a tuple of the map itself, and the byte value.
/// </summary>
public readonly struct OpmapCellIndex
{
    /// <summary>Get the individual map <see cref="Byte" /> indexes into.</summary>
    public OpcodeMaps Map { get; }

    /// <summary>Get the index into the individual map (<see cref="Map" />).</summary>
    public byte Byte { get; }


    /// <summary>
    /// Construct a new <see cref="OpmapCellIndex" /> with a specified map and byte value.
    /// </summary>
    /// <param name="map">The map <see cref="Byte" /> indexes into.</param>
    /// <param name="byte">The index into the map (<see cref="Map" />).</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="map" /> is not supported.</exception>
    public OpmapCellIndex(OpcodeMaps map, byte @byte)
    {
        if (OpmapTableStore.MapToStorageIndex[(int)map] is -1)
            throw new ArgumentOutOfRangeException(nameof(map), map, "Specified map is not supported.");

        Map = map;
        Byte = @byte;
    }

    internal int ToFlattenedOpmapIndex() =>
        OpmapTableStore.MapToStorageIndex[(int)Map] * 256 + Byte;

    /// <inheritdoc />
    public override string ToString() =>
        $"{Map}[{Byte:X2}]";
}
