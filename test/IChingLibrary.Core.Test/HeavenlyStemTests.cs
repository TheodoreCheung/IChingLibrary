using IChingLibrary.Core;

namespace IChingLibrary.Core.Test;

public class HeavenlyStemTests
{
    [Fact]
    public void HeavenlyStem_Values_ShouldReturnCorrectValues()
    {
        // Assert
        Assert.Equal(1, HeavenlyStem.Jia.Value);   // 甲
        Assert.Equal(2, HeavenlyStem.Yi.Value);     // 乙
        Assert.Equal(3, HeavenlyStem.Bing.Value);   // 丙
        Assert.Equal(4, HeavenlyStem.Ding.Value);   // 丁
        Assert.Equal(5, HeavenlyStem.Wu.Value);     // 戊
        Assert.Equal(6, HeavenlyStem.Ji.Value);     // 己
        Assert.Equal(7, HeavenlyStem.Geng.Value);   // 庚
        Assert.Equal(8, HeavenlyStem.Xin.Value);    // 辛
        Assert.Equal(9, HeavenlyStem.Ren.Value);    // 壬
        Assert.Equal(10, HeavenlyStem.Gui.Value);   // 癸
    }

    [Fact]
    public void HeavenlyStem_YinYang_ShouldReturnCorrectYinYang()
    {
        // Assert - 阳干: 甲丙戊庚壬
        Assert.Equal(YinYang.Yang, HeavenlyStem.Jia.YinYang);
        Assert.Equal(YinYang.Yang, HeavenlyStem.Bing.YinYang);
        Assert.Equal(YinYang.Yang, HeavenlyStem.Wu.YinYang);
        Assert.Equal(YinYang.Yang, HeavenlyStem.Geng.YinYang);
        Assert.Equal(YinYang.Yang, HeavenlyStem.Ren.YinYang);

        // 阴干: 乙丁己辛癸
        Assert.Equal(YinYang.Yin, HeavenlyStem.Yi.YinYang);
        Assert.Equal(YinYang.Yin, HeavenlyStem.Ding.YinYang);
        Assert.Equal(YinYang.Yin, HeavenlyStem.Ji.YinYang);
        Assert.Equal(YinYang.Yin, HeavenlyStem.Xin.YinYang);
        Assert.Equal(YinYang.Yin, HeavenlyStem.Gui.YinYang);
    }

    [Fact]
    public void HeavenlyStem_FivePhase_ShouldReturnCorrectFivePhase()
    {
        // Assert
        Assert.Equal(FivePhase.Wood, HeavenlyStem.Jia.FivePhase);    // 甲木
        Assert.Equal(FivePhase.Wood, HeavenlyStem.Yi.FivePhase);     // 乙木
        Assert.Equal(FivePhase.Fire, HeavenlyStem.Bing.FivePhase);   // 丙火
        Assert.Equal(FivePhase.Fire, HeavenlyStem.Ding.FivePhase);   // 丁火
        Assert.Equal(FivePhase.Earth, HeavenlyStem.Wu.FivePhase);    // 戊土
        Assert.Equal(FivePhase.Earth, HeavenlyStem.Ji.FivePhase);    // 己土
        Assert.Equal(FivePhase.Metal, HeavenlyStem.Geng.FivePhase);  // 庚金
        Assert.Equal(FivePhase.Metal, HeavenlyStem.Xin.FivePhase);   // 辛金
        Assert.Equal(FivePhase.Water, HeavenlyStem.Ren.FivePhase);   // 壬水
        Assert.Equal(FivePhase.Water, HeavenlyStem.Gui.FivePhase);   // 癸水
    }

    [Fact]
    public void HeavenlyStem_GetAll_ShouldReturnAllElements()
    {
        // Act
        var all = HeavenlyStem.GetAll().ToList();

        // Assert
        Assert.Equal(10, all.Count);
    }

