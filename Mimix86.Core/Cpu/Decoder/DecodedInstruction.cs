/* =============================================================================
 * File:   DecodedInstruction.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Contains the entirety of a decoded x86 instruction.
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

using DotNext;
using System;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents a single decoded instruction.
/// </summary>
public class DecodedInstruction
{
    /// <summary>
    /// The ID of the actual opcode that will be executed.
    /// </summary>
    public Opcode Opcode { get; set; } = Opcode.Error;

    /// <summary>
    /// The actual execution handler that will be called after decoding completes.
    /// </summary>
    public ExecutionHandler? ExecutionHandler { get; set; } = null;

    /// <summary>
    /// The raw instruction stream bytes.
    /// </summary>
    public byte[] RawInstruction = Array.Empty<byte>();

    // TODO: segments

    // future: CET segment

    /// <summary>
    /// The current processor mode.
    /// This, combined with <see cref="ASizeOverride" /> or <see cref="OSizeOverride" /> can be used to determine the
    ///   effective address and operand size.
    /// </summary>
    public int ProcessorMode { get; }

    /// <summary>
    /// Get or set a value indicating if the <c>ASIZE</c> (<c>0x67</c>) prefix was seen.
    /// </summary>
    public bool ASizeOverride { get; set; }

    /// <summary>
    /// Get or set a value indicating if the <c>OSIZE</c> (<c>0x66</c>) prefix was seen.
    /// </summary>
    public bool OSizeOverride { get; set; }

    /// <summary>
    /// Get or set the <c>REP</c> prefix that was first encountered, if any exist.
    /// </summary>
    public Optional<byte> RepPrefix { get; set; } = Optional<byte>.None;

    /// <summary>
    /// Get or set the ModR/M byte, if it exists.
    /// </summary>
    public Optional<byte> ModRM { get; set; } = Optional<byte>.None;

    /// <summary>
    /// Get or set the SIB byte, if it exists.
    /// </summary>
    public Optional<byte> Sib { get; set; } = Optional<byte>.None;

    /// <summary>
    /// Get or set the displacement, if it exists.
    /// </summary>
    public int Displacement { get; set; } = 0;

    /// <summary>
    /// Get or set the segment portion of the <see cref="Immediate" />, for instructions that contain far pointers, if
    ///   it exists.
    /// </summary>
    public ushort ImmediateSegment { get; set; } = 0;

    /// <summary>
    /// Get or set the immediate (zero extended), if it exists.
    /// </summary>
    public ushort Immediate { get; set; } = 0;

    // ReSharper disable once CommentTypo
    // future: W, Z, B, LL, v'vvvv, and aaa

    /// <summary>
    /// Construct a new <see cref="DecodedInstruction" /> object with a specified CPU mode bit width.
    /// </summary>
    /// <param name="processorMode">The current bit width of the current CPU mode.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="processorMode" /> is not 16, 32, or 64.
    /// </exception>
    public DecodedInstruction(int processorMode)
    {
        if (processorMode is not (16 or 32 or 64))
            throw new ArgumentOutOfRangeException(nameof(processorMode), processorMode, "Unknown processor bit width.");

        ProcessorMode = processorMode;
    }
}
