namespace IChingLibrary.SixLines.Providers.Abstractions;

/// <summary>
/// 纳甲法接口
/// </summary>
public interface INajiaProvider
{
    /// <summary>
    /// 为主卦，变卦（如有）中的每个爻绑定干支（纳甲法）
    /// </summary>
    /// <param name="context">构建器上下文</param>
    void BindStemBranches(BuilderContext context);
}