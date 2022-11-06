/* =============================================================================
 * File:   Opcode.List.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * <TODO>
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

using Mimix86.Core.Cpu.Execution;

#pragma warning disable CS1591 // missing XML doc comment

namespace Mimix86.Core.Cpu.Decoder;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public partial class Opcode
{
    public static Opcode Error { get; } = new("aaa", null, Execution.Error._);


    public static Opcode Aaa { get; } = new("aaa", null, Execution.Aaa._);

    public static Opcode AadIb { get; } = new("aad", "Ib", Aad.Ib);

    public static Opcode AamIb { get; } = new("aam", "Ib", Aam.Ib);

    public static Opcode Aas { get; } = new("aas", null, Execution.Aas._);

    public static Opcode AdcEbGb { get; } = new("adc", "Eb Gb", Adc.EbGb);
    public static Opcode AdcEwGw { get; } = new("adc", "Ew Gw", Adc.EwGw);
    public static Opcode AdcGbEb { get; } = new("adc", "Gb Eb", Adc.GbEb);
    public static Opcode AdcGwEw { get; } = new("adc", "Gw Ew", Adc.GwEw);
    public static Opcode AdcALIb { get; } = new("adc", "AL Ib", Adc.ALIb);
    public static Opcode AdcAXIw { get; } = new("adc", "AX Iw", Adc.AXIw);
    public static Opcode AdcEbIb { get; } = new("adc", "Eb Ib", Adc.EbIb);
    public static Opcode AdcEwIb { get; } = new("adc", "Ew Ib", Adc.EwIb);
    public static Opcode AdcEwIw { get; } = new("adc", "Ew Iw", Adc.EwIw);

    public static Opcode AddEbGb { get; } = new("add", "Eb Gb", Add.EbGb);
    public static Opcode AddEwGw { get; } = new("add", "Ew Gw", Add.EwGw);
    public static Opcode AddGbEb { get; } = new("add", "Gb Eb", Add.GbEb);
    public static Opcode AddGwEw { get; } = new("add", "Gw Ew", Add.GwEw);
    public static Opcode AddALIb { get; } = new("add", "AL Ib", Add.ALIb);
    public static Opcode AddAXIw { get; } = new("add", "AX Iw", Add.AXIw);
    public static Opcode AddEbIb { get; } = new("add", "Eb Ib", Add.EbIb);
    public static Opcode AddEwIb { get; } = new("add", "Ew Ib", Add.EwIb);
    public static Opcode AddEwIw { get; } = new("add", "Ew Iw", Add.EwIw);

    public static Opcode AndEbGb { get; } = new("and", "Eb Gb", And.EbGb);
    public static Opcode AndEwGw { get; } = new("and", "Ew Gw", And.EwGw);
    public static Opcode AndGbEb { get; } = new("and", "Gb Eb", And.GbEb);
    public static Opcode AndGwEw { get; } = new("and", "Gw Ew", And.GwEw);
    public static Opcode AndALIb { get; } = new("and", "AL Ib", And.ALIb);
    public static Opcode AndAXIw { get; } = new("and", "AX Iw", And.AXIw);
    public static Opcode AndEbIb { get; } = new("and", "Eb Ib", And.EbIb);
    public static Opcode AndEwIb { get; } = new("and", "Ew Ib", And.EwIb);
    public static Opcode AndEwIw { get; } = new("and", "Ew Iw", And.EwIw);

    public static Opcode CallApww { get; } = new("call", "apww", Call.Apww);
    public static Opcode CallEw { get; } = new("call", "ew", Call.Ew);
    public static Opcode CallJw { get; } = new("call", "jw", Call.Jw);
    public static Opcode CallMpww { get; } = new("call", "mpww", Call.Mpww);

    public static Opcode Cbw { get; } = new("cbw", null, Execution.Cbw._);

    public static Opcode Clc { get; } = new("clc", null, Execution.Clc._);

    public static Opcode Cld { get; } = new("cld", null, Execution.Cld._);

    public static Opcode Cli { get; } = new("cli", null, Execution.Cli._);

    public static Opcode Cmc { get; } = new("cmc", null, Execution.Cmc._);

    public static Opcode CmpEbGb { get; } = new("cmp", "Eb Gb", Cmp.EbGb);
    public static Opcode CmpEwGw { get; } = new("cmp", "Ew Gw", Cmp.EwGw);
    public static Opcode CmpGbEb { get; } = new("cmp", "Gb Eb", Cmp.GbEb);
    public static Opcode CmpGwEw { get; } = new("cmp", "Gw Ew", Cmp.GwEw);
    public static Opcode CmpALIb { get; } = new("cmp", "AL Ib", Cmp.ALIb);
    public static Opcode CmpAXIw { get; } = new("cmp", "AX Iw", Cmp.AXIw);
    public static Opcode CmpEbIb { get; } = new("cmp", "Eb Ib", Cmp.EbIb);
    public static Opcode CmpEwIb { get; } = new("cmp", "Ew Ib", Cmp.EwIb);
    public static Opcode CmpEwIw { get; } = new("cmp", "Ew Iw", Cmp.EwIw);

    public static Opcode Cmpsb { get; } = new("cmpsb", null, Execution.Cmpsb._);

    public static Opcode Cmpsw { get; } = new("cmpsw", null, Execution.Cmpsw._);

    public static Opcode Cwd { get; } = new("cwd", null, Execution.Cwd._);

    public static Opcode Daa { get; } = new("daa", null, Execution.Daa._);

    public static Opcode Das { get; } = new("das", null, Execution.Das._);

    public static Opcode DecEb { get; } = new("dec", "Eb", Dec.Eb);
    public static Opcode DecEw { get; } = new("dec", "Ew", Dec.Ew);
    public static Opcode DecZw { get; } = new("dec", "Zw", Dec.Zw);

    public static Opcode DivEb { get; } = new("div", "Eb", Div.Eb);
    public static Opcode DivEw { get; } = new("div", "Ew", Div.Ew);

    public static Opcode Hlt { get; } = new("hlt", null, Execution.Hlt._);

    public static Opcode IdivEb { get; } = new("idiv", "Eb", Idiv.Eb);
    public static Opcode IdivEw { get; } = new("idiv", "Ew", Idiv.Ew);

    public static Opcode ImulEb { get; } = new("imul", "Eb", Imul.Eb);
    public static Opcode ImulEw { get; } = new("imul", "Ew", Imul.Ew);

    public static Opcode InALDX { get; } = new("in", "AL DX", In.ALDX);
    public static Opcode InALIb { get; } = new("in", "AL Ib", In.ALIb);
    public static Opcode InAXDX { get; } = new("in", "AX DX", In.AXDX);
    public static Opcode InAXIb { get; } = new("in", "AX Ib", In.AXIb);

    public static Opcode IncEb { get; } = new("inc", "Eb", Inc.Eb);
    public static Opcode IncEw { get; } = new("inc", "Ew", Inc.Ew);
    public static Opcode IncZw { get; } = new("inc", "Zw", Inc.Zw);

    public static Opcode Int3 { get; } = new("int", "3", Int._3);
    public static Opcode IntIb { get; } = new("int", "Ib", Int.Ib);

    public static Opcode Into { get; } = new("into", null, Execution.Into._);

    public static Opcode Iret { get; } = new("iret", null, Execution.Iret._);

    public static Opcode JccJb { get; } = new("jcc", "Jb", Jcc.Jb);

    public static Opcode JcxzJb { get; } = new("jcxz", "Jb", Jcxz.Jb);

    public static Opcode JmpApww { get; } = new("jmp", "Apww", Jmp.Apww);
    public static Opcode JmpEw { get; } = new("jmp", "Ew", Jmp.Ew);
    public static Opcode JmpJb { get; } = new("jmp", "Jb", Jmp.Jb);
    public static Opcode JmpJw { get; } = new("jmp", "Jw", Jmp.Jw);
    public static Opcode JmpMpww { get; } = new("jmp", "Mpww", Jmp.Mpww);

    public static Opcode Lahf { get; } = new("lahf", null, Execution.Lahf._);

    public static Opcode LdsGwMw { get; } = new("lds", "Gw Mw", Lds.GwMw);

    public static Opcode LeaGwM { get; } = new("lea", "Gw M", Lea.GwM);

    public static Opcode LesGwMw { get; } = new("les", "Gw Mw", Les.GwMw);

    public static Opcode Lodsb { get; } = new("lodsb", null, Execution.Lodsb._);
    public static Opcode Lodsw { get; } = new("lodsw", null, Execution.Lodsw._);

    public static Opcode LoopJb { get; } = new("loop", "Jb", Loop.Jb);
    public static Opcode LoopeJb { get; } = new("loope", "Jb", Loope.Jb);
    public static Opcode LoopneJb { get; } = new("loopne", "Jb", Loopne.Jb);

    public static Opcode MovALOb { get; } = new("mov", "AL Ob", Mov.ALOb);
    public static Opcode MovAXOw { get; } = new("mov", "AX Ow", Mov.AXOw);
    public static Opcode MovEbGb { get; } = new("mov", "Eb Gb", Mov.EbGb);
    public static Opcode MovEbIb { get; } = new("mov", "Eb Ib", Mov.EbIb);
    public static Opcode MovEwGw { get; } = new("mov", "Ew Gw", Mov.EwGw);
    public static Opcode MovEwIw { get; } = new("mov", "Ew Iw", Mov.EwIw);
    public static Opcode MovEwSw { get; } = new("mov", "Ew Sw", Mov.EwSw);
    public static Opcode MovGbEb { get; } = new("mov", "Gb Eb", Mov.GbEb);
    public static Opcode MovGwEw { get; } = new("mov", "Gw Ew", Mov.GwEw);
    public static Opcode MovObAL { get; } = new("mov", "Ob AL", Mov.ObAL);
    public static Opcode MovOwAX { get; } = new("mov", "Ow AX", Mov.OwAX);
    public static Opcode MovSwGw { get; } = new("mov", "Sw Gw", Mov.SwGw);
    public static Opcode MovZbIb { get; } = new("mov", "Zb Ib", Mov.ZbIb);
    public static Opcode MovZwIw { get; } = new("mov", "Zw Iw", Mov.ZwIw);

    public static Opcode Movsb { get; } = new("movsb", null, Execution.Movsb._);
    public static Opcode Movsw { get; } = new("movsw", null, Execution.Movsw._);

    public static Opcode MulEb { get; } = new("mul", "Eb", Mul.Eb);
    public static Opcode MulEw { get; } = new("mul", "Ew", Mul.Ew);

    public static Opcode NegEb { get; } = new("neg", "Eb", Neg.Eb);
    public static Opcode NegEw { get; } = new("neg", "Ew", Neg.Ew);

    public static Opcode Nop { get; } = new("nop", null, Execution.Nop._);

    public static Opcode NotEb { get; } = new("not", "Eb", Not.Eb);
    public static Opcode NotEw { get; } = new("not", "Ew", Not.Ew);

    public static Opcode OrEbGb { get; } = new("or", "Eb Gb", Or.EbGb);
    public static Opcode OrEwGw { get; } = new("or", "Ew Gw", Or.EwGw);
    public static Opcode OrGbEb { get; } = new("or", "Gb Eb", Or.GbEb);
    public static Opcode OrGwEw { get; } = new("or", "Gw Ew", Or.GwEw);
    public static Opcode OrALIb { get; } = new("or", "AL Ib", Or.ALIb);
    public static Opcode OrAXIw { get; } = new("or", "AX Iw", Or.AXIw);
    public static Opcode OrEbIb { get; } = new("or", "Eb Ib", Or.EbIb);
    public static Opcode OrEwIb { get; } = new("or", "Ew Ib", Or.EwIb);
    public static Opcode OrEwIw { get; } = new("or", "Ew Iw", Or.EwIw);

    public static Opcode OutDXAL { get; } = new("out", "DX AL", Out.DXAL);
    public static Opcode OutDXAX { get; } = new("out", "DX AX", Out.DXAX);
    public static Opcode OutIbAL { get; } = new("out", "Ib AL", Out.IbAL);
    public static Opcode OutIbAX { get; } = new("out", "Ib AX", Out.IbAX);

    public static Opcode PopEw { get; } = new("pop", "Ew", Pop.Ew);
    public static Opcode PopZw { get; } = new("pop", "Zw", Pop.Zw);
    public static Opcode PopCS { get; } = new("pop", "CS", Pop.CS);
    public static Opcode PopDS { get; } = new("pop", "DS", Pop.DS);
    public static Opcode PopES { get; } = new("pop", "ES", Pop.ES);
    public static Opcode PopSS { get; } = new("pop", "SS", Pop.SS);

    public static Opcode Popf { get; } = new("popf", null, Execution.Popf._);

    public static Opcode PushEw { get; } = new("push", "Ew", Push.Ew);
    public static Opcode PushZw { get; } = new("push", "Zw", Push.Zw);
    public static Opcode PushCS { get; } = new("push", "CS", Push.CS);
    public static Opcode PushDS { get; } = new("push", "DS", Push.DS);
    public static Opcode PushES { get; } = new("push", "ES", Push.ES);
    public static Opcode PushSS { get; } = new("push", "SS", Push.SS);

    public static Opcode Pushf { get; } = new("pushf", null, Execution.Pushf._);

    public static Opcode RclEb1 { get; } = new("rcl", "Eb 1", Rcl.Eb1);
    public static Opcode RclEbCL { get; } = new("rcl", "Eb CL", Rcl.EbCL);
    public static Opcode RclEw1 { get; } = new("rcl", "Ew 1", Rcl.Ew1);
    public static Opcode RclEwCL { get; } = new("rcl", "Ew CL", Rcl.EwCL);

    public static Opcode RcrEb1 { get; } = new("rcr", "Eb 1", Rcr.Eb1);
    public static Opcode RcrEbCL { get; } = new("rcr", "Eb CL", Rcr.EbCL);
    public static Opcode RcrEw1 { get; } = new("rcr", "Ew 1", Rcr.Ew1);
    public static Opcode RcrEwCL { get; } = new("rcr", "Ew CL", Rcr.EwCL);

    public static Opcode Ret { get; } = new("ret", null, Execution.Ret._);
    public static Opcode RetIw { get; } = new("ret", "Iw", Execution.Ret.Iw);

    public static Opcode Retf { get; } = new("retf", null, Execution.Retf._);
    public static Opcode RetfIw { get; } = new("retf", "Iw", Execution.Retf.Iw);

    public static Opcode RolEb1 { get; } = new("rol", "Eb 1", Rol.Eb1);
    public static Opcode RolEbCL { get; } = new("rol", "Eb CL", Rol.EbCL);
    public static Opcode RolEw1 { get; } = new("rol", "Ew 1", Rol.Ew1);
    public static Opcode RolEwCL { get; } = new("rol", "Ew CL", Rol.EwCL);

    public static Opcode RorEb1 { get; } = new("ror", "Eb 1", Ror.Eb1);
    public static Opcode RorEbCL { get; } = new("ror", "Eb CL", Ror.EbCL);
    public static Opcode RorEw1 { get; } = new("ror", "Ew 1", Ror.Ew1);
    public static Opcode RorEwCL { get; } = new("ror", "Ew CL", Ror.EwCL);

    public static Opcode Sahf { get; } = new("sahf", null, Execution.Sahf._);

    public static Opcode Salc { get; } = new("salc", null, Execution.Salc._);

    public static Opcode SarEb1 { get; } = new("sar", "Eb 1", Sar.Eb1);
    public static Opcode SarEbCL { get; } = new("sar", "Eb CL", Sar.EbCL);
    public static Opcode SarEw1 { get; } = new("sar", "Ew 1", Sar.Ew1);
    public static Opcode SarEwCL { get; } = new("sar", "Ew CL", Sar.EwCL);

    public static Opcode SbbEbGb { get; } = new("sbb", "Eb Gb", Sbb.EbGb);
    public static Opcode SbbEwGw { get; } = new("sbb", "Ew Gw", Sbb.EwGw);
    public static Opcode SbbGbEb { get; } = new("sbb", "Gb Eb", Sbb.GbEb);
    public static Opcode SbbGwEw { get; } = new("sbb", "Gw Ew", Sbb.GwEw);
    public static Opcode SbbALIb { get; } = new("sbb", "AL Ib", Sbb.ALIb);
    public static Opcode SbbAXIw { get; } = new("sbb", "AX Iw", Sbb.AXIw);
    public static Opcode SbbEbIb { get; } = new("sbb", "Eb Ib", Sbb.EbIb);
    public static Opcode SbbEwIb { get; } = new("sbb", "Ew Ib", Sbb.EwIb);
    public static Opcode SbbEwIw { get; } = new("sbb", "Ew Iw", Sbb.EwIw);

    public static Opcode Scasb { get; } = new("scasb", null, Execution.Scasb._);
    public static Opcode Scasw { get; } = new("scasw", null, Execution.Scasw._);

    public static Opcode ShlEb1 { get; } = new("shl", "Eb 1", Shl.Eb1);
    public static Opcode ShlEbCL { get; } = new("shl", "Eb CL", Shl.EbCL);
    public static Opcode ShlEw1 { get; } = new("shl", "Ew 1", Shl.Ew1);
    public static Opcode ShlEwCL { get; } = new("shl", "Ew CL", Shl.EwCL);

    public static Opcode ShrEb1 { get; } = new("shr", "Eb 1", Shr.Eb1);
    public static Opcode ShrEbCL { get; } = new("shr", "Eb CL", Shr.EbCL);
    public static Opcode ShrEw1 { get; } = new("shr", "Ew 1", Shr.Ew1);
    public static Opcode ShrEwCL { get; } = new("shr", "Ew CL", Shr.EwCL);

    public static Opcode Stc { get; } = new("stc", null, Execution.Stc._);

    public static Opcode Std { get; } = new("std", null, Execution.Std._);

    public static Opcode Sti { get; } = new("sti", null, Execution.Sti._);

    public static Opcode Stosb { get; } = new("stosb", null, Execution.Stosb._);
    public static Opcode Stosw { get; } = new("stosw", null, Execution.Stosw._);

    public static Opcode SubEbGb { get; } = new("sub", "Eb Gb", Sub.EbGb);
    public static Opcode SubEwGw { get; } = new("sub", "Ew Gw", Sub.EwGw);
    public static Opcode SubGbEb { get; } = new("sub", "Gb Eb", Sub.GbEb);
    public static Opcode SubGwEw { get; } = new("sub", "Gw Ew", Sub.GwEw);
    public static Opcode SubALIb { get; } = new("sub", "AL Ib", Sub.ALIb);
    public static Opcode SubAXIw { get; } = new("sub", "AX Iw", Sub.AXIw);
    public static Opcode SubEbIb { get; } = new("sub", "Eb Ib", Sub.EbIb);
    public static Opcode SubEwIb { get; } = new("sub", "Ew Ib", Sub.EwIb);
    public static Opcode SubEwIw { get; } = new("sub", "Ew Iw", Sub.EwIw);

    public static Opcode TestALIb { get; } = new("test", "AL Ib", Test.ALIb);
    public static Opcode TestAXIw { get; } = new("test", "AX Iw", Test.AXIw);
    public static Opcode TestEbGb { get; } = new("test", "Eb Gb", Test.EbGb);
    public static Opcode TestEbIb { get; } = new("test", "Eb Ib", Test.EbIb);
    public static Opcode TestEwGw { get; } = new("test", "Ew Gw", Test.EwGw);
    public static Opcode TestEwIw { get; } = new("test", "Ew Iw", Test.EwIw);

    public static Opcode Wait { get; } = new("wait", null, Execution.Wait._);

    public static Opcode XchgAXZw { get; } = new("xchg", "AX Zw", Xchg.AXZw);
    public static Opcode XchgEbGb { get; } = new("xchg", "Eb Gb", Xchg.EbGb);
    public static Opcode XchgEwGw { get; } = new("xchg", "Ew Gw", Xchg.EwGw);

    public static Opcode Xlat { get; } = new("xlat", null, Execution.Xlat._);

    public static Opcode XorEbGb { get; } = new("xor", "Eb Gb", Xor.EbGb);
    public static Opcode XorEwGw { get; } = new("xor", "Ew Gw", Xor.EwGw);
    public static Opcode XorGbEb { get; } = new("xor", "Gb Eb", Xor.GbEb);
    public static Opcode XorGwEw { get; } = new("xor", "Gw Ew", Xor.GwEw);
    public static Opcode XorALIb { get; } = new("xor", "AL Ib", Xor.ALIb);
    public static Opcode XorAXIw { get; } = new("xor", "AX Iw", Xor.AXIw);
    public static Opcode XorEbIb { get; } = new("xor", "Eb Ib", Xor.EbIb);
    public static Opcode XorEwIb { get; } = new("xor", "Ew Ib", Xor.EwIb);
    public static Opcode XorEwIw { get; } = new("xor", "Ew Iw", Xor.EwIw);
}
