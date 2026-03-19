using IChingLibrary.Core;
using IChingLibrary.SixLines.Builder;

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
    public void Create_WithFourSymbolsAndByteArray_ShouldProduceEquivalentDivination()
    {
        var fourSymbols = new[]
        {
            FourSymbol.OldYang,
            FourSymbol.YoungYin,
            FourSymbol.YoungYang,
            FourSymbol.OldYin,
            FourSymbol.YoungYang,
            FourSymbol.YoungYin
        };

        var fromSymbols = SixLineDivination.Create(TestInquiryTime, fourSymbols);
        var fromBytes = SixLineDivination.Create(TestInquiryTime, fourSymbols.Select(symbol => symbol.Value).ToArray());

        AssertEquivalentDivinations(fromSymbols, fromBytes);
    }

    [Fact]
    public void Create_WithInquiryTimeOnly_ShouldUseTimeBasedHexagram()
    {
        // Act
        var divination = SixLineDivination.Create(TestInquiryTime);

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Original);
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
        const byte originalValue = 63;  // 乾卦
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
        const byte originalValue = 63;  // 乾卦
        const byte changedValue = 0;  // 坤卦

        // Act
        var divination = SixLineDivination.Create(TestInquiryTime, originalValue, changedValue);

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Changed);
        Assert.Equal(Hexagram.FromValue(changedValue), divination.Changed.Meta);
    }

    [Fact]
    public void Create_WithHexagramsAndByteValues_ShouldProduceEquivalentDivination()
    {
        var fromHexagrams = SixLineDivination.Create(TestInquiryTime, Hexagram.TheCreative, Hexagram.TheReceptive);
        var fromBytes = SixLineDivination.Create(TestInquiryTime, Hexagram.TheCreative.Value, Hexagram.TheReceptive.Value);

        AssertEquivalentDivinations(fromHexagrams, fromBytes);
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
        Assert.NotNull(divination.Original);
        Assert.NotNull(divination.SymbolicStars);

        // 检查 CastingTime 属性
        Assert.Equal(TestInquiryTime, divination.CastingTime.Solar);

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

    [Fact]
    public void ToString_PartialBuild_ShouldNotThrow()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var divination = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestInquiryTime, fourSymbols))
            .Build();

        // Act
        var ex = Record.Exception(() => divination.ToString());

        // Assert
        Assert.Null(ex);
    }

    private static void AssertEquivalentDivinations(SixLineDivination expected, SixLineDivination actual)
    {
        Assert.Equal(expected.CastingTime.Solar, actual.CastingTime.Solar);
        AssertEquivalentHexagramInstances(expected.Original, actual.Original);

        if (expected.Changed is null)
        {
            Assert.Null(actual.Changed);
        }
        else
        {
            Assert.NotNull(actual.Changed);
            AssertEquivalentHexagramInstances(expected.Changed, actual.Changed);
        }

        Assert.NotNull(expected.SymbolicStars);
        Assert.NotNull(actual.SymbolicStars);

        var expectedStars = expected.SymbolicStars.AllStars;
        var actualStars = actual.SymbolicStars.AllStars;

        Assert.Equal(expectedStars.Count, actualStars.Count);
        foreach (var (star, expectedBranches) in expectedStars)
        {
            Assert.True(actualStars.ContainsKey(star));
            Assert.Equal(expectedBranches.Select(branch => branch.Value), actualStars[star].Select(branch => branch.Value));
        }
    }

    private static void AssertEquivalentHexagramInstances(HexagramInstance expected, HexagramInstance actual)
    {
        Assert.Equal(expected.Meta, actual.Meta);
        Assert.Equal(expected.Lines.Count, actual.Lines.Count);

        for (var i = 0; i < expected.Lines.Count; i++)
        {
            var expectedLine = expected.Lines[i];
            var actualLine = actual.Lines[i];

            Assert.Equal(expectedLine.LinePosition, actualLine.LinePosition);
            Assert.Equal(expectedLine.YinYang, actualLine.YinYang);
            Assert.Equal(expectedLine.IsChanging, actualLine.IsChanging);
            Assert.Equal(expectedLine.FourSymbol, actualLine.FourSymbol);
            Assert.Equal(expectedLine.StemBranch.Stem, actualLine.StemBranch.Stem);
            Assert.Equal(expectedLine.StemBranch.Branch, actualLine.StemBranch.Branch);
            Assert.Equal(expectedLine.SixKin, actualLine.SixKin);
            Assert.Equal(expectedLine.SixSpirit, actualLine.SixSpirit);
            Assert.Equal(expectedLine.Position, actualLine.Position);
            Assert.Equal(expectedLine.HiddenDeity?.SixKin, actualLine.HiddenDeity?.SixKin);
            Assert.Equal(expectedLine.HiddenDeity?.StemBranch.Stem, actualLine.HiddenDeity?.StemBranch.Stem);
            Assert.Equal(expectedLine.HiddenDeity?.StemBranch.Branch, actualLine.HiddenDeity?.StemBranch.Branch);
        }
    }
}
