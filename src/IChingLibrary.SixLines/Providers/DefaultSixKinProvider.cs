using IChingLibrary.SixLines.Providers.Abstractions;

namespace IChingLibrary.SixLines.Providers;

/// <summary>
/// 默认六亲提供器
/// </summary>
internal class DefaultSixKinProvider : ISixKinProvider
{
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

    /// <inheritdoc />
    public void BindSixKin(BuilderContext context)
    {
        if (context.Original is null)
            throw new InvalidOperationException("未找到主卦");

        var palaceFivePhase = context.Original.Meta.Palace.FivePhase;

        for (var i = 0; i < 6; i++)
        {
            context.Original.Lines[i].SixKin =
                GetSixKin(palaceFivePhase, context.Original.Lines[i].StemBranch.Branch.FivePhase);

            context.Changed?.Lines[i].SixKin = 
                GetSixKin(palaceFivePhase, context.Changed.Lines[i].StemBranch.Branch.FivePhase);
        }
    }
}
