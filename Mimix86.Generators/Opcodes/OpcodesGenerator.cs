﻿/* =============================================================================
 * File:   OpcodesGenerator.cs
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
 *   Mimix86. If not, see <https://www.gnu.org/licenses/>.
 * =============================================================================
 */

using SExpressionReader;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mimix86.Generators.Opcodes;

public static class OpcodesGenerator
{
    private const string OPCODE_LIST_TEMPLATE_HEADER =
        $$"""
        // generated by `{{nameof(Mimix86)}}.{{nameof(Generators)}}.{{nameof(Opcodes)}}.{{nameof(OpcodesGenerator)}}`
        // any changes will be lost on next generation

        using Mimix86.Core.Cpu.Decoder;
        using Mimix86.Core.Cpu.Execution;
        using System;

        namespace Mimix86.Core.Cpu.Isa;

        public sealed partial class Opcode
        {
            /// <summary>
            /// The "undefined" opcode.
            /// Used to signify that the instruction stream decodes to an undefined opcode.
            /// </summary>
            public static Opcode Undefined { get; } = new("<error>", Execution.Error._, 0, null);

        """;

    private const string OPCODE_MAP_TEMPLATE_HEADER =
        $$"""
        // generated by `{{nameof(Mimix86)}}.{{nameof(Generators)}}.{{nameof(Opcodes)}}.{{nameof(OpcodesGenerator)}}`
        // any changes will be lost on next generation

        using static Mimix86.Core.Cpu.Decoder.DecodeFlags;
        using static Mimix86.Core.Cpu.Isa.Opcode;

        namespace Mimix86.Core.Cpu.Decoder;

        // CS1591 is XML doc comments
        #pragma warning disable CS1591
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// Enumerates all the supported opcodes by their location in the various opmaps.
        /// </summary>
        public static class OpcodeMap
        {
            public static readonly OpcodeMapEntry[] OpcodeUndefined =
            {
                new(Undefined, .., DecodeFlags.None),
            };

        """;

    /* (ADD  (Eb Gb)  (00 /r)  (lockable))
     *      |
     *      v
     * public static Opcode AddEbGb { get; } = new("add", Add.EbGb, OpcodeFlags.Lockable, 0);
     */

    private static readonly List<Instruction> KnownInstructions = new();

    public static void Run()
    {
        foreach (string path in Directory.EnumerateFiles("./Data/Opcodes", "*.lisp", SearchOption.AllDirectories))
            ProcessFile(path);

        WriteOpcodeList();

        // BuildInstructionMap();
    }

    private static void ProcessFile(string path)
    {
        string contents = File.ReadAllText(path);
        using Parser parser = new(contents);

        DataFile file = DataFile.Parse(parser);
        KnownInstructions.AddRange(file.Instructions);
    }

    private static void WriteOpcodeList()
    {
        string outputPath = Path.Combine(Helpers.Mimix86CorePath, "Cpu", "Isa", "Opcode.List.g.cs");
        using FileStream handle = File.Open(outputPath, FileMode.Create, FileAccess.Write);
        using StreamWriter writer = new(handle);

        writer.Write(OPCODE_LIST_TEMPLATE_HEADER);
        foreach (Instruction op in KnownInstructions.DistinctBy(op => (op.TitleCaseMnemonic, op.OperandsString)).Order())
        {
            writer.WriteLine();
            writer.WriteLine(op.GenerateOpcodeMember());
        }
        writer.WriteLine("}");
    }

    // private static void BuildInstructionMap()
    // {
    //     // when building the opcode map, we also need to consider the encoding, unlike the opcode list
    //     foreach (Instruction op in KnownInstructions.DistinctBy(op => (op.TitleCaseMnemonic, op.OperandsString, op.Encoding)))
    //     {
    //         // "get or insert" operation here
    //         string key = op.Encoding.OpcodeMapEntryName;
    //         if (!InstructionMap.TryGetValue(key, out List<Instruction>? value))
    //         {
    //             value = new();
    //             InstructionMap.Add(key, value);
    //         }
    //
    //         value.Add(op);
    //     }
    // }
}
