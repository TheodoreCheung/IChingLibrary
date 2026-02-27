using IChingLibrary.SixLines.Providers.Abstractions;

namespace IChingLibrary.SixLines.Providers;

/// <summary>
/// 默认伏神提供器
/// </summary>
internal class DefaultHiddenDeityProvider : IHiddenDeityProvider
{
    /// <inheritdoc />
    public void BindHiddenDeity(BuilderContext context, INajiaProvider najiaProvider, ISixKinProvider sixKinProvider)
    {
        if (context.Original is null)
            throw new InvalidOperationException("未找到主卦");
        
        // 1. 统计主卦现有的六亲
        var existingKins = new HashSet<SixKin>();
        foreach (var line in context.Original.Lines)
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
        var palace = context.Original.Meta.Palace;
        var originalHexagram = Hexagram.Create(palace, palace);

        // 4. 创建本宫卦实例并执行纳甲和六亲绑定
        var palaceBuilderContext = new BuilderContext(context.SolarInquiryTime)
        {
            Original = new HexagramInstance(originalHexagram)
        };
        najiaProvider.BindStemBranches(palaceBuilderContext);
        sixKinProvider.BindSixKin(palaceBuilderContext);

        // 5. 按位置对应查找伏神
        // 对于每个爻位，检查本宫卦对应位置的爻的六亲是否在主卦中缺少
        for (var i = 0; i < 6; i++)
        {
            var originalLine = palaceBuilderContext.Original.Lines[i];

            // 如果本宫卦此位置的六亲在主卦中缺少
            if (missingKins.Contains(originalLine.SixKin))
            {
                // 将本宫卦的爻作为伏神绑定到主卦对应位置的爻
                context.Original.Lines[i].HiddenDeity = HiddenDeityInfo.FromLine(originalLine);
            }
        }
    }
}
