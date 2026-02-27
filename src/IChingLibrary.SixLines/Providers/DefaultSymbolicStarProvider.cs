using IChingLibrary.SixLines.Providers.Abstractions;

namespace IChingLibrary.SixLines.Providers;

/// <summary>
/// 神煞计算委托
/// </summary>
/// <param name="inquiryTime">问时信息</param>
/// <param name="hexagram">主卦实例</param>
/// <returns>神煞对应的地支数组，返回 null 表示该神煞不适用于当前情况</returns>
public delegate EarthlyBranch[]? SymbolicStarCalculatorDelegate(
    InquiryTime inquiryTime,
    HexagramInstance hexagram);

/// <summary>
/// 默认神煞提供器
/// 管理神煞选择和神煞计算器
/// </summary>
public sealed class DefaultSymbolicStarProvider : ISymbolicStarProvider
{
    /// <summary>
    /// 神煞计算器注册表，存储神煞类型与其计算委托的映射关系
    /// </summary>
    private readonly Dictionary<SymbolicStar, SymbolicStarCalculatorDelegate> _calculators = new();

    #region 静态映射表

    /// <summary>
    /// 贵人映射表：甲戊→牛羊，乙己→鼠猴，丙丁→猪鸡，壬癸→兔蛇，庚辛→马虎
    /// </summary>
    private static readonly Dictionary<HeavenlyStem, EarthlyBranch[]> NoblemanMap = new()
    {
        { HeavenlyStem.Jia, [EarthlyBranch.Chou, EarthlyBranch.Wei] },
        { HeavenlyStem.Wu, [EarthlyBranch.Chou, EarthlyBranch.Wei] },
        { HeavenlyStem.Yi, [EarthlyBranch.Zi, EarthlyBranch.Shen] },
        { HeavenlyStem.Ji, [EarthlyBranch.Zi, EarthlyBranch.Shen] },
        { HeavenlyStem.Bing, [EarthlyBranch.Hai, EarthlyBranch.You] },
        { HeavenlyStem.Ding, [EarthlyBranch.Hai, EarthlyBranch.You] },
        { HeavenlyStem.Ren, [EarthlyBranch.Mao, EarthlyBranch.Si] },
        { HeavenlyStem.Gui, [EarthlyBranch.Mao, EarthlyBranch.Si] },
        { HeavenlyStem.Geng, [EarthlyBranch.Wu, EarthlyBranch.Yin] },
        { HeavenlyStem.Xin, [EarthlyBranch.Wu, EarthlyBranch.Yin] },
    };

    /// <summary>
    /// 禄神映射表：甲→寅，乙→卯，丙戊→巳，丁己→午，庚→申，辛→酉，壬→亥，癸→子
    /// </summary>
    private static readonly Dictionary<HeavenlyStem, EarthlyBranch[]> SalarySpiritMap = new()
    {
        { HeavenlyStem.Jia, [EarthlyBranch.Yin] },
        { HeavenlyStem.Yi, [EarthlyBranch.Mao] },
        { HeavenlyStem.Bing, [EarthlyBranch.Si] },
        { HeavenlyStem.Wu, [EarthlyBranch.Si] },
        { HeavenlyStem.Ding, [EarthlyBranch.Wu] },
        { HeavenlyStem.Ji, [EarthlyBranch.Wu] },
        { HeavenlyStem.Geng, [EarthlyBranch.Shen] },
        { HeavenlyStem.Xin, [EarthlyBranch.You] },
        { HeavenlyStem.Ren, [EarthlyBranch.Hai] },
        { HeavenlyStem.Gui, [EarthlyBranch.Zi] },
    };

    /// <summary>
    /// 文昌映射表：甲→巳，乙→午，丙戊→申，丁己→酉，庚→亥，辛→子，壬→寅，癸→卯
    /// </summary>
    private static readonly Dictionary<HeavenlyStem, EarthlyBranch[]> CultureFlourishMap = new()
    {
        { HeavenlyStem.Jia, [EarthlyBranch.Si] },
        { HeavenlyStem.Yi, [EarthlyBranch.Wu] },
        { HeavenlyStem.Bing, [EarthlyBranch.Shen] },
        { HeavenlyStem.Wu, [EarthlyBranch.Shen] },
        { HeavenlyStem.Ding, [EarthlyBranch.You] },
        { HeavenlyStem.Ji, [EarthlyBranch.You] },
        { HeavenlyStem.Geng, [EarthlyBranch.Hai] },
        { HeavenlyStem.Xin, [EarthlyBranch.Zi] },
        { HeavenlyStem.Ren, [EarthlyBranch.Yin] },
        { HeavenlyStem.Gui, [EarthlyBranch.Mao] },
    };

