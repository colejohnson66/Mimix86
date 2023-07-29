/* =============================================================================
 * File:   Tokenizer.cs
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
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SExpressionReader;

internal sealed class Tokenizer : IDisposable
{
    private readonly StringReader _reader;
    private int _peeked = -1;

    public Tokenizer(string input)
    {
        _reader = new(input);
    }

    private int Peek()
    {
        if (_peeked >= 0)
            return _peeked;

        int c = _reader.Read();
        while (c is '\r') // normalize new-lines to '\n' form
            c = _reader.Read();

        _peeked = c;
        return c;
    }

    private int Read(bool throwOnEof = false)
    {
        if (_peeked >= 0)
        {
            int peek = _peeked;
            _peeked = -1;
            return peek;
        }

        int c = _reader.Read();
        while (c is '\r') // normalize new-lines to '\n' form
            c = _reader.Read();

        if (throwOnEof && c is -1)
            throw new EndOfStreamException("Unexpected EOF.");

        return c;
    }

    private bool Match(char c)
    {
        if (Peek() == c)
        {
            Read();
            return true;
        }

        return false;
    }

    public IEnumerable<Token> Tokenize()
    {
        while (Peek() is not -1)
        {
            // eat all spaces
            if (Match(' ') || Match('\n'))
                continue;

            if (Match('('))
            {
                yield return new(TokenType.LeftParenthesis, "(");
                continue;
            }

            if (Match(')'))
            {
                yield return new(TokenType.RightParenthesis, ")");
                continue;
            }

            if (Match(';'))
            {
                // comment through the rest of the line
                while (Read() is not (-1 or '\n'))
                { }
                continue;
            }

            if (Match('"'))
            {
                // quoted string
                StringBuilder str = new();
                while (true)
                {
                    if (Match('"'))
                        break;

                    int c = Read(true);
                    if (c is '\\')
                        c = Read(true); // any escaped characters are allowed
                    str.Append((char)c);
                }

                yield return new(TokenType.String, str.ToString());
            }
            else
            {
                // unquoted string
                StringBuilder str = new();
                while (true)
                {
                    if (Peek() is -1 or '\n' or ' ' or '(' or ')')
                        break;

                    str.Append((char)Read());
                }

                // unquoted strings can be null
                yield return new(TokenType.String, str.ToString());
            }
        }
    }


    public void Dispose() =>
        _reader.Dispose();
}
