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
}
