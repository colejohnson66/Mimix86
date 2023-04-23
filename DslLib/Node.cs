/* =============================================================================
 * File:   Node.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * <TODO>
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

using JetBrains.Annotations;
using System;
using System.Text;

namespace DslLib;

/// <summary>
/// Represents a single node in the DSL.
/// </summary>
[PublicAPI]
public sealed class Node
{
    /// <summary>
    /// The contents of this node, if it is textual; <c>null</c> otherwise.
    /// </summary>
    /// <remarks>If this is <c>null</c>, <see cref="Children" /> is non-<c>null</c>, and vice versa.</remarks>
    public string? Text { get; }

    /// <summary>
    /// The children of this node, if it is an array; <c>null</c> otherwise.
    /// </summary>
    /// <remarks>If this is <c>null</c>, <see cref="Text" /> is non-<c>null</c>, and vice versa.</remarks>
    public Node[]? Children { get; }


    internal Node(string contents)
    {
        Text = contents;
        Children = null;
    }

    internal Node(Node[] children)
    {
        Text = null;
        Children = children;
    }


    /// <summary>
    /// Get the <see cref="Text" /> value of this node.
    /// </summary>
    /// <returns>The value of <see cref="Text" />.</returns>
    /// <exception cref="InvalidOperationException">If <see cref="Text" /> is <c>null</c>.</exception>
    public string AsTest()
    {
        if (Text is null)
            throw new InvalidOperationException("Cannot treat an array node as text.");
        return Text;
    }

    /// <summary>
    /// Get the <see cref="Children" /> value of this node.
    /// </summary>
    /// <returns>The value of <see cref="Children" />.</returns>
    /// <exception cref="InvalidOperationException">If <see cref="Children" /> is <c>null</c>.</exception>
    public Node[] AsArray()
    {
        if (Children is null)
            throw new InvalidOperationException("Cannot treat a text node as an array.");
        return Children;
    }


    /// <inheritdoc />
    public override string ToString()
    {
        if (Text is not null)
        {
            if (!Text.Contains(' ') && !Text.Contains('"'))
                return Text;

            string quoted = Text.Replace("\"", "\\\"");
            return $"\"{quoted}\"";
        }

        StringBuilder builder = new("[");
        for (int i = 0; i < Children!.Length; i++)
        {
            Node node = Children![i];
            builder.Append(node);

            if (i != Children.Length - 1)
                builder.Append(' ');
        }
        builder.Append(']');
        return builder.ToString();
    }
}
