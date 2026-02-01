namespace IChingLibrary.Core;

/// <summary>
/// 六爻（六十四卦），由上下两个三爻组成
/// </summary>
[IChingElementEnum]
public partial class Hexagram : IChingElement<Hexagram>
{
    /// <summary>
    /// 所属宫位
    /// </summary>
    public Trigram Palace { get; }

    /// <summary>
    /// 上卦（外卦）
    /// </summary>
    public Trigram Upper { get; }

    /// <summary>
    /// 下卦（内卦）
    /// </summary>
    public Trigram Lower { get; }

    /// <summary>
    /// 初始化六爻
    /// </summary>
    /// <param name="upper">上卦</param>
    /// <param name="lower">下卦</param>
    /// <param name="label">标签名称</param>
    /// <param name="palace">所属宫位</param>
    private Hexagram(Trigram upper, Trigram lower, string label, Trigram palace)
        : base((byte)((byte)upper << 3 | (byte)lower), label)
    {
        Upper = upper;
        Lower = lower;
        Palace = palace;
    }

    #region 乾宫

    /// <summary>
    /// 乾为天
    /// </summary>
    public static readonly Hexagram TheCreative =
        new(Trigram.Qian, Trigram.Qian, nameof(TheCreative), Trigram.Qian);

    /// <summary>
    /// 天风姤
    /// </summary>
    public static readonly Hexagram ComingToMeet =
        new(Trigram.Qian, Trigram.Xun, nameof(ComingToMeet), Trigram.Qian);

    /// <summary>
    /// 天山遁
    /// </summary>
    public static readonly Hexagram Retreat =
        new(Trigram.Qian, Trigram.Gen, nameof(Retreat), Trigram.Qian);

    /// <summary>
    /// 天地否
    /// </summary>
    public static readonly Hexagram Standstill =
        new(Trigram.Qian, Trigram.Kun, nameof(Standstill), Trigram.Qian);

    /// <summary>
    /// 风地观
    /// </summary>
    public static readonly Hexagram Contemplation =
        new(Trigram.Xun, Trigram.Kun, nameof(Contemplation), Trigram.Qian);

    /// <summary>
    /// 山地剥
    /// </summary>
    public static readonly Hexagram SplittingApart =
        new(Trigram.Gen, Trigram.Kun, nameof(SplittingApart), Trigram.Qian);

    /// <summary>
    /// 火地晋
    /// </summary>
    public static readonly Hexagram Progress =
        new(Trigram.Li, Trigram.Kun, nameof(Progress), Trigram.Qian);

    /// <summary>
    /// 火天大有
    /// </summary>
    public static readonly Hexagram PossessionInGreatMeasure =
        new(Trigram.Li, Trigram.Qian, nameof(PossessionInGreatMeasure), Trigram.Qian);

    #endregion

    #region 兑宫

    /// <summary>
    /// 兑为泽
    /// </summary>
    public static readonly Hexagram TheJoyous =
        new(Trigram.Dui, Trigram.Dui, nameof(TheJoyous), Trigram.Dui);

    /// <summary>
    /// 泽水困
    /// </summary>
    public static readonly Hexagram Oppression =
        new(Trigram.Dui, Trigram.Kan, nameof(Oppression), Trigram.Dui);

    /// <summary>
    /// 泽地萃
    /// </summary>
    public static readonly Hexagram GatheringTogether =
        new(Trigram.Dui, Trigram.Kun, nameof(GatheringTogether), Trigram.Dui);

    /// <summary>
    /// 泽山咸
    /// </summary>
    public static readonly Hexagram Influence =
        new(Trigram.Dui, Trigram.Gen, nameof(Influence), Trigram.Dui);

    /// <summary>
    /// 水山蹇
    /// </summary>
    public static readonly Hexagram Obstruction =
        new(Trigram.Kan, Trigram.Gen, nameof(Obstruction), Trigram.Dui);

