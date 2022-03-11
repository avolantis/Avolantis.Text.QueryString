namespace Avolantis.Text.QueryString;

public static partial class QueryStringSerializer
{
    /// <summary>
    ///     Serializes the given object to a <see cref="QueryParameterCollection" />
    /// </summary>
    /// <param name="value">The object to serialize</param>
    /// <param name="options">Serialization options</param>
    /// <typeparam name="T">The type of the object to serialize</typeparam>
    /// <returns>The constructed <see cref="QueryParameterCollection" /></returns>
    public static QueryParameterCollection Map<T>(T? value, QueryStringSerializerOptions? options = null)
    {
        return Map(value, typeof(T), options);
    }

    /// <summary>
    ///     Serializes the given object to a <see cref="QueryParameterCollection" />
    /// </summary>
    /// <param name="value">The object to serialize</param>
    /// <param name="type">The type of the object to serialize</param>
    /// <param name="options">Serialization options</param>
    /// <returns>The constructed <see cref="QueryParameterCollection" /></returns>
    public static QueryParameterCollection Map(object? value, Type type, QueryStringSerializerOptions? options = null)
    {
        var target = new QueryParameterCollection();
        MapTo(target, value, type, options);
        return target;
    }
}
