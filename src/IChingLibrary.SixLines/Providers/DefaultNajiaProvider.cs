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
        { Trigram.Qian, [EarthlyBranch.Zi, EarthlyBranch.Yin, EarthlyBranch.Chen, EarthlyBranch.Wu, EarthlyBranch.Shen, EarthlyBranch.Xu] },
        // 震：子寅辰午申戌
        { Trigram.Zhen, [EarthlyBranch.Zi, EarthlyBranch.Yin, EarthlyBranch.Chen, EarthlyBranch.Wu, EarthlyBranch.Shen, EarthlyBranch.Xu] },
        // 坎：寅辰午申戌子
        { Trigram.Kan, [EarthlyBranch.Yin, EarthlyBranch.Chen, EarthlyBranch.Wu, EarthlyBranch.Shen, EarthlyBranch.Xu, EarthlyBranch.Zi] },
        // 艮：辰午申戌子寅
        { Trigram.Gen, [EarthlyBranch.Chen, EarthlyBranch.Wu, EarthlyBranch.Shen, EarthlyBranch.Xu, EarthlyBranch.Zi, EarthlyBranch.Yin] }
    };

    // 阴卦纳支表（逆行）：坤、兑、离、巽
    private static readonly Dictionary<Trigram, EarthlyBranch[]> YinBranchMap = new()
    {
        // 坤：未巳卯丑亥酉
        { Trigram.Kun, [EarthlyBranch.Wei, EarthlyBranch.Si, EarthlyBranch.Mao, EarthlyBranch.Chou, EarthlyBranch.Hai, EarthlyBranch.You] },
        // 兑：巳卯丑亥酉未
        { Trigram.Dui, [EarthlyBranch.Si, EarthlyBranch.Mao, EarthlyBranch.Chou, EarthlyBranch.Hai, EarthlyBranch.You, EarthlyBranch.Wei] },
        // 离：卯丑亥酉未巳
        { Trigram.Li, [EarthlyBranch.Mao, EarthlyBranch.Chou, EarthlyBranch.Hai, EarthlyBranch.You, EarthlyBranch.Wei, EarthlyBranch.Si] },
        // 巽：丑亥酉未巳卯
        { Trigram.Xun, [EarthlyBranch.Chou, EarthlyBranch.Hai, EarthlyBranch.You, EarthlyBranch.Wei, EarthlyBranch.Si, EarthlyBranch.Mao] }
    };

    // 预计算的纳甲表，避免运行时重复计算和分配内存
    // 8个卦，2个位置（内/外），每个位置3个爻
    private static readonly StemBranch[][] InnerTables = new StemBranch[8][];
    private static readonly StemBranch[][] OuterTables = new StemBranch[8][];

    static DefaultNajiaProvider()
    {
        foreach (var trigram in Trigram.GetAll())
        {
            InnerTables[trigram.Value] = PrecalculateTable(trigram, true);
            OuterTables[trigram.Value] = PrecalculateTable(trigram, false);
        }
    }

    private static StemBranch[] PrecalculateTable(Trigram trigram, bool isInner)
    {
        var stem = isInner ? NajiaStemMap[trigram].Inner : NajiaStemMap[trigram].Outer;
        var branches = IsYangTrigram(trigram) ? YangBranchMap[trigram] : YinBranchMap[trigram];

        var result = new StemBranch[3];
        var offset = isInner ? 0 : 3;
        for (int i = 0; i < 3; i++)
        {
            result[i] = new StemBranch(stem, branches[offset + i]);
        }
        return result;
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

    /// <inheritdoc />
    public void BindStemBranches(BuilderContext context)
    {
        if (context.Original is null)
            throw new InvalidOperationException("未找到主卦");
        
        Bind(context.Original);
        
        if (context.Changed is not null)
            Bind(context.Changed);

        return;

        void Bind(HexagramInstance hexagram)
        {
            // 直接从预计算表中获取内卦和外卦的干支数组
            var lowerTable = InnerTables[hexagram.Meta.Lower.Value];
            var upperTable = OuterTables[hexagram.Meta.Upper.Value];

            for (var i = 0; i < 3; i++)
            {
                hexagram.Lines[i].StemBranch = lowerTable[i];
                hexagram.Lines[i + 3].StemBranch = upperTable[i];
            }
        }
    }
}