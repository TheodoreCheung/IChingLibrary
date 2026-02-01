namespace IChingLibrary.SixLines.Providers.Abstractions;

/// <summary>
/// 伏神提供器接口
/// </summary>
public interface IHiddenDeityProvider
{
    /// <summary>
    /// 计算并绑定伏神
    /// </summary>
    /// <param name="hexagram">主卦实例</param>
    /// <param name="inquiryTime">起卦时间信息</param>
    /// <param name="najiaProvider">纳甲提供器（应与主卦使用相同的实例）</param>
    /// <param name="sixKinProvider">六亲提供器（应与主卦使用相同的实例）</param>
    void BindHiddenDeity(HexagramInstance hexagram, InquiryTime inquiryTime, INajiaProvider najiaProvider, ISixKinProvider sixKinProvider);
}
