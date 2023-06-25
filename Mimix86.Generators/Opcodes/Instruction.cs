/* =============================================================================
 * File:   Instruction.cs
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

using Mimix86.Generators.Opcodes.Encoding;
using SExpressionReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mimix86.Generators.Opcodes;

public sealed class Instruction :
    IComparable<Instruction>,
    IEquatable<Instruction>
{
    private readonly string _mnemonic;
    private readonly string[] _operands;
    private readonly bool _lockable;
    private readonly bool _endTrace;

    private readonly string? _decodeFlags;

    public OpcodeEncoding Encoding { get; }

    public string TitleCaseMnemonic { get; }
    public string OperandsString { get; }


    public Instruction(Expression node)
    {
        if (node.Count is not 4)
            throw new InvalidDataException("Instruction entry is not the right length.");

        if (!node[0].TryAsAtom(out Atom mnemonic))
            throw new InvalidDataException("Instruction mnemonic must be an atom.");
        if (!node[1].TryAsExpression(out Expression? operands))
            throw new InvalidDataException("Instruction operands must be an expression.");
        if (!node[2].TryAsExpression(out Expression? encoding))
            throw new InvalidDataException("Instruction encoding must be an expression.");
        if (!node[3].TryAsExpression(out Expression? flags))
            throw new InvalidDataException("Instruction flags must be an expression.");

        _mnemonic = mnemonic.As<string>();
        _operands = operands.Select(aoe => aoe.AsAtom().As<string>()).ToArray();
        Encoding = new(encoding.Select(aoe => aoe.AsAtom().As<string>()).ToArray());

        foreach (AtomOrExpression aoe in flags)
        {
            if (!aoe.TryAsAtom(out Atom atom) || !atom.TryAs(out string? str))
                throw new InvalidDataException("Instruction flags must be strings.");
            if (str is "lockable")
            {
                if (_lockable)
                    throw new InvalidDataException("Lockable flag must only be specified once.");
                _lockable = true;
            }
            else if (str is "end-trace")
            {
                if (_endTrace)
                    throw new InvalidDataException("End trace flag must only be specified once.");
                _endTrace = true;
            }
            else
            {
                throw new InvalidDataException($"Unknown instruction flag: \"{str}\".");
            }
        }

        TitleCaseMnemonic = _mnemonic[0] + _mnemonic[1..].ToLowerInvariant();
        OperandsString = _operands.Join("");

        _decodeFlags = null;
        if (Encoding.ModRM?.HasAnyRequiredFields is true)
        {
            List<string> decodeFlags = new();
            EncodingPart.ModRM modRM = Encoding.ModRM;
            if (modRM.Mod is not null)
                decodeFlags.Add(modRM.Mod.Value is ModRMMod.Memory ? "ModMem" : "ModReg");
            if (modRM.Reg is not null)
                decodeFlags.Add($"Reg{modRM.Reg.Value}");
            if (modRM.RM is not null)
                decodeFlags.Add($"RM{modRM.RM.Value}");
            _decodeFlags = decodeFlags.Join(" | ");
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
        // always prefix namespace; opcodes with no operands have a name clash with the opcode definition
        builder.Append($"Execution.{TitleCaseMnemonic}.");
        if (_operands.Any())
        {
            // identifiers in C# can't begin with digits; prefix with an underscore if needed
            // this is really only for `INT 3` (`Int._3`)
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
        List<string> flags = new();
        if (_lockable)
            flags.Add("OpcodeFlags.Lockable");
        if (_endTrace)
            flags.Add("OpcodeFlags.EndTrace");
        builder.Append(flags.Any()
            ? flags.Join(" | ")
            : "0");
        builder.Append(", ");

        // immediate
        builder.Append(Encoding.Immediate is not null
            ? $"ImmediateSizes.{Encoding.Immediate}"
            : "null");

        builder.Append(");");
        return builder.ToString();
    }


    public int CompareTo(Instruction? other)
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
        obj is Instruction other && Equals(other);

    public bool Equals(Instruction? other)
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
