using System.Globalization;
using IChingLibrary.Core;

namespace IChingLibrary.Core.Test;

public class TrigramTests
{
    private static readonly CultureInfo En = new("en");

    [Fact]
    public void Trigram_Values_ShouldReturnCorrectValues()
    {
        // Assert - 八卦的二进制值 (从下到上三爻)
        Assert.Equal(0b111, Trigram.Qian.Value);  // 乾: 111
        Assert.Equal(0b011, Trigram.Dui.Value);   // 兑: 011
        Assert.Equal(0b101, Trigram.Li.Value);    // 离: 101
        Assert.Equal(0b001, Trigram.Zhen.Value);  // 震: 001
        Assert.Equal(0b110, Trigram.Xun.Value);   // 巽: 110
        Assert.Equal(0b010, Trigram.Kan.Value);   // 坎: 010
        Assert.Equal(0b100, Trigram.Gen.Value);   // 艮: 100
        Assert.Equal(0b000, Trigram.Kun.Value);   // 坤: 000
    }

    [Fact]
    public void Trigram_ToString_ShouldReturnLabel()
    {
        // Assert
        Assert.Equal("Qian", Trigram.Qian.ToString(En));
        Assert.Equal("Dui", Trigram.Dui.ToString(En));
        Assert.Equal("Li", Trigram.Li.ToString(En));
        Assert.Equal("Zhen", Trigram.Zhen.ToString(En));
        Assert.Equal("Xun", Trigram.Xun.ToString(En));
        Assert.Equal("Kan", Trigram.Kan.ToString(En));
        Assert.Equal("Gen", Trigram.Gen.ToString(En));
        Assert.Equal("Kun", Trigram.Kun.ToString(En));
    }

    [Fact]
    public void Trigram_FivePhase_ShouldReturnCorrectFivePhase()
    {
        // Assert - 八卦对应五行
        Assert.Equal(FivePhase.Metal, Trigram.Qian.FivePhase);  // 乾属金
        Assert.Equal(FivePhase.Metal, Trigram.Dui.FivePhase);   // 兑属金
        Assert.Equal(FivePhase.Fire, Trigram.Li.FivePhase);     // 离属火
        Assert.Equal(FivePhase.Wood, Trigram.Zhen.FivePhase);   // 震属木
        Assert.Equal(FivePhase.Wood, Trigram.Xun.FivePhase);    // 巽属木
        Assert.Equal(FivePhase.Water, Trigram.Kan.FivePhase);   // 坎属水
        Assert.Equal(FivePhase.Earth, Trigram.Gen.FivePhase);   // 艮属土
        Assert.Equal(FivePhase.Earth, Trigram.Kun.FivePhase);   // 坤属土
    }

    [Fact]
    public void Trigram_GetAll_ShouldReturnAllElements()
    {
        // Act
        var all = Trigram.GetAll().ToList();

        // Assert
        Assert.Equal(8, all.Count);
        Assert.Contains(Trigram.Qian, all);
        Assert.Contains(Trigram.Dui, all);
        Assert.Contains(Trigram.Li, all);
        Assert.Contains(Trigram.Zhen, all);
        Assert.Contains(Trigram.Xun, all);
        Assert.Contains(Trigram.Kan, all);
        Assert.Contains(Trigram.Gen, all);
        Assert.Contains(Trigram.Kun, all);
    }

    [Fact]
    public void Trigram_FromValue_ShouldReturnCorrectElement()
    {
        // Act & Assert
        Assert.Equal(Trigram.Qian, Trigram.FromValue(0b111));
        Assert.Equal(Trigram.Dui, Trigram.FromValue(0b011));
        Assert.Equal(Trigram.Li, Trigram.FromValue(0b101));
        Assert.Equal(Trigram.Zhen, Trigram.FromValue(0b001));
        Assert.Equal(Trigram.Xun, Trigram.FromValue(0b110));
        Assert.Equal(Trigram.Kan, Trigram.FromValue(0b010));
        Assert.Equal(Trigram.Gen, Trigram.FromValue(0b100));
        Assert.Equal(Trigram.Kun, Trigram.FromValue(0b000));
    }

    [Fact]
    public void Trigram_AllProperties_ShouldBeCorrect()
    {
        // Arrange
        var testData = new (byte Value, string Label, FivePhase ExpectedPhase)[]
        {
            (0b111, "Qian", FivePhase.Metal),
            (0b011, "Dui", FivePhase.Metal),
            (0b101, "Li", FivePhase.Fire),
            (0b001, "Zhen", FivePhase.Wood),
            (0b110, "Xun", FivePhase.Wood),
            (0b010, "Kan", FivePhase.Water),
            (0b100, "Gen", FivePhase.Earth),
            (0b000, "Kun", FivePhase.Earth)
        };

        // Act & Assert
        foreach (var (value, label, expectedPhase) in testData)
        {
            var trigram = Trigram.FromValue(value);
            Assert.Equal(value, trigram.Value);
            Assert.Equal(label, trigram.ToString(En));
            Assert.Equal(expectedPhase, trigram.FivePhase);
        }
    }
}
