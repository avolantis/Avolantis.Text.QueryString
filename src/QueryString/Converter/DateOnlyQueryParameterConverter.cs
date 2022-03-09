namespace Avolantis.Text.QueryString;

internal class DateOnlyQueryParameterConverter : QueryParameterConverter<DateOnly>
{
    public static readonly DateOnlyQueryParameterConverter Instance = new();

    private DateOnlyQueryParameterConverter()
    {
    }

    public override void Convert(QueryStringWriter writer, DateOnly value, QueryStringSerializerOptions options)
    {
        writer.WriteString(value.ToString(options.DateOnlyFormat));
    }
}
