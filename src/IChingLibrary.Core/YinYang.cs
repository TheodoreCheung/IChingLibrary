namespace IChingLibrary.Core;

/// <summary>
/// 阴阳，易学最基本的概念
/// </summary>
[IChingElementEnum]
public partial class YinYang : IChingElement<YinYang>
{
    /// <summary>
    /// 初始化阴阳
    /// </summary>
    /// <param name="value">唯一标识值</param>
    /// <param name="label">标签名称</param>
    private YinYang(byte value, string label) : base(value, label) { }

    /// <summary>
    /// 阴
    /// </summary>
    public static readonly YinYang Yin = new(0, nameof(Yin));

    /// <summary>
    /// 阳
    /// </summary>
    public static readonly YinYang Yang = new(1, nameof(Yang));
}