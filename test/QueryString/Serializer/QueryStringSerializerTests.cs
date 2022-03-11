using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryStringSerializerTests
{
    [Fact]
    public void TestSerializeEnumerableSuccess()
    {
        const string expected = "?3%2C14&23%2C32";
        var obj = new[] { 3.14, 23.32 };
        var result = QueryStringSerializer.Serialize(obj);
        result.ShouldBe(expected);
    }
}
