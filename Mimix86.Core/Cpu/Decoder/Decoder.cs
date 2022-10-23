using DotNext;
using System;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Contains the various decode methods used by <see cref="DecodeDescriptor" />.
/// </summary>
public static class Decoder
{
    /// <summary>
    /// Decode a single instruction.
    /// </summary>
    /// <param name="core"></param>
    /// <param name="byteStream"></param>
    /// <returns></returns>
    public static DecodedInstruction? Decode(CpuCore core, Span<byte> byteStream) =>
        throw new NotImplementedException();

    public static Opcode DecodeSimple(
        Span<byte> byteStream,
        uint opByte,
        DecodedInstruction instr,
        Optional<byte> ssePrefix,
        OpcodeMapEntry[]? opmapEntries,
        out int bytesConsumed) =>
        throw new NotImplementedException();

    public static Opcode DecodeImmediate(
        Span<byte> byteStream,
        uint opByte,
        DecodedInstruction instr,
        Optional<byte> ssePrefix,
        OpcodeMapEntry[]? opmapEntries,
        out int bytesConsumed) =>
        throw new NotImplementedException();

    public static Opcode DecodeModRM(
        Span<byte> byteStream,
        uint opByte,
        DecodedInstruction instr,
        Optional<byte> ssePrefix,
        OpcodeMapEntry[]? opmapEntries,
        out int bytesConsumed) =>
        throw new NotImplementedException();

    public static Opcode DecodeUD(
        Span<byte> byteStream,
        uint opByte,
        DecodedInstruction instr,
        Optional<byte> ssePrefix,
        OpcodeMapEntry[]? opmapEntries,
        out int bytesConsumed) =>
        throw new NotImplementedException();

    // future: MOV control

    // future: 3D Now!

    // future: VEX

    // future: EVEX
}
