namespace IChingLibrary.SixLines.Providers.Abstractions;

/// <summary>
/// 六神计算接口
/// </summary>
public interface ISixSpiritProvider
{
    /// <summary>
    /// 计算并绑定每个爻的六神
    /// </summary>
    /// <param name="hexagram">卦实例</param>
    /// <param name="inquiryTime">起卦时间信息</param>
    void BindSixSpirits(HexagramInstance hexagram, InquiryTime inquiryTime);
}
