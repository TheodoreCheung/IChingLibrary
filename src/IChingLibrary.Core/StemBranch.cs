using IChingLibrary.Core.Abstractions;

namespace IChingLibrary.Core;

/// <summary>
/// 干支，由天干和地支组成
/// </summary>
public class StemBranch(HeavenlyStem stem, EarthlyBranch branch)
{
    /// <summary>
    /// 天干
    /// </summary>
    public HeavenlyStem Stem { get; } = stem;

    /// <summary>
    /// 地支
    /// </summary>
    public EarthlyBranch Branch { get; } = branch;

    /// <summary>
    /// 旬空（空亡地支）
    /// </summary>
    public EarthlyBranch[] EmptyBranches
    {
        get
        {
            // 计算旬头地支（甲所在的地支）
            // 旬头地支索引 = (地支索引 - (天干索引 - 1) + 11) % 12 + 1
            var stemIndex = Stem.Value - 1; // 0-9
            var branchIndex = Branch.Value; // 1-12
            var xunBranchIndex = (branchIndex - stemIndex + 11) % 12 + 1;

            // 空亡地支是旬头地支往后第10、11位
            var empty1Index = (xunBranchIndex + 9) % 12 + 1;
            var empty2Index = (xunBranchIndex + 10) % 12 + 1;

            return
            [
                EarthlyBranch.GetAll().First(b => b.Value == empty1Index),
                EarthlyBranch.GetAll().First(b => b.Value == empty2Index)
            ];
        }
    }

    /// <inheritdoc />
    public override string ToString() => $"{Stem}{Branch}";
}

/// <summary>
/// 天干，十天干
/// </summary>
[IChingElementEnum]
public partial class HeavenlyStem : IChingElement<HeavenlyStem>, IGenerative<HeavenlyStem>, IRestrictive<HeavenlyStem>,
    IRelationship<HeavenlyStem>
{
    /// <summary>
    /// 对应的阴阳属性
    /// </summary>
    public YinYang YinYang { get; }

    /// <summary>
    /// 对应的五行属性
    /// </summary>
    public FivePhase FivePhase { get; }

    /// <summary>
    /// 初始化天干
    /// </summary>
    /// <param name="value">唯一标识值</param>
    /// <param name="label">标签名称</param>
    /// <param name="yinYang">对应的阴阳属性</param>
    /// <param name="fivePhase">对应的五行属性</param>
    private HeavenlyStem(byte value, string label, YinYang yinYang, FivePhase fivePhase) : base(value, label)
    {
        YinYang = yinYang;
        FivePhase = fivePhase;
    }

    /// <summary>
    /// 甲，阳木
    /// </summary>
    public static readonly HeavenlyStem Jia = new(1, nameof(Jia), YinYang.Yang, FivePhase.Wood);

    /// <summary>
    /// 乙，阴木
    /// </summary>
    public static readonly HeavenlyStem Yi = new(2, nameof(Yi), YinYang.Yin, FivePhase.Wood);

    /// <summary>
    /// 丙，阳火
    /// </summary>
    public static readonly HeavenlyStem Bing = new(3, nameof(Bing), YinYang.Yang, FivePhase.Fire);

    /// <summary>
    /// 丁，阴火
    /// </summary>
    public static readonly HeavenlyStem Ding = new(4, nameof(Ding), YinYang.Yin, FivePhase.Fire);

    /// <summary>
    /// 戊，阳土
    /// </summary>
    public static readonly HeavenlyStem Wu = new(5, nameof(Wu), YinYang.Yang, FivePhase.Earth);

    /// <summary>
    /// 己，阴土
    /// </summary>
    public static readonly HeavenlyStem Ji = new(6, nameof(Ji), YinYang.Yin, FivePhase.Earth);

    /// <summary>
    /// 庚，阳金
    /// </summary>
    public static readonly HeavenlyStem Geng = new(7, nameof(Geng), YinYang.Yang, FivePhase.Metal);

    /// <summary>
    /// 辛，阴金
    /// </summary>
    public static readonly HeavenlyStem Xin = new(8, nameof(Xin), YinYang.Yin, FivePhase.Metal);

    /// <summary>
    /// 壬，阳水
    /// </summary>
    public static readonly HeavenlyStem Ren = new(9, nameof(Ren), YinYang.Yang, FivePhase.Water);

    /// <summary>
    /// 癸，阴水
    /// </summary>
    public static readonly HeavenlyStem Gui = new(10, nameof(Gui), YinYang.Yin, FivePhase.Water);

    /// <summary>
    /// 天干五合映射（甲己合、乙庚合、丙辛合、丁壬合、戊癸合）
    /// </summary>
    private static readonly Dictionary<HeavenlyStem, HeavenlyStem> CombinesMap = new()
    {
        { Jia, Ji }, { Ji, Jia }, { Yi, Geng }, { Geng, Yi },
        { Bing, Xin }, { Xin, Bing }, { Ding, Ren }, { Ren, Ding },
        { Wu, Gui }, { Gui, Wu }
    };

    /// <summary>
    /// 天干四冲映射（甲庚冲、乙辛冲、丙壬冲、丁癸冲）
    /// </summary>
    private static readonly Dictionary<HeavenlyStem, HeavenlyStem> ClashesMap = new()
    {
        { Jia, Geng }, { Geng, Jia }, { Yi, Xin }, { Xin, Yi },
        { Ren, Bing }, { Bing, Ren }, { Ding, Gui }, { Gui, Ding }
    };

    /// <inheritdoc />
    public bool IsGenerates(HeavenlyStem other) => FivePhase.IsGenerates(other.FivePhase);

    /// <inheritdoc />
    public bool Generates(HeavenlyStem other) => FivePhase.Generates(other.FivePhase);

    /// <inheritdoc />
    public bool GeneratesBy(HeavenlyStem other) => FivePhase.GeneratesBy(other.FivePhase);

    /// <inheritdoc />
    public bool IsRestrains(HeavenlyStem other) => FivePhase.IsRestrains(other.FivePhase);

    /// <inheritdoc />
    public bool Restrains(HeavenlyStem other) => FivePhase.Restrains(other.FivePhase);

    /// <inheritdoc />
    public bool RestrainsBy(HeavenlyStem other) => FivePhase.RestrainsBy(other.FivePhase);

    /// <inheritdoc />
    public bool IsClashing(HeavenlyStem other) => ClashesMap.TryGetValue(this, out var target) && target == other;

    /// <inheritdoc />
    public bool IsCombining(HeavenlyStem other) => CombinesMap.TryGetValue(this, out var partner) && partner == other;

    /// <inheritdoc />
    bool IRelationship<HeavenlyStem>.IsTriangularCombination(HeavenlyStem other, HeavenlyStem another)
    {
        throw new NotSupportedException();
    }
}

