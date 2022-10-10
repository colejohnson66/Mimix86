/* =============================================================================
 * File:   DecodeDescriptors.cs
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
using static Mimix86.Core.Cpu.Decoder.Decoder;

namespace Mimix86.Core.Cpu.Decoder;

public static class DecodeDescriptors
{
    public static DecodeDescriptorEntry[] NoPrefixDescriptor { get; }

    public static ImmSize[] ImmediateDescriptor { get; }

    static DecodeDescriptors()
    {
        NoPrefixDescriptor = new DecodeDescriptorEntry[256];
        for (int i = 0; i < 256; i++)
            NoPrefixDescriptor[i] = new(null, DecodeUD);

        ImmediateDescriptor = new ImmSize[256];
        Array.Fill(ImmediateDescriptor, ImmSize.None);
    }
}
