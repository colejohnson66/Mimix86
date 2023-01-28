/* =============================================================================
 * File:   OpcodeEncoding.cs
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

using DotNext;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Mimix86.Generators.Opcodes.OpEncoding;

public class OpcodeEncoding
{
    private readonly string[] _operands;
    private readonly Lazy<byte> _opcode;
    private readonly Lazy<ModRM?> _modRM;
    private readonly Lazy<string?> _imm;

    public OpcodeEncoding(string[] operands)
    {
        _operands = operands;
        _opcode = new(() => Convert.ToByte(_operands[0], 16));
        _modRM = new(ComputeModRM);
        _imm = new(ComputeImmediate);
    }

    public byte Opcode => _opcode.Value;
    public ModRM? ModRM => _modRM.Value;
    public string? Immediate => _imm.Value;

    private ModRM? ComputeModRM()
    {
        if (_operands.Length is 1)
            return null; // nothing to do for single byte opcodes

        // the ModR/M byte is always the one after the opcode byte
        string possibleModRM = _operands[1];
        if (!possibleModRM.Contains('/'))
            return null; // not a ModR/M byte

        string[] parts = possibleModRM.Split('/');
        Debug.Assert(parts.Length >= 2);

        Optional<ModRMMod> mod = parts[0] switch
        {
            "m" => new(ModRMMod.Memory),
            "r" => new(ModRMMod.Register),
            "" => Optional<ModRMMod>.None,
            _ => throw new InvalidDataException($"Unknown ModR/M mod part: {parts[0]}."),
        };

        // the reg field is the second part
        // because `Contains('/')` was `true`, this won't be out of range
        Optional<byte> reg = parts[1] switch
        {
            "r" => Optional<byte>.None,
            "0" => new(0),
            "1" => new(1),
            "2" => new(2),
            "3" => new(3),
            "4" => new(4),
            "5" => new(5),
            "6" => new(6),
            "7" => new(7),
            _ => throw new InvalidDataException($"Unknown ModR/M reg part: {parts[1]}."),
        };

        Optional<byte> rm = parts.Length is 3
            ? new(byte.Parse(parts[2]))
            : Optional<byte>.None;

        return new(mod, reg, rm);
    }

    private string? ComputeImmediate()
    {
        if (_operands.Length is 1)
            return null; // nothing to do for single byte opcodes

        // the immediate is always at the end
        string possibleImm = _operands.Last();
        if (possibleImm.FirstOrDefault() is not 'i')
            return null; // not an immediate

        return possibleImm switch
        {
            // NOTE: these must be kept in sync with `ImmSize` in `Mimix86.Core`
            "ib" => "ImmSize.Byte",
            "iw" => "ImmSize.Word",
            "ipw" => "ImmSize.PointerWordWord",
            _ => throw new InvalidDataException($"Unknown immediate: {possibleImm}."),
        };
    }
}
