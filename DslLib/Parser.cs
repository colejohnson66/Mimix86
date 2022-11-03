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
        if (_tokens.Peek().TryGet(out tok) && tok.Type == type)
        {
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
        while (_tokens.Peek().HasValue)
        {
            List<Node> nodes = new();
            while (true)
            {
                if (!_tokens.Peek().HasValue)
                    break; // EOF

                if (_tokens.Peek().Value.Type is TokenType.NewLine)
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
            if (!_tokens.Peek().HasValue)
                throw new EndOfStreamException();

            if (_tokens.Peek().Value.Type is TokenType.RightBracket)
            {
                _tokens.Next();
                break;
            }

            nodes.Add(GetNextNode());
        }

        return new(nodes.ToArray());
    }
}
