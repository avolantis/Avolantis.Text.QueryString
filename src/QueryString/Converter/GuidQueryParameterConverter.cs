namespace Avolantis.Text.QueryString;

internal class GuidQueryParameterConverter : QueryParameterConverter<Guid>
{
    public static readonly GuidQueryParameterConverter Instance = new();

    private GuidQueryParameterConverter()
    {
    }

    public override bool CanConvert(Type type)
    {
        return type == typeof(Guid);
    }

    public override void Convert(QueryStringWriter writer, Guid value, QueryStringSerializerOptions options)
    {
        writer.WriteString(value.ToString("D"));
    }
}
