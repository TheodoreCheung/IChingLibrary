namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 伏神计算步骤
/// </summary>
public class HiddenDeityStep : IStructuringStep
{
    private static readonly Dictionary<Trigram, HiddenDeityInfo[]> _palaceTemplateCache = [];
    private static readonly Lock _palaceTemplateCacheLock = new();

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
        var missingKins = SixKin.GetAll()
            .Where(kin => !existingKins.Contains(kin))
            .ToHashSet();

        if (missingKins.Count == 0)
        {
            // 没有缺少的六亲，不需要找伏神
            return;
        }

        // 3. 获取本宫卦模板（按需缓存纳甲和六亲结果）
        var palaceTemplate = GetOrCreatePalaceTemplate(context.SixLineDivination.Original.Meta.Palace);

        // 4. 按位置对应查找伏神
        for (var i = 0; i < 6; i++)
        {
            var palaceLine = palaceTemplate[i];

            // 如果本宫卦此位置的六亲在主卦中缺少
            if (missingKins.Contains(palaceLine.SixKin))
            {
                // 将本宫卦的爻作为伏神绑定到主卦对应位置的爻
                context.SixLineDivination.Original.Lines[i].HiddenDeity = palaceLine;
            }
        }
    }

    private static HiddenDeityInfo[] GetOrCreatePalaceTemplate(Trigram palace)
    {
        using (_palaceTemplateCacheLock.EnterScope())
        {
            if (_palaceTemplateCache.TryGetValue(palace, out var template))
            {
                return template;
            }

            template = CreatePalaceTemplate(palace);
            _palaceTemplateCache[palace] = template;
            return template;
        }
    }

    private static HiddenDeityInfo[] CreatePalaceTemplate(Trigram palace)
    {
        var hexagram = new HexagramInstance(Hexagram.Create(palace, palace));
        NajiaStep.Bind(hexagram);

        var palacePhase = palace.FivePhase;
        var template = new HiddenDeityInfo[hexagram.Lines.Count];
        for (var i = 0; i < hexagram.Lines.Count; i++)
        {
            hexagram.Lines[i].SixKin =
                SixKinStep.GetSixKin(palacePhase, hexagram.Lines[i].StemBranch.Branch.FivePhase);
            template[i] = HiddenDeityInfo.FromLine(hexagram.Lines[i]);
        }

        return template;
    }
}
