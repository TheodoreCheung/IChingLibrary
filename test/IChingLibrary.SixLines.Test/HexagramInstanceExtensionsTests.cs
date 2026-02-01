using IChingLibrary.Core;

namespace IChingLibrary.SixLines.Test;

public class HexagramInstanceExtensionsTests
{
    private static DateTimeOffset TestInquiryTime => new(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void FindHexagramBody_YangWorldlyLine_ShouldStartFromZi()
    {
        // Arrange - 乾卦世在上爻（阳）
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);
        var hexagramBody = divination.Original.FindHexagramBody();

        // Assert - 阳世起子，从初爻数到世爻（上爻）
        // 初爻=子，二爻=丑，...，上爻（第六位）=巳
        Assert.NotNull(hexagramBody);
        Assert.Equal(EarthlyBranch.Si, hexagramBody);
    }

    [Fact]
    public void FindHexagramBody_YinWorldlyLine_ShouldStartFromWu()
    {
        // Arrange - 创建一个阴爻在世爻位置的卦
        // 使用姤卦（天风姤），初爻变阴，为一世卦
        var fourSymbols = new[]
        {
            FourSymbol.OldYin,    // 初爻变阴
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang
        };

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);
        var hexagramBody = divination.Original.FindHexagramBody();

        // Assert - 阴世起午，世在初爻
        // 初爻=午
        Assert.NotNull(hexagramBody);
        Assert.Equal(EarthlyBranch.Wu, hexagramBody);
    }

    [Fact]
    public void FindHexagramBody_NoWorldlyLine_ShouldReturnNull()
    {
        // Arrange - 创建一个没有设置世爻的卦实例
        var hexagram = Hexagram.TheCreative;
        var instance = new HexagramInstance(hexagram);

        // Act
        var hexagramBody = instance.FindHexagramBody();

        // Assert
        Assert.Null(hexagramBody);
    }

    [Fact]
    public void FindHexagramBody_YangWorldlyAtDifferentPositions_ShouldReturnCorrectBranch()
    {
        // Arrange - 乾卦世在上爻（阳）
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);
        var worldlyLine = divination.Original.Lines.FirstOrDefault(l => l.Position == Position.Worldly);

        // Assert - 阳世起子，从初爻数到世爻
        if (worldlyLine != null && worldlyLine.YinYang == YinYang.Yang)
        {
            var hexagramBody = divination.Original.FindHexagramBody();
            var offset = worldlyLine.LinePosition.ToArrayIndex();
            // 阳世起子，从子开始数
            var expectedValue = (byte)(((byte)EarthlyBranch.Zi + offset - 1) % 12 + 1);
            var expected = EarthlyBranch.FromValue(expectedValue);
            Assert.Equal(expected, hexagramBody);
        }
    }

    [Fact]
    public void FindHexagramBody_YinWorldlyAtFirstPosition_ShouldReturnWu()
    {
        // Arrange - 姤卦世在初爻（阴）
        var fourSymbols = new[]
        {
            FourSymbol.OldYin,    // 初爻变阴
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang
        };

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);
        var hexagramBody = divination.Original.FindHexagramBody();

        // Assert - 阴世起午，世在初爻
        Assert.Equal(EarthlyBranch.Wu, hexagramBody);
    }
}
