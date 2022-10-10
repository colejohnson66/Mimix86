/* =============================================================================
 * File:   DecodeDescriptorEntry.cs
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

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents a mapping between an array of <see cref="OpcodeMapEntry" /> objects and their associated
///   <see cref="DecodeHandler" />.
/// </summary>
/// <param name="OpcodeMapEntries">
/// The array of <see cref="OpcodeMapEntry" /> objects.
/// These will be passed in the <c>opmapEntries</c> argument of <see cref="Handler" />.
/// </param>
/// <param name="Handler">The handler for <see cref="OpcodeMapEntries" />.</param>
public record DecodeDescriptorEntry(
    OpcodeMapEntry[]? OpcodeMapEntries,
    DecodeHandler Handler);
