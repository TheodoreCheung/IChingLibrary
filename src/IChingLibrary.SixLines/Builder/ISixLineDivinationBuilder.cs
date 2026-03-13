namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 六爻起卦构建器接口
/// </summary>
public interface ISixLineDivinationBuilder
{
    /// <summary>
    /// 指定起卦方式
    /// </summary>
    /// <param name="castingMethod">起卦方式</param>
    /// <returns>构建器本身</returns>
    ISixLineDivinationBuilder UseMethod(ICastingMethod castingMethod);

    /// <summary>
    /// 使用默认的结构化步骤集合
    /// </summary>
    /// <returns>构建器本身</returns>
    ISixLineDivinationBuilder WithDefaultSteps();

    /// <summary>
    /// 添加一个结构化步骤
    /// </summary>
    /// <param name="structuringStep">结构化步骤</param>
    /// <returns>构建器本身</returns>
    ISixLineDivinationBuilder WithStep(IStructuringStep structuringStep);

    /// <summary>
    /// 构建并返回六爻盘
    /// </summary>
    /// <returns>六爻盘</returns>
    SixLineDivination Build();
}
