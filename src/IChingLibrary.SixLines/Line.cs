namespace IChingLibrary.SixLines;

/// <summary>
/// 爻，表示六爻占卜中的单个爻
/// </summary>
public class Line
{
    private SixKin? _sixKin;
    private StemBranch? _stemBranch;

    /// <summary>
    /// 爻位（初爻到上爻）
    /// </summary>
    public required LinePosition LinePosition { get; init; }

    /// <summary>
    /// 阴阳属性
    /// </summary>
    public required YinYang YinYang { get; init; }

    /// <summary>
    /// 是否为变爻（动爻）
    /// </summary>
    public bool IsChanging { get; internal set; }

    /// <summary>
    /// 爻的四象，根据<see cref="IsChanging"/>和<see cref="YinYang"/>判断
    /// </summary>
    public FourSymbol FourSymbol => IsChanging
        ? YinYang == YinYang.Yang ? FourSymbol.OldYang : FourSymbol.OldYin
        : YinYang == YinYang.Yang
            ? FourSymbol.YoungYang
            : FourSymbol.YoungYin;

    /// <summary>
    /// 干支（纳甲），通过纳甲法设置
    /// </summary>
    /// <exception cref="InvalidOperationException">未经过纳甲步骤构建</exception>
    public StemBranch StemBranch
    {
        get => _stemBranch ?? throw new InvalidOperationException($"{nameof(StemBranch)} not set in build step");
        internal set => _stemBranch = value;
    }

    /// <summary>
    /// 尝试获取干支（纳甲）
    /// </summary>
    public bool TryGetStemBranch(out StemBranch? stemBranch)
    {
        stemBranch = _stemBranch;
        return stemBranch is not null;
    }

    /// <summary>
    /// 六亲（父母、兄弟、妻财、官鬼、子孙），通过六亲提供器设置
    /// </summary>
    /// <exception cref="InvalidOperationException">未经过绑六亲步骤构建</exception>
    public SixKin SixKin
    {
        get => _sixKin ?? throw new InvalidOperationException($"{nameof(SixKin)} not set in build step");
        internal set => _sixKin = value;
    }

    /// <summary>
    /// 尝试获取六亲
    /// </summary>
    public bool TryGetSixKin(out SixKin? sixKin)
    {
        sixKin = _sixKin;
        return sixKin is not null;
    }

    /// <summary>
    /// 世应位置（世爻或应爻）
    /// </summary>
    public Position? Position { get; internal set; }

    /// <summary>
    /// 六神
    /// </summary>
    public SixSpirit? SixSpirit { get; internal set; }

    /// <summary>
    /// 伏神（从本宫卦对应位置借用的爻）
    /// </summary>
    public HiddenDeityInfo? HiddenDeity { get; internal set; }

    /// <summary>
    /// 是否有伏神
    /// </summary>
    public bool HasHiddenDeity => HiddenDeity.HasValue;
}

/// <summary>
/// 伏神信息，用于存储从本宫卦借用的爻信息
/// </summary>
public readonly struct HiddenDeityInfo
{
    /// <summary>
    /// 伏神的干支（纳甲）
    /// </summary>
    public StemBranch StemBranch { get; }

    /// <summary>
    /// 伏神的六亲
    /// </summary>
    public SixKin SixKin { get; }

    /// <summary>
    /// 初始化伏神信息
    /// </summary>
    private HiddenDeityInfo(StemBranch stemBranch, SixKin sixKin)
    {
        StemBranch = stemBranch;
        SixKin = sixKin;
    }

    /// <summary>
    /// 从爻创建伏神信息
    /// </summary>
    public static HiddenDeityInfo FromLine(Line line) => new(line.StemBranch, line.SixKin);
}
