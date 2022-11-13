﻿using DslLib;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Mimix86.Generators;

public static class AllOpcodesGenerator
{
    private const string FILE_TEMPLATE_HEADER =
        """
        // generated by `Mimix86.Generators.AllOpcodesGenerator`
        // any changes will be lost on next generation

        using Mimix86.Core.Cpu.Execution;

        #pragma warning disable CS1591 // missing XML doc comment

        namespace Mimix86.Core.Cpu.Decoder;

        [SuppressMessage("ReSharper", "IdentifierTypo")]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public partial class Opcode
        {
            /// <summary>
            /// The "undefined" opcode.
            /// Used to signify that the instruction stream decodes to an undefined opcode.
            /// </summary>
            public static Opcode Undefined { get; } = new("<error>", Execution.Error._);
        """;

    private static readonly HashSet<string> KnownOpcodes = new();
    private static readonly Dictionary<uint, List<string>> OutputMap = new();

    private static void InitOutputMap()
    {
        for (uint i = 0; i <= 0xFF; i++)
            OutputMap.Add(i, new());
    }

    public static void Run()
    {
        InitOutputMap();
        foreach (string path in Directory.GetFiles("./AllOpcodes", "*.m86"))
            HandleInput(path);
    }

    private static string FindDecoderFolder()
    {
        // find the folder containing the .git folder
        DirectoryInfo current = new(Directory.GetCurrentDirectory());
        while (current.GetDirectories().All(dir => dir.Name is not ".git"))
            current = current.Parent!;

        return Path.Combine(current.FullName, "Mimix86.Core", "Cpu", "Decoder");
    }

    private static void HandleInput(string path)
    {
        string contents = File.ReadAllText(path);
        Parser parser = new(contents);
        Node[] nodes = parser.Parse().ToArray();
        Debug.Assert(nodes.Any());
    }

//     private static string ParseInput(string text)
//     {
//         Parser parser = new(text);
//
//         StringBuilder opcodes = new();
//         opcodes.AppendLine(
//             """
//             // generated by `Mimix86.Generators.AllOpcodesGenerator`
//             // any changes will be lost on next generation
//
//             namespace Mimix86.Core.Cpu.Decoder;
//
//             /// <summary>
//             /// Contains all opcodes known the Mimix86.
//             /// </summary>
//             [SuppressMessage("ReSharper", "IdentifierTypo")]
//             [SuppressMessage("ReSharper", "InconsistentNaming")]
//             public enum Opcode
//             {
//                 /// <summary>A placeholder for undefined opcodes.</summary>
//                 Error,
//             """);
//         opcodes.AppendLine();
//
//         foreach (Node row in parser.Parse())
//             opcodes.AppendLine(GenerateOpcodeEntryForRow(row));
//
//         opcodes.AppendLine("}");
//         return opcodes.ToString();
//     }

    private static string GenerateOpcodeEntryForRow(Node row)
    {
        // string requiredCpuLevel = row.Children![3].Text!;

        string mnemonic = row.Children![0].Text!;
        StringBuilder enumMember = new(char.ToUpperInvariant(mnemonic[0]) + mnemonic[1..].ToLowerInvariant());
        foreach (string operand in row.Children[1].Children!.Select(cell => cell.Text!))
            enumMember.Append(operand);

        StringBuilder encoding = new();
        foreach (string bytes in row.Children[2].Children!.Select(cell => cell.Text!))
        {
            if (encoding.Length is not 0)
                encoding.Append(' ');
            encoding.Append(bytes);
        }

        const string TAB = "    ";
        return
            $$"""
            {{TAB}}/// <summary><c>[{{encoding}}]</c></summary>
            {{TAB}}{{enumMember}},
            """ ;
    }
}
