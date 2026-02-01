using IChingLibrary.Core.Annotations;

namespace IChingLibrary.Core;

/// <summary>
/// 世应位置，表示六爻占卜中的世爻和应爻位置
/// </summary>
[IChingElementEnum]
public partial class Position : IChingElement<Position>
{
    /// <inheritdoc />
    private Position(byte value, string label) : base(value, label)
    {
    }
    
    /// <summary>
    /// 世
    /// </summary>
    public static readonly Position Worldly = new(1, nameof(Worldly));
        
    /// <summary>
    /// 应
    /// </summary>
    public static readonly Position Corresponding = new(2, nameof(Corresponding));
}