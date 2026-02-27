namespace IChingLibrary.SixLines.Providers.Abstractions;

/// <summary>
/// 神煞提供器接口
/// </summary>
public interface ISymbolicStarProvider
{
    /// <summary>
    /// 计算并返回神煞集合
    /// </summary>
    /// <param name="context">构建器上下文</param>
    /// <returns>神煞集合</returns>
    SymbolicStarCollection Calculate(BuilderContext context);
}
