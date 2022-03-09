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
        var target = new QueryParameterCollection();
        var writer = new QueryStringWriter(target, QueryStringSerializerOptions.Default);
        var stamp = DateOnly.FromDateTime(DateTime.Now);

        DateOnlyQueryParameterConverter.Instance.Convert(writer, stamp, QueryStringSerializerOptions.Default);

        target.Count().ShouldBe(1);
        target.ContainsKey(stamp.ToString("YYYY-MM-dd")).ShouldBeTrue();
    }

    [Fact]
    public void TestConvertCustomFormatSuccess()
    {
        var target = new QueryParameterCollection();
        var options = new QueryStringSerializerOptions { DateOnlyFormat = "YY-MM-dd" };
        var writer = new QueryStringWriter(target, options);
        var stamp = DateOnly.FromDateTime(DateTime.Now);

        DateOnlyQueryParameterConverter.Instance.Convert(writer, stamp, options);

        target.Count().ShouldBe(1);
        target.ContainsKey(stamp.ToString("YY-MM-dd")).ShouldBeTrue();
    }
}
