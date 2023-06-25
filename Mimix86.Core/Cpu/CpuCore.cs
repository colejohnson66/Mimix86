/* =============================================================================
 * File:   CpuCore.cs
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

using System;

namespace Mimix86.Core.Cpu;

/// <summary>
/// Represents the whole state of a single CPU core.
/// </summary>
[PublicAPI]
public class CpuCore
{
    /// <summary>
    /// Get the current operating mode.
    /// </summary>
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    public CpuMode Mode => CpuMode.Real;

    /// <summary>
    /// Get the default address size for memory accesses based on the current operating mode.
    /// </summary>
    public int DefaultAddressSize => Mode.BitWidth();

    /// <summary>
    /// Get the default operand size for data accesses based on the current operating mode.
    /// </summary>
    public int DefaultOperandSize => Mode.BitWidth();


    /// <summary>
    /// Raise a CPU trap exception after the current instruction finishes executing.
    /// </summary>
    /// <param name="exception">The trap exception being raised.</param>
    public void TrapAfterCompletion(CpuException exception) =>
        throw new NotImplementedException();
}
