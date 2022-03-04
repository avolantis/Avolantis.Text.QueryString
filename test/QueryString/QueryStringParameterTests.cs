using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryStringParameterTests
{
    [Fact]
    public void TestToStringReturnsCorrectStringRepresentation()
    {
        var param = new QueryStringParameter("key", "value to be encoded");
        param.ToString().ShouldBe("key=value+to+be+encoded");
    }

    [Fact]
    public void TestToStringReturnsNullAsText()
    {
        var nullParam = new QueryStringParameter("key", null);
        nullParam.ToString().ShouldBe("key=null");
    }

    [Fact]
    public void TestInstanceIsNeverEqualToNull()
    {
        var param = new QueryStringParameter("key", "value");
        param.Equals(null).ShouldBe(false);
    }

    [Fact]
    public void TestDefaultEqualityComparisonIgnoresCase()
    {
        var param1 = new QueryStringParameter("key", "value");
        var param2 = new QueryStringParameter("Key", "Value");

        (param1 == param2).ShouldBe(true);
        param1.Equals(param2).ShouldBe(true);
        param2.Equals(param1).ShouldBe(true);
        Equals(param1, param2).ShouldBe(true);
    }

    [Fact]
    public void TestOrdinalEqualityComparison()
    {
        var param1 = new QueryStringParameter("key", "value");
        var param2 = new QueryStringParameter("Key", "Value");

        param1.Equals(param2, StringComparison.Ordinal).ShouldBe(false);
        param2.Equals(param1, StringComparison.Ordinal).ShouldBe(false);
    }

    [Fact]
    public void TestParseFlag()
    {
        var (key, value) = QueryStringParameter.Parse("flagName");
        key.ShouldBe("flagName");
        value.ShouldBeNull();
    }

    [Fact]
    public void TestParseEmptyValue()
    {
        var (key, value) = QueryStringParameter.Parse("name=");
        key.ShouldBe("name");
        value.ShouldBe(string.Empty);
    }

    [Fact]
    public void TestParseInvalidThrowsArgumentException()
    {
        Should.Throw<ArgumentException>(() => QueryStringParameter.Parse(string.Empty));
        Should.Throw<ArgumentException>(() => QueryStringParameter.Parse("    "));
    }
}
