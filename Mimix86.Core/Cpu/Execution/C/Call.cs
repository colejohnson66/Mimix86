﻿/* =============================================================================
 * File:   Call.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * <TODO>
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

using Mimix86.Core.Cpu.Decoder;
using System;

namespace Mimix86.Core.Cpu.Execution;

/// <summary>
/// Handler functions for the <c>CALL</c> opcode.
/// </summary>
public static class Call
{
    public static void Apww(CpuCore cpu, Instruction instr) =>
        throw new NotImplementedException();

    public static void Ew(CpuCore cpu, Instruction instr) =>
        throw new NotImplementedException();

    public static void Jw(CpuCore cpu, Instruction instr) =>
        throw new NotImplementedException();

    public static void Mpww(CpuCore cpu, Instruction instr) =>
        throw new NotImplementedException();
}
