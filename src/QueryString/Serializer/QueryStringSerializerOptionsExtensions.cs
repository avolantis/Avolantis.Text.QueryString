using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Avolantis.Text.QueryString;

internal static class QueryStringSerializerOptionsExtensions
{
    public static bool ShouldWriteDefaultValue(this QueryStringSerializerOptions options, MemberInfo? typeOrMember)
    {
        if (typeOrMember == null || !TryGetIgnoreCondition(typeOrMember, out var condition))
            return !options.IgnoreNullValues;

        return condition is not (QueryParameterIgnoreCondition.Always
            or QueryParameterIgnoreCondition.WhenWritingDefault);
    }

    private static bool TryGetIgnoreCondition(MemberInfo typeOrMember,
        [NotNullWhen(true)] out QueryParameterIgnoreCondition? condition)
    {
        var attribute = typeOrMember.GetCustomAttribute<QueryStringIgnoreAttribute>();
        if (attribute == null)
        {
            condition = null;
            return false;
        }

        condition = attribute.Condition;
        return true;
    }
}
