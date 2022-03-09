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
        var target = new QueryParameterCollection();
        var writer = new QueryStringWriter(target, QueryStringSerializerOptions.Default);
        var stamp = TimeOnly.FromDateTime(DateTime.Now);

        TimeOnlyQueryParameterConverter.Instance.Convert(writer, stamp, QueryStringSerializerOptions.Default);

        target.Count().ShouldBe(1);
        target.ContainsKey(stamp.ToString("HH:mm:ss")).ShouldBeTrue();
    }

    [Fact]
    public void TestConvertCustomFormatSuccess()
    {
        var target = new QueryParameterCollection();
        var options = new QueryStringSerializerOptions { TimeOnlyFormat = "HH:mm" };
        var writer = new QueryStringWriter(target, options);
        var stamp = TimeOnly.FromDateTime(DateTime.Now);

        TimeOnlyQueryParameterConverter.Instance.Convert(writer, stamp, options);

        target.Count().ShouldBe(1);
        target.ContainsKey(stamp.ToString("HH:mm")).ShouldBeTrue();
    }
}
