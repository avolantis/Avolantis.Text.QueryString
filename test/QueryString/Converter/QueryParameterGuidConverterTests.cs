using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryParameterGuidConverterTests
{
    [Fact]
    public void TestCanConvertReturnsCorrectValue()
    {
        GuidQueryParameterConverter.Instance.CanConvert(typeof(Guid)).ShouldBeTrue();
        GuidQueryParameterConverter.Instance.CanConvert(typeof(DateTime)).ShouldBeFalse();
        GuidQueryParameterConverter.Instance.CanConvert(typeof(int)).ShouldBeFalse();
    }

    [Fact]
    public void TestConvertDefaultFormatSuccess()
    {
        var target = new QueryParameterCollection();
        var writer = new QueryStringWriter(target, QueryStringSerializerOptions.Default);
        var guid = Guid.NewGuid();

        GuidQueryParameterConverter.Instance.Convert(writer, guid, typeof(Guid), QueryStringSerializerOptions.Default);

        target.Count().ShouldBe(1);
        target.ContainsKey(guid.ToString("D")).ShouldBeTrue();
    }
}