    /// <summary>
    /// 羊刃映射表：甲→卯，乙→寅，丙戊→午，丁己→巳，庚→酉，辛→申，壬→子，癸→亥
    /// </summary>
    private static readonly Dictionary<HeavenlyStem, EarthlyBranch[]> YangBladeMap = new()
    {
        { HeavenlyStem.Jia, [EarthlyBranch.Mao] },
        { HeavenlyStem.Yi, [EarthlyBranch.Yin] },
        { HeavenlyStem.Bing, [EarthlyBranch.Wu] },
        { HeavenlyStem.Wu, [EarthlyBranch.Wu] },
        { HeavenlyStem.Ding, [EarthlyBranch.Si] },
        { HeavenlyStem.Ji, [EarthlyBranch.Si] },
        { HeavenlyStem.Geng, [EarthlyBranch.You] },
        { HeavenlyStem.Xin, [EarthlyBranch.Shen] },
        { HeavenlyStem.Ren, [EarthlyBranch.Zi] },
        { HeavenlyStem.Gui, [EarthlyBranch.Hai] },
    };

    /// <summary>
    /// 驿马映射表：寅午戌→申，亥卯未→巳，巳酉丑→亥，申子辰→寅
    /// </summary>
    private static readonly Dictionary<EarthlyBranch, EarthlyBranch[]> PostHorseMap = new()
    {
        { EarthlyBranch.Yin, [EarthlyBranch.Shen] },
        { EarthlyBranch.Wu, [EarthlyBranch.Shen] },
        { EarthlyBranch.Xu, [EarthlyBranch.Shen] },
        { EarthlyBranch.Hai, [EarthlyBranch.Si] },
        { EarthlyBranch.Mao, [EarthlyBranch.Si] },
        { EarthlyBranch.Wei, [EarthlyBranch.Si] },
        { EarthlyBranch.Si, [EarthlyBranch.Hai] },
        { EarthlyBranch.You, [EarthlyBranch.Hai] },
        { EarthlyBranch.Chou, [EarthlyBranch.Hai] },
        { EarthlyBranch.Shen, [EarthlyBranch.Yin] },
        { EarthlyBranch.Zi, [EarthlyBranch.Yin] },
        { EarthlyBranch.Chen, [EarthlyBranch.Yin] },
    };

    /// <summary>
    /// 桃花映射表：寅午戌→卯，亥卯未→辰，巳酉丑→午，申子辰→酉
    /// </summary>
    private static readonly Dictionary<EarthlyBranch, EarthlyBranch[]> PeachBlossomMap = new()
    {
        { EarthlyBranch.Yin, [EarthlyBranch.Mao] },
        { EarthlyBranch.Wu, [EarthlyBranch.Mao] },
        { EarthlyBranch.Xu, [EarthlyBranch.Mao] },
        { EarthlyBranch.Hai, [EarthlyBranch.Chen] },
        { EarthlyBranch.Mao, [EarthlyBranch.Chen] },
        { EarthlyBranch.Wei, [EarthlyBranch.Chen] },
        { EarthlyBranch.Si, [EarthlyBranch.Wu] },
        { EarthlyBranch.You, [EarthlyBranch.Wu] },
        { EarthlyBranch.Chou, [EarthlyBranch.Wu] },
        { EarthlyBranch.Shen, [EarthlyBranch.You] },
        { EarthlyBranch.Zi, [EarthlyBranch.You] },
        { EarthlyBranch.Chen, [EarthlyBranch.You] },
    };

