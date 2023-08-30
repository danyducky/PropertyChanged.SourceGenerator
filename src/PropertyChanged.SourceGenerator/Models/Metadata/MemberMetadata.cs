using System.Text;
using PropertyChanged.SourceGenerator.Abstractions.Models;
using PropertyChanged.SourceGenerator.Infrastructure.Indent;

namespace PropertyChanged.SourceGenerator.Models.Metadata;

/// <summary>
/// Member metadata.
/// </summary>
public class MemberMetadata : SyntaxMetadata
{
    /// <summary>
    /// Member name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Member type.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Member modifier.
    /// </summary>
    public string Modifier { get; set; } = MemberModifiers.Public;

    /// <summary>
    /// Is member nullable.
    /// </summary>
    public bool IsNullable { get; set; }

    /// <inheritdoc/>
    public override string Build(IndentWriter writer)
    {
        var builder = new StringBuilder();

        builder.Append(Modifier);
        builder.Append($" {Type}");

        if (IsNullable)
        {
            builder.Append("?");
        }

        builder.Append($" {Name};");

        var member = builder.ToString();
        return writer.Append(member).ToString();
    }
}
