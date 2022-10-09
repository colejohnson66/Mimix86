/* =============================================================================
 * File:   RegisterBase.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Contains the base functionality of all registers: access to the raw value,
 *   and the ability to get or set any bit.
 * Also contains specializations for 8- and 16-bit registers.
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

using System;
using System.Runtime.InteropServices;

namespace Mimix86.Core.Cpu.Register;

/// <summary>
/// Represents an arbitrary register's base features: access to the <see cref="RawValue" />, and the ability to get and
///   set individual bits.
/// </summary>
/// <typeparam name="T">The underlying type of the register.</typeparam>
[PublicAPI]
public abstract class RegisterBase<T>
    where T : IBinaryInteger<T>
{
    /// <summary>
    /// The number of bits in this register.
    /// </summary>
    protected static readonly int SizeOf = Marshal.SizeOf<T>() * 8;

    /// <summary>
    /// The "raw" value of this register.
    /// </summary>
    /// <remarks>Setting reserved bits this way can cause undefined behavior.</remarks>
    protected T RawValue { get; set; } = T.Zero;

    /// <summary>
    /// Get or set/change a single bit at a specified index.
    /// </summary>
    /// <param name="index">The index of the bit to get or set/change.</param>
    /// <remarks>Setting reserved bits this way can cause undefined behavior.</remarks>
    public bool this[int index]
    {
        get => GetBit(index);
        set => SetBit(index, value);
    }

    /// <summary>
    /// Get multiple bits from a specified range.
    /// </summary>
    /// <param name="range">The range of the bits to get.</param>
    /// <exception cref="ArgumentException">
    /// If either the start of end <see cref="Index" />es in <paramref name="range" /> are from the end.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If the ending <see cref="Index" /> in <paramref name="range" /> is less than the starting <see cref="Index" />.
    /// </exception>
    public T this[Range range] =>
        GetBits(range);

    /// <summary>
    /// Get a single bit at a specified index.
    /// </summary>
    /// <param name="index">The index of the bit to get.</param>
    /// <returns>The value of the bit at index, <paramref name="index" />.</returns>
    protected bool GetBit(int index) =>
        RawValue.GetBit(index);

    /// <summary>
    /// Set/change a single bit at a specified index.
    /// </summary>
    /// <param name="index">The index of the bit to set/change.</param>
    /// <param name="value">The new value of the bit being set/changed.</param>
    /// <remarks>Setting reserved bits this way can cause undefined behavior.</remarks>
    protected void SetBit(int index, bool value) =>
        RawValue = RawValue.SetBit(index, value);

    /// <summary>
    /// Get multiple bits from a specified range.
    /// </summary>
    /// <param name="range">The range of the bits to get.</param>
    /// <exception cref="ArgumentException">
    /// If either the start of end <see cref="Index" />es in <paramref name="range" /> are from the end.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If the ending <see cref="Index" /> in <paramref name="range" /> is less than the starting <see cref="Index" />.
    /// </exception>
    protected T GetBits(Range range) =>
        RawValue.GetBits(range);
}


/// <summary>
/// Represents an arbitrary 8-bit register's base features.
/// </summary>
[PublicAPI]
[SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
public class Register8 : RegisterBase<byte>
{ }


/// <summary>
/// Represents an arbitrary 16-bit register's base features.
/// </summary>
[PublicAPI]
[SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
public class Register16 : RegisterBase<ushort>
{ }
