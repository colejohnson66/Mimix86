/* =============================================================================
 * File:   AtomOrExpression.cs
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
/// Represents a child of an expression (<see cref="Expression" />) that could be either an atom (<see cref="Atom" />)
///   or another expression.
/// </summary>
[PublicAPI]
public readonly struct AtomOrExpression
{
    private readonly Atom _atom;
    private readonly Expression? _expression;

    internal AtomOrExpression(Atom atom)
    {
        _atom = atom;
    }

    internal AtomOrExpression(Expression expression)
    {
        _expression = expression;
    }


    // CS1591 is XML documentation comments
#pragma warning disable CS1591

    public static implicit operator AtomOrExpression(Atom atom) =>
        new(atom);

    public static implicit operator AtomOrExpression(Expression expression)
    {
        ArgumentNullException.ThrowIfNull(expression);
        return new(expression);
    }

#pragma warning restore CS1591


    /// <summary>
    /// Get a boolean indicating if this <see cref="AtomOrExpression" /> is an atom.
    /// </summary>
    [MemberNotNullWhen(false, nameof(_expression))]
    public bool IsAtom =>
        _expression is null; // `_atom` is never null, but `_expression` could be

    /// <summary>
    /// Get a boolean indicating if this <see cref="AtomOrExpression" /> is an expression.
    /// </summary>
    [MemberNotNullWhen(true, nameof(_expression))]
    public bool IsExpression =>
        _expression is not null;


    /// <summary>
    /// Get this <see cref="AtomOrExpression" /> as an atom.
    /// </summary>
    /// <returns>The internal value as an atom.</returns>
    /// <exception cref="InvalidCastException">If the internal value is an expression.</exception>
    public Atom AsAtom()
    {
        if (IsAtom)
            return _atom;
        throw new InvalidCastException("Cannot cast an expression to an atom.");
    }

    /// <summary>
    /// Get this <see cref="AtomOrExpression" /> as an expression.
    /// </summary>
    /// <returns>The internal value as an expression.</returns>
    /// <exception cref="InvalidCastException">If the internal value is an atom.</exception>
    public Expression AsExpression()
    {
        if (IsExpression)
            return _expression;
        throw new InvalidCastException("Cannot cast an atom to an expression.");
    }


    /// <inheritdoc />
    public override string ToString() =>
        IsAtom
            ? _atom.ToString()
            : _expression.ToString();
}
