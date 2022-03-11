namespace Avolantis.Text.QueryString;

internal class QueryStringWriterOptions
{
    public bool AllowDuplicateParameters { get; set; }
    public int DefaultPrecision { get; set; }
    public QueryParameterKeyCasingPolicy KeyCasingPolicy { get; set; }
    public bool TrimStrings { get; set; }

    public static implicit operator QueryStringWriterOptions(QueryStringSerializerOptions options)
    {
        return new QueryStringWriterOptions
        {
            AllowDuplicateParameters = options.AllowDuplicateParameters,
            DefaultPrecision = options.DefaultPrecision,
            KeyCasingPolicy = options.KeyCasingPolicy,
            TrimStrings = options.TrimStrings
        };
    }
}
