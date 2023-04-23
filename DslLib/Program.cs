/* =============================================================================
 * File:   Program.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * <TODO>
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
 *   Mimix86. If not, see <http://www.gnu.org/licenses/>.
 * =============================================================================
 */

using JetBrains.Annotations;
using System;

namespace DslLib;

internal static class Program
{
    private const string INPUT =
        "# 8086+\r\n" +
        "ADD [Eb Gb] [00 /r] ..\r\n" +
        "ADD [Ew Gw] [01 /r] .. [OS16]\r\n" +
        "ADD [Gb Eb] [02 /r] ..\r\n" +
        "ADD [Gw Ew] [03 /r] .. [OS16]\r\n" +
        "ADD [AL Ib] [04 ib] ..\r\n" +
        "ADD [AX Iw] [05 iw] .. [OS16]\r\n" +
        "PUSH [ES] [06] ..\r\n" +
        "# ...\r\n" +
        "DAA [] [27] ..\r\n";

    [UsedImplicitly]
    public static void Main()
    {
        using Parser parser = new(INPUT);
        foreach (Node node in parser.Parse())
            Console.WriteLine(node);
    }
}