    /// <summary>
    /// 地山谦
    /// </summary>
    public static readonly Hexagram Modesty =
        new(Trigram.Kun, Trigram.Gen, nameof(Modesty), Trigram.Dui);

    /// <summary>
    /// 雷山小过
    /// </summary>
    public static readonly Hexagram PreponderanceOfTheSmall =
        new(Trigram.Zhen, Trigram.Gen, nameof(PreponderanceOfTheSmall), Trigram.Dui);

    /// <summary>
    /// 雷泽归妹
    /// </summary>
    public static readonly Hexagram TheMarryingMaiden =
        new(Trigram.Zhen, Trigram.Dui, nameof(TheMarryingMaiden), Trigram.Dui);

    #endregion

    #region 离宫

    /// <summary>
    /// 离为火
    /// </summary>
    public static readonly Hexagram TheClinging =
        new(Trigram.Li, Trigram.Li, nameof(TheClinging), Trigram.Li);

    /// <summary>
    /// 火山旅
    /// </summary>
    public static readonly Hexagram TheWanderer =
        new(Trigram.Li, Trigram.Gen, nameof(TheWanderer), Trigram.Li);

    /// <summary>
    /// 火风鼎
    /// </summary>
    public static readonly Hexagram TheCauldron =
        new(Trigram.Li, Trigram.Xun, nameof(TheCauldron), Trigram.Li);

    /// <summary>
    /// 火水未济
    /// </summary>
    public static readonly Hexagram BeforeCompletion =
        new(Trigram.Li, Trigram.Kan, nameof(BeforeCompletion), Trigram.Li);

    /// <summary>
    /// 山水蒙
    /// </summary>
    public static readonly Hexagram YouthfulFolly =
        new(Trigram.Gen, Trigram.Kan, nameof(YouthfulFolly), Trigram.Li);

    /// <summary>
    /// 风水涣
    /// </summary>
    public static readonly Hexagram Dispersion =
        new(Trigram.Xun, Trigram.Kan, nameof(Dispersion), Trigram.Li);

    /// <summary>
    /// 天水讼
    /// </summary>
    public static readonly Hexagram Conflict =
        new(Trigram.Qian, Trigram.Kan, nameof(Conflict), Trigram.Li);

    /// <summary>
    /// 天火同人
    /// </summary>
    public static readonly Hexagram FellowshipWithMen =
        new(Trigram.Qian, Trigram.Li, nameof(FellowshipWithMen), Trigram.Li);

    #endregion

    #region 震宫

    /// <summary>
    /// 震为雷
    /// </summary>
    public static readonly Hexagram TheArousing =
        new(Trigram.Zhen, Trigram.Zhen, nameof(TheArousing), Trigram.Zhen);

    /// <summary>
    /// 雷地豫
    /// </summary>
    public static readonly Hexagram Enthusiasm =
        new(Trigram.Zhen, Trigram.Kun, nameof(Enthusiasm), Trigram.Zhen);

    /// <summary>
    /// 雷水解
    /// </summary>
    public static readonly Hexagram Deliverance =
        new(Trigram.Zhen, Trigram.Kan, nameof(Deliverance), Trigram.Zhen);

    /// <summary>
    /// 雷风恒
    /// </summary>
    public static readonly Hexagram Duration =
        new(Trigram.Zhen, Trigram.Xun, nameof(Duration), Trigram.Zhen);

    /// <summary>
    /// 地风升
    /// </summary>
    public static readonly Hexagram PushingUpward =
        new(Trigram.Kun, Trigram.Xun, nameof(PushingUpward), Trigram.Zhen);

    /// <summary>
    /// 水风井
    /// </summary>
    public static readonly Hexagram TheWell =
        new(Trigram.Kan, Trigram.Xun, nameof(TheWell), Trigram.Zhen);

    /// <summary>
    /// 泽风大过
    /// </summary>
    public static readonly Hexagram PreponderanceOfTheGreat =
        new(Trigram.Dui, Trigram.Xun, nameof(PreponderanceOfTheGreat), Trigram.Zhen);

