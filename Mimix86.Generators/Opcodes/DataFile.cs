/* =============================================================================
 * File:   DataFile.cs
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

using SExpressionReader;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Mimix86.Generators.Opcodes;

public record DataFile(
    DataFileInfo Info,
    IReadOnlyDictionary<byte, string>? OneBytePrefixes,
    // IReadOnlyDictionary<byte, string>? TwoBytePrefixes,
    IReadOnlyList<Instruction> Instructions)
{
    public static DataFile Parse(Parser parser)
    {
        DataFileInfo? info = null;
        IReadOnlyDictionary<byte, string>? oneBytePrefixes = null;
        IReadOnlyList<Instruction>? instructions = null;

        foreach (Expression node in parser.Parse())
        {
            if (node.Count < 2)
                throw new InvalidDataException("Expression is too small.");
            string key = node[0].AsAtom().Value;

            if (key is "file")
            {
                if (info is not null)
                    throw new InvalidDataException("Duplicate file info expression encountered.");
                info = ProcessInfo(node.Skip(1));
            }

            if (key is "one-b-prefixes")
            {
                if (oneBytePrefixes is not null)
                    throw new InvalidDataException("Duplicate one-byte prefix expression encountered.");
                oneBytePrefixes = ProcessPrefixes(node.Skip(1), GetEnumMemberForOneBytePrefix);
            }

            if (key is "two-b-prefixes")
            {
                throw new NotSupportedException("Two-byte prefixes are not supported yet.");
                // if (twoBytePrefixes is not null)
                //     throw new InvalidDataException("Duplicate two-byte prefix expression encountered.");
                // twoBytePrefixes = ProcessPrefixes(node.Skip(1), GetEnumMemberForTwoBytePrefix);
            }

            if (key is "instructions")
            {
                if (instructions is not null)
                    throw new InvalidDataException("Duplicate instruction expression encountered.");
                instructions = ProcessInstructions(node.Skip(1));
            }
        }

        if (info is null)
            throw new InvalidDataException("File info expression was not encountered.");
        if (instructions is null)
            throw new InvalidOperationException("Instructions expression was not encountered.");

        return new(info, oneBytePrefixes, instructions);
    }

    private static DataFileInfo ProcessInfo(IEnumerable<AtomOrExpression> file)
    {
        string? name = null;
        string[]? summary = null;

        foreach (Expression expr in file.Select(aoe => aoe.AsExpression()))
        {
            string key = expr[0].AsAtom().Value;

            if (key is "name")
            {
                if (name is not null)
                    throw new InvalidDataException("Duplicate \"name\" entry.");
                name = expr[1].AsAtom().Value;
            }
            else if (key is "summary")
            {
                if (summary is not null)
                    throw new InvalidDataException("Duplicate \"summary\" entry.");
                summary = expr[1]
                    .AsExpression()
                    .Select(aoe => aoe.AsAtom().Value)
                    .ToArray();
            }
            else
            {
                throw new InvalidCastException("Unknown key in info.");
            }
        }

        if (name is null || summary is null)
            throw new InvalidDataException("Incomplete file info.");

        return new(name, summary);
    }

    private static SortedDictionary<byte, string> ProcessPrefixes(IEnumerable<AtomOrExpression> prefixes, Func<string, string> lookup)
    {
        SortedDictionary<byte, string> result = new();

        foreach (Expression expr in prefixes.Select(aoe => aoe.AsExpression()))
        {
            if (expr.Count is not 2)
                throw new InvalidDataException("Prefix expression is not the right length.");

            byte value = byte.Parse(expr[0].AsAtom().Value, NumberStyles.HexNumber);
            string prefix = expr[1].AsAtom().Value;

            result.Add(value, lookup(prefix));
        }

        return result;
    }

    private static string GetEnumMemberForOneBytePrefix(string key) =>
        key switch
        {
            "two-byte"    => "TwoByteEscape", // [0F]
            "seg-es"      => "SegmentES",     // [26]
            "seg-cs"      => "SegmentCS",     // [2E]
            "seg-ds"      => "SegmentDS",     // [36]
            "seg-ss"      => "SegmentSS",     // [3E]
            "rex"         => "Rex",           // [40]..[4F]
            "l1om-scalar" => "L1OMScalar",    // [62]
            "mvex"        => "Mvex",          // [62]
            "evex"        => "Evex",          // [62]
            "seg-ds2"     => "SegmentDS2",    // [63]
            "rep-c"       => "Repc",          // [64]
            "seg-fs"      => "SegmentFS",     // [64]
            "rep-nc"      => "Repnc",         // [65]
            "seg-gs"      => "SegmentGS",     // [65]
            "osize"       => "OperandSize",   // [66]
            "asize"       => "AddressSize",   // [67]
            "xop"         => "Xop",           // [8F]
            "vex3"        => "Vex3",          // [C4]
            "vex2"        => "Vex2",          // [C5]
            "rex2"        => "Rex2",          // [D5]
            "seg-ds3"     => "SegmentDS3",    // [D6]
            "l1om-vector" => "L1OMVector",    // [D6]
            "lock"        => "Lock",          // [F0]
            "seg-iram"    => "SegmentIRam",   // [F1]
            "rep-ne"      => "Repne",         // [F2]
            "rep-e"       => "RepRepe",       // [F3]
            _             => throw new InvalidDataException("Unknown or unsupported prefix."),
        };

    // private static string? GetEnumMemberForTwoBytePrefix(string key) =>
    //     key switch
    //     {
    //         "3d-now"        => "_3DNow",              // [0F 0F]
    //         "drex-24"       => "Drex0F24",            // [0F 24]
    //         "drex-25"       => "Drex0F25",            // [0F 25]
    //         "three-byte-38" => "ThreeByteEscape0F38", // [0F 38]
    //         "three-byte-3a" => "ThreeByteEscape0F3A", // [0F 3A]
    //         "drex-7a"       => "Drex0F7A",            // [0F 7A]
    //         "drex-7b"       => "Drex0F7B",            // [0F 7B]
    //         _               => throw new InvalidDataException("Unknown or unsupported prefix."),
    //     };

    private static List<Instruction> ProcessInstructions(IEnumerable<AtomOrExpression> instructions)
    {
        List<Instruction> result = new();

        foreach (AtomOrExpression aoe in instructions)
        {
            if (!aoe.TryAsExpression(out Expression? expr))
                throw new InvalidDataException("Unknown atom in instruction list.");

            result.Add(new(expr));
        }

        return result;
    }
}
