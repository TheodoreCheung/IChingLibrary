namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 起卦上下文
/// </summary>
/// <param name="sixLineDivination">起卦结果（种子数据）</param>
public class DivinationContext(SixLineDivination sixLineDivination)
{
    /// <summary>
    /// 六爻占卜结果
    /// </summary>
    internal SixLineDivination SixLineDivination { get; } = sixLineDivination;
}
