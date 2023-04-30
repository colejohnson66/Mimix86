/* =============================================================================
 * File:   Decoder.cs
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
 *   Mimix86. If not, see <http://www.gnu.org/licenses/>.
 * =============================================================================
 */

using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Linq;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Contains the various decode methods used by <see cref="DecodeDescriptor" />.
/// </summary>
public static class Decoder
{
    /// <summary>
    /// Decode a single instruction.
    /// </summary>
    /// <param name="core">The CPU core that is decoding this instruction.</param>
    /// <param name="stream">The byte stream beginning at the current instruction.</param>
    /// <param name="descriptors">The decode descriptors to use when decoding.</param>
    /// <returns>The decoded instruction object.</returns>
    // ReSharper disable once ReturnTypeCanBeNotNullable
    public static Instruction? Decode(
        CpuCore core,
        DecodeByteStream stream,
        DecodeDescriptor descriptors) =>
        throw new NotImplementedException();


    /// <summary>
    /// Decode a "simple" instruction; i.e. one that ends at the opcode byte.
    /// </summary>
    /// <param name="core">The CPU core that is decoding this instruction.</param>
    /// <param name="stream">The input byte stream.</param>
    /// <param name="opByte">The byte that triggered the call to the handler (with normalized prefixes).</param>
    /// <param name="instr">The decoded instruction object currently being built.</param>
    /// <param name="opmapEntries">
    /// The opcode map entries for <paramref name="opByte" />, or <c>null</c> if there aren't any.
    /// </param>
    /// <returns>The decoded opcode, or <see cref="Opcode.Undefined" /> if one doesn't exist.</returns>
    public static Opcode DecodeSimple(
        CpuCore core,
        DecodeByteStream stream,
        uint opByte,
        Instruction instr,
        OpcodeMapEntry[]? opmapEntries) =>
        throw new NotImplementedException();


    /// <summary>
    /// Decode an instruction with an immediate, and no ModR/M byte.
    /// </summary>
    /// <param name="core">The CPU core that is decoding this instruction.</param>
    /// <param name="stream">The input byte stream.</param>
    /// <param name="opByte">The byte that triggered the call to the handler (with normalized prefixes).</param>
    /// <param name="instr">The decoded instruction object currently being built.</param>
    /// <param name="opmapEntries">
    /// The opcode map entries for <paramref name="opByte" />, or <c>null</c> if there aren't any.
    /// </param>
    /// <returns>The decoded opcode, or <see cref="Opcode.Undefined" /> if one doesn't exist.</returns>
    public static Opcode DecodeImmediate(
        CpuCore core,
        DecodeByteStream stream,
        uint opByte,
        Instruction instr,
        OpcodeMapEntry[]? opmapEntries) =>
        throw new NotImplementedException();


    /// <summary>
    /// Decode an instruction with a ModR/M byte and (optionally) an immediate.
    /// </summary>
    /// <param name="core">The CPU core that is decoding this instruction.</param>
    /// <param name="stream">The input byte stream.</param>
    /// <param name="opByte">The byte that triggered the call to the handler (with normalized prefixes).</param>
    /// <param name="instr">The decoded instruction object currently being built.</param>
    /// <param name="opmapEntries">
    /// The opcode map entries for <paramref name="opByte" />, or <c>null</c> if there aren't any.
    /// </param>
    /// <returns>The decoded opcode, or <see cref="Opcode.Undefined" /> if one doesn't exist.</returns>
    public static Opcode DecodeModRM(
        CpuCore core,
        DecodeByteStream stream,
        uint opByte,
        Instruction instr,
        OpcodeMapEntry[]? opmapEntries) =>
        throw new NotImplementedException();


    /// <summary>
    /// "Decode" an undefined instruction; i.e. one that doesn't exist.
    /// All parameters are ignored.
    /// </summary>
    /// <param name="core">The CPU core that is decoding this instruction.</param>
    /// <param name="stream">The input byte stream.</param>
    /// <param name="opByte">The byte that triggered the call to the handler (with normalized prefixes).</param>
    /// <param name="instr">The decoded instruction object currently being built.</param>
    /// <param name="opmapEntries">
    /// The opcode map entries for <paramref name="opByte" />, or <c>null</c> if there aren't any.
    /// </param>
    /// <returns><see cref="Opcode.Undefined" />, regardless of the parameters.</returns>
    public static Opcode DecodeUD(
        CpuCore core,
        DecodeByteStream stream,
        uint opByte,
        Instruction instr,
        OpcodeMapEntry[]? opmapEntries) =>
        throw new NotImplementedException();

    // future: MOV control/debug/test

    // future: 3D Now!

    // future: XOP

    // future: VEX

    // future: EVEX


    private static OpcodeMapEntry FindOpcode(CpuCore core, DecodeFlagsBuilder extractedFlags, OpcodeMapEntry[]? opmapEntries)
    {
        foreach (OpcodeMapEntry entry in opmapEntries ?? Enumerable.Empty<OpcodeMapEntry>())
        {
            // check CPU level
            if (core.CpuLevel < entry.SupportedCpuLevels.Start.Value ||
                core.CpuLevel > entry.SupportedCpuLevels.End.Value)
                continue;

            if (extractedFlags.Matches(entry.Flags))
                return entry;
        }

        return OpcodeMap.OpcodeUndefined[0];
    }

    private static bool ReadImmediate(Span<byte> byteStream, Opcode opcode, Instruction instr, out int bytesRead)
    {
        bytesRead = 0;

        ImmSize size = opcode.Immediate;
        if (size is ImmSize.None)
            return true;

        int bytesToRead = size switch
        {
            ImmSize.Byte => 1,
            ImmSize.Word => 2,
            ImmSize.PointerWordWord => 4,
            _ => throw new UnreachableException(),
        };
        if (bytesToRead > byteStream.Length)
            return false;

        bytesRead = bytesToRead;

        int read = 0;
        if (size is ImmSize.PointerWordWord)
        {
            instr.ImmediateSegment = BinaryPrimitives.ReadUInt16LittleEndian(byteStream);
            read += 2;
            bytesToRead -= 2;
        }

        Span<byte> immediate = stackalloc byte[2]; // future: up to 8
        byteStream[read..(read + bytesToRead)].CopyTo(immediate);
        instr.Immediate = BinaryPrimitives.ReadUInt16LittleEndian(immediate); // future: up to UInt64
        return true;
    }
}
