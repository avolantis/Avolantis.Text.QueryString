namespace Avolantis.Text.QueryString;

internal static class QueryStringWriterOptionsExtensions
{
    public static string EncodeKey(this QueryStringWriterOptions options, string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return key;

        return options.KeyCasingPolicy switch
        {
            QueryParameterKeyCasingPolicy.NoChange => key,
            QueryParameterKeyCasingPolicy.CamelCase => key.Length > 1
                ? char.ToLowerInvariant(key[0]) + key[1..]
                : char.ToLowerInvariant(key[0]).ToString(),
            QueryParameterKeyCasingPolicy.PascalCase => key.Length > 1
                ? char.ToUpperInvariant(key[0]) + key[1..]
                : char.ToUpperInvariant(key[0]).ToString(),
            QueryParameterKeyCasingPolicy.LowerCase => key.ToLowerInvariant(),
            QueryParameterKeyCasingPolicy.UpperCase => key.ToUpperInvariant(),
            _ => throw new IndexOutOfRangeException($"Unknown {nameof(options.KeyCasingPolicy)}")
        };
    }
}
