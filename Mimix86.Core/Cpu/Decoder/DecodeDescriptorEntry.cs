/* =============================================================================
 * File:   DecodeDescriptorEntry.cs
 * Author: Cole Tobin
 * =============================================================================
 * Copyright (c) 2023 Cole Tobin
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

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents an entry for the <see cref="DecodeDescriptor" /> tables.
/// </summary>
public sealed class DecodeDescriptorEntry
{
    /// <summary>
    /// An array of <see cref="OpcodeMapEntry" /> objects that will be passed along to <see cref="Handler" />.
    /// </summary>
    public OpcodeMapEntry[]? OpcodeMapEntries { get; }

    /// <summary>
    /// The handler for this entry.
    /// </summary>
    public DecodeHandler Handler { get; }


    /// <summary>
    /// Construct a new <see cref="DecodeDescriptorEntry" /> with specified <see cref="OpcodeMapEntry" /> objects and a
    ///   decode handler.
    /// </summary>
    /// <param name="entries">The <see cref="OpcodeMapEntry" /> objects to pass along to the handler.</param>
    /// <param name="handler">The handler for this entry.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="handler" /> is <c>null</c>.</exception>
    public DecodeDescriptorEntry(OpcodeMapEntry[]? entries, DecodeHandler handler)
    {
        ArgumentNullException.ThrowIfNull(handler);

        OpcodeMapEntries = entries;
        Handler = handler;
    }
}
