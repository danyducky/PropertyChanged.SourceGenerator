using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using PropertyChanged.SourceGenerator.Abstractions.Diagnostics;
using PropertyChanged.SourceGenerator.Diagnostics.Reporters;
using PropertyChanged.SourceGenerator.Models.Descriptors;

namespace PropertyChanged.SourceGenerator.Diagnostics;

/// <summary>
/// Scope of <see cref="Diagnostic"/> descriptors.
/// </summary>
public class DiagnosticsScope : IDiagnosticsScope
{
    private static readonly WarningReporter warningReporter = new();
    private static readonly ErrorReporter errorReporter = new();

    private static Dictionary<Type, IDiagnostics> Reporters { get; } = new()
    {
        { typeof(WarningDescriptor), warningReporter },
        { typeof(ErrorDescriptor), errorReporter },
    };

    /// <inheritdoc/>
    public void AddDiagnostic<TDescriptor>(TDescriptor descriptor)
        where TDescriptor : IDiagnosticDescriptor
        => GetReporter<TDescriptor>().AddDiagnostic(descriptor);

    /// <inheritdoc/>
    public ImmutableArray<Diagnostic> GetDiagnostics<TDescriptor>()
        where TDescriptor : IDiagnosticDescriptor
        => GetReporter<TDescriptor>().Diagnostics;

    /// <inheritdoc/>
    public ImmutableArray<Diagnostic> GetDiagnostics()
        => Reporters.Values
            .SelectMany(reporter => reporter.Diagnostics)
            .ToImmutableArray();

    private IDiagnosticReporter<TDescriptor> GetReporter<TDescriptor>()
        where TDescriptor : IDiagnosticDescriptor
        => (IDiagnosticReporter<TDescriptor>)Reporters[typeof(TDescriptor)];
}
