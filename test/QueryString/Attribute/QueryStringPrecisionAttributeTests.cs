using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryStringPrecisionAttributeTests
{
    [Fact]
    public void TestApplyFormat()
    {
        new QueryStringPrecisionAttribute(0).Apply(12f).ShouldBe("12");
        new QueryStringPrecisionAttribute(0).Apply(12d).ShouldBe("12");
        new QueryStringPrecisionAttribute(2).Apply(12f).ShouldBe("12.00");
        new QueryStringPrecisionAttribute(2).Apply(12d).ShouldBe("12.00");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    public void TestInvalidPrecision(int precision)
    {
        Should.Throw<ArgumentOutOfRangeException>(() => new QueryStringPrecisionAttribute(precision));
    }
}
