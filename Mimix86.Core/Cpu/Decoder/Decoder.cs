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
    // TODO: replace `byteStream` with some kind of memory reader object that handles page faults and A20 wrapping for us
    /// <summary>
    /// Decode a single instruction.
    /// </summary>
    /// <param name="core">The CPU core that is decoding this instruction.</param>
    /// <param name="descriptors">The decode descriptors to use when decoding.</param>
    /// <param name="byteStream">The bytes from memory beginning at the current instruction.</param>
    /// <returns>The decoded instruction object.</returns>
    public static Instruction? Decode(CpuCore core, DecodeDescriptor descriptors, Span<byte> byteStream)
    {
        Instruction instr = new(core.DefaultOperandSize);

        int i = -1;
        uint opByte;
        while (true)
        {
            i++;
            if (i == byteStream.Length)
            {
                core.RaiseException(new(CpuExceptionCode.PF, 0)); // TODO: fault code; how does 8086 handle it?
                return null;
            }
            if (i is 16)
                return null; // overlong instruction; TODO: throw #GP; how does 8086 handle it?

            byte b = byteStream[i];
            switch (b)
            {
                // "group 1" lock and repeat prefixes
                // TODO: these are exclusive; how does the 8086 handle it?
                case 0xF0:
                case 0xF1 when core.CpuLevel is 0:
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
                // case 0x66 when core.CpuLevel >= 3:
                //     instr.OSizeOverride = true;
                //     break;

                // // "group 4" ASIZE prefix
                // case 0x67 when core.CpuLevel >= 3:
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
        DecodeDescriptor.Entry entry = descriptors.Entries[opByte];

        Debug.Assert(entry.OpcodeMapEntries is not null); // all prefixes must be handled above

        instr.Opcode = entry.Handler(core, rest, opByte, instr, Optional<byte>.None, entry.OpcodeMapEntries, out int bytesRead);

        if (!instr.Opcode.Flags.HasFlag(OpcodeFlags.Lockable) && instr.LockPrefix)
            throw new NotImplementedException(); // #UD on 80186+, but what about 8086?

        instr.RawInstruction = byteStream[..(i + bytesRead)].ToArray();
        return instr;
    }


    /// <summary>
    /// Decode a "simple" instruction; i.e. one that ends at the opcode byte.
    /// </summary>
    /// <param name="core">The CPU core that is decoding this instruction.</param>
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
        CpuCore core,
        Span<byte> byteStream,
        uint opByte,
        Instruction instr,
        Optional<byte> ssePrefix,
        OpcodeMapEntry[]? opmapEntries,
        out int bytesConsumed)
    {
        DecodeFlagsBuilder builder = new(); // no decode flags... yet

        OpcodeMapEntry entry = FindOpcode(core, builder, opmapEntries);
        Opcode opcode = entry.Opcode;
        Debug.Assert(opcode.Immediate is ImmSize.None);

        bytesConsumed = 0; // nothing consumed; op byte consumed in `Decode`
        return opcode;
    }


    /// <summary>
    /// Decode an instruction with an immediate, and no ModR/M byte.
    /// </summary>
    /// <param name="core">The CPU core that is decoding this instruction.</param>
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
        CpuCore core,
        Span<byte> byteStream,
        uint opByte,
        Instruction instr,
        Optional<byte> ssePrefix,
        OpcodeMapEntry[]? opmapEntries,
        out int bytesConsumed)
    {
        DecodeFlagsBuilder builder = new(); // no decode flags... yet

        OpcodeMapEntry entry = FindOpcode(core, builder, opmapEntries);
        Opcode opcode = entry.Opcode;

        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (ReadImmediate(byteStream, opcode, instr, out bytesConsumed))
            return opcode;

        return Opcode.Error;
    }


    /// <summary>
    /// Decode an instruction with a ModR/M byte, and (optionally) an immediate.
    /// </summary>
    /// <param name="core">The CPU core that is decoding this instruction.</param>
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
        CpuCore core,
        Span<byte> byteStream,
        uint opByte,
        Instruction instr,
        Optional<byte> ssePrefix,
        OpcodeMapEntry[]? opmapEntries,
        out int bytesConsumed)
    {
        bytesConsumed = 0;
        if (byteStream.Length is 0)
            return Opcode.Error;

        byte b = byteStream[0];
        instr.ModRM = new(b);

        DecodeFlagsBuilder builder = new();
        builder.ModRM(b);

        OpcodeMapEntry entry = FindOpcode(core, builder, opmapEntries);
        Opcode opcode = entry.Opcode;

        if (opcode.Immediate is ImmSize.None)
        {
            bytesConsumed = 1;
            return opcode;
        }

        if (ReadImmediate(byteStream[1..], opcode, instr, out int immBytes))
        {
            bytesConsumed = immBytes + 1;
            return opcode;
        }

        bytesConsumed = 1;
        return Opcode.Error;
    }


    /// <summary>
    /// "Decode" an undefined instruction; i.e. one that doesn't exist.
    /// All parameters are ignored.
    /// </summary>
    /// <param name="core">The CPU core that is decoding this instruction.</param>
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
        CpuCore core,
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

        return OpcodeMap.OpcodeError[0];
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
