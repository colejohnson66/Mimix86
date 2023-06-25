/* =============================================================================
 * File:   Parser.cs
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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace SExpressionReader;

/// <summary>
/// Represents a parser for the DSL.
/// The DSL syntax is described in <c>README.md</c>.
/// </summary>
[PublicAPI]
public sealed class Parser : IDisposable
{
    private readonly Tokenizer _tokenizer;
    private readonly EnumerableStream<Token> _tokens;

    /// <summary>
    /// Construct a new parser for a specified string of text.
    /// </summary>
    /// <param name="input">The string of text to parse.</param>
    public Parser(string input)
    {
        _tokenizer = new(input);
        _tokens = new(_tokenizer.Tokenize());
    }

    private bool Match(TokenType type, [NotNullWhen(true)] out Token? tok)
    {
        if (_tokens.Peek() is Token peek && peek.Type == type)
        {
            tok = peek;
            _tokens.Next();
            return true;
        }

        tok = null;
        return false;
    }

    /// <summary>
    /// Parse out the input string into a series of expressions.
    /// </summary>
    /// <returns>An enumeration of expressions, one for each outer-most one in the input.</returns>
    public IEnumerable<Expression> Parse()
    {
        while (_tokens.Peek() is not null)
        {
            while (true)
            {
                Token? peek = _tokens.Peek();
                if (peek is null)
                    break; // EOF

                AtomOrExpression aoe = GetNextAtomOrExpression();
                if (aoe.IsAtom)
                    throw new InvalidDataException("All top-level parts must be expressions, not atoms.");
                yield return aoe.AsExpression();
            }
        }
    }

    private AtomOrExpression GetNextAtomOrExpression()
    {
        if (Match(TokenType.LeftParenthesis, out _))
            return GetExpression();

        // `GetExpression` ensured this token isn't next
        if (Match(TokenType.RightParenthesis, out _))
            throw new InvalidDataException();

        if (Match(TokenType.Null, out Token? _))
            return new Atom(null);

        if (Match(TokenType.String, out Token? tok))
            return GetString(tok);

        throw new InvalidDataException();
    }

    private static Atom GetString(Token tok) =>
        new(tok.Text);

    private Expression GetExpression()
    {
        // at this point, the open/left parenthesis has been consumed
        // consume nodes until we see a closing/right parenthesis

        List<AtomOrExpression> nodes = new();
        while (true)
        {
            Token? peek = _tokens.Peek();
            if (peek is null)
                throw new EndOfStreamException();

            if (peek.Type is TokenType.RightParenthesis)
            {
                _tokens.Next(); // eat the closing parenthesis
                break;
            }

            nodes.Add(GetNextAtomOrExpression());
        }

        return new(nodes);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _tokens.Dispose();
        _tokenizer.Dispose();
    }
}
