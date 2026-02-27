using IChingLibrary.Core.Annotations;

namespace IChingLibrary.Core;

/// <summary>
/// 卦属特性（如六冲卦、六合卦、游魂卦、归魂卦等）
/// </summary>
[IChingElementEnum]
public partial class HexagramNature : IChingElement<HexagramNature>
{
    /// <inheritdoc />
    private HexagramNature(byte value, string label) : base(value, label)
    {
    }

    /// <summary>
    /// 六冲卦
    /// </summary>
    public static readonly HexagramNature SixClashes = new(1, nameof(SixClashes));

    /// <summary>
    /// 六合卦
    /// </summary>
    public static readonly HexagramNature SixHarmonies = new(2, nameof(SixHarmonies));

    /// <summary>
    /// 游魂卦
    /// </summary>
    public static readonly HexagramNature WanderingSoul = new(3, nameof(WanderingSoul));

    /// <summary>
    /// 归魂卦
    /// </summary>
    public static readonly HexagramNature ReturningSoul = new(4, nameof(ReturningSoul));
}