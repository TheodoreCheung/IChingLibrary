using System.Globalization;

namespace IChingLibrary.Core.Localization;

/// <summary>
/// 易学翻译提供者接口，用于提供易学元素的多语言翻译
/// </summary>
public interface IIChingTranslationProvider
{
    /// <summary>
    /// 获取指定元素的翻译文本
    /// </summary>
    /// <param name="typeName">元素类型名称（如 "YinYang"、"FivePhase"）</param>
    /// <param name="label">元素标签（如 "Yin"、"Yang"、"Metal"）</param>
    /// <param name="culture">文化信息</param>
    /// <returns>翻译后的文本，如果找不到翻译则返回 null</returns>
    string? GetTranslation(string typeName, string label, CultureInfo culture);
}
