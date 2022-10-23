/* =============================================================================
 * File:   UnreachableException.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * An exception that indicates a block of code is supposed to be unreachable.
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
using System.Runtime.Serialization;

namespace Mimix86.Core;

/// <summary>
/// Represents an error that occurs when a block of code that should have been unreachable was, in fact, reached.
/// </summary>
[Serializable]
[SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
public sealed class UnreachableException : Exception
{
    /// <summary>
    /// Construct a new <see cref="UnreachableException" /> with a generic exception message.
    /// </summary>
    public UnreachableException()
        : this("Unreachable code was reached.")
    { }

    /// <summary>
    /// Construct a new <see cref="UnreachableException" /> with a specified exception message.
    /// </summary>
    /// <param name="message">The message that explains the reason for the exception.</param>
    public UnreachableException(string? message)
        : base(message)
    { }

    /// <summary>
    /// Construct a new <see cref="UnreachableException" /> with a specified exception message and inner exception.
    /// </summary>
    /// <param name="message">The message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public UnreachableException(string? message, Exception? innerException)
        : base(message, innerException)
    { }

    /// <summary>
    /// Construct a new <see cref="UnreachableException" /> from serialized data.
    /// </summary>
    /// <param name="info">
    /// The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.
    /// </param>
    /// <param name="context">
    /// The <see cref="StreamingContext" /> that holds contextual information about the source.
    /// </param>
    private UnreachableException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    { }
}
