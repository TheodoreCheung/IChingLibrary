using IChingLibrary.Core.Annotations;

namespace IChingLibrary.Core;

/// <summary>
/// 六神，表示六爻占卜中的神煞
/// </summary>
[IChingElementEnum]
public partial class SixSpirit : IChingElement<SixSpirit>
{
    /// <inheritdoc />
    private SixSpirit(byte value, string label) : base(value, label)
    {
    }
    
    /// <summary>
    /// 青龙
    /// </summary>
    public static readonly SixSpirit AzureDragon = new(1, nameof(AzureDragon));

    /// <summary>
    /// 朱雀
    /// </summary>
    public static readonly SixSpirit VermilionBird = new(2, nameof(VermilionBird));

    /// <summary>
    /// 勾陈
    /// </summary>
    public static readonly SixSpirit HookChen = new(3, nameof(HookChen));

    /// <summary>
    /// 螣蛇
    /// </summary>
    public static readonly SixSpirit CoiledSnake = new(4, nameof(CoiledSnake));

    /// <summary>
    /// 白虎
    /// </summary>
    public static readonly SixSpirit WhiteTiger = new(5, nameof(WhiteTiger));

    /// <summary>
    /// 玄武
    /// </summary>
    public static readonly SixSpirit BlackTortoise = new(6, nameof(BlackTortoise));
}