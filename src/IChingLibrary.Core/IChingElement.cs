using System.Globalization;
using IChingLibrary.Core.Localization;

namespace IChingLibrary.Core;

/// <summary>
/// 易学元素抽象基类
/// </summary>
/// <typeparam name="T">易学元素类型</typeparam>
public abstract class IChingElement<T> : IEquatable<T> where T : IChingElement<T>
{
    /// <summary>
    /// 元素的唯一标识值
    /// </summary>
    public readonly byte Value;

    /// <summary>
    /// 元素的标签名称
    /// </summary>
    public readonly string Label;

    /// <summary>
    /// 元素的唯一键，由类型名和标签组成，用于资源查找
    /// </summary>
    public string UniqueKey => $"{typeof(T).Name}.{Label}";

    /// <summary>
    /// 初始化易学元素
    /// </summary>
    /// <param name="value">元素的唯一标识值</param>
    /// <param name="label">元素的标签名称</param>
    protected IChingElement(byte value, string label)
    {
        Value = value;
        Label = label;
    }

    /// <summary>
    /// 获取哈希码
    /// </summary>
    public override int GetHashCode() => HashCode.Combine(typeof(T), Value);

    /// <summary>
    /// 判断与指定对象是否相等
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as T);

    /// <summary>
    /// 判断与指定易学元素是否相等
    /// </summary>
    public bool Equals(T? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    /// <summary>
    /// 相等比较运算符
    /// </summary>
    public static bool operator ==(IChingElement<T>? left, IChingElement<T>? right) => Equals(left, right);

    /// <summary>
    /// 不相等比较运算符
    /// </summary>
    public static bool operator !=(IChingElement<T>? left, IChingElement<T>? right) => !Equals(left, right);

    /// <summary>
    /// 获取指定文化的翻译文本
    /// </summary>
    /// <param name="culture">文化信息</param>
    /// <returns>翻译后的文本</returns>
    protected virtual string GetTranslation(CultureInfo culture)
    {
        // 使用翻译管理器获取翻译
        return IChingTranslationManager.GetTranslation(typeof(T).Name, Label, culture) ?? Label;
    }

    /// <summary>
    /// 获取指定文化的翻译文本（公共方法）
    /// </summary>
    /// <param name="culture">文化信息</param>
    /// <returns>翻译后的文本</returns>
    public string ToString(CultureInfo culture)
    {
        return GetTranslation(culture);
    }

    /// <summary>
    /// 返回元素的字符串表示（使用 DefaultCulture 或当前 UI 文化）
    /// </summary>
    public override string ToString() => GetTranslation(IChingTranslationManager.GetEffectiveCulture());

    /// <summary>
    /// 隐式转换为 byte 类型
    /// </summary>
    public static implicit operator byte(IChingElement<T> element) => element.Value;
}