/// <summary>
/// 地支，十二地支
/// </summary>
[IChingElementEnum]
public partial class EarthlyBranch : IChingElement<EarthlyBranch>, IGenerative<EarthlyBranch>,
    IRestrictive<EarthlyBranch>, IRelationship<EarthlyBranch>
{
    /// <summary>
    /// 对应的阴阳属性
    /// </summary>
    public YinYang YinYang { get; }

    /// <summary>
    /// 对应的五行属性
    /// </summary>
    public FivePhase FivePhase { get; }

    /// <summary>
    /// 初始化地支
    /// </summary>
    /// <param name="value">唯一标识值</param>
    /// <param name="label">标签名称</param>
    /// <param name="yinYang">对应的阴阳属性</param>
    /// <param name="fivePhase">对应的五行属性</param>
    private EarthlyBranch(byte value, string label, YinYang yinYang, FivePhase fivePhase) : base(value, label)
    {
        YinYang = yinYang;
        FivePhase = fivePhase;
    }

    /// <summary>
    /// 寅，阳木
    /// </summary>
    public static readonly EarthlyBranch Yin = new(3, nameof(Yin), YinYang.Yang, FivePhase.Wood);

    /// <summary>
    /// 卯，阴木
    /// </summary>
    public static readonly EarthlyBranch Mao = new(4, nameof(Mao), YinYang.Yin, FivePhase.Wood);

    /// <summary>
    /// 辰，阳土
    /// </summary>
    public static readonly EarthlyBranch Chen = new(5, nameof(Chen), YinYang.Yang, FivePhase.Earth);

    /// <summary>
    /// 巳，阴火
    /// </summary>
    public static readonly EarthlyBranch Si = new(6, nameof(Si), YinYang.Yin, FivePhase.Fire);

    /// <summary>
    /// 午，阳火
    /// </summary>
    public static readonly EarthlyBranch Wu = new(7, nameof(Wu), YinYang.Yang, FivePhase.Fire);

    /// <summary>
    /// 未，阴土
    /// </summary>
    public static readonly EarthlyBranch Wei = new(8, nameof(Wei), YinYang.Yin, FivePhase.Earth);

    /// <summary>
    /// 申，阳金
    /// </summary>
    public static readonly EarthlyBranch Shen = new(9, nameof(Shen), YinYang.Yang, FivePhase.Metal);

    /// <summary>
    /// 酉，阴金
    /// </summary>
    public static readonly EarthlyBranch You = new(10, nameof(You), YinYang.Yin, FivePhase.Metal);

    /// <summary>
    /// 戌，阳土
    /// </summary>
    public static readonly EarthlyBranch Xu = new(11, nameof(Xu), YinYang.Yang, FivePhase.Earth);

    /// <summary>
    /// 亥，阴水
    /// </summary>
    public static readonly EarthlyBranch Hai = new(12, nameof(Hai), YinYang.Yin, FivePhase.Water);

    /// <summary>
    /// 子，阳水
    /// </summary>
    public static readonly EarthlyBranch Zi = new(1, nameof(Zi), YinYang.Yang, FivePhase.Water);

    /// <summary>
    /// 丑，阴土
    /// </summary>
    public static readonly EarthlyBranch Chou = new(2, nameof(Chou), YinYang.Yin, FivePhase.Earth);

    /// <summary>
    /// 地支六冲映射（子午冲、丑未冲、寅申冲、卯酉冲、辰戌冲、巳亥冲）
    /// </summary>
    private static readonly Dictionary<EarthlyBranch, EarthlyBranch> ClashesMap = new()
    {
        { Zi, Wu }, { Wu, Zi }, { Chou, Wei }, { Wei, Chou },
        { Yin, Shen }, { Shen, Yin }, { Mao, You }, { You, Mao },
        { Chen, Xu }, { Xu, Chen }, { Si, Hai }, { Hai, Si }
    };

    /// <summary>
    /// 地支六合映射（子丑合、寅亥合、卯戌合、辰酉合、巳申合、午未合）
    /// </summary>
    private static readonly Dictionary<EarthlyBranch, EarthlyBranch> CombinesMap = new()
    {
        { Zi, Chou }, { Chou, Zi }, { Yin, Hai }, { Hai, Yin },
        { Mao, Xu }, { Xu, Mao }, { Chen, You }, { You, Chen },
        { Si, Shen }, { Shen, Si }, { Wu, Wei }, { Wei, Wu }
    };

    /// <summary>
    /// 地支三合局（申子辰水局、亥卯未木局、寅午戌火局、巳酉丑金局）
    /// </summary>
    private static readonly EarthlyBranch[][] TriangularCombinationGroups =
    [
        [Shen, Zi, Chen], // 水局
        [Hai, Mao, Wei], // 木局
        [Yin, Wu, Xu], // 火局
        [Si, You, Chou] // 金局
    ];

    /// <inheritdoc />
    public bool IsGenerates(EarthlyBranch other) => FivePhase.IsGenerates(other.FivePhase);

    /// <inheritdoc />
    public bool Generates(EarthlyBranch other) => FivePhase.Generates(other.FivePhase);

    /// <inheritdoc />
    public bool GeneratesBy(EarthlyBranch other) => FivePhase.GeneratesBy(other.FivePhase);

    /// <inheritdoc />
    public bool IsRestrains(EarthlyBranch other) => FivePhase.IsRestrains(other.FivePhase);

    /// <inheritdoc />
    public bool Restrains(EarthlyBranch other) => FivePhase.Restrains(other.FivePhase);

    /// <inheritdoc />
    public bool RestrainsBy(EarthlyBranch other) => FivePhase.RestrainsBy(other.FivePhase);

    /// <inheritdoc />
    public bool IsClashing(EarthlyBranch other) => ClashesMap.TryGetValue(this, out var target) && target == other;

    /// <inheritdoc />
    public bool IsCombining(EarthlyBranch other) => CombinesMap.TryGetValue(this, out var partner) && partner == other;

    /// <inheritdoc />
    public bool IsTriangularCombination(EarthlyBranch other, EarthlyBranch another) =>
        TriangularCombinationGroups.Any(g => g.Contains(this) && g.Contains(other) && g.Contains(another));
}