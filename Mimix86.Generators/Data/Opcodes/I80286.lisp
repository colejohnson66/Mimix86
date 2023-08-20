; ==============================================================================
; File:   I80286.lisp
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
    (name  I80286)
    (summary
        (
            "The instructions added with the 80286."
            "SAVEALL and LOADALL are available with <see cref=\"I80286Undefined\" />."
        )
    )
)


(one-b-prefixes
    (0F two-byte)
)


(instructions
    (ARPL  (Ew Gw)  (63 /r)     ())
    (UD2   ()       (0F 0B)     ())
    (UD1   ()       (0F B9 /r)  ())
    ; TODO: does this have a ModR/M on 286? Intel manual says some processors don't use a ModR/M
    (UD0   ()       (0F FF /r)  ())
)
