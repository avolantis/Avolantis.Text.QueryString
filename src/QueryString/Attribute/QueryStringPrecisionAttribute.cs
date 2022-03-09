using System.Globalization;

namespace Avolantis.Text.QueryString;

/// <summary>
///     Specifies the precision of the fractional part for
///     serializing a floating-point number
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class QueryStringPrecisionAttribute : Attribute
{
    /// <summary>
    ///     Specifies the precision of the fractional part for
    ///     serializing a floating-point number
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if <paramref name="precision" /> is less than zero
    ///     -or-
    ///     greater than 5
    /// </exception>
    /// <param name="precision">The number of fractional digits to write</param>
    public QueryStringPrecisionAttribute(int precision)
    {
        if (precision is < 0 or > 5)
            throw new ArgumentOutOfRangeException(nameof(precision));

        Precision = precision;
    }

    /// <summary>
    ///     Gets the number of fractional digits to write
    /// </summary>
    public int Precision { get; }

    internal string Apply(float value)
    {
        return value.ToString($"F{Precision}", NumberFormatInfo.InvariantInfo);
    }

    internal string Apply(double value)
    {
        return value.ToString($"F{Precision}", NumberFormatInfo.InvariantInfo);
    }
}
