using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace DslLib;

internal class Tokenizer
{
    private readonly StringReader _reader;
    private int _peeked = -1;
    private bool _lastEmittedTokenWasNewLine = true;

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
            return Interlocked.Exchange(ref _peeked, -1);

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
            if (Match(' '))
                continue;

            if (Match('\n'))
            {
                // don't output consecutive newlines
                if (_lastEmittedTokenWasNewLine)
                    continue;

                _lastEmittedTokenWasNewLine = true;
                yield return new(TokenType.NewLine);
                continue;
            }

            if (Match('['))
            {
                _lastEmittedTokenWasNewLine = false;
                yield return new(TokenType.LeftBracket);
                continue;
            }

            if (Match(']'))
            {
                _lastEmittedTokenWasNewLine = false;
                yield return new(TokenType.RightBracket);
                continue;
            }

            if (Match('#'))
            {
                // comment through the rest of the line
                while (Read() is not (-1 or '\n'))
                { }

                // don't output consecutive newlines
                if (_lastEmittedTokenWasNewLine)
                    continue;

                _lastEmittedTokenWasNewLine = true;
                yield return new(TokenType.NewLine);
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
                        c = Read(true);
                    str.Append((char)c);
                }

                _lastEmittedTokenWasNewLine = false;
                yield return new(TokenType.String, str.ToString());
            }
            else
            {
                // unquoted string
                StringBuilder str = new();
                while (true)
                {
                    if (Peek() is -1 or '\n' or ' ' or '[' or ']')
                        break;

                    str.Append((char)Read());
                }

                _lastEmittedTokenWasNewLine = false;
                yield return new(TokenType.String, str.ToString());
            }
        }
    }
}
