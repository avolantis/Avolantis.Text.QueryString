namespace Avolantis.Text.QueryString;

public partial class QueryStringWriter
{
    /// <summary>
    ///     Writes a boolean value as a query parameter
    /// </summary>
    /// <param name="value">The value to write</param>
    public void WriteBoolean(bool value)
    {
        Add(null, value.ToString().ToLowerInvariant());
    }

    /// <summary>
    ///     Writes a boolean query parameter
    /// </summary>
    /// <param name="key">The key of the parameter</param>
    /// <param name="value">The value to write</param>
    public void WriteBoolean(string key, bool value)
    {
        Add(key, value.ToString().ToLowerInvariant());
    }

    /// <summary>
    ///     Writes <c>null</c> value as a query parameter
    /// </summary>
    public void WriteNull()
    {
        Add(null, QueryStringParameter.NullValue);
    }

    /// <summary>
    ///     Writes <c>null</c> as a query parameter
    /// </summary>
    /// <param name="key">The key of the parameter</param>
    public void WriteNull(string key)
    {
        Add(key, QueryStringParameter.NullValue);
    }

    /// <summary>
    ///     Writes a string value as a query parameter
    /// </summary>
    /// <param name="value">The value to write</param>
    public void WriteString(string value)
    {
        Add(null, _options.TrimStrings ? value.Trim() : value);
    }

    /// <summary>
    ///     Writes a string query parameter
    /// </summary>
    /// <param name="key">The key of the parameter</param>
    /// <param name="value">The value to write</param>
    public void WriteString(string key, string value)
    {
        Add(key, _options.TrimStrings ? value.Trim() : value);
    }
}