    /// <summary>
    /// 将星映射表：寅午戌→午，亥卯未→卯，巳酉丑→酉，申子辰→子
    /// </summary>
    private static readonly Dictionary<EarthlyBranch, EarthlyBranch[]> GeneralsStarMap = new()
    {
        { EarthlyBranch.Yin, [EarthlyBranch.Wu] },
        { EarthlyBranch.Wu, [EarthlyBranch.Wu] },
        { EarthlyBranch.Xu, [EarthlyBranch.Wu] },
        { EarthlyBranch.Hai, [EarthlyBranch.Mao] },
        { EarthlyBranch.Mao, [EarthlyBranch.Mao] },
        { EarthlyBranch.Wei, [EarthlyBranch.Mao] },
        { EarthlyBranch.Si, [EarthlyBranch.You] },
        { EarthlyBranch.You, [EarthlyBranch.You] },
        { EarthlyBranch.Chou, [EarthlyBranch.You] },
        { EarthlyBranch.Shen, [EarthlyBranch.Zi] },
        { EarthlyBranch.Zi, [EarthlyBranch.Zi] },
        { EarthlyBranch.Chen, [EarthlyBranch.Zi] },
    };

    /// <summary>
    /// 华盖映射表：寅午戌→戌，亥卯未→未，巳酉丑→丑，申子辰→辰
    /// </summary>
    private static readonly Dictionary<EarthlyBranch, EarthlyBranch[]> CanopyMap = new()
    {
        { EarthlyBranch.Yin, [EarthlyBranch.Xu] },
        { EarthlyBranch.Wu, [EarthlyBranch.Xu] },
        { EarthlyBranch.Xu, [EarthlyBranch.Xu] },
        { EarthlyBranch.Hai, [EarthlyBranch.Wei] },
        { EarthlyBranch.Mao, [EarthlyBranch.Wei] },
        { EarthlyBranch.Wei, [EarthlyBranch.Wei] },
        { EarthlyBranch.Si, [EarthlyBranch.Chou] },
        { EarthlyBranch.You, [EarthlyBranch.Chou] },
        { EarthlyBranch.Chou, [EarthlyBranch.Chou] },
        { EarthlyBranch.Shen, [EarthlyBranch.Chen] },
        { EarthlyBranch.Zi, [EarthlyBranch.Chen] },
        { EarthlyBranch.Chen, [EarthlyBranch.Chen] },
    };

    /// <summary>
    /// 谋星映射表：寅午戌→辰，亥卯未→丑，巳酉丑→未，申子辰→戌
    /// </summary>
    private static readonly Dictionary<EarthlyBranch, EarthlyBranch[]> StarOfStrategyMap = new()
    {
        { EarthlyBranch.Yin, [EarthlyBranch.Chen] },
        { EarthlyBranch.Wu, [EarthlyBranch.Chen] },
        { EarthlyBranch.Xu, [EarthlyBranch.Chen] },
        { EarthlyBranch.Hai, [EarthlyBranch.Chou] },
        { EarthlyBranch.Mao, [EarthlyBranch.Chou] },
        { EarthlyBranch.Wei, [EarthlyBranch.Chou] },
        { EarthlyBranch.Si, [EarthlyBranch.Wei] },
        { EarthlyBranch.You, [EarthlyBranch.Wei] },
        { EarthlyBranch.Chou, [EarthlyBranch.Wei] },
        { EarthlyBranch.Shen, [EarthlyBranch.Xu] },
        { EarthlyBranch.Zi, [EarthlyBranch.Xu] },
        { EarthlyBranch.Chen, [EarthlyBranch.Xu] },
    };

    /// <summary>
    /// 灾煞映射表：寅午戌→子，亥卯未→酉，巳酉丑→卯，申子辰→午
    /// </summary>
    private static readonly Dictionary<EarthlyBranch, EarthlyBranch[]> DisasterMalignityMap = new()
    {
        { EarthlyBranch.Yin, [EarthlyBranch.Zi] },
        { EarthlyBranch.Wu, [EarthlyBranch.Zi] },
        { EarthlyBranch.Xu, [EarthlyBranch.Zi] },
        { EarthlyBranch.Hai, [EarthlyBranch.You] },
        { EarthlyBranch.Mao, [EarthlyBranch.You] },
        { EarthlyBranch.Wei, [EarthlyBranch.You] },
        { EarthlyBranch.Si, [EarthlyBranch.Mao] },
        { EarthlyBranch.You, [EarthlyBranch.Mao] },
        { EarthlyBranch.Chou, [EarthlyBranch.Mao] },
        { EarthlyBranch.Shen, [EarthlyBranch.Wu] },
        { EarthlyBranch.Zi, [EarthlyBranch.Wu] },
        { EarthlyBranch.Chen, [EarthlyBranch.Wu] },
    };

