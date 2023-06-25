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
 *   Mimix86. If not, see <http://www.gnu.org/licenses/>.
 * =============================================================================
 */

using JetBrains.Annotations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace SExpressionReader;

/// <summary>
/// Represents an atom in an S-expression.
/// </summary>
[PublicAPI]
public readonly struct Atom
    : IEquatable<Atom>
{
    private readonly object? _value;

    internal Atom(object? value)
    {
        _value = value;
    }


    /// <summary>
    /// Get the internal value of this atom.
    /// </summary>
    /// <typeparam name="T">The type to get the value as.</typeparam>
    /// <returns>The internal value, cast to <typeparamref name="T" />.</returns>
    /// <exception cref="InvalidCastException">
    /// If the internal value is not assignable to a variable of <typeparamref name="T" />.
    /// </exception>
    public T As<T>()
    {
        if (_value is null)
        {
            // if `T` is not nullable but `_value` is, the cast is impossible
            if (default(T) is null)
                return default!;
            throw new InvalidCastException();
        }

        if (_value.GetType().IsAssignableTo(typeof(T)))
            return (T)_value;

        throw new InvalidCastException();
    }

    /// <summary>
    /// Try to cast the internal value of this atom to <typeparamref name="T" />.
    /// </summary>
    /// <param name="value">
    /// If this function returns <c>true</c>, this contains the internal value of this atom after casting to
    ///   <typeparamref name="T" />.
    /// </param>
    /// <typeparam name="T">The type to get the value as.</typeparam>
    /// <returns><c>true</c> if the value was cast successfully; <c>false</c> otherwise.</returns>
    public bool TryAs<T>([MaybeNullWhen(true)] out T value)
    {
        value = default!;

        if (_value is null)
        {
            // if `T` is not nullable but `_value` is, the cast is impossible
            return default(T) is null;
        }

        if (_value.GetType().IsAssignableTo(typeof(T)))
        {
            value = (T)_value;
            return true;
        }

        return false;
    }


    // CS1591 is documentation comments
#pragma warning disable CS1591

    public static bool operator ==(Atom lhs, Atom rhs)
    {
        object? left = lhs._value;
        object? right = rhs._value;
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
        _value?.GetHashCode() ?? 0;


    /// <inheritdoc />
    public override string ToString()
    {
        if (_value is null)
            return "NULL";

        string? str = _value.ToString();
        if (str is null)
            return "";

        if (str.Contains('"'))
            return $"\"{str.Replace("\"", "\\\"")}\""; // quote it and escape inner quotes
        if (str.Contains(' '))
            return $"\"{str}\"";

        return str; // no quotes or spaces, so no quoting needed
    }
}
