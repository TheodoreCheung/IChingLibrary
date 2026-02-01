using System.Globalization;
using IChingLibrary.Core;

namespace IChingLibrary.Core.Test;

public class IChingElementTests
{
    [Fact]
    public void YinYang_Value_ShouldReturnCorrectValue()
    {
        // Assert
        Assert.Equal(0, YinYang.Yin.Value);
        Assert.Equal(1, YinYang.Yang.Value);
    }

    [Fact]
    public void YinYang_ToString_ShouldReturnEnglishTranslation()
    {
        // Arrange
        var en = new CultureInfo("en");

        // Assert - ToString uses CurrentUICulture, we explicitly use English for consistent tests
        Assert.Equal("Yin", YinYang.Yin.ToString(en));
        Assert.Equal("Yang", YinYang.Yang.ToString(en));
    }

    [Fact]
    public void YinYang_Equals_ShouldWorkCorrectly()
    {
        // Arrange
        var yin1 = YinYang.Yin;
        var yin2 = YinYang.Yin;
        var yang = YinYang.Yang;

        // Assert
        Assert.Equal(yin1, yin2);
        Assert.NotEqual(yin1, yang);
        Assert.True(yin1 == yin2);
        Assert.True(yin1 != yang);
    }

    [Fact]
    public void YinYang_Equals_Null_ShouldReturnFalse()
    {
        // Assert
        Assert.False(YinYang.Yin.Equals(null));
        Assert.False(YinYang.Yin == null);
        Assert.True(null != YinYang.Yin);
    }

    [Fact]
    public void YinYang_GetHashCode_ShouldBeSameForSameValue()
    {
        // Arrange
        var yin1 = YinYang.Yin;
        var yin2 = YinYang.Yin;

        // Assert
        Assert.Equal(yin1.GetHashCode(), yin2.GetHashCode());
    }

    [Fact]
    public void YinYang_ImplicitByteConversion_ShouldWork()
    {
        // Assert
        byte yinValue = YinYang.Yin;
        byte yangValue = YinYang.Yang;

        Assert.Equal(0, yinValue);
        Assert.Equal(1, yangValue);
    }

    [Fact]
    public void YinYang_GetAll_ShouldReturnAllElements()
    {
        // Act
        var all = YinYang.GetAll().ToList();

        // Assert
        Assert.Equal(2, all.Count);
        Assert.Contains(YinYang.Yin, all);
        Assert.Contains(YinYang.Yang, all);
    }

    [Fact]
    public void YinYang_FromValue_ShouldReturnCorrectElement()
    {
        // Act & Assert
        Assert.Equal(YinYang.Yin, YinYang.FromValue(0));
        Assert.Equal(YinYang.Yang, YinYang.FromValue(1));
    }
}
