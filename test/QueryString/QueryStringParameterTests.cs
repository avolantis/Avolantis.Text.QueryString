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
    public void TestToStringReturnsNullAsFlag()
    {
        var nullParam = new QueryStringParameter("key", null);
        nullParam.ToString().ShouldBe("key");
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
    public void TestGetHashCode()
    {
        var param1 = new QueryStringParameter("key", "value");
        var param2 = new QueryStringParameter("Key", "Value");
        var param3 = new QueryStringParameter("Key2", "Value");
        var param4 = new QueryStringParameter("Key", "Value2");

        (param1.GetHashCode() == param2.GetHashCode()).ShouldBeTrue();
        (param3.GetHashCode() == param4.GetHashCode()).ShouldBeFalse();
        (param2.GetHashCode() == param3.GetHashCode()).ShouldBeFalse();
        (param2.GetHashCode() == param4.GetHashCode()).ShouldBeFalse();
        (param1.GetHashCode() == param3.GetHashCode()).ShouldBeFalse();
        (param1.GetHashCode() == param4.GetHashCode()).ShouldBeFalse();
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

    [Fact]
    public void TestNullFactory()
    {
        QueryStringParameter.Null("key").ToString().ShouldBe("key=null");
    }
}
