﻿/* =============================================================================
 * File:   MemorySystem.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Represents the whole memory system.
 * Provides access to read/write/DMA through registered handlers.
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

using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS1591

namespace Mimix86.Core.Memory;

/// <summary>
/// Represents the entirety of the memory system.
/// </summary>
public static class MemorySystem
{
    /// <summary>
    /// A delegate to invoke when reading from a memory chunk.
    /// </summary>
    /// <param name="address">The physical address being read from.</param>
    /// <param name="buffer">The buffer to place the read bytes.</param>
    /// <param name="param">The "param" parameter when <see cref="RegisterMemoryHandler" /> was invoked.</param>
    public delegate bool ReadDelegate(ulong address, Span<byte> buffer, object? param);

    /// <summary>
    /// A delegate to invoke when writing to a memory chunk.
    /// </summary>
    /// <param name="address">The physical address being written to.</param>
    /// <param name="data">The bytes to write.</param>
    /// <param name="param">The "param" parameter when <see cref="RegisterMemoryHandler" /> was invoked.</param>
    public delegate bool WriteDelegate(ulong address, ReadOnlySpan<byte> data, object? param);

    /// <summary>
    /// A delegate to invoke when performing DMA access to a memory chunk.
    /// </summary>
    /// <param name="address">The physical address being read/written to.</param>
    /// <param name="param">The "param" parameter when <see cref="RegisterMemoryHandler" /> was invoked.</param>
    public delegate Span<byte> DmaDelegate(ulong address, object? param);


    public const ulong MAXIMUM_MEMORY_ADDRESS = 0xFF_FFFF;


    private const int BITS_PER_CHUNK_INDEX = 20; // 1M
    // each array element is a list of chunk handlers for the whole 1M
    private static readonly List<ChunkHandler>?[] Handlers = new List<ChunkHandler>?[MAXIMUM_MEMORY_ADDRESS >> BITS_PER_CHUNK_INDEX];


    /// <summary>
    /// Register a memory chunk handler for a specified range of addresses with specified read and optional write and
    ///   DMA delegates.
    /// </summary>
    /// <param name="start">The starting address this chunk will handle.</param>
    /// <param name="end">The (inclusive) ending address this chunk will handle.</param>
    /// <param name="read">The delegate to invoke when reading from this chunk.</param>
    /// <param name="write">
    /// The delegate to invoke when writing to this chunk, or <c>null</c> for read-only chunks.
    /// </param>
    /// <param name="dma">
    /// The delegate to invoke for DMA access to this chunk, or <c>null</c> if DMA is unsupported.
    /// </param>
    /// <param name="param">
    /// A caller-defined object to be passed into the read/write/DMA delegates on all calls.
    /// </param>
    /// <exception cref="ArgumentException">
    /// If <paramref name="end" /> is less than <paramref name="start" />.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If <paramref name="end" /> is greater than <see cref="MAXIMUM_MEMORY_ADDRESS" />.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// If an existing handler already claimed ownership over the start/end rage.
    /// </exception>
    public static void RegisterMemoryHandler(ulong start, ulong end, ReadDelegate read, WriteDelegate? write, DmaDelegate? dma, object? param = null)
    {
        if (start > end)
            throw new ArgumentException("Starting address of a handler must be less than or equal to the ending address.");
        if (end > MAXIMUM_MEMORY_ADDRESS)
            throw new ArgumentException("End address must be lower than the maximum supported memory.");

        ulong firstIndex = start >> BITS_PER_CHUNK_INDEX;
        ulong lastIndex = end >> BITS_PER_CHUNK_INDEX;

        // check for overlapping ranges
        for (ulong i = firstIndex; i <= lastIndex; i++)
        {
            List<ChunkHandler>? handlers = Handlers[i];
            if (handlers is null)
                continue;

            if (handlers.Any(chunk => start <= chunk.End && end >= chunk.Start))
                throw new InvalidOperationException("Overlapping chunk.");
        }

        // register it
        ChunkHandler newHandler = new(start, end, read, write, dma, param);
        for (ulong i = firstIndex; i <= lastIndex; i++)
            GetOrCreateChunk(i).Add(newHandler);
    }

    private static List<ChunkHandler> GetOrCreateChunk(ulong index)
    {
        List<ChunkHandler>? list = Handlers[index];

        if (list is not null)
            return list;

        list = new();
        Handlers[index] = list;
        return list;
    }


    private record ChunkHandler(ulong Start, ulong End, ReadDelegate Read, WriteDelegate? Write, DmaDelegate? Dma, object? Parameter);
}
