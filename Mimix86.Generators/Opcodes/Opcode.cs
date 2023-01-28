/* =============================================================================
 * File:   Opcode.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * <TODO>
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
using Mimix86.Generators.Opcodes.OpEncoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mimix86.Generators.Opcodes;

public class Opcode : IEquatable<Opcode>
{
    private readonly string _mnemonic;
    private readonly string[] _operands;
    private readonly OpcodeEncoding _encoding;
    private readonly string _reqCpu;
    private readonly bool _lockable;

    private readonly string _titleCaseMnemonic;
    private readonly string _operandsStr;

    private readonly string? _opmapFlags;

    public Opcode(Node node)
    {
        Node[] children = node.Children!;

        _mnemonic = children[0].Text!;
        _operands = children[1].Children!.Select(operand => operand.Text!).ToArray();
        _encoding = new(children[2].Children!.Select(operand => operand.Text!).ToArray());
        _reqCpu = children[3].Text!;
        _lockable = children.Length > 4 && children[4].Children![0].Text! is "lock";

        _titleCaseMnemonic = _mnemonic[0] + _mnemonic[1..].ToLowerInvariant();
        _operandsStr = _operands.Join("");

        _opmapFlags = null;
        if (_encoding.ModRM?.HasRequiredFields is true)
        {
            List<string> flags = new();
            ModRM modRM = _encoding.ModRM;
            if (modRM.Mod.HasValue)
                flags.Add(modRM.Mod.Value is ModRMMod.Memory ? "MOD_MEM" : "MOD_REG");
            if (modRM.Reg.HasValue)
                flags.Add($"REG_{modRM.Reg.Value}");
            if (modRM.RM.HasValue)
                flags.Add($"RM_{modRM.RM.Value}");
            _opmapFlags = flags.Join(" | ");
        }
    }

    public string GenerateOpcodeMember(string indent)
    {
        string operands = _operands.Join("");

        StringBuilder builder = new();

        // doc comment
        builder.Append($"{indent}/// <summary>The <c>{_mnemonic}");
        if (_operands.Any())
            builder.Append($" {_operands.Join(", ")}");
        builder.AppendLine("</c> opcode.</summary>");

        // start of entry
        builder.Append(
            $"{indent}public static Opcode {_titleCaseMnemonic}{_operandsStr} {{ get; }} = new(");

        // mnemonic
        builder.Append('"');
        builder.Append(_mnemonic.ToLowerInvariant());
        builder.Append("\", ");

        // operands
        if (_operands.Any())
        {
            builder.Append("new[] { ");
            for (int i = 0; i < _operands.Length; i++)
            {
                builder.Append('"');
                builder.Append(_operands[i]);
                builder.Append('"');
                if (i != _operands.Length - 1)
                    builder.Append(", ");
            }
            builder.Append(" }, ");
        }
        else
        {
            builder.Append("Array.Empty<string>(), ");
        }

        // execution function
        builder.Append("Execution."); // always prefix; opcodes with no operands name clash with _opcodeName
        builder.Append(_titleCaseMnemonic);
        builder.Append('.');
        if (_operands.Any())
        {
            if (char.IsAsciiDigit(operands[0]))
                builder.Append('_');
            builder.Append(operands);
        }
        else
        {
            builder.Append('_');
        }
        builder.Append(')');

        // extras
        List<string> extra = new();
        if (_lockable)
            extra.Add("Flags = OpcodeFlags.Lockable");
        if (_encoding.Immediate is not null)
            extra.Add($"Immediate = {_encoding.Immediate}");
        if (extra.Any())
        {
            builder.Append(" { ");
            builder.Append(extra.Join(", "));
            builder.Append(" }");
        }

        builder.Append(';');
        return builder.ToString();
    }


    public string GenerateOpcodeMapMember(string indent)
    {
        string str = $"{indent}new({_titleCaseMnemonic}{_operandsStr}, {_reqCpu}";
        if (_opmapFlags is null)
            return str + "),";

        return $"{str}, {_opmapFlags}),";
    }


    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;

        return obj is Opcode other && Equals(other);
    }

    public bool Equals(Opcode? other)
    {
        if (ReferenceEquals(null, other))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return _mnemonic == other._mnemonic &&
            _operands.SequenceEqual(other._operands);
    }

    public override int GetHashCode()
    {
        // Only consider the resulting opcode's unique name; one opcode may have multiple encodings
        // `HashCode.Combine` doesn't work with arrays (`Array.GetHashCode` uses `Object.GetHashCode`)
        return _operands.Aggregate(
            _mnemonic.GetHashCode(),
            (current, operand) => current * 31 + operand.GetHashCode());
    }
}
