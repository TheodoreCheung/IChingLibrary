using System.Globalization;
using IChingLibrary.Core.Localization;

namespace IChingLibrary.Core;

/// <summary>
/// Hexagram 扩展方法，提供获取卦辞、彖辞、象辞的功能
/// </summary>
public static class HexagramExtensions
{
    /// <summary>
    /// 获取卦辞
    /// </summary>
    /// <param name="hexagram">卦</param>
    /// <param name="culture">文化信息，如果为 null 则使用 DefaultCulture 或 CurrentUICulture</param>
    /// <returns>卦辞文本</returns>
    public static string GetStatement(this Hexagram hexagram, CultureInfo? culture = null)
    {
        var label = $"{hexagram.Value}.Statement";
        return IChingTranslationManager.GetTranslation(nameof(Hexagram), label, culture)
            ?? string.Empty;
    }

    /// <summary>
    /// 获取彖辞
    /// </summary>
    /// <param name="hexagram">卦</param>
    /// <param name="culture">文化信息，如果为 null 则使用 DefaultCulture 或 CurrentUICulture</param>
    /// <returns>彖辞文本</returns>
    public static string GetCommentary(this Hexagram hexagram, CultureInfo? culture = null)
    {
        var label = $"{hexagram.Value}.Commentary";
        return IChingTranslationManager.GetTranslation(nameof(Hexagram), label, culture)
            ?? string.Empty;
    }

    /// <summary>
    /// 获取象辞（大象）
    /// </summary>
    /// <param name="hexagram">卦</param>
    /// <param name="culture">文化信息，如果为 null 则使用 DefaultCulture 或 CurrentUICulture</param>
    /// <returns>象辞文本</returns>
    public static string GetImage(this Hexagram hexagram, CultureInfo? culture = null)
    {
        var label = $"{hexagram.Value}.Image";
        return IChingTranslationManager.GetTranslation(nameof(Hexagram), label, culture)
            ?? string.Empty;
    }
}
