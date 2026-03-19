namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 缓存八宫本宫卦的纳甲与六亲模板，避免伏神计算时重复构建整套占断对象。
/// </summary>
internal static class PalaceHexagramTemplateCache
{
    private static readonly IReadOnlyDictionary<Trigram, HiddenDeityInfo[]> Templates = CreateTemplates();

    internal static HiddenDeityInfo[] GetTemplate(Trigram palace) => Templates[palace];

    private static IReadOnlyDictionary<Trigram, HiddenDeityInfo[]> CreateTemplates()
    {
        var templates = new Dictionary<Trigram, HiddenDeityInfo[]>();
        foreach (var palace in Trigram.GetAll())
        {
            templates[palace] = CreateTemplate(palace);
        }

        return templates;
    }

    private static HiddenDeityInfo[] CreateTemplate(Trigram palace)
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
