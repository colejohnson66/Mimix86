/* =============================================================================
 * File:   ByteEntry.cs
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

using System.Collections.Generic;

namespace Mimix86.Core.Cpu.Decoder.OpcodeMap;

/// <summary>
/// Represents a single opcode map plus byte entry in the opcode map store (<see cref="OpcodeMapStore" />).
/// Each entry contains flags to dictate decoding, and a list of possible opcodes that can result.
/// </summary>
internal sealed class OpcodeMapEntry
{
    // flags that dictate how the decoder should proceed when this opcode map/byte combo is reached
    public OpcodeMapFlags Flags { get; }

    // instructions that may be decoded from this opcode map/byte combo
    public List<OpcodeEntry> Instructions { get; } = new();


    public OpcodeMapEntry(OpcodeMapFlags flags)
    {
        Flags = flags;
    }
}
