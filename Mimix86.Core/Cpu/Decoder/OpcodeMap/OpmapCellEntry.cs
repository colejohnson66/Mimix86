/* =============================================================================
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
using OneOf;
using System;

namespace Mimix86.Core.Cpu.Decoder.OpcodeMap;

/// <summary>
/// Represents a single entry in an opcode map cell.
/// An entry consists of either an opcode to execute or a prefix to interpret.
/// </summary>
public readonly struct OpmapCellEntry
{
    /// <summary>
    /// Construct a new <see cref="OpmapCellEntry" /> with a specified opcode or prefix and decode flags.
    /// </summary>
    /// <param name="value">Either the opcode to execute or prefix to interpret.</param>
    /// <param name="flags">
    /// The flags that are required for this entry to be used.
    /// The default value is <see cref="OpmapCellEntryFlags.None" />.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// If <paramref name="value" /> is a <c>null</c> <see cref="Opcode" />.
    /// </exception>
    public OpmapCellEntry(OneOf<Opcode, Prefixes> value, OpmapCellEntryFlags flags = default)
    {
        // `default(OpmapCellEntryFlags)` is "none"
        if (value.IsT0)
            ArgumentNullException.ThrowIfNull(value.AsT0, nameof(value));

        Value = value;
        Flags = flags;
    }


    /// <summary>
    /// Get either the opcode to execute or prefix to interpret.
    /// </summary>
    public OneOf<Opcode, Prefixes> Value { get; }

    /// <summary>
    /// Get the flags that are required for this entry to be used.
    /// </summary>
    public OpmapCellEntryFlags Flags { get; }
}
