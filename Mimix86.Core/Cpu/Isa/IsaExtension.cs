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
 *   Mimix86. If not, see <https://www.gnu.org/licenses/>.
 * =============================================================================
 */

using Mimix86.Core.Cpu.Decoder;
using Mimix86.Core.Cpu.Decoder.OpcodeMap;
using System.Collections.Generic;

namespace Mimix86.Core.Cpu.Isa;

/// <summary>
/// Represents an ISA extension.
/// This contains the prefixes and opcodes that will be registered.
/// </summary>
public sealed class IsaExtension
{
    /// <summary>
    /// Get the opcode map cell entries that this ISA extension will register.
    /// </summary>
    public Dictionary<OpmapCellIndex, (OpmapCellFlags, List<OpmapCellEntry>)> OpcodeMapEntries { get; } = new();
}
