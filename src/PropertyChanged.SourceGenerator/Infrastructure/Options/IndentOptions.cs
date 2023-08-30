﻿using System;
using Microsoft.CodeAnalysis.Diagnostics;

namespace PropertyChanged.SourceGenerator.Infrastructure.Options;

/// <summary>
/// Options for user IDE indent settings.
/// </summary>
public class IndentOptions
{
    /// <summary>
    /// User indent style.
    /// </summary>
    public IndentStyle IndentStyle { get; } = IndentStyle.Space;

    /// <summary>
    /// User indent size.
    /// </summary>
    public int IndentSize { get; } = 4;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options">Analyzer options.</param>
    public IndentOptions(AnalyzerConfigOptions options)
    {
        if (options.TryGetValue(OptionNames.IndentStyleName, out var indentStyle))
        {
            if (Enum.TryParse<IndentStyle>(indentStyle, ignoreCase: true, out var style))
            {
                IndentStyle = style;
            }
        }

        if (options.TryGetValue(OptionNames.IndentSizeName, out var indentSize))
        {
            if (int.TryParse(indentSize, out var size))
            {
                IndentSize = size;
            }
        }
    }
}
