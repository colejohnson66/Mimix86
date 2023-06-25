/* =============================================================================
 * File:   Helpers.cs
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
 *   Mimix86. If not, see <http://www.gnu.org/licenses/>.
 * =============================================================================
 */

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Mimix86.Generators;

public static class Helpers
{
    private static readonly Lazy<string> Mimix86CorePathLazy = new(() =>
    {
        // find the folder containing the sln file
        DirectoryInfo current = new(Directory.GetCurrentDirectory());
        while (current.GetFiles().All(file => !file.Name.EndsWith(".sln")))
            current = current.Parent!;

        return Path.Combine(current.FullName, "Mimix86.Core");
    });
    public static string Mimix86CorePath => Mimix86CorePathLazy.Value;


    public static string KebabCaseToPascalCase(string input)
    {
        // this will throw if the input is empty or contains sequential skewer portions
        // as conforming strings should never do such things, no validation is performed

        // result will either be the same length, or equal to the input's
        //   (unless it starts with a digit, then add one for underscore)
        StringBuilder builder = new(input.Length + 1);
        string[] meats = input.Split('-');

        if (char.IsDigit(meats[0][0]))
            builder.Append('_'); // variable/property names can't start with digits

        foreach (string meat in meats)
        {
            char start = char.ToUpperInvariant(meat[0]);
            builder.Append(start);
            builder.Append(meat.AsSpan(1));
        }

        return builder.ToString();
    }
}
