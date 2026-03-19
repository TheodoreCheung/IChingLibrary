namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 神煞计算委托
/// </summary>
/// <param name="castingTime">起卦时间</param>
/// <param name="hexagram">主卦实例</param>
/// <returns>神煞对应的地支数组，返回 null 表示该神煞不适用于当前情况</returns>
internal delegate EarthlyBranch[]? SymbolicStarCalculatorDelegate(
    CastingTime castingTime,
    HexagramInstance hexagram);

/// <summary>
/// 神煞计算步骤
/// </summary>
public sealed class SymbolicStarStep : IStructuringStep
{
    /// <summary>
    /// 神煞计算器注册表，存储神煞类型与其计算委托的映射关系
    /// </summary>
    private readonly Dictionary<SymbolicStar, SymbolicStarCalculatorDelegate> _calculators = new();

    /// <summary>
    /// 计算三合局的长生位地支
    /// </summary>
    /// <param name="dayBranch">日支</param>
    /// <param name="offset">偏移量</param>
    /// <returns>返回对应的三合局长生位的地支</returns>
    /// <example>
    /// 驿马 = CalculateTrinityCombinationBranch(dayBranch, 6); // 对冲位
    /// 桃花 = CalculateTrinityCombinationBranch(dayBranch, 1); // 沐浴位
    /// 亡神 = CalculateTrinityCombinationBranch(dayBranch, 3); // 临官位
    /// 劫煞：+9
    /// 将星：+4
    /// 华盖：+8
    /// 谋星：+2
    /// 灾煞：+10
    /// </example>
    private static EarthlyBranch CalculateTrinityCombinationBranch(EarthlyBranch dayBranch, int offset)
    {
        // 根据日支的值模4的结果来确定三合局类型
        var changSheng = (dayBranch.Value % 4) switch
        {
            1 => EarthlyBranch.Shen.Value, // 申子辰 (水局)，长生在申
            2 => EarthlyBranch.Si.Value, // 巳酉丑 (金局)，长生在巳
            3 => EarthlyBranch.Yin.Value, // 寅午戌 (火局)，长生在寅
            0 => EarthlyBranch.Hai.Value, // 亥卯未 (木局)，长生在亥
            _ => throw new ArgumentException(null, nameof(dayBranch))
        };

        // 根据计算得到的值返回对应的地支
        return EarthlyBranch.FromValue((byte)((changSheng + offset - 1) % 12 + 1));
    }

    /// <summary>
    /// 计算依赖天干系列神煞（禄神、羊刃、文昌）地支
    /// </summary>
    /// <param name="dayStem">日干</param>
    /// <param name="table">映射表</param>
    /// <returns>返回天干系列神煞地支</returns>
    private static EarthlyBranch CalculateStemStar(HeavenlyStem dayStem, int[] table)
        => EarthlyBranch.FromValue((byte)table[dayStem.Value - 1]);

    #region 静态映射表

    /// <summary>
    /// 贵人映射表：甲戊→牛羊，乙己→鼠猴，丙丁→猪鸡，壬癸→兔蛇，庚辛→马虎
    /// </summary>
    /// <remarks>
    /// 索引 0-9 对应 甲(1)-癸(10)
    /// 每两个元素代表一对贵人地支 (1-indexed)
    /// </remarks>
    private static readonly byte[] NoblemanTable =
    [
        2, 8,  // 甲(1) -> 丑未
        1, 9,  // 乙(2) -> 子申
        12, 10,// 丙(3) -> 亥酉
        12, 10,// 丁(4) -> 亥酉
        2, 8,  // 戊(5) -> 丑未
        1, 9,  // 己(6) -> 子申
        7, 3,  // 庚(7) -> 午寅
        7, 3,  // 辛(8) -> 午寅
        4, 6,  // 壬(9) -> 卯巳
        4, 6   // 癸(10)-> 卯巳
    ];

    /// <summary>
    /// 禄神映射表：甲→寅，乙→卯，丙戊→巳，丁己→午，庚→申，辛→酉，壬→亥，癸→子
    /// </summary>
    private static readonly int[] SalarySpiritTable = [3, 4, 6, 7, 6, 7, 9, 10, 12, 1];

    /// <summary>
    /// 文昌映射表：甲→巳，乙→午，丙戊→申，丁己→酉，庚→亥，辛→子，壬→寅，癸→卯
    /// </summary>
    private static readonly int[] CultureFlourishTable = [6, 7, 9, 10, 9, 10, 12, 1, 3, 4];

    /// <summary>
    /// 羊刃映射表：甲→卯，乙→寅，丙戊→午，丁己→巳，庚→酉，辛→申，壬→子，癸→亥
    /// </summary>
    /// <remarks>给阳干(甲丙戊庚壬)定禄前一位，阴干定禄后一位</remarks>
    private static readonly int[] YangBladeTable = [4, 3, 7, 6, 7, 6, 10, 9, 1, 12];

    #endregion

    /// <inheritdoc />
    public IEnumerable<Type> RequiredSteps { get; } = [];

    /// <summary>
    /// 初始化神煞计算步骤并注册默认计算器
    /// </summary>
    public SymbolicStarStep()
    {
        RegisterDefaultCalculators();
    }

