using System.Threading;
using Microsoft.CodeAnalysis;
using PropertyChanged.SourceGenerator.Abstractions.Syntax;

namespace PropertyChanged.SourceGenerator.Abstractions.Models;

/// <summary>
/// Syntax symbol node.
/// </summary>
public class SyntaxSymbolNode<TSymbol> : ISyntaxNode<TSymbol>
    where TSymbol : class, ISymbol
{
    /// <inheritdoc/>
    public TSymbol? Symbol { get; }

    /// <inheritdoc/>
    public SemanticModel SemanticModel { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="context">Generator syntax context.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public SyntaxSymbolNode(GeneratorSyntaxContext context, CancellationToken cancellationToken = default)
    {
        Symbol = context.SemanticModel.GetDeclaredSymbol(context.Node, cancellationToken) as TSymbol;
        SemanticModel = context.SemanticModel;
    }
}
