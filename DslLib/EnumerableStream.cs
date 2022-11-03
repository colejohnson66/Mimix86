using DotNext;
using System.Collections.Generic;
using System.IO;

namespace DslLib;

internal class EnumerableStream<T>
{
    private readonly IEnumerator<T> _input;
    private Optional<T> _peek = Optional<T>.None;

    public EnumerableStream(IEnumerable<T> enumerable)
    {
        _input = enumerable.GetEnumerator();
    }

    public Optional<T> Peek()
    {
        if (_peek.HasValue)
            return _peek;

        if (!_input.MoveNext())
            return Optional<T>.None;

        T value = _input.Current;
        _peek = new(value);
        return _peek;
    }

    public T Next()
    {
        if (_peek.HasValue)
        {
            T value = _peek.Value;
            _peek = Optional<T>.None;
            return value;
        }

        if (!_input.MoveNext())
            throw new EndOfStreamException();

        return _input.Current;
    }
}
