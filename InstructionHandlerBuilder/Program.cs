/* =============================================================================
 * File:   Program.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * <TODO>
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

using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace InstructionHandlerBuilder;

// TODO: rework this to work directly on `Opcode.cs`

/// <summary>
/// A single instruction encoding handler.
/// </summary>
/// <param name="Encodings">The various encodings.</param>
/// <param name="Operands">The operands this handler handles.</param>
[Serializable]
public record struct Handler(string[] Encodings, string Operands);

[Serializable]
public class OutputFile
{
    /// <summary>
    /// A mapping between an output class and its handlers.
    /// </summary>
    public Dictionary<string, List<Handler>> Classes { get; init; } = new();
}

[Serializable]
public class State
{
    /// <summary>
    /// A mapping between output file names and their object.
    /// </summary>
    public Dictionary<string, OutputFile> OutputFiles { get; init; } = new();

    /// <summary>
    /// The line in the source.
    /// </summary>
    public int Line = 0;
}

public static class Program
{
    public static void Main()
    {
        JsonSerializerOptions serializerOptions = new()
        {
            IncludeFields = true,
            WriteIndented = true,
        };

        State state = File.Exists("state.json")
            ? JsonSerializer.Deserialize<State>(File.ReadAllText("state.json"), serializerOptions)!
            : new();

        ConsoleColor oldColor = Console.ForegroundColor;

        // minus 1 because the empty line at the end
        Span<string> input = File.ReadAllLines("input.txt").AsSpan();
        for (int i = state.Line; i < input.Length - 1; i++)
        {
            Debug.Assert(input[i].StartsWith("//"));

            int opcodeLineNum = i;
            while (input[opcodeLineNum].StartsWith("//"))
                opcodeLineNum++;

            string[] encodings = input[i..(opcodeLineNum - i)].ToArray();
            string opcode = input[opcodeLineNum];

            // extract the output class and function name from opcode
            (string classname, string operands) = GetClassAndOperandsFromOpcode(opcode);

            // fix up classname for all vector opcodes
            if (classname[0] is 'V')
                classname = char.ToUpper(classname[1]) + classname[2..]; // remove the 'V'

            Handler handler = new(encodings, operands);

            for (int j = 0; j < 10; j++)
                Console.WriteLine();

            // print info for prompt
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            foreach (string comment in encodings)
                Console.WriteLine(comment);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(opcode);

            // by default, the class name is the filename, but prompt for a change
            string filename = classname;
            // except FMA opcodes
            if (filename.Contains("132") || filename.Contains("213") || filename.Contains("231"))
                filename = filename.Replace("132", "nnn").Replace("213", "nnn").Replace("231", "nnn");
            // except kmask manipulation opcodes
            if (filename[0] is 'K')
                filename = filename[..^1]; // drop the size suffix
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"Place '{operands}' inside '{filename}.cs'?");

                Console.ForegroundColor = oldColor;
                Console.Write(" [Y/n] ");
                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();
                if (key.Key is ConsoleKey.Enter or ConsoleKey.Y)
                    break;

                if (key.Key is ConsoleKey.N)
                {
                    Console.Write("Enter new filename (without '.cs'): ");
                    filename = Console.ReadLine()!;
                    if (filename != "")
                        break;
                }
            }

            // V4FMADDxx will have the classname "4fmaddxx" which isn't allowed
            // just prefix with an underscore
            if (!char.IsLetter(classname[0]))
                classname = '_' + classname;

            AddHandler(state, filename, classname, handler);

            i = opcodeLineNum; // skip up to the line we last checked
            state.Line = i + 1; // set the new starting point for resuming

            // save state after every change
            File.WriteAllText("state.json", JsonSerializer.Serialize(state, typeof(State), serializerOptions));
        }

        // generate all the files!
        if (!Directory.Exists("output"))
            Directory.CreateDirectory("output");

        string template = File.ReadAllText("OutputFile.template");

        // build a file
        foreach ((string filename, OutputFile date) in state.OutputFiles)
            BuildAndSaveFile(template, filename, date);
    }

    public static void AddHandler(State state, string filename, string className, Handler handler)
    {
        OutputFile file = state.OutputFiles.GetOrAdd(filename);
        List<Handler> classHandlers = file.Classes.GetOrAdd(className);
        classHandlers.Add(handler);
    }

    public static (string, string) GetClassAndOperandsFromOpcode(string opcode)
    {
        const string REGEX = @"([A-Z][a-z0-9]*)(.*)";
        GroupCollection matches = Regex.Match(opcode, REGEX).Groups;

        // $0 is the whole match
        // $1 is the mnemonic
        // $2 is the operands (if any)
        Debug.Assert(matches.Count == 3);

        string mnemonic = matches[1].Value;
        string operands = matches[2].Value;

        // if there's no operands, the function is named `_`
        return operands == ""
            ? (mnemonic, "_")
            : (mnemonic, operands);
    }

    public static void BuildAndSaveFile(string fileTemplate, string filename, OutputFile fileData)
    {
        // build a class
        StringBuilder classesStr = new();
        foreach ((string className, List<Handler> handlers) in fileData.Classes)
        {
            // build a handler
            StringBuilder handlersStr = new();
            foreach (Handler handler in handlers)
            {
                handlersStr.AppendLine();
                foreach (string encoding in handler.Encodings)
                    handlersStr.AppendLine($"    {encoding}");
                handlersStr.AppendLine($"    public static void {handler.Operands}(CpuCore cpu, Instruction instr) =>");
                handlersStr.AppendLine("        throw new NotImplementedException();");
            }
            classesStr.AppendLine($"public static class {className}");
            classesStr.AppendLine("{");
            classesStr.AppendLine(handlersStr.ToString());
            classesStr.AppendLine("}");
        }

        string fileStr = fileTemplate
            .Replace("$1", filename)
            .Replace("$2", classesStr.ToString());

        // ensure output directory exists
        string outputFolder = Path.Combine("output", filename[0].ToString());
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
            Thread.Sleep(1000);
        }

        // write the data
        File.WriteAllText(Path.Combine(outputFolder, filename + ".cs"), fileStr);
    }
}

public static class DictionaryExtensions
{
    public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> d, TKey key)
        where TKey : notnull
        where TValue : new()
    {
        if (d.TryGetValue(key, out TValue? value))
            return value;

        TValue @new = new();
        d.Add(key, @new);
        return @new;
    }
}
