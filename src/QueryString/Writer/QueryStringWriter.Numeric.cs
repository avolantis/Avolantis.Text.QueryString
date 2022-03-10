namespace Avolantis.Text.QueryString;

public partial class QueryStringWriter
{
    #region Without key

    /// <summary>
    ///     Writes an integral value as a query parameter
    /// </summary>
    /// <param name="value">The value to write</param>
    public void WriteNumber(sbyte value)
    {
        Add(null, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(sbyte)" />
    public void WriteNumber(byte value)
    {
        Add(null, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(sbyte)" />
    public void WriteNumber(short value)
    {
        Add(null, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(sbyte)" />
    public void WriteNumber(ushort value)
    {
        Add(null, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(sbyte)" />
    public void WriteNumber(int value)
    {
        Add(null, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(sbyte)" />
    public void WriteNumber(uint value)
    {
        Add(null, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(sbyte)" />
    public void WriteNumber(long value)
    {
        Add(null, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(sbyte)" />
    public void WriteNumber(ulong value)
    {
        Add(null, value.ToString("F0"));
    }

    /// <summary>
    ///     Writes a floating-point value as a query parameter
    /// </summary>
    /// <param name="value">The value to write</param>
    /// <param name="precision">The number of fractional digits to write</param>
    public void WriteNumber(float value, int? precision = null)
    {
        Add(null, value.ToString($"F{precision ?? _options.DefaultPrecision}"));
    }

    /// <inheritdoc cref="WriteNumber(float, int?)" />
    public void WriteNumber(double value, int? precision = null)
    {
        Add(null, value.ToString($"F{precision ?? _options.DefaultPrecision}"));
    }

    #endregion

    #region WithKey

    /// <summary>
    ///     Writes an integral number query parameter
    /// </summary>
    /// <param name="key">The key of the parameter</param>
    /// <param name="value">The value to write</param>
    public void WriteNumber(string key, sbyte value)
    {
        Add(key, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(string, sbyte)" />
    public void WriteNumber(string key, byte value)
    {
        Add(key, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(string, sbyte)" />
    public void WriteNumber(string key, short value)
    {
        Add(key, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(string, sbyte)" />
    public void WriteNumber(string key, ushort value)
    {
        Add(key, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(string, sbyte)" />
    public void WriteNumber(string key, int value)
    {
        Add(key, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(string, sbyte)" />
    public void WriteNumber(string key, uint value)
    {
        Add(key, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(string, sbyte)" />
    public void WriteNumber(string key, long value)
    {
        Add(key, value.ToString("F0"));
    }

    /// <inheritdoc cref="WriteNumber(string, sbyte)" />
    public void WriteNumber(string key, ulong value)
    {
        Add(key, value.ToString("F0"));
    }

    /// <summary>
    ///     Writes a floating-point number query parameter
    /// </summary>
    /// <param name="key">The key of the parameter</param>
    /// <param name="value">The value to write</param>
    /// <param name="precision"></param>
    public void WriteNumber(string key, float value, int? precision = null)
    {
        Add(key, value.ToString($"F{precision ?? _options.DefaultPrecision}"));
    }

    /// <inheritdoc cref="WriteNumber(string, float, int?)" />
    public void WriteNumber(string key, double value, int? precision = null)
    {
        Add(key, value.ToString($"F{precision ?? _options.DefaultPrecision}"));
    }

    #endregion
}
