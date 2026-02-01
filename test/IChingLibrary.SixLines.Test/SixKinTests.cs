using IChingLibrary.Core;

namespace IChingLibrary.SixLines.Test;

public class SixKinTests
{
    [Fact]
    public void SixKin_Value_ShouldReturnCorrectValue()
    {
        Assert.Equal(1, SixKin.Parent.Value);
        Assert.Equal(2, SixKin.Sibling.Value);
        Assert.Equal(3, SixKin.Wealth.Value);
        Assert.Equal(4, SixKin.Officer.Value);
        Assert.Equal(5, SixKin.Offspring.Value);
    }

    [Fact]
    public void SixKin_ToString_ShouldReturnLabel()
    {
        Assert.Equal("Parent", SixKin.Parent.ToString());
        Assert.Equal("Sibling", SixKin.Sibling.ToString());
        Assert.Equal("Wealth", SixKin.Wealth.ToString());
        Assert.Equal("Officer", SixKin.Officer.ToString());
        Assert.Equal("Offspring", SixKin.Offspring.ToString());
    }

    [Fact]
    public void SixKin_Equals_ShouldWorkCorrectly()
    {
        var parent1 = SixKin.Parent;
        var parent2 = SixKin.Parent;
        var sibling = SixKin.Sibling;

        Assert.Equal(parent1, parent2);
        Assert.NotEqual(parent1, sibling);
        Assert.True(parent1 == parent2);
        Assert.True(parent1 != sibling);
    }

    [Fact]
    public void SixKin_GetAll_ShouldReturnAllElements()
    {
        var all = SixKin.GetAll().ToList();

        Assert.Equal(5, all.Count);
        Assert.Contains(SixKin.Parent, all);
        Assert.Contains(SixKin.Sibling, all);
        Assert.Contains(SixKin.Wealth, all);
        Assert.Contains(SixKin.Officer, all);
        Assert.Contains(SixKin.Offspring, all);
    }

    [Fact]
    public void SixKin_FromValue_ShouldReturnCorrectElement()
    {
        Assert.Equal(SixKin.Parent, SixKin.FromValue(1));
        Assert.Equal(SixKin.Sibling, SixKin.FromValue(2));
        Assert.Equal(SixKin.Wealth, SixKin.FromValue(3));
        Assert.Equal(SixKin.Officer, SixKin.FromValue(4));
        Assert.Equal(SixKin.Offspring, SixKin.FromValue(5));
    }
}
