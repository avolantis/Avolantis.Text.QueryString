using System.Reflection;

namespace Avolantis.Text.QueryString;

internal class FormatStringQueryParameterConverter : IQueryParameterConverter
{
    public static readonly FormatStringQueryParameterConverter Instance = new();

    private FormatStringQueryParameterConverter()
    {
    }

    public bool CanConvert(MemberInfo typeOrMember)
    {
        return typeOrMember.GetCustomAttribute<QueryStringFormatAttribute>() != null &&
               typeOrMember.GetActualType().IsAssignableTo(typeof(IFormattable));
    }

    public void Convert(QueryStringWriter writer, object value, MemberInfo typeOrMember,
        QueryStringSerializerOptions options)
    {
        var attribute = typeOrMember.GetCustomAttribute<QueryStringFormatAttribute>()!;
        writer.WriteString(attribute.Apply((IFormattable)value));
    }
}
