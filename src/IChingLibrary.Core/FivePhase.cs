using IChingLibrary.Core.Abstractions;

namespace IChingLibrary.Core;

/// <summary>
/// 五行，易学中构成世界的五种基本元素
/// </summary>
[IChingElementEnum]
public partial class FivePhase : IChingElement<FivePhase>, IGenerative<FivePhase>, IRestrictive<FivePhase>
{
    /// <summary>
    /// 初始化五行
    /// </summary>
    /// <param name="value">唯一标识值</param>
    /// <param name="label">标签名称</param>
    private FivePhase(byte value, string label) : base(value, label)
    {
    }

    /// <summary>
    /// 金
    /// </summary>
    public static readonly FivePhase Metal = new(1, nameof(Metal));

    /// <summary>
    /// 水
    /// </summary>
    public static readonly FivePhase Water = new(2, nameof(Water));

    /// <summary>
    /// 木
    /// </summary>
    public static readonly FivePhase Wood = new(3, nameof(Wood));

    /// <summary>
    /// 火
    /// </summary>
    public static readonly FivePhase Fire = new(4, nameof(Fire));

    /// <summary>
    /// 土
    /// </summary>
    public static readonly FivePhase Earth = new(5, nameof(Earth));

    /// <summary>
    /// 相生关系映射（五行相生：木生火、火生土、土生金、金生水、水生木）
    /// </summary>
    private static readonly Dictionary<FivePhase, FivePhase> GeneratesMap = new()
    {
        { Wood, Fire }, { Fire, Earth }, { Earth, Metal }, { Metal, Water }, { Water, Wood }
    };

    /// <summary>
    /// 被生关系映射（五行被生：木被水生、火被木生等）
    /// </summary>
    private static readonly Dictionary<FivePhase, FivePhase> GeneratesByMap = new()
    {
        { Fire, Wood }, { Earth, Fire }, { Metal, Earth }, { Water, Metal }, { Wood, Water }
    };

    /// <summary>
    /// 相克关系映射（五行相克：木克土、土克水、水克火、火克金、金克木）
    /// </summary>
    private static readonly Dictionary<FivePhase, FivePhase> RestrainsMap = new()
    {
        { Wood, Earth }, { Earth, Water }, { Water, Fire }, { Fire, Metal }, { Metal, Wood }
    };

    /// <summary>
    /// 被克关系映射（五行被克：木被金克、土被木克等）
    /// </summary>
    private static readonly Dictionary<FivePhase, FivePhase> RestrainsByMap = new()
    {
        { Earth, Wood }, { Water, Earth }, { Fire, Water }, { Metal, Fire }, { Wood, Metal }
    };

    /// <inheritdoc />
    public bool IsGenerates(FivePhase other) =>
        GeneratesMap.TryGetValue(this, out var target) && target == other;

    /// <inheritdoc />
    public bool Generates(FivePhase other) => GeneratesMap[this] == other;

    /// <inheritdoc />
    public bool GeneratesBy(FivePhase other) => GeneratesByMap[this] == other;

    /// <inheritdoc />
    public bool IsRestrains(FivePhase other) =>
        RestrainsMap.TryGetValue(this, out var target) && target == other;

    /// <inheritdoc />
    public bool Restrains(FivePhase other) => RestrainsMap[this] == other;

    /// <inheritdoc />
    public bool RestrainsBy(FivePhase other) => RestrainsByMap[this] == other;
}