using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryStringFormatAttributeTests
{
    [Fact]
    public void TestApplyFormat()
    {
        const double number = 12d;
        new QueryStringFormatAttribute("F0").Apply(number).ShouldBe("12");
        new QueryStringFormatAttribute("F2").Apply(number).ShouldBe("12.00");
    }
}