    /// <summary>
    /// 劫煞映射表：寅午戌→亥，亥卯未→申，巳酉丑→寅，申子辰→巳
    /// </summary>
    private static readonly Dictionary<EarthlyBranch, EarthlyBranch[]> RobberyMalignityMap = new()
    {
        { EarthlyBranch.Yin, [EarthlyBranch.Hai] },
        { EarthlyBranch.Wu, [EarthlyBranch.Hai] },
        { EarthlyBranch.Xu, [EarthlyBranch.Hai] },
        { EarthlyBranch.Hai, [EarthlyBranch.Shen] },
        { EarthlyBranch.Mao, [EarthlyBranch.Shen] },
        { EarthlyBranch.Wei, [EarthlyBranch.Shen] },
        { EarthlyBranch.Si, [EarthlyBranch.Yin] },
        { EarthlyBranch.You, [EarthlyBranch.Yin] },
        { EarthlyBranch.Chou, [EarthlyBranch.Yin] },
        { EarthlyBranch.Shen, [EarthlyBranch.Si] },
        { EarthlyBranch.Zi, [EarthlyBranch.Si] },
        { EarthlyBranch.Chen, [EarthlyBranch.Si] },
    };

    /// <summary>
    /// 亡神映射表：寅午戌→巳，亥卯未→寅，巳酉丑→申，申子辰→亥
    /// </summary>
    private static readonly Dictionary<EarthlyBranch, EarthlyBranch[]> DeathSpiritMap = new()
    {
        { EarthlyBranch.Yin, [EarthlyBranch.Si] },
        { EarthlyBranch.Wu, [EarthlyBranch.Si] },
        { EarthlyBranch.Xu, [EarthlyBranch.Si] },
        { EarthlyBranch.Hai, [EarthlyBranch.Yin] },
        { EarthlyBranch.Mao, [EarthlyBranch.Yin] },
        { EarthlyBranch.Wei, [EarthlyBranch.Yin] },
        { EarthlyBranch.Si, [EarthlyBranch.Shen] },
        { EarthlyBranch.You, [EarthlyBranch.Shen] },
        { EarthlyBranch.Chou, [EarthlyBranch.Shen] },
        { EarthlyBranch.Shen, [EarthlyBranch.Hai] },
        { EarthlyBranch.Zi, [EarthlyBranch.Hai] },
        { EarthlyBranch.Chen, [EarthlyBranch.Hai] },
    };

    /// <summary>
    /// 天医映射表：月支退一位
    /// </summary>
    private static readonly Dictionary<EarthlyBranch, EarthlyBranch[]> CelestialPhysicianMap = new()
    {
        { EarthlyBranch.Yin, [EarthlyBranch.Chou] },
        { EarthlyBranch.Mao, [EarthlyBranch.Yin] },
        { EarthlyBranch.Chen, [EarthlyBranch.Mao] },
        { EarthlyBranch.Si, [EarthlyBranch.Chen] },
        { EarthlyBranch.Wu, [EarthlyBranch.Si] },
        { EarthlyBranch.Wei, [EarthlyBranch.Wu] },
        { EarthlyBranch.Shen, [EarthlyBranch.Wei] },
        { EarthlyBranch.You, [EarthlyBranch.Shen] },
        { EarthlyBranch.Xu, [EarthlyBranch.You] },
        { EarthlyBranch.Hai, [EarthlyBranch.Xu] },
        { EarthlyBranch.Zi, [EarthlyBranch.Hai] },
        { EarthlyBranch.Chou, [EarthlyBranch.Zi] },
    };

