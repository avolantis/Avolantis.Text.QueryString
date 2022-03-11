namespace Avolantis.Text.QueryString;

/// <summary>
///     Specifies whether to adjust casing of <see cref="QueryStringParameter" /> keys
///     while serializing objects to <see cref="QueryParameterCollection" />s
/// </summary>
public enum QueryParameterKeyCasingPolicy
{
    /// <summary>
    ///     Leve casing as-is
    /// </summary>
    NoChange,

    /// <summary>
    ///     Change casing to camelCase
    /// </summary>
    CamelCase,

    /// <summary>
    ///     Change casing to PascalCase
    /// </summary>
    PascalCase,

    /// <summary>
    ///     Change casing to lowercase
    /// </summary>
    LowerCase,

    /// <summary>
    ///     Change casing to UPPERCASE
    /// </summary>
    UpperCase
}
