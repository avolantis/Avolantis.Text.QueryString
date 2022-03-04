using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryParameterCollectionApiTests
{
    [Fact]
    public void TestCopyConstructor()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value2" },
            { "key2", "value2" }
        };

        var copy = new QueryParameterCollection(qs);
        ReferenceEquals(qs["key"], copy["key"]).ShouldBeFalse();
        ReferenceEquals(qs["key2"], copy["key2"]).ShouldBeFalse();
    }

    [Fact]
    public void TestClone()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value2" },
            { "key2", "value2" }
        };

        var copy = qs.Clone();
        ReferenceEquals(qs["key"], copy["key"]).ShouldBeFalse();
        ReferenceEquals(qs["key2"], copy["key2"]).ShouldBeFalse();
    }

    [Fact]
    public void TestCount()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value" },
            { "key2", "value2" }
        };

        qs.Count().ShouldBe(3);
    }

    [Fact]
    public void TestContainsKey()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value" },
            { "key2", "value2" }
        };

        qs.ContainsKey("key").ShouldBeTrue();
        qs.ContainsKey("key2").ShouldBeTrue();
        qs.ContainsKey("key3").ShouldBeFalse();
    }

    [Fact]
    public void TestRemoveAll()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value" },
            { "key2", "value2" }
        };

        qs.RemoveAll("key").ShouldBeTrue();
        qs.ContainsKey("key").ShouldBeFalse();
        qs.ContainsKey("key2").ShouldBeTrue();
    }

    [Fact]
    public void TestRemoveAllMatching()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value" },
            { "key", "value2" },
            { "key2", "value2" }
        };

        qs.RemoveAll("key", "value").ShouldBe(2);
        qs.ContainsKey("key").ShouldBeTrue();
        qs.ContainsKey("key2").ShouldBeTrue();
        qs["key"].ShouldHaveSingleItem();
        qs["key", 0].ShouldBe("value2");
    }

    [Fact]
    public void TestRemoveAtIndex()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value2" },
            { "key", "value" },
            { "key2", "value2" }
        };

        qs.Remove("key", 1).ShouldBe(true);
        qs.ContainsKey("key").ShouldBeTrue();
        qs.ContainsKey("key2").ShouldBeTrue();
        qs["key"].Count.ShouldBe(2);
        qs["key"].ShouldNotContain("value2");
    }

    [Fact]
    public void TestSetParameter()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value2" },
            { "key", "value" },
            { "key2", "value2" }
        };

        qs.Set("key", "unrelated value");
        qs["key"].ShouldHaveSingleItem();
        qs["key", 0].ShouldBe("unrelated value");
    }
}
