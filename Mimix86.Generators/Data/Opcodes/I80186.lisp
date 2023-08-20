; ==============================================================================
; File:   I80186.lisp
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
    (name  I80186)
    (summary
        (
            "The instructions added with the 80186."
        )
    )
)


(instructions
    (PUSHA  ()          (60)        ())
    (POPA   ()          (61)        ())
    (BOUND  (Gw Ma)     (62 m/r)    ())
    (PUSH   (Iw)        (68 iw)     ())
    (IMUL   (Gw Ew Iw)  (69 /r iw)  ())
    (PUSH   (Ib)        (6A ib)     ())
    (IMUL   (Gw Ew Ib)  (6B /r ib)  ())
    (INSB   ()          (6C)        ())
    (INSW   ()          (6D)        ())
    (OUTSB  ()          (6E)        ())
    (OUTSW  ()          (6F)        ())
    (ROL    (Eb Ib)     (C0 /0 ib)  ())
    (ROR    (Eb Ib)     (C0 /1 ib)  ())
    (RCL    (Eb Ib)     (C0 /2 ib)  ())
    (RCR    (Eb Ib)     (C0 /3 ib)  ())
    (SHL    (Eb Ib)     (C0 /4 ib)  ())
    (SHR    (Eb Ib)     (C0 /5 ib)  ())
    (SHL    (Eb Ib)     (C0 /6 ib)  ())
    (SAR    (Eb Ib)     (C0 /7 ib)  ())
    (ROL    (Ew Ib)     (C1 /0 ib)  ())
    (ROR    (Ew Ib)     (C1 /1 ib)  ())
    (RCL    (Ew Ib)     (C1 /2 ib)  ())
    (RCR    (Ew Ib)     (C1 /3 ib)  ())
    (SHL    (Ew Ib)     (C1 /4 ib)  ())
    (SHR    (Ew Ib)     (C1 /5 ib)  ())
    (SHL    (Ew Ib)     (C1 /6 ib)  ())
    (SAR    (Ew Ib)     (C1 /7 ib)  ())
    (ENTER  (Iw Ib)     (C8 iw ib)  ())
    (LEAVE  ()          (C9)        ())
)
