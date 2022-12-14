/* =============================================================================
 * File:   Opcode.cs
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

using System;
using System.Linq;

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Represents a single opcode.
/// </summary>
public partial class Opcode : IEquatable<Opcode>
{
    /// <summary>
    /// Construct a new <see cref="Opcode" /> with a specified mnemonic and execution handler.
    /// </summary>
    /// <param name="mnemonic">The mnemonic for this opcode.</param>
    /// <param name="handler">The execution handler for this opcode.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="mnemonic" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentNullException">If <paramref name="handler" /> is <c>null</c>.</exception>
    public Opcode(string mnemonic, ExecutionHandler handler)
    {
        ArgumentNullException.ThrowIfNull(mnemonic);
        ArgumentNullException.ThrowIfNull(handler);

        Mnemonic = mnemonic;
        Operands = Array.Empty<string>();
        Handler = handler;
    }

    /// <summary>
    /// Construct a new <see cref="Opcode" /> with a specified mnemonic, operands, and execution handler.
    /// </summary>
    /// <param name="mnemonic">The mnemonic for this opcode.</param>
    /// <param name="operands">
    /// A space-separated list of the operands for this opcode, or <c>null</c> if there aren't any.
    /// </param>
    /// <param name="handler">The execution handler for this opcode.</param>
    /// <exception cref="ArgumentException">If <paramref name="operands" /> consists of only whitespace.</exception>
    /// <exception cref="ArgumentNullException">If <paramref name="mnemonic" /> is <c>null</c>.</exception>
    /// <exception cref="ArgumentNullException">If <paramref name="handler" /> is <c>null</c>.</exception>
    public Opcode(string mnemonic, string[] operands, ExecutionHandler handler)
    {
        ArgumentNullException.ThrowIfNull(mnemonic);
        ArgumentNullException.ThrowIfNull(handler);

        Mnemonic = mnemonic;
        Operands = operands;
        Handler = handler;
    }

    /// <summary>
    /// Get the mnemonic for this opcode.
    /// </summary>
    public string Mnemonic { get; }

    /// <summary>
    /// Get the operands for this opcode.
    /// </summary>
    public string[] Operands { get; }

    /// <summary>
    /// Get the handler used to execute this opcode.
    /// </summary>
    public ExecutionHandler Handler { get; }

    /// <summary>
    /// Get the flags to direct usage of this opcode post-decoding.
    /// </summary>
    public OpcodeFlags Flags { get; init; }

    /// <summary>
    /// Get the size of the immediate for this opcode entry.
    /// </summary>
    public ImmSize Immediate { get; init; } = ImmSize.None;


    /// <inheritdoc />
    public bool Equals(Opcode? other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Mnemonic == other.Mnemonic &&
            Operands == other.Operands;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        obj is Opcode other && Equals(other);

    /// <inheritdoc />
    public override int GetHashCode() =>
        HashCode.Combine(Mnemonic, Operands);

    /// <inheritdoc />
    public override string ToString() =>
        Operands.Any() ? $"{Mnemonic} {Operands}" : Mnemonic;
}
