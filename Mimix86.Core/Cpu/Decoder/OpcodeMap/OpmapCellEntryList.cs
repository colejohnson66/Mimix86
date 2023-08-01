using System.Collections.Generic;

namespace Mimix86.Core.Cpu.Decoder.OpcodeMap;

/// <summary>
/// Represents a single cell in the opcode map, specifically for ones containing possible opcodes.
/// </summary>
public sealed class OpmapCellEntryList
{
    /// <summary>
    /// Get the flags for this opmap cell.
    /// </summary>
    public OpmapCellFlags Flags { get; init; }

    /// <summary>
    /// Get the entries in this list.
    /// </summary>
    public List<OpmapCellEntry> Instructions { get; } = new();
}
