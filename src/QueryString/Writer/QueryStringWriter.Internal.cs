namespace Avolantis.Text.QueryString;

public partial class QueryStringWriter
{
    private readonly HashSet<object> _written = new(ReferenceEqualityComparer.Instance);

    /// <summary>
    ///     Maintains a log of written object references adn throws <see cref="NotImplementedException" />
    ///     if a given reference has already been written using this instance
    /// </summary>
    /// <param name="value">The object reference</param>
    /// <exception cref="NotImplementedException">Thrown if a circular object reference was found</exception>
    internal void WriteRef(object value)
    {
        if (!_written.Add(value))
            throw new NotImplementedException("Circular object reference");
    }

    internal void WriteNumeric(object value)
    {
        switch (value)
        {
            default:
                throw new ArgumentOutOfRangeException(nameof(value));

            // Floating point
            case float f:
                WriteNumber(f);
                break;
            case double d:
                WriteNumber(d);
                break;

            // Integral
            case sbyte b:
                WriteNumber(b);
                break;
            case byte b:
                WriteNumber(b);
                break;
            case short s:
                WriteNumber(s);
                break;
            case ushort s:
                WriteNumber(s);
                break;
            case int i:
                WriteNumber(i);
                break;
            case uint i:
                WriteNumber(i);
                break;
            case long l:
                WriteNumber(l);
                break;
            case ulong l:
                WriteNumber(l);
                break;
        }
    }
}
