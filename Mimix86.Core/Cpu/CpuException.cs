/* =============================================================================
 * File:   CpuException.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Represents a CPU exception.
 * For example, one can be used to indicate a divide-by-zero error by passing
 *   `CpuExceptionCode.DE` to the constructor.
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

using DotNext;
using System;

namespace Mimix86.Core.Cpu;

/// <summary>
/// Represents a CPU exception.
/// These should only ever be caught by the <see cref="CpuCore" /> object.
/// </summary>
public class CpuException
{
    /// <summary>
    /// Construct a new <see cref="CpuException" /> with a specified exception code.
    /// </summary>
    /// <param name="code">The CPU exception code.</param>
    /// <exception cref="ArgumentException">
    /// If <paramref name="code" /> requires a fault code, and isn't <see cref="CpuExceptionCode.DF" /> or
    ///   <see cref="CpuExceptionCode.AC" />.
    /// </exception>
    /// <remarks>
    /// If the provided code is <see cref="CpuExceptionCode.DF" /> or <see cref="CpuExceptionCode.AC" />, the required
    ///   fault code of 0 is implicitly created.
    /// </remarks>
    public CpuException(CpuExceptionCode code)
    {
        if (code is
            CpuExceptionCode.TS or CpuExceptionCode.NP or CpuExceptionCode.SS or
            CpuExceptionCode.GP or CpuExceptionCode.PF or CpuExceptionCode.CP)
            throw new ArgumentException($"Exception code, {code.ToString()}, requires a fault code.");

        Code = code;
        FaultCode = code is CpuExceptionCode.DF or CpuExceptionCode.AC ? new(0) : Optional<ushort>.None;
    }

    /// <summary>
    /// Construct a new <see cref="CpuException" /> with a specified exception code and inner exception.
    /// </summary>
    /// <param name="code">The CPU exception code.</param>
    /// <param name="faultCode">The fault code provided for user-code to debug the exception.</param>
    /// <exception cref="ArgumentException">
    /// If <paramref name="code" /> is an exception code that does not have a fault code.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="code" /> is <see cref="CpuExceptionCode.DF" /> and <paramref name="faultCode" /> is non-zero.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="code" /> is <see cref="CpuExceptionCode.AC" /> and <paramref name="faultCode" /> is non-zero.
    /// </exception>
    public CpuException(CpuExceptionCode code, ushort faultCode)
    {
        if (code is not (
            CpuExceptionCode.DF or CpuExceptionCode.TS or CpuExceptionCode.NP or
            CpuExceptionCode.SS or CpuExceptionCode.GP or CpuExceptionCode.PF or
            CpuExceptionCode.CP or CpuExceptionCode.AC))
            throw new ArgumentException($"Exception code, {code.ToString()}, does not take a fault code.");
        if (code is CpuExceptionCode.DF && faultCode is not 0)
            throw new ArgumentOutOfRangeException(nameof(faultCode), faultCode, "Double fault exceptions must have a fault code of 0.");
        if (code is CpuExceptionCode.AC && faultCode is not 0)
            throw new ArgumentOutOfRangeException(nameof(faultCode), faultCode, "Alignment check exceptions must have a fault code of 0.");

        Code = code;
        FaultCode = new(faultCode);
    }


    /// <summary>The <see cref="CpuExceptionCode" /> this exception is for.</summary>
    public CpuExceptionCode Code { get; }

    /// <summary>The integer vector of this exception.</summary>
    public int Vector => (int)Code;

    /// <summary>
    /// The fault code provided for user-code to debug the exception, or <see cref="Optional{T}.None" /> if there isn't
    ///   one.
    /// </summary>
    public Optional<ushort> FaultCode { get; }
}
