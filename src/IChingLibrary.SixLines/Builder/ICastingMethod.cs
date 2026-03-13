namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 起卦方式接口
/// </summary>
public interface ICastingMethod
{
    /// <summary>
    /// 执行起卦并生成仅包含基本信息的六爻盘
    /// </summary>
    /// <returns>仅包含基本信息的六爻盘</returns>
    SixLineDivination Cast();
}
