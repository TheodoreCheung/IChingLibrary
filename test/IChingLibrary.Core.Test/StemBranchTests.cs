using System.Globalization;
using IChingLibrary.Core;
using IChingLibrary.Core.Localization;

namespace IChingLibrary.Core.Test;

public class StemBranchTests
{
    private static readonly CultureInfo En = new("en");
    private static readonly CultureInfo ZhHans = new("zh-Hans");

    [Fact]
    public void StemBranch_Constructor_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var stem = HeavenlyStem.Jia;
        var branch = EarthlyBranch.Zi;

        // Act
        var stemBranch = new StemBranch(stem, branch);

        // Assert
        Assert.Equal(stem, stemBranch.Stem);
        Assert.Equal(branch, stemBranch.Branch);
    }

    [Fact]
    public void StemBranch_ToString_English_ShouldReturnEnglishTranslation()
    {
        // Arrange
        var stemBranch = new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi);

        // Act - 清理 DefaultCulture 并设置文化
        IChingTranslationManager.DefaultCulture = null;
        CultureInfo.CurrentUICulture = En;
        var result = stemBranch.ToString();

        // Assert
        Assert.Equal("JiaZi", result);
    }

    [Fact]
    public void StemBranch_ToString_Chinese_ShouldReturnChineseTranslation()
    {
        // Arrange
        var stemBranch = new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi);

        // Act - 清理 DefaultCulture 并设置文化
        IChingTranslationManager.DefaultCulture = null;
        CultureInfo.CurrentUICulture = ZhHans;
        var result = stemBranch.ToString();

        // Assert
        Assert.Equal("甲子", result);
    }

    [Fact]
    public void StemBranch_ToString_ShouldWorkForVariousCombinations_English()
    {
        // Arrange
        var testData = new (HeavenlyStem Stem, EarthlyBranch Branch, string Expected)[]
        {
            (HeavenlyStem.Jia, EarthlyBranch.Zi, "JiaZi"),
            (HeavenlyStem.Yi, EarthlyBranch.Chou, "YiChou"),
            (HeavenlyStem.Bing, EarthlyBranch.Yin, "BingYin"),
            (HeavenlyStem.Geng, EarthlyBranch.Shen, "GengShen"),
            (HeavenlyStem.Gui, EarthlyBranch.Hai, "GuiHai")
        };

        // Act & Assert - 清理 DefaultCulture 并设置文化
        IChingTranslationManager.DefaultCulture = null;
        CultureInfo.CurrentUICulture = En;
        foreach (var (stem, branch, expected) in testData)
        {
            var stemBranch = new StemBranch(stem, branch);
            var result = stemBranch.ToString();
            Assert.Equal(expected, result);
        }
    }

    #region 旬空测试

    [Fact]
    public void StemBranch_EmptyBranches_JiaZi_ShouldReturnXuAndHai()
    {
        // Arrange
        var stemBranch = new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi);

        // Act
        var emptyBranches = stemBranch.EmptyBranches;

        // Assert
        Assert.Equal(2, emptyBranches.Length);
        Assert.Contains(EarthlyBranch.Xu, emptyBranches);
        Assert.Contains(EarthlyBranch.Hai, emptyBranches);
    }

    [Fact]
    public void StemBranch_EmptyBranches_JiaXu_ShouldReturnShenAndYou()
    {
        // Arrange
        var stemBranch = new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Xu);

        // Act
        var emptyBranches = stemBranch.EmptyBranches;

        // Assert
        Assert.Equal(2, emptyBranches.Length);
        Assert.Contains(EarthlyBranch.Shen, emptyBranches);
        Assert.Contains(EarthlyBranch.You, emptyBranches);
    }

    [Fact]
    public void StemBranch_EmptyBranches_YiChou_ShouldReturnXuAndHai()
    {
        // Arrange
        var stemBranch = new StemBranch(HeavenlyStem.Yi, EarthlyBranch.Chou);

        // Act
        var emptyBranches = stemBranch.EmptyBranches;

        // Assert
        Assert.Equal(2, emptyBranches.Length);
        Assert.Contains(EarthlyBranch.Xu, emptyBranches);
        Assert.Contains(EarthlyBranch.Hai, emptyBranches);
    }

    [Fact]
    public void StemBranch_EmptyBranches_BingYin_ShouldReturnXuAndHai()
    {
        // Arrange
        var stemBranch = new StemBranch(HeavenlyStem.Bing, EarthlyBranch.Yin);

        // Act
        var emptyBranches = stemBranch.EmptyBranches;

        // Assert
        Assert.Equal(2, emptyBranches.Length);
        Assert.Contains(EarthlyBranch.Xu, emptyBranches);
        Assert.Contains(EarthlyBranch.Hai, emptyBranches);
    }

    [Fact]
    public void StemBranch_EmptyBranches_WuWu_ShouldReturnZiAndChou()
    {
        // Arrange
        var stemBranch = new StemBranch(HeavenlyStem.Wu, EarthlyBranch.Wu);

        // Act
        var emptyBranches = stemBranch.EmptyBranches;

        // Assert
        Assert.Equal(2, emptyBranches.Length);
        Assert.Contains(EarthlyBranch.Zi, emptyBranches);
        Assert.Contains(EarthlyBranch.Chou, emptyBranches);
    }

    [Fact]
    public void StemBranch_EmptyBranches_GengChen_ShouldReturnShenAndYou()
    {
        // Arrange
        var stemBranch = new StemBranch(HeavenlyStem.Geng, EarthlyBranch.Chen);

        // Act
        var emptyBranches = stemBranch.EmptyBranches;

        // Assert
        Assert.Equal(2, emptyBranches.Length);
        Assert.Contains(EarthlyBranch.Shen, emptyBranches);
        Assert.Contains(EarthlyBranch.You, emptyBranches);
    }

    [Fact]
    public void StemBranch_EmptyBranches_RenShen_ShouldReturnXuAndHai()
    {
        // Arrange
        var stemBranch = new StemBranch(HeavenlyStem.Ren, EarthlyBranch.Shen);

        // Act
        var emptyBranches = stemBranch.EmptyBranches;

        // Assert
        Assert.Equal(2, emptyBranches.Length);
        Assert.Contains(EarthlyBranch.Xu, emptyBranches);
        Assert.Contains(EarthlyBranch.Hai, emptyBranches);
    }

    // 验证各种干支组合的旬空
    [Theory]
    [InlineData(1, 1, 11, 12)]  // 甲子旬 - 戌亥空
    [InlineData(1, 11, 9, 10)]  // 甲戌旬 - 申酉空
    [InlineData(2, 2, 11, 12)]  // 乙丑旬 - 戌亥空
    [InlineData(3, 3, 11, 12)]  // 丙寅旬 - 戌亥空
    [InlineData(5, 7, 1, 2)]    // 戊午旬 - 子丑空
    [InlineData(7, 5, 9, 10)]   // 庚辰旬 - 申酉空
    [InlineData(9, 9, 11, 12)]  // 壬申旬 - 戌亥空
    [InlineData(4, 12, 7, 8)] // 丁亥 - 午未空
    [InlineData(8, 10, 1, 2)] // 辛酉 - 子丑空
    [InlineData(9, 5, 7, 8)] // 壬辰 - 午未空
    [InlineData(5, 9, 3, 4)] // 戊申 - 寅卯空
    [InlineData(10, 4, 5, 6)] // 癸卯 - 辰巳空
    public void StemBranch_EmptyBranches_ShouldCalculateCorrectly(
        byte stemValue, byte branchValue, byte empty1Value, byte empty2Value)
    {
        // Arrange
        var stem = HeavenlyStem.FromValue(stemValue);
        var branch = EarthlyBranch.FromValue(branchValue);
        var stemBranch = new StemBranch(stem, branch);

        // Act
        var emptyBranches = stemBranch.EmptyBranches;

        // Assert
        Assert.Equal(2, emptyBranches.Length);
        Assert.Equal(empty1Value, emptyBranches[0].Value);
        Assert.Equal(empty2Value, emptyBranches[1].Value);
    }

    #endregion

    #region 干支相生相克测试

    [Fact]
    public void StemBranch_FivePhaseRelationship_ShouldWorkThroughStemAndBranch()
    {
        // 甲子 - 甲木生丁火
        var jiaZi = new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi);
        var dingYou = new StemBranch(HeavenlyStem.Ding, EarthlyBranch.You);

        Assert.True(jiaZi.Stem.Generates(dingYou.Stem));
    }

    #endregion
}
