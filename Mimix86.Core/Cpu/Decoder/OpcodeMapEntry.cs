/* =============================================================================
 * File:   OpcodeMapEntry.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * A record containing a single entry in the opcode maps.
 * =============================================================================
 * Copyright (c) 2022 Cole Tobin
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

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents a single opcode entry and its <see cref="DecodeFlags" /> in the opcode maps.
/// </summary>
[PublicAPI]
public class OpcodeMapEntry
{
    /// <summary>
    /// Construct a new <see cref="OpcodeMapEntry" /> for an entry with no flags.
    /// </summary>
    /// <param name="opcode">The ID of the actual opcode.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="opcode" /> is <c>null</c>.</exception>
    public OpcodeMapEntry(Opcode opcode)
    {
        ArgumentNullException.ThrowIfNull(opcode);

        Opcode = opcode;
        Flags = new();
    }

    /// <summary>
    /// Construct a new <see cref="OpcodeMapEntry" /> for an entry with specified flags.
    /// </summary>
    /// <param name="opcode">The ID of the actual opcode.</param>
    /// <param name="flags">
    /// The required flags to decode to this opcode entry.
    /// These will be passed verbatim to the constructor of <see cref="DecodeFlags" />.
    /// </param>
    /// <exception cref="ArgumentNullException">If <paramref name="opcode" /> is <c>null</c>.</exception>
    public OpcodeMapEntry(Opcode opcode, ulong flags)
    {
        ArgumentNullException.ThrowIfNull(opcode);

        Opcode = opcode;
        Flags = new(flags);
    }

    /// <summary>
    /// Represents a single opcode entry and its <see cref="DecodeFlags" /> in the opcode maps.
    /// </summary>
    /// <param name="opcode">The ID of the actual opcode.</param>
    /// <param name="flags">The required flags to decode to this opcode entry.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="opcode" /> is <c>null</c>.</exception>
    public OpcodeMapEntry(Opcode opcode, DecodeFlags flags)
    {
        ArgumentNullException.ThrowIfNull(opcode);

        Opcode = opcode;
        Flags = flags;
    }

    /// <summary>The ID of the actual opcode.</summary>
    public Opcode Opcode { get; init; }

    /// <summary>The required flags to decode to this opcode entry.</summary>
    public DecodeFlags Flags { get; init; }
}
