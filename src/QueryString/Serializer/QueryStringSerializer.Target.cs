namespace Avolantis.Text.QueryString;

public static partial class QueryStringSerializer
{
    /// <summary>
    ///     Serializes the given object to a <see cref="QueryParameterCollection" />
    /// </summary>
    /// <param name="target">The <see cref="QueryParameterCollection" /> to add parameters to</param>
    /// <param name="value">The object to serialize</param>
    /// <param name="options">Serialization options</param>
    /// <typeparam name="T">The type of the object to serialize</typeparam>
    public static void MapTo<T>(QueryParameterCollection target, T? value, QueryStringSerializerOptions? options = null)
    {
        MapTo(target, value, typeof(T), options);
    }

    /// <summary>
    ///     Serializes the given object to a <see cref="QueryParameterCollection" />
    /// </summary>
    /// <param name="target">The <see cref="QueryParameterCollection" /> to add parameters to</param>
    /// <param name="value">The object to serialize</param>
    /// <param name="type">The type of the object to serialize</param>
    /// <param name="options">Serialization options</param>
    public static void MapTo(QueryParameterCollection target, object? value, Type type,
        QueryStringSerializerOptions? options = null)
    {
        options ??= QueryStringSerializerOptions.Default;
        var writer = new QueryStringWriter(target, options);
        Serialize(writer, value, type, options);
    }
}
