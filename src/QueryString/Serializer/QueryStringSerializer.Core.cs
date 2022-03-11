// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

using System.Reflection;

namespace Avolantis.Text.QueryString;

/// <summary>
///     Methods of this class can be used to convert CLR objects to query strings
///     or <see cref="QueryParameterCollection" /> instances
/// </summary>
public static partial class QueryStringSerializer
{
    /// <summary>
    ///     Serializes the given object using the given <see cref="QueryStringWriter" />
    /// </summary>
    /// <param name="writer">The <see cref="QueryStringWriter" /> to write parameters with</param>
    /// <param name="value">The object to serialize</param>
    /// <param name="options">Serialization options</param>
    /// <typeparam name="T">The type of the object to serialize</typeparam>
    public static void Serialize<T>(QueryStringWriter writer, object? value,
        QueryStringSerializerOptions? options = null)
    {
        Serialize(writer, value, typeof(T), options);
    }

    /// <summary>
    ///     Serializes the given object using the given <see cref="QueryStringWriter" />
    /// </summary>
    /// <param name="writer">The <see cref="QueryStringWriter" /> to write parameters with</param>
    /// <param name="value">The object to serialize</param>
    /// <param name="typeOrMember">The member info or type of the object to serialize</param>
    /// <param name="options">Serialization options</param>
    public static void Serialize(QueryStringWriter writer, object? value, MemberInfo typeOrMember,
        QueryStringSerializerOptions? options = null)
    {
        options ??= QueryStringSerializerOptions.Default;

        if (!options.ShouldWriteMaybeDefaultValue(value, typeOrMember))
            return;

        switch (value)
        {
            case null:
                writer.WriteNull();
                return;
            case string str when options.ShouldIgnoreString(str):
                // Do nothing
                return;
            case string str when options.ShouldWriteStringAsNull(str):
                if (options.ShouldWriteNullValue(typeOrMember))
                    writer.WriteNull();
                return;
            case string str:
                writer.WriteString(str);
                return;
        }

        var type = typeOrMember.GetActualType();

        // null-s are previously handled, so we no longer
        // care about the possible Nullable<> wrapper
        type = Nullable.GetUnderlyingType(type) ?? type;

        if (type.IsPrimitive)
        {
            if (type == typeof(bool))
            {
                writer.WriteBoolean((bool)value);
                return;
            }

            // Must be a numeric at this point
            writer.WriteNumeric(value);
            return;
        }

        // Check if this is a reference type
        if (type.IsByRef)
            writer.WriteRef(value); // If so, prevent circular references

        var converter = options.GetConverter(typeOrMember);
        converter.Convert(writer, value, typeOrMember, options);
    }
}
