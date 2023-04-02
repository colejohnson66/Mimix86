/* =============================================================================
 * File:   CpuExceptionCode.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Contains the various CPU exceptions.
 * =============================================================================
 * Copyright (c) 2022-2023 Cole Tobin
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

namespace Mimix86.Core.Cpu;

/// <summary>
/// Represents the five bit CPU exception index for all 32 reserved exception/interrupt vectors.
/// </summary>
[PublicAPI]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum CpuExceptionCode : byte
{
    /// <summary>#0: Divide Error</summary>
    DE = 0,

    /// <summary>#1: Debug Exception</summary>
    DB = 1,

    /// <summary>#2: Non-maskable Interrupt</summary>
    NMI = 2,

    /// <summary>#3: Breakpoint (<c>INT 3</c>)</summary>
    BP = 3,

    /// <summary>#4: Overflow (<c>INTO</c>)</summary>
    OF = 4,

    /// <summary>#5: BOUND Range Exceeded</summary>
    BR = 5,

    /// <summary>#6: Invalid/Undefined Opcode</summary>
    UD = 6,

    /// <summary>#7: Device Unavailable / No Math Coprocessor</summary>
    NM = 7,

    /// <summary>#8: Double Fault; Fault code is always 0</summary>
    DF = 8,

    // #9: Coprocessor Segment Overrun (80286 and 80386 only)
    // CSO = 9,

    /// <summary>#10: Invalid TSS; Fault code is a selector index</summary>
    TS = 10,

    /// <summary>#11: Segment Not Present; Fault code is the segment selector index where the exception originated</summary>
    NP = 11,

    /// <summary>#12: Stack Segment Fault</summary>
    /// <remarks>
    /// When accessing a selector with the "present" bit cleared, the fault code is the selector index in <c>SS</c>.
    /// Otherwise, it is 0.
    /// </remarks>
    SS = 12,

    /// <summary>#13: General Protection Fault; Fault code is either the segment selector index where the exception originated, or 0.</summary>
    GP = 13,

    /// <summary>#14: Page Fault; Fault code is a 32 bit value pushed onto the stack</summary>
    PF = 14,

    // #15 is reserved

    /// <summary>#16: FPU Floating Point Error / Math Fault</summary>
    MF = 16,

    /// <summary>#17: Alignment Check Fault; Fault code is always 0</summary>
    AC = 17,

    /// <summary>#18: Machine Check</summary>
    MC = 18,

    /// <summary>#19: SIMD Floating Point Exception</summary>
    /// <remarks>
    /// AMD abbreviates this as <c>#XF</c>.
    /// </remarks>
    XM = 19,

    /// <summary>#20: Virtualization Exception (VT)</summary>
    VE = 20,

    /// <summary>#21: Control Protection Exception (CET); Fault code indicates TODO</summary>
    CP = 21,

    // Exceptions 22 through 27 are reserved (mostly stolen by BIOSes)

    /// <summary>#28: Hypervisor Injection Exception (SVM)</summary>
    HV = 28,

    /// <summary>#29: VMM Communication Exception (SVM)</summary>
    VC = 29,

    /// <summary>#30: Security Exception (SVM)</summary>
    SX = 30,

    // Exception 31 is reserved
}
