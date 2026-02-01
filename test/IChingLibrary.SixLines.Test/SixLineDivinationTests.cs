using IChingLibrary.Core;

namespace IChingLibrary.SixLines.Test;

public class SixLineDivinationTests
{
    private static DateTimeOffset TestInquiryTime => new(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Create_WithFourSymbols_ShouldReturnValidDivination()
    {
        // Arrange
        var fourSymbols = new[]
        {
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang
        };

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Original);
        Assert.NotNull(divination.InquiryTime);
        Assert.NotNull(divination.SymbolicStars);
    }

    [Fact]
    public void Create_WithByteArray_ShouldReturnValidDivination()
    {
        // Arrange
        var fourSymbolValues = new byte[] { 7, 7, 7, 7, 7, 7 };

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbolValues);

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Original);
    }

    [Fact]
    public void Create_WithInquiryTimeOnly_ShouldUseTimeBasedHexagram()
    {
        // Act
        var divination = SixLineDivination.Create(TestInquiryTime);

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Original);
        Assert.NotNull(divination.InquiryTime);
    }

    [Fact]
    public void Create_WithRandomNumbers_ShouldReturnValidDivination()
    {
        // Arrange
        var upperTrigramNumber = 5;
        var lowerTrigramNumber = 3;
        var changingLineNumber = 2;

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, upperTrigramNumber, lowerTrigramNumber, changingLineNumber);

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Original);
    }

    [Fact]
    public void Create_WithHexagrams_ShouldReturnValidDivination()
    {
        // Arrange
        var original = Hexagram.TheCreative;
        Hexagram? changed = null;

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, original, changed);

        // Assert
        Assert.NotNull(divination);
        Assert.Equal(original, divination.Original.Meta);
        Assert.Null(divination.Changed);
    }

    [Fact]
    public void Create_WithHexagramsAndChanged_ShouldReturnValidDivination()
    {
        // Arrange
        var original = Hexagram.TheCreative;
        var changed = Hexagram.TheReceptive;

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, original, changed);

        // Assert
        Assert.NotNull(divination);
        Assert.Equal(original, divination.Original.Meta);
        Assert.NotNull(divination.Changed);
        Assert.Equal(changed, divination.Changed.Meta);
    }

    [Fact]
    public void Create_WithByteValues_ShouldReturnValidDivination()
    {
        // Arrange
        const byte originalValue = 0;  // 乾卦
        byte? changedValue = null;

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, originalValue, changedValue);

        // Assert
        Assert.NotNull(divination);
        Assert.Equal(Hexagram.FromValue(originalValue), divination.Original.Meta);
    }

    [Fact]
    public void Create_WithByteValuesAndChanged_ShouldReturnValidDivination()
    {
        // Arrange
        const byte originalValue = 0;  // 乾卦
        const byte changedValue = 63;  // 坤卦

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, originalValue, changedValue);

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Changed);
        Assert.Equal(Hexagram.FromValue(changedValue), divination.Changed.Meta);
    }

    [Fact]
    public void CreateBuilder_ShouldReturnValidBuilder()
    {
        // Act
        var builder = SixLineDivination.CreateBuilder(TestInquiryTime);

        // Assert
        Assert.NotNull(builder);
    }

    [Fact]
    public void Create_WithChangingLines_ShouldCreateChangedHexagram()
    {
        // Arrange - 使用老阴老阳创建有变爻的卦
        var fourSymbols = new[]
        {
            FourSymbol.OldYang,    // 初爻老阳，变阴
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang
        };

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        // Assert
        Assert.NotNull(divination.Changed);
        Assert.True(divination.Original.Lines[0].IsChanging);
        Assert.NotEqual(divination.Original.Meta, divination.Changed.Meta);
    }

    [Fact]
    public void Create_WithoutChangingLines_ShouldNotCreateChangedHexagram()
    {
        // Arrange - 使用少阴少阳，没有变爻
        var fourSymbols = new[]
        {
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang
        };

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        // Assert
        Assert.Null(divination.Changed);
        Assert.All(divination.Original.Lines, line => Assert.False(line.IsChanging));
    }

    [Fact]
    public void Create_MultipleChangingLines_ShouldCreateCorrectChangedHexagram()
    {
        // Arrange - 多个变爻
        var fourSymbols = new[]
        {
            FourSymbol.OldYang,    // 初爻变
            FourSymbol.YoungYang,
            FourSymbol.OldYin,     // 三爻变
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang
        };

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        // Assert
        Assert.NotNull(divination.Changed);
        Assert.True(divination.Original.Lines[0].IsChanging);
        Assert.True(divination.Original.Lines[2].IsChanging);
    }

    [Fact]
    public void SixLineDivination_Properties_ShouldBeAccessible()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        // Assert
        Assert.NotNull(divination.InquiryTime);
        Assert.NotNull(divination.Original);
        Assert.NotNull(divination.SymbolicStars);

        // 检查 InquiryTime 属性
        Assert.Equal(TestInquiryTime, divination.InquiryTime.Solar);

        // 检查 Original 属性
        Assert.Equal(6, divination.Original.Lines.Count);

        // 检查 SymbolicStars 属性
        Assert.NotEmpty(divination.SymbolicStars.AllStars);
    }

    [Fact]
    public void SixLineDivination_ChangedHexagram_ShouldHaveNajiaAndSixKin()
    {
        // Arrange - 有变爻的卦
        var fourSymbols = new[]
        {
            FourSymbol.OldYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang
        };

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        // Assert
        Assert.NotNull(divination.Changed);
        Assert.All(divination.Changed.Lines, line =>
        {
            // 变卦应该有纳甲（不会抛出异常）
            var stemBranch = line.StemBranch;
            Assert.NotNull(stemBranch);

            // 变卦应该有六亲（使用主卦卦宫五行，不会抛出异常）
            var sixKin = line.SixKin;
            Assert.NotNull(sixKin);
        });
    }
}
