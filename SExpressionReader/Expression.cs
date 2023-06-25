/* =============================================================================
 * File:   Expression.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SExpressionReader;

/// <summary>
/// Represents an expression in an S-expression.
/// Each expression contains zero or more "children", where each child is either an atom (<see cref="Atom" />) or
///   another expression.
/// </summary>
[PublicAPI]
public sealed class Expression : IReadOnlyList<AtomOrExpression>
{
    private readonly AtomOrExpression[] _children;

    internal Expression(IList<AtomOrExpression> children)
    {
        _children = children.ToArray();
    }


    /// <inheritdoc />
    public AtomOrExpression this[int index]
    {
        get
        {
            if (index < 0 || index >= _children.Length)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Index must be positive and less than the number of children.");
            return _children[index];
        }
    }

    /// <inheritdoc />
    public int Count => _children.Length;


    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    /// <inheritdoc />
    public IEnumerator<AtomOrExpression> GetEnumerator() =>
        ((IEnumerable<AtomOrExpression>)_children).GetEnumerator();


    /// <inheritdoc />
    public override string ToString()
    {
        StringBuilder builder = new("(");
        foreach (AtomOrExpression node in _children)
        {
            builder.Append(node.ToString());
            builder.Append(' ');
        }

        builder.Remove(builder.Length - 1, 1); // remove trailing space and
        builder.Append(')');                   // replace with closing parenthesis
        return builder.ToString();
    }
}
