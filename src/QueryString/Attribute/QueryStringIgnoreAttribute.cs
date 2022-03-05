// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Avolantis.Text.QueryString;

/// <summary>
///     Specifies the condition for ignoring a type or property during serialization
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class QueryStringIgnoreAttribute : Attribute
{
    /// <summary>
    ///     The condition
    /// </summary>
    public QueryParameterIgnoreCondition Condition { get; init; }
}
