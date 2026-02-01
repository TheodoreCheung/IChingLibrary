using IChingLibrary.SixLines.Providers;
using IChingLibrary.SixLines.Providers.Abstractions;

namespace IChingLibrary.SixLines.Builders;

/// <summary>
/// 六爻占卜构建器
/// </summary>
public sealed class SixLineDivinationBuilder
{
    /// <summary>
    /// 起卦时间
    /// </summary>
    private readonly DateTimeOffset _inquiryTime;

    /// <summary>
    /// 四象值数组（从初爻到上爻）
    /// </summary>
    private FourSymbol[]? _fourSymbols;

    /// <summary>
    /// 主卦处理步骤列表
    /// </summary>
    private readonly List<ISixLineStep> _originalSteps = [];

    /// <summary>
    /// 变卦处理步骤列表
    /// </summary>
    private readonly List<ISixLineStep> _changedSteps = [];

    /// <summary>
    /// 起卦时间信息转换器
    /// </summary>
    private IInquiryTimeProvider? _inquiryTimeProvider;

    /// <summary>
    /// 纳甲提供器
    /// </summary>
    private INajiaProvider? _najiaProvider;

    /// <summary>
    /// 六亲提供器
    /// </summary>
    private ISixKinProvider? _sixKinProvider;

    /// <summary>
    /// 神煞步骤引用（用于获取计算结果）
    /// </summary>
    private SymbolicStarStep? _symbolicStarStep;

    /// <summary>
    /// 获取纳甲提供器（延迟创建）
    /// </summary>
    private INajiaProvider NajiaProvider => _najiaProvider ?? new DefaultNajiaProvider();

    /// <summary>
    /// 获取六亲提供器（延迟创建）
    /// </summary>
    private ISixKinProvider SixKinProvider => _sixKinProvider ?? new DefaultSixKinProvider();

    /// <summary>
    /// 初始化构建器
    /// </summary>
    /// <param name="inquiryTime">起卦时间</param>
    public SixLineDivinationBuilder(DateTimeOffset inquiryTime)
    {
        _inquiryTime = inquiryTime;
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

        // 防御性复制，避免外部修改影响内部状态
        _fourSymbols = [..fourSymbols];
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

        _fourSymbols = new FourSymbol[6];
        for (var i = 0; i < 6; i++)
        {
            _fourSymbols[i] = FourSymbol.FromValue(fourSymbolValues[i]);
        }
        return this;
    }

