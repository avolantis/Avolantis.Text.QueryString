namespace Avolantis.Text.QueryString;

/// <summary>
///     Specifies an <see cref="IQueryParameterConverter" /> to be used
///     when serializing the attributed class or property
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class QueryStringConverterAttribute : Attribute
{
    /// <summary>
    ///     Specifies an <see cref="IQueryParameterConverter" /> to be used
    ///     when serializing the attributed class or property
    /// </summary>
    /// <param name="converter">The <see cref="IQueryParameterConverter" /> to use</param>
    public QueryStringConverterAttribute(IQueryParameterConverter converter)
    {
        Converter = converter;
    }

    /// <summary>
    ///     Specifies an <see cref="IQueryParameterConverter" /> to be used
    ///     when serializing the attributed class or property
    /// </summary>
    /// <exception cref="ArgumentException">
    ///     Thrown if the provided type does not include a public parameterless constructor
    ///     or is not of type <see cref="IQueryParameterConverter" />
    /// </exception>
    /// <param name="type">The type of the <see cref="IQueryParameterConverter" /> to use</param>
    public QueryStringConverterAttribute(Type type)
    {
        try
        {
            var converter = Activator.CreateInstance(type) as IQueryParameterConverter;
            Converter = converter ?? throw new ArgumentException(
                $"{type.AssemblyQualifiedName} does not include a public parameterless constructor", nameof(type));
        }
        catch (Exception e)
        {
            throw new ArgumentException(
                $"Failed to use {type.AssemblyQualifiedName} as QueryStringConverter", nameof(type), e);
        }
    }

    /// <summary>
    ///     Gets the <see cref="IQueryParameterConverter" />
    /// </summary>
    public IQueryParameterConverter Converter { get; }
}
