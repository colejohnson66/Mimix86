﻿/* =============================================================================
 * File:   OpcodeDecodeInfo.cs
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

using Mimix86.Core.Cpu.Isa;
using System;

namespace Mimix86.Core.Cpu.Decoder.OpcodeMap;

/// <summary>
/// Represents a single opcode in the decoder as its <see cref="Opcode" /> and <see cref="OpmapCellEntryFlags" />.
/// </summary>
public readonly struct OpmapCellEntry
{
    /// <summary>
    /// Construct a new <see cref="OpmapCellEntry" /> with a specified opcode, and no decode flags.
    /// </summary>
    /// <param name="opcode">The ID of the actual opcode.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="opcode" /> is <c>null</c>.</exception>
    public OpmapCellEntry(Opcode opcode)
        : this(opcode, OpmapCellEntryFlags.None)
    { }

    /// <summary>
    /// Construct a new <see cref="OpmapCellEntry" /> with a specified opcode and decode flags.
    /// </summary>
    /// <param name="opcode">The ID of the actual opcode.</param>
    /// <param name="flags">The required flags to decode to this opcode entry.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="opcode" /> is <c>null</c>.</exception>
    public OpmapCellEntry(Opcode opcode, OpmapCellEntryFlags flags)
    {
        ArgumentNullException.ThrowIfNull(opcode);

        Opcode = opcode;
        Flags = flags;
    }


    /// <summary>The actual opcode to execute.</summary>
    public Opcode Opcode { get; }

    /// <summary>The required flags to decode to this opcode entry.</summary>
    public OpmapCellEntryFlags Flags { get; init; }
}