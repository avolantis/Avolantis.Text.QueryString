// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Avolantis.Text.QueryString;

/// <summary>
///     Represents a collection of query string parameters
/// </summary>
public sealed partial class QueryParameterCollection
{
    private readonly Dictionary<string, List<string?>> _data = new();

    /// <summary>
    ///     Initializes an empty <see cref="QueryParameterCollection" /> instance
    /// </summary>
    public QueryParameterCollection()
    {
    }

    /// <summary>
    ///     Initializes a new <see cref="QueryParameterCollection" /> instance with the specified parameters
    /// </summary>
    /// <param name="parameters">The parameters to copy into the new <see cref="QueryParameterCollection" /></param>
    public QueryParameterCollection(IEnumerable<QueryStringParameter> parameters)
    {
        foreach (var parameter in parameters)
            Add(parameter);
    }

    /// <summary>
    ///     Initializes a new <see cref="QueryParameterCollection" /> instance with the specified parameters
    /// </summary>
    /// <param name="parameters">The parameters to copy into the new <see cref="QueryParameterCollection" /></param>
    public static QueryParameterCollection Create(IEnumerable<KeyValuePair<string, string?>> parameters)
    {
        var result = new QueryParameterCollection();

        foreach (var (key, value) in parameters)
            result.Add(key, value);

        return result;
    }

    /// <summary>
    ///     Initializes a new <see cref="QueryParameterCollection" /> instance with the specified parameters
    /// </summary>
    /// <param name="parameters">The parameters to copy into the new <see cref="QueryParameterCollection" /></param>
    public static QueryParameterCollection Create<T>(IEnumerable<KeyValuePair<string, T>> parameters)
        where T : IEnumerable<string?>
    {
        var result = new QueryParameterCollection();

        foreach (var (key, values) in parameters)
        foreach (var value in values)
            result.Add(key, value);

        return result;
    }

    /// <summary>
    ///     Gets the key of parameters defined in this query string
    /// </summary>
    public IReadOnlyCollection<string> Keys => _data.Keys;

    /// <summary>
    ///     Gets the values associated with the given key
    /// </summary>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown if a parameter with the given name could not be found
    /// </exception>
    /// <param name="name">The parameter name to find</param>
    public IReadOnlyList<string?> this[string name]
    {
        get
        {
            if (TryGetValues(name, out var values))
                return values;

            throw new KeyNotFoundException($"A parameter with name '{name}' could not be found.");
        }
    }

