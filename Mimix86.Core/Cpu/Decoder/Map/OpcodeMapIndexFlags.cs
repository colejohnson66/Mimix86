/* =============================================================================
 * File:   OpcodeMapIndexFlags.cs
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

namespace Mimix86.Core.Cpu.Decoder.Map;

/// <summary>
/// Represents the flags for an opcode map/byte index.
/// </summary>
public readonly struct OpcodeMapIndexFlags : IEquatable<OpcodeMapIndexFlags>
{
    /// <summary>
    /// Get a flag indicating if this opcode map/byte index has a ModR/M byte.
    /// </summary>
    public bool HasModRM { get; init; }


#pragma warning disable CS1591
    public static bool operator ==(OpcodeMapIndexFlags lhs, OpcodeMapIndexFlags rhs) =>
        lhs.HasModRM == rhs.HasModRM;

    public static bool operator !=(OpcodeMapIndexFlags lhs, OpcodeMapIndexFlags rhs) =>
        !(lhs == rhs);
#pragma warning restore CS1591

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj is OpcodeMapIndexFlags other && Equals(other);

    /// <inheritdoc />
    public bool Equals(OpcodeMapIndexFlags other) =>
        this == other;

    /// <inheritdoc />
    public override int GetHashCode() =>
        HasModRM.GetHashCode();
}
