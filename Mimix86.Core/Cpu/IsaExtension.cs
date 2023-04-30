/* =============================================================================
 * File:   IsaExtension.cs
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

namespace Mimix86.Core.Cpu;

// CS1591 is XML documentation
#pragma warning disable CS1591

/// <summary>
/// Contains the various ISA extensions.
/// These are used to determine if certain opcodes can be executed.
/// </summary>
public readonly partial struct IsaExtension
{
    // index into the bitfield for fast checking of support (compared to an Array.Contains call)
    private readonly int _bitIndex;

    private IsaExtension(int bitIndex)
    {
        _bitIndex = bitIndex;
    }
}
