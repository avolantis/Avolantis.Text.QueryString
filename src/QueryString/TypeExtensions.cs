using System.Reflection;

namespace Avolantis.Text.QueryString;

internal static class TypeExtensions
{
    public static Type GetActualType(this MemberInfo typeOrMember)
    {
        return typeOrMember switch
        {
            PropertyInfo p => p.PropertyType,
            Type t => t,
            _ => throw new InvalidOperationException(
                $"Unable to determine type info for {typeOrMember.Name} from {typeOrMember.GetType().Name}")
        };
    }
}
