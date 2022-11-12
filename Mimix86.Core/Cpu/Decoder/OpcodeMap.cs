/* =============================================================================
 * File:   OpcodeMap.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Enumerates all the opcode map entries for single byte opcodes.
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

using static Mimix86.Core.Cpu.Decoder.DecodeFlags;
using static Mimix86.Core.Cpu.Decoder.Opcode;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Enumerates all the supported opcodes by their location in the various opmaps.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
#pragma warning disable CS1591 // TODO: remove (XML doc)
public static class OpcodeMap
{
    public static readonly OpcodeMapEntry[] OpcodeError =
    {
        new(Error, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode00 =
    {
        // ADD Eb, Gb
        new(AddEbGb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode01 =
    {
        // ADD Ev, Gv
        new(AddEwGw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode02 =
    {
        // ADD Gb, Eb
        new(AddGbEb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode03 =
    {
        // ADD Gv, Ev
        new(AddGwEw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode04 =
    {
        // ADD AL, Ib
        new(AddALIb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode05 =
    {
        // ADD rAX, Iz
        new(AddAXIw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode06 =
    {
        // PUSH ES
        new(PushES, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode07 =
    {
        // POP ES
        new(PopES, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode08 =
    {
        // OR Eb, Gb
        new(OrEbGb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode09 =
    {
        // OR Ev, Gv
        new(OrEwGw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode0A =
    {
        // OR Gb, Eb
        new(OrGbEb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode0B =
    {
        // OR Gv, Ev
        new(OrGwEw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode0C =
    {
        // OR AL, Ib
        new(OrALIb, ..) ,
    };

    public static readonly OpcodeMapEntry[] Opcode0D =
    {
        // OR, rAX, Iz
        new(OrAXIw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode0E =
    {
        // PUSH CS
        new(PushCS, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode0F =
    {
        // [8086] POP CS
        new(PopCS, ..0), // documented as "illegal"
    };

    public static readonly OpcodeMapEntry[] Opcode10 =
    {
        // ADC Eb, Gb
        new(AdcEbGb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode11 =
    {
        // ADC Ev, Gv
        new(AdcEwGw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode12 =
    {
        // ADC Gb, Eb
        new(AdcGbEb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode13 =
    {
        // ADC Gv, Ev
        new(AdcGwEw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode14 =
    {
        // ADC AL, Ib
        new(AdcALIb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode15 =
    {
        // ADC rAX, Iz
        new(AdcAXIw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode16 =
    {
        // PUSH SS
        new(PushSS, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode17 =
    {
        // POP SS
        new(PopSS, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode18 =
    {
        // SBB Eb, Gb
        new(SbbEbGb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode19 =
    {
        // SBB Ev, Gv
        new(SbbEwGw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode1A =
    {
        // SBB Gb, Eb
        new(SbbGbEb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode1B =
    {
        // SBB Gv, Ev
        new(SbbGwEw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode1C =
    {
        // SBB AL, Ib
        new(SbbALIb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode1D =
    {
        // SBB rAX, Iz
        new(SbbAXIw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode1E =
    {
        // PUSH DS
        new(PushDS, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode1F =
    {
        // POP DS
        new(PopDS, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode20 =
    {
        // AND Eb, Gb
        new(AndEbGb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode21 =
    {
        // AND Ev, Gv
        new(AndEwGw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode22 =
    {
        // AND Gb, Eb
        new(AndGbEb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode23 =
    {
        // AND Gv, Ev
        new(AndGwEw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode24 =
    {
        // AND AL, Ib
        new(AndALIb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode25 =
    {
        // AND rAX, Iz
        new(AndAXIw, ..),
    };

    // [26] is ES: prefix; handled in decoders

    public static readonly OpcodeMapEntry[] Opcode27 =
    {
        // DAA
        new(Daa, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode28 =
    {
        // SUB Eb, Gb
        new(SubEbGb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode29 =
    {
        // SUB Ev, Gv
        new(SubEwGw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode2A =
    {
        // SUB Gb, Eb
        new(SubGbEb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode2B =
    {
        // SUB Gv, Ev
        new(SubGwEw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode2C =
    {
        // SUB AL, Ib
        new(SubALIb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode2D =
    {
        // SUB rAX, Iz
        new(SubAXIw, ..),
    };

    // [2E] is CS: prefix; handled in decoder

    public static readonly OpcodeMapEntry[] Opcode2F =
    {
        // DAS
        new(Das, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode30 =
    {
        // XOR Eb, Gb
        new(XorEbGb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode31 =
    {
        // XOR Ev, Gv
        new(XorEwGw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode32 =
    {
        // XOR Gb, Eb
        new(XorGbEb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode33 =
    {
        // XOR Gv, Ev
        new(XorGwEw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode34 =
    {
        // XOR AL, Ib
        new(XorALIb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode35 =
    {
        // XOR rAX, Iz
        new(XorAXIw, ..),
    };

    // [36] is SS: prefix; handled in decoder

    public static readonly OpcodeMapEntry[] Opcode37 =
    {
        // AAA
        new(Aaa, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode38 =
    {
        // CMP Eb, Gb
        new(CmpEbGb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode39 =
    {
        // CMP Ev, Gv
        new(CmpEwGw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode3A =
    {
        // CMP Gb, Eb
        new(CmpGbEb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode3B =
    {
        // CMP Gv, Ev
        new(CmpGwEw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode3C =
    {
        // CMP AL, Ib
        new(CmpALIb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode3D =
    {
        // CMP rAX, Iz
        new(CmpAXIw, ..),
    };

    // [3E] is DS: prefix; handled in decoder

    public static readonly OpcodeMapEntry[] Opcode3F =
    {
        // AAS
        new(Aas, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode40x47 =
    {
        // INC Zv
        new(IncZw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode48x4F =
    {
        // DEC Zv
        new(DecZw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode50x57 =
    {
        // PUSH Zv
        new(PushZw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode58x5F =
    {
        // POP Zv
        new(PopZw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode60 =
    {
        // [8086] Jcc Jb <-- duplicate of [70]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode61 =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [71]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode62 =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [72]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode63 =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [73]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode64 =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [74]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode65 =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [75]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode66 =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [76]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode67 =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [77]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode68 =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [78]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode69 =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [79]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode6A =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [7A]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode6B =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [7B]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode6C =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [7C]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode6D =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [7D]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode6E =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [7E]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode6F =
    {
        // [8086] Jcc Jb <-- undefined duplicate of [7F]
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode70x7F =
    {
        // Jcc Jb
        new(JccJb, ..0),
    };

    public static readonly OpcodeMapEntry[] Opcode80 =
    {
        // "group 1" Eb, Ib
        new(AddEbIb, .., REG0),
        new(OrEbIb, .., REG1),
        new(AdcEbIb, .., REG2),
        new(SbbEbIb, .., REG3),
        new(AndEbIb, .., REG4),
        new(SubEbIb, .., REG5),
        new(XorEbIb, .., REG6),
        new(CmpEbIb, .., REG7),
    };

    public static readonly OpcodeMapEntry[] Opcode81 =
    {
        // "group 1" Ev, Iz
        new(AddEwIw, .., REG0),
        new(OrEwIw, .., REG1),
        new(AdcEwIw, .., REG2),
        new(SbbEwIw, .., REG3),
        new(AndEwIw, .., REG4),
        new(SubEwIw, .., REG5),
        new(XorEwIw, .., REG6),
        new(CmpEwIw, .., REG7),
    };

    // TODO: 8086 manual/datasheet suggests OR/AND/XOR not allowed in [82]
    // [82] is an undefined copy of [80]; handled in decoder

    // TODO: 8086 manual/datasheet suggests OR/AND/XOR not allowed in [83]
    public static readonly OpcodeMapEntry[] Opcode83 =
    {
        // "group 1" Ev, Ib
        new(AddEwIb, .., REG0),
        new(OrEwIb, .., REG1),
        new(AdcEwIb, .., REG2),
        new(SbbEwIb, .., REG3),
        new(AndEwIb, .., REG4),
        new(SubEwIb, .., REG5),
        new(XorEwIb, .., REG6),
        new(CmpEwIb, .., REG7),
    };

    public static readonly OpcodeMapEntry[] Opcode84 =
    {
        // TEST Eb, Gb
        new(TestEbGb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode85 =
    {
        // TEST Ev, Gv
        new(TestEwGw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode86 =
    {
        // XCHG Eb, Gb
        new(XchgEbGb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode87 =
    {
        // XCHG Ev, Gv
        new(XchgEwGw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode88 =
    {
        // MOV Eb, Gb
        new(MovEbGb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode89 =
    {
        // MOV Ev, Gv
        new(MovEwGw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode8A =
    {
        // MOV Gb, Eb
        new(MovGbEb, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode8B =
    {
        // MOV Gv, Ev
        new(MovGwEw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode8C =
    {
        // MOV Ew, Sw
        new(MovEwSw, ..), // TODO: what happens on 8086 if Sw is >=4?
    };

    public static readonly OpcodeMapEntry[] Opcode8D =
    {
        // LEA Gv, M
        new(LeaGwM, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode8E =
    {
        // MOV Sw, Ew
        new(MovSwGw, ..), // TODO: what happens on 8086 if Sw is >=4?
    };

    public static readonly OpcodeMapEntry[] Opcode8F =
    {
        // "group 1A"
        new(PopEw, ..0), // [8086] matches all?; TODO: verify
    };

    public static readonly OpcodeMapEntry[] Opcode90 =
    {
        // NOP
        new(Nop, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode91x97 =
    {
        // XCHG rAX, Zv
        new(XchgAXZw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode98 =
    {
        // CBW / CWDE / CDQE
        new(Cbw, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode99 =
    {
        // CWD / CDQ / CQO
        new(Cwd, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode9A =
    {
        // CALL Ap
        new(CallApww, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode9B =
    {
        // WAIT
        new(Wait, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode9C =
    {
        // PUSHF
        new(Pushf, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode9D =
    {
        // POPF
        new(Popf, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode9E =
    {
        // SAHF
        new(Sahf, ..),
    };

    public static readonly OpcodeMapEntry[] Opcode9F =
    {
        // LAHF
        new(Lahf, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeA0 =
    {
        // MOV AL, Ob
        new(MovALOb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeA1 =
    {
        // MOV rAX, Ov
        new(MovAXOw, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeA2 =
    {
        // MOV Ob, AL
        new(MovObAL, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeA3 =
    {
        // MOV Ov, rAX
        new(MovOwAX, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeA4 =
    {
        // MOVSB
        new(Movsb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeA5 =
    {
        // MOVSW / ...
        new(Movsw, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeA6 =
    {
        // CMPSB
        new(Cmpsb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeA7 =
    {
        // CMPSW / ...
        new(Cmpsw, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeA8 =
    {
        // TEST AL, Ib
        new(TestALIb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeA9 =
    {
        // TEST rAX, Iz
        new(TestAXIw, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeAA =
    {
        // STOSB
        new(Stosb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeAB =
    {
        // STOSW / ...
        new(Stosw, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeAC =
    {
        // LODSB
        new(Lodsb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeAD =
    {
        // LODSW / ...
        new(Lodsw, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeAE =
    {
        // SCASB
        new(Scasb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeAF =
    {
        // SCASW / ...
        new(Scasw, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeB0xB7 =
    {
        // MOV Zb, Ib
        new(MovZbIb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeB8xBF =
    {
        // MOV Zv, Iv
        new(MovZwIw, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeC0 =
    {
        // [8086] RET Iw
        new(RetIw, ..0),
    };

    public static readonly OpcodeMapEntry[] OpcodeC1 =
    {
        // [8086] RET
        new(Ret, ..0),
    };

    public static readonly OpcodeMapEntry[] OpcodeC2 =
    {
        // RET Iw
        new(RetIw, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeC3 =
    {
        // RET
        new(Ret, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeC4 =
    {
        // LES Gw, Mp
        new(LesGwMw, ..), // TODO: sandpile.org says LES Gw, (IND:TMP)?
    };

    public static readonly OpcodeMapEntry[] OpcodeC5 =
    {
        // LDS Gw, Mp
        new(LdsGwMw, ..), // TODO: sandpile.org says LDS Gw, (IND:TMP)?
    };

    public static readonly OpcodeMapEntry[] OpcodeC6 =
    {
        // "group 11" Eb, Ib
        new(MovEbIb, ..0), // [8086] matches all?; TODO: verify
    };

    public static readonly OpcodeMapEntry[] OpcodeC7 =
    {
        // "group 11" Ev, Iz
        new(MovEwIw, ..0), // [8086] matches all?; TODO: verify
    };

    public static readonly OpcodeMapEntry[] OpcodeC8 =
    {
        // [8086] RETF Iw
        new(RetfIw, ..0),
    };

    public static readonly OpcodeMapEntry[] OpcodeC9 =
    {
        // [8086] RETF
        new(Retf, ..0),
    };

    public static readonly OpcodeMapEntry[] OpcodeCA =
    {
        // RETF Iw
        new(RetfIw, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeCB =
    {
        // RETF
        new(Retf, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeCC =
    {
        // INT 3
        new(Int3, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeCD =
    {
        // INT Ib
        new(IntIb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeCE =
    {
        // INTO
        new(Into, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeCF =
    {
        // IRET
        new(Iret, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeD0 =
    {
        // "group 2" Eb, 1
        new(RolEb1, .., REG0),
        new(RorEb1, .., REG1),
        new(RclEb1, .., REG2),
        new(RcrEb1, .., REG3),
        new(ShlEb1, .., REG4),
        new(ShrEb1, .., REG5),
        new(ShlEb1, .., REG6), // undocumented "shift arithmetic left"; i.e. copy of /4
        new(SarEb1, .., REG7),
    };

    public static readonly OpcodeMapEntry[] OpcodeD1 =
    {
        // "group 2" Ev, 1
        new(RolEw1, .., REG0),
        new(RorEw1, .., REG1),
        new(RclEw1, .., REG2),
        new(RcrEw1, .., REG3),
        new(ShlEw1, .., REG4),
        new(ShrEw1, .., REG5),
        new(ShlEw1, .., REG6), // undocumented "shift arithmetic left"; i.e. copy of /4
        new(SarEw1, .., REG7),
    };

    public static readonly OpcodeMapEntry[] OpcodeD2 =
    {
        // "group 2" Ev, 1
        new(RolEbCL, .., REG0),
        new(RorEbCL, .., REG1),
        new(RclEbCL, .., REG2),
        new(RcrEbCL, .., REG3),
        new(ShlEbCL, .., REG4),
        new(ShrEbCL, .., REG5),
        new(ShlEbCL, .., REG6), // undocumented "shift arithmetic left"; i.e. copy of /4
        new(SarEbCL, .., REG7),
    };

    public static readonly OpcodeMapEntry[] OpcodeD3 =
    {
        // "group 2" Ev, CL
        new(RolEwCL, .., REG0),
        new(RorEwCL, .., REG1),
        new(RclEwCL, .., REG2),
        new(RcrEwCL, .., REG3),
        new(ShlEwCL, .., REG4),
        new(ShrEwCL, .., REG5),
        new(ShlEwCL, .., REG6), // undocumented "shift arithmetic left"; i.e. copy of /4
        new(SarEwCL, .., REG7),
    };

    public static readonly OpcodeMapEntry[] OpcodeD4 =
    {
        // AAM Ib
        new(AamIb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeD5 =
    {
        // AAD Ib
        new(AadIb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeD6 =
    {
        // SALC
        new(Salc, ..), // TODO: not documented on 8086; was it present?
    };

    public static readonly OpcodeMapEntry[] OpcodeD7 =
    {
        // XLAT
        new(Xlat, ..),
    };

#pragma warning disable CA1825
    // TODO: [D8]..[DF] is ESC
    public static readonly OpcodeMapEntry[] OpcodeD8 = { };

    public static readonly OpcodeMapEntry[] OpcodeD9 = { };

    public static readonly OpcodeMapEntry[] OpcodeDA = { };

    public static readonly OpcodeMapEntry[] OpcodeDB = { };

    public static readonly OpcodeMapEntry[] OpcodeDC = { };

    public static readonly OpcodeMapEntry[] OpcodeDD = { };

    public static readonly OpcodeMapEntry[] OpcodeDE = { };

    public static readonly OpcodeMapEntry[] OpcodeDF = { };
#pragma warning restore CA1825

    public static readonly OpcodeMapEntry[] OpcodeE0 =
    {
        // LOOPNE Jb
        new(LoopneJb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeE1 =
    {
        // LOOPE Jb
        new(LoopeJb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeE2 =
    {
        // LOOP Jb
        new(LoopJb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeE3 =
    {
        // JrCXZ Jb
        new(JcxzJb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeE4 =
    {
        // IN AL, Ib
        new(InALIb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeE5 =
    {
        // IN eAX, Ib
        new(InAXIb, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeE6 =
    {
        // OUT Ib, AL
        new(OutIbAL, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeE7 =
    {
        // OUT Ib, eAX
        new(OutIbAX, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeE8 =
    {
        // CALL Jz
        new(CallJw, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeE9 =
    {
        // JMP Jz
        new(JmpJw, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeEA =
    {
        // JMP Ap
        new(JmpApww, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeEB =
    {
        // JMP Jb
        new(JmpJb),
    };

    public static readonly OpcodeMapEntry[] OpcodeEC =
    {
        // IN AL, DX
        new(InALDX, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeED =
    {
        // IN eAX, DX
        new(InAXDX, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeEE =
    {
        // OUT DX, AL
        new(OutDXAL, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeEF =
    {
        // OUT DX, eAX
        new(OutDXAX, ..),
    };

    // [F0] is LOCK: prefix; handled in decoder

    // [8086] [F1] is LOCK: prefix; handled in decoder
    public static readonly OpcodeMapEntry[] OpcodeF1 = { };

    // [F2] is REPNE prefix; handled in decoder

    // [F3] is REPE prefix; handled in decoder

    public static readonly OpcodeMapEntry[] OpcodeF4 =
    {
        // HLT
        new(Hlt, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeF5 =
    {
        // CMC
        new(Cmc, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeF6 =
    {
        // "group 3" Eb
        new(TestEbIb, .., REG0),
        new(TestEbIb, .., REG1), // undocumented; copy of /0
        new(NotEb, .., REG2),
        new(NegEb, .., REG3),
        new(MulEb, .., REG4),
        new(ImulEb, .., REG5),
        new(DivEb, .., REG6),
        new(IdivEb, .., REG7),
    };

    public static readonly OpcodeMapEntry[] OpcodeF7 =
    {
        // "group 3" Ev
        new(TestEwIw, .., REG0),
        new(TestEwIw, .., REG1), // undocumented; copy of /0
        new(NotEw, .., REG2),
        new(NegEw, .., REG3),
        new(MulEw, .., REG4),
        new(ImulEw, .., REG5),
        new(DivEw, .., REG6),
        new(IdivEw, .., REG7),
    };

    public static readonly OpcodeMapEntry[] OpcodeF8 =
    {
        // CLC
        new(Clc, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeF9 =
    {
        // STC
        new(Stc, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeFA =
    {
        // CLI
        new(Cli, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeFB =
    {
        // STI
        new(Sti, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeFC =
    {
        // CLD
        new(Cld, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeFD =
    {
        // STD
        new(Std, ..),
    };

    public static readonly OpcodeMapEntry[] OpcodeFE =
    {
        // "group 4"
        new(IncEb, .., REG0),
        new(DecEb, .., REG1),
        // TODO: [8086] operation for /2../7
    };

    public static readonly OpcodeMapEntry[] OpcodeFF =
    {
        // "group 5"
        new(IncEw, .., REG0),
        new(DecEw, .., REG1),
        new(CallEw, .., REG2),
        new(CallMpww, .., REG3),
        new(JmpEw, .., REG4),
        new(JmpMpww, .., REG5),
        new(PushEw, .., REG6),
        // TODO: [8086] operation for /7
    };
}
