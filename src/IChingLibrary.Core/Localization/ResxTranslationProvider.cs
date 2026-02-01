using System.Globalization;
using System.Resources;

namespace IChingLibrary.Core.Localization;

/// <summary>
/// 基于 RESX 资源文件的易学翻译提供者
/// </summary>
public sealed class ResxTranslationProvider : IIChingTranslationProvider
{
    private readonly ResourceManager _resourceManager;

    /// <summary>
    /// 初始化 RESX 翻译提供者
    /// </summary>
    /// <param name="resourceManager">资源管理器</param>
    public ResxTranslationProvider(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));
    }

    /// <summary>
    /// 获取指定元素的翻译文本
    /// </summary>
    /// <param name="typeName">元素类型名称（如 "YinYang"、"FivePhase"）</param>
    /// <param name="label">元素标签（如 "Yin"、"Yang"、"Metal"）</param>
    /// <param name="culture">文化信息</param>
    /// <returns>翻译后的文本，如果找不到翻译则返回 null</returns>
    public string? GetTranslation(string typeName, string label, CultureInfo culture)
    {
        if (string.IsNullOrEmpty(typeName))
            throw new ArgumentException("Type name cannot be null or empty.", nameof(typeName));

        if (string.IsNullOrEmpty(label))
            throw new ArgumentException("Label cannot be null or empty.", nameof(label));

        // 构建资源键：TypeName.Label
        var resourceKey = $"{typeName}.{label}";
        return _resourceManager.GetString(resourceKey, culture);
    }
}
