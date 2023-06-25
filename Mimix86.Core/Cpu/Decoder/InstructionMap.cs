/* =============================================================================
 * File:   InstructionMap.cs
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

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Contains the various maps that instructions may be in.
/// </summary>
public enum InstructionMap
{
    /// <summary>
    /// Indicates that the relevant opcode is in the (original) one-byte opcode space.
    /// </summary>
    OneByte,

    /// <summary>
    /// Indicates that the relevant opcode is in the two-byte <c>[0F]</c> opcode space.
    /// </summary>
    TwoByte,

    /// <summary>
    /// Indicates that the relevant opcode is in the 3D Now! opcode space, as an eight-bit immediate for the <c>[0F
    ///   0F]</c> "opcode".
    /// </summary>
    _3DNow,

    /// <summary>
    /// Indicates that the relevant opcode is in the three-byte <c>[0F 38]</c> opcode space.
    /// </summary>
    ThreeByte0F38,

    /// <summary>
    /// Indicates that the relevant opcode is in the three-byte <c>[0F 3A]</c> opcode space.
    /// </summary>
    ThreeByte0F3A,

    /// <summary>
    /// Indicates that the relevant opcode is in the DREX opcode space, prefixed by <c>[0F 24]</c>.
    /// </summary>
    Drex0F24,

    /// <summary>
    /// Indicates that the relevant opcode is in the DREX opcode space, prefixed by <c>[0F 25]</c>.
    /// </summary>
    Drex0F25,

    /// <summary>
    /// Indicates that the relevant opcode is in the DREX opcode space, prefixed by <c>[0F 7A]</c>.
    /// </summary>
    Drex0F7A,

    /// <summary>
    /// Indicates that the relevant opcode is in the DREX opcode space, prefixed by <c>[0F 7B]</c>.
    /// </summary>
    Drex0F7B,

    /// <summary>
    /// Indicates that the relevant opcode is in the VEX opcode space, using map 1, which corresponds to the legacy
    ///   <see cref="TwoByte" /> map.
    /// </summary>
    Vex0F,

    /// <summary>
    /// Indicates that the relevant opcode is in the VEX opcode space, using map 2, which corresponds to the legacy
    ///   <see cref="ThreeByte0F38" /> map.
    /// </summary>
    Vex0F38,

    /// <summary>
    /// Indicates that the relevant opcode is in the VEX opcode space, using map 3, which corresponds to the legacy
    ///   <see cref="ThreeByte0F3A" /> map.
    /// </summary>
    Vex0F3A,

    /// <summary>
    /// Indicates that the relevant opcode is in the XOP opcode space, using map 8.
    /// </summary>
    Xop8,

    /// <summary>
    /// Indicates that the relevant opcode is in the XOP opcode space, using map 9.
    /// </summary>
    Xop9,

    /// <summary>
    /// Indicates that the relevant opcode is in the XOP opcode space, using map 10 (0xA).
    /// </summary>
    XopA,

    /// <summary>
    /// Indicates that the relevant opcode is in the L1OM scalar opcode space.
    /// </summary>
    L1OMScalar,

    /// <summary>
    /// Indicates that the relevant opcode is in the L1OM vector opcode space.
    /// </summary>
    L1OMVector,

    /// <summary>
    /// Indicates that the relevant opcode is in the MVEX/EVEX opcode space, using map 0, which corresponds to the
    ///   legacy <see cref="OneByte" /> map.
    /// </summary>
    MvexEvex,

    /// <summary>
    /// Indicates that the relevant opcode is in the MVEX/EVEX opcode space, using map 1, which corresponds to the
    ///   legacy <see cref="TwoByte" /> map.
    /// </summary>
    MvexEvex0F,

    /// <summary>
    /// Indicates that the relevant opcode is in the MVEX/EVEX opcode space, using map 2, which corresponds to the
    ///   legacy <see cref="ThreeByte0F38" /> map.
    /// </summary>
    MvexEvex0F38,

    /// <summary>
    /// Indicates that the relevant opcode is in the MVEX/EVEX opcode space, using map 3, which corresponds to the
    ///   legacy <see cref="ThreeByte0F3A" /> map.
    /// </summary>
    MvexEvex0F3A,

    /// <summary>
    /// Indicates that the relevant opcode is in the MVEX/EVEX opcode space, using map 5.
    /// </summary>
    MvexEvex5,

    /// <summary>
    /// Indicates that the relevant opcode is in the MVEX/EVEX opcode space, using map 6.
    /// </summary>
    MvexEvex6,
}
