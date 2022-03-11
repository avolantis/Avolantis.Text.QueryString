using Shouldly;
using Xunit;

namespace Avolantis.Text.QueryString;

public class QueryStringWriterApiTests
{
    [Fact]
    public void TestChildNamespacingSingleLevel()
    {
        var target = new QueryParameterCollection();
        var writer = new QueryStringWriter(target, QueryStringSerializerOptions.Default);

        var child = writer.CreateChild("child");
        child.WriteString(".child-key", "child-value");
        writer.WriteString("key", "value");

        target.ContainsKey("key").ShouldBeTrue();
        target.ContainsKey("child.child-key").ShouldBeTrue();
        target["key"].ShouldHaveSingleItem();
        target["key", 0].ShouldBe("value");
        target["child.child-key"].ShouldHaveSingleItem();
        target["child.child-key", 0].ShouldBe("child-value");
    }

    [Fact]
    public void TestChildNamespacingMultiLevel()
    {
        var target = new QueryParameterCollection();
        var writer = new QueryStringWriter(target, QueryStringSerializerOptions.Default);

        var child = writer.CreateChild("child");
        var grandchild = child.CreateChild("grandchild");
        grandchild.WriteString(".grandchild-key", "grandchild-value");
        child.WriteString(".child-key", "child-value");
        writer.WriteString("key", "value");

        target.ContainsKey("key").ShouldBeTrue();
        target.ContainsKey("child.child-key").ShouldBeTrue();
        target.ContainsKey("child.grandchild.grandchild-key").ShouldBeTrue();
        target["key"].ShouldHaveSingleItem();
        target["key", 0].ShouldBe("value");
        target["child.child-key"].ShouldHaveSingleItem();
        target["child.child-key", 0].ShouldBe("child-value");
        target["child.grandchild.grandchild-key"].ShouldHaveSingleItem();
        target["child.grandchild.grandchild-key", 0].ShouldBe("grandchild-value");
    }

    [Fact]
    public void TestReferenceLog()
    {
        var target = new QueryParameterCollection();
        var writer = new QueryStringWriter(target, QueryStringSerializerOptions.Default);
        var obj = new object();

        writer.WriteRef(obj);
        Should.Throw<NotImplementedException>(() => writer.WriteRef(obj));
    }

    [Fact]
    public void TestIgnoreDuplicate()
    {
        var target = new QueryParameterCollection();
        var options = new QueryStringSerializerOptions { AllowDuplicateParameters = false };
        var writer = new QueryStringWriter(target, options);

        writer.WriteString("key", "value");
        writer.WriteString("key", "value2");
        writer.WriteString("key2", "value");
        writer.WriteString("key2", "value2");

        target.Count().ShouldBe(2);
        target.ContainsKey("key").ShouldBeTrue();
        target.ContainsKey("key2").ShouldBeTrue();
        target["key"].ShouldHaveSingleItem();
        target["key", 0].ShouldBe("value");
        target["key2"].ShouldHaveSingleItem();
        target["key2", 0].ShouldBe("value");
    }

    [Theory]
    [InlineData(QueryParameterKeyCasingPolicy.NoChange, "SomeKey", true, "?SomeKey")]
    [InlineData(QueryParameterKeyCasingPolicy.NoChange, "someKey", true, "?someKey")]
    [InlineData(QueryParameterKeyCasingPolicy.NoChange, "SomeKey", false, "?SomeKey=SomeValue")]
    [InlineData(QueryParameterKeyCasingPolicy.NoChange, "someKey", false, "?someKey=SomeValue")]
    [InlineData(QueryParameterKeyCasingPolicy.CamelCase, "SomeKey", true, "?someKey")]
    [InlineData(QueryParameterKeyCasingPolicy.CamelCase, "SomeKey", false, "?someKey=SomeValue")]
    [InlineData(QueryParameterKeyCasingPolicy.PascalCase, "someKey", true, "?SomeKey")]
    [InlineData(QueryParameterKeyCasingPolicy.PascalCase, "someKey", false, "?SomeKey=SomeValue")]
    [InlineData(QueryParameterKeyCasingPolicy.LowerCase, "SomeKey", true, "?somekey")]
    [InlineData(QueryParameterKeyCasingPolicy.LowerCase, "SomeKey", false, "?somekey=SomeValue")]
    [InlineData(QueryParameterKeyCasingPolicy.UpperCase, "SomeKey", true, "?SOMEKEY")]
    [InlineData(QueryParameterKeyCasingPolicy.UpperCase, "SomeKey", false, "?SOMEKEY=SomeValue")]
    public void TestKeyCasingPolicy(QueryParameterKeyCasingPolicy policy, string key, bool isFlag, string expected)
    {
        var target = new QueryParameterCollection();
        var options = new QueryStringWriterOptions { KeyCasingPolicy = policy };
        var writer = new QueryStringWriter(target, options);

        if (isFlag)
            writer.WriteString(key);
        else
            writer.WriteString(key, "SomeValue");

        target.ToString().ShouldBe(expected);
    }
}
