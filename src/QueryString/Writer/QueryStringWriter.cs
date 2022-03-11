namespace Avolantis.Text.QueryString;

/// <summary>
///     Provides an API for writing parameters to a <see cref="QueryParameterCollection" />
/// </summary>
public partial class QueryStringWriter
{
    private readonly string _prefix = string.Empty;

    private readonly QueryParameterCollection _target;
    private readonly QueryStringWriterOptions _options;

    internal QueryStringWriter(QueryParameterCollection target, QueryStringWriterOptions options)
    {
        _target = target;
        _options = options;
    }

    private QueryStringWriter(string prefix, HashSet<object> written,
        QueryParameterCollection target, QueryStringWriterOptions options) : this(target, options)
    {
        _prefix = prefix;
        _written = written;
    }

    /// <summary>
    ///     Creates a child <see cref="QueryStringWriter" /> which prefixes
    ///     written parameter keys with the given <paramref name="separator" />
    /// </summary>
    /// <param name="name">The namespace of the child</param>
    /// <param name="separator">The prefix to separate namespaces</param>
    /// <returns>A child <see cref="QueryStringWriter" /></returns>
    public QueryStringWriter CreateChild(string name, string separator = ".")
    {
        var @namespace = string.IsNullOrWhiteSpace(_prefix)
            ? _options.EncodeKey(name)
            : _prefix + separator + _options.EncodeKey(name);
        return new QueryStringWriter(@namespace, _written, _target, _options);
    }

    private void Add(string? key, string value)
    {
        var name = key == null ? _prefix : _prefix + _options.EncodeKey(key);

        // TODO: Add overload with string comparer (from options)
        if (!_options.AllowDuplicateParameters && _target.ContainsKey(name))
            return;

        if (string.IsNullOrWhiteSpace(name))
            _target.Add(_options.EncodeKey(value), null);
        else
            _target.Add(name, value);
    }
}
