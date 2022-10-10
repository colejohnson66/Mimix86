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

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents a single opcode entry and its <see cref="DecodeFlags" /> in the opcode maps.
/// </summary>
/// <param name="Opcode">The ID of the actual opcode.</param>
/// <param name="Flags">The required flags to decode to this opcode entry.</param>
[PublicAPI]
public record OpcodeMapEntry(
    Opcode Opcode,
    DecodeFlags Flags)
{
    /// <summary>
    /// Construct a new <see cref="OpcodeMapEntry" /> for an entry with no flags.
    /// </summary>
    /// <param name="opcode">The ID of the actual opcode.</param>
    [SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
    public OpcodeMapEntry(Opcode opcode)
        : this(opcode, new DecodeFlags())
    { }

    /// <summary>
    /// Construct a new <see cref="OpcodeMapEntry" /> for an entry with specified flags.
    /// </summary>
    /// <param name="opcode">The ID of the actual opcode.</param>
    /// <param name="flags">
    /// The required flags to decode to this opcode entry.
    /// These will be passed verbatim to the constructor of <see cref="DecodeFlags" />.
    /// </param>
    [SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
    public OpcodeMapEntry(Opcode opcode, ulong flags)
        : this(opcode, new DecodeFlags(flags))
    { }
}
