using System.Reflection;

namespace Avolantis.Text.QueryString;

/// <summary>
///     Base class for implementing custom query string parameter converters
/// </summary>
/// <remarks>
///     Use this class when the converter supports multiple types, otherwise
///     inherit from <see cref="QueryParameterConverter{T}" />
/// </remarks>
public abstract class QueryParameterConverter : IQueryParameterConverter
{
    /// <inheritdoc />
    public virtual bool CanConvert(MemberInfo typeOrMember)
    {
        return CanConvert(typeOrMember.GetActualType());
    }

    /// <summary>
    ///     Returns whether this converter is can convert values of the given type
    /// </summary>
    /// <param name="type">The type to be converted</param>
    /// <returns>Whether this converter is compatible with the given type</returns>
    public abstract bool CanConvert(Type type);

    /// <inheritdoc />
    public virtual void Convert(QueryStringWriter writer, object value, MemberInfo typeOrMember,
        QueryStringSerializerOptions options)
    {
        Convert(writer, value, typeOrMember.GetActualType(), options);
    }

    /// <summary>
    ///     Converts an object to <see cref="QueryStringParameter" />(s)
    ///     using the given <see cref="QueryStringWriter" />
    /// </summary>
    /// <param name="writer">The <see cref="QueryStringWriter" /> to write with</param>
    /// <param name="value">The value of be converted</param>
    /// <param name="type">The type to be converted</param>
    /// <param name="options">Serialization options</param>
    public abstract void Convert(QueryStringWriter writer, object value, Type type,
        QueryStringSerializerOptions options);
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
    public override void Convert(QueryStringWriter writer, object value, Type type,
        QueryStringSerializerOptions options)
    {
        Convert(writer, (T)value, options);
    }

    /// <summary>
    ///     Converts an object of type <typeparamref name="T" /> to
    ///     <see cref="QueryStringParameter" />(s) using the given
    ///     <see cref="QueryStringWriter" />
    /// </summary>
    /// <param name="writer">The <see cref="QueryStringWriter" /> to write with</param>
    /// <param name="value">The value to be converted</param>
    /// <param name="options">Serialization options</param>
    public abstract void Convert(QueryStringWriter writer, T value, QueryStringSerializerOptions options);
}
