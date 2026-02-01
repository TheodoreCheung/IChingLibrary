namespace IChingLibrary.Core.Test;

public class EarthlyBranchTests
{
    [Fact]
    public void EarthlyBranch_Values_ShouldReturnCorrectValues()
    {
        // Assert - 地支顺序: 子丑寅卯辰巳午未申酉戌亥
        Assert.Equal(1, EarthlyBranch.Zi.Value);    // 子
        Assert.Equal(2, EarthlyBranch.Chou.Value);  // 丑
        Assert.Equal(3, EarthlyBranch.Yin.Value);   // 寅
        Assert.Equal(4, EarthlyBranch.Mao.Value);   // 卯
        Assert.Equal(5, EarthlyBranch.Chen.Value);  // 辰
        Assert.Equal(6, EarthlyBranch.Si.Value);    // 巳
        Assert.Equal(7, EarthlyBranch.Wu.Value);    // 午
        Assert.Equal(8, EarthlyBranch.Wei.Value);   // 未
        Assert.Equal(9, EarthlyBranch.Shen.Value);  // 申
        Assert.Equal(10, EarthlyBranch.You.Value);  // 酉
        Assert.Equal(11, EarthlyBranch.Xu.Value);   // 戌
        Assert.Equal(12, EarthlyBranch.Hai.Value);  // 亥
    }

    [Fact]
    public void EarthlyBranch_YinYang_ShouldReturnCorrectYinYang()
    {
        // 阳支: 子寅辰午申戌
        Assert.Equal(YinYang.Yang, EarthlyBranch.Zi.YinYang);
        Assert.Equal(YinYang.Yang, EarthlyBranch.Yin.YinYang);
        Assert.Equal(YinYang.Yang, EarthlyBranch.Chen.YinYang);
        Assert.Equal(YinYang.Yang, EarthlyBranch.Wu.YinYang);
        Assert.Equal(YinYang.Yang, EarthlyBranch.Shen.YinYang);
        Assert.Equal(YinYang.Yang, EarthlyBranch.Xu.YinYang);

        // 阴支: 丑卯巳未酉亥
        Assert.Equal(YinYang.Yin, EarthlyBranch.Chou.YinYang);
        Assert.Equal(YinYang.Yin, EarthlyBranch.Mao.YinYang);
        Assert.Equal(YinYang.Yin, EarthlyBranch.Si.YinYang);
        Assert.Equal(YinYang.Yin, EarthlyBranch.Wei.YinYang);
        Assert.Equal(YinYang.Yin, EarthlyBranch.You.YinYang);
        Assert.Equal(YinYang.Yin, EarthlyBranch.Hai.YinYang);
    }

    [Fact]
    public void EarthlyBranch_FivePhase_ShouldReturnCorrectFivePhase()
    {
        // Assert
        Assert.Equal(FivePhase.Water, EarthlyBranch.Zi.FivePhase);    // 子水
        Assert.Equal(FivePhase.Earth, EarthlyBranch.Chou.FivePhase);  // 丑土
        Assert.Equal(FivePhase.Wood, EarthlyBranch.Yin.FivePhase);    // 寅木
        Assert.Equal(FivePhase.Wood, EarthlyBranch.Mao.FivePhase);    // 卯木
        Assert.Equal(FivePhase.Earth, EarthlyBranch.Chen.FivePhase);  // 辰土
        Assert.Equal(FivePhase.Fire, EarthlyBranch.Si.FivePhase);     // 巳火
        Assert.Equal(FivePhase.Fire, EarthlyBranch.Wu.FivePhase);     // 午火
        Assert.Equal(FivePhase.Earth, EarthlyBranch.Wei.FivePhase);   // 未土
        Assert.Equal(FivePhase.Metal, EarthlyBranch.Shen.FivePhase);  // 申金
        Assert.Equal(FivePhase.Metal, EarthlyBranch.You.FivePhase);   // 酉金
        Assert.Equal(FivePhase.Earth, EarthlyBranch.Xu.FivePhase);    // 戌土
        Assert.Equal(FivePhase.Water, EarthlyBranch.Hai.FivePhase);   // 亥水
    }

