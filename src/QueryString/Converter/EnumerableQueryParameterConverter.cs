using System.Collections;

namespace Avolantis.Text.QueryString;

internal class EnumerableQueryParameterConverter : QueryParameterConverter
{
    public static readonly EnumerableQueryParameterConverter Instance = new();

    private EnumerableQueryParameterConverter()
    {
    }

    public override bool CanConvert(Type type)
    {
        // Any enumerable that is not string (strings are IEnumerable<chars>-s)
        return type.IsAssignableTo(typeof(IEnumerable)) && type != typeof(string);
    }

    public override void Convert(QueryStringWriter writer, object value, Type type,
        QueryStringSerializerOptions options)
    {
        var itemType = type.IsArray
            ? type.GetElementType()!
            : type.GetGenericArguments().FirstOrDefault() ?? typeof(object);

        foreach (var item in (IEnumerable)value)
            QueryStringSerializer.Serialize(writer, item, itemType, options);
    }
}
