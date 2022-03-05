namespace Avolantis.Text.QueryString;

internal class DateTimeQueryParameterConverter : QueryParameterConverter<DateTime>
{
    public static readonly DateTimeQueryParameterConverter Instance = new();

    private DateTimeQueryParameterConverter()
    {
    }

    protected override void Convert(string key, DateTime value,
        QueryParameterCollection target, QueryStringSerializerOptions options)
    {
        var result = options.AutoConvertToUtc ? value.ToUniversalTime() : value;
        target.Add(key, result.ToString(options.DateTimeFormat));
    }
}
