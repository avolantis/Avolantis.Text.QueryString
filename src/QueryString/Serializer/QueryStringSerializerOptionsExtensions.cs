using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Avolantis.Text.QueryString;

internal static class QueryStringSerializerOptionsExtensions
{
    private const string NoConverterErrorMessage = "Cannot find a query parameter converter for type";

    public static IQueryParameterConverter GetConverter(this QueryStringSerializerOptions options, MemberInfo member)
    {
        if (TryGetConverterFromAttribute(member, out var typeConverter))
            return typeConverter;

        return options.Converters
                   .Reverse()
                   .FirstOrDefault(converter => converter.CanConvert(member))
               ?? throw new InvalidOperationException($"{NoConverterErrorMessage} {member.GetActualType().Name}");
    }

    public static bool ShouldIgnoreString(this QueryStringSerializerOptions options, string value)
    {
        return options.EmptyStringOptions is QueryStringEmptyStringOptions.TreatEmptyAsNotPresent &&
               string.IsNullOrEmpty(value) ||
               options.EmptyStringOptions is QueryStringEmptyStringOptions.TreatWhitespaceAsNotPresent &&
               string.IsNullOrWhiteSpace(value);
    }

    public static bool ShouldWriteMaybeDefaultValue(this QueryStringSerializerOptions options,
        object? value, MemberInfo typeOrMember)
    {
        if (!TryGetIgnoreCondition(typeOrMember, out var condition))
            return !options.IgnoreNullValues || value != null;

        if (condition is QueryParameterIgnoreCondition.Never)
            return true;

        return condition switch
        {
            QueryParameterIgnoreCondition.Always => false,
            QueryParameterIgnoreCondition.WhenWritingNull when value == null => false,
            QueryParameterIgnoreCondition.WhenWritingDefault => value == null || IsDefault(value, value.GetType()),
            _ => true
        };
    }

    public static bool ShouldWriteNullValue(this QueryStringSerializerOptions options, MemberInfo? typeOrMember)
    {
        if (typeOrMember == null || !TryGetIgnoreCondition(typeOrMember, out var condition))
            return !options.IgnoreNullValues;

        return condition is QueryParameterIgnoreCondition.Never;
    }

    public static bool ShouldWriteStringAsNull(this QueryStringSerializerOptions options, string value)
    {
        return options.EmptyStringOptions is QueryStringEmptyStringOptions.TreatEmptyAsNull &&
               string.IsNullOrEmpty(value) ||
               options.EmptyStringOptions is QueryStringEmptyStringOptions.TreatWhitespaceAsNull &&
               string.IsNullOrWhiteSpace(value);
    }

    public static bool TryGetConverterFromAttribute(this MemberInfo typeOrMember,
        [NotNullWhen(true)] out IQueryParameterConverter? converter)
    {
        var attribute = typeOrMember.GetCustomAttribute<QueryStringConverterAttribute>();
        if (attribute == null)
        {
            converter = null;
            return false;
        }

        converter = attribute.Converter;
        return true;
    }

    private static bool IsDefault<T>(T? value)
    {
        return EqualityComparer<T>.Default.Equals(value, default);
    }

    private static bool IsDefault(object? value, Type type)
    {
        if (!type.IsValueType && value == null)
            return true;

        return (bool)typeof(QueryStringSerializerOptionsExtensions)
            .GetMethod(nameof(IsDefault))!
            .MakeGenericMethod(type)
            .Invoke(null, new[] { value })!;
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