    [Fact]
    public void EarthlyBranch_GetAll_ShouldReturnAllElements()
    {
        // Act
        var all = EarthlyBranch.GetAll().ToList();

        // Assert
        Assert.Equal(12, all.Count);
    }

    [Fact]
    public void EarthlyBranch_FromValue_ShouldReturnCorrectElement()
    {
        // Act & Assert
        Assert.Equal(EarthlyBranch.Zi, EarthlyBranch.FromValue(1));
        Assert.Equal(EarthlyBranch.Chou, EarthlyBranch.FromValue(2));
        Assert.Equal(EarthlyBranch.Yin, EarthlyBranch.FromValue(3));
        Assert.Equal(EarthlyBranch.Mao, EarthlyBranch.FromValue(4));
        Assert.Equal(EarthlyBranch.Chen, EarthlyBranch.FromValue(5));
        Assert.Equal(EarthlyBranch.Si, EarthlyBranch.FromValue(6));
        Assert.Equal(EarthlyBranch.Wu, EarthlyBranch.FromValue(7));
        Assert.Equal(EarthlyBranch.Wei, EarthlyBranch.FromValue(8));
        Assert.Equal(EarthlyBranch.Shen, EarthlyBranch.FromValue(9));
        Assert.Equal(EarthlyBranch.You, EarthlyBranch.FromValue(10));
        Assert.Equal(EarthlyBranch.Xu, EarthlyBranch.FromValue(11));
        Assert.Equal(EarthlyBranch.Hai, EarthlyBranch.FromValue(12));
    }

    #region 地支六合测试

    [Fact]
    public void EarthlyBranch_IsCombining_ShouldReturnTrueForSixHarmonyPairs()
    {
        // 子丑合
        Assert.True(EarthlyBranch.Zi.IsCombining(EarthlyBranch.Chou));
        Assert.True(EarthlyBranch.Chou.IsCombining(EarthlyBranch.Zi));

        // 寅亥合
        Assert.True(EarthlyBranch.Yin.IsCombining(EarthlyBranch.Hai));
        Assert.True(EarthlyBranch.Hai.IsCombining(EarthlyBranch.Yin));

        // 卯戌合
        Assert.True(EarthlyBranch.Mao.IsCombining(EarthlyBranch.Xu));
        Assert.True(EarthlyBranch.Xu.IsCombining(EarthlyBranch.Mao));

        // 辰酉合
        Assert.True(EarthlyBranch.Chen.IsCombining(EarthlyBranch.You));
        Assert.True(EarthlyBranch.You.IsCombining(EarthlyBranch.Chen));

        // 巳申合
        Assert.True(EarthlyBranch.Si.IsCombining(EarthlyBranch.Shen));
        Assert.True(EarthlyBranch.Shen.IsCombining(EarthlyBranch.Si));

        // 午未合
        Assert.True(EarthlyBranch.Wu.IsCombining(EarthlyBranch.Wei));
        Assert.True(EarthlyBranch.Wei.IsCombining(EarthlyBranch.Wu));
    }

    #endregion

    #region 地支三合测试

