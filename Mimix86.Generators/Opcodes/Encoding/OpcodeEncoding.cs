/* =============================================================================
 * File:   OpcodeEncoding.cs
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
using System.Diagnostics;
using System.IO;

namespace Mimix86.Generators.Opcodes.Encoding;

public sealed class OpcodeEncoding :
    IComparable<OpcodeEncoding>
{
    private readonly byte? _opcode;
    private readonly string? _opmapEntryName;

    public OpcodeEncoding(string[] elements)
    {
        EncodingParser parser = new(elements);

        // currently, all opcodes are of the form:
        //   <op> <mod-rm>? <imm>?

        while (parser.Next() is EncodingPart part)
        {
            switch (part)
            {
                case EncodingPart.Byte b:
                    if (_opcode is not null)
                        throw new InvalidDataException("Multiple opcode bytes are not allowed.");
                    _opcode = b.Value;
                    _opmapEntryName = b.Value.ToString("X2");
                    break;

                case EncodingPart.BytePlusRegister b:
                    if (_opcode is not null)
                        throw new InvalidDataException("Multiple opcode bytes are not allowed.");
                    if ((b.BaseValue & 7) != 0)
                        throw new InvalidDataException("Opcode bytes with a register must have the three LSB cleared.");
                    _opcode = b.BaseValue;
                    _opmapEntryName = $"{b.BaseValue:X2}x{b.BaseValue + 7:X2}"; // "40+r" -> "40x47"
                    break;

                case EncodingPart.BytePlusCondition b:
                    if (_opcode is not null)
                        throw new InvalidDataException("Multiple opcode bytes are not allowed.");
                    if ((b.BaseValue & 15) != 0)
                        throw new InvalidDataException("Opcode bytes with a condition must have the four LSB cleared.");
                    _opcode = b.BaseValue;
                    _opmapEntryName = $"{b.BaseValue:X2}x{b.BaseValue + 15:X2}"; // "70+cc" -> "70x7F"
                    break;

                case EncodingPart.ModRM modRM:
                    if (ModRM is not null)
                        throw new InvalidDataException("Multiple ModR/M bytes are not allowed.");
                    ModRM = modRM;
                    break;

                case EncodingPart.Immediate imm:
                    if (Immediate is not null)
                    {
                        // `ENTER Iw, Ib` is the only opcode with two immediates
                        // validate that this is indeed `ENTER Iw, Ib`, and, if so, override the size to the correct enum member
                        if (Opcode is not 0xC8 || Immediate is not ImmediateSizes.Word || imm.Size is not ImmediateSizes.Byte)
                            throw new InvalidDataException("Multiple immediates are only allowed for ENTER Iw, Ib.");
                        Immediate = ImmediateSizes.WordByte;
                        break;
                    }

                    Immediate = imm.Size;
                    break;

                default:
                    throw new UnreachableException();
            }
        }

        if (_opcode is null)
        {
            Debug.Assert(_opmapEntryName is null);
            throw new InvalidDataException("Opcode byte not found.");
        }
        Debug.Assert(_opmapEntryName is not null);
    }


    public byte Opcode => _opcode!.Value; // SAFETY: validated at the end of the constructor

    public string OpcodeMapEntryName => _opmapEntryName!; // SAFETY: validated at the end of the constructor

    public EncodingPart.ModRM? ModRM { get; }

    public ImmediateSizes? Immediate { get; }


    public int CompareTo(OpcodeEncoding? other)
    {
        if (ReferenceEquals(this, other))
            return 0;
        if (ReferenceEquals(null, other))
            return 1;

        return string.Compare(OpcodeMapEntryName, other.OpcodeMapEntryName, StringComparison.Ordinal);
    }
}
