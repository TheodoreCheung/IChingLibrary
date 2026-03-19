using System.Globalization;
using IChingLibrary.Core;

namespace IChingLibrary.SixLines.Test;

public class CastingTimeTests
{
    public static readonly CultureInfo En = new("en");

    [Fact]
    public void InquiryTime_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        var solar = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var lunar = new DateTimeOffset(2023, 11, 20, 12, 0, 0, TimeSpan.Zero);
        var stemBranch = new LunarStemBranch(
            new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi),
            new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi),
            new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi),
            new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi)
        );

        // Act
        var inquiryTime = new CastingTime(solar, lunar, stemBranch);

        // Assert
        Assert.Equal(solar, inquiryTime.Solar);
        Assert.Equal(lunar, inquiryTime.Lunar);
        Assert.Equal(stemBranch, inquiryTime.StemBranch);
    }

    [Fact]
    public void InquiryTime_ShouldBeReadOnlyStruct()
    {
        // Arrange
        var solar = new DateTimeOffset(2024, 1, 1, 12, 0, 0, TimeSpan.Zero);
        var lunar = new DateTimeOffset(2023, 11, 20, 12, 0, 0, TimeSpan.Zero);
        var stemBranch = new LunarStemBranch(
            new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi),
            new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi),
            new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi),
            new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi)
        );

        // Act
        var inquiryTime = new CastingTime(solar, lunar, stemBranch);
        var copy = inquiryTime;

        // Assert
        Assert.Equal(inquiryTime.Solar, copy.Solar);
        Assert.Equal(inquiryTime.Lunar, copy.Lunar);
        Assert.Equal(inquiryTime.StemBranch, copy.StemBranch);
    }

    [Fact]
    public void ConvertFrom_SameInstantDifferentOffset_ShouldProduceDifferentLunarWallClock()
    {
        // Arrange - 同一绝对时刻，但输入方看到的本地时间不同
        var utc = new DateTimeOffset(2024, 2, 10, 18, 30, 0, TimeSpan.Zero);
        var plusEight = utc.ToOffset(TimeSpan.FromHours(8));

        // Act
        var utcResult = CastingTime.ConvertFrom(utc);
        var plusEightResult = CastingTime.ConvertFrom(plusEight);

        // Assert - 按调用方 wall-clock 计算时，农历本地时间和时辰都应不同
        Assert.NotEqual(utcResult.Lunar.DateTime, plusEightResult.Lunar.DateTime);
        Assert.NotEqual(utcResult.StemBranch.Hour.Branch, plusEightResult.StemBranch.Hour.Branch);
    }
}

public class LunarStemBranchTests
{
    [Fact]
    public void LunarStemBranch_Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        var year = new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi);
        var month = new StemBranch(HeavenlyStem.Yi, EarthlyBranch.Chou);
        var day = new StemBranch(HeavenlyStem.Bing, EarthlyBranch.Yin);
        var hour = new StemBranch(HeavenlyStem.Ding, EarthlyBranch.Mao);

        // Act
        var lunarStemBranch = new LunarStemBranch(year, month, day, hour);

        // Assert
        Assert.Equal(year, lunarStemBranch.Year);
        Assert.Equal(month, lunarStemBranch.Month);
        Assert.Equal(day, lunarStemBranch.Day);
        Assert.Equal(hour, lunarStemBranch.Hour);
    }

    [Fact]
    public void LunarStemBranch_ToString_ShouldReturnFormattedString()
    {
        // Arrange
        var year = new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi);
        var month = new StemBranch(HeavenlyStem.Yi, EarthlyBranch.Chou);
        var day = new StemBranch(HeavenlyStem.Bing, EarthlyBranch.Yin);
        var hour = new StemBranch(HeavenlyStem.Ding, EarthlyBranch.Mao);
        var lunarStemBranch = new LunarStemBranch(year, month, day, hour);

        // Act
        CultureInfo.CurrentUICulture = CastingTimeTests.En;
        var result = lunarStemBranch.ToString();

        // Assert
        Assert.Equal("JiaZi YiChou BingYin DingMao", result);
    }

    [Fact]
    public void LunarStemBranch_ShouldBeReadOnlyStruct()
    {
        // Arrange
        var year = new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi);
        var month = new StemBranch(HeavenlyStem.Yi, EarthlyBranch.Chou);
        var day = new StemBranch(HeavenlyStem.Bing, EarthlyBranch.Yin);
        var hour = new StemBranch(HeavenlyStem.Ding, EarthlyBranch.Mao);
        var lunarStemBranch = new LunarStemBranch(year, month, day, hour);

        // Act
        var copy = lunarStemBranch;

        // Assert
        Assert.Equal(lunarStemBranch.Year, copy.Year);
        Assert.Equal(lunarStemBranch.Month, copy.Month);
        Assert.Equal(lunarStemBranch.Day, copy.Day);
        Assert.Equal(lunarStemBranch.Hour, copy.Hour);
    }
}
