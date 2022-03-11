using System.Globalization;
using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryStringWriterNumericsTests
{
    private const sbyte Sbyte = -10;
    private const byte Byte = 11;
    private const short Short = -12;
    private const ushort UShort = 13;
    private const int Int = -7;
    private const uint UInt = 69;
    private const long Long = -420;
    private const ulong ULong = 42;
    private const float Float = 12.34f;
    private const double Double = 3.14d;

    public QueryStringWriterNumericsTests()
    {
        Target = new QueryParameterCollection();
        Writer = new QueryStringWriter(Target, QueryStringSerializerOptions.Default);
    }

    private QueryParameterCollection Target { get; }
    private QueryStringWriter Writer { get; }

    [Fact]
    public void TestIntegralWithoutKey()
    {
        Writer.WriteNumber(Sbyte);
        Writer.WriteNumber(Byte);
        Writer.WriteNumber(Short);
        Writer.WriteNumber(UShort);
        Writer.WriteNumber(Int);
        Writer.WriteNumber(UInt);
        Writer.WriteNumber(Long);
        Writer.WriteNumber(ULong);

        Target.Keys.ShouldContain(Sbyte.ToString("F0", CultureInfo.InvariantCulture));
        Target.Keys.ShouldContain(Byte.ToString("F0", CultureInfo.InvariantCulture));
        Target.Keys.ShouldContain(Short.ToString("F0", CultureInfo.InvariantCulture));
        Target.Keys.ShouldContain(UShort.ToString("F0", CultureInfo.InvariantCulture));
        Target.Keys.ShouldContain(Int.ToString("F0", CultureInfo.InvariantCulture));
        Target.Keys.ShouldContain(UInt.ToString("F0", CultureInfo.InvariantCulture));
        Target.Keys.ShouldContain(Long.ToString("F0", CultureInfo.InvariantCulture));
        Target.Keys.ShouldContain(ULong.ToString("F0", CultureInfo.InvariantCulture));
    }

    [Fact]
    public void TestIntegralWithKey()
    {
        Writer.WriteNumber("key", Sbyte);
        Writer.WriteNumber("key", Byte);
        Writer.WriteNumber("key", Short);
        Writer.WriteNumber("key", UShort);
        Writer.WriteNumber("key", Int);
        Writer.WriteNumber("key", UInt);
        Writer.WriteNumber("key", Long);
        Writer.WriteNumber("key", ULong);

        Target.Keys.ShouldContain("key");
        Target["key"].Count.ShouldBe(8);
        Target["key", 0].ShouldBe(Sbyte.ToString("F0", CultureInfo.InvariantCulture));
        Target["key", 1].ShouldBe(Byte.ToString("F0", CultureInfo.InvariantCulture));
        Target["key", 2].ShouldBe(Short.ToString("F0", CultureInfo.InvariantCulture));
        Target["key", 3].ShouldBe(UShort.ToString("F0", CultureInfo.InvariantCulture));
        Target["key", 4].ShouldBe(Int.ToString("F0", CultureInfo.InvariantCulture));
        Target["key", 5].ShouldBe(UInt.ToString("F0", CultureInfo.InvariantCulture));
        Target["key", 6].ShouldBe(Long.ToString("F0", CultureInfo.InvariantCulture));
        Target["key", 7].ShouldBe(ULong.ToString("F0", CultureInfo.InvariantCulture));
    }

    [Fact]
    public void TestFloatingPointWithoutKey()
    {
        Writer.WriteNumber(Float);
        Writer.WriteNumber(Double);

        Target.Keys.ShouldContain(Float.ToString("F2", CultureInfo.InvariantCulture));
        Target.Keys.ShouldContain(Double.ToString("F2", CultureInfo.InvariantCulture));
    }

    [Fact]
    public void TestFloatingPointWithoutKeyWithCustomPrecision()
    {
        Writer.WriteNumber(Float, 3);
        Writer.WriteNumber(Double, 4);

        Target.Keys.ShouldContain(Float.ToString("F3", CultureInfo.InvariantCulture));
        Target.Keys.ShouldContain(Double.ToString("F4", CultureInfo.InvariantCulture));
    }

    [Fact]
    public void TestFloatingPointWithKey()
    {
        Writer.WriteNumber("key", Float);
        Writer.WriteNumber("key", Double);

        Target.Keys.ShouldContain("key");
        Target["key"].Count.ShouldBe(2);
        Target["key", 0].ShouldBe(Float.ToString("F2", CultureInfo.InvariantCulture));
        Target["key", 1].ShouldBe(Double.ToString("F2", CultureInfo.InvariantCulture));
    }

    [Fact]
    public void TestFloatingPointWithKeyWithCustomPrecision()
    {
        Writer.WriteNumber("key", Float, 5);
        Writer.WriteNumber("key", Double, 1);

        Target.Keys.ShouldContain("key");
        Target["key"].Count.ShouldBe(2);
        Target["key", 0].ShouldBe(Float.ToString("F5", CultureInfo.InvariantCulture));
        Target["key", 1].ShouldBe(Double.ToString("F1", CultureInfo.InvariantCulture));
    }
}
