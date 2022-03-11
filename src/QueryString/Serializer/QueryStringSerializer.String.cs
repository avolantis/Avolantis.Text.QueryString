namespace Avolantis.Text.QueryString;

public static partial class QueryStringSerializer
{
    /// <summary>
    ///     Serializes the given object to a query string
    /// </summary>
    /// <param name="value">The object to serialize</param>
    /// <param name="options">Serialization options</param>
    /// <typeparam name="T">The type of the object to serialize</typeparam>
    /// <returns>The URL-encoded query string</returns>
    public static string Serialize<T>(T? value, QueryStringSerializerOptions? options = null)
    {
        return Serialize(value, typeof(T), options);
    }

    /// <summary>
    ///     Serializes the given object to a query string
    /// </summary>
    /// <param name="value">The object to serialize</param>
    /// <param name="type">The type of the object to serialize</param>
    /// <param name="options">Serialization options</param>
    /// <returns>The URL-encoded query string</returns>
    public static string Serialize(object? value, Type type, QueryStringSerializerOptions? options = null)
    {
        var target = new QueryParameterCollection();
        MapTo(target, value, type, options);
        options ??= QueryStringSerializerOptions.Default;
        return target.ToString(options.IgnoreNullValues, options.ParameterSeparator, options.Prefix);
    }
}
