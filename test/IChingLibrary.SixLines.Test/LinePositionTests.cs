using IChingLibrary.Core;

namespace IChingLibrary.SixLines.Test;

public class LinePositionTests
{
    [Fact]
    public void LinePosition_Value_ShouldReturnCorrectValue()
    {
        Assert.Equal(1, LinePosition.First.Value);
        Assert.Equal(2, LinePosition.Second.Value);
        Assert.Equal(3, LinePosition.Third.Value);
        Assert.Equal(4, LinePosition.Fourth.Value);
        Assert.Equal(5, LinePosition.Fifth.Value);
        Assert.Equal(6, LinePosition.Sixth.Value);
    }

    [Fact]
    public void LinePosition_ToString_ShouldReturnLabel()
    {
        Assert.Equal("First", LinePosition.First.ToString());
        Assert.Equal("Second", LinePosition.Second.ToString());
        Assert.Equal("Third", LinePosition.Third.ToString());
        Assert.Equal("Fourth", LinePosition.Fourth.ToString());
        Assert.Equal("Fifth", LinePosition.Fifth.ToString());
        Assert.Equal("Sixth", LinePosition.Sixth.ToString());
    }

    [Fact]
    public void LinePosition_Equals_ShouldWorkCorrectly()
    {
        var first1 = LinePosition.First;
        var first2 = LinePosition.First;
        var second = LinePosition.Second;

        Assert.Equal(first1, first2);
        Assert.NotEqual(first1, second);
        Assert.True(first1 == first2);
        Assert.True(first1 != second);
    }

    [Fact]
    public void LinePosition_GetAll_ShouldReturnAllElements()
    {
        var all = LinePosition.GetAll().ToList();

        Assert.Equal(6, all.Count);
        Assert.Contains(LinePosition.First, all);
        Assert.Contains(LinePosition.Second, all);
        Assert.Contains(LinePosition.Third, all);
        Assert.Contains(LinePosition.Fourth, all);
        Assert.Contains(LinePosition.Fifth, all);
        Assert.Contains(LinePosition.Sixth, all);
    }

    [Fact]
    public void LinePosition_FromValue_ShouldReturnCorrectElement()
    {
        Assert.Equal(LinePosition.First, LinePosition.FromValue(1));
        Assert.Equal(LinePosition.Second, LinePosition.FromValue(2));
        Assert.Equal(LinePosition.Third, LinePosition.FromValue(3));
        Assert.Equal(LinePosition.Fourth, LinePosition.FromValue(4));
        Assert.Equal(LinePosition.Fifth, LinePosition.FromValue(5));
        Assert.Equal(LinePosition.Sixth, LinePosition.FromValue(6));
    }

    [Fact]
    public void LinePosition_ToArrayIndex_ShouldReturnCorrectIndex()
    {
        Assert.Equal(0, LinePosition.First.ToArrayIndex());
        Assert.Equal(1, LinePosition.Second.ToArrayIndex());
        Assert.Equal(2, LinePosition.Third.ToArrayIndex());
        Assert.Equal(3, LinePosition.Fourth.ToArrayIndex());
        Assert.Equal(4, LinePosition.Fifth.ToArrayIndex());
        Assert.Equal(5, LinePosition.Sixth.ToArrayIndex());
    }

    [Fact]
    public void LinePosition_FromArrayIndex_ShouldReturnCorrectPosition()
    {
        Assert.Equal(LinePosition.First, LinePosition.FromArrayIndex(0));
        Assert.Equal(LinePosition.Second, LinePosition.FromArrayIndex(1));
        Assert.Equal(LinePosition.Third, LinePosition.FromArrayIndex(2));
        Assert.Equal(LinePosition.Fourth, LinePosition.FromArrayIndex(3));
        Assert.Equal(LinePosition.Fifth, LinePosition.FromArrayIndex(4));
        Assert.Equal(LinePosition.Sixth, LinePosition.FromArrayIndex(5));
    }

    [Fact]
    public void LinePosition_FromArrayIndex_InvalidIndex_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => LinePosition.FromArrayIndex(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => LinePosition.FromArrayIndex(6));
        Assert.Throws<ArgumentOutOfRangeException>(() => LinePosition.FromArrayIndex(10));
    }

    [Fact]
    public void LinePosition_RoundTrip_Conversion_ShouldPreserveValue()
    {
        foreach (var position in LinePosition.GetAll())
        {
            var arrayIndex = position.ToArrayIndex();
            var result = LinePosition.FromArrayIndex(arrayIndex);
            Assert.Equal(position, result);
        }
    }
}
