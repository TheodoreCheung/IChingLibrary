namespace IChingLibrary.SixLines.Providers.Abstractions;

/// <summary>
/// 六神计算接口
/// </summary>
public interface ISixSpiritProvider
{
    /// <summary>
    /// 计算并绑定主卦每个爻的六神
    /// </summary>
    /// <param name="context">构建器上下文</param>
    void BindSixSpirits(BuilderContext context);
}
