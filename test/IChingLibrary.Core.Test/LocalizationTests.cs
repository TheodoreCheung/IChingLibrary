using System.Globalization;
using IChingLibrary.Core;
using IChingLibrary.Core.Localization;

namespace IChingLibrary.Core.Test;

public class LocalizationTests
{
    [Fact]
    public void YinYang_ToString_English_ShouldReturnEnglishTranslation()
    {
        // Arrange
        var en = new CultureInfo("en");

        // Act & Assert
        Assert.Equal("Yin", YinYang.Yin.ToString(en));
        Assert.Equal("Yang", YinYang.Yang.ToString(en));
    }

    [Fact]
    public void YinYang_ToString_ChineseSimplified_ShouldReturnChineseTranslation()
    {
        // Arrange
        var zhHans = new CultureInfo("zh-Hans");

        // Act & Assert
        Assert.Equal("阴", YinYang.Yin.ToString(zhHans));
        Assert.Equal("阳", YinYang.Yang.ToString(zhHans));
    }

    [Fact]
    public void FivePhase_ToString_English_ShouldReturnEnglishTranslation()
    {
        // Arrange
        var en = new CultureInfo("en");

        // Act & Assert
        Assert.Equal("Metal", FivePhase.Metal.ToString(en));
        Assert.Equal("Water", FivePhase.Water.ToString(en));
        Assert.Equal("Wood", FivePhase.Wood.ToString(en));
        Assert.Equal("Fire", FivePhase.Fire.ToString(en));
        Assert.Equal("Earth", FivePhase.Earth.ToString(en));
    }

    [Fact]
    public void FivePhase_ToString_ChineseSimplified_ShouldReturnChineseTranslation()
    {
        // Arrange
        var zhHans = new CultureInfo("zh-Hans");

        // Act & Assert
        Assert.Equal("金", FivePhase.Metal.ToString(zhHans));
        Assert.Equal("水", FivePhase.Water.ToString(zhHans));
        Assert.Equal("木", FivePhase.Wood.ToString(zhHans));
        Assert.Equal("火", FivePhase.Fire.ToString(zhHans));
        Assert.Equal("土", FivePhase.Earth.ToString(zhHans));
    }

    [Fact]
    public void FourSymbol_ToString_English_ShouldReturnEnglishTranslation()
    {
        // Arrange
        var en = new CultureInfo("en");

        // Act & Assert
        Assert.Equal("Old Yin", FourSymbol.OldYin.ToString(en));
        Assert.Equal("Young Yang", FourSymbol.YoungYang.ToString(en));
        Assert.Equal("Young Yin", FourSymbol.YoungYin.ToString(en));
        Assert.Equal("Old Yang", FourSymbol.OldYang.ToString(en));
    }

    [Fact]
    public void FourSymbol_ToString_ChineseSimplified_ShouldReturnChineseTranslation()
    {
        // Arrange
        var zhHans = new CultureInfo("zh-Hans");

        // Act & Assert
        Assert.Equal("老阴", FourSymbol.OldYin.ToString(zhHans));
        Assert.Equal("少阳", FourSymbol.YoungYang.ToString(zhHans));
        Assert.Equal("少阴", FourSymbol.YoungYin.ToString(zhHans));
        Assert.Equal("老阳", FourSymbol.OldYang.ToString(zhHans));
    }

    [Fact]
    public void Trigram_ToString_English_ShouldReturnEnglishTranslation()
    {
        // Arrange
        var en = new CultureInfo("en");

        // Act & Assert
        Assert.Equal("Qian", Trigram.Qian.ToString(en));
        Assert.Equal("Dui", Trigram.Dui.ToString(en));
        Assert.Equal("Li", Trigram.Li.ToString(en));
        Assert.Equal("Zhen", Trigram.Zhen.ToString(en));
        Assert.Equal("Xun", Trigram.Xun.ToString(en));
        Assert.Equal("Kan", Trigram.Kan.ToString(en));
        Assert.Equal("Gen", Trigram.Gen.ToString(en));
        Assert.Equal("Kun", Trigram.Kun.ToString(en));
    }

    [Fact]
    public void Trigram_ToString_ChineseSimplified_ShouldReturnChineseTranslation()
    {
        // Arrange
        var zhHans = new CultureInfo("zh-Hans");

        // Act & Assert
        Assert.Equal("乾", Trigram.Qian.ToString(zhHans));
        Assert.Equal("兑", Trigram.Dui.ToString(zhHans));
        Assert.Equal("离", Trigram.Li.ToString(zhHans));
        Assert.Equal("震", Trigram.Zhen.ToString(zhHans));
        Assert.Equal("巽", Trigram.Xun.ToString(zhHans));
        Assert.Equal("坎", Trigram.Kan.ToString(zhHans));
        Assert.Equal("艮", Trigram.Gen.ToString(zhHans));
        Assert.Equal("坤", Trigram.Kun.ToString(zhHans));
    }