    [Fact]
    public void EarthlyBranch_IsTriangularCombination_ShouldReturnTrueForThreeHarmonyGroups()
    {
        // 申子辰三合水局
        Assert.True(EarthlyBranch.Shen.IsTriangularCombination(EarthlyBranch.Zi, EarthlyBranch.Chen));
        Assert.True(EarthlyBranch.Zi.IsTriangularCombination(EarthlyBranch.Shen, EarthlyBranch.Chen));
        Assert.True(EarthlyBranch.Chen.IsTriangularCombination(EarthlyBranch.Shen, EarthlyBranch.Zi));

        // 亥卯未三合木局
        Assert.True(EarthlyBranch.Hai.IsTriangularCombination(EarthlyBranch.Mao, EarthlyBranch.Wei));
        Assert.True(EarthlyBranch.Mao.IsTriangularCombination(EarthlyBranch.Hai, EarthlyBranch.Wei));
        Assert.True(EarthlyBranch.Wei.IsTriangularCombination(EarthlyBranch.Hai, EarthlyBranch.Mao));

        // 寅午戌三合火局
        Assert.True(EarthlyBranch.Yin.IsTriangularCombination(EarthlyBranch.Wu, EarthlyBranch.Xu));
        Assert.True(EarthlyBranch.Wu.IsTriangularCombination(EarthlyBranch.Yin, EarthlyBranch.Xu));
        Assert.True(EarthlyBranch.Xu.IsTriangularCombination(EarthlyBranch.Yin, EarthlyBranch.Wu));

        // 巳酉丑三合金局
        Assert.True(EarthlyBranch.Si.IsTriangularCombination(EarthlyBranch.You, EarthlyBranch.Chou));
        Assert.True(EarthlyBranch.You.IsTriangularCombination(EarthlyBranch.Si, EarthlyBranch.Chou));
        Assert.True(EarthlyBranch.Chou.IsTriangularCombination(EarthlyBranch.Si, EarthlyBranch.You));
    }

    [Fact]
    public void EarthlyBranch_IsTriangularCombination_ShouldReturnFalseForNonHarmonyGroups()
    {
        // 测试非三合局组合应返回 false
        Assert.False(EarthlyBranch.Zi.IsTriangularCombination(EarthlyBranch.Chou, EarthlyBranch.Yin)); // 子丑寅不是三合
        Assert.False(EarthlyBranch.Mao.IsTriangularCombination(EarthlyBranch.Chen, EarthlyBranch.Si)); // 卯辰巳不是三合
        Assert.False(EarthlyBranch.Wu.IsTriangularCombination(EarthlyBranch.Wei, EarthlyBranch.Shen)); // 午未申不是三合
    }

    #endregion

    #region 地支相冲测试

    [Fact]
    public void EarthlyBranch_IsClashing_ShouldReturnTrueForCorrectPairs()
    {
        // 子午冲
        Assert.True(EarthlyBranch.Zi.IsClashing(EarthlyBranch.Wu));
        Assert.True(EarthlyBranch.Wu.IsClashing(EarthlyBranch.Zi));

        // 丑未冲
        Assert.True(EarthlyBranch.Chou.IsClashing(EarthlyBranch.Wei));
        Assert.True(EarthlyBranch.Wei.IsClashing(EarthlyBranch.Chou));

        // 寅申冲
        Assert.True(EarthlyBranch.Yin.IsClashing(EarthlyBranch.Shen));
        Assert.True(EarthlyBranch.Shen.IsClashing(EarthlyBranch.Yin));

        // 卯酉冲
        Assert.True(EarthlyBranch.Mao.IsClashing(EarthlyBranch.You));
        Assert.True(EarthlyBranch.You.IsClashing(EarthlyBranch.Mao));

        // 辰戌冲
        Assert.True(EarthlyBranch.Chen.IsClashing(EarthlyBranch.Xu));
        Assert.True(EarthlyBranch.Xu.IsClashing(EarthlyBranch.Chen));

        // 巳亥冲
        Assert.True(EarthlyBranch.Si.IsClashing(EarthlyBranch.Hai));
        Assert.True(EarthlyBranch.Hai.IsClashing(EarthlyBranch.Si));
    }

    #endregion

    #region 地支相生相克测试

