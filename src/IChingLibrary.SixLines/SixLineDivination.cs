using IChingLibrary.SixLines.Builders;

namespace IChingLibrary.SixLines;

/// <summary>
/// 六爻占卜，包含起卦时间和卦象信息
/// </summary>
public class SixLineDivination
{
    /// <summary>
    /// 问时信息
    /// </summary>
    public InquiryTime InquiryTime { get; }

    /// <summary>
    /// 主卦
    /// </summary>
    public HexagramInstance Original { get; }

    /// <summary>
    /// 变卦
    /// </summary>
    public HexagramInstance? Changed { get; }

    /// <summary>
    /// 神煞集合
    /// </summary>
    public SymbolicStarCollection? SymbolicStars { get; }

    internal SixLineDivination(InquiryTime inquiryTime, HexagramInstance original, HexagramInstance? changed = null, SymbolicStarCollection? symbolicStars = null)
    {
        InquiryTime = inquiryTime;
        Original = original;
        Changed = changed;
        SymbolicStars = symbolicStars;
    }

    /// <summary>
    /// 创建构建器（用于自定义流程）
    /// </summary>
    /// <param name="inquiryTime">起卦时间</param>
    /// <returns>构建器实例</returns>
    /// <remarks>
    /// 使用 Builder 可以自定义起卦方式和处理流程。示例：
    /// <code>
    /// // 时间起卦 + 默认流程
    /// var divination = SixLineDivination.CreateBuilder(inquiryTime)
    ///     .UseTimeBasedHexagram()
    ///     .WithDefaultSteps()
    ///     .Build();
    ///
    /// // 随机数起卦 + 自定义流程
    /// var divination = SixLineDivination.CreateBuilder(inquiryTime)
    ///     .UseRandomHexagram(upperNum, lowerNum)
    ///     .WithNajia()
    ///     .WithPosition()
    ///     .Build();
    ///
    /// // 直接指定卦象 + 自定义流程
    /// var divination = SixLineDivination.CreateBuilder(inquiryTime)
    ///     .UseHexagram(originalHex, changedHex)
    ///     .WithDefaultSteps()
    ///     .Build();
    /// </code>
    /// </remarks>
    public static SixLineDivinationBuilder CreateBuilder(DateTimeOffset inquiryTime)
    {
        return new SixLineDivinationBuilder(inquiryTime);
    }

    /// <summary>
    /// 创建六爻占卜（使用默认完整流程：纳甲+世应+六亲+六神）
    /// </summary>
    /// <param name="inquiryTime">起卦时间</param>
    /// <param name="fourSymbols">六个四象值</param>
    /// <returns>六爻占卜实例</returns>
    public static SixLineDivination Create(DateTimeOffset inquiryTime, FourSymbol[] fourSymbols)
    {
        return CreateBuilder(inquiryTime)
            .UseFourSymbols(fourSymbols)
            .WithDefaultSteps()
            .Build();
    }

    /// <summary>
    /// 创建六爻占卜（byte[] 版本，使用默认完整流程）
    /// </summary>
    /// <param name="inquiryTime">起卦时间</param>
    /// <param name="fourSymbolValues">六个四象值</param>
    /// <returns>六爻占卜实例</returns>
    public static SixLineDivination Create(DateTimeOffset inquiryTime, byte[] fourSymbolValues)
    {
        return CreateBuilder(inquiryTime)
            .UseFourSymbols(fourSymbolValues)
            .WithDefaultSteps()
            .Build();
    }

    /// <summary>
    /// 时间起卦法（根据年月日时自动起卦）
    /// </summary>
    /// <param name="inquiryTime">起卦时间</param>
    /// <returns>六爻占卜实例</returns>
    public static SixLineDivination Create(DateTimeOffset inquiryTime)
    {
        return CreateBuilder(inquiryTime)
            .UseTimeBasedHexagram()
            .WithDefaultSteps()
            .Build();
    }

    /// <summary>
    /// 随机数起卦
    /// </summary>
    /// <param name="inquiryTime">起卦时间</param>
    /// <param name="upperTrigramNumber">上卦随机数</param>
    /// <param name="lowerTrigramNumber">下卦随机数</param>
    /// <param name="changingLineNumber">动爻随机数（可选）</param>
    /// <returns>六爻占卜实例</returns>
    public static SixLineDivination Create(
        DateTimeOffset inquiryTime,
        int upperTrigramNumber,
        int lowerTrigramNumber,
        int? changingLineNumber = null)
    {
        return CreateBuilder(inquiryTime)
            .UseRandomHexagram(upperTrigramNumber, lowerTrigramNumber, changingLineNumber)
            .WithDefaultSteps()
            .Build();
    }

    /// <summary>
    /// 指定主卦和变卦起卦
    /// </summary>
    /// <param name="inquiryTime">起卦时间</param>
    /// <param name="original">主卦</param>
    /// <param name="changed">变卦（可选）</param>
    /// <returns>六爻占卜实例</returns>
    public static SixLineDivination Create(
        DateTimeOffset inquiryTime,
        Hexagram original,
        Hexagram? changed = null)
    {
        return CreateBuilder(inquiryTime)
            .UseHexagram(original, changed)
            .WithDefaultSteps()
            .Build();
    }

    /// <summary>
    /// 指定主卦值和变卦值起卦
    /// </summary>
    /// <param name="inquiryTime">起卦时间</param>
    /// <param name="originalValue">主卦值</param>
    /// <param name="changedValue">变卦值（可选）</param>
    /// <returns>六爻占卜实例</returns>
    public static SixLineDivination Create(
        DateTimeOffset inquiryTime,
        byte originalValue,
        byte? changedValue = null)
    {
        var original = Hexagram.FromValue(originalValue);
        Hexagram? changed = changedValue.HasValue ? Hexagram.FromValue(changedValue.Value) : null;
        return Create(inquiryTime, original, changed);
    }
}
