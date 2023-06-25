/* =============================================================================
 * File:   Program.cs
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

using JetBrains.Annotations;
using System;

namespace SExpressionReader;

internal static class Program
{
    // excerpts from the 8086 definition file
    private const string INPUT =
        """
        (file
            (type  cpu-base-set)
            (name  8086)
        )

        (one-b-prefixes
            (26 ES)
            (2E CS)
            ; ...
        )

        (instructions
            ; ALU instructions of [00-bbb-xxx] (the [00-3F] block)
            ;   where `bbb` is the operation and `xxx` (0 through 5) is the form
            (ADD  (Eb Gb)  (00 /r)  (lockable))
            (ADD  (Ew Gw)  (01 /r)  (lockable))
            (ADD  (Gb Eb)  (02 /r)  ())
            (ADD  (Gw Ew)  (03 /r)  ())
            ; ...
        )
        """;

    [UsedImplicitly]
    public static void Main()
    {
        using Parser parser = new(INPUT);
        foreach (AtomOrExpression node in parser.Parse())
            Console.WriteLine(node);
        // using Tokenizer tokenizer = new(INPUT);
        // foreach (Token tok in tokenizer.Tokenize())
        //     Console.WriteLine(tok);
    }
}
