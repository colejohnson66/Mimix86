/* =============================================================================
 * File:   DecodeByteStream.cs
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

using System;
using System.IO;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents the byte feeder for the decoder.
/// </summary>
public sealed class DecoderByteStream : Stream
{
    /// <inheritdoc />
    public override bool CanRead => true;

    /// <inheritdoc />
    public override bool CanSeek => false;

    /// <inheritdoc />
    public override bool CanWrite => false;

    /// <inheritdoc />
    public override long Length => throw new NotImplementedException();

    /// <inheritdoc />
    public override long Position
    {
        get => throw new NotImplementedException();
        set => throw new NotSupportedException();
    }

    /// <inheritdoc />
    public override void Flush() =>
        throw new NotSupportedException();

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
        ArgumentNullException.ThrowIfNull(buffer);
        if (offset < 0)
            throw new ArgumentOutOfRangeException(nameof(offset), offset, "Offset must be non-negative.");
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), count, "Count must be non-negative.");
        if (offset + count > buffer.Length)
            throw new ArgumentException("Offset plus count must not exceed the bounds of the buffer.");

        return Read(buffer.AsSpan(offset, count));
    }

    /// <inheritdoc />
    public override int Read(Span<byte> buffer) =>
        throw new NotImplementedException();

    /// <inheritdoc />
    public override int ReadByte() =>
        throw new NotImplementedException();

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin) =>
        throw new NotSupportedException();

    /// <inheritdoc />
    public override void SetLength(long value) =>
        throw new NotSupportedException();

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count) =>
        throw new NotSupportedException();
}