    [Fact]
    public void HeavenlyStem_FromValue_ShouldReturnCorrectElement()
    {
        // Act & Assert
        Assert.Equal(HeavenlyStem.Jia, HeavenlyStem.FromValue(1));
        Assert.Equal(HeavenlyStem.Yi, HeavenlyStem.FromValue(2));
        Assert.Equal(HeavenlyStem.Bing, HeavenlyStem.FromValue(3));
        Assert.Equal(HeavenlyStem.Ding, HeavenlyStem.FromValue(4));
        Assert.Equal(HeavenlyStem.Wu, HeavenlyStem.FromValue(5));
        Assert.Equal(HeavenlyStem.Ji, HeavenlyStem.FromValue(6));
        Assert.Equal(HeavenlyStem.Geng, HeavenlyStem.FromValue(7));
        Assert.Equal(HeavenlyStem.Xin, HeavenlyStem.FromValue(8));
        Assert.Equal(HeavenlyStem.Ren, HeavenlyStem.FromValue(9));
        Assert.Equal(HeavenlyStem.Gui, HeavenlyStem.FromValue(10));
    }

    #region 天干相合测试

    [Fact]
    public void HeavenlyStem_IsCombining_ShouldReturnTrueForCorrectPairs()
    {
        // 甲己合
        Assert.True(HeavenlyStem.Jia.IsCombining(HeavenlyStem.Ji));
        Assert.True(HeavenlyStem.Ji.IsCombining(HeavenlyStem.Jia));

        // 乙庚合
        Assert.True(HeavenlyStem.Yi.IsCombining(HeavenlyStem.Geng));
        Assert.True(HeavenlyStem.Geng.IsCombining(HeavenlyStem.Yi));

        // 丙辛合
        Assert.True(HeavenlyStem.Bing.IsCombining(HeavenlyStem.Xin));
        Assert.True(HeavenlyStem.Xin.IsCombining(HeavenlyStem.Bing));

        // 丁壬合
        Assert.True(HeavenlyStem.Ding.IsCombining(HeavenlyStem.Ren));
        Assert.True(HeavenlyStem.Ren.IsCombining(HeavenlyStem.Ding));

        // 戊癸合
        Assert.True(HeavenlyStem.Wu.IsCombining(HeavenlyStem.Gui));
        Assert.True(HeavenlyStem.Gui.IsCombining(HeavenlyStem.Wu));
    }

    [Fact]
    public void HeavenlyStem_IsCombining_ShouldReturnFalseForIncorrectPairs()
    {
        Assert.False(HeavenlyStem.Jia.IsCombining(HeavenlyStem.Yi));
        Assert.False(HeavenlyStem.Bing.IsCombining(HeavenlyStem.Ding));
    }

    #endregion

    #region 天干相冲测试

    [Fact]
    public void HeavenlyStem_IsClashing_ShouldReturnTrueForCorrectPairs()
    {
        // 甲庚冲
        Assert.True(HeavenlyStem.Jia.IsClashing(HeavenlyStem.Geng));
        Assert.True(HeavenlyStem.Geng.IsClashing(HeavenlyStem.Jia));

        // 乙辛冲
        Assert.True(HeavenlyStem.Yi.IsClashing(HeavenlyStem.Xin));
        Assert.True(HeavenlyStem.Xin.IsClashing(HeavenlyStem.Yi));

        // 丙壬冲
        Assert.True(HeavenlyStem.Bing.IsClashing(HeavenlyStem.Ren));
        Assert.True(HeavenlyStem.Ren.IsClashing(HeavenlyStem.Bing));

        // 丁癸冲
        Assert.True(HeavenlyStem.Ding.IsClashing(HeavenlyStem.Gui));
        Assert.True(HeavenlyStem.Gui.IsClashing(HeavenlyStem.Ding));
    }

    [Fact]
    public void HeavenlyStem_IsClashing_ShouldReturnFalseForIncorrectPairs()
    {
        Assert.False(HeavenlyStem.Jia.IsClashing(HeavenlyStem.Yi));
        Assert.False(HeavenlyStem.Bing.IsClashing(HeavenlyStem.Ding));
    }

    #endregion

    #region 天干相生相克测试

    [Fact]
    public void HeavenlyStem_Generates_ShouldWorkThroughFivePhase()
    {
        // 甲木生丙火
        Assert.True(HeavenlyStem.Jia.Generates(HeavenlyStem.Bing));
        // 丙火生戊土
        Assert.True(HeavenlyStem.Bing.Generates(HeavenlyStem.Wu));
    }

    [Fact]
    public void HeavenlyStem_Restrains_ShouldWorkThroughFivePhase()
    {
        // 甲木克戊土
        Assert.True(HeavenlyStem.Jia.Restrains(HeavenlyStem.Wu));
        // 戊土克壬水
        Assert.True(HeavenlyStem.Wu.Restrains(HeavenlyStem.Ren));
    }

    #endregion
}
