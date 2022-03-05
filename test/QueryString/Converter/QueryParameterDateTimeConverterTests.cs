using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryParameterDateTimeConverterTests
{
    [Fact]
    public void TestCanConvertReturnsCorrectValue()
    {
        DateTimeQueryParameterConverter.Instance.CanConvert(typeof(DateTime)).ShouldBeTrue();
        DateTimeQueryParameterConverter.Instance.CanConvert(typeof(DateOnly)).ShouldBeFalse();
        DateTimeQueryParameterConverter.Instance.CanConvert(typeof(int)).ShouldBeFalse();
    }

    [Fact]
    public void TestConvertDefaultFormatSuccess()
    {
        var stamp = DateTime.Now;
        var target = new QueryParameterCollection();
        DateTimeQueryParameterConverter.Instance.Convert("key", stamp,
            typeof(DateTime), target, new QueryStringSerializerOptions { AutoConvertToUtc = false });

        target.Count().ShouldBe(1);
        target.ContainsKey("key").ShouldBeTrue();
        target["key", 0].ShouldBe(stamp.ToString("O"));
    }

    [Fact]
    public void TestConvertCustomFormatSuccess()
    {
        var stamp = DateTime.Now;
        var target = new QueryParameterCollection();
        DateTimeQueryParameterConverter.Instance.Convert("key", stamp,
            typeof(DateTime), target, new QueryStringSerializerOptions
            {
                AutoConvertToUtc = false,
                DateTimeFormat = "YY-MM-dd"
            });

        target.Count().ShouldBe(1);
        target.ContainsKey("key").ShouldBeTrue();
        target["key", 0].ShouldBe(stamp.ToString("YY-MM-dd"));
    }

    [Fact]
    public void TestConvertToUtc()
    {
        var stamp = DateTime.Now;
        var target = new QueryParameterCollection();
        DateTimeQueryParameterConverter.Instance.Convert("key", stamp,
            typeof(DateTime), target, new QueryStringSerializerOptions { AutoConvertToUtc = true });

        target.Count().ShouldBe(1);
        target.ContainsKey("key").ShouldBeTrue();
        target["key", 0].ShouldBe(stamp.ToUniversalTime().ToString("O"));
    }
}