    /// <summary>
    ///     Gets the value at the given index of the specified parameter
    /// </summary>
    /// <exception cref="KeyNotFoundException">
    ///     Thrown if a parameter with the given name could not be found
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if <paramref name="index" /> is less than 0 or greater
    ///     than the count of values associated with the given parameter
    /// </exception>
    /// <param name="name">The name of the parameter</param>
    /// <param name="index">The index of the value to retrieve</param>
    public string? this[string name, int index]
    {
        get
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (!_data.TryGetValue(name, out var values))
                throw new KeyNotFoundException($"A parameter with name '{name}' could not be found.");

            if (values.Count <= index)
                throw new ArgumentOutOfRangeException(nameof(index));

            return values[index];
        }
    }

    /// <summary>
    ///     Gets the values associated with the given key
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if <paramref name="index" /> is less than 0
    /// </exception>
    /// <param name="name">The parameter name to find</param>
    /// <param name="index">The index of the value to retrieve</param>
    /// <param name="value">The value</param>
    /// <returns>Whether a parameter with a matching name and a value at the given index exists</returns>
    public bool TryGetValue(string name, int index, out string? value)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (_data.TryGetValue(name, out var items) && items.Any())
        {
            value = items[index];
            return true;
        }

        value = null;
        return false;
    }

    /// <summary>
    ///     Gets the values associated with the given key
    /// </summary>
    /// <param name="name">The parameter name to find</param>
    /// <param name="values">The values</param>
    /// <returns>Whether at least one parameter with a matching name exists</returns>
    public bool TryGetValues(string name, [NotNullWhen(true)] out IReadOnlyList<string?>? values)
    {
        if (_data.TryGetValue(name, out var items) && items.Any())
        {
            values = items;
            return true;
        }

        values = null;
        return false;
    }

    /// <summary>
    ///     Returns the count of parameters
    /// </summary>
    public int Count()
    {
        return _data.Sum(i => i.Value.Count);
    }

    /// <summary>
    ///     Returns a copy of this query parameter collection
    /// </summary>
    public QueryParameterCollection Clone()
    {
        return new QueryParameterCollection(this);
    }

    /// <summary>
    ///     Adds a parameter to the query string
    /// </summary>
    /// <param name="name">The name of the parameter</param>
    /// <param name="value">The value of the parameter</param>
    public void Add(string name, string? value)
    {
        if (!_data.TryGetValue(name, out var values))
        {
            values = new List<string?>();
            _data[name] = values;
        }

        values.Add(value);
    }

    /// <summary>
    ///     Sets a parameter in the query string
    /// </summary>
    /// <remarks>
    ///     Existing parameters with the same name are removed
    /// </remarks>
    /// <param name="name">The name of the parameter</param>
    /// <param name="value">The value of the parameter</param>
    public void Set(string name, string? value)
    {
        _data[name] = new List<string?> { value };
    }

    /// <summary>
    ///     Determines if the query string contains at least one parameter with the specified name
    /// </summary>
    /// <param name="name">The parameter name to look for</param>
    public bool ContainsKey(string name)
    {
        return _data.ContainsKey(name) && _data[name].Any();
    }

    /// <summary>
    ///     Determines if the query string contains a parameter with the specified name and value
    /// </summary>
    /// <param name="name">The parameter name to look for</param>
    /// <param name="value">The value to look for</param>
    /// <param name="comparer">The string comparer to use</param>
    public bool Contains(string name, string? value, StringComparer? comparer = null)
    {
        comparer ??= StringComparer.InvariantCultureIgnoreCase;
        return _data.TryGetValue(name, out var values) && values.Contains(value, comparer);
    }

    /// <summary>
    ///     Removes the value at the given index from the specified parameter
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///     Thrown if <paramref name="index" /> is less than 0 -or-
    ///     <paramref name="index" /> is greater than or equal to
    ///     the number of values
    /// </exception>
    /// <param name="name">The name of parameter to remove</param>
    /// <param name="index">The index of the value to remove</param>
    /// <returns>Whether parameters were removed</returns>
    public bool Remove(string name, int index)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (!_data.TryGetValue(name, out var values))
            return false;

        if (values.Count == 1)
            _data.Remove(name);
        else
            values.RemoveAt(index);

        return true;
    }

    /// <summary>
    ///     Removes the first parameter with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the parameter to remove</param>
    /// <param name="value">The value to remove</param>
    /// <returns>Whether a parameter was removed</returns>
    public bool Remove(string name, string? value)
    {
        if (!_data.TryGetValue(name, out var values))
            return false;

        var idx = values.FindIndex(i => Equals(i, value));
        if (idx < 0)
            return false;

        values.RemoveAt(idx);
        if (values.Count == 0)
            _data.Remove(name);

        return true;
    }

    /// <summary>
    ///     Removes all parameters with the specified name
    /// </summary>
    /// <param name="name">The name of parameters to remove</param>
    /// <returns>Whether parameters were removed</returns>
    public bool RemoveAll(string name)
    {
        return _data.Remove(name);
    }

    /// <summary>
    ///     Removes all parameters with the specified name and value
    /// </summary>
    /// <param name="name">The name of parameters to remove</param>
    /// <param name="value">The value to match when deciding whether to remove</param>
    /// <returns>The count of parameters removed</returns>
    public int RemoveAll(string name, string value)
    {
        if (!_data.TryGetValue(name, out var values))
            return 0;

        var count = values.RemoveAll(i => Equals(i, value));
        if (values.Count == 0)
            _data.Remove(name);

        return count;
    }

    /// <summary>
    ///     Serializes the query string to its string representation
    /// </summary>
    /// <remarks>If there are no parameters, an empty string will be returned</remarks>
    /// <returns>The URL-encoded query string</returns>
    public override string ToString()
    {
        return ToString(false);
    }

    /// <summary>
    ///     Serializes the query string to its string representation using the specified settings
    /// </summary>
    /// <param name="ignoreNullValues">Whether to ignore parameters with <c>null</c> value</param>
    /// <param name="separator">The separator to use, defaults to <c>&amp;</c></param>
    /// <param name="prefix">The prefix to use, defaults to <c>?</c></param>
    /// <remarks>If there are no parameters, an empty string will be returned</remarks>
    /// <returns>The URL-encoded query string</returns>
    public string ToString(bool ignoreNullValues, string separator = "&", string prefix = "?")
    {
        if (!_data.Any())
            return string.Empty;

        var sb = new StringBuilder();
        sb.Append(prefix);

        foreach (var param in this)
        {
            if (ignoreNullValues && param.Value == QueryStringParameter.NullValue)
                continue;

            param.ToString(sb);
            sb.Append(separator);
        }

        if (sb.Length == prefix.Length)
            return string.Empty;

        sb.Remove(sb.Length - separator.Length, separator.Length);
        return sb.ToString();
    }

    /// <summary>
    ///     Parses a <see cref="QueryParameterCollection" /> instance from its string representation
    /// </summary>
    /// <exception cref="ArgumentException">
    ///     Thrown when <paramref name="input" /> cannot be parsed
    /// </exception>
    /// <param name="input">The input string</param>
    /// <param name="separator">The separator to look for, defaults to <c>&amp;</c></param>
    /// <param name="prefix">The prefix to look for, defaults to <c>?</c></param>
    /// <returns>The parsed <see cref="QueryParameterCollection" /> instance</returns>
    public static QueryParameterCollection Parse(string input, string separator = "&", string prefix = "?")
    {
        if (!TryParse(input, out var result, separator, prefix))
            throw new ArgumentException("Unable to parse query string", nameof(input));

        return result;
    }

    /// <summary>
    ///     Tries to parse a <see cref="QueryParameterCollection" /> instance from its string representation
    /// </summary>
    /// <param name="input">The input string</param>
    /// <param name="result">The parsed <see cref="QueryParameterCollection" /> instance or <c>null</c></param>
    /// <param name="separator">The separator to look for, defaults to <c>&amp;</c></param>
    /// <param name="prefix">The prefix to look for, defaults to <c>?</c></param>
    /// <returns>Whether parsing was successful</returns>
    public static bool TryParse(string input, [NotNullWhen(true)] out QueryParameterCollection? result,
        string separator = "&", string prefix = "?")
    {
        if (string.IsNullOrWhiteSpace(input) || input.Length < prefix.Length + 1)
        {
            result = null;
            return false;
        }

        result = new QueryParameterCollection();
        input = input.StartsWith(prefix) ? input[prefix.Length..] : input;

        foreach (var pair in input.Split(separator))
        {
            if (!QueryStringParameter.TryParse(pair, out var param))
            {
                result = null;
                return false;
            }

            result.Add(param.Value);
        }

        return true;
    }
}
