namespace IChingLibrary.SixLines;

/// <summary>
/// 卦象在六爻体系中的扩展方法
/// </summary>
public static class HexagramExtensions
{
    private static readonly Dictionary<Hexagram, HexagramNature> NatureMap = new()
    {
        // 六冲卦 (纯卦8个 + 2个特殊)
        { Hexagram.TheCreative, HexagramNature.SixClashes },
        { Hexagram.TheJoyous, HexagramNature.SixClashes },
        { Hexagram.TheClinging, HexagramNature.SixClashes },
        { Hexagram.TheArousing, HexagramNature.SixClashes },
        { Hexagram.TheGentle, HexagramNature.SixClashes },
        { Hexagram.TheAbysmal, HexagramNature.SixClashes },
        { Hexagram.KeepingStill, HexagramNature.SixClashes },
        { Hexagram.TheReceptive, HexagramNature.SixClashes },
        { Hexagram.Innocence, HexagramNature.SixClashes },
        { Hexagram.ThePowerOfTheGreat, HexagramNature.SixClashes },

        // 六合卦 (8个)
        { Hexagram.Standstill, HexagramNature.SixHarmonies },
        { Hexagram.Peace, HexagramNature.SixHarmonies },
        { Hexagram.Limitation, HexagramNature.SixHarmonies },
        { Hexagram.Oppression, HexagramNature.SixHarmonies },
        { Hexagram.TheWanderer, HexagramNature.SixHarmonies },
        { Hexagram.Grace, HexagramNature.SixHarmonies },
        { Hexagram.Enthusiasm, HexagramNature.SixHarmonies },
        { Hexagram.Return, HexagramNature.SixHarmonies },

        // 游魂卦 (各宫第7卦，8个)
        { Hexagram.Progress, HexagramNature.WanderingSoul },
        { Hexagram.PreponderanceOfTheSmall, HexagramNature.WanderingSoul },
        { Hexagram.Conflict, HexagramNature.WanderingSoul },
        { Hexagram.PreponderanceOfTheGreat, HexagramNature.WanderingSoul },
        { Hexagram.TheCornersOfTheMouth, HexagramNature.WanderingSoul },
        { Hexagram.DarkeningOfTheLight, HexagramNature.WanderingSoul },
        { Hexagram.InnerTruth, HexagramNature.WanderingSoul },
        { Hexagram.Waiting, HexagramNature.WanderingSoul },

        // 归魂卦 (各宫第8卦，8个)
        { Hexagram.PossessionInGreatMeasure, HexagramNature.ReturningSoul },
        { Hexagram.TheMarryingMaiden, HexagramNature.ReturningSoul },
        { Hexagram.FellowshipWithMen, HexagramNature.ReturningSoul },
        { Hexagram.Following, HexagramNature.ReturningSoul },
        { Hexagram.WorkOnTheDecayed, HexagramNature.ReturningSoul },
        { Hexagram.TheArmy, HexagramNature.ReturningSoul },
        { Hexagram.Development, HexagramNature.ReturningSoul },
        { Hexagram.HoldingTogether, HexagramNature.ReturningSoul }
    };

    /// <summary>
    /// 获取当前卦所属的卦性（如六冲、六合、游魂、归魂）
    /// </summary>
    /// <param name="hexagram">六十四卦实例</param>
    /// <returns>该卦所对应的特殊的属性状态。若无特殊属性，则返回 null</returns>
    public static HexagramNature? GetNature(this Hexagram hexagram) => NatureMap.GetValueOrDefault(hexagram);
}