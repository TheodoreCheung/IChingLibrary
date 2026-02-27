namespace IChingLibrary.SixLines.Providers.Abstractions;

/// <summary>
/// 六亲计算接口
/// </summary>
public interface ISixKinProvider
{
    /// <summary>
    /// 计算并绑定主卦、变卦（如有）每个爻的六亲
    /// </summary>
    /// <param name="context">构建器上下文</param>
    void BindSixKin(BuilderContext context);
}
