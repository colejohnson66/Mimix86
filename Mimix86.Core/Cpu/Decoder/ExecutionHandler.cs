/* =============================================================================
 * File:   ExecutionHandler.cs
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

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// The type of an instruction execution handler.
/// </summary>
/// <param name="core">
/// The CPU core currently executing this instruction.
/// All register accesses, memory accesses, exceptions, etc. will be through this object.
/// </param>
/// <param name="instruction">The <see cref="DecodedInstruction" /> object.</param>
public delegate void ExecutionHandler(CpuCore core, DecodedInstruction instruction);
