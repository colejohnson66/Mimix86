/* =============================================================================
 * File:   Xchg.cs
 * Author: Cole Tobin
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
 *   Mimix86. If not, see <https://www.gnu.org/licenses/>.
 * =============================================================================
 */

using Mimix86.Core.Cpu.Decoder;
using System;

namespace Mimix86.Core.Cpu.Execution;

/// <summary>
/// Handler functions for the <c>XCHG</c> opcode.
/// </summary>
public static class Xchg
{
    public static void AXZw(CpuCore cpu, DecodedInstruction instruction) =>
        throw new NotImplementedException();

    public static void EbGb(CpuCore cpu, DecodedInstruction instruction) =>
        throw new NotImplementedException();

    public static void EwGw(CpuCore cpu, DecodedInstruction instruction) =>
        throw new NotImplementedException();
}