    /// <summary>
    /// 使用时间起卦法（根据年月日时自动起卦）
    /// </summary>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder UseTimeBasedHexagram()
    {
        _fourSymbols = HexagramGenerator.FromTime(_inquiryTime);
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
        _fourSymbols = HexagramGenerator.FromRandomNumbers(
            _inquiryTime,
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
        _fourSymbols = HexagramGenerator.FromHexagrams(original, changed);
        return this;
    }

    /// <summary>
    /// 设置起卦时间信息转换器
    /// </summary>
    /// <param name="provider">问时信息转换器</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithInquiryTimeProvider(IInquiryTimeProvider provider)
    {
        _inquiryTimeProvider = provider;
        return this;
    }

    /// <summary>
    /// 添加纳甲步骤（主卦）
    /// </summary>
    /// <param name="provider">纳甲提供器（为 null 时使用默认实现）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithNajia(INajiaProvider? provider = null)
    {
        _najiaProvider = provider;
        _originalSteps.Add(new NajiaStep(NajiaProvider));
        return this;
    }

    /// <summary>
    /// 添加世应位置步骤（主卦）
    /// </summary>
    /// <param name="provider">世应位置提供器（为 null 时使用默认实现）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithPosition(IPositionProvider? provider = null)
    {
        _originalSteps.Add(new PositionStep(provider));
        return this;
    }

    /// <summary>
    /// 添加六亲步骤（主卦）
    /// </summary>
    /// <param name="provider">六亲提供器（为 null 时使用默认实现）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithSixKin(ISixKinProvider? provider = null)
    {
        _sixKinProvider = provider;
        _originalSteps.Add(new SixKinStep(SixKinProvider, useOriginalPalace: false));
        return this;
    }

    /// <summary>
    /// 添加六神步骤（主卦）
    /// </summary>
    /// <param name="provider">六神提供器（为 null 时使用默认实现）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithSixSpirit(ISixSpiritProvider? provider = null)
    {
        _originalSteps.Add(new SixSpiritStep(provider));
        return this;
    }

    /// <summary>
    /// 添加伏神步骤（主卦）
    /// </summary>
    /// <param name="provider">伏神提供器（为 null 时使用默认实现）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithHiddenDeity(IHiddenDeityProvider? provider = null)
    {
        // 使用与主卦相同的纳甲和六亲 Provider
        _originalSteps.Add(new HiddenDeityStep(provider, NajiaProvider, SixKinProvider));
        return this;
    }

    /// <summary>
    /// 添加纳甲步骤（变卦）
    /// </summary>
    /// <param name="provider">纳甲提供器（为 null 时使用默认实现）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithNajiaForChanged(INajiaProvider? provider = null)
    {
        _changedSteps.Add(new NajiaStep(provider));
        return this;
    }

    /// <summary>
    /// 添加六亲步骤（变卦，使用主卦卦宫五行）
    /// </summary>
    /// <param name="provider">六亲提供器（为 null 时使用默认实现）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithSixKinForChanged(ISixKinProvider? provider = null)
    {
        _changedSteps.Add(new SixKinStep(provider, useOriginalPalace: true));
        return this;
    }

    /// <summary>
    /// 配置神煞计算（主卦）
    /// </summary>
    /// <param name="configureProvider">配置神煞提供器的动作（为 null 时使用默认所有神煞）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithSymbolicStars(Action<DefaultSymbolicStarProvider>? configureProvider = null)
    {
        var provider = new DefaultSymbolicStarProvider();
        configureProvider?.Invoke(provider);

        _symbolicStarStep = new SymbolicStarStep(provider);
        _originalSteps.Add(_symbolicStarStep);

        return this;
    }

    /// <summary>
    /// 添加默认完整流程（主卦：纳甲+世应+六亲+伏神+六神+神煞，变卦：纳甲+六亲）
    /// </summary>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithDefaultSteps()
    {
        return WithNajia()
            .WithPosition()
            .WithSixKin()
            .WithHiddenDeity()
            .WithSixSpirit()
            .WithSymbolicStars()
            .WithNajiaForChanged()
            .WithSixKinForChanged();
    }

    /// <summary>
    /// 添加自定义步骤
    /// </summary>
    /// <param name="step">自定义步骤</param>
    /// <param name="forChanged">是否应用于变卦（false 表示应用于主卦）</param>
    /// <returns>构建器实例</returns>
    public SixLineDivinationBuilder WithCustomStep(ISixLineStep step, bool forChanged = false)
    {
        if (forChanged)
            _changedSteps.Add(step);
        else
            _originalSteps.Add(step);
        return this;
    }

    /// <summary>
    /// 构建六爻占卜实例
    /// </summary>
    /// <returns>六爻占卜实例</returns>
    /// <exception cref="InvalidOperationException">未调用起卦方法时抛出</exception>
    public SixLineDivination Build()
    {
        // 0. 验证四象值已设置
        if (_fourSymbols is null)
            throw new InvalidOperationException("必须先调用起卦方法（如 UseTimeBasedHexagram、UseRandomHexagram、UseFourSymbols 或 UseHexagram）");

        // 1. 转换起卦时间信息
        var inquiryTimeProvider = _inquiryTimeProvider ?? new DefaultInquiryTimeProvider();
        var convertedInquiryTime = inquiryTimeProvider.ConvertFrom(_inquiryTime);

        // 2. 从四象值计算卦值，并标记变爻
        byte hexagramValue = 0;
        var isChangingLines = new bool[6];

        for (var i = 0; i < 6; i++)
        {
            var symbol = _fourSymbols[i];
            var yinYang = symbol.YinYang;
            var bitValue = yinYang == YinYang.Yang ? 1 : 0;
            hexagramValue |= (byte)(bitValue << i);

            isChangingLines[i] = symbol.Value == 6 || symbol.Value == 9;
        }

        // 3. 创建主卦实例
        var originalHexagram = Hexagram.FromValue(hexagramValue);
        var originalInstance = new HexagramInstance(originalHexagram);

        // 4. 标记变爻并计算变卦值（合并遍历）
        byte changedValue = hexagramValue;
        var hasChanging = false;

        for (var i = 0; i < 6; i++)
        {
            if (!isChangingLines[i]) continue;
            
            originalInstance.Lines[i].IsChanging = true;
            changedValue ^= (byte)(1 << i);
            hasChanging = true;
        }

        // 5. 执行主卦步骤
        foreach (var step in _originalSteps)
        {
            step.Execute(originalInstance, convertedInquiryTime, null);
        }

        // 6. 生成变卦（如有变爻）
        HexagramInstance? changedInstance = null;
        if (hasChanging)
        {
            var changedHexagram = Hexagram.FromValue(changedValue);
            changedInstance = new HexagramInstance(changedHexagram);

            // 执行变卦步骤
            foreach (var step in _changedSteps)
            {
                step.Execute(changedInstance, convertedInquiryTime, originalInstance);
            }
        }

        // 7. 获取神煞集合并创建六爻占卜实例
        return new SixLineDivination(convertedInquiryTime, originalInstance, changedInstance, _symbolicStarStep?.Result);
    }
}
