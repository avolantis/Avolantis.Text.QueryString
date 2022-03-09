namespace Avolantis.Text.QueryString;

internal class DateTimeQueryParameterConverter : QueryParameterConverter<DateTime>
{
    public static readonly DateTimeQueryParameterConverter Instance = new();

    private DateTimeQueryParameterConverter()
    {
    }

    public override void Convert(QueryStringWriter writer, DateTime value, QueryStringSerializerOptions options)
    {
        var result = options.AutoConvertToUtc ? value.ToUniversalTime() : value;
        writer.WriteString(result.ToString(options.DateTimeFormat));
    }
}