    /// <summary>
    /// 泽雷随
    /// </summary>
    public static readonly Hexagram Following =
        new(Trigram.Dui, Trigram.Zhen, nameof(Following), Trigram.Zhen);

    #endregion

    #region 巽宫

    /// <summary>
    /// 巽为风
    /// </summary>
    public static readonly Hexagram TheGentle =
        new(Trigram.Xun, Trigram.Xun, nameof(TheGentle), Trigram.Xun);

    /// <summary>
    /// 风天小畜
    /// </summary>
    public static readonly Hexagram TheTamingPowerOfTheSmall =
        new(Trigram.Xun, Trigram.Qian, nameof(TheTamingPowerOfTheSmall), Trigram.Xun);

    /// <summary>
    /// 风火家人
    /// </summary>
    public static readonly Hexagram TheFamily =
        new(Trigram.Xun, Trigram.Li, nameof(TheFamily), Trigram.Xun);

    /// <summary>
    /// 风雷益
    /// </summary>
    public static readonly Hexagram Increase =
        new(Trigram.Xun, Trigram.Zhen, nameof(Increase), Trigram.Xun);

    /// <summary>
    /// 天雷无妄
    /// </summary>
    public static readonly Hexagram Innocence =
        new(Trigram.Qian, Trigram.Zhen, nameof(Innocence), Trigram.Xun);

    /// <summary>
    /// 火雷噬嗑
    /// </summary>
    public static readonly Hexagram BitingThrough =
        new(Trigram.Li, Trigram.Zhen, nameof(BitingThrough), Trigram.Xun);

    /// <summary>
    /// 山雷颐
    /// </summary>
    public static readonly Hexagram TheCornersOfTheMouth =
        new(Trigram.Gen, Trigram.Zhen, nameof(TheCornersOfTheMouth), Trigram.Xun);

    /// <summary>
    /// 山风蛊
    /// </summary>
    public static readonly Hexagram WorkOnTheDecayed =
        new(Trigram.Gen, Trigram.Xun, nameof(WorkOnTheDecayed), Trigram.Xun);

    #endregion

    #region 坎宫

    /// <summary>
    /// 坎为水
    /// </summary>
    public static readonly Hexagram TheAbysmal =
        new(Trigram.Kan, Trigram.Kan, nameof(TheAbysmal), Trigram.Kan);

    /// <summary>
    /// 水泽节
    /// </summary>
    public static readonly Hexagram Limitation =
        new(Trigram.Kan, Trigram.Dui, nameof(Limitation), Trigram.Kan);

    /// <summary>
    /// 水雷屯
    /// </summary>
    public static readonly Hexagram DifficultyAtTheBeginning =
        new(Trigram.Kan, Trigram.Zhen, nameof(DifficultyAtTheBeginning), Trigram.Kan);

    /// <summary>
    /// 水火既济
    /// </summary>
    public static readonly Hexagram AfterCompletion =
        new(Trigram.Kan, Trigram.Li, nameof(AfterCompletion), Trigram.Kan);

    /// <summary>
    /// 泽火革
    /// </summary>
    public static readonly Hexagram Revolution =
        new(Trigram.Dui, Trigram.Li, nameof(Revolution), Trigram.Kan);

    /// <summary>
    /// 雷火丰
    /// </summary>
    public static readonly Hexagram Abundance =
        new(Trigram.Zhen, Trigram.Li, nameof(Abundance), Trigram.Kan);

    /// <summary>
    /// 地火明夷
    /// </summary>
    public static readonly Hexagram DarkeningOfTheLight =
        new(Trigram.Kun, Trigram.Li, nameof(DarkeningOfTheLight), Trigram.Kan);

    /// <summary>
    /// 地水师
    /// </summary>
    public static readonly Hexagram TheArmy =
        new(Trigram.Kun, Trigram.Kan, nameof(TheArmy), Trigram.Kan);

