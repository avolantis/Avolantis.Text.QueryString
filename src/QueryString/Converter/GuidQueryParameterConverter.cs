using System.Reflection;

namespace Avolantis.Text.QueryString;

internal class GuidQueryParameterConverter : IQueryParameterConverter
{
    public static readonly GuidQueryParameterConverter Instance = new();

    private GuidQueryParameterConverter()
    {
    }

    public bool CanConvert(MemberInfo typeOrMember)
    {
        var type = typeOrMember as Type;
        type ??= typeOrMember.DeclaringType!;
        return type == typeof(Guid);
    }

    public void Convert(QueryStringWriter writer, object value, MemberInfo typeOrMember,
        QueryStringSerializerOptions options)
    {
        var guid = (Guid)value;

        if (guid == Guid.Empty && !options.ShouldWriteDefaultValue(typeOrMember))
            return;

        writer.WriteString(guid.ToString("D"));
    }
}
