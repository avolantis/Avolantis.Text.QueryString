using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryStringSerializerTests
{
    [Fact]
    public void TestSerializeAnonymousObjectSuccess()
    {
        const string expected = "?Name=ABC&Age=21&CanDrinkAlcohol=true&FavoriteNumber=3.14";
        var obj = new { Name = "ABC", Age = 21, CanDrinkAlcohol = true, FavoriteNumber = 3.14d };
        var result = QueryStringSerializer.Serialize(obj);
        result.ShouldBe(expected);
    }

    [Fact]
    public void TestSerializeNestedObjectSuccess()
    {
        const string expected =
            "?Name=ABC&Properties.Age=21&Properties.CanDrinkAlcohol=true&Properties.FavoriteNumber=3.14";
        var obj = new
        {
            Name = "ABC",
            Properties = new { Age = 21, CanDrinkAlcohol = true, FavoriteNumber = 3.14d }
        };
        var result = QueryStringSerializer.Serialize(obj);
        result.ShouldBe(expected);
    }

    [Fact]
    public void TestSerializeEnumerableSuccess()
    {
        const string expected = "?3.14&23.32";
        var obj = new[] { 3.14, 23.32 };
        var result = QueryStringSerializer.Serialize(obj);
        result.ShouldBe(expected);
    }

    [Fact]
    public void TestSerializeEnumerablePropertySuccess()
    {
        const string expected = "?List=3.14&List=23.32";
        var obj = new { List = new List<double> { 3.14, 23.32 } };
        var result = QueryStringSerializer.Serialize(obj);
        result.ShouldBe(expected);
    }
}
