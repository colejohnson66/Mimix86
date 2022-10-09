/* =============================================================================
 * File:   Gpr.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Represents a general purpose register.
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

namespace Mimix86.Core.Cpu.Register;

/// <summary>
/// Represents a general purpose register.
/// </summary>
[PublicAPI]
[SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
public class Gpr : Register16
{
    /* ┌─────┬─────┬─────┬─────┬─────┬─────┐
     * │  63 │ ... │  56 │  55 │  .. │  48 │
     * │         64-bit ('Rrx') ...        │
     * ├─────┬─────┬─────┬─────┬─────┬─────┤
     * │  47 │ ... │  40 │  39 │  .. │  32 │
     * │         ... 64-bit ('Rrx')        │
     * ├─────┬─────┬─────┬─────┬─────┬─────┤
     * │  31 │ ... │  24 │  23 │  .. │  16 │
     * │           32-bit  ('Erx')         │
     * ├─────┬─────┬─────┬─────┬─────┬─────┤
     * │  15 │ ... │   8 │   7 │  .. │   0 │
     * │            16-bit (rX)            │
     * │ High 8-bit (rH) │  Low 8-bit (rL) │
     * └─────────────────┴─────────────────┘
     *
     * NOTE: To reduce register dependencies (and help the register renamer), when operating on a 64-bit processor,
     *   writing to the extended ('Erx') portion of a GPR clears the upper 32-bits.
     * Basically, all 32-bit register writes are zero extended into 64-bit writes.
     */

    /// <summary>
    /// Construct a new <see cref="Gpr" /> with a value of zero.
    /// </summary>
    public Gpr()
    {
        RawValue = 0;
    }

    /// <summary>
    /// Get or set the 16-bit (word) portion of this register.
    /// </summary>
    public ushort Word
    {
        get => RawValue;
        set => RawValue = value;
    }

    /// <summary>
    /// Get or set the upper 8-bit portion of the 16-bit (word) portion of this register.
    /// </summary>
    public byte ByteHigh
    {
        get => (byte)(RawValue >> 8);
        set => RawValue = (ushort)((RawValue & 0xFF) | (value << 8));
    }

    /// <summary>
    /// Get or set the lower 8-bit portion of the 16-bit (word) portion of this register.
    /// </summary>
    public byte ByteLow
    {
        get => (byte)(RawValue & 0xFF);
        set => RawValue = (ushort)((RawValue & 0xFF00) | value);
    }
}
