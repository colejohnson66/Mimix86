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
