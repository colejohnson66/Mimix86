; ==============================================================================
; File:   8086.lisp
; Author: Cole Tobin
; ==============================================================================
; Copyright (c) 2023 Cole Tobin
;
; This file is part of Mimix86.
;
; Mimix86 is free software: you can redistribute it and/or modify it under the
;   terms of the GNU General Public License as published by the Free Software
;   Foundation, either version 3 of the License, or (at your option) any later
;   version.
;
; Mimix86 is distributed in the hope that it will be useful, but WITHOUT ANY
;   WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
;   FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
;   details.
;
; You should have received a copy of the GNU General Public License along with
;   Mimix86. If not, see <http://www.gnu.org/licenses/>.
; ==============================================================================

(file
    (type  cpu-base-set)
    (name  8086)
)

; Flags:
;   - lockable: This opcode is lockable if, and only if, the destination operand is memory (i.e., the ModR/M's "mod"
;         bits indicate memory form)
;       How this interacts on the 8086 (which does not feature a #UD exception) when the ModR/M's "mod" bits indicate
;         register form is unknown.


; Undefined opcodes on 8086
; The 8086 does not have "undefined" opcodes; all byte sequences will do *something*.
;
; Disassembly of 8086 microcode is available here: <https://www.reenigne.org/blog/8086-microcode-disassembled/>
; The `microcode_8086.txt` file is a bit hard to read, especially the "match" column.
; It is formatted as `abbbbbbbb.pp` where:
;   - `a` is ???
;   - `bbbbbbbb` is the byte
;   - `pp` is the microcode "phase" (a counter)
; The exception is [F6], [F7], [FE], and [FF], where they are formatted as `aaaaaabbb.pp` where:
;   - `aaaaaa` is:
;     - `011110` for [F6]
;     - `111110` for [F7]
;     - `011111` for [FE]
;     - `111111` for [FF]
;     - all other [Fx] opcodes are handled by random logic
;   - `bbb` is the ModR/M's "reg" field
;   - `pp` is the microcode "phase" (the "PC")
;
;  - POP Sw
;    - `0000??111`, so all four segment registers match
;  - Jcc Jb
;    - `0011?????`, so [60-7F] match
;  - POP Ev
;    - `010001111` with no match on "reg" field, so [8F /1] through [8F /7] match
;  - RET
;    - `01100?0?0`, so [C0] and [C2] match
;  - RET
;    - `0110000?1`, so [C1] and [C3] match
;  - MOV Eb, Ib and MOV Ew, Iw
;    - `01100011?` with no match on "reg" field, so [C6 /1] through [C6 /7] and [C7 /1] through [C7 /7] match
;  - RETF
;    - `01100?0?0`, so [C8] and [CA] match
;  - RETF
;    - `0110010?1`, so [C9] and [CB] match
;  - TEST Eb Ib and TEST Ew Iw
;    - `?1111000?`, so [F6 /0], [F6 /1], [F7 /0], and [F7 /1] match
;  - CALL Ew
;    - `?11111010`, so [FE /2] and [FF /2] match
;  - CALL Mpww
;    - `?11111011`, so [FE /3] and [FF /3] match
;  - JMP Ew
;    - `?11111100`, so [FE /4] and [FF /4] match
;  - JMP Mpww
;    - `?11111101`, so [FE /5] and [FF /5] match
;  - PUSH Ew
;    - `?1111111?`, so [FE /6], [FE /7], [FF /6], and [FF /7] match
;
; All memory-only opcodes (such as LEA, LDS, LES, etc.) do not match on the "reg" field, and will implicitly use
;   whatever old values are in the internal register pair `IND:TMP`


(one-b-prefixes
    (26 ES)
    (2E CS)
    (36 SS)
    (3E DS)
    (F0 LOCK)
    (F1 LOCK)
    (F2 REPNE)
    (F3 REPE)
)


(instructions
    ; ALU instructions of [00-bbb-xxx] (the [00-3F] block)
    ;   where `bbb` is the operation and `xxx` (0 through 5) is the form
    (ADD  (Eb Gb)  (00 /r)  (lockable))
    (ADD  (Ew Gw)  (01 /r)  (lockable))
    (ADD  (Gb Eb)  (02 /r)  ())
    (ADD  (Gw Ew)  (03 /r)  ())
    (ADD  (AL Ib)  (04 ib)  ())
    (ADD  (AX Iw)  (05 iw)  ())
    (OR   (Eb Gb)  (08 /r)  (lockable))
    (OR   (Ew Gw)  (09 /r)  (lockable))
    (OR   (Gb Eb)  (0A /r)  ())
    (OR   (Gw Ew)  (0B /r)  ())
    (OR   (AL Ib)  (0C ib)  ())
    (OR   (AX Iw)  (0D iw)  ())
    (ADC  (Eb Gb)  (10 /r)  (lockable))
    (ADC  (Ew Gw)  (11 /r)  (lockable))
    (ADC  (Gb Eb)  (12 /r)  ())
    (ADC  (Gw Ew)  (13 /r)  ())
    (ADC  (AL Ib)  (14 ib)  ())
    (ADC  (AX Iw)  (15 iw)  ())
    (SBB  (Eb Gb)  (18 /r)  (lockable))
    (SBB  (Ew Gw)  (19 /r)  (lockable))
    (SBB  (Gb Eb)  (1A /r)  ())
    (SBB  (Gw Ew)  (1B /r)  ())
    (SBB  (AL Ib)  (1C ib)  ())
    (SBB  (AX Iw)  (1D iw)  ())
    (AND  (Eb Gb)  (20 /r)  (lockable))
    (AND  (Ew Gw)  (21 /r)  (lockable))
    (AND  (Gb Eb)  (22 /r)  ())
    (AND  (Gw Ew)  (23 /r)  ())
    (AND  (AL Ib)  (24 ib)  ())
    (AND  (AX Iw)  (25 iw)  ())
    (SUB  (Eb Gb)  (28 /r)  (lockable))
    (SUB  (Ew Gw)  (29 /r)  (lockable))
    (SUB  (Gb Eb)  (2A /r)  ())
    (SUB  (Gw Ew)  (2B /r)  ())
    (SUB  (AL Ib)  (2C ib)  ())
    (SUB  (AX Iw)  (2D iw)  ())
    (XOR  (Eb Gb)  (30 /r)  (lockable))
    (XOR  (Ew Gw)  (31 /r)  (lockable))
    (XOR  (Gb Eb)  (32 /r)  ())
    (XOR  (Gw Ew)  (33 /r)  ())
    (XOR  (AL Ib)  (34 ib)  ())
    (XOR  (AX Iw)  (35 iw)  ())
    (CMP  (Eb Gb)  (38 /r)  ())
    (CMP  (Ew Gw)  (39 /r)  ())
    (CMP  (Gb Eb)  (3A /r)  ())
    (CMP  (Gw Ew)  (3B /r)  ())
    (CMP  (AL Ib)  (3C ib)  ())
    (CMP  (AX Iw)  (3D iw)  ())

    ; ALU instructions of [10-000-0ws /r] ("group 1"; the [80-83] block)
    ;   where `/r` is the operation
    ; NOTE: CMP is not lockable as it does not write back to the destination (only reads)
    (ADD  (Eb Ib)  (80 /0)  (lockable))
    (OR   (Eb Ib)  (80 /1)  (lockable))
    (ADC  (Eb Ib)  (80 /2)  (lockable))
    (SBB  (Eb Ib)  (80 /3)  (lockable))
    (AND  (Eb Ib)  (80 /4)  (lockable))
    (SUB  (Eb Ib)  (80 /5)  (lockable))
    (XOR  (Eb Ib)  (80 /6)  (lockable))
    (CMP  (Eb Ib)  (80 /7)  ())
    (ADD  (Ew Iw)  (81 /0)  (lockable))
    (OR   (Ew Iw)  (81 /1)  (lockable))
    (ADC  (Ew Iw)  (81 /2)  (lockable))
    (SBB  (Ew Iw)  (81 /3)  (lockable))
    (AND  (Ew Iw)  (81 /4)  (lockable))
    (SUB  (Ew Iw)  (81 /5)  (lockable))
    (XOR  (Ew Iw)  (81 /6)  (lockable))
    (CMP  (Ew Iw)  (81 /7)  ())
    (ADD  (Eb Ib)  (82 /0)  (lockable))  ; undocumented
    (OR   (Eb Ib)  (82 /1)  (lockable))  ; undocumented
    (ADC  (Eb Ib)  (82 /2)  (lockable))  ; undocumented
    (SBB  (Eb Ib)  (82 /3)  (lockable))  ; undocumented
    (AND  (Eb Ib)  (82 /4)  (lockable))  ; undocumented
    (SUB  (Eb Ib)  (82 /5)  (lockable))  ; undocumented
    (XOR  (Eb Ib)  (82 /6)  (lockable))  ; undocumented
    (CMP  (Eb Ib)  (82 /7)  ())          ; undocumented
    (ADD  (Ew Ib)  (83 /0)  (lockable))
    (OR   (Ew Ib)  (83 /1)  (lockable))
    (ADC  (Ew Ib)  (83 /2)  (lockable))
    (SBB  (Ew Ib)  (83 /3)  (lockable))
    (AND  (Ew Ib)  (83 /4)  (lockable))
    (SUB  (Ew Ib)  (83 /5)  (lockable))
    (XOR  (Ew Ib)  (83 /6)  (lockable))
    (CMP  (Ew Ib)  (83 /7)  ())

    ; PUSH/POP segment instructions of [00-0ss-11d] (ES/CS/SS/DS)
    ;   where `ss` is the segment
    (PUSH  (ES)  (06)  ())
    (POP   (ES)  (07)  ())
    (PUSH  (CS)  (0E)  ())
    (POP   (CS)  (0F)  ())  ; undocumented
    (PUSH  (SS)  (16)  ())
    (POP   (SS)  (17)  ())
    (PUSH  (DS)  (1E)  ())
    (POP   (DS)  (1F)  ())

    ; PUSH/POP GPR and FLAGS instructions
    (PUSH   (Zw)  (50+r)   ())
    (POP    (Zw)  (58+r)   ())
    (POP    (Ew)  (8F /r)  ())  ; /1 through /7 are undocumented
    (PUSHF  ()    (9C)     ())
    (POPF   ()    (9D)     ())
    (PUSH   (Eb)  (FE /6)  ())  ; undocumented
    (PUSH   (Eb)  (FE /7)  ())  ; undocumented
    (PUSH   (Ew)  (FF /6)  ())
    (PUSH   (Ew)  (FF /7)  ())  ; undocumented

    ; BCD instructions
    (DAA  ()    (27)     ())
    (DAS  ()    (2F)     ())
    (AAA  ()    (37)     ())
    (AAS  ()    (3F)     ())
    (AAM  (Ib)  (D4 ib)  ())
    (AAD  (Ib)  (D5 ib)  ())

    ; String instructions
    (MOVSB  ()  (A4)  ())
    (MOVSW  ()  (A5)  ())
    (CMPSB  ()  (A6)  ())
    (CMPSW  ()  (A7)  ())
    (STOSB  ()  (AA)  ())
    (STOSW  ()  (AB)  ())
    (LODSB  ()  (AC)  ())
    (LODSW  ()  (AD)  ())
    (SCASB  ()  (AE)  ())
    (SCASW  ()  (AF)  ())

    ; Rotate/shift instructions ("group 2"; the [D0-D3] block)
    ; NOTE: on later processors, [Dx /6] is an alias for SHL (should be SAL, but arithmetic and local shifts are the
    ;   same); however, the 8086 seems to treat it as a "set minus one":
    ;   <https://www.os2museum.com/wp/undocumented-8086-opcodes-part-i/>
    ; Sandpile.org even lists it as `OR Ev, -1`:
    ;   <https://sandpile.org/x86/opc_grp.htm>
    (ROL   (Eb 1)   (D0 /0)  ())
    (ROR   (Eb 1)   (D0 /1)  ())
    (RCL   (Eb 1)   (D0 /2)  ())
    (RCR   (Eb 1)   (D0 /3)  ())
    (SHL   (Eb 1)   (D0 /4)  ())
    (SHR   (Eb 1)   (D0 /5)  ())
    (SETMO (Eb 1)   (D0 /6)  ())  ; undocumented
    (SAR   (Eb 1)   (D0 /7)  ())
    (ROL   (Ew 1)   (D1 /0)  ())
    (ROR   (Ew 1)   (D1 /1)  ())
    (RCL   (Ew 1)   (D1 /2)  ())
    (RCR   (Ew 1)   (D1 /3)  ())
    (SHL   (Ew 1)   (D1 /4)  ())
    (SHR   (Ew 1)   (D1 /5)  ())
    (SETMO (Ew 1)   (D1 /6)  ())  ; undocumented
    (SAR   (Ew 1)   (D1 /7)  ())
    (ROL   (Eb CL)  (D2 /0)  ())
    (ROR   (Eb CL)  (D2 /1)  ())
    (RCL   (Eb CL)  (D2 /2)  ())
    (RCR   (Eb CL)  (D2 /3)  ())
    (SHL   (Eb CL)  (D2 /4)  ())
    (SHR   (Eb CL)  (D2 /5)  ())
    (SETMO (Eb CL)  (D2 /6)  ())  ; undocumented
    (SAR   (Eb CL)  (D2 /7)  ())
    (ROL   (Ew CL)  (D3 /0)  ())
    (ROR   (Ew CL)  (D3 /1)  ())
    (RCL   (Ew CL)  (D3 /2)  ())
    (RCR   (Ew CL)  (D3 /3)  ())
    (SHL   (Ew CL)  (D3 /4)  ())
    (SHR   (Ew CL)  (D3 /5)  ())
    (SETMO (Ew CL)  (D3 /6)  ())  ; undocumented
    (SAR   (Ew CL)  (D3 /7)  ())

    ; I/O instructions
    (IN   (AL Ib) (E4 ib)  ())
    (IN   (AX Ib) (E5 ib)  ())
    (OUT  (Ib AL) (E6 ib)  ())
    (OUT  (Ib AX) (E7 ib)  ())
    (IN   (AL DX) (EC)  ())
    (IN   (AX DX) (ED)  ())
    (OUT  (DX AL) (EE)  ())
    (OUT  (DX AX) (EF)  ())

    ; Control flow instructions
    (Jcc     (Jb)    (60+cc ib)  ())  ; undocumented
    (Jcc     (Jb)    (70+cc ib)  ())
    (CALL    (Apww)  (9A ipw)    ())
    (RET     (Iw)    (C0 iw)     ())  ; undocumented
    (RET     ()      (C1)        ())  ; undocumented
    (RET     (Iw)    (C2 iw)     ())
    (RET     ()      (C3)        ())
    (RETF    (Iw)    (C8 iw)     ())  ; undocumented
    (RETF    ()      (C9)        ())  ; undocumented
    (RETF    (Iw)    (CA iw)     ())
    (RETF    ()      (CB)        ())
    (INT     (3)     (CC)        ())
    (INT     (Ib)    (CD ib)     ())
    (INTO    ()      (CE)        ())
    (IRET    ()      (CF)        ())
    (LOOPNE  (Jb)    (E0 ib)     ())
    (LOOPE   (Jb)    (E1 ib)     ())
    (LOOP    (Jb)    (E2 ib)     ())
    (JCXZ    (Jb)    (E3 ib)     ())
    (CALL    (Jw)    (E8 iw)     ())
    (JMP     (Jw)    (E9 iw)     ())
    (JMP     (Apww)  (EA ipw)    ())
    (JMP     (Jb)    (EB ib)     ())
    (HLT     ()      (F4)        ())
    (CALL    (Eb)    (FE /2)     ())  ; undocumented
    (CALL    (Mpww)  (FE /3)     ())  ; undocumented
    (JMP     (Eb)    (FE /4)     ())  ; undocumented
    (JMP     (Mpww)  (FE /5)     ())  ; undocumented
    (CALL    (Ew)    (FF /2)     ())
    (CALL    (Mpww)  (FF /3)     ())  ; register form is undocumented
    (JMP     (Ew)    (FF /4)     ())
    (JMP     (Mpww)  (FF /5)     ())  ; register form is undocumented


    ; MOV and related instructions
    (XCHG  (Eb Gb)  (86 /r)     (lockable))
    (XCHG  (Ew Gw)  (87 /r)     (lockable))
    (MOV   (Eb Gb)  (88 /r)     ())
    (MOV   (Ew Gw)  (89 /r)     ())
    (MOV   (Gb Eb)  (8A /r)     ())
    (MOV   (Gw Ew)  (8B /r)     ())
    (MOV   (Ew Sw)  (8C /r)     ())
    (LEA   (Gw M)   (8D /r)     ())
    (MOV   (Sw Ew)  (8E /r)     ())
    (NOP   ()       (90)        ())
    (XCHG  (AX Zw)  (90+r)      ())
    (MOV   (AL Ob)  (A0 ib)     ())
    (MOV   (AX Ow)  (A1 iw)     ())
    (MOV   (Ob AL)  (A2 ib)     ())
    (MOV   (Ow AX)  (A3 iw)     ())
    (MOV   (Zb Ib)  (B0+r)      ())
    (MOV   (Zw Iw)  (B8+r)      ())
    (LES   (Gw Mw)  (C4 /r)     ())  ; register form is undocumented
    (LDS   (Gw Mw)  (C5 /r)     ())  ; register form is undocumented
    (MOV   (Eb Ib)  (C6 /r ib)  ())  ; /1 through /7 are undocumented
    (MOV   (Ew Iw)  (C7 /r iw)  ())  ; /1 through /7 are undocumented
    (XLAT  ()       (D7)  ())

    ; Miscellaneous arithmetic instructions
    (INC   (Zw)     (40+r)      ())
    (DEC   (Zw)     (48+r)      ())
    (TEST  (Eb Gb)  (84 /r)     ())
    (TEST  (Ew Gw)  (85 /r)     ())
    (CBW   ()       (98)        ())
    (CWD   ()       (99)        ())
    (SAHF  ()       (9E)        ())
    (LAHF  ()       (9F)        ())
    (TEST  (AL Ib)  (A8 ib)     ())
    (TEST  (AX Iw)  (A9 iw)     ())
    (SALC  ()       (D6)        ())
    (CMC   ()       (F5)        ())
    (TEST  (Eb Ib)  (F6 /0 ib)  ())
    (TEST  (Eb Ib)  (F6 /1 ib)  ())  ; undocumented
    (NOT   (Eb)     (F6 /2)     ())
    (NEG   (Eb)     (F6 /3)     ())
    (MUL   (Eb)     (F6 /4)     ())
    (IMUL  (Eb)     (F6 /5)     ())
    (DIV   (Eb)     (F6 /6)     ())
    (IDIV  (Eb)     (F6 /7)     ())
    (TEST  (Ew Iw)  (F7 /0 iw)  ())
    (TEST  (Ew Iw)  (F7 /1 iw)  ())  ; undocumented
    (NOT   (Ew)     (F7 /2)     ())
    (NEG   (Ew)     (F7 /3)     ())
    (MUL   (Ew)     (F7 /4)     ())
    (IMUL  (Ew)     (F7 /5)     ())
    (DIV   (Ew)     (F7 /6)     ())
    (IDIV  (Ew)     (F7 /7)     ())
    (CLC   ()       (F8)        ())
    (STC   ()       (F9)        ())
    (CLI   ()       (FA)        ())
    (STI   ()       (FB)        ())
    (CLD   ()       (FC)        ())
    (STD   ()       (FD)        ())
    (INC   (Eb)     (FE /0)     ())  ; undocumented
    (DEC   (Eb)     (FE /1)     ())  ; undocumented
    (INC   (Ew)     (FF /0)     ())
    (DEC   (Ew)     (FF /1)     ())


    ; FPU instructions
    ; TODO: [D8-DF] ESC block (in FpuBaseSet/8087.m86)
    (WAIT   ()      (9B)  ())
)
