using System.Reflection;

namespace Avolantis.Text.QueryString;

/// <summary>
///     Interface for query string parameter converters
/// </summary>
/// <remarks>
///     Use the provided <see cref="QueryParameterConverter" /> or
///     <see cref="QueryParameterConverter{T}" /> base classes, when appropriate
/// </remarks>
public interface IQueryParameterConverter
{
    /// <summary>
    ///     Returns whether this converter is can convert values of the given type or member
    /// </summary>
    /// <param name="typeOrMember">The type or member to be converted</param>
    /// <returns>Whether this converter is compatible with the given type or member</returns>
    public bool CanConvert(MemberInfo typeOrMember);

    /// <summary>
    ///     Converts an object to <see cref="QueryStringParameter" />(s)
    ///     using the given <see cref="QueryStringWriter" />
    /// </summary>
    /// <param name="writer">The <see cref="QueryStringWriter" /> to write with</param>
    /// <param name="value">The value of to be converted</param>
    /// <param name="typeOrMember">The type or member to be converted</param>
    /// <param name="options">Serialization options</param>
    public void Convert(QueryStringWriter writer, object value, MemberInfo typeOrMember,
        QueryStringSerializerOptions options);
}
