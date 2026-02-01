using IChingLibrary.SixLines.Providers;
using IChingLibrary.SixLines.Providers.Abstractions;

namespace IChingLibrary.SixLines.Builders;

/// <summary>
/// 纳甲步骤
/// </summary>
public sealed class NajiaStep(INajiaProvider? provider = null) : ISixLineStep
{
    /// <summary>
    /// 纳甲提供器
    /// </summary>
    private readonly INajiaProvider _provider = provider ?? new DefaultNajiaProvider();

    /// <summary>
    /// 执行纳甲步骤
    /// </summary>
    /// <param name="hexagram">要处理的卦实例</param>
    /// <param name="inquiryTime">问时信息</param>
    /// <param name="originalHexagram">主卦实例（仅在处理变卦时有值）</param>
    public void Execute(HexagramInstance hexagram, InquiryTime inquiryTime, HexagramInstance? originalHexagram)
    {
        _provider.BindStemBranches(hexagram, inquiryTime);
    }
}

/// <summary>
/// 世应位置步骤
/// </summary>
public sealed class PositionStep(IPositionProvider? provider = null) : ISixLineStep
{
    /// <summary>
    /// 世应位置提供器
    /// </summary>
    private readonly IPositionProvider _provider = provider ?? new DefaultPositionProvider();

    /// <summary>
    /// 执行世应位置步骤
    /// </summary>
    /// <param name="hexagram">要处理的卦实例</param>
    /// <param name="inquiryTime">问时信息</param>
    /// <param name="originalHexagram">主卦实例（仅在处理变卦时有值）</param>
    public void Execute(HexagramInstance hexagram, InquiryTime inquiryTime, HexagramInstance? originalHexagram)
    {
        _provider.BindPositions(hexagram);
    }
}

/// <summary>
/// 六亲步骤
/// </summary>
public sealed class SixKinStep(ISixKinProvider? provider = null, bool useOriginalPalace = false) : ISixLineStep
{
    /// <summary>
    /// 六亲提供器
    /// </summary>
    private readonly ISixKinProvider _provider = provider ?? new DefaultSixKinProvider();

    /// <summary>
    /// 是否使用主卦的卦宫五行计算六亲（用于变卦）
    /// </summary>
    private readonly bool _useOriginalPalace = useOriginalPalace;

    /// <summary>
    /// 执行六亲步骤
    /// </summary>
    /// <param name="hexagram">要处理的卦实例</param>
    /// <param name="inquiryTime">问时信息</param>
    /// <param name="originalHexagram">主卦实例（仅在处理变卦时有值）</param>
    public void Execute(HexagramInstance hexagram, InquiryTime inquiryTime, HexagramInstance? originalHexagram)
    {
        if (_useOriginalPalace && originalHexagram != null)
        {
            _provider.BindSixKin(hexagram, originalHexagram.Meta.Palace.FivePhase);
        }
        else
        {
            _provider.BindSixKin(hexagram);
        }
    }
}

/// <summary>
/// 六神步骤
/// </summary>
public sealed class SixSpiritStep(ISixSpiritProvider? provider = null) : ISixLineStep
{
    /// <summary>
    /// 六神提供器
    /// </summary>
    private readonly ISixSpiritProvider _provider = provider ?? new DefaultSixSpiritProvider();

    /// <summary>
    /// 执行六神步骤
    /// </summary>
    /// <param name="hexagram">要处理的卦实例</param>
    /// <param name="inquiryTime">问时信息</param>
    /// <param name="originalHexagram">主卦实例（仅在处理变卦时有值）</param>
    public void Execute(HexagramInstance hexagram, InquiryTime inquiryTime, HexagramInstance? originalHexagram)
    {
        _provider.BindSixSpirits(hexagram, inquiryTime);
    }
}

/// <summary>
/// 伏神步骤
/// </summary>
public sealed class HiddenDeityStep(IHiddenDeityProvider? hiddenDeityProvider = null, INajiaProvider? najiaProvider = null, ISixKinProvider? sixKinProvider = null) : ISixLineStep
{
    /// <summary>
    /// 伏神提供器
    /// </summary>
    private readonly IHiddenDeityProvider _hiddenDeityProvider = hiddenDeityProvider ?? new DefaultHiddenDeityProvider();

    /// <summary>
    /// 纳甲提供器（用于获取干支信息）
    /// </summary>
    private readonly INajiaProvider _najiaProvider = najiaProvider ?? new DefaultNajiaProvider();

    /// <summary>
    /// 六亲提供器（用于获取六亲信息）
    /// </summary>
    private readonly ISixKinProvider _sixKinProvider = sixKinProvider ?? new DefaultSixKinProvider();

    /// <summary>
    /// 执行伏神步骤
    /// </summary>
    /// <param name="hexagram">要处理的卦实例</param>
    /// <param name="inquiryTime">问时信息</param>
    /// <param name="originalHexagram">主卦实例（仅在处理变卦时有值）</param>
    public void Execute(HexagramInstance hexagram, InquiryTime inquiryTime, HexagramInstance? originalHexagram)
    {
        // 变卦不需要找伏神
        if (originalHexagram != null)
            return;

        _hiddenDeityProvider.BindHiddenDeity(hexagram, inquiryTime, _najiaProvider, _sixKinProvider);
    }
}

/// <summary>
/// 神煞步骤（Symbolic Star Step）
/// </summary>
public sealed class SymbolicStarStep(DefaultSymbolicStarProvider? provider = null) : ISixLineStep
{
    private readonly DefaultSymbolicStarProvider _provider = provider ?? new DefaultSymbolicStarProvider();

    /// <summary>
    /// 神煞计算结果
    /// </summary>
    internal SymbolicStarCollection? Result { get; private set; }

    /// <summary>
    /// 执行神煞步骤
    /// </summary>
    /// <param name="hexagram">要处理的卦实例</param>
    /// <param name="inquiryTime">问时信息</param>
    /// <param name="originalHexagram">主卦实例（仅在处理变卦时有值）</param>
    public void Execute(HexagramInstance hexagram, InquiryTime inquiryTime, HexagramInstance? originalHexagram)
    {
        // 变卦不需要计算神煞
        if (originalHexagram != null)
            return;

        // 遍历所有已注册的神煞计算器进行计算
        var stars = new Dictionary<SymbolicStar, EarthlyBranch[]>();
        foreach (var (symbolicStar, calculator) in _provider.Calculators)
        {
            var branches = calculator(inquiryTime, hexagram);
            if (branches != null)
            {
                stars[symbolicStar] = branches;
            }
        }

        // 存储计算结果
        Result = new SymbolicStarCollection(stars);
    }
}
