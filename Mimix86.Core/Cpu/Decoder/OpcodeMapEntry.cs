/* =============================================================================
 * File:   OpcodeMapEntry.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * A single entry in the opcode maps.
 * =============================================================================
 * Copyright (c) 2022-2023 Cole Tobin
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
    /// Construct a new <see cref="OpcodeMapEntry" /> for an entry with a specified supported CPU range and specified
    ///   decode flags.
    /// </summary>
    /// <param name="opcode">The ID of the actual opcode.</param>
    /// <param name="supportedCpuLevels">
    /// The (inclusive) allowed range of CPU levels for this opcode to be supported.
    /// </param>
    /// <param name="flags">
    /// The required flags to decode to this opcode entry, or <c>null</c> for <see cref="DecodeFlags.None" />.
    /// </param>
    public OpcodeMapEntry(Opcode opcode, Range supportedCpuLevels, DecodeFlags? flags = null)
    {
        if (supportedCpuLevels.Start.Value > 5 || supportedCpuLevels.End.Value > 5)
        {
            throw new ArgumentOutOfRangeException(
                nameof(supportedCpuLevels),
                supportedCpuLevels,
                "Both ends of the supported CPU level range must be less than or equal to five.");
        }
        if (supportedCpuLevels.Start.Value > supportedCpuLevels.End.Value)
            throw new ArgumentException("Start of the supported CPU level range must be before the end.", nameof(supportedCpuLevels));

        Opcode = opcode;
        SupportedCpuLevels = supportedCpuLevels;
        Flags = flags ?? DecodeFlags.None;
    }


    /// <summary>The ID of the actual opcode.</summary>
    public Opcode Opcode { get; init; }

    /// <summary>The (inclusive) allowed range of CPU levels for this opcode to be supported.</summary>
    /// <remarks>
    /// This is an <em>inclusive</em> range.
    /// In other words, the <see cref="Range.End" /> value is <em>also</em> a supported CPU level.
    /// </remarks>
    public Range SupportedCpuLevels { get; init; }

    /// <summary>The required flags to decode to this opcode entry.</summary>
    public DecodeFlags Flags { get; init; }
}
