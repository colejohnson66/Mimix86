/* =============================================================================
 * File:   Opcode.cs
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

namespace Mimix86.Core.Cpu.Decoder;

/// <summary>
/// Contains all opcodes known the Mimix86.
/// </summary>
[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public enum Opcode
{
    /// <summary>Undefined opcode.</summary>
    Error,

    #region 8086+ Instructions

    /// <summary> </summary>
    Aaa,

    /// <summary> </summary>
    Aad,

    /// <summary> </summary>
    Aam,

    /// <summary> </summary>
    Aas,

    /// <summary> </summary>
    AdcALIb,
    /// <summary> </summary>
    AdcAXIw,
    /// <summary> </summary>
    AdcEbGb,
    /// <summary> </summary>
    AdcEbIb,
    /// <summary> </summary>
    AdcEwGw,
    /// <summary> </summary>
    AdcEwIb,
    /// <summary> </summary>
    AdcEwIw,
    /// <summary> </summary>
    AdcGbEb,
    /// <summary> </summary>
    AdcGwEw,

    /// <summary> </summary>
    AddALIb,
    /// <summary> </summary>
    AddAXIw,
    /// <summary> </summary>
    AddEbGb,
    /// <summary> </summary>
    AddEbIb,
    /// <summary> </summary>
    AddEwGw,
    /// <summary> </summary>
    AddEwIb,
    /// <summary> </summary>
    AddEwIw,
    /// <summary> </summary>
    AddGbEb,
    /// <summary> </summary>
    AddGwEw,

    /// <summary> </summary>
    AndALIb,
    /// <summary> </summary>
    AndAXIw,
    /// <summary> </summary>
    AndEbGb,
    /// <summary> </summary>
    AndEbIb,
    /// <summary> </summary>
    AndEwGw,
    /// <summary> </summary>
    AndEwIb,
    /// <summary> </summary>
    AndEwIw,
    /// <summary> </summary>
    AndGbEb,
    /// <summary> </summary>
    AndGwEw,

    /// <summary> </summary>
    CallEw,
    /// <summary> </summary>
    CallJz,
    /// <summary> </summary>
    CallMpww,

    /// <summary> </summary>
    Clc,

    /// <summary> </summary>
    Cld,

    /// <summary> </summary>
    Cli,

    /// <summary> </summary>
    Cmc,

    /// <summary> </summary>
    CmpALIb,
    /// <summary> </summary>
    CmpAXIw,
    /// <summary> </summary>
    CmpEbGb,
    /// <summary> </summary>
    CmpEbIb,
    /// <summary> </summary>
    CmpEwGw,
    /// <summary> </summary>
    CmpEwIb,
    /// <summary> </summary>
    CmpEwIw,
    /// <summary> </summary>
    CmpGbEb,
    /// <summary> </summary>
    CmpGwEw,

    /// <summary> </summary>
    Cmpsb,
    /// <summary> </summary>
    Cmpsw,

    /// <summary> </summary>
    Daa,

    /// <summary> </summary>
    Das,

    /// <summary> </summary>
    DecEb,
    /// <summary> </summary>
    DecEw,
    /// <summary> </summary>
    DecZw,

    /// <summary> </summary>
    DivEbIb,
    /// <summary> </summary>
    DivEwIw,

    /// <summary> </summary>
    Hlt,

    /// <summary> </summary>
    IdivEbIb,
    /// <summary> </summary>
    IdivEwIw,

    /// <summary> </summary>
    ImulEbIb,
    /// <summary> </summary>
    ImulEwIw,

    /// <summary> </summary>
    InALDX,
    /// <summary> </summary>
    InALIb,
    /// <summary> </summary>
    InAXDX,
    /// <summary> </summary>
    InAXIb,

    /// <summary> </summary>
    IncEb,
    /// <summary> </summary>
    IncEw,
    /// <summary> </summary>
    IncZw,

    /// <summary> </summary>
    Int3,

    /// <summary> </summary>
    IntIb,

    /// <summary> </summary>
    Into,

    /// <summary> </summary>
    Iret,

    /// <summary> </summary>
    JccJb,

    /// <summary> </summary>
    JcxzJb,

    /// <summary> </summary>
    JmpApww,
    /// <summary> </summary>
    JmpEw,
    /// <summary> </summary>
    JmpJb,
    /// <summary> </summary>
    JmpJz,
    /// <summary> </summary>
    JmpMpww,

    /// <summary> </summary>
    LdsGwMw,
    /// <summary> </summary>
    LeaGwM,
    /// <summary> </summary>
    LesGwMw,

    /// <summary> </summary>
    Lodsb,
    /// <summary> </summary>
    Lodsw,

    /// <summary> </summary>
    MovALOb,
    /// <summary> </summary>
    MovAXOw,
    /// <summary> </summary>
    MovEbGb,
    /// <summary> </summary>
    MovEbIb,
    /// <summary> </summary>
    MovEwGw,
    /// <summary> </summary>
    MovEwIw,
    /// <summary> </summary>
    MovEwSw,
    /// <summary> </summary>
    MovGbEb,
    /// <summary> </summary>
    MovGwEw,
    /// <summary> </summary>
    MovObAL,
    /// <summary> </summary>
    MovOwAX,
    /// <summary> </summary>
    MovSwGw,
    /// <summary> </summary>
    MovZbIb,
    /// <summary> </summary>
    MovZwIw,

    /// <summary> </summary>
    Movsb,
    /// <summary> </summary>
    Movsw,

    /// <summary> </summary>
    MulEbIb,
    /// <summary> </summary>
    MulEwIw,

    /// <summary> </summary>
    NegEbIb,
    /// <summary> </summary>
    NegEwIw,

    /// <summary> </summary>
    NotEbIb,
    /// <summary> </summary>
    NotEwIw,

    /// <summary> </summary>
    OrALIb,
    /// <summary> </summary>
    OrAXIw,
    /// <summary> </summary>
    OrEbGb,
    /// <summary> </summary>
    OrEbIb,
    /// <summary> </summary>
    OrEwGw,
    /// <summary> </summary>
    OrEwIb,
    /// <summary> </summary>
    OrEwIw,
    /// <summary> </summary>
    OrGbEb,
    /// <summary> </summary>
    OrGwEw,

    /// <summary> </summary>
    OutDXAL,
    /// <summary> </summary>
    OutDXAX,
    /// <summary> </summary>
    OutIbAL,
    /// <summary> </summary>
    OutIbAW,

    /// <summary> </summary>
    PopEw,
    /// <summary> </summary>
    PopZw,

    /// <summary> </summary>
    PopCS,
    /// <summary> </summary>
    PopDS,
    /// <summary> </summary>
    PopES,
    /// <summary> </summary>
    PopSS,

    /// <summary> </summary>
    PushEw,
    /// <summary> </summary>
    PushZw,

    /// <summary> </summary>
    PushCS,
    /// <summary> </summary>
    PushDS,
    /// <summary> </summary>
    PushES,
    /// <summary> </summary>
    PushSS,

    /// <summary> </summary>
    RclEb1,
    /// <summary> </summary>
    RclEbIb,
    /// <summary> </summary>
    RclEw1,
    /// <summary> </summary>
    RclEwIb,

    /// <summary> </summary>
    RcrEb1,
    /// <summary> </summary>
    RcrEbIb,
    /// <summary> </summary>
    RcrEw1,
    /// <summary> </summary>
    RcrEwIb,

    /// <summary> </summary>
    Ret,
    /// <summary> </summary>
    RetIw,

    /// <summary> </summary>
    Retf,
    /// <summary> </summary>
    RetfIw,

    /// <summary> </summary>
    RolEb1,
    /// <summary> </summary>
    RolEbIb,
    /// <summary> </summary>
    RolEw1,
    /// <summary> </summary>
    RolEwIb,

    /// <summary> </summary>
    RorEb1,
    /// <summary> </summary>
    RorEbIb,
    /// <summary> </summary>
    RorEw1,
    /// <summary> </summary>
    RorEwIb,

    /// <summary> </summary>
    Salc,

    /// <summary> </summary>
    SarEb1,
    /// <summary> </summary>
    SarEbIb,
    /// <summary> </summary>
    SarEw1,
    /// <summary> </summary>
    SarEwIb,

    /// <summary> </summary>
    SbbALIb,
    /// <summary> </summary>
    SbbAXIw,
    /// <summary> </summary>
    SbbEbGb,
    /// <summary> </summary>
    SbbEbIb,
    /// <summary> </summary>
    SbbEwGw,
    /// <summary> </summary>
    SbbEwIb,
    /// <summary> </summary>
    SbbEwIw,
    /// <summary> </summary>
    SbbGbEb,
    /// <summary> </summary>
    SbbGwEw,

    /// <summary> </summary>
    Scasb,
    /// <summary> </summary>
    Scasw,

    /// <summary> </summary>
    ShlEb1,
    /// <summary> </summary>
    ShlEbIb,
    /// <summary> </summary>
    ShlEw1,
    /// <summary> </summary>
    ShlEwIb,

    /// <summary> </summary>
    ShrEb1,
    /// <summary> </summary>
    ShrEbIb,
    /// <summary> </summary>
    ShrEw1,
    /// <summary> </summary>
    ShrEwIb,

    /// <summary> </summary>
    Stc,

    /// <summary> </summary>
    Std,

    /// <summary> </summary>
    Sti,

    /// <summary> </summary>
    Stosb,
    /// <summary> </summary>
    Stosw,

    /// <summary> </summary>
    SubALIb,
    /// <summary> </summary>
    SubAXIw,
    /// <summary> </summary>
    SubEbGb,
    /// <summary> </summary>
    SubEbIb,
    /// <summary> </summary>
    SubEwGw,
    /// <summary> </summary>
    SubEwIb,
    /// <summary> </summary>
    SubEwIw,
    /// <summary> </summary>
    SubGbEb,
    /// <summary> </summary>
    SubGwEw,

    /// <summary> </summary>
    TestALIb,
    /// <summary> </summary>
    TestAXIw,
    /// <summary> </summary>
    TestEbGb,
    /// <summary> </summary>
    TestEbIb,
    /// <summary> </summary>
    TestEwGw,
    /// <summary> </summary>
    TestEwIw,

    /// <summary> </summary>
    XchgAXZw,
    /// <summary> </summary>
    XchgEbGb,
    /// <summary> </summary>
    XchgEwGw,

    /// <summary> </summary>
    Xlat,

    /// <summary> </summary>
    XorALIb,
    /// <summary> </summary>
    XorAXIw,
    /// <summary> </summary>
    XorEbGb,
    /// <summary> </summary>
    XorEbIb,
    /// <summary> </summary>
    XorEwGw,
    /// <summary> </summary>
    XorEwIb,
    /// <summary> </summary>
    XorEwIw,
    /// <summary> </summary>
    XorGbEb,
    /// <summary> </summary>
    XorGwEw,

    // TODO: esc [D8-DF]
    // TODO: loopnz/loopz/loop

    #endregion
}