    /// <summary>
    /// 天喜映射表：寅卯辰→戌，巳午未→丑，申酉戌→辰，亥子丑→未
    /// </summary>
    private static readonly Dictionary<EarthlyBranch, EarthlyBranch[]> HeavenlyJoyMap = new()
    {
        { EarthlyBranch.Yin, [EarthlyBranch.Xu] },
        { EarthlyBranch.Mao, [EarthlyBranch.Xu] },
        { EarthlyBranch.Chen, [EarthlyBranch.Xu] },
        { EarthlyBranch.Si, [EarthlyBranch.Chou] },
        { EarthlyBranch.Wu, [EarthlyBranch.Chou] },
        { EarthlyBranch.Wei, [EarthlyBranch.Chou] },
        { EarthlyBranch.Shen, [EarthlyBranch.Chen] },
        { EarthlyBranch.You, [EarthlyBranch.Chen] },
        { EarthlyBranch.Xu, [EarthlyBranch.Chen] },
        { EarthlyBranch.Hai, [EarthlyBranch.Wei] },
        { EarthlyBranch.Zi, [EarthlyBranch.Wei] },
        { EarthlyBranch.Chou, [EarthlyBranch.Wei] },
    };

    /// <summary>
    /// 床帐映射表：火→辰戌丑未，金→寅卯，水→巳午，木→申酉，土→亥子
    /// </summary>
    private static readonly Dictionary<FivePhase, EarthlyBranch[]> MarriageBedMap = new()
    {
        { FivePhase.Fire, [EarthlyBranch.Chen, EarthlyBranch.Xu, EarthlyBranch.Chou, EarthlyBranch.Wei] },
        { FivePhase.Metal, [EarthlyBranch.Yin, EarthlyBranch.Mao] },
        { FivePhase.Water, [EarthlyBranch.Si, EarthlyBranch.Wu] },
        { FivePhase.Wood, [EarthlyBranch.Shen, EarthlyBranch.You] },
        { FivePhase.Earth, [EarthlyBranch.Hai, EarthlyBranch.Zi] },
    };

    /// <summary>
    /// 香闺映射表：火→申酉，金→寅卯，水→巳午，木→辰戌丑未，土→亥子
    /// </summary>
    private static readonly Dictionary<FivePhase, EarthlyBranch[]> BridalChamberMap = new()
    {
        { FivePhase.Fire, [EarthlyBranch.Shen, EarthlyBranch.You] },
        { FivePhase.Metal, [EarthlyBranch.Yin, EarthlyBranch.Mao] },
        { FivePhase.Water, [EarthlyBranch.Si, EarthlyBranch.Wu] },
        { FivePhase.Wood, [EarthlyBranch.Chen, EarthlyBranch.Xu, EarthlyBranch.Chou, EarthlyBranch.Wei] },
        { FivePhase.Earth, [EarthlyBranch.Hai, EarthlyBranch.Zi] },
    };

    #endregion

    /// <summary>
    /// 初始化默认神煞提供器，自动注册所有默认神煞计算器
    /// </summary>
    public DefaultSymbolicStarProvider()
    {
        RegisterDefaultCalculators();
    }

    /// <summary>
    /// 添加或覆盖神煞计算器
    /// </summary>
    /// <param name="symbolicStar">神煞类型</param>
    /// <param name="calculator">神煞计算委托</param>
    /// <remarks>
    /// 如果神煞已存在，则不会覆盖原有计算器
    /// </remarks>
    public void Add(SymbolicStar symbolicStar, SymbolicStarCalculatorDelegate calculator)
    {
        _calculators.TryAdd(symbolicStar, calculator);
    }

    /// <summary>
    /// 移除指定神煞的计算器
    /// </summary>
    /// <param name="symbolicStar">要移除的神煞类型</param>
    /// <returns>如果成功移除返回 true，否则返回 false</returns>
    public bool Remove(SymbolicStar symbolicStar)
    {
        return _calculators.Remove(symbolicStar);
    }

    /// <summary>
    /// 获取所有已注册的神煞计算器
    /// </summary>
    /// <returns>神煞类型到计算委托的只读字典</returns>
    public IReadOnlyDictionary<SymbolicStar, SymbolicStarCalculatorDelegate> Calculators => _calculators;

