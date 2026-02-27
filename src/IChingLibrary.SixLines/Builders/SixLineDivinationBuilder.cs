using IChingLibrary.SixLines.Providers;
using IChingLibrary.SixLines.Providers.Abstractions;

namespace IChingLibrary.SixLines;

/// <summary>
/// 六爻占卜构建器
/// </summary>
public sealed class SixLineDivinationBuilder
{
    private readonly List<IBuildStep> _steps = [];

    private readonly BuilderContext _context;

    /// <summary>
    /// 初始化构建器
    /// </summary>
    /// <param name="inquiryTime">起卦时间</param>
    public SixLineDivinationBuilder(DateTimeOffset inquiryTime)
    {
        _context = new BuilderContext(inquiryTime);
    }

    /// <summary>
    /// 使用指定的四象值起卦
    /// </summary>
    /// <param name="fourSymbols">六个四象值（从初爻到上爻）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder UseFourSymbols(FourSymbol[] fourSymbols)
    {
        if (fourSymbols.Length != 6)
            throw new ArgumentException("必须提供6个四象值", nameof(fourSymbols));

        _context.FourSymbols = [..fourSymbols];
        
        return this;
    }

    /// <summary>
    /// 使用指定的四象值起卦（byte[] 版本）
    /// </summary>
    /// <param name="fourSymbolValues">六个四象值（从初爻到上爻）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder UseFourSymbols(byte[] fourSymbolValues)
    {
        if (fourSymbolValues.Length != 6)
            throw new ArgumentException("必须提供6个四象值", nameof(fourSymbolValues));

        _context.FourSymbols = new FourSymbol[6];
        for (var i = 0; i < 6; i++)
        {
            _context.FourSymbols[i] = FourSymbol.FromValue(fourSymbolValues[i]);
        }
        
        return this;
    }

    /// <summary>
    /// 使用时间起卦法（根据年月日时自动起卦）
    /// </summary>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder UseTimeBasedHexagram()
    {
        _context.FourSymbols = HexagramGenerator.FromTime(_context.SolarInquiryTime);
        return this;
    }

