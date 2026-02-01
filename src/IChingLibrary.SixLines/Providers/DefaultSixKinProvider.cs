using IChingLibrary.SixLines.Providers.Abstractions;

namespace IChingLibrary.SixLines.Providers;

/// <summary>
/// 默认六亲提供器
/// </summary>
internal class DefaultSixKinProvider : ISixKinProvider
{
    /// <inheritdoc />
    public void BindSixKin(HexagramInstance hexagram)
    {
        BindSixKin(hexagram, hexagram.Meta.Palace.FivePhase);
    }

    /// <inheritdoc />
    public void BindSixKin(HexagramInstance hexagram, FivePhase palacePhase)
    {
        for (int i = 0; i < hexagram.Lines.Count; i++)
        {
            var line = hexagram.Lines[i];
            var linePhase = line.StemBranch.Branch.FivePhase;
            line.SixKin = GetSixKin(palacePhase, linePhase);
        }
    }

    /// <summary>
    /// 根据卦宫五行和爻五行计算六亲
    /// </summary>
    private static SixKin GetSixKin(FivePhase palacePhase, FivePhase linePhase)
    {
        // 父母：生我者（爻生卦宫）
        if (linePhase.Generates(palacePhase))
            return SixKin.Parent;

        // 兄弟：同我者（五行相同）
        if (linePhase == palacePhase)
            return SixKin.Sibling;

        // 妻财：我克者（卦宫克爻）
        if (palacePhase.Restrains(linePhase))
            return SixKin.Wealth;

        // 官鬼：克我者（爻克卦宫）
        if (linePhase.Restrains(palacePhase))
            return SixKin.Officer;

        // 子孙：我生者（卦宫生爻）
        // 默认返回子孙
        return SixKin.Offspring;
    }
}
