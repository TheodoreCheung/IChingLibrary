using IChingLibrary.Core;

namespace IChingLibrary.SixLines.Test;

public class SymbolicStarCollectionTests
{
    private static DateTimeOffset TestInquiryTime => new(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void SymbolicStarCollection_GetStars_ShouldReturnCorrectBranches()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        // Act & Assert
        var noblemanBranches = divination.SymbolicStars!.GetStars(SymbolicStar.Nobleman);
        Assert.NotNull(noblemanBranches);
        Assert.Equal(2, noblemanBranches.Length);

        var salarySpiritBranches = divination.SymbolicStars.GetStars(SymbolicStar.SalarySpirit);
        Assert.NotNull(salarySpiritBranches);
        Assert.Single(salarySpiritBranches);
    }

    [Fact]
    public void SymbolicStarCollection_GetStars_NonExistentStar_ShouldReturnNull()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        // 创建一个自定义神煞
        var customStar = SymbolicStar.CreateCustom("NonExistent");

        // Act & Assert
        // 自定义神煞不在集合中
        var result = divination.SymbolicStars!.GetStars(customStar);
        Assert.Null(result);
    }

    [Fact]
    public void SymbolicStarCollection_HasStar_ShouldReturnCorrectValue()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        // Act & Assert
        // 甲日贵人：牛羊
        Assert.True(divination.SymbolicStars!.HasStar(EarthlyBranch.Chou, SymbolicStar.Nobleman));
        Assert.True(divination.SymbolicStars.HasStar(EarthlyBranch.Wei, SymbolicStar.Nobleman));
        Assert.False(divination.SymbolicStars.HasStar(EarthlyBranch.Yin, SymbolicStar.Nobleman));

        // 甲日禄神：寅
        Assert.True(divination.SymbolicStars.HasStar(EarthlyBranch.Yin, SymbolicStar.SalarySpirit));
        Assert.False(divination.SymbolicStars.HasStar(EarthlyBranch.Mao, SymbolicStar.SalarySpirit));
    }

    [Fact]
    public void SymbolicStarCollection_GetStarsForBranch_ShouldReturnAllStarsForBranch()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        // Act
        var starsForZi = divination.SymbolicStars!.GetStarsForBranch(EarthlyBranch.Zi).ToList();
        var starsForChen = divination.SymbolicStars.GetStarsForBranch(EarthlyBranch.Chen).ToList();

        // Assert
        // 甲子日应该有一些神煞
        Assert.NotEmpty(starsForZi);

        // 检查是否包含预期的神煞
        // 甲日贵人：牛羊（不含子）
        // 甲日禄神：寅（不含子）
        // 子日驿马：寅（不含子）
        // 结果可能为空或包含其他神煞
    }

    [Fact]
    public void SymbolicStarCollection_AllStars_ShouldReturnAllStars()
    {
        // Arrange
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        // Act
        var allStars = divination.SymbolicStars!.AllStars;

        // Assert
        Assert.Equal(16, allStars.Count);
        Assert.True(allStars.ContainsKey(SymbolicStar.Nobleman));
        Assert.True(allStars.ContainsKey(SymbolicStar.SalarySpirit));
    }

    [Fact]
    public void SymbolicStarCollection_GetStars_ShouldReturnCopyInsteadOfInternalArray()
    {
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        var branches = divination.SymbolicStars!.GetStars(SymbolicStar.Nobleman)!;
        branches[0] = EarthlyBranch.Zi;

        var reread = divination.SymbolicStars.GetStars(SymbolicStar.Nobleman)!;
        Assert.DoesNotContain(EarthlyBranch.Zi, reread);
    }

    [Fact]
    public void SymbolicStarCollection_AllStars_ShouldReturnSnapshot()
    {
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        var allStars = divination.SymbolicStars!.AllStars;
        allStars[SymbolicStar.Nobleman][0] = EarthlyBranch.Zi;

        var reread = divination.SymbolicStars.GetStars(SymbolicStar.Nobleman)!;
        Assert.DoesNotContain(EarthlyBranch.Zi, reread);
        Assert.False(allStars is Dictionary<SymbolicStar, EarthlyBranch[]>);
    }

    [Fact]
    public void SymbolicStarCollection_AllStars_ShouldReturnIndependentSnapshotPerCall()
    {
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        var firstSnapshot = divination.SymbolicStars!.AllStars;
        var secondSnapshot = divination.SymbolicStars.AllStars;
        firstSnapshot[SymbolicStar.Nobleman][0] = EarthlyBranch.Zi;

        Assert.NotSame(firstSnapshot, secondSnapshot);
        Assert.DoesNotContain(EarthlyBranch.Zi, secondSnapshot[SymbolicStar.Nobleman]);
    }

    [Fact]
    public void SymbolicStarCollection_TryGetStarsMemory_ShouldExposeReadOnlyView()
    {
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        var found = divination.SymbolicStars!.TryGetStarsMemory(SymbolicStar.Nobleman, out var branches);

        Assert.True(found);
        Assert.Equal(
            divination.SymbolicStars.GetStars(SymbolicStar.Nobleman)!.Select(branch => branch.Value),
            branches.ToArray().Select(branch => branch.Value));
    }

    [Fact]
    public void SymbolicStarCollection_AllStarsMemory_ShouldExposeAllStarsWithoutSnapshotCopies()
    {
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);

        var allStars = divination.SymbolicStars!.AllStarsMemory;

        Assert.Equal(16, allStars.Count);
        Assert.True(allStars.ContainsKey(SymbolicStar.Nobleman));
        Assert.False(allStars is Dictionary<SymbolicStar, ReadOnlyMemory<EarthlyBranch>>);
        Assert.Equal(
            divination.SymbolicStars.GetStars(SymbolicStar.Nobleman)!.Select(branch => branch.Value),
            allStars[SymbolicStar.Nobleman].ToArray().Select(branch => branch.Value));
    }

    [Fact]
    public void SymbolicStarCollection_AllStarsMemory_ShouldStayInSyncAfterAddAndRemove()
    {
        var fourSymbols = Enumerable.Repeat(FourSymbol.YoungYang, 6).ToArray();
        var divination = SixLineDivination.Create(TestInquiryTime, fourSymbols);
        var customStar = SymbolicStar.CreateCustom("DynamicStar");

        var added = divination.SymbolicStars!.Add(customStar, () => [EarthlyBranch.Zi, EarthlyBranch.Wu]);

        Assert.True(added);
        Assert.True(divination.SymbolicStars.AllStarsMemory.ContainsKey(customStar));
        Assert.Equal(
            [EarthlyBranch.Zi.Value, EarthlyBranch.Wu.Value],
            divination.SymbolicStars.AllStarsMemory[customStar].ToArray().Select(branch => branch.Value));

        var removed = divination.SymbolicStars.Remove(customStar);

        Assert.True(removed);
        Assert.False(divination.SymbolicStars.AllStarsMemory.ContainsKey(customStar));
        Assert.False(divination.SymbolicStars.TryGetStarsMemory(customStar, out _));
    }
}
