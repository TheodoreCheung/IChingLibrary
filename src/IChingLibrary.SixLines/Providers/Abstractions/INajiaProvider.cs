namespace IChingLibrary.SixLines.Providers.Abstractions;

/// <summary>
/// 纳甲法接口
/// </summary>
public interface INajiaProvider
{
    /// <summary>
    /// 为卦的每个爻绑定干支（纳甲法）
    /// </summary>
    /// <param name="hexagram">卦实例</param>
    /// <param name="inquiryTime">起卦时间信息（用于确定月干支）</param>
    void BindStemBranches(HexagramInstance hexagram, InquiryTime inquiryTime);

    /// <summary>
    /// 获取指定八卦的纳甲表（6个爻对应的干支）
    /// </summary>
    /// <param name="trigram">八卦</param>
    /// <param name="isInner">是否为内卦（下卦）</param>
    /// <returns>6个干支（从初爻到上爻）</returns>
    StemBranch[] GetNajiaTable(Trigram trigram, bool isInner);
}