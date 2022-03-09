namespace Avolantis.Text.QueryString;

internal class TimeOnlyQueryParameterConverter : QueryParameterConverter<TimeOnly>
{
    public static readonly TimeOnlyQueryParameterConverter Instance = new();

    private TimeOnlyQueryParameterConverter()
    {
    }

    public override void Convert(QueryStringWriter writer, TimeOnly value, QueryStringSerializerOptions options)
    {
        writer.WriteString(value.ToString(options.TimeOnlyFormat));
    }
}
