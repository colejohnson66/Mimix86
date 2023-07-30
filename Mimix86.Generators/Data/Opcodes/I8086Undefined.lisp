; ==============================================================================
; File:   I8086Undefined.lisp
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
;   Mimix86. If not, see <https://www.gnu.org/licenses/>.
; ==============================================================================

(file
    (name  I8086Undefined)
    (summary
        (
            "The undefined instructions available in the 8086."
        )
    )
)

; Undefined opcodes on 8086
; The 8086 does not have "undefined" opcodes; all byte sequences will do *something*.
; Ken Shiriff has used the 8086 microcode to determine what these undefined sequences will do:
;   <https://www.righto.com/2023/07/undocumented-8086-instructions.html>


(one-b-prefixes
    (F1 lock)
)


(instructions
    (POP    (CS)     (0F)        ())
    (Jcc    (Jb)     (60+cc ib)  (end-trace))
    (POP    (Ew)     (8F /1)     ())  ; [8F /r] is believed to ignore /r on 8086, but this is untested
    (POP    (Ew)     (8F /2)     ())  ; <https://www.righto.com/2023/07/undocumented-8086-instructions.html?showComment=1690236769108#c3208796967961476172>
    (POP    (Ew)     (8F /3)     ())
    (POP    (Ew)     (8F /4)     ())
    (POP    (Ew)     (8F /5)     ())
    (POP    (Ew)     (8F /6)     ())
    (POP    (Ew)     (8F /7)     ())
    (RET    (Iw)     (C0 iw)     (end-trace))
    (RET    ()       (C1)        (end-trace))
    (LES    (Gw Mw)  (C4 r/r)    ())
    (LDS    (Gw Mw)  (C5 r/r)    ())
    (MOV    (Eb Ib)  (C6 /1 ib)  ())  ; [C6 /r] ignores /r
    (MOV    (Eb Ib)  (C6 /2 ib)  ())  ; <https://www.righto.com/2023/07/undocumented-8086-instructions.html?showComment=1690236769108#c3208796967961476172>
    (MOV    (Eb Ib)  (C6 /3 ib)  ())
    (MOV    (Eb Ib)  (C6 /4 ib)  ())
    (MOV    (Eb Ib)  (C6 /5 ib)  ())
    (MOV    (Eb Ib)  (C6 /6 ib)  ())
    (MOV    (Eb Ib)  (C6 /7 ib)  ())
    (MOV    (Ew Iw)  (C7 /1 iw)  ())  ; [C7 /r] ignores /r
    (MOV    (Ew Iw)  (C7 /2 iw)  ())  ; <https://www.righto.com/2023/07/undocumented-8086-instructions.html?showComment=1690236769108#c3208796967961476172>
    (MOV    (Ew Iw)  (C7 /3 iw)  ())
    (MOV    (Ew Iw)  (C7 /4 iw)  ())
    (MOV    (Ew Iw)  (C7 /5 iw)  ())
    (MOV    (Ew Iw)  (C7 /6 iw)  ())
    (MOV    (Ew Iw)  (C7 /7 iw)  ())
    (RETF   (Iw)     (C8 iw)     ())
    (RETF   ()       (C9)        ())
    (SETMO  (Eb 1)   (D0 /6)     ())
    (SETMO  (Ew 1)   (D1 /6)     ())
    (SETMO  (Eb CL)  (D2 /6)     ())
    (SETMO  (Ew CL)  (D3 /6)     ())
    ; TODO: FE /2../7 and FF /7 and register forms
)
