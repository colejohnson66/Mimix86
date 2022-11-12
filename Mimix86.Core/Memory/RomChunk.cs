using System;
using System.IO;

namespace Mimix86.Core.Memory;

/// <summary>
/// Represents a <see cref="MemoryChunk" /> for a ROM.
/// </summary>
public class RomChunk : MemoryChunk
{
    private readonly byte[] _rom;

    /// <summary>
    /// Construct a new <see cref="RomChunk" /> from a specified starting address and a <see cref="Stream" />.
    /// </summary>
    /// <param name="start">The starting physical address that this <see cref="RomChunk" /> will handle.</param>
    /// <param name="input">The ROM.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="start" /> plus <see cref="input" />'s length is greater than the maximum supported physical
    ///   address.
    /// </exception>
    public RomChunk(PhysicalAddress start, Stream input)
        : base(start)
    {
        ArgumentNullException.ThrowIfNull(input);

        EndAddress = new(start.Value + (ulong)input.Length - 1);
        CanWrite = false;
        CanDma = false;

        _rom = input.ReadFully();
    }


    /// <inheritdoc />
    public override bool Read(PhysicalAddress address, Span<byte> buffer)
    {
        int start = (int)(address.Value - StartAddress.Value);

        _rom.AsSpan(start, buffer.Length).CopyTo(buffer);
        return true;
    }

    /// <inheritdoc />
    public override bool Write(PhysicalAddress address, ReadOnlySpan<byte> data) =>
        throw new NotSupportedException("Cannot write to a ROM.");

    /// <inheritdoc />
    public override bool Dma(PhysicalAddress address, out Span<byte> span) =>
        throw new NotSupportedException("Cannot write to a ROM.");
}
