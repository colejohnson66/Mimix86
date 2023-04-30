/* =============================================================================
 * File:   OpcodeMapEntry.cs
 * Author: Cole Tobin
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
    /// Construct a new <see cref="OpcodeMapEntry" /> for an entry with a specified opcode.
    /// The supported CPU range will be Pentium and newer.
    /// </summary>
    /// <param name="opcode">The ID of the actual opcode.</param>
    public OpcodeMapEntry(Opcode opcode)
        : this(opcode, 5.., DecodeFlags.None)
    {
        // should not be used until P5 support
        throw new NotSupportedException();
    }

    /// <summary>
    /// Construct a new <see cref="OpcodeMapEntry" /> for an entry with a specified opcode and decode flags.
    /// The supported CPU range will be Pentium and newer.
    /// </summary>
    /// <param name="opcode">The ID of the actual opcode.</param>
    /// <param name="flags">The required flags to decode to this opcode entry.</param>
    public OpcodeMapEntry(Opcode opcode, DecodeFlags flags)
        : this(opcode, 5.., flags)
    {
        // should not be used until P5 support
        throw new NotSupportedException();
    }

    /// <summary>
    /// Construct a new <see cref="OpcodeMapEntry" /> for an entry with a specified opcode and supported CPU range.
    /// </summary>
    /// <param name="opcode">The ID of the actual opcode.</param>
    /// <param name="supportedCpuLevels">
    /// The (inclusive) allowed range of CPU levels for this opcode to be supported.
    /// </param>
    public OpcodeMapEntry(Opcode opcode, Range supportedCpuLevels)
        : this(opcode, supportedCpuLevels, DecodeFlags.None)
    { }

    /// <summary>
    /// Construct a new <see cref="OpcodeMapEntry" /> for an entry with a specified opcode, supported CPU range, and
    ///   decode flags.
    /// </summary>
    /// <param name="opcode">The ID of the actual opcode.</param>
    /// <param name="supportedCpuLevels">
    /// The (inclusive) allowed range of CPU levels for this opcode to be supported.
    /// </param>
    /// <param name="flags">The required flags to decode to this opcode entry.</param>
    public OpcodeMapEntry(Opcode opcode, Range supportedCpuLevels, DecodeFlags flags)
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
        Flags = flags;
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
