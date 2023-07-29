/* =============================================================================
 * File:   Atom.cs
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

using JetBrains.Annotations;
using System;
using System.Globalization;

namespace SExpressionReader;

/// <summary>
/// Represents an atom in an S-expression.
/// </summary>
[PublicAPI]
public readonly struct Atom
    : IEquatable<Atom>
{
    /// <summary>
    /// Get the string value of this atom.
    /// </summary>
    public string Value { get; }

    internal Atom(string value)
    {
        Value = value;
    }


    /// <summary>
    /// Parse this atom into a specified type.
    /// </summary>
    /// <typeparam name="T">The type to get the value as.</typeparam>
    /// <returns>The internal value, cast to <typeparamref name="T" />.</returns>
    /// <exception cref="InvalidCastException">
    /// If the internal value is not assignable to a variable of <typeparamref name="T" />.
    /// </exception>
    public T As<T>()
        where T : ISpanParsable<T> =>
        T.Parse(Value, CultureInfo.InvariantCulture);


    // CS1591 is documentation comments
#pragma warning disable CS1591

    public static bool operator ==(Atom lhs, Atom rhs)
    {
        object? left = lhs.Value;
        object? right = rhs.Value;
        if (ReferenceEquals(left, right))
            return true;
        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Atom left, Atom right) =>
        !(left == right);

#pragma warning restore CS1591

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj is Atom other && this == other;

    /// <inheritdoc />
    public bool Equals(Atom other) =>
        this == other;

    /// <inheritdoc />
    public override int GetHashCode() =>
        Value?.GetHashCode() ?? 0;


    /// <inheritdoc />
    public override string ToString()
    {
        if (Value.Contains('"'))
            return $"\"{Value.Replace("\"", "\\\"")}\""; // quote it and escape inner quotes
        if (Value.Contains(' '))
            return $"\"{Value}\"";

        return Value; // no quotes or spaces, so no quoting needed
    }
}
