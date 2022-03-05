using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryParameterDateOnlyConverterTests
{
    [Fact]
    public void TestCanConvertReturnsCorrectValue()
    {
        DateOnlyQueryParameterConverter.Instance.CanConvert(typeof(DateOnly)).ShouldBeTrue();
        DateOnlyQueryParameterConverter.Instance.CanConvert(typeof(DateTime)).ShouldBeFalse();
        DateOnlyQueryParameterConverter.Instance.CanConvert(typeof(int)).ShouldBeFalse();
    }

    [Fact]
    public void TestConvertDefaultFormatSuccess()
    {
        var stamp = DateOnly.FromDateTime(DateTime.Now);
        var target = new QueryParameterCollection();
        DateOnlyQueryParameterConverter.Instance.Convert("key", stamp,
            typeof(DateOnly), target, QueryStringSerializerOptions.Default);

        target.Count().ShouldBe(1);
        target.ContainsKey("key").ShouldBeTrue();
        target["key", 0].ShouldBe(stamp.ToString("YYYY-MM-dd"));
    }

    [Fact]
    public void TestConvertCustomFormatSuccess()
    {
        var stamp = DateOnly.FromDateTime(DateTime.Now);
        var target = new QueryParameterCollection();
        DateOnlyQueryParameterConverter.Instance.Convert("key", stamp,
            typeof(DateOnly), target, new QueryStringSerializerOptions { DateOnlyFormat = "YY-MM-dd" });

        target.Count().ShouldBe(1);
        target.ContainsKey("key").ShouldBeTrue();
        target["key", 0].ShouldBe(stamp.ToString("YY-MM-dd"));
    }
}
