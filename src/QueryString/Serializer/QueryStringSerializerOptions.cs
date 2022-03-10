// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Avolantis.Text.QueryString;

/// <summary>
///     Specifies options for the serialization process
/// </summary>
public class QueryStringSerializerOptions
{
    /// <summary>
    ///     Gets or sets the default query string serializer options
    /// </summary>
    public static QueryStringSerializerOptions Default { get; set; } = new();

    /// <summary>
    ///     Gets or sets whether to print duplicate values during serialization
    /// </summary>
    public bool AllowDuplicateParameters { get; set; } = true;

    /// <summary>
    ///     Gets or sets whether to convert <see cref="DateTime" /> values to UTC
    /// </summary>
    public bool AutoConvertToUtc { get; set; } = true;

    /// <summary>
    ///     Gets or sets the format string for <see cref="DateOnly" /> values
    /// </summary>
    public string DateOnlyFormat { get; set; } = "YYYY-MM-dd";

    /// <summary>
    ///     Gets or sets the format string for <see cref="DateTime" /> values
    /// </summary>
    public string DateTimeFormat { get; set; } = "O";

    /// <summary>
    ///     Gets or sets the default precision of fractional numbers
    /// </summary>
    public int DefaultPrecision { get; set; } = 2;

    /// <summary>
    ///     Gets or sets the <see cref="QueryStringEmptyStringOptions" />
    /// </summary>
    public QueryStringEmptyStringOptions EmptyStringOptions { get; set; } = QueryStringEmptyStringOptions.Default;

    /// <summary>
    ///     Gets or sets whether to ignore <c>null</c> values during serialization
    /// </summary>
    public bool IgnoreNullValues { get; set; } = true;

    /// <summary>
    ///     Gets or sets the <see cref="QueryParameterKeyCasingPolicy" />
    /// </summary>
    public QueryParameterKeyCasingPolicy KeyCasingPolicy { get; set; } = QueryParameterKeyCasingPolicy.NoChange;

    /// <summary>
    ///     Gets or sets the parameter separator of the query string
    /// </summary>
    public string ParameterSeparator { get; set; } = "&";

    /// <summary>
    ///     Gets or sets the prefix of the query string
    /// </summary>
    public string Prefix { get; set; } = "?";

    /// <summary>
    ///     Gets or sets the default string comparer used when checking for duplicate parameters
    /// </summary>
    public StringComparer StringComparer { get; set; } = StringComparer.InvariantCultureIgnoreCase;

    /// <summary>
    ///     Gets or sets the format string for <see cref="TimeOnly" /> values
    /// </summary>
    public string TimeOnlyFormat { get; set; } = "HH:mm:sss";

    /// <summary>
    ///     Gets or sets whether to auto-trim string values
    /// </summary>
    public bool TrimStrings { get; set; } = false;

    /// <summary>
    ///     Gets or sets the available <see cref="IQueryParameterConverter" />s used to convert
    ///     CLR objects to their corresponding <see cref="QueryStringParameter" />s
    /// </summary>
    public ICollection<IQueryParameterConverter> Converters { get; set; } = new List<IQueryParameterConverter>
    {
        FormatStringQueryParameterConverter.Instance,
        GuidQueryParameterConverter.Instance,
        DateTimeQueryParameterConverter.Instance,
        DateOnlyQueryParameterConverter.Instance,
        TimeOnlyQueryParameterConverter.Instance
    };
}
