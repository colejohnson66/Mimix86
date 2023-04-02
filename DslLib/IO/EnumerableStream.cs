/* =============================================================================
 * File:   EnumerableStream.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * <TODO>
 * =============================================================================
 * Copyright (c) 2022 Cole Tobin
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

using System.Collections.Generic;
using System.IO;

namespace DslLib.IO;

internal class EnumerableStream<T>
{
    private readonly IEnumerator<T> _input;
    private T? _peek = default;

    public EnumerableStream(IEnumerable<T> enumerable)
    {
        _input = enumerable.GetEnumerator();
    }

    public T? Peek()
    {
        if (_peek is not null)
            return _peek;

        if (!_input.MoveNext())
            return default;

        T value = _input.Current;
        _peek = value;
        return _peek;
    }

    public T Next()
    {
        if (_peek is not null)
        {
            T value = _peek;
            _peek = default;
            return value;
        }

        if (!_input.MoveNext())
            throw new EndOfStreamException();

        return _input.Current;
    }
}