    /// <summary>
    /// 使用随机数起卦法
    /// </summary>
    /// <param name="upperTrigramNumber">上卦随机数</param>
    /// <param name="lowerTrigramNumber">下卦随机数</param>
    /// <param name="changingLineNumber">动爻随机数（可选）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder UseRandomHexagram(
        int upperTrigramNumber,
        int lowerTrigramNumber,
        int? changingLineNumber = null)
    {
        _context.FourSymbols = HexagramGenerator.FromRandomNumbers(
            _context.SolarInquiryTime,
            upperTrigramNumber,
            lowerTrigramNumber,
            changingLineNumber);
        return this;
    }

    /// <summary>
    /// 使用指定的卦象起卦
    /// </summary>
    /// <param name="original">主卦</param>
    /// <param name="changed">变卦（可选）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder UseHexagram(Hexagram original, Hexagram? changed = null)
    {
        _context.FourSymbols = HexagramGenerator.FromHexagrams(original, changed);
        return this;
    }

    /// <summary>
    /// 添加起卦时间转换步骤
    /// </summary>
    /// <param name="provider">问时信息转换器</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithInquiryTimeProvider(IInquiryTimeProvider? provider = null)
    {
        _steps.Add(new InquiryTimeStep(provider ?? new DefaultInquiryTimeProvider()));

        return this;
    }

    /// <summary>
    /// 添加纳甲步骤（主卦和变卦）
    /// </summary>
    /// <param name="provider">纳甲提供器（为 null 时使用默认实现）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithNajia(INajiaProvider? provider = null)
    {
        _steps.Add(new NajiaStep(provider ?? new DefaultNajiaProvider()));
        
        return this;
    }

    /// <summary>
    /// 添加世应位置步骤（仅主卦）
    /// </summary>
    /// <param name="provider">世应位置提供器（为 null 时使用默认实现）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithPosition(IPositionProvider? provider = null)
    {
        _steps.Add(new PositionStep(provider ?? new DefaultPositionProvider()));
        
        return this;
    }

    /// <summary>
    /// 添加六亲步骤（主卦和变卦）
    /// </summary>
    /// <param name="provider">六亲提供器（为 null 时使用默认实现）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithSixKin(ISixKinProvider? provider = null)
    {
        _steps.Add(new SixKinStep(provider ?? new DefaultSixKinProvider()));
        
        return this;
    }

    /// <summary>
    /// 添加六神步骤（仅主卦）
    /// </summary>
    /// <param name="provider">六神提供器（为 null 时使用默认实现）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithSixSpirit(ISixSpiritProvider? provider = null)
    {
        _steps.Add(new SixSpiritStep(provider ?? new DefaultSixSpiritProvider()));
        return this;
    }

    /// <summary>
    /// 添加伏神步骤（仅主卦）
    /// </summary>
    /// <param name="provider">伏神提供器（为 null 时使用默认实现）</param>
    /// <param name="najiaProvider">纳甲提供器（为 null 时使用默认实现）</param>
    /// <param name="sixKinProvider">六亲提供器（为 null 时使用默认实现）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithHiddenDeity(IHiddenDeityProvider? provider = null, INajiaProvider? najiaProvider = null, ISixKinProvider? sixKinProvider = null)
    {
        _steps.Add(new HiddenDeityStep(
            provider ?? new DefaultHiddenDeityProvider(),
            najiaProvider ?? new DefaultNajiaProvider(),
            sixKinProvider ?? new DefaultSixKinProvider()));
        return this;
    }

    /// <summary>
    /// 配置神煞计算（仅主卦）
    /// </summary>
    /// <param name="configureProvider">配置神煞提供器的动作（为 null 时使用默认所有神煞）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithSymbolicStars(Action<DefaultSymbolicStarProvider>? configureProvider = null)
    {
        var provider = new DefaultSymbolicStarProvider();
        configureProvider?.Invoke(provider);

        _steps.Add(new SymbolicStarStep(provider));

        return this;
    }

    /// <summary>
    /// 添加默认完整流程（主卦：纳甲+世应+六亲+伏神+六神+神煞，变卦：纳甲+六亲）
    /// </summary>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithDefaultSteps()
    {
        return WithInquiryTimeProvider()
            .WithNajia()
            .WithPosition()
            .WithSixKin()
            .WithHiddenDeity()
            .WithSixSpirit()
            .WithSymbolicStars();
    }

    /// <summary>
    /// 添加自定义步骤
    /// </summary>
    /// <param name="step">自定义步骤</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithCustomStep(IBuildStep step)
    {
        _steps.Add(step);
        return this;
    }

    /// <summary>
    /// 构建六爻占卜实例
    /// </summary>
    /// <returns>六爻占卜实例</returns>
    /// <exception cref="InvalidOperationException">未调用起卦方法时抛出</exception>
    public SixLineDivination Build()
    {
        if (_context.FourSymbols is null)
            throw new InvalidOperationException("必须先调用起卦方法（如 UseTimeBasedHexagram 等）");
        
        byte originalValue = 0;
        byte changingMask = 0;
        
        for (var i = 0; i < 6; i++)
        {
            var symbol = _context.FourSymbols[i];
            if (symbol.YinYang == YinYang.Yang)
                originalValue |= (byte)(1 << i);
            if (symbol.IsChanging)
                changingMask |= (byte)(1 << i);
        }

        var hasChanging = changingMask != 0;
        var changedValue = (byte)(originalValue ^ changingMask);

        _context.Original = new HexagramInstance(Hexagram.FromValue(originalValue));
        for (var i = 0; i < 6; i++)
        {
            if (_context.FourSymbols[i].IsChanging) 
                _context.Original.Lines[i].IsChanging = true;
        }

        if (hasChanging)
        {
            _context.Changed = new HexagramInstance(Hexagram.FromValue(changedValue));
        }

        // 若未显式添加 InquiryTimeStep，则使用默认时间转换
        _context.InquiryTime ??= new DefaultInquiryTimeProvider().ConvertFrom(_context.SolarInquiryTime);

        foreach (var step in _steps)
        {
            step.Execute(_context);
        }

        return new SixLineDivination(_context.InquiryTime.Value, _context.Original, _context.Changed, _context.SymbolicStars);
    }
}
