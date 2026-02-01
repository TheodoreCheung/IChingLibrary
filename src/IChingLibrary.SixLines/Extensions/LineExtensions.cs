using System.Globalization;
using IChingLibrary.Core.Localization;

namespace IChingLibrary.SixLines;

/// <summary>
/// Line 扩展方法，提供获取爻辞、小象辞的功能
/// </summary>
public static class LineExtensions
{
    /// <summary>
    /// 获取爻辞
    /// </summary>
    /// <param name="line">爻</param>
    /// <param name="hexagram">所属卦</param>
    /// <param name="culture">文化信息，如果为 null 则使用 DefaultCulture 或 CurrentUICulture</param>
    /// <returns>爻辞文本</returns>
    public static string GetStatement(this Line line, Hexagram hexagram, CultureInfo? culture = null)
    {
        var label = $"{hexagram.Value}.{line.LinePosition.Label}.Statement";
        return IChingTranslationManager.GetTranslation("Line", label, culture)
            ?? string.Empty;
    }

    /// <summary>
    /// 获取小象辞（爻的象辞）
    /// </summary>
    /// <param name="line">爻</param>
    /// <param name="hexagram">所属卦</param>
    /// <param name="culture">文化信息，如果为 null 则使用 DefaultCulture 或 CurrentUICulture</param>
    /// <returns>小象辞文本</returns>
    public static string GetImage(this Line line, Hexagram hexagram, CultureInfo? culture = null)
    {
        var label = $"{hexagram.Value}.{line.LinePosition.Label}.Image";
        return IChingTranslationManager.GetTranslation("Line", label, culture)
            ?? string.Empty;
    }
}
