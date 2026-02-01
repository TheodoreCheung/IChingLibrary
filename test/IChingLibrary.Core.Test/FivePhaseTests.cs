using System.Globalization;
using IChingLibrary.Core;

namespace IChingLibrary.Core.Test;

public class FivePhaseTests
{
    private static readonly CultureInfo En = new("en");

    [Fact]
    public void FivePhase_Values_ShouldReturnCorrectValues()
    {
        // Assert
        Assert.Equal(1, FivePhase.Metal.Value);
        Assert.Equal(2, FivePhase.Water.Value);
        Assert.Equal(3, FivePhase.Wood.Value);
        Assert.Equal(4, FivePhase.Fire.Value);
        Assert.Equal(5, FivePhase.Earth.Value);
    }

    [Fact]
    public void FivePhase_ToString_ShouldReturnLabel()
    {
        // Assert
        Assert.Equal("Metal", FivePhase.Metal.ToString(En));
        Assert.Equal("Water", FivePhase.Water.ToString(En));
        Assert.Equal("Wood", FivePhase.Wood.ToString(En));
        Assert.Equal("Fire", FivePhase.Fire.ToString(En));
        Assert.Equal("Earth", FivePhase.Earth.ToString(En));
    }

    [Fact]
    public void FivePhase_GetAll_ShouldReturnAllElements()
    {
        // Act
        var all = FivePhase.GetAll().ToList();

        // Assert
        Assert.Equal(5, all.Count);
        Assert.Contains(FivePhase.Metal, all);
        Assert.Contains(FivePhase.Water, all);
        Assert.Contains(FivePhase.Wood, all);
        Assert.Contains(FivePhase.Fire, all);
        Assert.Contains(FivePhase.Earth, all);
    }

    [Fact]
    public void FivePhase_FromValue_ShouldReturnCorrectElement()
    {
        // Act & Assert
        Assert.Equal(FivePhase.Metal, FivePhase.FromValue(1));
        Assert.Equal(FivePhase.Water, FivePhase.FromValue(2));
        Assert.Equal(FivePhase.Wood, FivePhase.FromValue(3));
        Assert.Equal(FivePhase.Fire, FivePhase.FromValue(4));
        Assert.Equal(FivePhase.Earth, FivePhase.FromValue(5));
    }

    #region 五行相生测试

    [Fact]
    public void FivePhase_Generates_ShouldReturnTrueForCorrectPairs()
    {
        // 木生火
        Assert.True(FivePhase.Wood.Generates(FivePhase.Fire));
        // 火生土
        Assert.True(FivePhase.Fire.Generates(FivePhase.Earth));
        // 土生金
        Assert.True(FivePhase.Earth.Generates(FivePhase.Metal));
        // 金生水
        Assert.True(FivePhase.Metal.Generates(FivePhase.Water));
        // 水生木
        Assert.True(FivePhase.Water.Generates(FivePhase.Wood));
    }

    [Fact]
    public void FivePhase_Generates_ShouldReturnFalseForIncorrectPairs()
    {
        // 木不生金
        Assert.False(FivePhase.Wood.Generates(FivePhase.Metal));
        // 火不生水
        Assert.False(FivePhase.Fire.Generates(FivePhase.Water));
    }

    [Fact]
    public void FivePhase_GeneratesBy_ShouldReturnTrueForCorrectPairs()
    {
        // 火被木生
        Assert.True(FivePhase.Fire.GeneratesBy(FivePhase.Wood));
        // 土被火生
        Assert.True(FivePhase.Earth.GeneratesBy(FivePhase.Fire));
        // 金被土生
        Assert.True(FivePhase.Metal.GeneratesBy(FivePhase.Earth));
        // 水被金生
        Assert.True(FivePhase.Water.GeneratesBy(FivePhase.Metal));
        // 木被水生
        Assert.True(FivePhase.Wood.GeneratesBy(FivePhase.Water));
    }

