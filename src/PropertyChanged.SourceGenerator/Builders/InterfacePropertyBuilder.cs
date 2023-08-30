using System;
using PropertyChanged.SourceGenerator.Abstractions.Syntax;
using PropertyChanged.SourceGenerator.Models.Analyzers;
using PropertyChanged.SourceGenerator.Models.Metadata;

namespace PropertyChanged.SourceGenerator.Builders;

/// <summary>
/// Builder for <see cref="InterfaceAnalysis"/>.
/// </summary>
public class InterfacePropertyBuilder : ISyntaxBuilder<PropertyMetadata, InterfaceAnalysis>
{
    private readonly string propertyName;
    private readonly Type propertyType;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    /// <param name="propertyType">Property type.</param>
    public InterfacePropertyBuilder(string propertyName, Type propertyType)
    {
        this.propertyName = propertyName;
        this.propertyType = propertyType;
    }

    /// <inheritdoc/>
    public PropertyMetadata Build(InterfaceAnalysis analysis)
    {
        var method = new PropertyMetadata()
        {
            Name = propertyName,
            Type = propertyType.Name,
            Modifier = MemberModifiers.Public,
            IsDelegate = true,
            IsNullable = true,
        };
        return method;
    }
}