    [Fact]
    public void HeavenlyStem_ToString_English_ShouldReturnEnglishTranslation()
    {
        // Arrange
        var en = new CultureInfo("en");

        // Act & Assert
        Assert.Equal("Jia", HeavenlyStem.Jia.ToString(en));
        Assert.Equal("Yi", HeavenlyStem.Yi.ToString(en));
        Assert.Equal("Bing", HeavenlyStem.Bing.ToString(en));
        Assert.Equal("Ding", HeavenlyStem.Ding.ToString(en));
        Assert.Equal("Wu", HeavenlyStem.Wu.ToString(en));
        Assert.Equal("Ji", HeavenlyStem.Ji.ToString(en));
        Assert.Equal("Geng", HeavenlyStem.Geng.ToString(en));
        Assert.Equal("Xin", HeavenlyStem.Xin.ToString(en));
        Assert.Equal("Ren", HeavenlyStem.Ren.ToString(en));
        Assert.Equal("Gui", HeavenlyStem.Gui.ToString(en));
    }

    [Fact]
    public void HeavenlyStem_ToString_ChineseSimplified_ShouldReturnChineseTranslation()
    {
        // Arrange
        var zhHans = new CultureInfo("zh-Hans");

        // Act & Assert
        Assert.Equal("甲", HeavenlyStem.Jia.ToString(zhHans));
        Assert.Equal("乙", HeavenlyStem.Yi.ToString(zhHans));
        Assert.Equal("丙", HeavenlyStem.Bing.ToString(zhHans));
        Assert.Equal("丁", HeavenlyStem.Ding.ToString(zhHans));
        Assert.Equal("戊", HeavenlyStem.Wu.ToString(zhHans));
        Assert.Equal("己", HeavenlyStem.Ji.ToString(zhHans));
        Assert.Equal("庚", HeavenlyStem.Geng.ToString(zhHans));
        Assert.Equal("辛", HeavenlyStem.Xin.ToString(zhHans));
        Assert.Equal("壬", HeavenlyStem.Ren.ToString(zhHans));
        Assert.Equal("癸", HeavenlyStem.Gui.ToString(zhHans));
    }

    [Fact]
    public void EarthlyBranch_ToString_English_ShouldReturnEnglishTranslation()
    {
        // Arrange
        var en = new CultureInfo("en");

        // Act & Assert
        Assert.Equal("Zi", EarthlyBranch.Zi.ToString(en));
        Assert.Equal("Chou", EarthlyBranch.Chou.ToString(en));
        Assert.Equal("Yin", EarthlyBranch.Yin.ToString(en));
        Assert.Equal("Mao", EarthlyBranch.Mao.ToString(en));
        Assert.Equal("Chen", EarthlyBranch.Chen.ToString(en));
        Assert.Equal("Si", EarthlyBranch.Si.ToString(en));
        Assert.Equal("Wu", EarthlyBranch.Wu.ToString(en));
        Assert.Equal("Wei", EarthlyBranch.Wei.ToString(en));
        Assert.Equal("Shen", EarthlyBranch.Shen.ToString(en));
        Assert.Equal("You", EarthlyBranch.You.ToString(en));
        Assert.Equal("Xu", EarthlyBranch.Xu.ToString(en));
        Assert.Equal("Hai", EarthlyBranch.Hai.ToString(en));
    }

    [Fact]
    public void EarthlyBranch_ToString_ChineseSimplified_ShouldReturnChineseTranslation()
    {
        // Arrange
        var zhHans = new CultureInfo("zh-Hans");

        // Act & Assert
        Assert.Equal("子", EarthlyBranch.Zi.ToString(zhHans));
        Assert.Equal("丑", EarthlyBranch.Chou.ToString(zhHans));
        Assert.Equal("寅", EarthlyBranch.Yin.ToString(zhHans));
        Assert.Equal("卯", EarthlyBranch.Mao.ToString(zhHans));
        Assert.Equal("辰", EarthlyBranch.Chen.ToString(zhHans));
        Assert.Equal("巳", EarthlyBranch.Si.ToString(zhHans));
        Assert.Equal("午", EarthlyBranch.Wu.ToString(zhHans));
        Assert.Equal("未", EarthlyBranch.Wei.ToString(zhHans));
        Assert.Equal("申", EarthlyBranch.Shen.ToString(zhHans));
        Assert.Equal("酉", EarthlyBranch.You.ToString(zhHans));
        Assert.Equal("戌", EarthlyBranch.Xu.ToString(zhHans));
        Assert.Equal("亥", EarthlyBranch.Hai.ToString(zhHans));
    }

