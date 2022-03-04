using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryParameterCollectionEqualityTests
{
    [Fact]
    public void TestInstanceIsNeverEqualToNull()
    {
        var qs = new QueryParameterCollection();
        qs.Equals(null).ShouldBe(false);
        qs!.Add("key", "value");
        qs.Equals(null).ShouldBe(false);
    }

    [Fact]
    public void TestDifferentCollectionsNotEqual()
    {
        var qs1 = new QueryParameterCollection
        {
            { "key", "value" }
        };
        var qs2 = new QueryParameterCollection
        {
            { "key", "value" },
            { "key2", "value2" }
        };

        (qs1 == qs2).ShouldBeFalse();
        qs1.Equals(qs2).ShouldBeFalse();
    }

    [Fact]
    public void TestSameCollectionEquals()
    {
        var qs1 = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value" },
            { "key2", "value2" }
        };
        var qs2 = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value" },
            { "key2", "value2" }
        };

        (qs1 == qs2).ShouldBeTrue();
        qs1.Equals(qs2).ShouldBeTrue();
    }

    [Fact]
    public void TestEqualityIgnoresParameterOrder()
    {
        var qs1 = new QueryParameterCollection
        {
            { "key", "value" },
            { "key2", "value2" },
            { "key", "value" }
        };
        var qs2 = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value" },
            { "key2", "value2" }
        };

        (qs1 == qs2).ShouldBeTrue();
        qs1.Equals(qs2).ShouldBeTrue();
    }
}
