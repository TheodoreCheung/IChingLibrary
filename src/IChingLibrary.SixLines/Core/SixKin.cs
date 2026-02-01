using IChingLibrary.Core.Annotations;

namespace IChingLibrary.Core;

/// <summary>
/// 六亲，表示六爻占卜中的五行关系
/// </summary>
[IChingElementEnum]
public partial class SixKin : IChingElement<SixKin>
{
    /// <inheritdoc />
    private SixKin(byte value, string label) : base(value, label)
    {
    }
    
    /// <summary>
    /// 父母
    /// </summary>
    public static readonly SixKin Parent = new(1, nameof(Parent));
        
    /// <summary>
    /// 兄弟
    /// </summary>
    public static readonly SixKin Sibling = new(2, nameof(Sibling));
        
    /// <summary>
    /// 妻财
    /// </summary>
    public static readonly SixKin Wealth = new(3, nameof(Wealth));
        
    /// <summary>
    /// 官鬼
    /// </summary>
    public static readonly SixKin Officer = new(4, nameof(Officer));
        
    /// <summary>
    /// 子孙
    /// </summary>
    public static readonly SixKin Offspring = new(5, nameof(Offspring));
}