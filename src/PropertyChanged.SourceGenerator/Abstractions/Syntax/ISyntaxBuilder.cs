using PropertyChanged.SourceGenerator.Abstractions.Analyzers;
using PropertyChanged.SourceGenerator.Abstractions.Models;

namespace PropertyChanged.SourceGenerator.Abstractions.Syntax;

/// <summary>
/// Syntax builder.
/// </summary>
public interface ISyntaxBuilder<TSyntaxNode, TAnalysis>
    where TSyntaxNode : SyntaxMetadata
    where TAnalysis : ISyntaxAnalysis
{
    /// <summary>
    /// Build syntax tree node.
    /// </summary>
    /// <param name="analysis">Syntax symbol node analysis.</param>
    /// <returns>Syntax tree node.</returns>
    TSyntaxNode Build(TAnalysis analysis);
}
