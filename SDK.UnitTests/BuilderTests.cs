using MoBro.Plugin.SDK.Builders;
using MoBro.Plugin.SDK.Enums;

namespace MoBro.SDK.UnitTests;

public class BuilderTests
{
  [Fact]
  public void CategoryTest()
  {
    var category = MoBroItem.CreateCategory()
      .WithId("id1")
      .WithLabel("category1", "desc1")
      .WithSubCategory(sub => sub
        .WithId("sub1")
        .WithLabel("subcategory1")
        .WithIcon("test.png")
        .Build()
      )
      .Build();

    Assert.Equal("id1", category.Id);
    Assert.Equal("category1", category.Label);
    Assert.Equal("desc1", category.Description);
    Assert.Null(category.Icon);
    Assert.NotNull(category.SubCategories);
    Assert.Collection(category.SubCategories, sub =>
    {
      Assert.Equal("sub1", sub.Id);
      Assert.Equal("subcategory1", sub.Label);
      Assert.Null(sub.Description);
      Assert.Equal("test.png", sub.Icon);
      Assert.NotNull(sub.SubCategories);
      Assert.Empty(sub.SubCategories);
    });
  }

  [Fact]
  public void GroupTest()
  {
    var group = MoBroItem.CreateGroup()
      .WithId("group1")
      .WithLabel("label", "description")
      .WithSubGroup(sub => sub
        .WithId("sub1")
        .WithLabel("subgroup1")
        .WithIcon("test.png")
        .Build())
      .Build();

    Assert.Equal("group1", group.Id);
    Assert.Equal("label", group.Label);
    Assert.Equal("description", group.Description);
    Assert.Null(group.Icon);
    Assert.NotNull(group.SubGroups);
    Assert.Collection(group.SubGroups, sub =>
    {
      Assert.Equal("sub1", sub.Id);
      Assert.Equal("subgroup1", sub.Label);
      Assert.Null(sub.Description);
      Assert.Equal("test.png", sub.Icon);
      Assert.NotNull(sub.SubGroups);
      Assert.Empty(sub.SubGroups);
    });
  }

  [Fact]
  public void ResourceTest()
  {
    var image = MoBroItem.CreateResource()
      .WithId("r1")
      .WithAlt("alt1")
      .Image()
      .FromRelativePath("Usings.cs")
      .Build();

    Assert.Equal("r1", image.Id);
    Assert.Equal("alt1", image.Alt);
    Assert.Equal("Usings.cs", image.RelativeFilePath);

    var icon = MoBroItem.CreateResource()
      .WithId("r2")
      .WithoutAlt()
      .Icon()
      .AddFromAbsolutePath(Path.Combine(Directory.GetCurrentDirectory(), "Usings.cs"))
      .Build();

    Assert.Equal("r2", icon.Id);
    Assert.Null(icon.Alt);
    Assert.NotEmpty(icon.RelativeFilePaths);
    Assert.Equal("Usings.cs", icon.RelativeFilePaths[IconSize.Default]);
  }

  [Fact]
  public void MetricTypeTest()
  {
    var customType = MoBroItem.CreateMetricType()
      .WithId("type1")
      .WithLabel("label")
      .OfValueType(MetricValueType.Custom)
      .Build();

    Assert.Equal("type1", customType.Id);
    Assert.Equal("label", customType.Label);
    Assert.Null(customType.Description);
    Assert.Equal(MetricValueType.Custom, customType.ValueType);
    Assert.Null(customType.BaseUnit);
    Assert.NotNull(customType.Units);
    Assert.Empty(customType.Units);

    var numericType = MoBroItem.CreateMetricType()
      .WithId("type2")
      .WithLabel("label", "desc")
      .OfValueType(MetricValueType.Numeric)
      .WithIcon("icon.png")
      .WithBaseUnit(bu => bu
        .WithLabel("BaseUnit")
        .WithAbbreviation("BU")
        .Build())
      .WithDerivedUnit(u => u
        .WithConversionFormula("x = x/2", "x = x*2")
        .WithLabel("Unit1")
        .WithAbbreviation("U1")
        .Build())
      .WithDerivedUnit(u => u
        .WithConversionFormula("x = x/3", "x = x*3")
        .WithLabel("Unit2", "desc2")
        .WithAbbreviation("U2")
        .Build())
      .Build();

    Assert.Equal("type2", numericType.Id);
    Assert.Equal("label", numericType.Label);
    Assert.Equal("desc", numericType.Description);
    Assert.Equal(MetricValueType.Numeric, numericType.ValueType);
    Assert.Equal("BaseUnit", numericType.BaseUnit?.Label);
    Assert.Equal("BU", numericType.BaseUnit?.Abbreviation);
    Assert.Null(numericType.BaseUnit?.Description);
    Assert.Equal("x", numericType.BaseUnit?.FromBaseFormula);

    Assert.NotNull(numericType.Units);
    Assert.NotEmpty(numericType.Units);
    Assert.Collection(numericType.Units, u1 =>
    {
      Assert.Equal("Unit1", u1.Label);
      Assert.Equal("U1", u1.Abbreviation);
      Assert.Null(u1.Description);
      Assert.Equal("x = x/2", u1.FromBaseFormula);
      Assert.Equal("x = x*2", u1.ToBaseFormula);
    }, u2 =>
    {
      Assert.Equal("Unit2", u2.Label);
      Assert.Equal("U2", u2.Abbreviation);
      Assert.Equal("desc2", u2.Description);
      Assert.Equal("x = x/3", u2.FromBaseFormula);
      Assert.Equal("x = x*3", u2.ToBaseFormula);
    });
  }

  [Fact]
  public void MetricTest()
  {
    var metric = MoBroItem.CreateMetric()
      .WithId("id1")
      .WithLabel("label")
      .OfType("type1")
      .OfCategory("category1")
      .OfNoGroup()
      .AsStaticValue()
      .Build();

    Assert.Equal("id1", metric.Id);
    Assert.Equal("label", metric.Label);
    Assert.Null(metric.Description);
    Assert.Equal("type1", metric.TypeId);
    Assert.Equal("category1", metric.CategoryId);
    Assert.Null(metric.GroupId);
    Assert.True(metric.IsStatic);
  }
}