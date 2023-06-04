/* =============================================================================
 * File:   Opcode.cs
 * Author: Cole Tobin
 * =============================================================================
 * Copyright (c) 2022-2023 Cole Tobin
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

using DslLib;
using Mimix86.Generators.Opcodes.Encoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mimix86.Generators.Opcodes;

public class Opcode :
    IComparable<Opcode>,
    IEquatable<Opcode>
{
    private readonly string _mnemonic;
    private readonly string[] _operands;
    public OpcodeEncoding Encoding { get; }
    public string RequiredCpuLevelString { get; }
    private readonly bool _lockable;

    public string TitleCaseMnemonic { get; }
    public string OperandsString { get; }

    private readonly string? _opmapFlags;

    public Opcode(Node node)
    {
        Node[] children = node.Children!;

        _mnemonic = children[0].Text!;
        _operands = children[1].Children!.Select(operand => operand.Text!).ToArray();
        Encoding = new(children[2].Children!.Select(operand => operand.Text!).ToArray());
        RequiredCpuLevelString = children[3].Text!;
        _lockable = children.Length > 4 && children[4].Children![0].Text! is "lock";

        TitleCaseMnemonic = _mnemonic[0] + _mnemonic[1..].ToLowerInvariant();
        OperandsString = _operands.Join("");

        _opmapFlags = null;
        if (Encoding.ModRM?.HasAnyRequiredFields is true)
        {
            List<string> flags = new();
            EncodingPart.ModRM modRM = Encoding.ModRM;
            if (modRM.Mod is not null)
                flags.Add(modRM.Mod.Value is ModRMMod.Memory ? "MOD_MEM" : "MOD_REG");
            if (modRM.Reg is not null)
                flags.Add($"REG_{modRM.Reg.Value}");
            if (modRM.RM is not null)
                flags.Add($"RM_{modRM.RM.Value}");
            _opmapFlags = flags.Join(" | ");
        }
    }

    public string GenerateOpcodeMember()
    {
        string operands = _operands.Join("");

        StringBuilder builder = new();

        // doc comment
        builder.Append($"    /// <summary>The <c>{_mnemonic}");
        if (_operands.Any())
            builder.Append($" {_operands.Join(", ")}");
        builder.AppendLine("</c> opcode.</summary>");

        // start of entry
        builder.Append(
            $"    public static Opcode {TitleCaseMnemonic}{OperandsString} {{ get; }} = new(");

        // mnemonic
        builder.Append(
            $"\"{_mnemonic.ToLowerInvariant()}\", ");

        // execution function
        builder.Append(
            $"Execution.{TitleCaseMnemonic}."); // always prefix namespace; opcodes with no operands name clash with _opcodeName
        if (_operands.Any())
        {
            // identifiers in C# can't begin with digits; prefix with an underscore if needed
            if (char.IsAsciiDigit(operands[0]))
                builder.Append('_');
            builder.Append(operands);
        }
        else
        {
            builder.Append('_');
        }
        builder.Append(", ");

        // flags
        builder.Append(_lockable ? "OpcodeFlags.Lockable, " : "0, ");

        // immediate
        builder.Append(Encoding.Immediate is not null
            ? $"ImmediateSizes.{Encoding.Immediate}"
            : "null");

        builder.Append(");");
        return builder.ToString();
    }



    public int CompareTo(Opcode? other)
    {
        if (ReferenceEquals(this, other))
            return 0;
        if (ReferenceEquals(other, null))
            return 1;

        int mnemonic = string.Compare(_mnemonic, other._mnemonic, StringComparison.Ordinal);
        if (mnemonic != 0)
            return mnemonic;

        return string.Compare(OperandsString, other.OperandsString, StringComparison.Ordinal);
    }

    public override bool Equals(object? obj) =>
        obj is Opcode other && Equals(other);

    public bool Equals(Opcode? other)
    {
        if (ReferenceEquals(this, other))
            return true;
        if (ReferenceEquals(other, null))
            return false;

        return _mnemonic == other._mnemonic && _operands.SequenceEqual(other._operands);
    }

    public override int GetHashCode() =>
        // Only consider the resulting opcode's unique name; one opcode may have multiple encodings
        // `HashCode.Combine` doesn't work with arrays (`Array.GetHashCode` uses `Object.GetHashCode`)
        _operands.Aggregate(
            _mnemonic.GetHashCode(),
            (current, operand) => current * 31 + operand.GetHashCode());
}
