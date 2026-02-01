using IChingLibrary.Core;

namespace IChingLibrary.SixLines.Test;

public class SixSpiritTests
{
    [Fact]
    public void SixSpirit_Value_ShouldReturnCorrectValue()
    {
        Assert.Equal(1, SixSpirit.AzureDragon.Value);
        Assert.Equal(2, SixSpirit.VermilionBird.Value);
        Assert.Equal(3, SixSpirit.HookChen.Value);
        Assert.Equal(4, SixSpirit.CoiledSnake.Value);
        Assert.Equal(5, SixSpirit.WhiteTiger.Value);
        Assert.Equal(6, SixSpirit.BlackTortoise.Value);
    }

    [Fact]
    public void SixSpirit_ToString_ShouldReturnLabel()
    {
        Assert.Equal("AzureDragon", SixSpirit.AzureDragon.ToString());
        Assert.Equal("VermilionBird", SixSpirit.VermilionBird.ToString());
        Assert.Equal("HookChen", SixSpirit.HookChen.ToString());
        Assert.Equal("CoiledSnake", SixSpirit.CoiledSnake.ToString());
        Assert.Equal("WhiteTiger", SixSpirit.WhiteTiger.ToString());
        Assert.Equal("BlackTortoise", SixSpirit.BlackTortoise.ToString());
    }

    [Fact]
    public void SixSpirit_Equals_ShouldWorkCorrectly()
    {
        var azureDragon1 = SixSpirit.AzureDragon;
        var azureDragon2 = SixSpirit.AzureDragon;
        var vermilionBird = SixSpirit.VermilionBird;

        Assert.Equal(azureDragon1, azureDragon2);
        Assert.NotEqual(azureDragon1, vermilionBird);
        Assert.True(azureDragon1 == azureDragon2);
        Assert.True(azureDragon1 != vermilionBird);
    }

    [Fact]
    public void SixSpirit_GetAll_ShouldReturnAllElements()
    {
        var all = SixSpirit.GetAll().ToList();

        Assert.Equal(6, all.Count);
        Assert.Contains(SixSpirit.AzureDragon, all);
        Assert.Contains(SixSpirit.VermilionBird, all);
        Assert.Contains(SixSpirit.HookChen, all);
        Assert.Contains(SixSpirit.CoiledSnake, all);
        Assert.Contains(SixSpirit.WhiteTiger, all);
        Assert.Contains(SixSpirit.BlackTortoise, all);
    }

    [Fact]
    public void SixSpirit_FromValue_ShouldReturnCorrectElement()
    {
        Assert.Equal(SixSpirit.AzureDragon, SixSpirit.FromValue(1));
        Assert.Equal(SixSpirit.VermilionBird, SixSpirit.FromValue(2));
        Assert.Equal(SixSpirit.HookChen, SixSpirit.FromValue(3));
        Assert.Equal(SixSpirit.CoiledSnake, SixSpirit.FromValue(4));
        Assert.Equal(SixSpirit.WhiteTiger, SixSpirit.FromValue(5));
        Assert.Equal(SixSpirit.BlackTortoise, SixSpirit.FromValue(6));
    }
}
