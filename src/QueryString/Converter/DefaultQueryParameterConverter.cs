using System.Reflection;

namespace Avolantis.Text.QueryString;

internal class DefaultQueryParameterConverter : QueryParameterConverter
{
    public static readonly DefaultQueryParameterConverter Instance = new();

    private DefaultQueryParameterConverter()
    {
    }

    public override bool CanConvert(Type type)
    {
        // A reference type that is not string
        return (type.IsClass || type.IsByRef) && type != typeof(string);
    }

    public override void Convert(QueryStringWriter writer, object value, Type type,
        QueryStringSerializerOptions options)
    {
        var properties = type
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.CanRead);

        foreach (var property in properties)
        {
            var name = property.GetCustomAttribute<QueryStringPropertyNameAttribute>()?.Name ?? property.Name;
            QueryStringSerializer.Serialize(writer.CreateChild(name), property.GetValue(value), property, options);
        }
    }
}
