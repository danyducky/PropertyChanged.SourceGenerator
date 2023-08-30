using Microsoft.CodeAnalysis;
using PropertyChanged.SourceGenerator.Models.Descriptors;

namespace PropertyChanged.SourceGenerator.Diagnostics.Reporters;

/// <summary>
/// <see cref="ErrorDescriptor"/> reporter.
/// </summary>
internal class ErrorReporter : ReporterBase<ErrorDescriptor>
{
    /// <inheritdoc/>
    public override void AddDiagnostic(ErrorDescriptor descriptor)
    {
        var diagnosticDescriptor = new DiagnosticDescriptor(
            descriptor.Code,
            descriptor.Title,
            descriptor.Message,
            "",
            descriptor.Severity,
            isEnabledByDefault: true,
            customTags: new[] { WellKnownDiagnosticTags.NotConfigurable });
        var diagnostic = Diagnostic.Create(diagnosticDescriptor, Location.None);
        AddDiagnostic(diagnostic);
    }
}
