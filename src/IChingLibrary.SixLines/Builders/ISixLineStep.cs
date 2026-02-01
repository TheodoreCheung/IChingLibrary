namespace IChingLibrary.SixLines.Builders;

/// <summary>
/// 六爻占卜构建步骤接口
/// </summary>
public interface ISixLineStep
{
    /// <summary>
    /// 执行此步骤
    /// </summary>
    /// <param name="hexagram">要处理的卦实例</param>
    /// <param name="inquiryTime">问时信息</param>
    /// <param name="originalHexagram">主卦实例（仅在处理变卦时有值）</param>
    void Execute(HexagramInstance hexagram, InquiryTime inquiryTime, HexagramInstance? originalHexagram);
}
