/* =============================================================================
 * File:   DecoderStoreByOpcodeMap.cs
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
 *   Mimix86. If not, see <http://www.gnu.org/licenses/>.
 * =============================================================================
 */

using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mimix86.Core.Cpu.Decoder;

// stores a mapping from `DecoderMap` to an array of `T[256]`, but in a more compact form than a dictionary would

internal static class DecoderStoreByOpcodeMap
{
    // maps enum values to their index in the storage array
    public static readonly int[] MapToStorageIndex;
    public static readonly int SupportedMapCount;

    static DecoderStoreByOpcodeMap()
    {
        MapToStorageIndex = new int[Enum.GetValues<DecoderMap>().Length];
        MapToStorageIndex[(int)DecoderMap.OneByte] = 0;
        MapToStorageIndex[(int)DecoderMap.TwoByte] = -1;
        MapToStorageIndex[(int)DecoderMap._3DNow] = -1;
        MapToStorageIndex[(int)DecoderMap.ThreeByte0F38] = -1;
        MapToStorageIndex[(int)DecoderMap.ThreeByte0F3A] = -1;
        MapToStorageIndex[(int)DecoderMap.Drex0F24] = -1;
        MapToStorageIndex[(int)DecoderMap.Drex0F25] = -1;
        MapToStorageIndex[(int)DecoderMap.Drex0F7A] = -1;
        MapToStorageIndex[(int)DecoderMap.Drex0F7B] = -1;
        MapToStorageIndex[(int)DecoderMap.Vex0F] = -1;
        MapToStorageIndex[(int)DecoderMap.Vex0F38] = -1;
        MapToStorageIndex[(int)DecoderMap.Vex0F3A] = -1;
        MapToStorageIndex[(int)DecoderMap.Xop8] = -1;
        MapToStorageIndex[(int)DecoderMap.Xop9] = -1;
        MapToStorageIndex[(int)DecoderMap.XopA] = -1;
        MapToStorageIndex[(int)DecoderMap.L1OMScalar] = -1;
        MapToStorageIndex[(int)DecoderMap.L1OMVector] = -1;
        MapToStorageIndex[(int)DecoderMap.MvexEvex] = -1;
        MapToStorageIndex[(int)DecoderMap.MvexEvex0F] = -1;
        MapToStorageIndex[(int)DecoderMap.MvexEvex0F38] = -1;
        MapToStorageIndex[(int)DecoderMap.MvexEvex0F3A] = -1;
        MapToStorageIndex[(int)DecoderMap.MvexEvex5] = -1;
        MapToStorageIndex[(int)DecoderMap.MvexEvex6] = -1;

        SupportedMapCount = MapToStorageIndex.Max() + 1;
    }
}

internal sealed class DecoderStoreByOpcodeMap<T>
{
    // can't get a span of a 2D array, so use a 1D with a stride (how a 2D works internally)
    private readonly T[] _storage = new T[DecoderStoreByOpcodeMap.SupportedMapCount * 256];

    public Span<T> this[DecoderMap map] =>
        _storage.AsSpan(GetIndex(map, 0), 256);

    public T this[DecoderMap map, byte b]
    {
        get => _storage[GetIndex(map, b)];
        set => _storage[GetIndex(map, b)] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int GetIndex(DecoderMap map, byte b)
    {
        int index = DecoderStoreByOpcodeMap.MapToStorageIndex[(int)map];
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(map), map, "Specified opcode map is unsupported.");

        return index * 256 + b;
    }
}
