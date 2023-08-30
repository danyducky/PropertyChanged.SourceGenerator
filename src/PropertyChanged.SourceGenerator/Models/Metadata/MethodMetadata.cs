﻿using System.Collections.Generic;
using System.Text;
using PropertyChanged.SourceGenerator.Abstractions.Models;
using PropertyChanged.SourceGenerator.Infrastructure.Indent;
using PropertyChanged.SourceGenerator.Models.Metadata;

namespace PropertyChanged.SourceGenerator.Models.Nodes;

/// <summary>
/// Method metadata.
/// </summary>
public class MethodMetadata : SyntaxMetadata
{
    /// <summary>
    /// Method name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Method modifier.
    /// </summary>
    public string Modifier { get; set; } = MemberModifiers.Public;

    /// <summary>
    /// Method parameters.
    /// </summary>
    public IList<MethodParameterMetadata> Parameters { get; } = new List<MethodParameterMetadata>();

    /// <summary>
    /// Method invocations.
    /// </summary>
    public IList<InvocationMethodMetadata> Invocations { get; } = new List<InvocationMethodMetadata>();

    /// <inheritdoc/>
    public override string Build(IndentWriter writer)
    {
        var builder = new StringBuilder();

        builder.Append(Modifier);
        builder.Append($" void {Name}");
        builder.Append("(");

        foreach (var parameter in Parameters)
        {
            parameter.Build(builder);
        }

        builder.Append(")");

        var declaration = builder.ToString();
        writer.Append(declaration).AppendLine();

        writer.Append("{").AppendLine();

        using (writer.IncreaseIndent())
        {
            foreach (var invocation in Invocations)
            {
                invocation.Build(writer);
                writer.AppendLine();
            }
        }

        writer.Append("}").AppendLine();

        return writer.ToString();
    }
}
