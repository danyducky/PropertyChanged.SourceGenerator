using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using PropertyChanged.SourceGenerator.Abstractions.Analyzers;
using PropertyChanged.SourceGenerator.Abstractions.Diagnostics;
using PropertyChanged.SourceGenerator.Infrastructure.Options;
using PropertyChanged.SourceGenerator.Models.Analyzers;
using PropertyChanged.SourceGenerator.Utils;

namespace PropertyChanged.SourceGenerator.Analyzers;

/// <summary>
/// Analyzer of <see cref="IPropertySymbol"/>.
/// </summary>
internal class PropertyAnalyzer : ISyntaxAnalyzer<IPropertySymbol, PropertyAnalysis>
{
    private readonly IEnumerable<FieldAnalysis> fields;
    private readonly FieldOptions fieldOptions;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="fields">Field analysis.</param>
    /// <param name="fieldOptions">Field options.</param>
    public PropertyAnalyzer(IEnumerable<FieldAnalysis> fields, FieldOptions fieldOptions)
    {
        this.fields = fields;
        this.fieldOptions = fieldOptions;
    }

    /// <inheritdoc/>
    public PropertyAnalysis Analyze(IPropertySymbol symbol, SemanticModel semanticModel, IDiagnosticsScope scope)
    {
        var propertyAnalysis = new PropertyAnalysis()
        {
            Name = symbol.Name,
            Type = SymbolUtils.GetType(symbol),
            Modifier = SymbolUtils.GetModifier(symbol),
        };

        var propertyFieldName = PropertyUtils.GetFieldName(symbol.Name, fieldOptions);
        var backingField = fields.FirstOrDefault(field => field.Name == propertyFieldName);
        if (backingField != null)
        {
            backingField.AssociatedProperty = propertyAnalysis;
            propertyAnalysis.BackingField = backingField;
        }

        return propertyAnalysis;
    }
}
