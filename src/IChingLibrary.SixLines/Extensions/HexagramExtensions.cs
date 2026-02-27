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

    /// <summary>
    /// 将卦转换为互卦
    /// <para>由卦中间四爻交互组成的新卦，揭示事物内在的、隐藏的联系与过程。</para>
    /// </summary>
    /// <param name="hexagram">原卦</param>
    /// <returns>互卦</returns>
    public static Hexagram ToNuclear(this Hexagram hexagram)
    {
        var value = hexagram.Value;
        // 互卦的下卦取原卦的二、三、四爻
        var lowerValue = (value >> 1) & 0b111;
        // 互卦的上卦取原卦的三、四、五爻
        var upperValue = (value >> 2) & 0b111;
        return Hexagram.FromValue((byte)(upperValue << 3 | lowerValue));
    }

    /// <summary>
    /// 将卦转换为错卦
    /// <para>将卦的每一爻阴阳互变得到的新卦，代表事物的对立面、阴阳颠倒的状态。</para>
    /// </summary>
    /// <param name="hexagram">原卦</param>
    /// <returns>错卦</returns>
    public static Hexagram ToOpposite(this Hexagram hexagram)
    {
        // 每一爻阴阳互变即对二进制位取反（仅保留低6位）
        return Hexagram.FromValue((byte)(~hexagram.Value & 0b111111));
    }

    /// <summary>
    /// 将卦转换为综卦
    /// <para>卦整体颠倒（旋转180度）得到的新卦，代表事物的反面、不同视角或过程的反转。</para>
    /// </summary>
    /// <param name="hexagram">原卦</param>
    /// <returns>综卦</returns>
    public static Hexagram ToInverted(this Hexagram hexagram)
    {
        var value = hexagram.Value;
        int invertedValue = 0;
        // 1-6爻颠倒，即第1位(bit 0)与第6位(bit 5)互换，以此类推
        for (int i = 0; i < 6; i++)
        {
            if (((value >> i) & 1) == 1)
            {
                invertedValue |= (1 << (5 - i));
            }
        }
        return Hexagram.FromValue((byte)invertedValue);
    }
}