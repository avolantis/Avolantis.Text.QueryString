namespace Avolantis.Text.QueryString;

/// <summary>
///     Specifies the property name to write when serializing
///     this property to a query string
/// </summary>
/// <remarks>
///     The <see cref="QueryParameterKeyCasingPolicy" /> is ignored,
///     when a custom parameter name is set with this attribute
/// </remarks>
[AttributeUsage(AttributeTargets.Property)]
public class QueryStringPropertyNameAttribute : Attribute
{
    /// <summary>
    ///     Specifies the property name to write when serializing
    ///     this property to a query string
    /// </summary>
    /// <param name="name">The desired name of the property</param>
    /// <remarks>
    ///     The <see cref="QueryParameterKeyCasingPolicy" /> is ignored,
    ///     when a custom parameter name is set with this attribute
    /// </remarks>
    public QueryStringPropertyNameAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    ///     Gets the name of the property
    /// </summary>
    public string Name { get; }
}
