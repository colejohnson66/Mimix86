/* =============================================================================
 * File:   StringArrayExtensions.cs
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

using System.Collections.Generic;
using System.Text;

namespace Mimix86.Generators;

public static class StringArrayExtensions
{
    public static string Join(this IList<string> array, string joiner)
    {
        if (array.Count is 0)
            return "";

        StringBuilder builder = new();
        for (int i = 0; i < array.Count; i++)
        {
            builder.Append(array[i]);
            if (i != array.Count - 1)
                builder.Append(joiner);
        }
        return builder.ToString();
    }
}