    [Fact]
    public void Hexagram_ToString_English_ShouldReturnEnglishTranslation()
    {
        // Arrange
        var en = new CultureInfo("en");

        // Act & Assert - Sample of key hexagrams
        Assert.Equal("The Creative", Hexagram.TheCreative.ToString(en));
        Assert.Equal("The Receptive", Hexagram.TheReceptive.ToString(en));
        Assert.Equal("Peace", Hexagram.Peace.ToString(en));
        Assert.Equal("Standstill", Hexagram.Standstill.ToString(en));
        Assert.Equal("The Abysmal", Hexagram.TheAbysmal.ToString(en));
        Assert.Equal("The Clinging", Hexagram.TheClinging.ToString(en));
    }

    [Fact]
    public void Hexagram_ToString_ChineseSimplified_ShouldReturnChineseTranslation()
    {
        // Arrange
        var zhHans = new CultureInfo("zh-Hans");

        // Act & Assert - Sample of key hexagrams
        Assert.Equal("乾为天", Hexagram.TheCreative.ToString(zhHans));
        Assert.Equal("坤为地", Hexagram.TheReceptive.ToString(zhHans));
        Assert.Equal("地天泰", Hexagram.Peace.ToString(zhHans));
        Assert.Equal("天地否", Hexagram.Standstill.ToString(zhHans));
        Assert.Equal("坎为水", Hexagram.TheAbysmal.ToString(zhHans));
        Assert.Equal("离为火", Hexagram.TheClinging.ToString(zhHans));
    }

    [Fact]
    public void UniqueKey_ShouldReturnCorrectFormat()
    {
        // Act & Assert
        Assert.Equal("YinYang.Yin", YinYang.Yin.UniqueKey);
        Assert.Equal("YinYang.Yang", YinYang.Yang.UniqueKey);
        Assert.Equal("FivePhase.Metal", FivePhase.Metal.UniqueKey);
        Assert.Equal("Trigram.Qian", Trigram.Qian.UniqueKey);
        Assert.Equal("Hexagram.TheCreative", Hexagram.TheCreative.UniqueKey);
        Assert.Equal("HeavenlyStem.Jia", HeavenlyStem.Jia.UniqueKey);
        Assert.Equal("EarthlyBranch.Zi", EarthlyBranch.Zi.UniqueKey);
    }

    [Fact]
    public void CustomTranslationProvider_ShouldBeUsed()
    {
        // Arrange
        var customProvider = new TestTranslationProvider();
        IChingTranslationManager.Provider = customProvider;

        // Act
        var result = YinYang.Yin.ToString();

        // Assert
        Assert.Equal("CustomYin", result);

        // Cleanup
        IChingTranslationManager.ResetToDefault();
    }

    [Fact]
    public void TranslationManager_ResetToDefault_ShouldRestoreDefaultProvider()
    {
        // Arrange
        IChingTranslationManager.Provider = new TestTranslationProvider();
        Assert.Equal("CustomYin", YinYang.Yin.ToString());

        // Act
        IChingTranslationManager.ResetToDefault();

        // Assert
        Assert.Equal("Yin", YinYang.Yin.ToString(new CultureInfo("en")));
    }

    [Fact]
    public void UnsupportedCulture_ShouldFallbackToLabel()
    {
        // Arrange
        var fr = new CultureInfo("fr"); // French - no translation provided

        // Act & Assert - Should fall back to Label when no translation exists
        Assert.Equal("Yin", YinYang.Yin.ToString(fr));
        Assert.Equal("Metal", FivePhase.Metal.ToString(fr));
    }

    #region DefaultCulture 功能测试

    [Fact]
    public void DefaultCulture_ShouldOverrideCurrentUICulture()
    {
        // Arrange - 设置系统文化为英语
        var originalCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentUICulture = new CultureInfo("en");

        try
        {
            // Act - 设置易学库默认文化为简体中文
            IChingTranslationManager.DefaultCulture = new CultureInfo("zh-Hans");

            // Assert - 应该显示中文，而不是系统语言的英文
            Assert.Equal("阴", YinYang.Yin.ToString());
            Assert.Equal("阳", YinYang.Yang.ToString());
            Assert.Equal("金", FivePhase.Metal.ToString());
        }
        finally
        {
            // Cleanup
            IChingTranslationManager.DefaultCulture = null;
            CultureInfo.CurrentUICulture = originalCulture;
        }
    }