    /// <inheritdoc/>
    public SymbolicStarCollection Calculate(BuilderContext context)
    {
        if (context.InquiryTime is null)
            throw new InvalidOperationException("未找到起卦时间");
        
        if (context.Original is null)
            throw new InvalidOperationException("未找到主卦");
        
        var stars = new Dictionary<SymbolicStar, EarthlyBranch[]>();
        foreach (var (symbolicStar, calculator) in _calculators)
        {
            var branches = calculator(context.InquiryTime.Value, context.Original);
            if (branches != null)
            {
                stars[symbolicStar] = branches;
            }
        }
        return new SymbolicStarCollection(stars);
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
        Add(SymbolicStar.Nobleman, CalculateNobleman);
        Add(SymbolicStar.SalarySpirit, CalculateSalarySpirit);
        Add(SymbolicStar.CultureFlourish, CalculateCultureFlourish);
        Add(SymbolicStar.YangBlade, CalculateYangBlade);

        // 基于日支的神煞
        Add(SymbolicStar.PostHorse, CalculatePostHorse);
        Add(SymbolicStar.PeachBlossom, CalculatePeachBlossom);
        Add(SymbolicStar.GeneralsStar, CalculateGeneralsStar);
        Add(SymbolicStar.Canopy, CalculateCanopy);
        Add(SymbolicStar.StarOfStrategy, CalculateStarOfStrategy);
        Add(SymbolicStar.DisasterMalignity, CalculateDisasterMalignity);
        Add(SymbolicStar.RobberyMalignity, CalculateRobberyMalignity);
        Add(SymbolicStar.DeathSpirit, CalculateDeathSpirit);

        // 基于月支的神煞
        Add(SymbolicStar.CelestialPhysician, CalculateCelestialPhysician);
        Add(SymbolicStar.HeavenlyJoy, CalculateHeavenlyJoy);

        // 基于卦身的神煞
        Add(SymbolicStar.MarriageBed, CalculateMarriageBed);
        Add(SymbolicStar.BridalChamber, CalculateBridalChamber);
    }

    #region 默认神煞计算方法

    /// <summary>
    /// 根据日干查询映射表的神煞计算器
    /// </summary>
    private static EarthlyBranch[]? CalculateByDayStem(
        InquiryTime inquiryTime,
        HexagramInstance hexagram,
        Dictionary<HeavenlyStem, EarthlyBranch[]> map)
        => map.GetValueOrDefault(inquiryTime.StemBranch.Day.Stem);

    /// <summary>
    /// 根据日支查询映射表的神煞计算器
    /// </summary>
    private static EarthlyBranch[]? CalculateByDayBranch(
        InquiryTime inquiryTime,
        HexagramInstance hexagram,
        Dictionary<EarthlyBranch, EarthlyBranch[]> map)
        => map.GetValueOrDefault(inquiryTime.StemBranch.Day.Branch);

    /// <summary>
    /// 根据月支查询映射表的神煞计算器
    /// </summary>
    private static EarthlyBranch[]? CalculateByMonthBranch(
        InquiryTime inquiryTime,
        HexagramInstance hexagram,
        Dictionary<EarthlyBranch, EarthlyBranch[]> map)
        => map.GetValueOrDefault(inquiryTime.StemBranch.Month.Branch);

    /// <summary>
    /// 根据卦身五行查询映射表的神煞计算器
    /// </summary>
    private static EarthlyBranch[]? CalculateByHexagramBody(
        InquiryTime inquiryTime,
        HexagramInstance hexagram,
        Dictionary<FivePhase, EarthlyBranch[]> map)
    {
        var hexagramBody = hexagram.FindHexagramBody();
        return hexagramBody == null ? null : map.GetValueOrDefault(hexagramBody.FivePhase);
    }

