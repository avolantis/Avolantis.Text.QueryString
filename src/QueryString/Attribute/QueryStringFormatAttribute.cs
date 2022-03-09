using System.Globalization;

namespace Avolantis.Text.QueryString;

/// <summary>
///     Apply the given <see cref="Format" /> and <see cref="Provider" />
///     when serializing an object of type <see cref="IFormattable" />
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class QueryStringFormatAttribute : Attribute
{
    /// <summary>
    ///     Apply the given <see cref="Format" /> and <see cref="Provider" />
    ///     when serializing an object of type <see cref="IFormattable" />
    /// </summary>
    /// <param name="format">The format to use when serializing this type or property</param>
    public QueryStringFormatAttribute(string format)
    {
        Format = format;
    }

    /// <summary>
    ///     Gets the format to use when serializing this type or property
    /// </summary>
    public string Format { get; }

    /// <summary>
    ///     Gets or sets the format provider to use when serializing this type or property
    /// </summary>
    public IFormatProvider? Provider { get; set; }

    internal string Apply(IFormattable value)
    {
        return value.ToString(Format, Provider ?? CultureInfo.InvariantCulture);
    }
}
