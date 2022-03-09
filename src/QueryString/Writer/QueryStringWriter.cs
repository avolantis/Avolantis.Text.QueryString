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
    /// <param name="prefix">The prefix</param>
    /// <returns>A child <see cref="QueryStringWriter" /></returns>
    public QueryStringWriter CreateChild(string prefix)
    {
        return new QueryStringWriter(_prefix + prefix, _target, _options, _written);
    }

    private void Add(string? key, string? value)
    {
        var name = key == null ? _prefix : _prefix + key;

        if (!_options.AllowDuplicateParameters && _target.ContainsKey(name))
            return;

        if (string.IsNullOrWhiteSpace(name))
            _target.Add(value ?? QueryStringParameter.NullValue, null);
        else
            _target.Add(name, value);
    }
}
