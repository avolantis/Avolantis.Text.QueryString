using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryParameterTimeOnlyConverterTests
{
    [Fact]
    public void TestCanConvertReturnsCorrectValue()
    {
        TimeOnlyQueryParameterConverter.Instance.CanConvert(typeof(TimeOnly)).ShouldBeTrue();
        TimeOnlyQueryParameterConverter.Instance.CanConvert(typeof(DateTime)).ShouldBeFalse();
        TimeOnlyQueryParameterConverter.Instance.CanConvert(typeof(int)).ShouldBeFalse();
    }

    [Fact]
    public void TestConvertDefaultFormatSuccess()
    {
        var stamp = TimeOnly.FromDateTime(DateTime.Now);
        var target = new QueryParameterCollection();
        TimeOnlyQueryParameterConverter.Instance.Convert("key", stamp,
            typeof(TimeOnly), target, QueryStringSerializerOptions.Default);

        target.Count().ShouldBe(1);
        target.ContainsKey("key").ShouldBeTrue();
        target["key", 0].ShouldBe(stamp.ToString("HH:mm:ss"));
    }

    [Fact]
    public void TestConvertCustomFormatSuccess()
    {
        var stamp = TimeOnly.FromDateTime(DateTime.Now);
        var target = new QueryParameterCollection();
        TimeOnlyQueryParameterConverter.Instance.Convert("key", stamp,
            typeof(TimeOnly), target, new QueryStringSerializerOptions { TimeOnlyFormat = "HH:mm" });

        target.Count().ShouldBe(1);
        target.ContainsKey("key").ShouldBeTrue();
        target["key", 0].ShouldBe(stamp.ToString("HH:mm"));
    }
}
