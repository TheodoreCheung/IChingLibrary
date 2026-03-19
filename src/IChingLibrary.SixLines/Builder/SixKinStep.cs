namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 绑六亲步骤
/// </summary>
public sealed class SixKinStep : IStructuringStep
{
    /// <summary>
    /// 根据卦宫五行和爻五行计算六亲
    /// </summary>
    /// <param name="palacePhase">卦宫五行</param>
    /// <param name="linePhase">爻五行</param>
    /// <returns>六亲</returns>
    internal static SixKin GetSixKin(FivePhase palacePhase, FivePhase linePhase)
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
    public IEnumerable<Type> RequiredSteps { get; } = [typeof(NajiaStep)];

    /// <inheritdoc />
    public void Execute(DivinationContext context)
    {
        var palaceFivePhase = context.SixLineDivination.Original.Meta.Palace.FivePhase;

        for (var i = 0; i < 6; i++)
        {
            context.SixLineDivination.Original.Lines[i].SixKin =
                GetSixKin(palaceFivePhase, context.SixLineDivination.Original.Lines[i].StemBranch.Branch.FivePhase);

            context.SixLineDivination.Changed?.Lines[i].SixKin = 
                GetSixKin(palaceFivePhase, context.SixLineDivination.Changed.Lines[i].StemBranch.Branch.FivePhase);
        }
    }
}
