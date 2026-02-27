namespace IChingLibrary.SixLines.Providers.Abstractions;

/// <summary>
/// 世应位置计算接口
/// </summary>
public interface IPositionProvider
{
    /// <summary>
    /// 计算并绑定世爻和应爻位置
    /// </summary>
    /// <param name="context">构建器上下文</param>
    void BindPositions(BuilderContext context);
}
