using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryStringPrecisionAttributeTests
{
    [Fact]
    public void TestApplyFormat()
    {
        const double number1 = 12f;
        const double number2 = 12d;
        new QueryStringPrecisionAttribute(0).Apply(number1).ShouldBe("12");
        new QueryStringPrecisionAttribute(0).Apply(number2).ShouldBe("12");
        new QueryStringPrecisionAttribute(2).Apply(number1).ShouldBe("12.00");
        new QueryStringPrecisionAttribute(2).Apply(number2).ShouldBe("12.00");
    }
}
