/* =============================================================================
 * File:   PhysicalAddress.cs
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
 *   Mimix86. If not, see <https://www.gnu.org/licenses/>.
 * =============================================================================
 */

using System;

namespace Mimix86.Core;

/// <summary>
/// Represents a "physical address".
/// </summary>
[PublicAPI]
public struct PhysicalAddress
    : IEquatable<PhysicalAddress>
{
    /// <summary>
    /// Construct a new <see cref="PhysicalAddress" /> with a specified value.
    /// </summary>
    /// <param name="value">The physical address.</param>
    public PhysicalAddress(ulong value)
    {
        Value = value & Config.MAXIMUM_PHYSICAL_ADDRESS;
    }


    /// <summary>
    /// Get this <see cref="PhysicalAddress" />'s value as a <see cref="ulong" />.
    /// </summary>
    public ulong Value { get; }


    #region Casting

    /// <summary>
    /// Cast a <see cref="PhysicalAddress" /> object to a <see cref="ulong" />.
    /// </summary>
    /// <param name="address">The <see cref="PhysicalAddress" /> to cast.</param>
    /// <returns><paramref name="address" />'s <see cref="Value" />.</returns>
    public static implicit operator ulong(PhysicalAddress address) =>
        address.Value;

    /// <summary>
    /// Cast a <see cref="ulong" /> to a new <see cref="PhysicalAddress" /> object.
    /// </summary>
    /// <param name="address">The address to get a <see cref="PhysicalAddress" /> for.</param>
    /// <returns>A new <see cref="PhysicalAddress" /> with a value of <paramref name="address" />.</returns>
    public static explicit operator PhysicalAddress(ulong address) =>
        new(address);

    #endregion

    #region Comparison

    /// <summary>Compare two <see cref="PhysicalAddress" /> objects for equality.</summary>
    public static bool operator ==(PhysicalAddress lhs, PhysicalAddress rhs) =>
        lhs.Value == rhs.Value;

    /// <summary>Compare two <see cref="PhysicalAddress" /> objects for inequality.</summary>
    public static bool operator !=(PhysicalAddress lhs, PhysicalAddress rhs) =>
        lhs.Value != rhs.Value;

    /// <summary>
    /// Compare two <see cref="PhysicalAddress" /> objects to see if the first is greater than the second.
    /// </summary>
    public static bool operator >(PhysicalAddress lhs, PhysicalAddress rhs) =>
        lhs.Value > rhs.Value;

    /// <summary>
    /// Compare two <see cref="PhysicalAddress" /> objects to see if the first is less than the second.
    /// </summary>
    public static bool operator <(PhysicalAddress lhs, PhysicalAddress rhs) =>
        lhs.Value < rhs.Value;

    /// <summary>
    /// Compare two <see cref="PhysicalAddress" /> objects to see if the first is greater than or equal to the second.
    /// </summary>
    public static bool operator >=(PhysicalAddress lhs, PhysicalAddress rhs) =>
        lhs.Value >= rhs.Value;

    /// <summary>
    /// Compare two <see cref="PhysicalAddress" /> objects to see if the first is less than or equal to the second.
    /// </summary>
    public static bool operator <=(PhysicalAddress lhs, PhysicalAddress rhs) =>
        lhs.Value <= rhs.Value;

    #endregion


    /// <inheritdoc />
    public bool Equals(PhysicalAddress other) =>
        Value == other.Value;

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj is PhysicalAddress other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() =>
        Value.GetHashCode();

    /// <inheritdoc />
    public override string ToString() =>
        Value.ToString("x8"); // $"{Value >> 32:x8}_{Value:x8}";
}
