/* =============================================================================
 * File:   OpcodeMapStore.cs
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

// TODO: remove
#pragma warning disable CS1591

namespace Mimix86.Core.Cpu.Decoder.OpcodeMap;

/// <summary>
/// Represents the "store" for the opcode map.
/// This maps individual bytes to operations that instruct the decoder (<see cref="Decoder" />) how to proceed.
/// </summary>
public class OpcodeMapStore
{
    private readonly Prefixes.OneByte?[] _prefixes1B = new Prefixes.OneByte?[256];
    private readonly Prefixes.TwoByte?[] _prefixes2B = new Prefixes.TwoByte?[256];
    private readonly OpcodeMapDictionary<OpcodeMapEntry?> _map = new();

    public void RegisterOneBytePrefix(byte b, Prefixes.OneByte prefix)
    {
        Prefixes.OneByte? entry = _prefixes1B[b];
        if (entry is not null)
        {
            if (entry != prefix)
                throw new InvalidOperationException($"One-byte prefix at [{b:X2}] is already registered as {entry}.");
            return;
        }

        // check for conflict with opcodes
        if (_map[OpcodeMaps.OneByte][b] is not null)
            throw new InvalidOperationException($"One-byte prefix at [{b:X2}] is already registered for instructions.");

        _prefixes1B[b] = prefix;
    }

    public void RegisterTwoBytePrefix(byte b, Prefixes.TwoByte prefix)
    {
        Prefixes.TwoByte? entry = _prefixes2B[b];
        if (entry is not null)
        {
            if (entry != prefix)
                throw new InvalidOperationException($"Two-byte prefix at [{b:X2}] is already registered as {entry}.");
            return;
        }

        // check for conflict with opcodes
        if (_map[OpcodeMaps.TwoByte][b] is not null)
            throw new InvalidOperationException($"Two-byte prefix at [{b:X2}] is already registered for instructions.");

        _prefixes2B[b] = prefix;
    }

    public void RegisterOpcodeMapIndex(OpcodeMapIndex index, OpcodeMapFlags flags)
    {
        // check for conflict with prefixes
        if (index.Map is OpcodeMaps.OneByte && _prefixes1B[index.Byte] is not null)
            throw new InvalidOperationException($"One-byte opcode at [{index.Byte:X2}] is already registered as prefix, {_prefixes1B[index.Byte]}.");
        if (index.Map is OpcodeMaps.TwoByte && _prefixes2B[index.Byte] is not null)
            throw new InvalidOperationException($"Two-byte opcode at [{index.Byte:X2}] is already registered as prefix, {_prefixes2B[index.Byte]}.");

        // check for conflict with previous registration
        OpcodeMapEntry? mapEntry = _map[index];
        if (mapEntry is not null)
        {
            if (mapEntry.Flags != flags)
                throw new InvalidOperationException($"Opcode map index, {index}, has already been registered differently.");
            return;
        }

        _map[index] = new(flags);
    }

    public void RegisterOpcodeEntries(OpcodeMapIndex index, OpcodeEntry entry)
    {
        // check for conflict with prefixes
        if (index.Map is OpcodeMaps.OneByte && _prefixes1B[index.Byte] is not null)
            throw new InvalidOperationException($"One-byte opcode at [{index.Byte:X2}] is already registered as prefix, {_prefixes1B[index.Byte]}.");
        if (index.Map is OpcodeMaps.TwoByte && _prefixes2B[index.Byte] is not null)
            throw new InvalidOperationException($"Two-byte opcode at [{index.Byte:X2}] is already registered as prefix, {_prefixes2B[index.Byte]}.");

        OpcodeMapEntry? mapEntry = _map[index];
        if (mapEntry is null)
            throw new InvalidOperationException($"Opcode map index, {index}, has not been registered yet.");

        // TODO: check for conflicting decode flags?
        mapEntry.Instructions.Add(entry);
    }

    public void RegisterOpcodeEntries(OpcodeMapIndex index, params OpcodeEntry[] entries)
    {
        // check for conflict with prefixes
        if (index.Map is OpcodeMaps.OneByte && _prefixes1B[index.Byte] is not null)
            throw new InvalidOperationException($"One-byte opcode at [{index.Byte:X2}] is already registered as prefix, {_prefixes1B[index.Byte]}.");
        if (index.Map is OpcodeMaps.TwoByte && _prefixes2B[index.Byte] is not null)
            throw new InvalidOperationException($"Two-byte opcode at [{index.Byte:X2}] is already registered as prefix, {_prefixes2B[index.Byte]}.");

        OpcodeMapEntry? mapEntry = _map[index];
        if (mapEntry is null)
            throw new InvalidOperationException($"Opcode map index, {index}, has not been registered yet.");

        // TODO: check for conflicting decode flags?
        mapEntry.Instructions.AddRange(entries);
    }
}
