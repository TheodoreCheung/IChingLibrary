namespace IChingLibrary.Core;

/// <summary>
/// 三爻（八卦），由三个阴阳爻组成
/// </summary>
[IChingElementEnum]
public partial class Trigram : IChingElement<Trigram>
{
    /// <summary>
    /// 对应的五行属性
    /// </summary>
    public FivePhase FivePhase { get; }

    /// <summary>
    /// 初始化三爻
    /// </summary>
    /// <param name="value">唯一标识值（三位二进制表示）</param>
    /// <param name="label">标签名称</param>
    /// <param name="fivePhase">对应的五行属性</param>
    private Trigram(byte value, string label, FivePhase fivePhase) : base(value, label)
    {
        FivePhase = fivePhase;
    }

    /// <summary>
    /// 乾（天），属金
    /// </summary>
    public static readonly Trigram Qian = new(0b111, nameof(Qian), FivePhase.Metal);

    /// <summary>
    /// 兑（泽），属金
    /// </summary>
    public static readonly Trigram Dui = new(0b011, nameof(Dui), FivePhase.Metal);

    /// <summary>
    /// 离（火），属火
    /// </summary>
    public static readonly Trigram Li = new(0b101, nameof(Li), FivePhase.Fire);

    /// <summary>
    /// 震（雷），属木
    /// </summary>
    public static readonly Trigram Zhen = new(0b001, nameof(Zhen), FivePhase.Wood);

    /// <summary>
    /// 巽（风），属木
    /// </summary>
    public static readonly Trigram Xun = new(0b110, nameof(Xun), FivePhase.Wood);

    /// <summary>
    /// 坎（水），属水
    /// </summary>
    public static readonly Trigram Kan = new(0b010, nameof(Kan), FivePhase.Water);

    /// <summary>
    /// 艮（山），属土
    /// </summary>
    public static readonly Trigram Gen = new(0b100, nameof(Gen), FivePhase.Earth);

    /// <summary>
    /// 坤（地），属土
    /// </summary>
    public static readonly Trigram Kun = new(0b000, nameof(Kun), FivePhase.Earth);
}