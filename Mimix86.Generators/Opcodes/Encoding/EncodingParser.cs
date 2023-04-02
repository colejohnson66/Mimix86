/* =============================================================================
 * File:   Tokenzier.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * <TODO>
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
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Mimix86.Generators.Opcodes.Encoding;

public class EncodingParser
{
    private readonly string[] _input;
    private int _index = 0;
    private EncodingPart? _next;

    public EncodingParser(string[] input)
    {
        if (input.Length is 0)
            throw new ArgumentException("Input must contain at least one element.", nameof(input));
        if (input.Any(part => part.Length < 2))
            throw new ArgumentException("All elements must be at least two characters long.", nameof(input));

        _input = input;
    }

    public EncodingPart? Peek()
    {
        if (_next is null)
            return _next;

        _next = Next();
        return _next;
    }

    public EncodingPart? Next()
    {
        if (_next is not null)
        {
            EncodingPart next = _next;
            _next = null;
            return next;
        }

        if (_index == _input.Length)
            return null;

        string part = _input[_index++];
        if (part.Length < 2)
            throw new UnreachableException(); // validated in constructor

        // handle byte values
        if (char.IsAsciiHexDigit(part[0]) && char.IsAsciiHexDigit(part[1]))
        {
            byte value = Convert.ToByte(part[..2], 16);

            if (part.Length == 2)
                return new EncodingPart.Byte(value);

            // handle `+r` or `+cc`
            if (part.AsSpan(2).SequenceEqual("+r"))
                return new EncodingPart.BytePlusRegister(value);
            if (part.AsSpan(2).SequenceEqual("+cc"))
                return new EncodingPart.BytePlusCondition(value);

            throw new InvalidDataException($"Unknown byte suffix for part: \"{part}\".");
        }

        // handle ModR/M values
        if (part.Contains('/'))
        {
            string[] split = part.Split('/');
            if (split.Length is not (2 or 3))
                throw new InvalidDataException($"Possible ModR/M value is not valid: \"{part}\".");

            ModRMMod? mod = split[0] switch
            {
                "m" => ModRMMod.Memory,
                "r" => ModRMMod.Register,
                "" => null,
                _ => throw new InvalidDataException($"Unknown ModR/M mod part: \"{split[0]}\"."),
            };

            byte? reg = split[1] switch
            {
                "r" => null,
                "0" => 0,
                "1" => 1,
                "2" => 2,
                "3" => 3,
                "4" => 4,
                "5" => 5,
                "6" => 6,
                "7" => 7,
                _ => throw new InvalidDataException($"Unknown ModR/M reg part: \"{split[1]}\"."),
            };

            byte? rm = null;
            if (split.Length is 3)
            {
                rm = split[2] switch
                {
                    "0" => 0,
                    "1" => 1,
                    "2" => 2,
                    "3" => 3,
                    "4" => 4,
                    "5" => 5,
                    "6" => 6,
                    "7" => 7,
                    _ => throw new InvalidDataException($"Unknown ModR/M R/M part: {split[2]}."),
                };
            }

            return new EncodingPart.ModRM(mod, reg, rm);
        }

        // handle immediate values
        if (part[0] is 'i')
        {
            ImmSize size = part switch
            {
                "ib" => ImmSize.Byte,
                "iw" => ImmSize.Word,
                "ipw" => ImmSize.PointerWordWord,
                _ => throw new InvalidDataException($"Unknown immediate: \"{part}\"."),
            };
            return new EncodingPart.Immediate(size);
        }

        throw new InvalidDataException($"Unknown opcode encoding part: \"{part}\".");
    }
}
