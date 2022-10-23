using DotNext;
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
    /// <param name="byteStream">The bytes from memory beginning at the current instruction.</param>
    /// <returns>The decoded instruction object.</returns>
    public static Instruction? Decode(CpuCore core, Span<byte> byteStream)
    {
        Instruction instr = new(core.DefaultOperandSize);
        DecodeDescriptor descriptor = new(core.CpuLevel);

        int i = -1;
        uint opByte;
        while (true)
        {
            i++;
            if (i == byteStream.Length)
                return null; // page fault would occur reading next byte; TODO: throw #PF
            if (i is 16)
                return null; // overlong instruction; TODO: throw #GP

            byte b = byteStream[i];

            switch (b)
            {
                // "group 1" lock and repeat prefixes
                // TODO: aren't these exclusive? (one or the other, not both)
                case 0xF0:
                    instr.LockPrefix = true;
                    break;
                case 0xF2 or 0xF3:
                    instr.RepPrefix = new(b);
                    break;

                // "group 2" segment overrides
                // historically, on the P4, CS, DS, and FS were also branch hints for Jcc
                case 0x26 or 0x2E or 0x36 or 0x3E:
                    instr.SegmentOverride = new((SegmentNames)b);
                    break;
                // case 0x64 or 0x65 when core.CpuLevel >= 3:
                //     instr.SegmentOverride = new((SegmentNames)b);
                //     break;

                // // "group 3" OSIZE prefix
                // case 0x66:
                //     instr.OSizeOverride = true;
                //     break;

                // // "group 4" ASIZE prefix
                // case 0x67:
                //     instr.ASizeOverride = true;
                //     break;

                // two/three byte opcodes
                // case 0x0F when core.CpuLevel >= 2:
                //     throw new NotImplementedException();

                default:
                    opByte = b;
                    goto done;
            }
        }

    done:
        i++;
        Span<byte> rest = i < byteStream.Length ? byteStream[i..] : Span<byte>.Empty;
        DecodeDescriptor.Entry entry = descriptor.Entries[opByte];

        Debug.Assert(entry.OpcodeMapEntries is not null); // all prefixes must be handled above

        instr.Opcode = entry.Handler(rest, opByte, instr, Optional<byte>.None, entry.OpcodeMapEntries, out int bytesRead);

        // TODO: handle LOCK prefix; only allowed on select opcodes and destination must be memory

        instr.RawInstruction = byteStream[..(i + bytesRead)].ToArray();
        return instr;
    }


    /// <summary>
    /// Decode a "simple" instruction; i.e. one that ends at the opcode byte.
    /// </summary>
    /// <param name="byteStream">The input byte stream beginning after <paramref name="opByte" />.</param>
    /// <param name="opByte">The byte that triggered the call to the handler (with normalized prefixes).</param>
    /// <param name="instr">The decoded instruction object currently being built.</param>
    /// <param name="ssePrefix">
    /// The first legacy SSE prefix, or <see cref="Optional{T}.None" /> if there wasn't any.
    /// </param>
    /// <param name="opmapEntries">
    /// The opcode map entries for <paramref name="opByte" />, or <c>null</c> if there aren't any.
    /// </param>
    /// <param name="bytesConsumed">The number of bytes consumed by this function (always one).</param>
    /// <returns>The decoded opcode, or <see cref="Opcode.Error" /> if one doesn't exist.</returns>
    public static Opcode DecodeSimple(
        Span<byte> byteStream,
        uint opByte,
        Instruction instr,
        Optional<byte> ssePrefix,
        OpcodeMapEntry[]? opmapEntries,
        out int bytesConsumed)
    {
        DecodeFlagsBuilder builder = new();

        bytesConsumed = 0; // nothing consumed; op byte consumed in `Decode`
        OpcodeMapEntry entry = FindOpcode(builder, opmapEntries);
        Debug.Assert(entry.Immediate is ImmSize.None);
        return entry.Opcode;
    }


    /// <summary>
    /// Decode an instruction with an immediate, and not ModR/M byte.
    /// </summary>
    /// <param name="byteStream">The input byte stream beginning after <paramref name="opByte" />.</param>
    /// <param name="opByte">The byte that triggered the call to the handler (with normalized prefixes).</param>
    /// <param name="instr">The decoded instruction object currently being built.</param>
    /// <param name="ssePrefix">
    /// The first legacy SSE prefix, or <see cref="Optional{T}.None" /> if there wasn't any.
    /// </param>
    /// <param name="opmapEntries">
    /// The opcode map entries for <paramref name="opByte" />, or <c>null</c> if there aren't any.
    /// </param>
    /// <param name="bytesConsumed">The number of bytes consumed by this function.</param>
    /// <returns>The decoded opcode, or <see cref="Opcode.Error" /> if one doesn't exist.</returns>
    public static Opcode DecodeImmediate(
        Span<byte> byteStream,
        uint opByte,
        Instruction instr,
        Optional<byte> ssePrefix,
        OpcodeMapEntry[]? opmapEntries,
        out int bytesConsumed)
    {
        DecodeFlagsBuilder builder = new();

        OpcodeMapEntry entry = FindOpcode(builder, opmapEntries);

        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (!ReadImmediate(byteStream, entry, instr, out bytesConsumed))
            return Opcode.Error;

        return entry.Opcode;
    }


    /// <summary>
    /// Decode an instruction with a ModR/M byte, and (optionally) an immediate.
    /// </summary>
    /// <param name="byteStream">The input byte stream beginning after <paramref name="opByte" />.</param>
    /// <param name="opByte">The byte that triggered the call to the handler (with normalized prefixes).</param>
    /// <param name="instr">The decoded instruction object currently being built.</param>
    /// <param name="ssePrefix">
    /// The first legacy SSE prefix, or <see cref="Optional{T}.None" /> if there wasn't any.
    /// </param>
    /// <param name="opmapEntries">
    /// The opcode map entries for <paramref name="opByte" />, or <c>null</c> if there aren't any.
    /// </param>
    /// <param name="bytesConsumed">The number of bytes consumed by this function.</param>
    /// <returns>The decoded opcode, or <see cref="Opcode.Error" /> if one doesn't exist.</returns>
    public static Opcode DecodeModRM(
        Span<byte> byteStream,
        uint opByte,
        Instruction instr,
        Optional<byte> ssePrefix,
        OpcodeMapEntry[]? opmapEntries,
        out int bytesConsumed)
    {
        throw new NotImplementedException();
    }


    /// <summary>
    /// "Decode" an undefined instruction; i.e. one that doesn't exist.
    /// All parameters are ignored.
    /// </summary>
    /// <param name="byteStream">The input byte stream beginning after <paramref name="opByte" />.</param>
    /// <param name="opByte">The byte that triggered the call to the handler (with normalized prefixes).</param>
    /// <param name="instr">The decoded instruction object currently being built.</param>
    /// <param name="ssePrefix">
    /// The first legacy SSE prefix, or <see cref="Optional{T}.None" /> if there wasn't any.
    /// </param>
    /// <param name="opmapEntries">
    /// The opcode map entries for <paramref name="opByte" />, or <c>null</c> if there aren't any.
    /// </param>
    /// <param name="bytesConsumed">The number of bytes consumed by this function (always zero).</param>
    /// <returns><see cref="Opcode.Error" />, regardless of the parameters.</returns>
    public static Opcode DecodeUD(
        Span<byte> byteStream,
        uint opByte,
        Instruction instr,
        Optional<byte> ssePrefix,
        OpcodeMapEntry[]? opmapEntries,
        out int bytesConsumed)
    {
        bytesConsumed = 0;
        return Opcode.Error;
    }

    // future: MOV control

    // future: 3D Now!

    // future: VEX

    // future: EVEX


    // ReSharper disable once ParameterTypeCanBeEnumerable.Local
    private static OpcodeMapEntry FindOpcode(DecodeFlagsBuilder extractedFlags, OpcodeMapEntry[]? opmapEntries) =>
        opmapEntries?.FirstOrDefault(entry => extractedFlags.Matches(entry.Flags)) ?? OpcodeMap.OpcodeError[0];

    private static bool ReadImmediate(Span<byte> byteStream, OpcodeMapEntry entry, Instruction instr, out int bytesRead)
    {
        bytesRead = 0;

        ImmSize size = entry.Immediate;
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
