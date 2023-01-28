/* =============================================================================
 * File:   CpuMode.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Contains the various CPU operating modes.
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

using System;

namespace Mimix86.Core.Cpu;

/// <summary>
/// Indicates the current operating mode of a CPU thread.
/// </summary>
public enum CpuMode
{
    /// <summary>
    /// This CPU thread is in "real mode".
    /// </summary>
    /// <remarks><c>CR0.PE</c> (bit 0) is cleared.</remarks>
    Real,

    /// <summary>
    /// This CPU thread is in "virtual-8086 mode".
    /// </summary>
    /// <remarks><c>CR0.PE</c> (bit 0) is set, but <c>EFLAGS.VM</c> (bit 17) is set.</remarks>
    Virtual8086,

    /// <summary>
    /// This CPU thread is in "protected mode".
    /// </summary>
    /// <remarks><c>CR0.PE</c> (bit 0) is set, and <c>EFLAGS.VM</c> (bit 17) is cleared.</remarks>
    Protected,

    /// <summary>
    /// This CPU thread is in "compatibility mode".
    /// </summary>
    /// <remarks>
    /// <c>CR4.PAE</c> (bit 5) and <c>EFER.LME</c> (MSR <c>0xC000_0080</c>; bit 8) are set
    /// </remarks>
    Compatibility,

    /// <summary>
    /// This CPU thread is in "long mode".
    /// </summary>
    /// <remarks>
    /// <c>CR4.PAE</c> (bit 5), <c>IA32_EFER.LME</c> (MSR <c>0xC000_0080</c>; bit 8), and <c>CS.L</c> are set.
    /// </remarks>
    Long,
}

/// <summary>
/// Extension methods for <see cref="CpuMode" />.
/// </summary>
public static class CpuModeExtensions
{
    /// <summary>
    /// Get the width, in bits, TODO
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static int BitWidth(this CpuMode mode) =>
        mode switch
        {
            CpuMode.Real or CpuMode.Virtual8086 => 16,
            CpuMode.Protected or CpuMode.Compatibility => 32,
            CpuMode.Long => 64,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, $"Unknown value of {nameof(CpuMode)}."),
        };
}
