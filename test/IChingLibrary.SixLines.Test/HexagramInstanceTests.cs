using IChingLibrary.Core;

namespace IChingLibrary.SixLines.Test;

public class HexagramInstanceTests
{
    private static DateTimeOffset TestInquiryTime => new(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void HexagramInstance_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        var hexagram = Hexagram.TheCreative;

        // Act
        var instance = new HexagramInstance(hexagram);

        // Assert
        Assert.Same(hexagram, instance.Meta);
        Assert.Equal(6, instance.Lines.Count);
    }

    [Fact]
    public void HexagramInstance_TheCreativeHexagram_ShouldHaveAllYangLines()
    {
        // Arrange
        var hexagram = Hexagram.TheCreative;

        // Act
        var instance = new HexagramInstance(hexagram);

        // Assert
        Assert.All(instance.Lines, line => Assert.Equal(YinYang.Yang, line.YinYang));
    }

    [Fact]
    public void HexagramInstance_TheReceptiveHexagram_ShouldHaveAllYinLines()
    {
        // Arrange
        var hexagram = Hexagram.TheReceptive;

        // Act
        var instance = new HexagramInstance(hexagram);

        // Assert
        Assert.All(instance.Lines, line => Assert.Equal(YinYang.Yin, line.YinYang));
    }

    [Fact]
    public void HexagramInstance_Lines_ShouldHaveCorrectPositions()
    {
        // Arrange
        var hexagram = Hexagram.TheCreative;

        // Act
        var instance = new HexagramInstance(hexagram);

        // Assert
        Assert.Equal(LinePosition.First, instance.Lines[0].LinePosition);
        Assert.Equal(LinePosition.Second, instance.Lines[1].LinePosition);
        Assert.Equal(LinePosition.Third, instance.Lines[2].LinePosition);
        Assert.Equal(LinePosition.Fourth, instance.Lines[3].LinePosition);
        Assert.Equal(LinePosition.Fifth, instance.Lines[4].LinePosition);
        Assert.Equal(LinePosition.Sixth, instance.Lines[5].LinePosition);
    }

    [Fact]
    public void HexagramInstance_Indexer_ShouldReturnCorrectLine()
    {
        // Arrange
        var hexagram = Hexagram.TheCreative;

        // Act
        var instance = new HexagramInstance(hexagram);

        // Assert
        Assert.Same(instance.Lines[0], instance[0]);
        Assert.Same(instance.Lines[3], instance[3]);
        Assert.Same(instance.Lines[5], instance[5]);
    }

    [Theory]
    [InlineData(0)]   // 乾为天 - 全阳
    [InlineData(63)]  // 坤为地 - 全阴
    [InlineData(38)]  // 火水未济 - 阴阳相间
    public void HexagramInstance_Value_ShouldMatchBinaryPattern(byte hexagramValue)
    {
        // Arrange
        var hexagram = Hexagram.FromValue(hexagramValue);

        // Act
        var instance = new HexagramInstance(hexagram);

        // Assert
        for (int i = 0; i < 6; i++)
        {
            var expectedYinYang = ((hexagramValue << i) & 1) == 1 ? YinYang.Yang : YinYang.Yin;
            Assert.Equal(expectedYinYang, instance.Lines[i].YinYang);
        }
    }

    [Fact]
    public void HexagramInstance_Lines_ShouldBeModifiable()
    {
        // Arrange
        var fourSymbols = new[]
        {
            FourSymbol.OldYang,    // 有变爻
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang
        };

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        // Assert - 通过 Builder 设置的属性应该是正确的
        Assert.True(divination.Original.Lines[0].IsChanging);
        Assert.NotNull(divination.Original.Lines[0].Position);  // 通过 WithPosition 设置
    }
}