    /// <summary>
    /// 计算贵人（甲戊→牛羊，乙己→鼠猴，丙丁→猪鸡，壬癸→兔蛇，庚辛→马虎）
    /// </summary>
    private static EarthlyBranch[]? CalculateNobleman(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByDayStem(inquiryTime, hexagram, NoblemanMap);

    /// <summary>
    /// 计算禄神（甲→寅，乙→卯，丙戊→巳，丁己→午，庚→申，辛→酉，壬→亥，癸→子）
    /// </summary>
    private static EarthlyBranch[]? CalculateSalarySpirit(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByDayStem(inquiryTime, hexagram, SalarySpiritMap);

    /// <summary>
    /// 计算文昌（甲→巳，乙→午，丙戊→申，丁己→酉，庚→亥，辛→子，壬→寅，癸→卯）
    /// </summary>
    private static EarthlyBranch[]? CalculateCultureFlourish(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByDayStem(inquiryTime, hexagram, CultureFlourishMap);

    /// <summary>
    /// 计算羊刃（甲→卯，乙→寅，丙戊→午，丁己→巳，庚→酉，辛→申，壬→子，癸→亥）
    /// </summary>
    private static EarthlyBranch[]? CalculateYangBlade(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByDayStem(inquiryTime, hexagram, YangBladeMap);

    /// <summary>
    /// 计算驿马（寅午戌→申，亥卯未→巳，巳酉丑→亥，申子辰→寅）
    /// </summary>
    private static EarthlyBranch[]? CalculatePostHorse(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByDayBranch(inquiryTime, hexagram, PostHorseMap);

    /// <summary>
    /// 计算桃花（寅午戌→卯，亥卯未→辰，巳酉丑→午，申子辰→酉）
    /// </summary>
    private static EarthlyBranch[]? CalculatePeachBlossom(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByDayBranch(inquiryTime, hexagram, PeachBlossomMap);

    /// <summary>
    /// 计算将星（寅午戌→午，亥卯未→卯，巳酉丑→酉，申子辰→子）
    /// </summary>
    private static EarthlyBranch[]? CalculateGeneralsStar(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByDayBranch(inquiryTime, hexagram, GeneralsStarMap);

    /// <summary>
    /// 计算华盖（寅午戌→戌，亥卯未→未，巳酉丑→丑，申子辰→辰）
    /// </summary>
    private static EarthlyBranch[]? CalculateCanopy(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByDayBranch(inquiryTime, hexagram, CanopyMap);

    /// <summary>
    /// 计算谋星（寅午戌→辰，亥卯未→丑，巳酉丑→未，申子辰→戌）
    /// </summary>
    private static EarthlyBranch[]? CalculateStarOfStrategy(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByDayBranch(inquiryTime, hexagram, StarOfStrategyMap);

    /// <summary>
    /// 计算灾煞（寅午戌→子，亥卯未→酉，巳酉丑→卯，申子辰→午）
    /// </summary>
    private static EarthlyBranch[]? CalculateDisasterMalignity(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByDayBranch(inquiryTime, hexagram, DisasterMalignityMap);

    /// <summary>
    /// 计算劫煞（寅午戌→亥，亥卯未→申，巳酉丑→寅，申子辰→巳）
    /// </summary>
    private static EarthlyBranch[]? CalculateRobberyMalignity(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByDayBranch(inquiryTime, hexagram, RobberyMalignityMap);

    /// <summary>
    /// 计算亡神（寅午戌→巳，亥卯未→寅，巳酉丑→申，申子辰→亥）
    /// </summary>
    private static EarthlyBranch[]? CalculateDeathSpirit(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByDayBranch(inquiryTime, hexagram, DeathSpiritMap);

    /// <summary>
    /// 计算天医（月支退一位）
    /// </summary>
    private static EarthlyBranch[]? CalculateCelestialPhysician(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByMonthBranch(inquiryTime, hexagram, CelestialPhysicianMap);

    /// <summary>
    /// 计算天喜（寅卯辰→戌，巳午未→丑，申酉戌→辰，亥子丑→未）
    /// </summary>
    private static EarthlyBranch[]? CalculateHeavenlyJoy(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByMonthBranch(inquiryTime, hexagram, HeavenlyJoyMap);

    /// <summary>
    /// 计算床帐（火→辰戌丑未，金→寅卯，水→巳午，木→申酉，土→亥子）
    /// </summary>
    private static EarthlyBranch[]? CalculateMarriageBed(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByHexagramBody(inquiryTime, hexagram, MarriageBedMap);

    /// <summary>
    /// 计算香闺（火→寅卯，金→辰戌丑未，水→申酉，木→亥子，土→巳午）
    /// </summary>
    private static EarthlyBranch[]? CalculateBridalChamber(InquiryTime inquiryTime, HexagramInstance hexagram)
        => CalculateByHexagramBody(inquiryTime, hexagram, BridalChamberMap);

    #endregion
}