    [Fact]
    public void EarthlyBranch_Generates_ShouldWorkThroughFivePhase()
    {
        // 水生木：子、亥 → 寅、卯
        Assert.True(EarthlyBranch.Zi.Generates(EarthlyBranch.Yin));
        Assert.True(EarthlyBranch.Zi.Generates(EarthlyBranch.Mao));
        Assert.True(EarthlyBranch.Hai.Generates(EarthlyBranch.Yin));
        Assert.True(EarthlyBranch.Hai.Generates(EarthlyBranch.Mao));

        // 木生火：寅、卯 → 巳、午
        Assert.True(EarthlyBranch.Yin.Generates(EarthlyBranch.Si));
        Assert.True(EarthlyBranch.Yin.Generates(EarthlyBranch.Wu));
        Assert.True(EarthlyBranch.Mao.Generates(EarthlyBranch.Si));
        Assert.True(EarthlyBranch.Mao.Generates(EarthlyBranch.Wu));

        // 火生土：巳、午 → 丑、辰、未、戌
        Assert.True(EarthlyBranch.Si.Generates(EarthlyBranch.Chou));
        Assert.True(EarthlyBranch.Si.Generates(EarthlyBranch.Chen));
        Assert.True(EarthlyBranch.Si.Generates(EarthlyBranch.Wei));
        Assert.True(EarthlyBranch.Si.Generates(EarthlyBranch.Xu));
        Assert.True(EarthlyBranch.Wu.Generates(EarthlyBranch.Chou));
        Assert.True(EarthlyBranch.Wu.Generates(EarthlyBranch.Chen));
        Assert.True(EarthlyBranch.Wu.Generates(EarthlyBranch.Wei));
        Assert.True(EarthlyBranch.Wu.Generates(EarthlyBranch.Xu));

        // 土生金：丑、辰、未、戌 → 申、酉
        Assert.True(EarthlyBranch.Chou.Generates(EarthlyBranch.Shen));
        Assert.True(EarthlyBranch.Chou.Generates(EarthlyBranch.You));
        Assert.True(EarthlyBranch.Chen.Generates(EarthlyBranch.Shen));
        Assert.True(EarthlyBranch.Chen.Generates(EarthlyBranch.You));
        Assert.True(EarthlyBranch.Wei.Generates(EarthlyBranch.Shen));
        Assert.True(EarthlyBranch.Wei.Generates(EarthlyBranch.You));
        Assert.True(EarthlyBranch.Xu.Generates(EarthlyBranch.Shen));
        Assert.True(EarthlyBranch.Xu.Generates(EarthlyBranch.You));

        // 金生水：申、酉 → 子、亥
        Assert.True(EarthlyBranch.Shen.Generates(EarthlyBranch.Zi));
        Assert.True(EarthlyBranch.Shen.Generates(EarthlyBranch.Hai));
        Assert.True(EarthlyBranch.You.Generates(EarthlyBranch.Zi));
        Assert.True(EarthlyBranch.You.Generates(EarthlyBranch.Hai));
    }

    [Fact]
    public void EarthlyBranch_GeneratesBy_ShouldWorkThroughFivePhase()
    {
        // 木被水生：寅、卯 ← 子、亥
        Assert.True(EarthlyBranch.Yin.GeneratesBy(EarthlyBranch.Zi));
        Assert.True(EarthlyBranch.Yin.GeneratesBy(EarthlyBranch.Hai));
        Assert.True(EarthlyBranch.Mao.GeneratesBy(EarthlyBranch.Zi));
        Assert.True(EarthlyBranch.Mao.GeneratesBy(EarthlyBranch.Hai));

        // 火被木生：巳、午 ← 寅、卯
        Assert.True(EarthlyBranch.Si.GeneratesBy(EarthlyBranch.Yin));
        Assert.True(EarthlyBranch.Si.GeneratesBy(EarthlyBranch.Mao));
        Assert.True(EarthlyBranch.Wu.GeneratesBy(EarthlyBranch.Yin));
        Assert.True(EarthlyBranch.Wu.GeneratesBy(EarthlyBranch.Mao));

        // 土被火生：丑、辰、未、戌 ← 巳、午
        Assert.True(EarthlyBranch.Chou.GeneratesBy(EarthlyBranch.Si));
        Assert.True(EarthlyBranch.Chou.GeneratesBy(EarthlyBranch.Wu));
        Assert.True(EarthlyBranch.Chen.GeneratesBy(EarthlyBranch.Si));
        Assert.True(EarthlyBranch.Chen.GeneratesBy(EarthlyBranch.Wu));
        Assert.True(EarthlyBranch.Wei.GeneratesBy(EarthlyBranch.Si));
        Assert.True(EarthlyBranch.Wei.GeneratesBy(EarthlyBranch.Wu));
        Assert.True(EarthlyBranch.Xu.GeneratesBy(EarthlyBranch.Si));
        Assert.True(EarthlyBranch.Xu.GeneratesBy(EarthlyBranch.Wu));

        // 金被土生：申、酉 ← 丑、辰、未、戌
        Assert.True(EarthlyBranch.Shen.GeneratesBy(EarthlyBranch.Chou));
        Assert.True(EarthlyBranch.Shen.GeneratesBy(EarthlyBranch.Chen));
        Assert.True(EarthlyBranch.Shen.GeneratesBy(EarthlyBranch.Wei));
        Assert.True(EarthlyBranch.Shen.GeneratesBy(EarthlyBranch.Xu));
        Assert.True(EarthlyBranch.You.GeneratesBy(EarthlyBranch.Chou));
        Assert.True(EarthlyBranch.You.GeneratesBy(EarthlyBranch.Chen));
        Assert.True(EarthlyBranch.You.GeneratesBy(EarthlyBranch.Wei));
        Assert.True(EarthlyBranch.You.GeneratesBy(EarthlyBranch.Xu));

        // 水被金生：子、亥 ← 申、酉
        Assert.True(EarthlyBranch.Zi.GeneratesBy(EarthlyBranch.Shen));
        Assert.True(EarthlyBranch.Zi.GeneratesBy(EarthlyBranch.You));
        Assert.True(EarthlyBranch.Hai.GeneratesBy(EarthlyBranch.Shen));
        Assert.True(EarthlyBranch.Hai.GeneratesBy(EarthlyBranch.You));
    }

