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
        var target = new QueryParameterCollection();
        var options = new QueryStringSerializerOptions { AutoConvertToUtc = false };
        var writer = new QueryStringWriter(target, options);
        var stamp = DateTime.Now;

        DateTimeQueryParameterConverter.Instance.Convert(writer, stamp, options);

        target.Count().ShouldBe(1);
        target.ContainsKey(stamp.ToString("O")).ShouldBeTrue();
    }

    [Fact]
    public void TestConvertCustomFormatSuccess()
    {
        var target = new QueryParameterCollection();
        var options = new QueryStringSerializerOptions
        {
            AutoConvertToUtc = false,
            DateTimeFormat = "YY-MM-dd"
        };
        var writer = new QueryStringWriter(target, options);
        var stamp = DateTime.Now;

        DateTimeQueryParameterConverter.Instance.Convert(writer, stamp, options);

        target.Count().ShouldBe(1);
        target.ContainsKey(stamp.ToString("YY-MM-dd")).ShouldBeTrue();
    }

    [Fact]
    public void TestConvertToUtc()
    {
        var target = new QueryParameterCollection();
        var writer = new QueryStringWriter(target, QueryStringSerializerOptions.Default);
        var stamp = DateTime.Now;

        DateTimeQueryParameterConverter.Instance.Convert(writer, stamp, QueryStringSerializerOptions.Default);

        target.Count().ShouldBe(1);
        target.ContainsKey(stamp.ToUniversalTime().ToString("O")).ShouldBeTrue();
    }
}
