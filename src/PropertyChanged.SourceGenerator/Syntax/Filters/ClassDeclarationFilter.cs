﻿using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PropertyChanged.SourceGenerator.Abstractions.Syntax;

namespace PropertyChanged.SourceGenerator.Syntax.Filters;

/// <summary>
/// Partial class filter for <see cref="SyntaxManager"/>.
/// </summary>
internal class ClassDeclarationFilter : ISyntaxFilter<ClassDeclarationSyntax>
{
    /// <summary>
    /// Class modifier kinds.
    /// </summary>
    public SyntaxKind[] Modifiers { get; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="modifiers">Declaration modifier kinds.</param>
    public ClassDeclarationFilter(params SyntaxKind[] modifiers)
    {
        Modifiers = modifiers;
    }

    /// <inheritdoc/>
    public IncrementalValuesProvider<ClassDeclarationSyntax> Apply(IncrementalValuesProvider<ClassDeclarationSyntax> nodes)
        => nodes.Where(IsValid);

    /// <inheritdoc/>
    public bool IsValid(ClassDeclarationSyntax node)
    {
        var classModifiers = node.Modifiers.Select(m => m.Kind());
        var isPartial = classModifiers.Any(modifier => modifier == SyntaxKind.PartialKeyword);
        return isPartial && Modifiers.Any(classModifiers.Contains);
    }
}
