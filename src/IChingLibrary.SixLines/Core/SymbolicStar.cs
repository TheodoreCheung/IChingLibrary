using IChingLibrary.Core.Annotations;

namespace IChingLibrary.Core;

/// <summary>
/// 神煞类型（Symbolic Stars），用于标识六爻占卜中的各种神煞
/// </summary>
[IChingElementEnum]
public partial class SymbolicStar : IChingElement<SymbolicStar>
{
    private SymbolicStar(byte value, string label) : base(value, label)
    {
    }

    #region 基于日干的神煞

    /// <summary>
    /// 贵人（甲戊→牛羊，乙己→鼠猴，丙丁→猪鸡，壬癸→兔蛇，庚辛→马虎）
    /// </summary>
    public static readonly SymbolicStar Nobleman = new(1, nameof(Nobleman));

    /// <summary>
    /// 禄神（甲→寅，乙→卯，丙戊→巳，丁己→午，庚→申，辛→酉，壬→亥，癸→子）
    /// </summary>
    public static readonly SymbolicStar SalarySpirit = new(2, nameof(SalarySpirit));

    /// <summary>
    /// 文昌（甲→巳，乙→午，丙戊→申，丁己→酉，庚→亥，辛→子，壬→寅，癸→卯）
    /// </summary>
    public static readonly SymbolicStar CultureFlourish = new(3, nameof(CultureFlourish));

    /// <summary>
    /// 羊刃（甲→卯，乙→寅，丙戊→午，丁己→巳，庚→酉，辛→申，壬→子，癸→亥）
    /// </summary>
    public static readonly SymbolicStar YangBlade = new(6, nameof(YangBlade));

    #endregion

    #region 基于三合局的神煞

    /// <summary>
    /// 驿马（寅午戌→申，亥卯未→巳，巳酉丑→亥，申子辰→寅）
    /// </summary>
    public static readonly SymbolicStar PostHorse = new(4, nameof(PostHorse));

    /// <summary>
    /// 桃花（寅午戌→卯，亥卯未→辰，巳酉丑→午，申子辰→酉）
    /// </summary>
    public static readonly SymbolicStar PeachBlossom = new(5, nameof(PeachBlossom));

    /// <summary>
    /// 将星（寅午戌→午，亥卯未→卯，巳酉丑→酉，申子辰→子）
    /// </summary>
    public static readonly SymbolicStar GeneralsStar = new(9, nameof(GeneralsStar));

    /// <summary>
    /// 华盖（寅午戌→戌，亥卯未→未，巳酉丑→丑，申子辰→辰）
    /// </summary>
    public static readonly SymbolicStar Canopy = new(10, nameof(Canopy));

    /// <summary>
    /// 谋星（寅午戌→辰，亥卯未→丑，巳酉丑→未，申子辰→戌）
    /// </summary>
    public static readonly SymbolicStar StarOfStrategy = new(11, nameof(StarOfStrategy));

    /// <summary>
    /// 灾煞（寅午戌→子，亥卯未→酉，巳酉丑→卯，申子辰→午）
    /// </summary>
    public static readonly SymbolicStar DisasterMalignity = new(8, nameof(DisasterMalignity));

    /// <summary>
    /// 劫煞（寅午戌→亥，亥卯未→申，巳酉丑→寅，申子辰→巳）
    /// </summary>
    public static readonly SymbolicStar RobberyMalignity = new(7, nameof(RobberyMalignity));

    /// <summary>
    /// 亡神（寅午戌→巳，亥卯未→寅，巳酉丑→申，申子辰→亥）
    /// </summary>
    public static readonly SymbolicStar DeathSpirit = new(12, nameof(DeathSpirit));

    #endregion

    #region 其他神煞

    /// <summary>
    /// 天医（月支退一位）
    /// </summary>
    public static readonly SymbolicStar CelestialPhysician = new(13, nameof(CelestialPhysician));

    /// <summary>
    /// 天喜（寅卯辰→戌，巳午未→丑，申酉戌→辰，亥子丑→未）
    /// </summary>
    public static readonly SymbolicStar HeavenlyJoy = new(14, nameof(HeavenlyJoy));

    /// <summary>
    /// 床帐（火→辰戌丑未，金→寅卯，水→巳午，木→申酉，土→亥子）
    /// </summary>
    public static readonly SymbolicStar MarriageBed = new(15, nameof(MarriageBed));

    /// <summary>
    /// 香闺（火→寅卯，金→辰戌丑未，水→申酉，木→亥子，土→巳午）
    /// </summary>
    public static readonly SymbolicStar BridalChamber = new(16, nameof(BridalChamber));

    #endregion

    #region 自定义神煞

    /// <summary>
    /// 自定义神煞的起始值
    /// </summary>
    private const byte CustomStartValue = 17;

    /// <summary>
    /// 下一个自定义神煞的值
    /// </summary>
    private static byte _nextCustomValue = CustomStartValue;

    /// <summary>
    /// 线程安全锁
    /// </summary>
    private static readonly Lock Lock = new();

    /// <summary>
    /// 创建自定义神煞类型
    /// </summary>
    /// <param name="name">神煞名称</param>
    /// <returns>自定义神煞实例</returns>
    public static SymbolicStar CreateCustom(string name)
    {
        using (Lock.EnterScope())
        {
            var value = _nextCustomValue++;
            return new SymbolicStar(value, name);
        }
    }

    #endregion
}
