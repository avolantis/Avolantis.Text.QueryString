namespace Avolantis.Text.QueryString;

internal class TimeOnlyQueryParameterConverter : QueryParameterConverter<TimeOnly>
{
    public static readonly TimeOnlyQueryParameterConverter Instance = new();

    private TimeOnlyQueryParameterConverter()
    {
    }

    protected override void Convert(string key, TimeOnly value,
        QueryParameterCollection target, QueryStringSerializerOptions options)
    {
        target.Add(key, value.ToString(options.TimeOnlyFormat));
    }
}
