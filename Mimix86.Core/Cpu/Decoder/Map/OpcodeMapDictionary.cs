/* =============================================================================
 * File:   OpcodeMapDictionary.cs
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
using System.Runtime.CompilerServices;

namespace Mimix86.Core.Cpu.Decoder.Map;

// stores a mapping from `OpcodeMaps` to an array of `T[256]`,
//   but in a more compact form than a dictionary would

internal static class OpcodeMapDictionary
{
    // maps enum values to their index in the storage array
    public static readonly int[] MapToStorageIndex;
    public static readonly int SupportedMapCount;

    static OpcodeMapDictionary()
    {
        MapToStorageIndex = new int[Enum.GetValues<OpcodeMaps>().Length];
        MapToStorageIndex[(int)OpcodeMaps.OneByte] = 0;
        MapToStorageIndex[(int)OpcodeMaps.TwoByte] = -1;
        MapToStorageIndex[(int)OpcodeMaps._3DNow] = -1;
        MapToStorageIndex[(int)OpcodeMaps.ThreeByte0F38] = -1;
        MapToStorageIndex[(int)OpcodeMaps.ThreeByte0F3A] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Drex0F24] = -1;
        MapToStorageIndex[(int)OpcodeMaps.Drex0F25] = -1;
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
}

internal sealed class OpcodeMapDictionary<T>
{
    // can't get a span of a 2D array, so use a 1D with a stride (how a 2D works internally)
    private const int ELEMENTS_PER_MAP = 256; // max(8 bits)
    private readonly T[] _storage = new T[OpcodeMapDictionary.SupportedMapCount * ELEMENTS_PER_MAP];

    public Span<T> this[OpcodeMaps map] =>
        _storage.AsSpan(GetIndex(new(map, 0)), ELEMENTS_PER_MAP);

    public ref T this[OpcodeMapIndex index] =>
        ref _storage[GetIndex(index)];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetIndex(OpcodeMapIndex index)
    {
        int offset = OpcodeMapDictionary.MapToStorageIndex[(int)index.Map];
        if (offset < 0)
            throw new ArgumentOutOfRangeException(nameof(index), index, "Specified opcode map is unsupported.");

        return offset * ELEMENTS_PER_MAP + index.Byte;
    }
}
