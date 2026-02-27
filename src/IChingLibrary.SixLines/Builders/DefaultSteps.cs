using IChingLibrary.SixLines.Providers;
using IChingLibrary.SixLines.Providers.Abstractions;

namespace IChingLibrary.SixLines;

/// <summary>
/// 起卦时间步骤
/// </summary>
/// <param name="provider">问时信息转换器</param>
public sealed class InquiryTimeStep(IInquiryTimeProvider provider) : IBuildStep
{
    /// <inheritdoc />
    public void Execute(BuilderContext context)
    {
        context.InquiryTime = provider.ConvertFrom(context.SolarInquiryTime);
    }
}

/// <summary>
/// 纳甲步骤
/// </summary>
public sealed class NajiaStep(INajiaProvider provider) : IBuildStep
{
    /// <inheritdoc />
    public void Execute(BuilderContext context)
    {
        provider.BindStemBranches(context);
    }
}

/// <summary>
/// 世应位置步骤
/// </summary>
public sealed class PositionStep(IPositionProvider provider) : IBuildStep
{
    /// <inheritdoc />
    public void Execute(BuilderContext context)
    {
        provider.BindPositions(context);
    }
}

/// <summary>
/// 六亲步骤
/// </summary>
public sealed class SixKinStep(ISixKinProvider provider) : IBuildStep
{
    /// <inheritdoc />
    public void Execute(BuilderContext context)
    {
        provider.BindSixKin(context);
    }
}

/// <summary>
/// 六神步骤
/// </summary>
public sealed class SixSpiritStep(ISixSpiritProvider provider) : IBuildStep
{
    /// <inheritdoc />
    public void Execute(BuilderContext context)
    {
        provider.BindSixSpirits(context);
    }
}

/// <summary>
/// 伏神步骤
/// </summary>
public sealed class HiddenDeityStep(IHiddenDeityProvider hiddenDeityProvider, INajiaProvider najiaProvider, ISixKinProvider sixKinProvider) : IBuildStep
{
    /// <inheritdoc />
    public void Execute(BuilderContext context)
    {
        hiddenDeityProvider.BindHiddenDeity(context, najiaProvider, sixKinProvider);
    }
}

/// <summary>
/// 神煞步骤（Symbolic Star Step）
/// </summary>
public sealed class SymbolicStarStep(ISymbolicStarProvider provider) : IBuildStep
{
    /// <summary>
    /// 执行神煞步骤
    /// </summary>
    /// <param name="context">构建上下文</param>
    public void Execute(BuilderContext context)
    {
        context.SymbolicStars = provider.Calculate(context);
    }
}
