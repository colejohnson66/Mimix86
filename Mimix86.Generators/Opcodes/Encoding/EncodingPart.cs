/* =============================================================================
 * File:   EncodingPart.cs
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
using System.Text;

namespace Mimix86.Generators.Opcodes.Encoding;

// simulating a sum type enum

public record EncodingPart
{
    /// <summary>
    /// Represents a raw byte value.
    /// </summary>
    /// <param name="Value">The value.</param>
    public record Byte(byte Value)
        : EncodingPart;

    /// <summary>
    /// Represents a raw byte value with a register occupying the three least-significant bits.
    /// </summary>
    /// <param name="BaseValue">The value of the opcode with the three least-significant bits cleared.</param>
    public record BytePlusRegister(byte BaseValue)
        : EncodingPart;

    /// <summary>
    /// Represents a raw byte value with a condition code ("cc") occupying the four least-significant bits.
    /// </summary>
    /// <param name="BaseValue">The value of the opcode with the four least-significant bits cleared.</param>
    public record BytePlusCondition(byte BaseValue)
        : EncodingPart;

    /// <summary>
    /// Represents a ModR/M byte with optional required fields.
    /// </summary>
    /// <param name="Mod">
    /// The required value for the "mod" field, or <c>null</c> if register and memory forms are allowed.
    /// </param>
    /// <param name="Reg">The required value for the "reg" field, or <c>null</c> if any value is allowed.</param>
    /// <param name="RM">The required value for the "r/m" field, or <c>null</c> if any value is allowed.</param>
    public record ModRM(ModRMMod? Mod, int? Reg, int? RM)
        : EncodingPart
    {
        public bool HasAnyRequiredFields =>
            Mod is not null || Reg is not null || RM is not null;

        public string BuildDecodeFlagsString()
        {
            if (!HasAnyRequiredFields)
                throw new InvalidOperationException();
            StringBuilder builder = new();

            if (Mod is not null)
                builder.Append(Mod is ModRMMod.Memory ? "ModMem" : "ModReg");

            if (Reg is not null)
            {
                if (builder.Length is not 0)
                    builder.Append(" | ");
                builder.Append($"Reg{Reg}");
            }

            if (RM is not null)
            {
                if (builder.Length is not 0)
                    builder.Append(" | ");
                builder.Append($"RM{RM}");
            }

            return builder.ToString();
        }
    }

    /// <summary>
    /// Represents an immediate with a specified size.
    /// </summary>
    /// <param name="Size">The size of the immediate.</param>
    public record Immediate(ImmediateSizes Size)
        : EncodingPart;
}
