/* =============================================================================
 * File:   Parser.cs
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
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace DslLib;

/// <summary>
/// Represents a parser for the DSL.
/// The DSL syntax is described in <c>README</c>.
/// </summary>
[PublicAPI]
public class Parser
{
    private readonly EnumerableStream<Token> _tokens;

    /// <summary>
    /// Construct a new parser for a specified string of text.
    /// </summary>
    /// <param name="input">The string of text to parse.</param>
    public Parser(string input)
    {
        _tokens = new(new Tokenizer(input).Tokenize());
    }

    private bool Match(TokenType type, [NotNullWhen(true)] out Token? tok)
    {
        if (_tokens.Peek() is Token tok2 && tok2.Type == type)
        {
            tok = tok2;
            _tokens.Next();
            return true;
        }

        tok = null;
        return false;
    }

    /// <summary>
    /// Parse out the input string into a series of nodes, one for each line of the input.
    /// </summary>
    /// <returns>
    /// An <see cref="IEnumerable{T}" /> of <see cref="Node" /> objects, one for each line of the input that contained
    ///   content.
    /// </returns>
    public IEnumerable<Node> Parse()
    {
        while (_tokens.Peek() is not null)
        {
            List<Node> nodes = new();
            while (true)
            {
                Token? peek = _tokens.Peek();
                if (peek is null)
                    break; // EOF

                if (peek.Type is TokenType.NewLine)
                {
                    _tokens.Next();
                    break;
                }

                nodes.Add(GetNextNode());
            }

            yield return new(nodes.ToArray());
        }
    }

    private Node GetNextNode()
    {
        if (Match(TokenType.NewLine, out _))
            throw new InvalidOperationException(); // handled above

        if (Match(TokenType.LeftBracket, out _))
            return HandleArray();

        // `HandleArray` ensured this token isn't next
        if (Match(TokenType.RightBracket, out _))
            throw new InvalidDataException();

        if (Match(TokenType.String, out Token? tok))
            return HandleString(tok);

        throw new InvalidDataException();
    }

    private static Node HandleString(Token tok)
    {
        Debug.Assert(tok.Text is not null); // all string tokens should have text
        return new(tok.Text!);
    }

    private Node HandleArray()
    {
        // at this point, the left bracket has been consumed
        // consume nodes until we see a right bracket

        List<Node> nodes = new();
        while (true)
        {
            Token? peek = _tokens.Peek();
            if (peek is null)
                throw new EndOfStreamException();

            if (peek.Type is TokenType.RightBracket)
            {
                _tokens.Next();
                break;
            }

            nodes.Add(GetNextNode());
        }

        return new(nodes.ToArray());
    }
}
