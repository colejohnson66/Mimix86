﻿/* =============================================================================
 * File:   OpcodesGenerator.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Generates the following:
 *   - `Opcode.List.g.cs` - all opcodes known to Mimix86
 *   - `OpcodeMap.cs`     - the map of bytes into opcodes (TODO)
 * =============================================================================
 * Copyright (c) 2022 Cole Tobin
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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mimix86.Generators.Opcodes;

public static class OpcodesGenerator
{
    private const string FILE_TEMPLATE_HEADER =
        $$"""
        // generated by `{{nameof(Mimix86)}}.{{nameof(Generators)}}.{{nameof(Opcodes)}}.{{nameof(OpcodesGenerator)}}`
        // any changes will be lost on next generation

        using Mimix86.Core.Cpu.Execution;
        using System;

        namespace Mimix86.Core.Cpu.Decoder;

        public partial class Opcode
        {
            /// <summary>
            /// The "undefined" opcode.
            /// Used to signify that the instruction stream decodes to an undefined opcode.
            /// </summary>
            public static Opcode Undefined { get; } = new("<error>", Execution.Error._);

        """;

    /* ADD [Eb Gb] [00 /r] .. [lock]
     *      |
     *      v
     * public static Opcode AddEbGb { get; } = new("add", new[] { "Eb", "Gb" }, Add.EbGb) { Flags = OpcodeFlags.Lockable }
     */

    private static readonly List<Opcode> KnownOpcodes = new();
    // ReSharper disable once CollectionNeverQueried.Local
    private static readonly Dictionary<uint, List<string>> OutputMap = new();

    private static void InitOutputMap()
    {
        for (uint i = 0; i <= 0xFF; i++)
            OutputMap.Add(i, new());
    }

    private static string FindMimix86Core()
    {
        // find the folder containing the .git folder
        DirectoryInfo current = new(Directory.GetCurrentDirectory());
        while (current.GetDirectories().All(dir => dir.Name is not ".git"))
            current = current.Parent!;

        return Path.Combine(current.FullName, "Mimix86.Core");
    }

    public static void Run()
    {
        InitOutputMap();
        foreach (string path in Directory.GetFiles("./Data/Opcodes", "*.m86"))
            HandleInput(path);
    }

    private static void HandleInput(string path)
    {
        string contents = File.ReadAllText(path);
        Parser parser = new(contents);

        foreach (Node node in parser.Parse())
            KnownOpcodes.Add(new(node));

        string outputPath = Path.Combine(FindMimix86Core(), "Cpu", "Decoder", "Opcode.List.g.cs");
        using FileStream handle = File.OpenWrite(outputPath);
        using StreamWriter writer = new(handle);

        writer.Write(FILE_TEMPLATE_HEADER);
        foreach (Opcode op in KnownOpcodes.Distinct())
        {
            writer.WriteLine();
            writer.WriteLine(op.GenerateOpcodeMember("    "));
        }
        writer.WriteLine("}");
    }
}