    #endregion

    #region 艮宫

    /// <summary>
    /// 艮为山
    /// </summary>
    public static readonly Hexagram KeepingStill =
        new(Trigram.Gen, Trigram.Gen, nameof(KeepingStill), Trigram.Gen);

    /// <summary>
    /// 山火贲
    /// </summary>
    public static readonly Hexagram Grace =
        new(Trigram.Gen, Trigram.Li, nameof(Grace), Trigram.Gen);

    /// <summary>
    /// 山天大畜
    /// </summary>
    public static readonly Hexagram TheTamingPowerOfTheGreat =
        new(Trigram.Gen, Trigram.Qian, nameof(TheTamingPowerOfTheGreat), Trigram.Gen);

    /// <summary>
    /// 山泽损
    /// </summary>
    public static readonly Hexagram Decrease =
        new(Trigram.Gen, Trigram.Dui, nameof(Decrease), Trigram.Gen);

    /// <summary>
    /// 火泽睽
    /// </summary>
    public static readonly Hexagram Opposition =
        new(Trigram.Li, Trigram.Dui, nameof(Opposition), Trigram.Gen);

    /// <summary>
    /// 天泽履
    /// </summary>
    public static readonly Hexagram Treading =
        new(Trigram.Qian, Trigram.Dui, nameof(Treading), Trigram.Gen);

    /// <summary>
    /// 风泽中孚
    /// </summary>
    public static readonly Hexagram InnerTruth =
        new(Trigram.Xun, Trigram.Dui, nameof(InnerTruth), Trigram.Gen);

    /// <summary>
    /// 风山渐
    /// </summary>
    public static readonly Hexagram Development =
        new(Trigram.Xun, Trigram.Gen, nameof(Development), Trigram.Gen);

    #endregion

    #region 坤宫

    /// <summary>
    /// 坤为地
    /// </summary>
    public static readonly Hexagram TheReceptive =
        new(Trigram.Kun, Trigram.Kun, nameof(TheReceptive), Trigram.Kun);

    /// <summary>
    /// 地雷复
    /// </summary>
    public static readonly Hexagram Return =
        new(Trigram.Kun, Trigram.Zhen, nameof(Return), Trigram.Kun);

    /// <summary>
    /// 地泽临
    /// </summary>
    public static readonly Hexagram Approach =
        new(Trigram.Kun, Trigram.Dui, nameof(Approach), Trigram.Kun);

    /// <summary>
    /// 地天泰
    /// </summary>
    public static readonly Hexagram Peace =
        new(Trigram.Kun, Trigram.Qian, nameof(Peace), Trigram.Kun);

    /// <summary>
    /// 雷天大壮
    /// </summary>
    public static readonly Hexagram ThePowerOfTheGreat =
        new(Trigram.Zhen, Trigram.Qian, nameof(ThePowerOfTheGreat), Trigram.Kun);

    /// <summary>
    /// 泽天夬
    /// </summary>
    public static readonly Hexagram BreakThrough =
        new(Trigram.Dui, Trigram.Qian, nameof(BreakThrough), Trigram.Kun);

    /// <summary>
    /// 水天需
    /// </summary>
    public static readonly Hexagram Waiting =
        new(Trigram.Kan, Trigram.Qian, nameof(Waiting), Trigram.Kun);

    /// <summary>
    /// 水地比
    /// </summary>
    public static readonly Hexagram HoldingTogether =
        new(Trigram.Kan, Trigram.Kun, nameof(HoldingTogether), Trigram.Kun);

    #endregion

    /// <summary>
    /// 根据上下卦创建六爻
    /// </summary>
    /// <param name="upper">上卦</param>
    /// <param name="lower">下卦</param>
    /// <returns>对应的六爻</returns>
    public static Hexagram Create(Trigram upper, Trigram lower) => FromValue((byte)((byte)upper << 3 | (byte)lower));
}