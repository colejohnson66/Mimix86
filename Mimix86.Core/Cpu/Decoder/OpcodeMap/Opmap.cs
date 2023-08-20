/* =============================================================================
 * File:   Opmap.cs
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
using System.Diagnostics;
using System.Linq;

namespace Mimix86.Core.Cpu.Decoder.OpcodeMap;

/// <summary>
/// Represents the actual opcode map.
/// This maps individual bytes to operations that instruct the decoder (<see cref="Decoder" />) how to proceed.
/// </summary>
public class Opmap
{
    internal static readonly int[] MapToStorageIndex;
    internal static readonly int SupportedMapCount;

    static Opmap()
    {
        MapToStorageIndex = new int[Enum.GetValues<OpcodeMaps>().Length];
        MapToStorageIndex[(int)OpcodeMaps.OneByte] = 0;
        MapToStorageIndex[(int)OpcodeMaps.TwoByte] = -1;
        MapToStorageIndex[(int)OpcodeMaps._3DNow] = -1;
        MapToStorageIndex[(int)OpcodeMaps.ThreeByte0F24] = -1;
        MapToStorageIndex[(int)OpcodeMaps.ThreeByte0F25] = -1;
        MapToStorageIndex[(int)OpcodeMaps.ThreeByte0F38] = -1;
        MapToStorageIndex[(int)OpcodeMaps.ThreeByte0F3A] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Drex0F7A] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Drex0F7B] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Vex0F] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Vex0F38] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Vex0F3A] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Xop8] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Xop9] = -1;
        MapToStorageIndex[(int)OpcodeMaps.XopA] = -1;
        MapToStorageIndex[(int)OpcodeMaps.L1OMScalar] = -1;
        MapToStorageIndex[(int)OpcodeMaps.L1OMVector] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Mvex] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Mvex0F] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Mvex0F38] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Mvex0F3A] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Evex] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Evex0F] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Evex0F38] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Evex0F3A] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Evex4] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Evex5] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Evex6] = -1;

        // ensure all elements have been assigned
        // skip the first element as it's `OneByte` which should be zero
        Debug.Assert(MapToStorageIndex.Skip(1).All(idx => idx is not 0));

        SupportedMapCount = MapToStorageIndex.Max() + 1;
    }


    private readonly OpmapCell?[] _storage = new OpmapCell?[SupportedMapCount * 256];

    /// <summary>
    /// Get a span of opcode map cells for a specified opcode map.
    /// </summary>
    /// <param name="map">The map to get a span for.</param>
    /// <exception cref="ArgumentOutOfRangeException">If <paramref name="map" /> is not supported.</exception>
    /// <remarks>
    /// If any elements are <c>null</c>, they are not registered yet.
    /// </remarks>
    public Span<OpmapCell?> this[OpcodeMaps map]
    {
        get
        {
            int index = MapToStorageIndex[(int)map];
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(map), map, "Specified map is not supported.");
            return _storage.AsSpan(index * 256, 256);
        }
    }

    /// <summary>
    /// Get a single opcode map cell at a specified location.
    /// </summary>
    /// <param name="index">The location in the overarching opcode map to get a cell for.</param>
    /// <remarks>
    /// If <c>null</c>, the specified location is not registered yet.
    /// </remarks>
    public ref OpmapCell? this[OpmapCellIndex index] =>
        ref _storage[index.ToFlattenedOpmapIndex()];
}