    [Fact]
    public void DefaultCulture_Null_ShouldUseCurrentUICulture()
    {
        // Arrange - 设置系统文化为英语
        var originalCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentUICulture = new CultureInfo("en");

        try
        {
            // Act - 确保 DefaultCulture 为 null
            IChingTranslationManager.DefaultCulture = null;

            // Assert - 应该使用系统的 CurrentUICulture
            Assert.Equal("Yin", YinYang.Yin.ToString());
            Assert.Equal("Metal", FivePhase.Metal.ToString());
        }
        finally
        {
            // Cleanup
            CultureInfo.CurrentUICulture = originalCulture;
        }
    }

    [Fact]
    public void DefaultCulture_CanSwitchAtRuntime()
    {
        // Arrange - 设置系统文化为法语（无翻译）
        var originalCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentUICulture = new CultureInfo("fr");

        try
        {
            // Act & Assert - 初始设置为中文
            IChingTranslationManager.DefaultCulture = new CultureInfo("zh-Hans");
            Assert.Equal("阴", YinYang.Yin.ToString());
            Assert.Equal("金", FivePhase.Metal.ToString());

            // 切换为英语
            IChingTranslationManager.DefaultCulture = new CultureInfo("en");
            Assert.Equal("Yin", YinYang.Yin.ToString());
            Assert.Equal("Metal", FivePhase.Metal.ToString());

            // 恢复为 null（使用系统语言）
            IChingTranslationManager.DefaultCulture = null;
            Assert.Equal("Yin", YinYang.Yin.ToString()); // 法语无翻译，回退到 Label
        }
        finally
        {
            // Cleanup
            IChingTranslationManager.DefaultCulture = null;
            CultureInfo.CurrentUICulture = originalCulture;
        }
    }

    [Fact]
    public void DefaultCulture_WorksWithStemBranch()
    {
        // Arrange - 设置系统文化为英语
        var originalCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentUICulture = new CultureInfo("en");

        try
        {
            // Act - 设置易学库默认文化为简体中文
            IChingTranslationManager.DefaultCulture = new CultureInfo("zh-Hans");
            var stemBranch = new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi);

            // Assert - StemBranch 应该使用 DefaultCulture
            Assert.Equal("甲子", stemBranch.ToString());

            // 切换为英语
            IChingTranslationManager.DefaultCulture = new CultureInfo("en");
            Assert.Equal("JiaZi", stemBranch.ToString());
        }
        finally
        {
            // Cleanup
            IChingTranslationManager.DefaultCulture = null;
            CultureInfo.CurrentUICulture = originalCulture;
        }
    }

    [Fact]
    public void DefaultCulture_DoesNotAffectOtherLocalizations()
    {
        // Arrange - 设置系统文化为英语
        var originalCulture = CultureInfo.CurrentUICulture;
        CultureInfo.CurrentUICulture = new CultureInfo("en");

        try
        {
            // Act - 设置易学库默认文化为简体中文
            IChingTranslationManager.DefaultCulture = new CultureInfo("zh-Hans");

            // Assert - 易学元素应该显示中文
            Assert.Equal("阴", YinYang.Yin.ToString());

            // 其他本地化（如 DateTime）应该仍然使用系统文化
            var date = new DateTime(2024, 1, 1);
            var dateStr = date.ToString("d"); // 短日期格式
            // 英语系统应该是 "1/1/2024" 格式
            Assert.Contains("2024", dateStr); // 至少应该包含年份

            // 切换系统文化为中文，日期格式应该改变
            CultureInfo.CurrentUICulture = new CultureInfo("zh-Hans");
            dateStr = date.ToString("d");
            Assert.Contains("2024", dateStr);
        }
        finally
        {
            // Cleanup
            IChingTranslationManager.DefaultCulture = null;
            CultureInfo.CurrentUICulture = originalCulture;
        }
    }

    [Fact]
    public void ResetToDefault_ShouldClearDefaultCulture()
    {
        // Arrange
        IChingTranslationManager.DefaultCulture = new CultureInfo("zh-Hans");
        Assert.Equal("阴", YinYang.Yin.ToString());

        // Act
        IChingTranslationManager.ResetToDefault();

        // Assert - DefaultCulture 应该被清除
        Assert.Null(IChingTranslationManager.DefaultCulture);
        // 现在应该使用系统的 CurrentUICulture
        Assert.Equal("Yin", YinYang.Yin.ToString(new CultureInfo("en")));
    }

    #endregion

    /// <summary>
    /// 测试用的自定义翻译提供者
    /// </summary>
    private class TestTranslationProvider : IIChingTranslationProvider
    {
        public string? GetTranslation(string typeName, string label, CultureInfo culture)
        {
            if (typeName == "YinYang" && label == "Yin")
                return "CustomYin";
            if (typeName == "YinYang" && label == "Yang")
                return "CustomYang";
            return null;
        }
    }
}
