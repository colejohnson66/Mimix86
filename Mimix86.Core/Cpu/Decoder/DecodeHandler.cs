/* =============================================================================
 * File:   DecodeHandler.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Contains the definition for the `DecodeHandler` delegate.
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
/// The type of a function decode handler.
/// </summary>
/// <param name="byteStream">
/// The input byte stream beginning at the start of the instruction, and ending after 15 bytes or the end of the page
///   (whichever comes first).
/// </param>
/// <param name="opByte">
/// The byte that triggered the call to the handler (with normalized prefixes; see remarks).
/// </param>
/// <param name="instr">The decoded instruction object currently being built.</param>
/// <param name="ssePrefix">The first legacy SSE prefix, or <see cref="Optional{T}.None" /> if there wasn't any.</param>
/// <param name="opmapEntries">
/// The opcode map entries for <paramref name="opByte" />, or <c>null</c> if there aren't any.
/// </param>
/// <param name="bytesConsumed">The number of bytes consumed by the called function.</param>
/// <returns>The name of the opcode.</returns>
/// <remarks>
/// The "normalized" form of the opcode byte (<paramref name="opByte" />) is one where the lower eight bits are the
///   last byte seen, and the next few indicate the prefix bytes that preface the last byte.
/// Essentially, as a byte is seen, the value is shifted left and the new byte ORed into it.
/// Here are the currently used mappings:
/// <code>
/// - xx       => 0x{xx}      (8-bits)
/// - 0F xx    => 0x0F{xx}    (16-bits) // if CPU level is 0 or 1, these
/// - 0F 38 xx => 0x0F_38{xx} (24-bits) //   will never happen (only
/// - 0F 3A xx => 0x0F_3A{xx} (24-bits) //   single-byte opcodes exist)
/// </code>
/// For example, if the opcode is <c>0F&#xA0;02</c>, <paramref name="opByte" /> would be <c>0x0F02</c>.
/// </remarks>
public delegate Opcode DecodeHandler(
    Span<byte> byteStream,
    uint opByte,
    DecodedInstruction instr,
    Optional<byte> ssePrefix,
    OpcodeMapEntry[]? opmapEntries,
    out int bytesConsumed);
