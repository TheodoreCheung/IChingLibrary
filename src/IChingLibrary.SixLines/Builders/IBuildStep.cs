namespace IChingLibrary.SixLines;

/// <summary>
/// 六爻卦构建步骤接口
/// </summary>
public interface IBuildStep
{
    /// <summary>
    /// 执行步骤
    /// </summary>
    /// <param name="context">构建器上下文</param>
    void Execute(BuilderContext context);
}