﻿/* =============================================================================
 * File:   OpcodeEncoding.cs
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

using System.Diagnostics;
using System.IO;

namespace Mimix86.Generators.Opcodes.Encoding;

public sealed class OpcodeEncoding
{
    private readonly byte? _opcode;

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
                    break;

                case EncodingPart.BytePlusRegister b:
                    if (_opcode is not null)
                        throw new InvalidDataException("Multiple opcode bytes are not allowed.");
                    if ((b.BaseValue & 7) != 0)
                        throw new InvalidDataException("Opcode bytes with a register must have the three LSB cleared.");
                    _opcode = b.BaseValue;
                    break;

                case EncodingPart.BytePlusCondition b:
                    if (_opcode is not null)
                        throw new InvalidDataException("Multiple opcode bytes are not allowed.");
                    if ((b.BaseValue & 15) != 0)
                        throw new InvalidDataException("Opcode bytes with a condition must have the four LSB cleared.");
                    _opcode = b.BaseValue;
                    break;

                case EncodingPart.ModRM modRM:
                    if (ModRM is not null)
                        throw new InvalidDataException("Multiple ModR/M bytes are not allowed.");
                    ModRM = modRM;
                    break;

                case EncodingPart.Immediate imm:
                    if (Immediate is not null)
                    {
                        if (Opcode is not 0xC8 || Immediate is not ImmSize.Word || imm.Size is not ImmSize.Byte)
                            throw new InvalidDataException("Multiple immediates are only allowed for ENTER.");
                        Immediate = ImmSize.WordByte;
                        break;
                    }

                    Immediate = imm.Size;
                    break;

                default:
                    throw new UnreachableException();
            }
        }

        if (_opcode is null)
            throw new InvalidDataException("Opcode byte not found.");
    }


    public byte Opcode => _opcode!.Value; // SAFETY: validated at the end of the constructor

    public EncodingPart.ModRM? ModRM { get; }

    public ImmSize? Immediate { get; }


    // Sample output:
    //   public static Opcode AddEbGb { get; } = new("add", new[] { "Eb", "Gb" }, Add.EbGb) { Flags = OpcodeFlags.HasModRM | OpcodeFlags.Lockable }
}