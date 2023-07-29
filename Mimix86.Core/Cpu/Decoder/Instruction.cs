/* =============================================================================
 * File:   Instruction.cs
 * Author: Cole Tobin
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
 *   Mimix86. If not, see <https://www.gnu.org/licenses/>.
 * =============================================================================
 */

using Mimix86.Core.Cpu.Isa;
using System;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents a single decoded instruction.
/// </summary>
public class Instruction
{
    /// <summary>
    /// The ID of the actual opcode that will be executed.
    /// </summary>
    public Opcode Opcode { get; set; } = Opcode.Undefined;

    /// <summary>
    /// The actual execution handler that will be called after decoding completes.
    /// </summary>
    public ExecutionHandler? ExecutionHandler { get; set; } = null;

    // /// <summary>
    // /// The raw instruction stream bytes.
    // /// </summary>
    // public byte[] RawInstruction = Array.Empty<byte>();

    /// <summary>
    /// The segment override prefix.
    /// </summary>
    /// <remarks>If multiple segment overrides exist in the instruction stream, the last one takes priority.</remarks>
    public SegmentNames? SegmentOverride { get; set; } = null;

    // future: CET segment

    /// <summary>
    /// The default operand size.
    /// </summary>
    public int DefaultOperandSize { get; }

    /// <summary>
    /// The default address size.
    /// </summary>
    public int DefaultAddressSize { get; }

    // /// <summary>
    // /// Get or set a value indicating if the <c>ASIZE</c> (<c>[67]</c>) prefix was seen.
    // /// </summary>
    // public bool ASizeOverride { get; set; }

    // /// <summary>
    // /// Get or set a value indicating if the <c>OSIZE</c> (<c>[66]</c>) prefix was seen.
    // /// </summary>
    // public bool OSizeOverride { get; set; }

    /// <summary>
    /// Get or set a value indicating if the <c>LOCK</c> (<c>[F0]</c>) prefix was seen.
    /// </summary>
    public bool LockPrefix { get; set; }

    /// <summary>
    /// Get or set the <c>REP</c> prefix that was first encountered, if any exist.
    /// </summary>
    public byte? RepPrefix { get; set; } = null;

    /// <summary>
    /// Get or set the ModR/M byte, if it exists.
    /// </summary>
    public byte? ModRM { get; set; } = null;

    // /// <summary>
    // /// Get or set the SIB byte, if it exists.
    // /// </summary>
    // public Optional<byte> Sib { get; set; } = Optional<byte>.None;

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
    /// Construct a new <see cref="Instruction" /> object with a specified CPU mode bit width.
    /// </summary>
    /// <param name="defaultOperandSize">The default operand size.</param>
    /// <param name="defaultAddressSize">The default address size.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If any of the following are <c>true</c>:
    /// <list type="bullet">
    ///   <item>if <paramref name="defaultOperandSize" /> is not 16, 32, or 64</item>
    ///   <item>if <paramref name="defaultAddressSize" /> is not 16, 32, or 64</item>
    /// </list>
    /// </exception>
    public Instruction(int defaultOperandSize, int defaultAddressSize)
    {
        if (defaultOperandSize is not (16 or 32 or 64))
            throw new ArgumentOutOfRangeException(nameof(defaultOperandSize), defaultOperandSize, "Unknown operand size.");
        if (defaultAddressSize is not (16 or 32 or 64))
            throw new ArgumentOutOfRangeException(nameof(defaultAddressSize), defaultAddressSize, "Unknown address size.");

        DefaultOperandSize = defaultOperandSize;
        DefaultAddressSize = defaultAddressSize;
    }
}
