// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UseDeconstructionOnParameter

using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;

namespace Avolantis.Text.QueryString;

/// <summary>
///     Represents a query parameter in a key-value pair form
/// </summary>
/// <param name="Key">The key of the query parameter</param>
/// <param name="Value">The value of the query parameter</param>
public readonly record struct QueryStringParameter(string Key, string? Value)
{
    /// <summary>
    ///     Represents the value <c>null</c> as a string
    /// </summary>
    public const string NullValue = "null";

    /// <summary>
    ///     Creates a new <see cref="QueryStringParameter" /> with <see cref="NullValue" />
    /// </summary>
    /// <param name="key">The key of the query parameter</param>
    /// <returns>A <see cref="QueryStringParameter" /> instance with <see cref="NullValue" /></returns>
    public static QueryStringParameter Null(string key)
    {
        return new(key, NullValue);
    }

    /// <summary>
    ///     Returns the key of this query parameter, with URL encoding applied
    /// </summary>
    public string EncodedKey => WebUtility.UrlEncode(Key);

    /// <summary>
    ///     Returns the value of this query parameter, with URL encoding applied
    /// </summary>
    public string? EncodedValue => WebUtility.UrlEncode(Value);

    /// <summary>
    ///     Determines if this query string parameter is equal to the specified one, using the default string comparison
    /// </summary>
    /// <param name="other">The other query parameter</param>
    /// <returns>Whether the two query parameters are considered equal</returns>
    public bool Equals(QueryStringParameter other)
    {
        return Equals(other, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///     Return the string representation of this query parameter
    /// </summary>
    public override string ToString()
    {
        return EncodedValue == null ? EncodedKey : $"{EncodedKey}={EncodedValue}";
    }

    /// <summary>
    ///     Appends the string representation of this query parameter to a <see cref="StringBuilder" />
    /// </summary>
    /// <param name="sb">The <see cref="StringBuilder" /> instance to append onto</param>
    public void ToString(StringBuilder sb)
    {
        sb.Append(EncodedKey);

        if (EncodedValue == null)
            return;

        sb.Append('=');
        sb.Append(EncodedValue);
    }

    /// <summary>
    ///     Determines if this query string parameter is equal to the specified one, using the default string comparison
    /// </summary>
    /// <param name="other">The other query parameter</param>
    /// <returns>Whether the two query parameters are considered equal</returns>
    public bool Equals(QueryStringParameter? other)
    {
        return Equals(other, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    ///     Determines if this query string parameter is equal to the specified one, using the specified string comparison
    /// </summary>
    /// <param name="other">The other query parameter</param>
    /// <param name="stringComparison">The <see cref="StringComparison" /> to use when comparing keys and values</param>
    /// <returns>Whether the two query parameters are considered equal</returns>
    public bool Equals(QueryStringParameter? other, StringComparison stringComparison)
    {
        if (!other.HasValue)
            return false;
        var keysEqual = string.Equals(Key, other.Value.Key, stringComparison);
        return keysEqual && string.Equals(Value, other.Value.Value, stringComparison);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Key, StringComparer.InvariantCultureIgnoreCase);
        hashCode.Add(Value, StringComparer.InvariantCultureIgnoreCase);
        return hashCode.ToHashCode();
    }

    /// <summary>
    ///     Parses a <see cref="QueryStringParameter" /> instance from its string representation
    /// </summary>
    /// <exception cref="ArgumentException">
    ///     Thrown when <paramref name="input" /> cannot be parsed
    /// </exception>
    /// <param name="input">The value to parse</param>
    /// <returns>The parsed <see cref="QueryStringParameter" /> instance</returns>
    public static QueryStringParameter Parse(string input)
    {
        if (!TryParse(input, out var result))
            throw new ArgumentException("Unable to parse query parameter", nameof(input));

        return result.Value;
    }

    /// <summary>
    ///     Tries to parse a <see cref="QueryStringParameter" /> instance from its string representation
    /// </summary>
    /// <param name="input">The value to parse</param>
    /// <param name="result">The parsed <see cref="QueryStringParameter" /> instance or <c>null</c></param>
    /// <returns>Whether parsing was successful</returns>
    public static bool TryParse(string input, [NotNullWhen(true)] out QueryStringParameter? result)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            result = null;
            return false;
        }

        var idx = input.IndexOf('=');
        var key = WebUtility.UrlDecode(idx < 0 ? input : input[..idx++]);
        var value = idx < 0 ? null : input[idx..];
        result = new QueryStringParameter(key, value);
        return true;
    }
}
