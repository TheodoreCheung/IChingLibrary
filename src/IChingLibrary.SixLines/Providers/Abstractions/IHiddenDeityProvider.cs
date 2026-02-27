namespace IChingLibrary.SixLines.Providers.Abstractions;

/// <summary>
/// 伏神提供器接口
/// </summary>
public interface IHiddenDeityProvider
{
    /// <summary>
    /// 计算并绑定伏神
    /// </summary>
    /// <param name="context">构建器上下文</param>
    /// <param name="najiaProvider">纳甲提供器（应与主卦使用相同的实例）</param>
    /// <param name="sixKinProvider">六亲提供器（应与主卦使用相同的实例）</param>
    void BindHiddenDeity(BuilderContext context, INajiaProvider najiaProvider, ISixKinProvider sixKinProvider);
}
