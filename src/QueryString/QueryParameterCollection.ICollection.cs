// ReSharper disable UseDeconstructionOnParameter

using System.Collections;

namespace Avolantis.Text.QueryString;

public sealed partial class QueryParameterCollection : ICollection<QueryStringParameter>
{
    int ICollection<QueryStringParameter>.Count => Count();
    bool ICollection<QueryStringParameter>.IsReadOnly => false;

    /// <summary>
    ///     Add the given parameter to this query string
    /// </summary>
    /// <remarks>Use <see cref="Add(string, string)" /> instead, when possible</remarks>
    /// <param name="item">The parameter to add</param>
    public void Add(QueryStringParameter item)
    {
        Add(item.Key, item.Value);
    }

    /// <summary>
    ///     Removes all parameters from the query string
    /// </summary>
    public void Clear()
    {
        _data.Clear();
    }

    /// <summary>
    ///     Determines whether this query string contains the specified parameter
    /// </summary>
    /// <param name="item">The parameter to search for</param>
    public bool Contains(QueryStringParameter item)
    {
        return Contains(item.Key, item.Value);
    }

    /// <summary>
    ///     Removes the specified parameter from this query string
    /// </summary>
    /// <param name="item">The parameter to remove</param>
    /// <returns>Whether the parameter was removed</returns>
    public bool Remove(QueryStringParameter item)
    {
        return Remove(item.Key, item.Value);
    }

    /// <summary>
    ///     Gets an enumerator to enumerate the query string parameters
    /// </summary>
    /// <returns>The <see cref="IEnumerator{T}" /> instance</returns>
    public IEnumerator<QueryStringParameter> GetEnumerator()
    {
        foreach (var (key, values) in _data)
        foreach (var value in values)
            yield return new QueryStringParameter(key, value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    void ICollection<QueryStringParameter>.CopyTo(QueryStringParameter[] array, int arrayIndex)
    {
        this.ToArray().CopyTo(array, arrayIndex);
    }
}
