using IChingLibrary.Core;

namespace IChingLibrary.SixLines.Test;

public class PositionTests
{
    [Fact]
    public void Position_Value_ShouldReturnCorrectValue()
    {
        Assert.Equal(1, Position.Worldly.Value);
        Assert.Equal(2, Position.Corresponding.Value);
    }

    [Fact]
    public void Position_ToString_ShouldReturnLabel()
    {
        Assert.Equal("Worldly", Position.Worldly.Label);
        Assert.Equal("Corresponding", Position.Corresponding.Label);
    }

    [Fact]
    public void Position_Equals_ShouldWorkCorrectly()
    {
        var worldly1 = Position.Worldly;
        var worldly2 = Position.Worldly;
        var corresponding = Position.Corresponding;

        Assert.Equal(worldly1, worldly2);
        Assert.NotEqual(worldly1, corresponding);
        Assert.True(worldly1 == worldly2);
        Assert.True(worldly1 != corresponding);
    }

    [Fact]
    public void Position_GetAll_ShouldReturnAllElements()
    {
        var all = Position.GetAll().ToList();

        Assert.Equal(2, all.Count);
        Assert.Contains(Position.Worldly, all);
        Assert.Contains(Position.Corresponding, all);
    }

    [Fact]
    public void Position_FromValue_ShouldReturnCorrectElement()
    {
        Assert.Equal(Position.Worldly, Position.FromValue(1));
        Assert.Equal(Position.Corresponding, Position.FromValue(2));
    }
}
