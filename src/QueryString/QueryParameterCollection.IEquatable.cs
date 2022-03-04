namespace Avolantis.Text.QueryString;

public sealed partial class QueryParameterCollection : IEquatable<QueryParameterCollection>
{
    /// <summary>
    ///     Determines whether the current query string is equivalent to the provided query string
    /// </summary>
    /// <param name="other">The query string to compare to</param>
    /// <returns>Returns true if the query string has the exact same parameters as the current query string</returns>
    public bool Equals(QueryParameterCollection? other)
    {
        if (other == null)
            return false;

        // Has the same number of keys
        if (_data.Count != other._data.Count)
            return false;

        foreach (var (key, values) in _data)
        {
            // Given key is present in both
            if (!other._data.TryGetValue(key, out var otherValues))
                return false;

            // Equal number of values on given key
            if (values.Count != otherValues.Count)
                return false;

            // Has the same set of values
            var a = new SortedSet<string?>(values, StringComparer.InvariantCulture);
            var b = new SortedSet<string?>(otherValues, StringComparer.InvariantCulture);

            if (!a.SequenceEqual(b, StringComparer.InvariantCulture))
                return false;
        }

        return true;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || obj is QueryParameterCollection other && Equals(other);
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        return _data.GetHashCode();
    }

    /// <inheritdoc cref="Equals(Avolantis.Text.QueryString.QueryParameterCollection?)" />
    public static bool operator ==(QueryParameterCollection? left, QueryParameterCollection? right)
    {
        return Equals(left, right);
    }

    /// <inheritdoc cref="Equals(Avolantis.Text.QueryString.QueryParameterCollection?)" />
    public static bool operator !=(QueryParameterCollection? left, QueryParameterCollection? right)
    {
        return !Equals(left, right);
    }
}
