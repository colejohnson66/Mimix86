/* =============================================================================
 * File:   FlagsRegister.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Represents the EFLAGS register.
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

namespace Mimix86.Core.Cpu.Register;

/// <summary>
/// Represents the <c>EFLAGS</c> register.
/// </summary>
[PublicAPI]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class FlagsRegister : Register16
{
    /* ┌─────┬─────┬─────┬─────┬─────┬─────┬─────┬─────┐
     * │ >31 │  30 │  29 │  28 │  27 │  26 │  25 │  24 │
     * │                  Reserved (0)                 │
     * ├─────┬─────┬─────┬─────┬─────┬─────┬─────┬─────┤
     * │  23 │  22 │  21 │  20 │  19 │  18 │  17 │  16 │
     * │  Reserved │  ID │ VIP │ VIF │  AC │  VM │  RF │
     * ├─────┬─────┼─────┼─────┼─────┼─────┼─────┼─────┤
     * │  15 │  14 │  13 │  12 │  11 │  10 │   9 │   8 │
     * │ (0) │  NT │    IOPL   │  OF │  DF │  IF │  TF │
     * ├─────┼─────┼─────┬─────┼─────┼─────┼─────┼─────┤
     * │   7 │   6 │   5 │   4 │   3 │   2 │   1 │   0 │
     * │  SF │  ZF │ (0) │  AF │ (0) │  PF │ (1) │  CF │
     * └─────┴─────┴─────┴─────┴─────┴─────┴─────┴─────┘
     *
     * Differences between CPU "levels" (in real mode) when FLAGS (16-bit form) is pushed to the stack[a]:
     *   - 8086:   12..=15 set
     *   - 80286:  12..=15 cleared
     *   - 80386+: 12..=14 untouched; 15 cleared
     *
     * TODO: 8086 programmers manuals I've looked at list bits 1, 3, and 5 as "undefined"; what is the actual behavior
     * TODO:  when pushed to the stack?
     *
     * Of historical note: on the 8085, bit 1 is VF and bit 5 is KF.[b][c]
     * VF would indicate signed overflow or underflow for addition and subtraction.
     * KF would indicate a signed comparison where LHS is less than RHS.
     *
     * [a]: Intel SDM, Volume 3, section 22.17.2 "EFLAGS Pushed on the Stack"
     * [b]: https://www.righto.com/2013/02/looking-at-silicon-to-understanding.html
     * [c]: https://www.righto.com/2013/07/reverse-engineering-flag-circuits-in.html
     */

    /// <summary>
    /// Construct a new <see cref="FlagsRegister" /> object.
    /// </summary>
    public FlagsRegister()
    {
        RawValue = 2;
    }

    /// <summary>
    /// Get or set the full 16-bit register.
    /// </summary>
    public ushort Value
    {
        get => RawValue;
        set => RawValue = (ushort)(value | 2); // TODO: handle CPU level and operating mode
    }

    /// <summary>
    /// Get or set the carry flag.
    /// </summary>
    public bool CF
    {
        get => GetBit(0);
        set => SetBit(0, value);
    }

    /// <summary>
    /// Get or set the parity flag.
    /// </summary>
    public bool PF
    {
        get => GetBit(2);
        set => SetBit(2, value);
    }

    /// <summary>
    /// Get or set the auxiliary carry flag.
    /// </summary>
    public bool AF
    {
        get => GetBit(4);
        set => SetBit(4, value);
    }

    /// <summary>
    /// Get or set the zero flag.
    /// </summary>
    public bool ZF
    {
        get => GetBit(6);
        set => SetBit(6, value);
    }

    /// <summary>
    /// Get or set the sign flag.
    /// </summary>
    public bool SF
    {
        get => GetBit(7);
        set => SetBit(7, value);
    }

    /// <summary>
    /// Get or set the trap flag.
    /// </summary>
    public bool TF
    {
        get => GetBit(8);
        set => SetBit(8, value);
    }

    /// <summary>
    /// Get or set the "interrupt enable" flag.
    /// </summary>
    public bool IF
    {
        get => GetBit(9);
        set => SetBit(9, value);
    }

    /// <summary>
    /// Get or set the direction flag.
    /// </summary>
    public bool DF
    {
        get => GetBit(10);
        set => SetBit(10, value);
    }

    /// <summary>
    /// Get or set the overflow flag.
    /// </summary>
    public bool OF
    {
        get => GetBit(11);
        set => SetBit(11, value);
    }

    // /// <summary>
    // /// Get or set the I/O privilege level flag.
    // /// </summary>
    // /// <exception cref="ArgumentException">If <paramref name="value" /> is greater than three.</exception>
    // public byte IOPL
    // {
    //     get => (byte)GetBits(12..14);
    //     set
    //     {
    //         if (value > 0b11)
    //             throw new ArgumentException("I/O Privilege Level must be between zero and three, inclusive.", nameof(value));
    //
    //         // clear out bits 12 and 13
    //         const ushort MASK = 0b11 << 12;
    //         ushort temp = (ushort)(RawValue & ~MASK);
    //         RawValue = (ushort)(temp | (value << 12));
    //     }
    // }

    // extra bits to add when 32 bit support is added:
    //   - RF:  bit 16; resume flag
    //   - VM:  bit 17; virtual-8086 mode
    //   - AC:  bit 18; alignment check enable
    //   - VIF: bit 19; virtual interrupt flag
    //   - VIP: bit 20; virtual interrupt pending
    //   - ID:  bit 21; CPUID toggle / identification flag
}
