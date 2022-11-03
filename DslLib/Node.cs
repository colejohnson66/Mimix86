using JetBrains.Annotations;
using System.Text;

namespace DslLib;

/// <summary>
/// Represents a single node in the DSL.
/// </summary>
[PublicAPI]
public class Node
{
    /// <summary>
    /// The contents of this node, if it is textual; <c>null</c> otherwise.
    /// </summary>
    /// <remarks>If this is <c>null</c>, <see cref="Children" /> is non-<c>null</c>, and vice versa.</remarks>
    public string? Text { get; }

    /// <summary>
    /// The children of this node, if it is an array; <c>null</c> otherwise.
    /// </summary>
    /// <remarks>If this is <c>null</c>, <see cref="Text" /> is non-<c>null</c>, and vice versa.</remarks>
    public Node[]? Children { get; }

    internal Node(string contents)
    {
        Text = contents;
        Children = null;
    }

    internal Node(Node[] children)
    {
        Text = null;
        Children = children;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        if (Text is not null)
        {
            if (!Text.Contains(' ') && !Text.Contains('"'))
                return Text;

            string quoted = Text.Replace("\"", "\\\"");
            return $"\"{quoted}\"";
        }

        StringBuilder builder = new("[");
        for (int i = 0; i < Children!.Length; i++)
        {
            Node node = Children![i];
            builder.Append(node);

            if (i != Children.Length - 1)
                builder.Append(' ');
        }
        builder.Append(']');
        return builder.ToString();
    }
}
