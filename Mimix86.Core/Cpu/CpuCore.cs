﻿/* =============================================================================
 * File:   CpuCore.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * <TODO>
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

namespace Mimix86.Core.Cpu;

/// <summary>
/// Represents the whole state of a single CPU core.
/// </summary>
[PublicAPI]
public class CpuCore
{
    /// <summary>
    /// Get the default address size for memory accesses based on the current operating mode.
    /// </summary>
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    public int DefaultAddressSize => 16;

    /// <summary>
    /// Get the default operand size for data accesses based on the current operating mode.
    /// </summary>
    [SuppressMessage("Performance", "CA1822:Mark members as static")]
    public int DefaultOperandSize => 16;
}