    [Fact]
    public void FivePhase_IsGenerates_ShouldReturnTrueForCorrectPairs()
    {
        Assert.True(FivePhase.Wood.IsGenerates(FivePhase.Fire));
        Assert.True(FivePhase.Fire.IsGenerates(FivePhase.Earth));
        Assert.True(FivePhase.Earth.IsGenerates(FivePhase.Metal));
        Assert.True(FivePhase.Metal.IsGenerates(FivePhase.Water));
        Assert.True(FivePhase.Water.IsGenerates(FivePhase.Wood));
    }

    #endregion

    #region 五行相克测试

    [Fact]
    public void FivePhase_Restrains_ShouldReturnTrueForCorrectPairs()
    {
        // 木克土
        Assert.True(FivePhase.Wood.Restrains(FivePhase.Earth));
        // 土克水
        Assert.True(FivePhase.Earth.Restrains(FivePhase.Water));
        // 水克火
        Assert.True(FivePhase.Water.Restrains(FivePhase.Fire));
        // 火克金
        Assert.True(FivePhase.Fire.Restrains(FivePhase.Metal));
        // 金克木
        Assert.True(FivePhase.Metal.Restrains(FivePhase.Wood));
    }

    [Fact]
    public void FivePhase_Restrains_ShouldReturnFalseForIncorrectPairs()
    {
        // 木不克水
        Assert.False(FivePhase.Wood.Restrains(FivePhase.Water));
        // 火不克土
        Assert.False(FivePhase.Fire.Restrains(FivePhase.Earth));
    }

    [Fact]
    public void FivePhase_RestrainsBy_ShouldReturnTrueForCorrectPairs()
    {
        // 土被木克
        Assert.True(FivePhase.Earth.RestrainsBy(FivePhase.Wood));
        // 水被土克
        Assert.True(FivePhase.Water.RestrainsBy(FivePhase.Earth));
        // 火被水克
        Assert.True(FivePhase.Fire.RestrainsBy(FivePhase.Water));
        // 金被火克
        Assert.True(FivePhase.Metal.RestrainsBy(FivePhase.Fire));
        // 木被金克
        Assert.True(FivePhase.Wood.RestrainsBy(FivePhase.Metal));
    }

    [Fact]
    public void FivePhase_IsRestrains_ShouldReturnTrueForCorrectPairs()
    {
        Assert.True(FivePhase.Wood.IsRestrains(FivePhase.Earth));
        Assert.True(FivePhase.Earth.IsRestrains(FivePhase.Water));
        Assert.True(FivePhase.Water.IsRestrains(FivePhase.Fire));
        Assert.True(FivePhase.Fire.IsRestrains(FivePhase.Metal));
        Assert.True(FivePhase.Metal.IsRestrains(FivePhase.Wood));
    }

    #endregion

    [Fact]
    public void FivePhase_CompleteCycle_ShouldWorkCorrectly()
    {
        // 相生循环: 木 -> 火 -> 土 -> 金 -> 水 -> 木
        Assert.True(FivePhase.Wood.Generates(FivePhase.Fire));
        Assert.True(FivePhase.Fire.Generates(FivePhase.Earth));
        Assert.True(FivePhase.Earth.Generates(FivePhase.Metal));
        Assert.True(FivePhase.Metal.Generates(FivePhase.Water));
        Assert.True(FivePhase.Water.Generates(FivePhase.Wood));

        // 相克循环: 木克土 -> 土克水 -> 水克火 -> 火克金 -> 金克木
        Assert.True(FivePhase.Wood.Restrains(FivePhase.Earth));
        Assert.True(FivePhase.Earth.Restrains(FivePhase.Water));
        Assert.True(FivePhase.Water.Restrains(FivePhase.Fire));
        Assert.True(FivePhase.Fire.Restrains(FivePhase.Metal));
        Assert.True(FivePhase.Metal.Restrains(FivePhase.Wood));
    }
}
