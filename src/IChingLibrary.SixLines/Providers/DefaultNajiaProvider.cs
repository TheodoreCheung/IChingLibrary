using IChingLibrary.SixLines.Providers.Abstractions;

namespace IChingLibrary.SixLines.Providers;

/// <summary>
/// 默认纳甲法提供器
/// </summary>
internal class DefaultNajiaProvider : INajiaProvider
{
    /// <summary>
    /// 八卦纳干表（内卦/外卦对应的天干）
    /// </summary>
    private static readonly Dictionary<Trigram, (HeavenlyStem Inner, HeavenlyStem Outer)> NajiaStemMap = new()
    {
        { Trigram.Qian, (HeavenlyStem.Jia, HeavenlyStem.Ren) },   // 乾：内甲外壬
        { Trigram.Kun, (HeavenlyStem.Yi, HeavenlyStem.Gui) },     // 坤：内乙外癸
        { Trigram.Zhen, (HeavenlyStem.Geng, HeavenlyStem.Geng) }, // 震：内外庚
        { Trigram.Xun, (HeavenlyStem.Xin, HeavenlyStem.Xin) },    // 巽：内外辛
        { Trigram.Kan, (HeavenlyStem.Wu, HeavenlyStem.Wu) },      // 坎：内外戊
        { Trigram.Li, (HeavenlyStem.Ji, HeavenlyStem.Ji) },       // 离：内外己
        { Trigram.Gen, (HeavenlyStem.Bing, HeavenlyStem.Bing) },  // 艮：内外丙
        { Trigram.Dui, (HeavenlyStem.Ding, HeavenlyStem.Ding) }   // 兑：内外丁
    };

    // 阳卦纳支表（顺行）：乾、震、坎、艮
    private static readonly Dictionary<Trigram, EarthlyBranch[]> YangBranchMap = new()
    {
        // 乾：子寅辰午申戌
        { Trigram.Qian, new[] { EarthlyBranch.Zi, EarthlyBranch.Yin, EarthlyBranch.Chen, EarthlyBranch.Wu, EarthlyBranch.Shen, EarthlyBranch.Xu } },
        // 震：子寅辰午申戌
        { Trigram.Zhen, new[] { EarthlyBranch.Zi, EarthlyBranch.Yin, EarthlyBranch.Chen, EarthlyBranch.Wu, EarthlyBranch.Shen, EarthlyBranch.Xu } },
        // 坎：寅辰午申戌子
        { Trigram.Kan, new[] { EarthlyBranch.Yin, EarthlyBranch.Chen, EarthlyBranch.Wu, EarthlyBranch.Shen, EarthlyBranch.Xu, EarthlyBranch.Zi } },
        // 艮：辰午申戌子寅
        { Trigram.Gen, new[] { EarthlyBranch.Chen, EarthlyBranch.Wu, EarthlyBranch.Shen, EarthlyBranch.Xu, EarthlyBranch.Zi, EarthlyBranch.Yin } }
    };

    // 阴卦纳支表（逆行）：坤、兑、离、巽
    private static readonly Dictionary<Trigram, EarthlyBranch[]> YinBranchMap = new()
    {
        // 坤：未巳卯丑亥酉
        { Trigram.Kun, new[] { EarthlyBranch.Wei, EarthlyBranch.Si, EarthlyBranch.Mao, EarthlyBranch.Chou, EarthlyBranch.Hai, EarthlyBranch.You } },
        // 兑：巳卯丑亥酉未
        { Trigram.Dui, new[] { EarthlyBranch.Si, EarthlyBranch.Mao, EarthlyBranch.Chou, EarthlyBranch.Hai, EarthlyBranch.You, EarthlyBranch.Wei } },
        // 离：卯丑亥酉未巳
        { Trigram.Li, new[] { EarthlyBranch.Mao, EarthlyBranch.Chou, EarthlyBranch.Hai, EarthlyBranch.You, EarthlyBranch.Wei, EarthlyBranch.Si } },
        // 巽：丑亥酉未巳卯
        { Trigram.Xun, new[] { EarthlyBranch.Chou, EarthlyBranch.Hai, EarthlyBranch.You, EarthlyBranch.Wei, EarthlyBranch.Si, EarthlyBranch.Mao } }
    };

    /// <inheritdoc />
    public void BindStemBranches(HexagramInstance hexagram, InquiryTime inquiryTime)
    {
        var lowerTable = GetNajiaTable(hexagram.Meta.Lower, true);
        var upperTable = GetNajiaTable(hexagram.Meta.Upper, false);

        // 内卦（初爻到三爻）使用下卦纳甲
        for (int i = 0; i < 3; i++)
        {
            hexagram.Lines[i].StemBranch = lowerTable[i];
        }

        // 外卦（四爻到上爻）使用上卦纳甲
        for (int i = 3; i < 6; i++)
        {
            hexagram.Lines[i].StemBranch = upperTable[i];
        }
    }

    /// <inheritdoc />
    public StemBranch[] GetNajiaTable(Trigram trigram, bool isInner)
    {
        var stem = isInner ? NajiaStemMap[trigram].Inner : NajiaStemMap[trigram].Outer;
        var branches = IsYangTrigram(trigram) ? YangBranchMap[trigram] : YinBranchMap[trigram];

        // 根据内外卦选择对应的地支范围
        var startIndex = isInner ? 0 : 3;
        var branchesForTrigram = isInner
            ? branches.Take(3).ToArray()
            : branches.Skip(3).Take(3).ToArray();

        // 组合天干和地支
        return branchesForTrigram.Select(branch => new StemBranch(stem, branch)).ToArray();
    }

    /// <summary>
    /// 判断是否为阳卦（乾、震、坎、艮为阳卦）
    /// </summary>
    private static bool IsYangTrigram(Trigram trigram)
    {
        return trigram == Trigram.Qian ||
               trigram == Trigram.Zhen ||
               trigram == Trigram.Kan ||
               trigram == Trigram.Gen;
    }
}