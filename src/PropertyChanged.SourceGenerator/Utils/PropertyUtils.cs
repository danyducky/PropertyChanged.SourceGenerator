﻿using PropertyChanged.SourceGenerator.Infrastructure.Options;

namespace PropertyChanged.SourceGenerator.Utils;

/// <summary>
/// Property utilities.
/// </summary>
internal class PropertyUtils
{
    /// <summary>
    /// Gets possible property field name.
    /// </summary>
    /// <param name="propertyName">Property name.</param>
    /// <param name="options">Field options.</param>
    /// <returns>Property field name.</returns>
    public static string GetFieldName(string propertyName, FieldOptions options)
    {
        var name = NamingUtils.ToConvention(propertyName, options.Convention);

        if (options.UseUnderscore || PascalCaseConvention(options))
        {
            name = '_' + name;
        }

        return name;
    }

    private static bool PascalCaseConvention(FieldOptions options)
        => options.Convention == NamingConvention.PascalCase;
}