    /// <summary>
    /// 注册所有默认神煞计算器
    /// </summary>
    /// <remarks>
    /// 包含基于日干、日支、月支和卦身的神煞
    /// </remarks>
    private void RegisterDefaultCalculators()
    {
        // 基于日干的神煞
        Add(SymbolicStar.Nobleman, (ct, _) => {
            var startIndex = (ct.StemBranch.Day.Stem.Value - 1) * 2;
            return
            [
                EarthlyBranch.FromValue(NoblemanTable[startIndex]),
                EarthlyBranch.FromValue(NoblemanTable[startIndex + 1])
            ];
        });
        Add(SymbolicStar.SalarySpirit, (ct, _) => [CalculateStemStar(ct.StemBranch.Day.Stem, SalarySpiritTable)]);
        Add(SymbolicStar.CultureFlourish, (ct, _) => [CalculateStemStar(ct.StemBranch.Day.Stem, CultureFlourishTable)]);
        Add(SymbolicStar.YangBlade, (ct, _) => [CalculateStemStar(ct.StemBranch.Day.Stem, YangBladeTable)]);

        // 基于日支的神煞
        Add(SymbolicStar.PostHorse, (ct, _) => [CalculateTrinityCombinationBranch(ct.StemBranch.Day.Branch, 6)]);
        Add(SymbolicStar.PeachBlossom, (ct, _) => [CalculateTrinityCombinationBranch(ct.StemBranch.Day.Branch, 1)]);
        Add(SymbolicStar.GeneralsStar, (ct, _) => [CalculateTrinityCombinationBranch(ct.StemBranch.Day.Branch, 4)]);
        Add(SymbolicStar.Canopy, (ct, _) => [CalculateTrinityCombinationBranch(ct.StemBranch.Day.Branch, 8)]);
        Add(SymbolicStar.StarOfStrategy, (ct, _) => [CalculateTrinityCombinationBranch(ct.StemBranch.Day.Branch, 2)]);
        Add(SymbolicStar.DisasterMalignity, (ct, _) => [CalculateTrinityCombinationBranch(ct.StemBranch.Day.Branch, 10)]);
        Add(SymbolicStar.RobberyMalignity, (ct, _) => [CalculateTrinityCombinationBranch(ct.StemBranch.Day.Branch, 9)]);
        Add(SymbolicStar.DeathSpirit, (ct, _) => [CalculateTrinityCombinationBranch(ct.StemBranch.Day.Branch, 3)]);

        // 基于月支的神煞
        Add(SymbolicStar.CelestialPhysician,
            (ct, _) => [EarthlyBranch.FromValue((byte)((ct.StemBranch.Month.Branch.Value - 1 + 11) % 12 + 1))]);
        // 1. 将地支对齐到以 寅(3) 为起始的正月逻辑
        // 寅(3)->0, 卯(4)->1, 辰(5)->2, ..., 丑(2)->11
        // 2. 确定所属季度 (0:春, 1:夏, 2:秋, 3:冬)
        // 3. 计算天喜地支 (以 1-indexed 返回)
        // 春(0)->11, 夏(1)->2, 秋(2)->5, 冬(3)->8
        // 公式：(11 + season * 3 - 1) % 12 + 1
        Add(SymbolicStar.HeavenlyJoy, (ct, _) => [EarthlyBranch.FromValue((byte)(((ct.StemBranch.Month.Branch.Value + 9) % 12 / 3 * 3 + 10) % 12 + 1))]);

        // 基于卦身的神煞
        Add(SymbolicStar.MarriageBed, (_, hi) => 
            hi.FindHexagramBody()?.FivePhase.Value switch
            {
                1 => [EarthlyBranch.Hai, EarthlyBranch.Zi],
                2 => [EarthlyBranch.Yin, EarthlyBranch.Mao],
                3 => [EarthlyBranch.Si, EarthlyBranch.Wu],
                4 => [EarthlyBranch.Chen, EarthlyBranch.Xu, EarthlyBranch.Chou, EarthlyBranch.Wei],
                5 => [EarthlyBranch.Shen, EarthlyBranch.You],
                _ => []
            });
        Add(SymbolicStar.BridalChamber, (_, hi) => 
            hi.FindHexagramBody()?.FivePhase.Value switch
            {
                1 => [EarthlyBranch.Yin, EarthlyBranch.Mao],
                2 => [EarthlyBranch.Si, EarthlyBranch.Wu],
                3 => [EarthlyBranch.Chen, EarthlyBranch.Xu, EarthlyBranch.Chou, EarthlyBranch.Wei],
                4 => [EarthlyBranch.Shen, EarthlyBranch.You],
                5 => [EarthlyBranch.Hai, EarthlyBranch.Zi],
                _ => []
            });
    }

    /// <summary>
    /// 添加神煞计算器（不覆盖已存在的计算器）
    /// </summary>
    /// <param name="symbolicStar">神煞类型</param>
    /// <param name="calculator">神煞计算委托</param>
    /// <remarks>
    /// 如果神煞已存在，则不会覆盖原有计算器
    /// </remarks>
    private void Add(SymbolicStar symbolicStar, SymbolicStarCalculatorDelegate calculator)
    {
        _calculators.TryAdd(symbolicStar, calculator);
    }

    /// <inheritdoc />
    public void Execute(DivinationContext context)
    {
        var stars = new Dictionary<SymbolicStar, EarthlyBranch[]>();
        foreach (var (symbolicStar, calculator) in _calculators)
        {
            var branches = calculator(context.SixLineDivination.CastingTime, context.SixLineDivination.Original);
            if (branches != null)
            {
                stars[symbolicStar] = branches;
            }
        }

        context.SixLineDivination.SymbolicStars = new SymbolicStarCollection(stars);
    }
}
