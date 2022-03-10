using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryParameterCollectionStringConversionTests
{
    [Fact]
    public void TestToStringReturnsCorrectStringRepresentation()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value to be encoded" },
            { "key2", "value2" }
        };
        qs.ToString().ShouldBe("?key=value+to+be+encoded&key2=value2");
        qs.ToString(false, "?", "&").ShouldBe("&key=value+to+be+encoded?key2=value2");
    }

    [Fact]
    public void TestToStringIgnoreNullValuesWhenSpecified()
    {
        var qs = new QueryParameterCollection
        {
            { "key", null },
            { "key", QueryStringParameter.NullValue },
            { "key2", "value2" }
        };
        qs.ToString().ShouldBe("?key&key=null&key2=value2");
        qs.ToString(true).ShouldBe("?key&key2=value2");
        qs.RemoveAll("key2");
        qs.ToString(true).ShouldBe("?key");
    }

    [Fact]
    public void TestToStringEmptyReturnsEmptyString()
    {
        new QueryParameterCollection().ToString().ShouldBe(string.Empty);
    }

    [Fact]
    public void TestParseInvalidStringThrows()
    {
        Should.Throw<ArgumentException>(() => QueryParameterCollection.Parse(string.Empty));
        Should.Throw<ArgumentException>(() => QueryParameterCollection.Parse("   "));
        Should.Throw<ArgumentException>(() => QueryParameterCollection.Parse("?"));
    }

    [Fact]
    public void TestParseSuccess()
    {
        var qs = QueryParameterCollection.Parse("?key=value&key=value&key2&key3=null");

        qs.Count().ShouldBe(4);
        qs["key"].ShouldBe(new List<string?> { "value", "value" });
        qs["key2"].ShouldHaveSingleItem();
        qs["key2"][0].ShouldBeNull();
        qs["key3"].ShouldHaveSingleItem();
        qs["key3"][0].ShouldBe("null");
    }
}
