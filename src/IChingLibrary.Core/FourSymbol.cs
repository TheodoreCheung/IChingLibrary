namespace IChingLibrary.Core;

/// <summary>
/// 四象，由阴阳爻叠加形成
/// </summary>
[IChingElementEnum]
public partial class FourSymbol : IChingElement<FourSymbol>
{
    public YinYang YinYang { get; }

    /// <summary>
    /// 初始化四象
    /// </summary>
    /// <param name="value">唯一标识值（6=老阴，7=少阳，8=少阴，9=老阳）</param>
    /// <param name="label">标签名称</param>
    /// <param name="yinYang">阴阳</param>
    private FourSymbol(byte value, string label, YinYang yinYang) : base(value, label)
    {
        YinYang = yinYang;
    }

    /// <summary>
    /// 老阴（值为 6）
    /// </summary>
    public static readonly FourSymbol OldYin = new(6, nameof(OldYin), YinYang.Yin);

    /// <summary>
    /// 少阳（值为 7）
    /// </summary>
    public static readonly FourSymbol YoungYang = new(7, nameof(YoungYang), YinYang.Yang);

    /// <summary>
    /// 少阴（值为 8）
    /// </summary>
    public static readonly FourSymbol YoungYin = new(8, nameof(YoungYin), YinYang.Yin);

    /// <summary>
    /// 老阳（值为 9）
    /// </summary>
    public static readonly FourSymbol OldYang = new(9, nameof(OldYang), YinYang.Yang);
}