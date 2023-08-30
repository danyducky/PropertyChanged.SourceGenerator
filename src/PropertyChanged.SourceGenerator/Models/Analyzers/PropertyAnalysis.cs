﻿namespace PropertyChanged.SourceGenerator.Models.Analyzers;

/// <summary>
/// Property analysis.
/// </summary>
public record PropertyAnalysis : MemberAnalysis
{
    /// <summary>
    /// Property backing field.
    /// </summary>
    public FieldAnalysis? BackingField { get; set; }
}
