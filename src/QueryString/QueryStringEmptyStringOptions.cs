namespace Avolantis.Text.QueryString;

/// <summary>
///     Specifies options for handling empty and whitespace-only strings
/// </summary>
public enum QueryStringEmptyStringOptions
{
    /// <summary>
    ///     Leave string values as-is
    /// </summary>
    Default,

    /// <summary>
    ///     Treat empty strings as null values
    /// </summary>
    /// <remarks>
    ///     <see cref="QueryStringSerializerOptions.IgnoreNullValues" /> will still control
    ///     whether the parameter will be added to the result
    /// </remarks>
    TreatEmptyAsNull,

    /// <summary>
    ///     Treat empty strings as if the parameter was not present
    /// </summary>
    TreatEmptyAsNotPresent,

    /// <summary>
    ///     Treat empty strings and string only of whitespace as null values
    /// </summary>
    /// <remarks>
    ///     <see cref="QueryStringSerializerOptions.IgnoreNullValues" /> will still control
    ///     whether the parameter will be added to the result
    /// </remarks>
    TreatWhitespaceAsNull,

    /// <summary>
    ///     Treat empty strings and strings only of whitespace as if the parameter was not present
    /// </summary>
    TreatWhitespaceAsNotPresent
}
