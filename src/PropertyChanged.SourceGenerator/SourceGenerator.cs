﻿using System.ComponentModel;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using PropertyChanged.SourceGenerator.Abstractions.Models;
using PropertyChanged.SourceGenerator.Analyzers;
using PropertyChanged.SourceGenerator.Builders;
using PropertyChanged.SourceGenerator.Diagnostics;
using PropertyChanged.SourceGenerator.Infrastructure.Indent;
using PropertyChanged.SourceGenerator.Infrastructure.Options;
using PropertyChanged.SourceGenerator.Models.Analyzers;
using PropertyChanged.SourceGenerator.Syntax;
using PropertyChanged.SourceGenerator.Syntax.Filters;

namespace PropertyChanged.SourceGenerator;

/// <summary>
/// Generate <b>partial class</b> for each class which implement <see cref="INotifyPropertyChanged"/> interface.
/// <br/>Generator will create the <i>public property</i> for each <i>private field</i>.
/// <br/>Generated <i>public property</i> will raise <see cref="INotifyPropertyChanged.PropertyChanged"/> event.
/// </summary>
[Generator(LanguageNames.CSharp)]
internal class SourceGenerator : IIncrementalGenerator
{
    /// <inheritdoc/>
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource("Attributes", Constants.Attributes));

        var options = context.AnalyzerConfigOptionsProvider.Select((opt, _) => opt);
        var syntaxManager = new SyntaxManager(context.SyntaxProvider);

        var partialClassFilter = new ClassDeclarationFilter(SyntaxKind.PublicKeyword, SyntaxKind.InternalKeyword);
        var classSymbols = syntaxManager.GetSymbols<ClassDeclarationSyntax, ITypeSymbol>(partialClassFilter);
        var classAnalysisAndDiagnostics = classSymbols.Combine(options).Select(GetAnalyzedNode);

        var diagnostics = classAnalysisAndDiagnostics.SelectMany((pair, token) => pair.Scope.GetDiagnostics());
        context.RegisterSourceOutput(diagnostics, Report);

        var analysis = classAnalysisAndDiagnostics
            .Select((pair, token) => (pair.Analysis, pair.Options))
            .Where(pair => pair.Analysis.ShouldBuild);
        context.RegisterSourceOutput(analysis, Build);
    }

    private static (ClassAnalysis Analysis, DiagnosticsScope Scope, OptionsManager Options) GetAnalyzedNode(
        (SyntaxSymbolNode<ITypeSymbol> Node, AnalyzerConfigOptionsProvider Options) provider,
        CancellationToken cancellationToken)
    {
        var scope = new DiagnosticsScope();
        var options = provider.Options.GetOptions(provider.Node.SemanticModel.SyntaxTree);
        var optionsManager = new OptionsManager(options, scope);

        if (provider.Node.Symbol is null)
        {
            return (Analysis: ClassAnalysis.Instance, Scope: scope, Options: optionsManager);
        }

        var analyzer = new ClassAnalyzer(optionsManager);
        var analysis = analyzer.Analyze(provider.Node.Symbol, provider.Node.SemanticModel, scope);

        return (Analysis: analysis, Scope: scope, Options: optionsManager);
    }

    private static void Report(SourceProductionContext context, Diagnostic diagnostic)
        => context.ReportDiagnostic(diagnostic);

    private static void Build(
        SourceProductionContext context,
        (ClassAnalysis Analysis, OptionsManager Options) provider)
    {
        var builder = new ClassBuilder(provider.Options);
        var node = builder.Build(provider.Analysis);
        var writer = new IndentWriter(provider.Options.IndentOptions);
        var symbol = node.Build(writer);
        context.AddSource($"{node.Name}.g.cs", symbol);
    }
}
