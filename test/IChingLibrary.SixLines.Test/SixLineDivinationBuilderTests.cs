using System.Collections;
using System.Reflection;
using IChingLibrary.Core;
using IChingLibrary.SixLines.Builder;
using Xunit.Abstractions;

namespace IChingLibrary.SixLines.Test;

public class SixLineDivinationBuilderTests
{
    private sealed class LoopStepA : IStructuringStep
    {
        private int _readCount;

        public IEnumerable<Type> RequiredSteps
        {
            get
            {
                if (_readCount++ == 0)
                {
                    return [typeof(LoopStepB)];
                }

                return [];
            }
        }

        public void Execute(DivinationContext context)
        {
        }
    }

    private sealed class LoopStepB : IStructuringStep
    {
        public IEnumerable<Type> RequiredSteps => [typeof(LoopStepA)];

        public void Execute(DivinationContext context)
        {
        }
    }

    private readonly ITestOutputHelper _testOutputHelper;

    public SixLineDivinationBuilderTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private static DateTimeOffset TestCastingTime => new(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

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
        var divination = SixLineDivination.Create(TestCastingTime, fourSymbols);

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
            SixLineDivination.CreateBuilder()
                .UseMethod(new CoinCastingMethod(TestCastingTime, invalidFourSymbols))
                .Build();
        });
    }

    [Fact]
    public void Builder_UseFourSymbols_ByteArray_ShouldAcceptValidArray()
    {
        // Arrange
        var fourSymbolValues = new byte[] { 7, 7, 7, 7, 7, 7 };

        // Act
        var divination = SixLineDivination.Create(TestCastingTime, fourSymbolValues);

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
        Assert.Throws<InvalidOperationException>(() =>
        {
            SixLineDivination.Create(TestCastingTime, invalidValues);
        });
    }

    [Fact]
    public void Builder_UseTimeBasedHexagram_ShouldCreateValidDivination()
    {
        // Act
        var divination = SixLineDivination.Create(TestCastingTime);

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
        var divination = SixLineDivination.Create(TestCastingTime, upperTrigramNumber, lowerTrigramNumber, changingLineNumber);

        // Assert
        Assert.NotNull(divination);
        Assert.NotNull(divination.Original);
    }

    [Fact]
    public void Builder_UseRandomHexagram_NegativeInput_ShouldThrow()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            SixLineDivination.Create(TestCastingTime, -1, 1, 1);
        });
    }

    [Fact]
    public void Builder_UseRandomHexagram_WithoutChangingLine_ShouldCreateValidDivination()
    {
        // Arrange
        var upperTrigramNumber = 5;
        var lowerTrigramNumber = 3;

        // Act
        var divination = SixLineDivination.Create(TestCastingTime, upperTrigramNumber, lowerTrigramNumber);

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
        var divination = SixLineDivination.Create(TestCastingTime, original, changed);

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
        var divination = SixLineDivination.Create(TestCastingTime, original, changed);

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
        var divination = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
            .WithStep(new NajiaStep())
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
        var divination = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
            .WithStep(new NajiaStep())
            .WithStep(new PositionStep())
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
        var divination = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
            .WithStep(new NajiaStep())
            .WithStep(new SixKinStep())
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
        var divination = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
            .WithStep(new NajiaStep())
            .WithStep(new SixSpiritStep())
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
        var divination = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
            .WithStep(new NajiaStep())
            .WithStep(new SixKinStep())
            .WithStep(new HiddenDeityStep())
            .Build();

        // Assert
        // 至少应该运行不抛出异常
        Assert.NotNull(divination);
    }

    [Fact]
    public void Builder_WithHiddenDeity_WithoutSixKin_ShouldThrowClearError()
    {
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        var ex = Assert.Throws<InvalidOperationException>(() =>
        {
            SixLineDivination.CreateBuilder()
                .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
                .WithStep(new NajiaStep())
                .WithStep(new HiddenDeityStep())
            .Build();
        });

        Assert.Contains("SixKin", ex.Message);
    }

    [Fact]
    public void Builder_WithSymbolicStars_ShouldCalculateSymbolicStars()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
            .WithStep(new NajiaStep())
            .WithStep(new SixKinStep())
            .WithStep(new SymbolicStarStep())
            .Build();

        // Assert
        Assert.NotNull(divination.SymbolicStars);
        Assert.NotEmpty(divination.SymbolicStars.AllStars);
    }

    [Fact]
    public void Builder_WithDefaultSteps_ShouldApplyAllSteps()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.Create(TestCastingTime, fourSymbols);

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
    public void Builder_WithDefaultSteps_CalledTwice_ShouldMatchSingleDefaultPipeline()
    {
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        var actual = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
            .WithDefaultSteps()
            .WithDefaultSteps()
            .Build();

        var expected = SixLineDivination.Create(TestCastingTime, fourSymbols);

        Assert.Equal(expected.Original.Meta, actual.Original.Meta);
        Assert.Equal(expected.SymbolicStars!.AllStars.Count, actual.SymbolicStars!.AllStars.Count);
        for (var i = 0; i < 6; i++)
        {
            Assert.Equal(expected.Original[i].StemBranch.Stem, actual.Original[i].StemBranch.Stem);
            Assert.Equal(expected.Original[i].StemBranch.Branch, actual.Original[i].StemBranch.Branch);
            Assert.Equal(expected.Original[i].SixKin, actual.Original[i].SixKin);
            Assert.Equal(expected.Original[i].SixSpirit, actual.Original[i].SixSpirit);
            Assert.Equal(expected.Original[i].Position, actual.Original[i].Position);
            Assert.Equal(expected.Original[i].HiddenDeity?.SixKin, actual.Original[i].HiddenDeity?.SixKin);
        }
    }

    [Fact]
    public void Builder_WithCustomStepThenDefaultSteps_ShouldMatchSingleDefaultPipeline()
    {
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        var actual = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
            .WithStep(new HiddenDeityStep())
            .WithDefaultSteps()
            .Build();

        var expected = SixLineDivination.Create(TestCastingTime, fourSymbols);

        Assert.Equal(expected.Original.Meta, actual.Original.Meta);
        Assert.Equal(expected.SymbolicStars!.AllStars.Count, actual.SymbolicStars!.AllStars.Count);
        for (var i = 0; i < 6; i++)
        {
            Assert.Equal(expected.Original[i].StemBranch.Stem, actual.Original[i].StemBranch.Stem);
            Assert.Equal(expected.Original[i].StemBranch.Branch, actual.Original[i].StemBranch.Branch);
            Assert.Equal(expected.Original[i].SixKin, actual.Original[i].SixKin);
            Assert.Equal(expected.Original[i].SixSpirit, actual.Original[i].SixSpirit);
            Assert.Equal(expected.Original[i].Position, actual.Original[i].Position);
            Assert.Equal(expected.Original[i].HiddenDeity?.SixKin, actual.Original[i].HiddenDeity?.SixKin);
        }
    }

    [Fact]
    public void HiddenDeityStep_ShouldCachePalaceTemplateLazily()
    {
        var cacheField = typeof(HiddenDeityStep).GetField("_palaceTemplateCache", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(cacheField);

        var cache = Assert.IsAssignableFrom<IDictionary>(cacheField!.GetValue(null));
        cache.Clear();

        var cacheLockField = typeof(HiddenDeityStep).GetField("_palaceTemplateCacheLock", BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(cacheLockField);
        Assert.Equal(typeof(System.Threading.Lock), cacheLockField!.FieldType);

        var getTemplateMethod = typeof(HiddenDeityStep).GetMethod(
            "GetOrCreatePalaceTemplate",
            BindingFlags.NonPublic | BindingFlags.Static);
        Assert.NotNull(getTemplateMethod);

        var first = getTemplateMethod!.Invoke(null, [Trigram.Qian]);

        Assert.NotNull(first);
        Assert.True(cache.Contains(Trigram.Qian));
        Assert.True(cache.Count < 8);

        var second = getTemplateMethod.Invoke(null, [Trigram.Qian]);

        Assert.Same(first, second);
        Assert.True(cache.Contains(Trigram.Qian));
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
        var divination = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
            .WithStep(new NajiaStep())
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
        var divination = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
            .WithStep(new NajiaStep())
            .WithStep(new SixKinStep())
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
            SixLineDivination.CreateBuilder()
                .WithDefaultSteps()
                .Build();
        });
    }

    [Fact]
    public void Builder_WithCircularDependencies_ShouldThrowClearError()
    {
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        var ex = Assert.Throws<InvalidOperationException>(() =>
        {
            SixLineDivination.CreateBuilder()
                .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
                .WithStep(new LoopStepA())
                .WithStep(new LoopStepB())
                .Build();
        });

        Assert.Contains("循环", ex.Message);
    }

    [Fact]
    public void Builder_FluentApi_ShouldWork()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();

        // Act
        var divination = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
            .WithStep(new NajiaStep())
            .WithStep(new PositionStep())
            .WithStep(new SixKinStep())
            .WithStep(new HiddenDeityStep())
            .WithStep(new SixSpiritStep())
            .WithStep(new SymbolicStarStep())
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
        var divination = SixLineDivination.CreateBuilder()
            .UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols))
            .WithStep(new NajiaStep())
            .WithStep(new SixKinStep())
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
        var builder = SixLineDivination.CreateBuilder();

        // Act
        var result1 = builder.UseMethod(new CoinCastingMethod(TestCastingTime, fourSymbols));
        var result2 = builder.WithStep(new NajiaStep());

        // Assert - 流式 API 应该返回同一个 builder 实例
        Assert.Same(builder, result1);
        Assert.Same(builder, result2);
    }

    [Fact]
    public void Builder_CoinCastingMethod_SpecifyMethod_ShouldReturnSameDivination()
    {
        var dt = new DateTimeOffset(2026, 3, 3, 13, 30, 00, TimeSpan.FromHours(8));

        var d1 = new SixLineDivinationBuilder()
            .UseMethod(new CoinCastingMethod(dt,
            [
                FourSymbol.YoungYin, FourSymbol.YoungYang, FourSymbol.YoungYin, 
                FourSymbol.YoungYang, FourSymbol.YoungYang, FourSymbol.YoungYang
            ]))
            .WithDefaultSteps()
            .Build();

        var d2 = SixLineDivination.Create(dt, Hexagram.FromValue(0b111010));
        
        Assert.Equal(d1.Original.Meta.Value, d2.Original.Meta.Value);
        for (var i = 0; i < 6; i++)
        {
            Assert.Equal(d1.Original[i].YinYang, d2.Original[i].YinYang);
            Assert.Equal(d1.Original[i].StemBranch.Stem, d2.Original[i].StemBranch.Stem);
            Assert.Equal(d1.Original[i].StemBranch.Branch, d2.Original[i].StemBranch.Branch);
            Assert.Equal(d1.Original[i].SixSpirit, d2.Original[i].SixSpirit);
            Assert.Equal(d1.Original[i].SixKin, d2.Original[i].SixKin);
        }
    }
}
