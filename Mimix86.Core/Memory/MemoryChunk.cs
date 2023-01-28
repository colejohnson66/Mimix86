/* =============================================================================
 * File:   MemoryChunk.cs
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

using System;

namespace Mimix86.Core.Memory;

/// <summary>
/// Represents the base class that all memory chunks must inherit from.
/// </summary>
public abstract class MemoryChunk
{
    /// <summary>
    /// Construct a new <see cref="MemoryChunk" /> with an unspecified size.
    /// </summary>
    /// <param name="start">The starting physical address that this memory chunk will handle.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="start" /> is greater than the maximum supported physical address.
    /// </exception>
    protected MemoryChunk(PhysicalAddress start)
    {
        if (start.Value > Config.MAXIMUM_PHYSICAL_ADDRESS)
            throw new ArgumentOutOfRangeException(nameof(start), start, "Start address must be lower than the maximum supported memory.");

        StartAddress = start;
    }

    /// <summary>
    /// Construct a new <see cref="MemoryChunk" />.
    /// </summary>
    /// <param name="start">The starting physical address that this memory chunk will handle.</param>
    /// <param name="end">The (inclusive) physical address that this memory chunk will handle.</param>
    /// <exception cref="ArgumentException">
    /// If <paramref name="end" /> is less or equal to than <paramref name="start" />.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="start" /> is greater than the maximum supported physical address.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="end" /> is greater than the maximum supported physical address.
    /// </exception>
    protected MemoryChunk(PhysicalAddress start, PhysicalAddress end)
    {
        if (start >= end)
            throw new ArgumentException("Starting address of a handler must be less than or equal to the ending address.");
        if (start.Value > Config.MAXIMUM_PHYSICAL_ADDRESS)
            throw new ArgumentOutOfRangeException(nameof(start), start, "Start address must be lower than the maximum supported memory.");
        if (end.Value > Config.MAXIMUM_PHYSICAL_ADDRESS)
            throw new ArgumentOutOfRangeException(nameof(end), end, "End address must be lower than the maximum supported memory.");

        StartAddress = start;
        EndAddress = end;
    }


    /// <summary>
    /// Get the starting physical address that this memory chunk will handle.
    /// </summary>
    public PhysicalAddress StartAddress { get; }

    /// <summary>
    /// Get the (inclusive) physical ending address that this memory chunk will handle.
    /// </summary>
    public PhysicalAddress EndAddress { get; protected set; }


    /// <summary>
    /// Get a boolean indicating if this memory chunk is writable.
    /// </summary>
    public bool CanWrite { get; protected set; }

    /// <summary>
    /// Get a boolean indicating if this memory chunk supports DMA.
    /// </summary>
    public bool CanDma { get; protected set; }


    /// <summary>
    /// Read from this memory chunk.
    /// </summary>
    /// <param name="address">The physical address marking the beginning of the memory being read from.</param>
    /// <param name="buffer">The buffer to place the read bytes.</param>
    /// <returns><c>true</c> if the data was read successfully; <c>false</c> otherwise.</returns>
    /// <remarks>
    /// The output buffer will never extend past the end of the handler.
    /// In other words, <paramref name="address" /> plus <paramref name="buffer" />'s length will never extend past
    ///   <see cref="EndAddress" />.
    /// </remarks>
    public abstract bool Read(PhysicalAddress address, Span<byte> buffer);

    /// <summary>
    /// Write into this memory chunk.
    /// </summary>
    /// <param name="address">The address marking the beginning of the memory being written to.</param>
    /// <param name="data">The bytes to write.</param>
    /// <returns><c>true</c> if the data was written successfully; <c>false</c> otherwise.</returns>
    /// <remarks>
    /// The output buffer will never extend past the end of the handler.
    /// In other words, <paramref name="address" /> plus <paramref name="data" />'s length will never extend past
    ///   <see cref="EndAddress" />.
    /// </remarks>
    public abstract bool Write(PhysicalAddress address, ReadOnlySpan<byte> data);

    /// <summary>
    /// Perform a DMA access to the backing array buffer.
    /// </summary>
    /// <param name="address">The address marking the beginning of the memory being read/written to.</param>
    /// <param name="span">
    /// If the DMA request was successful, a <see cref="Span{T}">Span&lt;byte&gt;</see> to the backing memory array,
    ///   beginning at <paramref name="address" />.
    /// </param>
    /// <returns><c>true</c> if the DMA request was successful; <c>false</c> otherwise.</returns>
    public abstract bool Dma(PhysicalAddress address, out Span<byte> span);
}
