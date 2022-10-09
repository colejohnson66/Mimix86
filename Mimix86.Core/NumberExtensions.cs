/* =============================================================================
 * File:   NumberExtensions.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Contains extension methods for any (binary) numerical integer to get or set
 *   any bit.
 * Set methods return the new value.
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

namespace Mimix86.Core;

/// <summary>
/// Extension methods for numerical values.
/// </summary>
internal static class NumberExtensions
{
    #region Gets

    /// <summary>
    /// Get a single bit from a number at a specified index.
    /// </summary>
    /// <param name="value">The value to get a bit from.</param>
    /// <param name="index">The index of the bit to get.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>The value of the bit at index, <paramref name="index" />.</returns>
    /// <remarks>
    /// If the <paramref name="index" /> is greater than or equal to the number of bits in <typeparamref name="T" />,
    ///   the index will wrap around (i.e. the index modulus the bit width of <typeparamref name="T" /> is used).
    /// For example, if <typeparamref name="T" /> is <see cref="ushort" />, and this parameter is <c>17</c>, bit
    ///   <c>1</c> will be returned (<c>17&#xA0;%&#xA0;16&#xA0;==&#xA0;1</c>).
    /// </remarks>
    public static bool GetBit<T>(this T value, int index)
        where T : IBinaryInteger<T>
    {
        T mask = T.One << index;
        return (value & mask) != T.Zero;
    }

    /// <summary>
    /// Get multiple bits from a number that are located at a specified range.
    /// </summary>
    /// <param name="value">The value to get bits from.</param>
    /// <param name="range">The range of the bits to get.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>The value of the bits in the range, <paramref name="range" />.</returns>
    /// <exception cref="ArgumentException">
    /// If either the start of end <see cref="Index" />es in <paramref name="range" /> are from the end.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// If the ending <see cref="Index" /> in <paramref name="range" /> is less than the starting <see cref="Index" />.
    /// </exception>
    /// <remarks>
    /// If the range, <paramref name="range" />, contains indexes that are greater than or equal to the number of bits
    ///   in <typeparamref name="T" />, the behavior is undefined.<!-- TODO: does it just wrap around also? -->
    /// </remarks>
    /// <remarks>
    /// If <typeparamref name="T" /> is a signed number, sign extension will occur on the returned bits.
    /// </remarks>
    public static T GetBits<T>(this T value, Range range)
        where T : IBinaryInteger<T>
    {
        if (range.Start.IsFromEnd || range.End.IsFromEnd)
            throw new ArgumentException("Range must consist of only bit indexes from the start.", nameof(range));
        if (range.End.Value < range.Start.Value)
            throw new ArgumentException("Cannot extract a negative number of bits.", nameof(range));

        int width = range.End.Value - range.Start.Value;
        T mask = (T.One << width) - T.One; // mask is right (bit zero) aligned

        return (value >> range.Start.Value) & mask;
    }

    #endregion

    #region Sets

    /// <summary>
    /// Set a single bit in a number at a specified index to a specified value.
    /// The new value is returned.
    /// </summary>
    /// <param name="value">The value to change.</param>
    /// <param name="index">The index of the bit to set.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>The original value, but with the bit at index, <paramref name="index" />, set.</returns>
    /// <remarks>
    /// If the <paramref name="index" /> is greater than or equal to the number of bits in <typeparamref name="T" />,
    ///   the index will wrap around (i.e. the index modulus the bit width of <typeparamref name="T" /> is used).
    /// For example, if <typeparamref name="T" /> is <see cref="ushort" />, and this parameter is <c>17</c>, bit
    ///   <c>1</c> will be set (<c>17&#xA0;%&#xA0;16&#xA0;==&#xA0;1</c>).
    /// </remarks>
    public static T SetBit<T>(this T value, int index)
        where T : IBinaryInteger<T>
    {
        T mask = T.One << index;
        return value | mask;
    }

    /// <summary>
    /// Clear a single bit in a number at a specified index to a specified value.
    /// The new value is returned.
    /// </summary>
    /// <param name="value">The value to change.</param>
    /// <param name="index">The index of the bit to clear.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>The original value, but with the bit at index, <paramref name="index" />, cleared.</returns>
    /// <remarks>
    /// If the <paramref name="index" /> is greater than or equal to the number of bits in <typeparamref name="T" />,
    ///   the index will wrap around (i.e. the index modulus the bit width of <typeparamref name="T" /> is used).
    /// For example, if <typeparamref name="T" /> is <see cref="ushort" />, and this parameter is <c>17</c>, bit
    ///   <c>1</c> will be cleared (<c>17&#xA0;%&#xA0;16&#xA0;==&#xA0;1</c>).
    /// </remarks>
    public static T ClearBit<T>(this T value, int index)
        where T : IBinaryInteger<T>
    {
        T mask = T.One << index;
        return value & ~mask;
    }

    /// <summary>
    /// Set/change a single bit in a number at a specified index to a specified value.
    /// The new value is returned.
    /// </summary>
    /// <param name="value">The value to change.</param>
    /// <param name="index">The index of the bit to set.</param>
    /// <param name="bit">The new value of the bit being set/changed.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>
    /// The original value, but with the bit at index, <paramref name="index" />, changed to <paramref name="bit" />.
    /// </returns>
    /// <remarks>
    /// If the <paramref name="index" /> is greater than or equal to the number of bits in <typeparamref name="T" />,
    ///   the index will wrap around (i.e. the index modulus the bit width of <typeparamref name="T" /> is used).
    /// For example, if <typeparamref name="T" /> is <see cref="ushort" />, and this parameter is <c>17</c>, bit
    ///   <c>1</c> will be changed (<c>17&#xA0;%&#xA0;16&#xA0;==&#xA0;1</c>).
    /// </remarks>
    public static T SetBit<T>(this T value, int index, bool bit)
        where T : IBinaryInteger<T> =>
        bit ? value.SetBit(index) : value.ClearBit(index);

    #endregion
}