    [Fact]
    public void EarthlyBranch_Restrains_ShouldWorkThroughFivePhase()
    {
        // 木克土：寅、卯 → 丑、辰、未、戌
        Assert.True(EarthlyBranch.Yin.Restrains(EarthlyBranch.Chou));
        Assert.True(EarthlyBranch.Yin.Restrains(EarthlyBranch.Chen));
        Assert.True(EarthlyBranch.Yin.Restrains(EarthlyBranch.Wei));
        Assert.True(EarthlyBranch.Yin.Restrains(EarthlyBranch.Xu));
        Assert.True(EarthlyBranch.Mao.Restrains(EarthlyBranch.Chou));
        Assert.True(EarthlyBranch.Mao.Restrains(EarthlyBranch.Chen));
        Assert.True(EarthlyBranch.Mao.Restrains(EarthlyBranch.Wei));
        Assert.True(EarthlyBranch.Mao.Restrains(EarthlyBranch.Xu));

        // 土克水：丑、辰、未、戌 → 子、亥
        Assert.True(EarthlyBranch.Chou.Restrains(EarthlyBranch.Zi));
        Assert.True(EarthlyBranch.Chou.Restrains(EarthlyBranch.Hai));
        Assert.True(EarthlyBranch.Chen.Restrains(EarthlyBranch.Zi));
        Assert.True(EarthlyBranch.Chen.Restrains(EarthlyBranch.Hai));
        Assert.True(EarthlyBranch.Wei.Restrains(EarthlyBranch.Zi));
        Assert.True(EarthlyBranch.Wei.Restrains(EarthlyBranch.Hai));
        Assert.True(EarthlyBranch.Xu.Restrains(EarthlyBranch.Zi));
        Assert.True(EarthlyBranch.Xu.Restrains(EarthlyBranch.Hai));

        // 水克火：子、亥 → 巳、午
        Assert.True(EarthlyBranch.Zi.Restrains(EarthlyBranch.Si));
        Assert.True(EarthlyBranch.Zi.Restrains(EarthlyBranch.Wu));
        Assert.True(EarthlyBranch.Hai.Restrains(EarthlyBranch.Si));
        Assert.True(EarthlyBranch.Hai.Restrains(EarthlyBranch.Wu));

        // 火克金：巳、午 → 申、酉
        Assert.True(EarthlyBranch.Si.Restrains(EarthlyBranch.Shen));
        Assert.True(EarthlyBranch.Si.Restrains(EarthlyBranch.You));
        Assert.True(EarthlyBranch.Wu.Restrains(EarthlyBranch.Shen));
        Assert.True(EarthlyBranch.Wu.Restrains(EarthlyBranch.You));

        // 金克木：申、酉 → 寅、卯
        Assert.True(EarthlyBranch.Shen.Restrains(EarthlyBranch.Yin));
        Assert.True(EarthlyBranch.Shen.Restrains(EarthlyBranch.Mao));
        Assert.True(EarthlyBranch.You.Restrains(EarthlyBranch.Yin));
        Assert.True(EarthlyBranch.You.Restrains(EarthlyBranch.Mao));
    }

