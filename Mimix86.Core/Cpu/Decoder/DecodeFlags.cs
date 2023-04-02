/* =============================================================================
 * File:   DecodeFlags.cs
 * Author: Cole Tobin
 * =============================================================================
 * Purpose:
 *
 * Information for the decoder to indicate if a specific opcode in a group is
 *   valid.
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

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Contains information for the instruction decoder that indicates if a specific opcode in a group is valid.
/// </summary>
/// <remarks>
/// After decoding all the flags with a <see cref="DecodeFlagsBuilder" />, an opcode from the group can be checked with
///   <see cref="DecodeFlagsBuilder.Matches" />.
/// </remarks>
/// <example>
/// The <c>0F&#xA0;01</c> opcode group contains a few dozen individual opcodes.
/// Whether one is valid depends on the value of the ModR/M byte and legacy vector prefixes.
/// <br />
/// So, for example, the entry for <c>SMSW</c> (store machine status word), is documented as
///   <c>0F&#xA0;01&#xA0;/4</c> (register form only).
/// Therefore, its entry will contain <see cref="MOD_REG" />&#xA0;|&#xA0;<see cref="REG4" />.
/// </example>
[SuppressMessage("ReSharper", "ShiftExpressionZeroLeftOperand")]
public readonly struct DecodeFlags
{
    /* Flags are organized internally as follows:
     * ┌─────┬─────┬─────┬─────┬─────┬─────┬─────┬─────┐
     * │  31 │  30 │  29 │  28 │  27 │  26 │  25 │  24 │
     * │                  Reserved (0)                 │
     * ├─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┤
     * │  23 │  22 │  21 │  20 │  19 │  18 │  17 │  16 │
     * │                  Reserved (0)                 │
     * ├─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┤
     * │  15 │  14 │  13 │  12 │  11 │  10 │   9 │   8 │
     * │                  Reserved (0)                 │
     * ├─────┼─────┼─────┼─────┼─────┼─────┼─────┼─────┤
     * │   7 │   6 │   5 │   4 │   3 │   2 │   1 │   0 │
     * │ (0) │ Mod │   ModR/M.Reg    │    ModR/M.RM    │ <-- "Mod" is memory/register form
     * └─────┴─────┴─────────────────┴─────────────────┘
     *
     * These bits are contained in `RawValue` like this:
     * ┌─────┬─────┬─────┬─────┬─────┬─────┬─────┬─────┬─────┬─────┐
     * │  63 │  62 │  .. │  33 │  32 │  31 │  30 │  .. │   1 │   0 │
     * │            Masks            │            Values           │
     * └─────────────────────────────┴─────────────────────────────┘
     *
     */

    private const int MASKS_OFFSET = 32;

    /// <summary>
    /// The "raw" value of this <see cref="DecodeFlags" />.
    /// This contains the <see cref="Masks" /> in the upper 32 bits and the <see cref="Values" /> in the lower 32.
    /// </summary>
    public ulong RawValue { get; }

    /// <summary>
    /// The "masks" of this <see cref="DecodeFlags" />.
    /// These bits indicate which bits in <see cref="Values" /> are valid.
    /// </summary>
    public uint Masks => (uint)((RawValue >> 32) & 0xFFFF_FFFFu);

    /// <summary>
    /// The "values" of this <see cref="DecodeFlags" />.
    /// These bits indicate flags that must match in order for the decoder to consider the associated opcode a valid
    ///   target.
    /// </summary>
    public uint Values => (uint)(RawValue & 0xFFFF_FFFFu);

    /// <summary>
    /// Construct a new <see cref="DecodeFlags" /> with a given value.
    /// </summary>
    /// <param name="value">The value to assign to <see cref="RawValue" />.</param>
    /// <remarks>
    /// It is assumed that <paramref name="value" /> is a valid value.
    /// Passing an invalid one can cause unpredictable behavior.
    /// </remarks>
    public DecodeFlags(ulong value)
    {
        RawValue = value;
    }

    #region [0..=2] ModR/M.RM

    private const int RM_OFFSET = 0;
    private const ulong RM_ENABLE = 0b111ul << (RM_OFFSET + MASKS_OFFSET);

    /// <summary>Decode requires ModRM.rm is 0</summary>
    public const ulong RM0 = RM_ENABLE | (0ul << RM_OFFSET);
    /// <summary>Decode requires ModRM.rm is 1</summary>
    public const ulong RM1 = RM_ENABLE | (1ul << RM_OFFSET);
    /// <summary>Decode requires ModRM.rm is 2</summary>
    public const ulong RM2 = RM_ENABLE | (2ul << RM_OFFSET);
    /// <summary>Decode requires ModRM.rm is 3</summary>
    public const ulong RM3 = RM_ENABLE | (3ul << RM_OFFSET);
    /// <summary>Decode requires ModRM.rm is 4</summary>
    public const ulong RM4 = RM_ENABLE | (4ul << RM_OFFSET);
    /// <summary>Decode requires ModRM.rm is 5</summary>
    public const ulong RM5 = RM_ENABLE | (5ul << RM_OFFSET);
    /// <summary>Decode requires ModRM.rm is 6</summary>
    public const ulong RM6 = RM_ENABLE | (6ul << RM_OFFSET);
    /// <summary>Decode requires ModRM.rm is 7</summary>
    public const ulong RM7 = RM_ENABLE | (7ul << RM_OFFSET);

    #endregion

    #region [3..=5] ModR/M.Reg

    private const int REG_OFFSET = 2;
    private const ulong REG_ENABLE = 0b111ul << (REG_OFFSET + MASKS_OFFSET);

    /// <summary>Decode requires ModRM.reg is 0</summary>
    public const ulong REG0 = REG_ENABLE | (0ul << REG_OFFSET);
    /// <summary>Decode requires ModRM.reg is 1</summary>
    public const ulong REG1 = REG_ENABLE | (1ul << REG_OFFSET);
    /// <summary>Decode requires ModRM.reg is 2</summary>
    public const ulong REG2 = REG_ENABLE | (2ul << REG_OFFSET);
    /// <summary>Decode requires ModRM.reg is 3</summary>
    public const ulong REG3 = REG_ENABLE | (3ul << REG_OFFSET);
    /// <summary>Decode requires ModRM.reg is 4</summary>
    public const ulong REG4 = REG_ENABLE | (4ul << REG_OFFSET);
    /// <summary>Decode requires ModRM.reg is 5</summary>
    public const ulong REG5 = REG_ENABLE | (5ul << REG_OFFSET);
    /// <summary>Decode requires ModRM.reg is 6</summary>
    public const ulong REG6 = REG_ENABLE | (6ul << REG_OFFSET);
    /// <summary>Decode requires ModRM.reg is 7</summary>
    public const ulong REG7 = REG_ENABLE | (7ul << REG_OFFSET);

    #endregion

    #region [6] ModRM.Mod

    private const int MOD_OFFSET = 6;
    private const ulong MOD_ENABLE = 0b1ul << (MOD_OFFSET + MASKS_OFFSET);

    /// <summary>Decode requires ModRM.mod is b11 (register form)</summary>
    public const ulong MOD_REG = MOD_ENABLE | (0ul << MOD_OFFSET);
    /// <summary>Decode requires ModRM.mod is not b11 (memory form)</summary>
    public const ulong MOD_MEM = MOD_ENABLE | (1ul << MOD_OFFSET);

    #endregion

    // future: instruction set (16, 32, 64)

    // future: OSIZE (16, 32, 64)

    // future: ASIZE (16, 32, 64)

    // future: legacy SSE prefixes (NP, 66, F3, F2)

    // future: VLEN (128, 256, 512)

    // future: VEX.W bit

    // future: MOD_MEM_SIB (shorthand for MOD_MEM | RM4 | AS32)
}
