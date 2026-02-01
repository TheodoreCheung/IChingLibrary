using System.Globalization;
using IChingLibrary.Core;

namespace IChingLibrary.Core.Test;

public class FourSymbolTests
{
    private static readonly CultureInfo En = new("en");

    [Fact]
    public void FourSymbol_Values_ShouldReturnCorrectValues()
    {
        // Assert
        Assert.Equal(6, FourSymbol.OldYin.Value);
        Assert.Equal(7, FourSymbol.YoungYang.Value);
        Assert.Equal(8, FourSymbol.YoungYin.Value);
        Assert.Equal(9, FourSymbol.OldYang.Value);
    }

    [Fact]
    public void FourSymbol_ToString_ShouldReturnLabel()
    {
        // Assert
        Assert.Equal("Old Yin", FourSymbol.OldYin.ToString(En));
        Assert.Equal("Young Yang", FourSymbol.YoungYang.ToString(En));
        Assert.Equal("Young Yin", FourSymbol.YoungYin.ToString(En));
        Assert.Equal("Old Yang", FourSymbol.OldYang.ToString(En));
    }

    [Fact]
    public void FourSymbol_Equals_ShouldWorkCorrectly()
    {
        // Arrange
        var oldYin1 = FourSymbol.OldYin;
        var oldYin2 = FourSymbol.OldYin;
        var youngYang = FourSymbol.YoungYang;

        // Assert
        Assert.Equal(oldYin1, oldYin2);
        Assert.NotEqual(oldYin1, youngYang);
        Assert.True(oldYin1 == oldYin2);
        Assert.True(oldYin1 != youngYang);
    }

    [Fact]
    public void FourSymbol_GetAll_ShouldReturnAllElements()
    {
        // Act
        var all = FourSymbol.GetAll().ToList();

        // Assert
        Assert.Equal(4, all.Count);
        Assert.Contains(FourSymbol.OldYin, all);
        Assert.Contains(FourSymbol.YoungYang, all);
        Assert.Contains(FourSymbol.YoungYin, all);
        Assert.Contains(FourSymbol.OldYang, all);
    }

    [Fact]
    public void FourSymbol_FromValue_ShouldReturnCorrectElement()
    {
        // Act & Assert
        Assert.Equal(FourSymbol.OldYin, FourSymbol.FromValue(6));
        Assert.Equal(FourSymbol.YoungYang, FourSymbol.FromValue(7));
        Assert.Equal(FourSymbol.YoungYin, FourSymbol.FromValue(8));
        Assert.Equal(FourSymbol.OldYang, FourSymbol.FromValue(9));
    }

    [Theory]
    [InlineData(6, "Old Yin")]
    [InlineData(7, "Young Yang")]
    [InlineData(8, "Young Yin")]
    [InlineData(9, "Old Yang")]
    public void FourSymbol_FromValue_ShouldWorkForAllValues(byte value, string expectedLabel)
    {
        // Act
        var element = FourSymbol.FromValue(value);

        // Assert
        Assert.Equal(value, element.Value);
        Assert.Equal(expectedLabel, element.ToString(En));
    }
}
