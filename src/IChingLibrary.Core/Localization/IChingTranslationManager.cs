using System.Globalization;
using IChingLibrary.Core.Resources;

namespace IChingLibrary.Core.Localization;

/// <summary>
/// 易学翻译管理器，负责管理易学元素的翻译提供者
/// </summary>
public static class IChingTranslationManager
{
    private static IIChingTranslationProvider? _provider;
    private static CultureInfo? _defaultCulture;

    /// <summary>
    /// 获取或设置当前翻译提供者
    /// </summary>
    /// <exception cref="ArgumentNullException">当设置为 null 时抛出</exception>
    public static IIChingTranslationProvider Provider
    {
        get => _provider ??= CreateDefaultProvider();
        set => _provider = value ?? throw new ArgumentNullException(nameof(value));
    }

    /// <summary>
    /// 获取或设置易学库的默认文化
    /// 设置后，所有易学元素的翻译将优先使用此文化，而非系统的 CurrentUICulture
    /// </summary>
    /// <remarks>
    /// 设置为 null 可恢复为使用系统的 CurrentUICulture
    /// </remarks>
    /// <example>
    /// <code>
    /// // 应用启动时设置易学库语言为简体中文
    /// IChingTranslationManager.DefaultCulture = new CultureInfo("zh-Hans");
    ///
    /// // 此时无论系统语言是什么，易学元素都会显示中文
    /// Console.WriteLine(YinYang.Yin); // 输出：阴
    ///
    /// // 用户切换语言为英语
    /// IChingTranslationManager.DefaultCulture = new CultureInfo("en");
    /// Console.WriteLine(YinYang.Yin); // 输出：Yin
    ///
    /// // 恢复使用系统语言
    /// IChingTranslationManager.DefaultCulture = null;
    /// </code>
    /// </example>
    public static CultureInfo? DefaultCulture
    {
        get => _defaultCulture;
        set => _defaultCulture = value;
    }

    /// <summary>
    /// 获取有效的文化信息
    /// </summary>
    /// <returns>如果设置了 DefaultCulture 则返回它，否则返回 CurrentUICulture</returns>
    internal static CultureInfo GetEffectiveCulture()
    {
        return _defaultCulture ?? CultureInfo.CurrentUICulture;
    }

    /// <summary>
    /// 获取指定元素的翻译文本
    /// </summary>
    /// <param name="typeName">元素类型名称（如 "YinYang"、"FivePhase"）</param>
    /// <param name="label">元素标签（如 "Yin"、"Yang"、"Metal"）</param>
    /// <param name="culture">文化信息，如果为 null 则使用 DefaultCulture 或 CurrentUICulture</param>
    /// <returns>翻译后的文本，如果找不到翻译则返回 null</returns>
    public static string? GetTranslation(string typeName, string label, CultureInfo? culture = null)
    {
        var effectiveCulture = culture ?? GetEffectiveCulture();
        return Provider.GetTranslation(typeName, label, effectiveCulture);
    }

    /// <summary>
    /// 重置为默认翻译提供者和文化设置
    /// </summary>
    public static void ResetToDefault()
    {
        _provider = null;
        _defaultCulture = null;
    }

    /// <summary>
    /// 创建默认的 RESX 翻译提供者
    /// </summary>
    private static IIChingTranslationProvider CreateDefaultProvider()
    {
        return new ResxTranslationProvider(IChingResources.ResourceManager);
    }
}
