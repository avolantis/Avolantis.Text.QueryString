using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryParameterFormatStringConverterTests
{
    [QueryStringFormat("B")]
    private class TestClass : IFormattable
    {
        public Guid Guid { get; set; }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            return Guid.ToString(format, formatProvider);
        }
    }

    [Theory]
    [InlineData(typeof(TestClass), true)]
    [InlineData(typeof(Guid), false)]
    [InlineData(typeof(QueryParameterFormatStringConverterTests), false)]
    public void TestCanConvertReturnsCorrectValue(Type type, bool expected)
    {
        FormatStringQueryParameterConverter.Instance.CanConvert(type).ShouldBe(expected);
    }

    [Fact]
    public void TestClassConvertSuccess()
    {
        var target = new QueryParameterCollection();
        var writer = new QueryStringWriter(target, QueryStringSerializerOptions.Default);
        var value = new TestClass { Guid = Guid.NewGuid() };

        FormatStringQueryParameterConverter.Instance.Convert(writer, value, typeof(TestClass),
            QueryStringSerializerOptions.Default);

        target.Count().ShouldBe(1);
        target.ContainsKey(value.Guid.ToString("B")).ShouldBeTrue();
    }
}
