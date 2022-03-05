namespace Avolantis.Text.QueryString;

/// <summary>
///     Base class for implementing custom query string parameter converters
/// </summary>
/// <remarks>
///     Use this class when the converter supports multiple types, otherwise
///     inherit from <see cref="QueryParameterConverter{T}" />
/// </remarks>
public abstract class QueryParameterConverter
{
    /// <summary>
    ///     Returns whether this converter is can convert values of the given type
    /// </summary>
    /// <param name="type">The type to be converted</param>
    /// <returns>Whether this converter is compatible with the given type</returns>
    public abstract bool CanConvert(Type type);

    /// <summary>
    ///     Converts an object to <see cref="QueryStringParameter" />(s) and adds
    ///     such parameter(s) to the given <see cref="QueryParameterCollection" />
    /// </summary>
    /// <param name="key">The key of the parameter</param>
    /// <param name="value">The value of the parameter</param>
    /// <param name="type">The type of the object to convert</param>
    /// <param name="target">The target <see cref="QueryParameterCollection" /></param>
    /// <param name="options">Serialization options</param>
    public abstract void Convert(string key, object? value, Type type,
        QueryParameterCollection target, QueryStringSerializerOptions options);
}

/// <summary>
///     Base class for implementing custom query string parameter converters
/// </summary>
/// <typeparam name="T">The type of the converted object</typeparam>
public abstract class QueryParameterConverter<T> : QueryParameterConverter
{
    /// <inheritdoc />
    public override bool CanConvert(Type type)
    {
        return ReferenceEquals(typeof(T), type);
    }

    /// <inheritdoc />
    public override void Convert(string key, object? value, Type type, QueryParameterCollection target,
        QueryStringSerializerOptions options)
    {
        if (!CanConvert(type))
            throw new InvalidOperationException($"Unsupported type {type.AssemblyQualifiedName}");

        Convert(key, (T?)value, target, options);
    }

    /// <summary>
    ///     Converts an object of type <typeparamref name="T" /> to
    ///     <see cref="QueryStringParameter" />(s) and adds such parameter(s)
    ///     to the given <see cref="QueryParameterCollection" />
    /// </summary>
    /// <param name="key">The key of the parameter</param>
    /// <param name="value">The value of the parameter</param>
    /// <param name="target">The target <see cref="QueryParameterCollection" /></param>
    /// <param name="options">Serialization options</param>
    protected abstract void Convert(string key, T? value, QueryParameterCollection target,
        QueryStringSerializerOptions options);
}
