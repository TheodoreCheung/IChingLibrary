namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 伏神计算步骤
/// </summary>
public class HiddenDeityStep : IStructuringStep
{
    /// <inheritdoc />
    public IEnumerable<Type> RequiredSteps { get; } = [typeof(SixKinStep)];

    /// <inheritdoc />
    public void Execute(DivinationContext context)
    {
        // 1. 统计主卦现有的六亲
        var existingKins = new HashSet<SixKin>();
        foreach (var line in context.SixLineDivination.Original.Lines)
        {
            existingKins.Add(line.SixKin);
        }

        // 2. 检查是否有缺少的六亲
        var allKins = SixKin.GetAll().ToList();
        var missingKins = allKins.Where(kin => !existingKins.Contains(kin)).ToList();

        if (missingKins.Count == 0)
        {
            // 没有缺少的六亲，不需要找伏神
            return;
        }

        // 3. 获取本宫卦（上下卦相同的卦）
        var palace = context.SixLineDivination.Original.Meta.Palace;
        var palaceHexagram = Hexagram.Create(palace, palace);

        
        // 4. 创建本宫卦实例并执行纳甲和六亲绑定
        var palaceDivination = new SixLineDivinationBuilder()
            .UseMethod(new SpecifyingHexagramCastingMethod(context.SixLineDivination.CastingTime.Solar, palaceHexagram))
            .WithStep(new NajiaStep())
            .WithStep(new SixKinStep())
            .Build();

        // 5. 按位置对应查找伏神
        // 对于每个爻位，检查本宫卦对应位置的爻的六亲是否在主卦中缺少
        for (var i = 0; i < 6; i++)
        {
            var palaceLine = palaceDivination.Original.Lines[i];

            // 如果本宫卦此位置的六亲在主卦中缺少
            if (missingKins.Contains(palaceLine.SixKin))
            {
                // 将本宫卦的爻作为伏神绑定到主卦对应位置的爻
                context.SixLineDivination.Original.Lines[i].HiddenDeity = HiddenDeityInfo.FromLine(palaceLine);
            }
        }
    }
}
