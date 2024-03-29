// generated by `Mimix86.Generators.Opcodes.OpcodesGenerator`
// any changes will be lost on next generation

using Mimix86.Core.Cpu.Decoder;
using Mimix86.Core.Cpu.Execution;
using System;

namespace Mimix86.Core.Cpu.Isa;

public static class Opcodes
{
    /// <summary>
    /// The "undefined" opcode.
    /// Used to signify that the instruction stream decodes to an undefined opcode.
    /// </summary>
    public static Opcode Undefined { get; } = new("<error>", Execution.Error._, 0, null);

    /// <summary>The <c>AAA</c> opcode.</summary>
    public static Opcode Aaa { get; } = new("aaa", Execution.Aaa._, 0, null);

    /// <summary>The <c>AAD Ib</c> opcode.</summary>
    public static Opcode AadIb { get; } = new("aad", Execution.Aad.Ib, 0, ImmediateSizes.Byte);

    /// <summary>The <c>AAM Ib</c> opcode.</summary>
    public static Opcode AamIb { get; } = new("aam", Execution.Aam.Ib, 0, ImmediateSizes.Byte);

    /// <summary>The <c>AAS</c> opcode.</summary>
    public static Opcode Aas { get; } = new("aas", Execution.Aas._, 0, null);

    /// <summary>The <c>ADC AL, Ib</c> opcode.</summary>
    public static Opcode AdcALIb { get; } = new("adc", Execution.Adc.ALIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>ADC AX, Iw</c> opcode.</summary>
    public static Opcode AdcAXIw { get; } = new("adc", Execution.Adc.AXIw, 0, ImmediateSizes.Word);

    /// <summary>The <c>ADC Eb, Gb</c> opcode.</summary>
    public static Opcode AdcEbGb { get; } = new("adc", Execution.Adc.EbGb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>ADC Eb, Ib</c> opcode.</summary>
    public static Opcode AdcEbIb { get; } = new("adc", Execution.Adc.EbIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>ADC Ew, Gw</c> opcode.</summary>
    public static Opcode AdcEwGw { get; } = new("adc", Execution.Adc.EwGw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>ADC Ew, Ib</c> opcode.</summary>
    public static Opcode AdcEwIb { get; } = new("adc", Execution.Adc.EwIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>ADC Ew, Iw</c> opcode.</summary>
    public static Opcode AdcEwIw { get; } = new("adc", Execution.Adc.EwIw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>ADC Gb, Eb</c> opcode.</summary>
    public static Opcode AdcGbEb { get; } = new("adc", Execution.Adc.GbEb, 0, null);

    /// <summary>The <c>ADC Gw, Ew</c> opcode.</summary>
    public static Opcode AdcGwEw { get; } = new("adc", Execution.Adc.GwEw, 0, null);

    /// <summary>The <c>ADD AL, Ib</c> opcode.</summary>
    public static Opcode AddALIb { get; } = new("add", Execution.Add.ALIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>ADD AX, Iw</c> opcode.</summary>
    public static Opcode AddAXIw { get; } = new("add", Execution.Add.AXIw, 0, ImmediateSizes.Word);

    /// <summary>The <c>ADD Eb, Gb</c> opcode.</summary>
    public static Opcode AddEbGb { get; } = new("add", Execution.Add.EbGb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>ADD Eb, Ib</c> opcode.</summary>
    public static Opcode AddEbIb { get; } = new("add", Execution.Add.EbIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>ADD Ew, Gw</c> opcode.</summary>
    public static Opcode AddEwGw { get; } = new("add", Execution.Add.EwGw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>ADD Ew, Ib</c> opcode.</summary>
    public static Opcode AddEwIb { get; } = new("add", Execution.Add.EwIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>ADD Ew, Iw</c> opcode.</summary>
    public static Opcode AddEwIw { get; } = new("add", Execution.Add.EwIw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>ADD Gb, Eb</c> opcode.</summary>
    public static Opcode AddGbEb { get; } = new("add", Execution.Add.GbEb, 0, null);

    /// <summary>The <c>ADD Gw, Ew</c> opcode.</summary>
    public static Opcode AddGwEw { get; } = new("add", Execution.Add.GwEw, 0, null);

    /// <summary>The <c>AND AL, Ib</c> opcode.</summary>
    public static Opcode AndALIb { get; } = new("and", Execution.And.ALIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>AND AX, Iw</c> opcode.</summary>
    public static Opcode AndAXIw { get; } = new("and", Execution.And.AXIw, 0, ImmediateSizes.Word);

    /// <summary>The <c>AND Eb, Gb</c> opcode.</summary>
    public static Opcode AndEbGb { get; } = new("and", Execution.And.EbGb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>AND Eb, Ib</c> opcode.</summary>
    public static Opcode AndEbIb { get; } = new("and", Execution.And.EbIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>AND Ew, Gw</c> opcode.</summary>
    public static Opcode AndEwGw { get; } = new("and", Execution.And.EwGw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>AND Ew, Ib</c> opcode.</summary>
    public static Opcode AndEwIb { get; } = new("and", Execution.And.EwIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>AND Ew, Iw</c> opcode.</summary>
    public static Opcode AndEwIw { get; } = new("and", Execution.And.EwIw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>AND Gb, Eb</c> opcode.</summary>
    public static Opcode AndGbEb { get; } = new("and", Execution.And.GbEb, 0, null);

    /// <summary>The <c>AND Gw, Ew</c> opcode.</summary>
    public static Opcode AndGwEw { get; } = new("and", Execution.And.GwEw, 0, null);

    /// <summary>The <c>BOUND Gw, Ma</c> opcode.</summary>
    public static Opcode BoundGwMa { get; } = new("bound", Execution.Bound.GwMa, 0, null);

    /// <summary>The <c>CALL Apww</c> opcode.</summary>
    public static Opcode CallApww { get; } = new("call", Execution.Call.Apww, OpcodeFlags.EndTrace, ImmediateSizes.PointerWordWord);

    /// <summary>The <c>CALL Ew</c> opcode.</summary>
    public static Opcode CallEw { get; } = new("call", Execution.Call.Ew, OpcodeFlags.EndTrace, null);

    /// <summary>The <c>CALL Jw</c> opcode.</summary>
    public static Opcode CallJw { get; } = new("call", Execution.Call.Jw, OpcodeFlags.EndTrace, ImmediateSizes.Word);

    /// <summary>The <c>CALL Mpww</c> opcode.</summary>
    public static Opcode CallMpww { get; } = new("call", Execution.Call.Mpww, OpcodeFlags.EndTrace, null);

    /// <summary>The <c>CBW</c> opcode.</summary>
    public static Opcode Cbw { get; } = new("cbw", Execution.Cbw._, 0, null);

    /// <summary>The <c>CLC</c> opcode.</summary>
    public static Opcode Clc { get; } = new("clc", Execution.Clc._, 0, null);

    /// <summary>The <c>CLD</c> opcode.</summary>
    public static Opcode Cld { get; } = new("cld", Execution.Cld._, 0, null);

    /// <summary>The <c>CLI</c> opcode.</summary>
    public static Opcode Cli { get; } = new("cli", Execution.Cli._, 0, null);

    /// <summary>The <c>CMC</c> opcode.</summary>
    public static Opcode Cmc { get; } = new("cmc", Execution.Cmc._, 0, null);

    /// <summary>The <c>CMP AL, Ib</c> opcode.</summary>
    public static Opcode CmpALIb { get; } = new("cmp", Execution.Cmp.ALIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>CMP AX, Iw</c> opcode.</summary>
    public static Opcode CmpAXIw { get; } = new("cmp", Execution.Cmp.AXIw, 0, ImmediateSizes.Word);

    /// <summary>The <c>CMP Eb, Gb</c> opcode.</summary>
    public static Opcode CmpEbGb { get; } = new("cmp", Execution.Cmp.EbGb, 0, null);

    /// <summary>The <c>CMP Eb, Ib</c> opcode.</summary>
    public static Opcode CmpEbIb { get; } = new("cmp", Execution.Cmp.EbIb, 0, null);

    /// <summary>The <c>CMP Ew, Gw</c> opcode.</summary>
    public static Opcode CmpEwGw { get; } = new("cmp", Execution.Cmp.EwGw, 0, null);

    /// <summary>The <c>CMP Ew, Ib</c> opcode.</summary>
    public static Opcode CmpEwIb { get; } = new("cmp", Execution.Cmp.EwIb, 0, null);

    /// <summary>The <c>CMP Ew, Iw</c> opcode.</summary>
    public static Opcode CmpEwIw { get; } = new("cmp", Execution.Cmp.EwIw, 0, null);

    /// <summary>The <c>CMP Gb, Eb</c> opcode.</summary>
    public static Opcode CmpGbEb { get; } = new("cmp", Execution.Cmp.GbEb, 0, null);

    /// <summary>The <c>CMP Gw, Ew</c> opcode.</summary>
    public static Opcode CmpGwEw { get; } = new("cmp", Execution.Cmp.GwEw, 0, null);

    /// <summary>The <c>CMPSB</c> opcode.</summary>
    public static Opcode Cmpsb { get; } = new("cmpsb", Execution.Cmpsb._, 0, null);

    /// <summary>The <c>CMPSW</c> opcode.</summary>
    public static Opcode Cmpsw { get; } = new("cmpsw", Execution.Cmpsw._, 0, null);

    /// <summary>The <c>CWD</c> opcode.</summary>
    public static Opcode Cwd { get; } = new("cwd", Execution.Cwd._, 0, null);

    /// <summary>The <c>DAA</c> opcode.</summary>
    public static Opcode Daa { get; } = new("daa", Execution.Daa._, 0, null);

    /// <summary>The <c>DAS</c> opcode.</summary>
    public static Opcode Das { get; } = new("das", Execution.Das._, 0, null);

    /// <summary>The <c>DEC Eb</c> opcode.</summary>
    public static Opcode DecEb { get; } = new("dec", Execution.Dec.Eb, 0, null);

    /// <summary>The <c>DEC Ew</c> opcode.</summary>
    public static Opcode DecEw { get; } = new("dec", Execution.Dec.Ew, 0, null);

    /// <summary>The <c>DEC Zw</c> opcode.</summary>
    public static Opcode DecZw { get; } = new("dec", Execution.Dec.Zw, 0, null);

    /// <summary>The <c>DIV Eb</c> opcode.</summary>
    public static Opcode DivEb { get; } = new("div", Execution.Div.Eb, 0, null);

    /// <summary>The <c>DIV Ew</c> opcode.</summary>
    public static Opcode DivEw { get; } = new("div", Execution.Div.Ew, 0, null);

    /// <summary>The <c>ENTER Iw, Ib</c> opcode.</summary>
    public static Opcode EnterIwIb { get; } = new("enter", Execution.Enter.IwIb, 0, ImmediateSizes.WordByte);

    /// <summary>The <c>HLT</c> opcode.</summary>
    public static Opcode Hlt { get; } = new("hlt", Execution.Hlt._, OpcodeFlags.EndTrace, null);

    /// <summary>The <c>IDIV Eb</c> opcode.</summary>
    public static Opcode IdivEb { get; } = new("idiv", Execution.Idiv.Eb, 0, null);

    /// <summary>The <c>IDIV Ew</c> opcode.</summary>
    public static Opcode IdivEw { get; } = new("idiv", Execution.Idiv.Ew, 0, null);

    /// <summary>The <c>IMUL Eb</c> opcode.</summary>
    public static Opcode ImulEb { get; } = new("imul", Execution.Imul.Eb, 0, null);

    /// <summary>The <c>IMUL Ew</c> opcode.</summary>
    public static Opcode ImulEw { get; } = new("imul", Execution.Imul.Ew, 0, null);

    /// <summary>The <c>IMUL Gw, Ew, Ib</c> opcode.</summary>
    public static Opcode ImulGwEwIb { get; } = new("imul", Execution.Imul.GwEwIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>IMUL Gw, Ew, Iw</c> opcode.</summary>
    public static Opcode ImulGwEwIw { get; } = new("imul", Execution.Imul.GwEwIw, 0, ImmediateSizes.Word);

    /// <summary>The <c>IN AL, DX</c> opcode.</summary>
    public static Opcode InALDX { get; } = new("in", Execution.In.ALDX, 0, null);

    /// <summary>The <c>IN AL, Ib</c> opcode.</summary>
    public static Opcode InALIb { get; } = new("in", Execution.In.ALIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>IN AX, DX</c> opcode.</summary>
    public static Opcode InAXDX { get; } = new("in", Execution.In.AXDX, 0, null);

    /// <summary>The <c>IN AX, Ib</c> opcode.</summary>
    public static Opcode InAXIb { get; } = new("in", Execution.In.AXIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>INC Eb</c> opcode.</summary>
    public static Opcode IncEb { get; } = new("inc", Execution.Inc.Eb, 0, null);

    /// <summary>The <c>INC Ew</c> opcode.</summary>
    public static Opcode IncEw { get; } = new("inc", Execution.Inc.Ew, 0, null);

    /// <summary>The <c>INC Zw</c> opcode.</summary>
    public static Opcode IncZw { get; } = new("inc", Execution.Inc.Zw, 0, null);

    /// <summary>The <c>INSB</c> opcode.</summary>
    public static Opcode Insb { get; } = new("insb", Execution.Insb._, 0, null);

    /// <summary>The <c>INSW</c> opcode.</summary>
    public static Opcode Insw { get; } = new("insw", Execution.Insw._, 0, null);

    /// <summary>The <c>INT 3</c> opcode.</summary>
    public static Opcode Int3 { get; } = new("int", Execution.Int._3, OpcodeFlags.EndTrace, null);

    /// <summary>The <c>INT Ib</c> opcode.</summary>
    public static Opcode IntIb { get; } = new("int", Execution.Int.Ib, OpcodeFlags.EndTrace, ImmediateSizes.Byte);

    /// <summary>The <c>INTO</c> opcode.</summary>
    public static Opcode Into { get; } = new("into", Execution.Into._, OpcodeFlags.EndTrace, null);

    /// <summary>The <c>IRET</c> opcode.</summary>
    public static Opcode Iret { get; } = new("iret", Execution.Iret._, OpcodeFlags.EndTrace, null);

    /// <summary>The <c>JCXZ Jb</c> opcode.</summary>
    public static Opcode JcxzJb { get; } = new("jcxz", Execution.Jcxz.Jb, OpcodeFlags.EndTrace, ImmediateSizes.Byte);

    /// <summary>The <c>JMP Apww</c> opcode.</summary>
    public static Opcode JmpApww { get; } = new("jmp", Execution.Jmp.Apww, OpcodeFlags.EndTrace, ImmediateSizes.PointerWordWord);

    /// <summary>The <c>JMP Ew</c> opcode.</summary>
    public static Opcode JmpEw { get; } = new("jmp", Execution.Jmp.Ew, OpcodeFlags.EndTrace, null);

    /// <summary>The <c>JMP Jb</c> opcode.</summary>
    public static Opcode JmpJb { get; } = new("jmp", Execution.Jmp.Jb, OpcodeFlags.EndTrace, ImmediateSizes.Byte);

    /// <summary>The <c>JMP Jw</c> opcode.</summary>
    public static Opcode JmpJw { get; } = new("jmp", Execution.Jmp.Jw, OpcodeFlags.EndTrace, ImmediateSizes.Word);

    /// <summary>The <c>JMP Mpww</c> opcode.</summary>
    public static Opcode JmpMpww { get; } = new("jmp", Execution.Jmp.Mpww, OpcodeFlags.EndTrace, null);

    /// <summary>The <c>Jcc Jb</c> opcode.</summary>
    public static Opcode JccJb { get; } = new("jcc", Execution.Jcc.Jb, OpcodeFlags.EndTrace, ImmediateSizes.Byte);

    /// <summary>The <c>LAHF</c> opcode.</summary>
    public static Opcode Lahf { get; } = new("lahf", Execution.Lahf._, 0, null);

    /// <summary>The <c>LDS Gw, Mw</c> opcode.</summary>
    public static Opcode LdsGwMw { get; } = new("lds", Execution.Lds.GwMw, 0, null);

    /// <summary>The <c>LEA Gw, M</c> opcode.</summary>
    public static Opcode LeaGwM { get; } = new("lea", Execution.Lea.GwM, 0, null);

    /// <summary>The <c>LEAVE</c> opcode.</summary>
    public static Opcode Leave { get; } = new("leave", Execution.Leave._, 0, null);

    /// <summary>The <c>LES Gw, Mw</c> opcode.</summary>
    public static Opcode LesGwMw { get; } = new("les", Execution.Les.GwMw, 0, null);

    /// <summary>The <c>LODSB</c> opcode.</summary>
    public static Opcode Lodsb { get; } = new("lodsb", Execution.Lodsb._, 0, null);

    /// <summary>The <c>LODSW</c> opcode.</summary>
    public static Opcode Lodsw { get; } = new("lodsw", Execution.Lodsw._, 0, null);

    /// <summary>The <c>LOOP Jb</c> opcode.</summary>
    public static Opcode LoopJb { get; } = new("loop", Execution.Loop.Jb, OpcodeFlags.EndTrace, ImmediateSizes.Byte);

    /// <summary>The <c>LOOPE Jb</c> opcode.</summary>
    public static Opcode LoopeJb { get; } = new("loope", Execution.Loope.Jb, OpcodeFlags.EndTrace, ImmediateSizes.Byte);

    /// <summary>The <c>LOOPNE Jb</c> opcode.</summary>
    public static Opcode LoopneJb { get; } = new("loopne", Execution.Loopne.Jb, OpcodeFlags.EndTrace, ImmediateSizes.Byte);

    /// <summary>The <c>MOV AL, Ob</c> opcode.</summary>
    public static Opcode MovALOb { get; } = new("mov", Execution.Mov.ALOb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>MOV AX, Ow</c> opcode.</summary>
    public static Opcode MovAXOw { get; } = new("mov", Execution.Mov.AXOw, 0, ImmediateSizes.Word);

    /// <summary>The <c>MOV Eb, Gb</c> opcode.</summary>
    public static Opcode MovEbGb { get; } = new("mov", Execution.Mov.EbGb, 0, null);

    /// <summary>The <c>MOV Eb, Ib</c> opcode.</summary>
    public static Opcode MovEbIb { get; } = new("mov", Execution.Mov.EbIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>MOV Ew, Gw</c> opcode.</summary>
    public static Opcode MovEwGw { get; } = new("mov", Execution.Mov.EwGw, 0, null);

    /// <summary>The <c>MOV Ew, Iw</c> opcode.</summary>
    public static Opcode MovEwIw { get; } = new("mov", Execution.Mov.EwIw, 0, ImmediateSizes.Word);

    /// <summary>The <c>MOV Ew, Sw</c> opcode.</summary>
    public static Opcode MovEwSw { get; } = new("mov", Execution.Mov.EwSw, 0, null);

    /// <summary>The <c>MOV Gb, Eb</c> opcode.</summary>
    public static Opcode MovGbEb { get; } = new("mov", Execution.Mov.GbEb, 0, null);

    /// <summary>The <c>MOV Gw, Ew</c> opcode.</summary>
    public static Opcode MovGwEw { get; } = new("mov", Execution.Mov.GwEw, 0, null);

    /// <summary>The <c>MOV Ob, AL</c> opcode.</summary>
    public static Opcode MovObAL { get; } = new("mov", Execution.Mov.ObAL, 0, ImmediateSizes.Byte);

    /// <summary>The <c>MOV Ow, AX</c> opcode.</summary>
    public static Opcode MovOwAX { get; } = new("mov", Execution.Mov.OwAX, 0, ImmediateSizes.Word);

    /// <summary>The <c>MOV Sw, Ew</c> opcode.</summary>
    public static Opcode MovSwEw { get; } = new("mov", Execution.Mov.SwEw, 0, null);

    /// <summary>The <c>MOV Zb, Ib</c> opcode.</summary>
    public static Opcode MovZbIb { get; } = new("mov", Execution.Mov.ZbIb, 0, null);

    /// <summary>The <c>MOV Zw, Iw</c> opcode.</summary>
    public static Opcode MovZwIw { get; } = new("mov", Execution.Mov.ZwIw, 0, null);

    /// <summary>The <c>MOVSB</c> opcode.</summary>
    public static Opcode Movsb { get; } = new("movsb", Execution.Movsb._, 0, null);

    /// <summary>The <c>MOVSW</c> opcode.</summary>
    public static Opcode Movsw { get; } = new("movsw", Execution.Movsw._, 0, null);

    /// <summary>The <c>MUL Eb</c> opcode.</summary>
    public static Opcode MulEb { get; } = new("mul", Execution.Mul.Eb, 0, null);

    /// <summary>The <c>MUL Ew</c> opcode.</summary>
    public static Opcode MulEw { get; } = new("mul", Execution.Mul.Ew, 0, null);

    /// <summary>The <c>NEG Eb</c> opcode.</summary>
    public static Opcode NegEb { get; } = new("neg", Execution.Neg.Eb, 0, null);

    /// <summary>The <c>NEG Ew</c> opcode.</summary>
    public static Opcode NegEw { get; } = new("neg", Execution.Neg.Ew, 0, null);

    /// <summary>The <c>NOT Eb</c> opcode.</summary>
    public static Opcode NotEb { get; } = new("not", Execution.Not.Eb, 0, null);

    /// <summary>The <c>NOT Ew</c> opcode.</summary>
    public static Opcode NotEw { get; } = new("not", Execution.Not.Ew, 0, null);

    /// <summary>The <c>OR AL, Ib</c> opcode.</summary>
    public static Opcode OrALIb { get; } = new("or", Execution.Or.ALIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>OR AX, Iw</c> opcode.</summary>
    public static Opcode OrAXIw { get; } = new("or", Execution.Or.AXIw, 0, ImmediateSizes.Word);

    /// <summary>The <c>OR Eb, Gb</c> opcode.</summary>
    public static Opcode OrEbGb { get; } = new("or", Execution.Or.EbGb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>OR Eb, Ib</c> opcode.</summary>
    public static Opcode OrEbIb { get; } = new("or", Execution.Or.EbIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>OR Ew, Gw</c> opcode.</summary>
    public static Opcode OrEwGw { get; } = new("or", Execution.Or.EwGw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>OR Ew, Ib</c> opcode.</summary>
    public static Opcode OrEwIb { get; } = new("or", Execution.Or.EwIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>OR Ew, Iw</c> opcode.</summary>
    public static Opcode OrEwIw { get; } = new("or", Execution.Or.EwIw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>OR Gb, Eb</c> opcode.</summary>
    public static Opcode OrGbEb { get; } = new("or", Execution.Or.GbEb, 0, null);

    /// <summary>The <c>OR Gw, Ew</c> opcode.</summary>
    public static Opcode OrGwEw { get; } = new("or", Execution.Or.GwEw, 0, null);

    /// <summary>The <c>OUT DX, AL</c> opcode.</summary>
    public static Opcode OutDXAL { get; } = new("out", Execution.Out.DXAL, 0, null);

    /// <summary>The <c>OUT DX, AX</c> opcode.</summary>
    public static Opcode OutDXAX { get; } = new("out", Execution.Out.DXAX, 0, null);

    /// <summary>The <c>OUT Ib, AL</c> opcode.</summary>
    public static Opcode OutIbAL { get; } = new("out", Execution.Out.IbAL, 0, ImmediateSizes.Byte);

    /// <summary>The <c>OUT Ib, AX</c> opcode.</summary>
    public static Opcode OutIbAX { get; } = new("out", Execution.Out.IbAX, 0, ImmediateSizes.Byte);

    /// <summary>The <c>OUTSB</c> opcode.</summary>
    public static Opcode Outsb { get; } = new("outsb", Execution.Outsb._, 0, null);

    /// <summary>The <c>OUTSW</c> opcode.</summary>
    public static Opcode Outsw { get; } = new("outsw", Execution.Outsw._, 0, null);

    /// <summary>The <c>POP CS</c> opcode.</summary>
    public static Opcode PopCS { get; } = new("pop", Execution.Pop.CS, 0, null);

    /// <summary>The <c>POP DS</c> opcode.</summary>
    public static Opcode PopDS { get; } = new("pop", Execution.Pop.DS, 0, null);

    /// <summary>The <c>POP ES</c> opcode.</summary>
    public static Opcode PopES { get; } = new("pop", Execution.Pop.ES, 0, null);

    /// <summary>The <c>POP Ew</c> opcode.</summary>
    public static Opcode PopEw { get; } = new("pop", Execution.Pop.Ew, 0, null);

    /// <summary>The <c>POP SS</c> opcode.</summary>
    public static Opcode PopSS { get; } = new("pop", Execution.Pop.SS, 0, null);

    /// <summary>The <c>POP Zw</c> opcode.</summary>
    public static Opcode PopZw { get; } = new("pop", Execution.Pop.Zw, 0, null);

    /// <summary>The <c>POPA</c> opcode.</summary>
    public static Opcode Popa { get; } = new("popa", Execution.Popa._, 0, null);

    /// <summary>The <c>POPF</c> opcode.</summary>
    public static Opcode Popf { get; } = new("popf", Execution.Popf._, 0, null);

    /// <summary>The <c>PUSH CS</c> opcode.</summary>
    public static Opcode PushCS { get; } = new("push", Execution.Push.CS, 0, null);

    /// <summary>The <c>PUSH DS</c> opcode.</summary>
    public static Opcode PushDS { get; } = new("push", Execution.Push.DS, 0, null);

    /// <summary>The <c>PUSH ES</c> opcode.</summary>
    public static Opcode PushES { get; } = new("push", Execution.Push.ES, 0, null);

    /// <summary>The <c>PUSH Ew</c> opcode.</summary>
    public static Opcode PushEw { get; } = new("push", Execution.Push.Ew, 0, null);

    /// <summary>The <c>PUSH Ib</c> opcode.</summary>
    public static Opcode PushIb { get; } = new("push", Execution.Push.Ib, 0, ImmediateSizes.Byte);

    /// <summary>The <c>PUSH Iw</c> opcode.</summary>
    public static Opcode PushIw { get; } = new("push", Execution.Push.Iw, 0, ImmediateSizes.Word);

    /// <summary>The <c>PUSH SS</c> opcode.</summary>
    public static Opcode PushSS { get; } = new("push", Execution.Push.SS, 0, null);

    /// <summary>The <c>PUSH Zw</c> opcode.</summary>
    public static Opcode PushZw { get; } = new("push", Execution.Push.Zw, 0, null);

    /// <summary>The <c>PUSHA</c> opcode.</summary>
    public static Opcode Pusha { get; } = new("pusha", Execution.Pusha._, 0, null);

    /// <summary>The <c>PUSHF</c> opcode.</summary>
    public static Opcode Pushf { get; } = new("pushf", Execution.Pushf._, 0, null);

    /// <summary>The <c>RCL Eb, 1</c> opcode.</summary>
    public static Opcode RclEb1 { get; } = new("rcl", Execution.Rcl.Eb1, 0, null);

    /// <summary>The <c>RCL Eb, CL</c> opcode.</summary>
    public static Opcode RclEbCL { get; } = new("rcl", Execution.Rcl.EbCL, 0, null);

    /// <summary>The <c>RCL Eb, Ib</c> opcode.</summary>
    public static Opcode RclEbIb { get; } = new("rcl", Execution.Rcl.EbIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>RCL Ew, 1</c> opcode.</summary>
    public static Opcode RclEw1 { get; } = new("rcl", Execution.Rcl.Ew1, 0, null);

    /// <summary>The <c>RCL Ew, CL</c> opcode.</summary>
    public static Opcode RclEwCL { get; } = new("rcl", Execution.Rcl.EwCL, 0, null);

    /// <summary>The <c>RCL Ew, Ib</c> opcode.</summary>
    public static Opcode RclEwIb { get; } = new("rcl", Execution.Rcl.EwIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>RCR Eb, 1</c> opcode.</summary>
    public static Opcode RcrEb1 { get; } = new("rcr", Execution.Rcr.Eb1, 0, null);

    /// <summary>The <c>RCR Eb, CL</c> opcode.</summary>
    public static Opcode RcrEbCL { get; } = new("rcr", Execution.Rcr.EbCL, 0, null);

    /// <summary>The <c>RCR Eb, Ib</c> opcode.</summary>
    public static Opcode RcrEbIb { get; } = new("rcr", Execution.Rcr.EbIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>RCR Ew, 1</c> opcode.</summary>
    public static Opcode RcrEw1 { get; } = new("rcr", Execution.Rcr.Ew1, 0, null);

    /// <summary>The <c>RCR Ew, CL</c> opcode.</summary>
    public static Opcode RcrEwCL { get; } = new("rcr", Execution.Rcr.EwCL, 0, null);

    /// <summary>The <c>RCR Ew, Ib</c> opcode.</summary>
    public static Opcode RcrEwIb { get; } = new("rcr", Execution.Rcr.EwIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>RET</c> opcode.</summary>
    public static Opcode Ret { get; } = new("ret", Execution.Ret._, OpcodeFlags.EndTrace, null);

    /// <summary>The <c>RET Iw</c> opcode.</summary>
    public static Opcode RetIw { get; } = new("ret", Execution.Ret.Iw, OpcodeFlags.EndTrace, ImmediateSizes.Word);

    /// <summary>The <c>RETF</c> opcode.</summary>
    public static Opcode Retf { get; } = new("retf", Execution.Retf._, OpcodeFlags.EndTrace, null);

    /// <summary>The <c>RETF Iw</c> opcode.</summary>
    public static Opcode RetfIw { get; } = new("retf", Execution.Retf.Iw, OpcodeFlags.EndTrace, ImmediateSizes.Word);

    /// <summary>The <c>ROL Eb, 1</c> opcode.</summary>
    public static Opcode RolEb1 { get; } = new("rol", Execution.Rol.Eb1, 0, null);

    /// <summary>The <c>ROL Eb, CL</c> opcode.</summary>
    public static Opcode RolEbCL { get; } = new("rol", Execution.Rol.EbCL, 0, null);

    /// <summary>The <c>ROL Eb, Ib</c> opcode.</summary>
    public static Opcode RolEbIb { get; } = new("rol", Execution.Rol.EbIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>ROL Ew, 1</c> opcode.</summary>
    public static Opcode RolEw1 { get; } = new("rol", Execution.Rol.Ew1, 0, null);

    /// <summary>The <c>ROL Ew, CL</c> opcode.</summary>
    public static Opcode RolEwCL { get; } = new("rol", Execution.Rol.EwCL, 0, null);

    /// <summary>The <c>ROL Ew, Ib</c> opcode.</summary>
    public static Opcode RolEwIb { get; } = new("rol", Execution.Rol.EwIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>ROR Eb, 1</c> opcode.</summary>
    public static Opcode RorEb1 { get; } = new("ror", Execution.Ror.Eb1, 0, null);

    /// <summary>The <c>ROR Eb, CL</c> opcode.</summary>
    public static Opcode RorEbCL { get; } = new("ror", Execution.Ror.EbCL, 0, null);

    /// <summary>The <c>ROR Eb, Ib</c> opcode.</summary>
    public static Opcode RorEbIb { get; } = new("ror", Execution.Ror.EbIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>ROR Ew, 1</c> opcode.</summary>
    public static Opcode RorEw1 { get; } = new("ror", Execution.Ror.Ew1, 0, null);

    /// <summary>The <c>ROR Ew, CL</c> opcode.</summary>
    public static Opcode RorEwCL { get; } = new("ror", Execution.Ror.EwCL, 0, null);

    /// <summary>The <c>ROR Ew, Ib</c> opcode.</summary>
    public static Opcode RorEwIb { get; } = new("ror", Execution.Ror.EwIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>SAHF</c> opcode.</summary>
    public static Opcode Sahf { get; } = new("sahf", Execution.Sahf._, 0, null);

    /// <summary>The <c>SALC</c> opcode.</summary>
    public static Opcode Salc { get; } = new("salc", Execution.Salc._, 0, null);

    /// <summary>The <c>SAR Eb, 1</c> opcode.</summary>
    public static Opcode SarEb1 { get; } = new("sar", Execution.Sar.Eb1, 0, null);

    /// <summary>The <c>SAR Eb, CL</c> opcode.</summary>
    public static Opcode SarEbCL { get; } = new("sar", Execution.Sar.EbCL, 0, null);

    /// <summary>The <c>SAR Eb, Ib</c> opcode.</summary>
    public static Opcode SarEbIb { get; } = new("sar", Execution.Sar.EbIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>SAR Ew, 1</c> opcode.</summary>
    public static Opcode SarEw1 { get; } = new("sar", Execution.Sar.Ew1, 0, null);

    /// <summary>The <c>SAR Ew, CL</c> opcode.</summary>
    public static Opcode SarEwCL { get; } = new("sar", Execution.Sar.EwCL, 0, null);

    /// <summary>The <c>SAR Ew, Ib</c> opcode.</summary>
    public static Opcode SarEwIb { get; } = new("sar", Execution.Sar.EwIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>SBB AL, Ib</c> opcode.</summary>
    public static Opcode SbbALIb { get; } = new("sbb", Execution.Sbb.ALIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>SBB AX, Iw</c> opcode.</summary>
    public static Opcode SbbAXIw { get; } = new("sbb", Execution.Sbb.AXIw, 0, ImmediateSizes.Word);

    /// <summary>The <c>SBB Eb, Gb</c> opcode.</summary>
    public static Opcode SbbEbGb { get; } = new("sbb", Execution.Sbb.EbGb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>SBB Eb, Ib</c> opcode.</summary>
    public static Opcode SbbEbIb { get; } = new("sbb", Execution.Sbb.EbIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>SBB Ew, Gw</c> opcode.</summary>
    public static Opcode SbbEwGw { get; } = new("sbb", Execution.Sbb.EwGw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>SBB Ew, Ib</c> opcode.</summary>
    public static Opcode SbbEwIb { get; } = new("sbb", Execution.Sbb.EwIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>SBB Ew, Iw</c> opcode.</summary>
    public static Opcode SbbEwIw { get; } = new("sbb", Execution.Sbb.EwIw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>SBB Gb, Eb</c> opcode.</summary>
    public static Opcode SbbGbEb { get; } = new("sbb", Execution.Sbb.GbEb, 0, null);

    /// <summary>The <c>SBB Gw, Ew</c> opcode.</summary>
    public static Opcode SbbGwEw { get; } = new("sbb", Execution.Sbb.GwEw, 0, null);

    /// <summary>The <c>SCASB</c> opcode.</summary>
    public static Opcode Scasb { get; } = new("scasb", Execution.Scasb._, 0, null);

    /// <summary>The <c>SCASW</c> opcode.</summary>
    public static Opcode Scasw { get; } = new("scasw", Execution.Scasw._, 0, null);

    /// <summary>The <c>SETMO Eb, 1</c> opcode.</summary>
    public static Opcode SetmoEb1 { get; } = new("setmo", Execution.Setmo.Eb1, 0, null);

    /// <summary>The <c>SETMO Eb, CL</c> opcode.</summary>
    public static Opcode SetmoEbCL { get; } = new("setmo", Execution.Setmo.EbCL, 0, null);

    /// <summary>The <c>SETMO Ew, 1</c> opcode.</summary>
    public static Opcode SetmoEw1 { get; } = new("setmo", Execution.Setmo.Ew1, 0, null);

    /// <summary>The <c>SETMO Ew, CL</c> opcode.</summary>
    public static Opcode SetmoEwCL { get; } = new("setmo", Execution.Setmo.EwCL, 0, null);

    /// <summary>The <c>SHL Eb, 1</c> opcode.</summary>
    public static Opcode ShlEb1 { get; } = new("shl", Execution.Shl.Eb1, 0, null);

    /// <summary>The <c>SHL Eb, CL</c> opcode.</summary>
    public static Opcode ShlEbCL { get; } = new("shl", Execution.Shl.EbCL, 0, null);

    /// <summary>The <c>SHL Eb, Ib</c> opcode.</summary>
    public static Opcode ShlEbIb { get; } = new("shl", Execution.Shl.EbIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>SHL Ew, 1</c> opcode.</summary>
    public static Opcode ShlEw1 { get; } = new("shl", Execution.Shl.Ew1, 0, null);

    /// <summary>The <c>SHL Ew, CL</c> opcode.</summary>
    public static Opcode ShlEwCL { get; } = new("shl", Execution.Shl.EwCL, 0, null);

    /// <summary>The <c>SHL Ew, Ib</c> opcode.</summary>
    public static Opcode ShlEwIb { get; } = new("shl", Execution.Shl.EwIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>SHR Eb, 1</c> opcode.</summary>
    public static Opcode ShrEb1 { get; } = new("shr", Execution.Shr.Eb1, 0, null);

    /// <summary>The <c>SHR Eb, CL</c> opcode.</summary>
    public static Opcode ShrEbCL { get; } = new("shr", Execution.Shr.EbCL, 0, null);

    /// <summary>The <c>SHR Eb, Ib</c> opcode.</summary>
    public static Opcode ShrEbIb { get; } = new("shr", Execution.Shr.EbIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>SHR Ew, 1</c> opcode.</summary>
    public static Opcode ShrEw1 { get; } = new("shr", Execution.Shr.Ew1, 0, null);

    /// <summary>The <c>SHR Ew, CL</c> opcode.</summary>
    public static Opcode ShrEwCL { get; } = new("shr", Execution.Shr.EwCL, 0, null);

    /// <summary>The <c>SHR Ew, Ib</c> opcode.</summary>
    public static Opcode ShrEwIb { get; } = new("shr", Execution.Shr.EwIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>STC</c> opcode.</summary>
    public static Opcode Stc { get; } = new("stc", Execution.Stc._, 0, null);

    /// <summary>The <c>STD</c> opcode.</summary>
    public static Opcode Std { get; } = new("std", Execution.Std._, 0, null);

    /// <summary>The <c>STI</c> opcode.</summary>
    public static Opcode Sti { get; } = new("sti", Execution.Sti._, 0, null);

    /// <summary>The <c>STOSB</c> opcode.</summary>
    public static Opcode Stosb { get; } = new("stosb", Execution.Stosb._, 0, null);

    /// <summary>The <c>STOSW</c> opcode.</summary>
    public static Opcode Stosw { get; } = new("stosw", Execution.Stosw._, 0, null);

    /// <summary>The <c>SUB AL, Ib</c> opcode.</summary>
    public static Opcode SubALIb { get; } = new("sub", Execution.Sub.ALIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>SUB AX, Iw</c> opcode.</summary>
    public static Opcode SubAXIw { get; } = new("sub", Execution.Sub.AXIw, 0, ImmediateSizes.Word);

    /// <summary>The <c>SUB Eb, Gb</c> opcode.</summary>
    public static Opcode SubEbGb { get; } = new("sub", Execution.Sub.EbGb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>SUB Eb, Ib</c> opcode.</summary>
    public static Opcode SubEbIb { get; } = new("sub", Execution.Sub.EbIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>SUB Ew, Gw</c> opcode.</summary>
    public static Opcode SubEwGw { get; } = new("sub", Execution.Sub.EwGw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>SUB Ew, Ib</c> opcode.</summary>
    public static Opcode SubEwIb { get; } = new("sub", Execution.Sub.EwIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>SUB Ew, Iw</c> opcode.</summary>
    public static Opcode SubEwIw { get; } = new("sub", Execution.Sub.EwIw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>SUB Gb, Eb</c> opcode.</summary>
    public static Opcode SubGbEb { get; } = new("sub", Execution.Sub.GbEb, 0, null);

    /// <summary>The <c>SUB Gw, Ew</c> opcode.</summary>
    public static Opcode SubGwEw { get; } = new("sub", Execution.Sub.GwEw, 0, null);

    /// <summary>The <c>TEST AL, Ib</c> opcode.</summary>
    public static Opcode TestALIb { get; } = new("test", Execution.Test.ALIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>TEST AX, Iw</c> opcode.</summary>
    public static Opcode TestAXIw { get; } = new("test", Execution.Test.AXIw, 0, ImmediateSizes.Word);

    /// <summary>The <c>TEST Eb, Gb</c> opcode.</summary>
    public static Opcode TestEbGb { get; } = new("test", Execution.Test.EbGb, 0, null);

    /// <summary>The <c>TEST Eb, Ib</c> opcode.</summary>
    public static Opcode TestEbIb { get; } = new("test", Execution.Test.EbIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>TEST Ew, Gw</c> opcode.</summary>
    public static Opcode TestEwGw { get; } = new("test", Execution.Test.EwGw, 0, null);

    /// <summary>The <c>TEST Ew, Iw</c> opcode.</summary>
    public static Opcode TestEwIw { get; } = new("test", Execution.Test.EwIw, 0, ImmediateSizes.Word);

    /// <summary>The <c>WAIT</c> opcode.</summary>
    public static Opcode Wait { get; } = new("wait", Execution.Wait._, 0, null);

    /// <summary>The <c>XCHG AX, Zw</c> opcode.</summary>
    public static Opcode XchgAXZw { get; } = new("xchg", Execution.Xchg.AXZw, 0, null);

    /// <summary>The <c>XCHG Eb, Gb</c> opcode.</summary>
    public static Opcode XchgEbGb { get; } = new("xchg", Execution.Xchg.EbGb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>XCHG Ew, Gw</c> opcode.</summary>
    public static Opcode XchgEwGw { get; } = new("xchg", Execution.Xchg.EwGw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>XLAT</c> opcode.</summary>
    public static Opcode Xlat { get; } = new("xlat", Execution.Xlat._, 0, null);

    /// <summary>The <c>XOR AL, Ib</c> opcode.</summary>
    public static Opcode XorALIb { get; } = new("xor", Execution.Xor.ALIb, 0, ImmediateSizes.Byte);

    /// <summary>The <c>XOR AX, Iw</c> opcode.</summary>
    public static Opcode XorAXIw { get; } = new("xor", Execution.Xor.AXIw, 0, ImmediateSizes.Word);

    /// <summary>The <c>XOR Eb, Gb</c> opcode.</summary>
    public static Opcode XorEbGb { get; } = new("xor", Execution.Xor.EbGb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>XOR Eb, Ib</c> opcode.</summary>
    public static Opcode XorEbIb { get; } = new("xor", Execution.Xor.EbIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>XOR Ew, Gw</c> opcode.</summary>
    public static Opcode XorEwGw { get; } = new("xor", Execution.Xor.EwGw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>XOR Ew, Ib</c> opcode.</summary>
    public static Opcode XorEwIb { get; } = new("xor", Execution.Xor.EwIb, OpcodeFlags.Lockable, null);

    /// <summary>The <c>XOR Ew, Iw</c> opcode.</summary>
    public static Opcode XorEwIw { get; } = new("xor", Execution.Xor.EwIw, OpcodeFlags.Lockable, null);

    /// <summary>The <c>XOR Gb, Eb</c> opcode.</summary>
    public static Opcode XorGbEb { get; } = new("xor", Execution.Xor.GbEb, 0, null);

    /// <summary>The <c>XOR Gw, Ew</c> opcode.</summary>
    public static Opcode XorGwEw { get; } = new("xor", Execution.Xor.GwEw, 0, null);
}
