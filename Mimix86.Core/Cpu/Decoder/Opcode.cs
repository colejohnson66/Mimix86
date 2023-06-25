/* =============================================================================
 * File:   Opcode.cs
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

using System;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents a single opcode.
/// </summary>
public sealed partial class Opcode :
    IEquatable<Opcode>
{
    private Opcode(string mnemonic, ExecutionHandler handler, OpcodeFlags flags, ImmediateSizes? immediateSize)
    {
        Mnemonic = mnemonic;
        Handler = handler;
        Flags = flags;
        Immediate = immediateSize;
    }


    /// <summary>
    /// The mnemonic of this opcode.
    /// </summary>
    public string Mnemonic { get; }

    /// <summary>
    /// The handler of this opcode.
    /// </summary>
    public ExecutionHandler Handler { get; }

    /// <summary>
    /// Any flags used to direct post-decoding.
    /// </summary>
    public OpcodeFlags Flags { get; }

    /// <summary>
    /// The size of the immediate for this opcode.
    /// </summary>
    public ImmediateSizes? Immediate { get; }


    /// <inheritdoc />
    public bool Equals(Opcode? other)
    {
        if (ReferenceEquals(this, other))
            return true;
        if (ReferenceEquals(other, null))
            return false;

        // the handlers are unique to their opcodes
        return Mnemonic == other.Mnemonic && Handler == other.Handler;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj is Opcode other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() =>
        HashCode.Combine(Mnemonic, Handler); // the handlers are unique
}
