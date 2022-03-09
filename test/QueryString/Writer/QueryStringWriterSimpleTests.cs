using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryStringWriterSimpleTests
{
    [Theory]
    [InlineData(null, true, "?true=null")]
    [InlineData(null, false, "?false=null")]
    [InlineData("key", true, "?key=true")]
    [InlineData("key", false, "?key=false")]
    public void TestWriteBooleanSuccess(string? key, bool value, string expected)
    {
        var target = new QueryParameterCollection();
        var writer = new QueryStringWriter(target, QueryStringSerializerOptions.Default);

        if (key != null)
            writer.WriteBoolean(key, value);
        else
            writer.WriteBoolean(value);

        target.ToString().ShouldBe(expected);
    }

    [Theory]
    [InlineData(null, "?null=null")]
    [InlineData("key", "?key=null")]
    public void TestWriteNullSuccess(string? key, string expected)
    {
        var target = new QueryParameterCollection();
        var writer = new QueryStringWriter(target, QueryStringSerializerOptions.Default);

        if (key != null)
            writer.WriteNull(key);
        else
            writer.WriteNull();

        target.ToString().ShouldBe(expected);
    }

    [Theory]
    [InlineData(null, "value", "?value=null")]
    [InlineData("key", "value", "?key=value")]
    public void TestWriteStringSuccess(string? key, string value, string expected)
    {
        var target = new QueryParameterCollection();
        var writer = new QueryStringWriter(target, QueryStringSerializerOptions.Default);

        if (key != null)
            writer.WriteString(key, value);
        else
            writer.WriteString(value);

        target.ToString().ShouldBe(expected);
    }
}
