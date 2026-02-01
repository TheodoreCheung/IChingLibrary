using IChingLibrary.Core;

namespace IChingLibrary.SixLines.Test;

public class SixLineDivinationBuilderTests
{
    private static DateTimeOffset TestInquiryTime => new(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Builder_UseFourSymbols_ShouldAcceptValidArray()
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
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithDefaultSteps()
            .Build();

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Original);
    }

    [Fact]
    public void Builder_UseFourSymbols_InvalidLength_ShouldThrowArgumentException()
    {
        // Arrange
        var invalidFourSymbols = new[]
        {
            FourSymbol.YoungYang,
            FourSymbol.YoungYang,
            FourSymbol.YoungYang
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            SixLineDivination.CreateBuilder(TestInquiryTime)
                .UseFourSymbols(invalidFourSymbols);
        });
    }

    [Fact]
    public void Builder_UseFourSymbols_ByteArray_ShouldAcceptValidArray()
    {
        // Arrange
        var fourSymbolValues = new byte[] { 7, 7, 7, 7, 7, 7 };

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbolValues)
            .WithDefaultSteps()
            .Build();

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Original);
    }

    [Fact]
    public void Builder_UseFourSymbols_ByteArrayInvalidLength_ShouldThrowArgumentException()
    {
        // Arrange
        var invalidValues = new byte[] { 7, 7, 7 };

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            SixLineDivination.CreateBuilder(TestInquiryTime)
                .UseFourSymbols(invalidValues);
        });
    }

    [Fact]
    public void Builder_UseTimeBasedHexagram_ShouldCreateValidDivination()
    {
        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseTimeBasedHexagram()
            .WithDefaultSteps()
            .Build();

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Original);
    }

    [Fact]
    public void Builder_UseRandomHexagram_ShouldCreateValidDivination()
    {
        // Arrange
        var upperTrigramNumber = 5;
        var lowerTrigramNumber = 3;
        var changingLineNumber = 2;

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseRandomHexagram(upperTrigramNumber, lowerTrigramNumber, changingLineNumber)
            .WithDefaultSteps()
            .Build();

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Original);
    }

    [Fact]
    public void Builder_UseRandomHexagram_WithoutChangingLine_ShouldCreateValidDivination()
    {
        // Arrange
        var upperTrigramNumber = 5;
        var lowerTrigramNumber = 3;

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseRandomHexagram(upperTrigramNumber, lowerTrigramNumber)
            .WithDefaultSteps()
            .Build();

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Original);
    }

    [Fact]
    public void Builder_UseHexagram_ShouldCreateValidDivination()
    {
        // Arrange
        var original = Hexagram.TheCreative;
        Hexagram? changed = null;

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseHexagram(original, changed)
            .WithDefaultSteps()
            .Build();

        // Assert
        Assert.NotNull(divination);
        Assert.Equal(original, divination.Original.Meta);
    }

    [Fact]
    public void Builder_UseHexagram_WithChanged_ShouldCreateValidDivination()
    {
        // Arrange
        var original = Hexagram.TheCreative;
        var changed = Hexagram.TheReceptive;

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseHexagram(original, changed)
            .WithDefaultSteps()
            .Build();

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Changed);
    }

    [Fact]
    public void Builder_WithNajia_ShouldBindStemBranches()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithNajia()
            .Build();

        // Assert
        Assert.All(divination.Original.Lines, line =>
        {
            // 访问 StemBranch 不应抛出异常
            var stemBranch = line.StemBranch;
            Assert.NotNull(stemBranch);
        });
    }

    [Fact]
    public void Builder_WithPosition_ShouldBindWorldlyAndCorresponding()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithNajia()
            .WithPosition()
            .Build();

        // Assert
        var worldlyLine = divination.Original.Lines.FirstOrDefault(l => l.Position == Position.Worldly);
        var correspondingLine = divination.Original.Lines.FirstOrDefault(l => l.Position == Position.Corresponding);

        Assert.NotNull(worldlyLine);
        Assert.NotNull(correspondingLine);
        Assert.NotSame(worldlyLine, correspondingLine);
    }

    [Fact]
    public void Builder_WithSixKin_ShouldBindSixKin()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithNajia()
            .WithSixKin()
            .Build();

        // Assert
        Assert.All(divination.Original.Lines, line =>
        {
            // 访问 SixKin 不应抛出异常
            var sixKin = line.SixKin;
            Assert.NotNull(sixKin);
        });
    }

    [Fact]
    public void Builder_WithSixSpirit_ShouldBindSixSpirits()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithNajia()
            .WithSixSpirit()
            .Build();

        // Assert
        Assert.All(divination.Original.Lines, line =>
        {
            // 六神应该已设置
            Assert.NotNull(line.SixSpirit);
        });
    }

    [Fact]
    public void Builder_WithHiddenDeity_ShouldBindHiddenDeities()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithNajia()
            .WithSixKin()
            .WithHiddenDeity()
            .Build();

        // Assert
        // 至少应该运行不抛出异常
        Assert.NotNull(divination);
    }

    [Fact]
    public void Builder_WithSymbolicStars_ShouldCalculateSymbolicStars()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithNajia()
            .WithSixKin()
            .WithSymbolicStars()
            .Build();

        // Assert
        Assert.NotNull(divination.SymbolicStars);
        Assert.NotEmpty(divination.SymbolicStars.AllStars);
    }

    [Fact]
    public void Builder_WithSymbolicStars_Configured_ShouldRespectConfiguration()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithNajia()
            .WithSixKin()
            .WithSymbolicStars(provider =>
            {
                // 只保留部分神煞
                provider.Remove(SymbolicStar.SalarySpirit);
                provider.Remove(SymbolicStar.CultureFlourish);
                provider.Remove(SymbolicStar.YangBlade);
                provider.Remove(SymbolicStar.PostHorse);
                provider.Remove(SymbolicStar.PeachBlossom);
            })
            .Build();

        // Assert
        Assert.NotNull(divination.SymbolicStars);
        Assert.True(divination.SymbolicStars.AllStars.Count <= 16);
    }

    [Fact]
    public void Builder_WithDefaultSteps_ShouldApplyAllSteps()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithDefaultSteps()
            .Build();

        // Assert
        Assert.NotNull(divination.Original);
        Assert.NotNull(divination.SymbolicStars);

        // 所有爻应该有纳甲
        Assert.All(divination.Original.Lines, line =>
        {
            var stemBranch = line.StemBranch;
            Assert.NotNull(stemBranch);
        });

        // 所有爻应该有六亲
        Assert.All(divination.Original.Lines, line =>
        {
            var sixKin = line.SixKin;
            Assert.NotNull(sixKin);
        });

        // 所有爻应该有六神
        Assert.All(divination.Original.Lines, line =>
        {
            Assert.NotNull(line.SixSpirit);
        });
    }

    [Fact]
    public void Builder_WithNajiaForChanged_ShouldBindChangedStemBranches()
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
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithNajia()
            .WithNajiaForChanged()
            .Build();

        // Assert
        Assert.NotNull(divination.Changed);
        Assert.All(divination.Changed.Lines, line =>
        {
            // 变卦应该有纳甲
            var stemBranch = line.StemBranch;
            Assert.NotNull(stemBranch);
        });
    }

    [Fact]
    public void Builder_WithSixKinForChanged_ShouldBindChangedSixKin()
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
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithNajia()
            .WithSixKin()
            .WithNajiaForChanged()
            .WithSixKinForChanged()
            .Build();

        // Assert
        Assert.NotNull(divination.Changed);
        Assert.All(divination.Changed.Lines, line =>
        {
            // 变卦应该有六亲（使用主卦卦宫五行）
            var sixKin = line.SixKin;
            Assert.NotNull(sixKin);
        });
    }

    [Fact]
    public void Builder_BuildWithoutHexagram_ShouldThrowInvalidOperationException()
    {
        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
        {
            SixLineDivination.CreateBuilder(TestInquiryTime)
                .WithDefaultSteps()
                .Build();
        });
    }

    [Fact]
    public void Builder_FluentApi_ShouldWork()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithNajia()
            .WithPosition()
            .WithSixKin()
            .WithHiddenDeity()
            .WithSixSpirit()
            .WithSymbolicStars()
            .Build();

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Original);
        Assert.NotNull(divination.SymbolicStars);
    }

    [Fact]
    public void Builder_CustomStepsOnly_ShouldWork()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act - 只添加纳甲和六亲
        var divination = SixLineDivination.CreateBuilder(TestInquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithNajia()
            .WithSixKin()
            .Build();

        // Assert
        Assert.NotNull(divination);
        Assert.Null(divination.SymbolicStars);  // 没有配置神煞步骤
        Assert.All(divination.Original.Lines, line =>
        {
            Assert.NotNull(line.StemBranch);
            Assert.NotNull(line.SixKin);
            Assert.Null(line.SixSpirit);  // 没有配置六神步骤
        });
    }

    [Fact]
    public void Builder_MultipleCalls_ShouldReturnSameInstance()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var builder = SixLineDivination.CreateBuilder(TestInquiryTime);

        // Act
        var result1 = builder.UseFourSymbols(fourSymbols);
        var result2 = builder.WithNajia();

        // Assert - 流式 API 应该返回同一个 builder 实例
        Assert.Same(builder, result1);
        Assert.Same(builder, result2);
    }
}
