namespace Avolantis.Text.QueryString;

/// <summary>
///     Specifies the conditions for skipping a parameter during serialization
/// </summary>
public enum QueryParameterIgnoreCondition
{
    /// <summary>
    ///     Never serialize this parameter
    /// </summary>
    Never,

    /// <summary>
    ///     Always serialize this parameter
    /// </summary>
    Always,

    /// <summary>
    ///     Skip this parameter when the value is <c>null</c>
    /// </summary>
    WhenWritingNull,

    /// <summary>
    ///     Skip this parameter when the value is <c>default</c>
    /// </summary>
    WhenWritingDefault
}
