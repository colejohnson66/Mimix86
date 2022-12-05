﻿// generated by `Mimix86.Generators.Opcodes.OpcodesGenerator`
// any changes will be lost on next generation

using Mimix86.Core.Cpu.Execution;
using System;

namespace Mimix86.Core.Cpu.Decoder;

public partial class Opcode
{
    /// <summary>
    /// The "undefined" opcode.
    /// Used to signify that the instruction stream decodes to an undefined opcode.
    /// </summary>
    public static Opcode Undefined { get; } = new("<error>", Execution.Error._);

    /// <summary>The <c>ADD Eb, Gb</c> opcode.</summary>
    public static Opcode AddEbGb { get; } = new("add", new[] { "Eb", "Gb" }, Execution.Add.EbGb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>ADD Ew, Gw</c> opcode.</summary>
    public static Opcode AddEwGw { get; } = new("add", new[] { "Ew", "Gw" }, Execution.Add.EwGw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>ADD Gb, Eb</c> opcode.</summary>
    public static Opcode AddGbEb { get; } = new("add", new[] { "Gb", "Eb" }, Execution.Add.GbEb);

    /// <summary>The <c>ADD Gw, Ew</c> opcode.</summary>
    public static Opcode AddGwEw { get; } = new("add", new[] { "Gw", "Ew" }, Execution.Add.GwEw);

    /// <summary>The <c>ADD AL, Ib</c> opcode.</summary>
    public static Opcode AddALIb { get; } = new("add", new[] { "AL", "Ib" }, Execution.Add.ALIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>ADD AX, Iw</c> opcode.</summary>
    public static Opcode AddAXIw { get; } = new("add", new[] { "AX", "Iw" }, Execution.Add.AXIw) { Immediate = ImmSize.Word };

    /// <summary>The <c>OR Eb, Gb</c> opcode.</summary>
    public static Opcode OrEbGb { get; } = new("or", new[] { "Eb", "Gb" }, Execution.Or.EbGb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>OR Ew, Gw</c> opcode.</summary>
    public static Opcode OrEwGw { get; } = new("or", new[] { "Ew", "Gw" }, Execution.Or.EwGw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>OR Gb, Eb</c> opcode.</summary>
    public static Opcode OrGbEb { get; } = new("or", new[] { "Gb", "Eb" }, Execution.Or.GbEb);

    /// <summary>The <c>OR Gw, Ew</c> opcode.</summary>
    public static Opcode OrGwEw { get; } = new("or", new[] { "Gw", "Ew" }, Execution.Or.GwEw);

    /// <summary>The <c>OR AL, Ib</c> opcode.</summary>
    public static Opcode OrALIb { get; } = new("or", new[] { "AL", "Ib" }, Execution.Or.ALIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>OR AX, Iw</c> opcode.</summary>
    public static Opcode OrAXIw { get; } = new("or", new[] { "AX", "Iw" }, Execution.Or.AXIw) { Immediate = ImmSize.Word };

    /// <summary>The <c>ADC Eb, Gb</c> opcode.</summary>
    public static Opcode AdcEbGb { get; } = new("adc", new[] { "Eb", "Gb" }, Execution.Adc.EbGb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>ADC Ew, Gw</c> opcode.</summary>
    public static Opcode AdcEwGw { get; } = new("adc", new[] { "Ew", "Gw" }, Execution.Adc.EwGw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>ADC Gb, Eb</c> opcode.</summary>
    public static Opcode AdcGbEb { get; } = new("adc", new[] { "Gb", "Eb" }, Execution.Adc.GbEb);

    /// <summary>The <c>ADC Gw, Ew</c> opcode.</summary>
    public static Opcode AdcGwEw { get; } = new("adc", new[] { "Gw", "Ew" }, Execution.Adc.GwEw);

    /// <summary>The <c>ADC AL, Ib</c> opcode.</summary>
    public static Opcode AdcALIb { get; } = new("adc", new[] { "AL", "Ib" }, Execution.Adc.ALIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>ADC AX, Iw</c> opcode.</summary>
    public static Opcode AdcAXIw { get; } = new("adc", new[] { "AX", "Iw" }, Execution.Adc.AXIw) { Immediate = ImmSize.Word };

    /// <summary>The <c>SBB Eb, Gb</c> opcode.</summary>
    public static Opcode SbbEbGb { get; } = new("sbb", new[] { "Eb", "Gb" }, Execution.Sbb.EbGb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>SBB Ew, Gw</c> opcode.</summary>
    public static Opcode SbbEwGw { get; } = new("sbb", new[] { "Ew", "Gw" }, Execution.Sbb.EwGw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>SBB Gb, Eb</c> opcode.</summary>
    public static Opcode SbbGbEb { get; } = new("sbb", new[] { "Gb", "Eb" }, Execution.Sbb.GbEb);

    /// <summary>The <c>SBB Gw, Ew</c> opcode.</summary>
    public static Opcode SbbGwEw { get; } = new("sbb", new[] { "Gw", "Ew" }, Execution.Sbb.GwEw);

    /// <summary>The <c>SBB AL, Ib</c> opcode.</summary>
    public static Opcode SbbALIb { get; } = new("sbb", new[] { "AL", "Ib" }, Execution.Sbb.ALIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>SBB AX, Iw</c> opcode.</summary>
    public static Opcode SbbAXIw { get; } = new("sbb", new[] { "AX", "Iw" }, Execution.Sbb.AXIw) { Immediate = ImmSize.Word };

    /// <summary>The <c>AND Eb, Gb</c> opcode.</summary>
    public static Opcode AndEbGb { get; } = new("and", new[] { "Eb", "Gb" }, Execution.And.EbGb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>AND Ew, Gw</c> opcode.</summary>
    public static Opcode AndEwGw { get; } = new("and", new[] { "Ew", "Gw" }, Execution.And.EwGw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>AND Gb, Eb</c> opcode.</summary>
    public static Opcode AndGbEb { get; } = new("and", new[] { "Gb", "Eb" }, Execution.And.GbEb);

    /// <summary>The <c>AND Gw, Ew</c> opcode.</summary>
    public static Opcode AndGwEw { get; } = new("and", new[] { "Gw", "Ew" }, Execution.And.GwEw);

    /// <summary>The <c>AND AL, Ib</c> opcode.</summary>
    public static Opcode AndALIb { get; } = new("and", new[] { "AL", "Ib" }, Execution.And.ALIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>AND AX, Iw</c> opcode.</summary>
    public static Opcode AndAXIw { get; } = new("and", new[] { "AX", "Iw" }, Execution.And.AXIw) { Immediate = ImmSize.Word };

    /// <summary>The <c>SUB Eb, Gb</c> opcode.</summary>
    public static Opcode SubEbGb { get; } = new("sub", new[] { "Eb", "Gb" }, Execution.Sub.EbGb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>SUB Ew, Gw</c> opcode.</summary>
    public static Opcode SubEwGw { get; } = new("sub", new[] { "Ew", "Gw" }, Execution.Sub.EwGw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>SUB Gb, Eb</c> opcode.</summary>
    public static Opcode SubGbEb { get; } = new("sub", new[] { "Gb", "Eb" }, Execution.Sub.GbEb);

    /// <summary>The <c>SUB Gw, Ew</c> opcode.</summary>
    public static Opcode SubGwEw { get; } = new("sub", new[] { "Gw", "Ew" }, Execution.Sub.GwEw);

    /// <summary>The <c>SUB AL, Ib</c> opcode.</summary>
    public static Opcode SubALIb { get; } = new("sub", new[] { "AL", "Ib" }, Execution.Sub.ALIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>SUB AX, Iw</c> opcode.</summary>
    public static Opcode SubAXIw { get; } = new("sub", new[] { "AX", "Iw" }, Execution.Sub.AXIw) { Immediate = ImmSize.Word };

    /// <summary>The <c>XOR Eb, Gb</c> opcode.</summary>
    public static Opcode XorEbGb { get; } = new("xor", new[] { "Eb", "Gb" }, Execution.Xor.EbGb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>XOR Ew, Gw</c> opcode.</summary>
    public static Opcode XorEwGw { get; } = new("xor", new[] { "Ew", "Gw" }, Execution.Xor.EwGw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>XOR Gb, Eb</c> opcode.</summary>
    public static Opcode XorGbEb { get; } = new("xor", new[] { "Gb", "Eb" }, Execution.Xor.GbEb);

    /// <summary>The <c>XOR Gw, Ew</c> opcode.</summary>
    public static Opcode XorGwEw { get; } = new("xor", new[] { "Gw", "Ew" }, Execution.Xor.GwEw);

    /// <summary>The <c>XOR AL, Ib</c> opcode.</summary>
    public static Opcode XorALIb { get; } = new("xor", new[] { "AL", "Ib" }, Execution.Xor.ALIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>XOR AX, Iw</c> opcode.</summary>
    public static Opcode XorAXIw { get; } = new("xor", new[] { "AX", "Iw" }, Execution.Xor.AXIw) { Immediate = ImmSize.Word };

    /// <summary>The <c>CMP Eb, Gb</c> opcode.</summary>
    public static Opcode CmpEbGb { get; } = new("cmp", new[] { "Eb", "Gb" }, Execution.Cmp.EbGb);

    /// <summary>The <c>CMP Ew, Gw</c> opcode.</summary>
    public static Opcode CmpEwGw { get; } = new("cmp", new[] { "Ew", "Gw" }, Execution.Cmp.EwGw);

    /// <summary>The <c>CMP Gb, Eb</c> opcode.</summary>
    public static Opcode CmpGbEb { get; } = new("cmp", new[] { "Gb", "Eb" }, Execution.Cmp.GbEb);

    /// <summary>The <c>CMP Gw, Ew</c> opcode.</summary>
    public static Opcode CmpGwEw { get; } = new("cmp", new[] { "Gw", "Ew" }, Execution.Cmp.GwEw);

    /// <summary>The <c>CMP AL, Ib</c> opcode.</summary>
    public static Opcode CmpALIb { get; } = new("cmp", new[] { "AL", "Ib" }, Execution.Cmp.ALIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>CMP AX, Iw</c> opcode.</summary>
    public static Opcode CmpAXIw { get; } = new("cmp", new[] { "AX", "Iw" }, Execution.Cmp.AXIw) { Immediate = ImmSize.Word };

    /// <summary>The <c>ADD Eb, Ib</c> opcode.</summary>
    public static Opcode AddEbIb { get; } = new("add", new[] { "Eb", "Ib" }, Execution.Add.EbIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>OR Eb, Ib</c> opcode.</summary>
    public static Opcode OrEbIb { get; } = new("or", new[] { "Eb", "Ib" }, Execution.Or.EbIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>ADC Eb, Ib</c> opcode.</summary>
    public static Opcode AdcEbIb { get; } = new("adc", new[] { "Eb", "Ib" }, Execution.Adc.EbIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>SBB Eb, Ib</c> opcode.</summary>
    public static Opcode SbbEbIb { get; } = new("sbb", new[] { "Eb", "Ib" }, Execution.Sbb.EbIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>AND Eb, Ib</c> opcode.</summary>
    public static Opcode AndEbIb { get; } = new("and", new[] { "Eb", "Ib" }, Execution.And.EbIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>SUB Eb, Ib</c> opcode.</summary>
    public static Opcode SubEbIb { get; } = new("sub", new[] { "Eb", "Ib" }, Execution.Sub.EbIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>XOR Eb, Ib</c> opcode.</summary>
    public static Opcode XorEbIb { get; } = new("xor", new[] { "Eb", "Ib" }, Execution.Xor.EbIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>CMP Eb, Ib</c> opcode.</summary>
    public static Opcode CmpEbIb { get; } = new("cmp", new[] { "Eb", "Ib" }, Execution.Cmp.EbIb);

    /// <summary>The <c>ADD Ew, Iw</c> opcode.</summary>
    public static Opcode AddEwIw { get; } = new("add", new[] { "Ew", "Iw" }, Execution.Add.EwIw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>OR Ew, Iw</c> opcode.</summary>
    public static Opcode OrEwIw { get; } = new("or", new[] { "Ew", "Iw" }, Execution.Or.EwIw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>ADC Ew, Iw</c> opcode.</summary>
    public static Opcode AdcEwIw { get; } = new("adc", new[] { "Ew", "Iw" }, Execution.Adc.EwIw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>SBB Ew, Iw</c> opcode.</summary>
    public static Opcode SbbEwIw { get; } = new("sbb", new[] { "Ew", "Iw" }, Execution.Sbb.EwIw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>AND Ew, Iw</c> opcode.</summary>
    public static Opcode AndEwIw { get; } = new("and", new[] { "Ew", "Iw" }, Execution.And.EwIw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>SUB Ew, Iw</c> opcode.</summary>
    public static Opcode SubEwIw { get; } = new("sub", new[] { "Ew", "Iw" }, Execution.Sub.EwIw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>XOR Ew, Iw</c> opcode.</summary>
    public static Opcode XorEwIw { get; } = new("xor", new[] { "Ew", "Iw" }, Execution.Xor.EwIw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>CMP Ew, Iw</c> opcode.</summary>
    public static Opcode CmpEwIw { get; } = new("cmp", new[] { "Ew", "Iw" }, Execution.Cmp.EwIw);

    /// <summary>The <c>ADD Ew, Ib</c> opcode.</summary>
    public static Opcode AddEwIb { get; } = new("add", new[] { "Ew", "Ib" }, Execution.Add.EwIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>OR Ew, Ib</c> opcode.</summary>
    public static Opcode OrEwIb { get; } = new("or", new[] { "Ew", "Ib" }, Execution.Or.EwIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>ADC Ew, Ib</c> opcode.</summary>
    public static Opcode AdcEwIb { get; } = new("adc", new[] { "Ew", "Ib" }, Execution.Adc.EwIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>SBB Ew, Ib</c> opcode.</summary>
    public static Opcode SbbEwIb { get; } = new("sbb", new[] { "Ew", "Ib" }, Execution.Sbb.EwIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>AND Ew, Ib</c> opcode.</summary>
    public static Opcode AndEwIb { get; } = new("and", new[] { "Ew", "Ib" }, Execution.And.EwIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>SUB Ew, Ib</c> opcode.</summary>
    public static Opcode SubEwIb { get; } = new("sub", new[] { "Ew", "Ib" }, Execution.Sub.EwIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>XOR Ew, Ib</c> opcode.</summary>
    public static Opcode XorEwIb { get; } = new("xor", new[] { "Ew", "Ib" }, Execution.Xor.EwIb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>CMP Ew, Ib</c> opcode.</summary>
    public static Opcode CmpEwIb { get; } = new("cmp", new[] { "Ew", "Ib" }, Execution.Cmp.EwIb);

    /// <summary>The <c>PUSH ES</c> opcode.</summary>
    public static Opcode PushES { get; } = new("push", new[] { "ES" }, Execution.Push.ES);

    /// <summary>The <c>POP ES</c> opcode.</summary>
    public static Opcode PopES { get; } = new("pop", new[] { "ES" }, Execution.Pop.ES);

    /// <summary>The <c>PUSH CS</c> opcode.</summary>
    public static Opcode PushCS { get; } = new("push", new[] { "CS" }, Execution.Push.CS);

    /// <summary>The <c>POP CS</c> opcode.</summary>
    public static Opcode PopCS { get; } = new("pop", new[] { "CS" }, Execution.Pop.CS);

    /// <summary>The <c>PUSH SS</c> opcode.</summary>
    public static Opcode PushSS { get; } = new("push", new[] { "SS" }, Execution.Push.SS);

    /// <summary>The <c>POP SS</c> opcode.</summary>
    public static Opcode PopSS { get; } = new("pop", new[] { "SS" }, Execution.Pop.SS);

    /// <summary>The <c>PUSH DS</c> opcode.</summary>
    public static Opcode PushDS { get; } = new("push", new[] { "DS" }, Execution.Push.DS);

    /// <summary>The <c>POP DS</c> opcode.</summary>
    public static Opcode PopDS { get; } = new("pop", new[] { "DS" }, Execution.Pop.DS);

    /// <summary>The <c>PUSH Zw</c> opcode.</summary>
    public static Opcode PushZw { get; } = new("push", new[] { "Zw" }, Execution.Push.Zw);

    /// <summary>The <c>POP Zw</c> opcode.</summary>
    public static Opcode PopZw { get; } = new("pop", new[] { "Zw" }, Execution.Pop.Zw);

    /// <summary>The <c>POP Ew</c> opcode.</summary>
    public static Opcode PopEw { get; } = new("pop", new[] { "Ew" }, Execution.Pop.Ew);

    /// <summary>The <c>PUSHF</c> opcode.</summary>
    public static Opcode Pushf { get; } = new("pushf", Array.Empty<string>(), Execution.Pushf._);

    /// <summary>The <c>POPF</c> opcode.</summary>
    public static Opcode Popf { get; } = new("popf", Array.Empty<string>(), Execution.Popf._);

    /// <summary>The <c>PUSH Ew</c> opcode.</summary>
    public static Opcode PushEw { get; } = new("push", new[] { "Ew" }, Execution.Push.Ew);

    /// <summary>The <c>DAA</c> opcode.</summary>
    public static Opcode Daa { get; } = new("daa", Array.Empty<string>(), Execution.Daa._);

    /// <summary>The <c>DAS</c> opcode.</summary>
    public static Opcode Das { get; } = new("das", Array.Empty<string>(), Execution.Das._);

    /// <summary>The <c>AAA</c> opcode.</summary>
    public static Opcode Aaa { get; } = new("aaa", Array.Empty<string>(), Execution.Aaa._);

    /// <summary>The <c>AAS</c> opcode.</summary>
    public static Opcode Aas { get; } = new("aas", Array.Empty<string>(), Execution.Aas._);

    /// <summary>The <c>AAM Ib</c> opcode.</summary>
    public static Opcode AamIb { get; } = new("aam", new[] { "Ib" }, Execution.Aam.Ib) { Immediate = ImmSize.Byte };

    /// <summary>The <c>AAD Ib</c> opcode.</summary>
    public static Opcode AadIb { get; } = new("aad", new[] { "Ib" }, Execution.Aad.Ib) { Immediate = ImmSize.Byte };

    /// <summary>The <c>MOVSB</c> opcode.</summary>
    public static Opcode Movsb { get; } = new("movsb", Array.Empty<string>(), Execution.Movsb._);

    /// <summary>The <c>MOVSW</c> opcode.</summary>
    public static Opcode Movsw { get; } = new("movsw", Array.Empty<string>(), Execution.Movsw._);

    /// <summary>The <c>CMPSB</c> opcode.</summary>
    public static Opcode Cmpsb { get; } = new("cmpsb", Array.Empty<string>(), Execution.Cmpsb._);

    /// <summary>The <c>CMPSW</c> opcode.</summary>
    public static Opcode Cmpsw { get; } = new("cmpsw", Array.Empty<string>(), Execution.Cmpsw._);

    /// <summary>The <c>STOSB</c> opcode.</summary>
    public static Opcode Stosb { get; } = new("stosb", Array.Empty<string>(), Execution.Stosb._);

    /// <summary>The <c>STOSW</c> opcode.</summary>
    public static Opcode Stosw { get; } = new("stosw", Array.Empty<string>(), Execution.Stosw._);

    /// <summary>The <c>LODSB</c> opcode.</summary>
    public static Opcode Lodsb { get; } = new("lodsb", Array.Empty<string>(), Execution.Lodsb._);

    /// <summary>The <c>LODSW</c> opcode.</summary>
    public static Opcode Lodsw { get; } = new("lodsw", Array.Empty<string>(), Execution.Lodsw._);

    /// <summary>The <c>SCASB</c> opcode.</summary>
    public static Opcode Scasb { get; } = new("scasb", Array.Empty<string>(), Execution.Scasb._);

    /// <summary>The <c>SCASW</c> opcode.</summary>
    public static Opcode Scasw { get; } = new("scasw", Array.Empty<string>(), Execution.Scasw._);

    /// <summary>The <c>ROL Eb, 1</c> opcode.</summary>
    public static Opcode RolEb1 { get; } = new("rol", new[] { "Eb", "1" }, Execution.Rol.Eb1);

    /// <summary>The <c>ROR Eb, 1</c> opcode.</summary>
    public static Opcode RorEb1 { get; } = new("ror", new[] { "Eb", "1" }, Execution.Ror.Eb1);

    /// <summary>The <c>RCL Eb, 1</c> opcode.</summary>
    public static Opcode RclEb1 { get; } = new("rcl", new[] { "Eb", "1" }, Execution.Rcl.Eb1);

    /// <summary>The <c>RCR Eb, 1</c> opcode.</summary>
    public static Opcode RcrEb1 { get; } = new("rcr", new[] { "Eb", "1" }, Execution.Rcr.Eb1);

    /// <summary>The <c>SHL Eb, 1</c> opcode.</summary>
    public static Opcode ShlEb1 { get; } = new("shl", new[] { "Eb", "1" }, Execution.Shl.Eb1);

    /// <summary>The <c>SHR Eb, 1</c> opcode.</summary>
    public static Opcode ShrEb1 { get; } = new("shr", new[] { "Eb", "1" }, Execution.Shr.Eb1);

    /// <summary>The <c>SETMO Eb, 1</c> opcode.</summary>
    public static Opcode SetmoEb1 { get; } = new("setmo", new[] { "Eb", "1" }, Execution.Setmo.Eb1);

    /// <summary>The <c>SAR Eb, 1</c> opcode.</summary>
    public static Opcode SarEb1 { get; } = new("sar", new[] { "Eb", "1" }, Execution.Sar.Eb1);

    /// <summary>The <c>ROL Ew, 1</c> opcode.</summary>
    public static Opcode RolEw1 { get; } = new("rol", new[] { "Ew", "1" }, Execution.Rol.Ew1);

    /// <summary>The <c>ROR Ew, 1</c> opcode.</summary>
    public static Opcode RorEw1 { get; } = new("ror", new[] { "Ew", "1" }, Execution.Ror.Ew1);

    /// <summary>The <c>RCL Ew, 1</c> opcode.</summary>
    public static Opcode RclEw1 { get; } = new("rcl", new[] { "Ew", "1" }, Execution.Rcl.Ew1);

    /// <summary>The <c>RCR Ew, 1</c> opcode.</summary>
    public static Opcode RcrEw1 { get; } = new("rcr", new[] { "Ew", "1" }, Execution.Rcr.Ew1);

    /// <summary>The <c>SHL Ew, 1</c> opcode.</summary>
    public static Opcode ShlEw1 { get; } = new("shl", new[] { "Ew", "1" }, Execution.Shl.Ew1);

    /// <summary>The <c>SHR Ew, 1</c> opcode.</summary>
    public static Opcode ShrEw1 { get; } = new("shr", new[] { "Ew", "1" }, Execution.Shr.Ew1);

    /// <summary>The <c>SETMO Ew, 1</c> opcode.</summary>
    public static Opcode SetmoEw1 { get; } = new("setmo", new[] { "Ew", "1" }, Execution.Setmo.Ew1);

    /// <summary>The <c>SAR Ew, 1</c> opcode.</summary>
    public static Opcode SarEw1 { get; } = new("sar", new[] { "Ew", "1" }, Execution.Sar.Ew1);

    /// <summary>The <c>ROL Eb, CL</c> opcode.</summary>
    public static Opcode RolEbCL { get; } = new("rol", new[] { "Eb", "CL" }, Execution.Rol.EbCL);

    /// <summary>The <c>ROR Eb, CL</c> opcode.</summary>
    public static Opcode RorEbCL { get; } = new("ror", new[] { "Eb", "CL" }, Execution.Ror.EbCL);

    /// <summary>The <c>RCL Eb, CL</c> opcode.</summary>
    public static Opcode RclEbCL { get; } = new("rcl", new[] { "Eb", "CL" }, Execution.Rcl.EbCL);

    /// <summary>The <c>RCR Eb, CL</c> opcode.</summary>
    public static Opcode RcrEbCL { get; } = new("rcr", new[] { "Eb", "CL" }, Execution.Rcr.EbCL);

    /// <summary>The <c>SHL Eb, CL</c> opcode.</summary>
    public static Opcode ShlEbCL { get; } = new("shl", new[] { "Eb", "CL" }, Execution.Shl.EbCL);

    /// <summary>The <c>SHR Eb, CL</c> opcode.</summary>
    public static Opcode ShrEbCL { get; } = new("shr", new[] { "Eb", "CL" }, Execution.Shr.EbCL);

    /// <summary>The <c>SETMO Eb, CL</c> opcode.</summary>
    public static Opcode SetmoEbCL { get; } = new("setmo", new[] { "Eb", "CL" }, Execution.Setmo.EbCL);

    /// <summary>The <c>SAR Eb, CL</c> opcode.</summary>
    public static Opcode SarEbCL { get; } = new("sar", new[] { "Eb", "CL" }, Execution.Sar.EbCL);

    /// <summary>The <c>ROL Ew, CL</c> opcode.</summary>
    public static Opcode RolEwCL { get; } = new("rol", new[] { "Ew", "CL" }, Execution.Rol.EwCL);

    /// <summary>The <c>ROR Ew, CL</c> opcode.</summary>
    public static Opcode RorEwCL { get; } = new("ror", new[] { "Ew", "CL" }, Execution.Ror.EwCL);

    /// <summary>The <c>RCL Ew, CL</c> opcode.</summary>
    public static Opcode RclEwCL { get; } = new("rcl", new[] { "Ew", "CL" }, Execution.Rcl.EwCL);

    /// <summary>The <c>RCR Ew, CL</c> opcode.</summary>
    public static Opcode RcrEwCL { get; } = new("rcr", new[] { "Ew", "CL" }, Execution.Rcr.EwCL);

    /// <summary>The <c>SHL Ew, CL</c> opcode.</summary>
    public static Opcode ShlEwCL { get; } = new("shl", new[] { "Ew", "CL" }, Execution.Shl.EwCL);

    /// <summary>The <c>SHR Ew, CL</c> opcode.</summary>
    public static Opcode ShrEwCL { get; } = new("shr", new[] { "Ew", "CL" }, Execution.Shr.EwCL);

    /// <summary>The <c>SETMO Ew, CL</c> opcode.</summary>
    public static Opcode SetmoEwCL { get; } = new("setmo", new[] { "Ew", "CL" }, Execution.Setmo.EwCL);

    /// <summary>The <c>SAR Ew, CL</c> opcode.</summary>
    public static Opcode SarEwCL { get; } = new("sar", new[] { "Ew", "CL" }, Execution.Sar.EwCL);

    /// <summary>The <c>IN AL, Ib</c> opcode.</summary>
    public static Opcode InALIb { get; } = new("in", new[] { "AL", "Ib" }, Execution.In.ALIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>IN AX, Ib</c> opcode.</summary>
    public static Opcode InAXIb { get; } = new("in", new[] { "AX", "Ib" }, Execution.In.AXIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>OUT Ib, AL</c> opcode.</summary>
    public static Opcode OutIbAL { get; } = new("out", new[] { "Ib", "AL" }, Execution.Out.IbAL) { Immediate = ImmSize.Byte };

    /// <summary>The <c>OUT Ib, AX</c> opcode.</summary>
    public static Opcode OutIbAX { get; } = new("out", new[] { "Ib", "AX" }, Execution.Out.IbAX) { Immediate = ImmSize.Byte };

    /// <summary>The <c>IN AL, DX</c> opcode.</summary>
    public static Opcode InALDX { get; } = new("in", new[] { "AL", "DX" }, Execution.In.ALDX);

    /// <summary>The <c>IN AX, DX</c> opcode.</summary>
    public static Opcode InAXDX { get; } = new("in", new[] { "AX", "DX" }, Execution.In.AXDX);

    /// <summary>The <c>OUT DX, AL</c> opcode.</summary>
    public static Opcode OutDXAL { get; } = new("out", new[] { "DX", "AL" }, Execution.Out.DXAL);

    /// <summary>The <c>OUT DX, AX</c> opcode.</summary>
    public static Opcode OutDXAX { get; } = new("out", new[] { "DX", "AX" }, Execution.Out.DXAX);

    /// <summary>The <c>Jcc Jb</c> opcode.</summary>
    public static Opcode JccJb { get; } = new("jcc", new[] { "Jb" }, Execution.Jcc.Jb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>CALL Apww</c> opcode.</summary>
    public static Opcode CallApww { get; } = new("call", new[] { "Apww" }, Execution.Call.Apww) { Immediate = ImmSize.PointerWordWord };

    /// <summary>The <c>RET Iw</c> opcode.</summary>
    public static Opcode RetIw { get; } = new("ret", new[] { "Iw" }, Execution.Ret.Iw) { Immediate = ImmSize.Word };

    /// <summary>The <c>RET</c> opcode.</summary>
    public static Opcode Ret { get; } = new("ret", Array.Empty<string>(), Execution.Ret._);

    /// <summary>The <c>RETF Iw</c> opcode.</summary>
    public static Opcode RetfIw { get; } = new("retf", new[] { "Iw" }, Execution.Retf.Iw) { Immediate = ImmSize.Word };

    /// <summary>The <c>RETF</c> opcode.</summary>
    public static Opcode Retf { get; } = new("retf", Array.Empty<string>(), Execution.Retf._);

    /// <summary>The <c>INT 3</c> opcode.</summary>
    public static Opcode Int3 { get; } = new("int", new[] { "3" }, Execution.Int._3);

    /// <summary>The <c>INT Ib</c> opcode.</summary>
    public static Opcode IntIb { get; } = new("int", new[] { "Ib" }, Execution.Int.Ib) { Immediate = ImmSize.Byte };

    /// <summary>The <c>INTO</c> opcode.</summary>
    public static Opcode Into { get; } = new("into", Array.Empty<string>(), Execution.Into._);

    /// <summary>The <c>IRET</c> opcode.</summary>
    public static Opcode Iret { get; } = new("iret", Array.Empty<string>(), Execution.Iret._);

    /// <summary>The <c>LOOPNE Jb</c> opcode.</summary>
    public static Opcode LoopneJb { get; } = new("loopne", new[] { "Jb" }, Execution.Loopne.Jb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>LOOPE Jb</c> opcode.</summary>
    public static Opcode LoopeJb { get; } = new("loope", new[] { "Jb" }, Execution.Loope.Jb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>LOOP Jb</c> opcode.</summary>
    public static Opcode LoopJb { get; } = new("loop", new[] { "Jb" }, Execution.Loop.Jb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>JCXZ Jb</c> opcode.</summary>
    public static Opcode JcxzJb { get; } = new("jcxz", new[] { "Jb" }, Execution.Jcxz.Jb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>CALL Jw</c> opcode.</summary>
    public static Opcode CallJw { get; } = new("call", new[] { "Jw" }, Execution.Call.Jw) { Immediate = ImmSize.Word };

    /// <summary>The <c>JMP Jw</c> opcode.</summary>
    public static Opcode JmpJw { get; } = new("jmp", new[] { "Jw" }, Execution.Jmp.Jw) { Immediate = ImmSize.Word };

    /// <summary>The <c>JMP Apww</c> opcode.</summary>
    public static Opcode JmpApww { get; } = new("jmp", new[] { "Apww" }, Execution.Jmp.Apww) { Immediate = ImmSize.PointerWordWord };

    /// <summary>The <c>JMP Jb</c> opcode.</summary>
    public static Opcode JmpJb { get; } = new("jmp", new[] { "Jb" }, Execution.Jmp.Jb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>HLT</c> opcode.</summary>
    public static Opcode Hlt { get; } = new("hlt", Array.Empty<string>(), Execution.Hlt._);

    /// <summary>The <c>CALL Ew</c> opcode.</summary>
    public static Opcode CallEw { get; } = new("call", new[] { "Ew" }, Execution.Call.Ew);

    /// <summary>The <c>CALL Mpww</c> opcode.</summary>
    public static Opcode CallMpww { get; } = new("call", new[] { "Mpww" }, Execution.Call.Mpww);

    /// <summary>The <c>JMP Ew</c> opcode.</summary>
    public static Opcode JmpEw { get; } = new("jmp", new[] { "Ew" }, Execution.Jmp.Ew);

    /// <summary>The <c>JMP Mpww</c> opcode.</summary>
    public static Opcode JmpMpww { get; } = new("jmp", new[] { "Mpww" }, Execution.Jmp.Mpww);

    /// <summary>The <c>XCHG Eb, Gb</c> opcode.</summary>
    public static Opcode XchgEbGb { get; } = new("xchg", new[] { "Eb", "Gb" }, Execution.Xchg.EbGb) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>XCHG Ew, Gw</c> opcode.</summary>
    public static Opcode XchgEwGw { get; } = new("xchg", new[] { "Ew", "Gw" }, Execution.Xchg.EwGw) { Flags = OpcodeFlags.Lockable };

    /// <summary>The <c>MOV Eb, Gb</c> opcode.</summary>
    public static Opcode MovEbGb { get; } = new("mov", new[] { "Eb", "Gb" }, Execution.Mov.EbGb);

    /// <summary>The <c>MOV Ew, Gw</c> opcode.</summary>
    public static Opcode MovEwGw { get; } = new("mov", new[] { "Ew", "Gw" }, Execution.Mov.EwGw);

    /// <summary>The <c>MOV Gb, Eb</c> opcode.</summary>
    public static Opcode MovGbEb { get; } = new("mov", new[] { "Gb", "Eb" }, Execution.Mov.GbEb);

    /// <summary>The <c>MOV Gw, Ew</c> opcode.</summary>
    public static Opcode MovGwEw { get; } = new("mov", new[] { "Gw", "Ew" }, Execution.Mov.GwEw);

    /// <summary>The <c>MOV Ew, Sw</c> opcode.</summary>
    public static Opcode MovEwSw { get; } = new("mov", new[] { "Ew", "Sw" }, Execution.Mov.EwSw);

    /// <summary>The <c>LEA Gw, M</c> opcode.</summary>
    public static Opcode LeaGwM { get; } = new("lea", new[] { "Gw", "M" }, Execution.Lea.GwM);

    /// <summary>The <c>MOV Sw, Ew</c> opcode.</summary>
    public static Opcode MovSwEw { get; } = new("mov", new[] { "Sw", "Ew" }, Execution.Mov.SwEw);

    /// <summary>The <c>NOP</c> opcode.</summary>
    public static Opcode Nop { get; } = new("nop", Array.Empty<string>(), Execution.Nop._);

    /// <summary>The <c>XCHG AX, Zw</c> opcode.</summary>
    public static Opcode XchgAXZw { get; } = new("xchg", new[] { "AX", "Zw" }, Execution.Xchg.AXZw);

    /// <summary>The <c>MOV AL, Ob</c> opcode.</summary>
    public static Opcode MovALOb { get; } = new("mov", new[] { "AL", "Ob" }, Execution.Mov.ALOb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>MOV AX, Ow</c> opcode.</summary>
    public static Opcode MovAXOw { get; } = new("mov", new[] { "AX", "Ow" }, Execution.Mov.AXOw) { Immediate = ImmSize.Word };

    /// <summary>The <c>MOV Ob, AL</c> opcode.</summary>
    public static Opcode MovObAL { get; } = new("mov", new[] { "Ob", "AL" }, Execution.Mov.ObAL) { Immediate = ImmSize.Byte };

    /// <summary>The <c>MOV Ow, AX</c> opcode.</summary>
    public static Opcode MovOwAX { get; } = new("mov", new[] { "Ow", "AX" }, Execution.Mov.OwAX) { Immediate = ImmSize.Word };

    /// <summary>The <c>MOV Zb, Ib</c> opcode.</summary>
    public static Opcode MovZbIb { get; } = new("mov", new[] { "Zb", "Ib" }, Execution.Mov.ZbIb);

    /// <summary>The <c>MOV Zw, Iw</c> opcode.</summary>
    public static Opcode MovZwIw { get; } = new("mov", new[] { "Zw", "Iw" }, Execution.Mov.ZwIw);

    /// <summary>The <c>LES Gw, Mw</c> opcode.</summary>
    public static Opcode LesGwMw { get; } = new("les", new[] { "Gw", "Mw" }, Execution.Les.GwMw);

    /// <summary>The <c>LDS Gw, Mw</c> opcode.</summary>
    public static Opcode LdsGwMw { get; } = new("lds", new[] { "Gw", "Mw" }, Execution.Lds.GwMw);

    /// <summary>The <c>MOV Eb, Ib</c> opcode.</summary>
    public static Opcode MovEbIb { get; } = new("mov", new[] { "Eb", "Ib" }, Execution.Mov.EbIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>MOV Ew, Iw</c> opcode.</summary>
    public static Opcode MovEwIw { get; } = new("mov", new[] { "Ew", "Iw" }, Execution.Mov.EwIw) { Immediate = ImmSize.Word };

    /// <summary>The <c>XLAT</c> opcode.</summary>
    public static Opcode Xlat { get; } = new("xlat", Array.Empty<string>(), Execution.Xlat._);

    /// <summary>The <c>INC Zw</c> opcode.</summary>
    public static Opcode IncZw { get; } = new("inc", new[] { "Zw" }, Execution.Inc.Zw);

    /// <summary>The <c>DEC Zw</c> opcode.</summary>
    public static Opcode DecZw { get; } = new("dec", new[] { "Zw" }, Execution.Dec.Zw);

    /// <summary>The <c>TEST Eb, Gb</c> opcode.</summary>
    public static Opcode TestEbGb { get; } = new("test", new[] { "Eb", "Gb" }, Execution.Test.EbGb);

    /// <summary>The <c>TEST Ew, Gw</c> opcode.</summary>
    public static Opcode TestEwGw { get; } = new("test", new[] { "Ew", "Gw" }, Execution.Test.EwGw);

    /// <summary>The <c>CBW</c> opcode.</summary>
    public static Opcode Cbw { get; } = new("cbw", Array.Empty<string>(), Execution.Cbw._);

    /// <summary>The <c>CWD</c> opcode.</summary>
    public static Opcode Cwd { get; } = new("cwd", Array.Empty<string>(), Execution.Cwd._);

    /// <summary>The <c>SAHF</c> opcode.</summary>
    public static Opcode Sahf { get; } = new("sahf", Array.Empty<string>(), Execution.Sahf._);

    /// <summary>The <c>LAHF</c> opcode.</summary>
    public static Opcode Lahf { get; } = new("lahf", Array.Empty<string>(), Execution.Lahf._);

    /// <summary>The <c>TEST AL, Ib</c> opcode.</summary>
    public static Opcode TestALIb { get; } = new("test", new[] { "AL", "Ib" }, Execution.Test.ALIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>TEST AX, Iw</c> opcode.</summary>
    public static Opcode TestAXIw { get; } = new("test", new[] { "AX", "Iw" }, Execution.Test.AXIw) { Immediate = ImmSize.Word };

    /// <summary>The <c>SALC</c> opcode.</summary>
    public static Opcode Salc { get; } = new("salc", Array.Empty<string>(), Execution.Salc._);

    /// <summary>The <c>CMC</c> opcode.</summary>
    public static Opcode Cmc { get; } = new("cmc", Array.Empty<string>(), Execution.Cmc._);

    /// <summary>The <c>TEST Eb, Ib</c> opcode.</summary>
    public static Opcode TestEbIb { get; } = new("test", new[] { "Eb", "Ib" }, Execution.Test.EbIb) { Immediate = ImmSize.Byte };

    /// <summary>The <c>NOT Eb</c> opcode.</summary>
    public static Opcode NotEb { get; } = new("not", new[] { "Eb" }, Execution.Not.Eb);

    /// <summary>The <c>NEG Eb</c> opcode.</summary>
    public static Opcode NegEb { get; } = new("neg", new[] { "Eb" }, Execution.Neg.Eb);

    /// <summary>The <c>MUL Eb</c> opcode.</summary>
    public static Opcode MulEb { get; } = new("mul", new[] { "Eb" }, Execution.Mul.Eb);

    /// <summary>The <c>IMUL Eb</c> opcode.</summary>
    public static Opcode ImulEb { get; } = new("imul", new[] { "Eb" }, Execution.Imul.Eb);

    /// <summary>The <c>DIV Eb</c> opcode.</summary>
    public static Opcode DivEb { get; } = new("div", new[] { "Eb" }, Execution.Div.Eb);

    /// <summary>The <c>IDIV Eb</c> opcode.</summary>
    public static Opcode IdivEb { get; } = new("idiv", new[] { "Eb" }, Execution.Idiv.Eb);

    /// <summary>The <c>TEST Ew, Iw</c> opcode.</summary>
    public static Opcode TestEwIw { get; } = new("test", new[] { "Ew", "Iw" }, Execution.Test.EwIw) { Immediate = ImmSize.Word };

    /// <summary>The <c>NOT Ew</c> opcode.</summary>
    public static Opcode NotEw { get; } = new("not", new[] { "Ew" }, Execution.Not.Ew);

    /// <summary>The <c>NEG Ew</c> opcode.</summary>
    public static Opcode NegEw { get; } = new("neg", new[] { "Ew" }, Execution.Neg.Ew);

    /// <summary>The <c>MUL Ew</c> opcode.</summary>
    public static Opcode MulEw { get; } = new("mul", new[] { "Ew" }, Execution.Mul.Ew);

    /// <summary>The <c>IMUL Ew</c> opcode.</summary>
    public static Opcode ImulEw { get; } = new("imul", new[] { "Ew" }, Execution.Imul.Ew);

    /// <summary>The <c>DIV Ew</c> opcode.</summary>
    public static Opcode DivEw { get; } = new("div", new[] { "Ew" }, Execution.Div.Ew);

    /// <summary>The <c>IDIV Ew</c> opcode.</summary>
    public static Opcode IdivEw { get; } = new("idiv", new[] { "Ew" }, Execution.Idiv.Ew);

    /// <summary>The <c>CLC</c> opcode.</summary>
    public static Opcode Clc { get; } = new("clc", Array.Empty<string>(), Execution.Clc._);

    /// <summary>The <c>STC</c> opcode.</summary>
    public static Opcode Stc { get; } = new("stc", Array.Empty<string>(), Execution.Stc._);

    /// <summary>The <c>CLI</c> opcode.</summary>
    public static Opcode Cli { get; } = new("cli", Array.Empty<string>(), Execution.Cli._);

    /// <summary>The <c>STI</c> opcode.</summary>
    public static Opcode Sti { get; } = new("sti", Array.Empty<string>(), Execution.Sti._);

    /// <summary>The <c>CLD</c> opcode.</summary>
    public static Opcode Cld { get; } = new("cld", Array.Empty<string>(), Execution.Cld._);

    /// <summary>The <c>STD</c> opcode.</summary>
    public static Opcode Std { get; } = new("std", Array.Empty<string>(), Execution.Std._);

    /// <summary>The <c>INC Eb</c> opcode.</summary>
    public static Opcode IncEb { get; } = new("inc", new[] { "Eb" }, Execution.Inc.Eb);

    /// <summary>The <c>DEC Eb</c> opcode.</summary>
    public static Opcode DecEb { get; } = new("dec", new[] { "Eb" }, Execution.Dec.Eb);

    /// <summary>The <c>INC Ew</c> opcode.</summary>
    public static Opcode IncEw { get; } = new("inc", new[] { "Ew" }, Execution.Inc.Ew);

    /// <summary>The <c>DEC Ew</c> opcode.</summary>
    public static Opcode DecEw { get; } = new("dec", new[] { "Ew" }, Execution.Dec.Ew);

    /// <summary>The <c>WAIT</c> opcode.</summary>
    public static Opcode Wait { get; } = new("wait", Array.Empty<string>(), Execution.Wait._);
}