    [Fact]
    public void EarthlyBranch_RestrainsBy_ShouldWorkThroughFivePhase()
    {
        // 土被木克：丑、辰、未、戌 ← 寅、卯
        Assert.True(EarthlyBranch.Chou.RestrainsBy(EarthlyBranch.Yin));
        Assert.True(EarthlyBranch.Chou.RestrainsBy(EarthlyBranch.Mao));
        Assert.True(EarthlyBranch.Chen.RestrainsBy(EarthlyBranch.Yin));
        Assert.True(EarthlyBranch.Chen.RestrainsBy(EarthlyBranch.Mao));
        Assert.True(EarthlyBranch.Wei.RestrainsBy(EarthlyBranch.Yin));
        Assert.True(EarthlyBranch.Wei.RestrainsBy(EarthlyBranch.Mao));
        Assert.True(EarthlyBranch.Xu.RestrainsBy(EarthlyBranch.Yin));
        Assert.True(EarthlyBranch.Xu.RestrainsBy(EarthlyBranch.Mao));

        // 水被土克：子、亥 ← 丑、辰、未、戌
        Assert.True(EarthlyBranch.Zi.RestrainsBy(EarthlyBranch.Chou));
        Assert.True(EarthlyBranch.Zi.RestrainsBy(EarthlyBranch.Chen));
        Assert.True(EarthlyBranch.Zi.RestrainsBy(EarthlyBranch.Wei));
        Assert.True(EarthlyBranch.Zi.RestrainsBy(EarthlyBranch.Xu));
        Assert.True(EarthlyBranch.Hai.RestrainsBy(EarthlyBranch.Chou));
        Assert.True(EarthlyBranch.Hai.RestrainsBy(EarthlyBranch.Chen));
        Assert.True(EarthlyBranch.Hai.RestrainsBy(EarthlyBranch.Wei));
        Assert.True(EarthlyBranch.Hai.RestrainsBy(EarthlyBranch.Xu));

        // 火被水克：巳、午 ← 子、亥
        Assert.True(EarthlyBranch.Si.RestrainsBy(EarthlyBranch.Zi));
        Assert.True(EarthlyBranch.Si.RestrainsBy(EarthlyBranch.Hai));
        Assert.True(EarthlyBranch.Wu.RestrainsBy(EarthlyBranch.Zi));
        Assert.True(EarthlyBranch.Wu.RestrainsBy(EarthlyBranch.Hai));

        // 金被火克：申、酉 ← 巳、午
        Assert.True(EarthlyBranch.Shen.RestrainsBy(EarthlyBranch.Si));
        Assert.True(EarthlyBranch.Shen.RestrainsBy(EarthlyBranch.Wu));
        Assert.True(EarthlyBranch.You.RestrainsBy(EarthlyBranch.Si));
        Assert.True(EarthlyBranch.You.RestrainsBy(EarthlyBranch.Wu));

        // 木被金克：寅、卯 ← 申、酉
        Assert.True(EarthlyBranch.Yin.RestrainsBy(EarthlyBranch.Shen));
        Assert.True(EarthlyBranch.Yin.RestrainsBy(EarthlyBranch.You));
        Assert.True(EarthlyBranch.Mao.RestrainsBy(EarthlyBranch.Shen));
        Assert.True(EarthlyBranch.Mao.RestrainsBy(EarthlyBranch.You));
    }

    #endregion
}
