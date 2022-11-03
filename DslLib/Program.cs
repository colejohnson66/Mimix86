// See https://aka.ms/new-console-template for more information

using System;

namespace DslLib;

internal static class Program
{
    private const string INPUT =
        """
        # 8086+
        ADD [Eb Gb] [00 /r] ..
        ADD [Ew Gw] [01 /r] .. [OS16]
        ADD [Gb Eb] [02 /r] ..
        """;

    public static void Main()
    {
        // Tokenizer tokenizer = new(INPUT);
        // foreach (Token tok in tokenizer.Tokenize())
        //     Console.WriteLine(tok);

        Parser parser = new(INPUT);
        foreach (Node node in parser.Parse())
            Console.WriteLine(node);
    }
}
