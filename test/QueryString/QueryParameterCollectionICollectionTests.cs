using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryParameterCollectionICollectionTests
{
    [Fact]
    public void TestEnumerator()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value" },
            { "key2", "value2" }
        };

        var expected = new List<QueryStringParameter>
        {
            new("key", "value"),
            new("key", "value"),
            new("key2", "value2")
        };

        qs.AsEnumerable().ShouldBe(expected);
    }

    [Fact]
    public void TestClear()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value" },
            { "key2", "value2" }
        };
        qs.Any().ShouldBeTrue();

        qs.Clear();

        qs.Count().ShouldBe(0);
        qs.Any().ShouldBeFalse();
    }

    [Fact]
    public void TestContains()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value" },
            { "key2", "value2" }
        };

        qs.Contains(new QueryStringParameter("key", "value")).ShouldBeTrue();
        qs.Contains(new QueryStringParameter("key2", "value2")).ShouldBeTrue();
    }

    [Fact]
    public void TestReadOnly()
    {
        ((ICollection<QueryStringParameter>)new QueryParameterCollection())
            .IsReadOnly.ShouldBeFalse();
    }

    [Fact]
    public void TestRemove()
    {
        var qs = new QueryParameterCollection
        {
            { "key", "value" },
            { "key", "value" },
            { "key2", "value2" }
        };

        qs["key"].Count.ShouldBe(2);
        qs["key2"].Count.ShouldBe(1);

        qs.Remove(new QueryStringParameter("key", "value")).ShouldBeTrue();

        qs["key"].Count.ShouldBe(1);
        qs["key2"].Count.ShouldBe(1);
    }
}
