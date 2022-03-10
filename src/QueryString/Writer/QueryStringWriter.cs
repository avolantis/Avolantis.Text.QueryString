namespace Avolantis.Text.QueryString;

/// <summary>
///     Provides an API for writing parameters to a <see cref="QueryParameterCollection" />
/// </summary>
public partial class QueryStringWriter
{
    private readonly string _prefix = string.Empty;

    private readonly QueryParameterCollection _target;
    private readonly QueryStringSerializerOptions _options;

    internal QueryStringWriter(QueryParameterCollection target, QueryStringSerializerOptions options)
    {
        _target = target;
        _options = options;
    }

    private QueryStringWriter(string prefix, QueryParameterCollection target,
        QueryStringSerializerOptions options, HashSet<object> written) : this(target, options)
    {
        _prefix = prefix;
        _written = written;
    }

    /// <summary>
    ///     Creates a child <see cref="QueryStringWriter" /> which prefixes
    ///     written parameter keys with the given <paramref name="prefix" />
    /// </summary>
    /// <param name="name">The namespace of the child</param>
    /// <param name="prefix">The prefix to separate namespaces</param>
    /// <returns>A child <see cref="QueryStringWriter" /></returns>
    public QueryStringWriter CreateChild(string name, string prefix = ".")
    {
        var target = string.IsNullOrWhiteSpace(_prefix) ? name : _prefix + prefix + name;
        return new QueryStringWriter(target, _target, _options, _written);
    }

    private void Add(string? key, string value)
    {
        var name = key == null ? _prefix : _prefix + key;

        if (!_options.AllowDuplicateParameters && _target.ContainsKey(name))
            return;

        if (string.IsNullOrWhiteSpace(name))
            _target.Add(value, null);
        else
            _target.Add(name, value);
    }
}
