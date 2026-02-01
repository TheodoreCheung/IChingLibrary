namespace IChingLibrary.SixLines.Providers.Abstractions;

/// <summary>
/// 六亲计算接口
/// </summary>
public interface ISixKinProvider
{
    /// <summary>
    /// 计算并绑定每个爻的六亲
    /// </summary>
    /// <param name="hexagram">卦实例</param>
    void BindSixKin(HexagramInstance hexagram);

    /// <summary>
    /// 使用指定的卦宫五行计算并绑定每个爻的六亲
    /// </summary>
    /// <param name="hexagram">卦实例</param>
    /// <param name="palacePhase">卦宫五行</param>
    void BindSixKin(HexagramInstance hexagram, FivePhase palacePhase);
}
