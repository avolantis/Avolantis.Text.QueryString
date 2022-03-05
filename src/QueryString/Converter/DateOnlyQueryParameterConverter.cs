namespace Avolantis.Text.QueryString;

internal class DateOnlyQueryParameterConverter : QueryParameterConverter<DateOnly>
{
    public static readonly DateOnlyQueryParameterConverter Instance = new();

    private DateOnlyQueryParameterConverter()
    {
    }

    protected override void Convert(string key, DateOnly value,
        QueryParameterCollection target, QueryStringSerializerOptions options)
    {
        target.Add(key, value.ToString(options.DateOnlyFormat));
    }
}
