using IChingLibrary.Core.Annotations;

namespace IChingLibrary.Core;

/// <summary>
/// 爻位，表示六爻占卜中的六个位置
/// </summary>
/// <remarks>
/// 索引说明：
/// - Value 属性使用 1-based 索引（1-6），对应人类可读的爻位（初爻到上爻）
/// - 数组索引使用 0-based 索引（0-5），对应程序中的数组位置
/// - 转换公式：数组索引 = Value - 1
/// </remarks>
[IChingElementEnum]
public partial class LinePosition : IChingElement<LinePosition>
{
    private LinePosition(byte value, string label) : base(value, label) {}

    /// <summary>
    /// 初爻（第一爻）
    /// </summary>
    public static readonly LinePosition First = new(1, nameof(First));

    /// <summary>
    /// 二爻（第二爻）
    /// </summary>
    public static readonly LinePosition Second = new(2, nameof(Second));

    /// <summary>
    /// 三爻（第三爻）
    /// </summary>
    public static readonly LinePosition Third = new(3, nameof(Third));

    /// <summary>
    /// 四爻（第四爻）
    /// </summary>
    public static readonly LinePosition Fourth = new(4, nameof(Fourth));

    /// <summary>
    /// 五爻（第五爻）
    /// </summary>
    public static readonly LinePosition Fifth = new(5, nameof(Fifth));

    /// <summary>
    /// 上爻（第六爻）
    /// </summary>
    public static readonly LinePosition Sixth = new(6, nameof(Sixth));

    /// <summary>
    /// 获取对应的数组索引（0-based）
    /// </summary>
    /// <returns>0-5 的数组索引</returns>
    public int ToArrayIndex() => Value - 1;

    /// <summary>
    /// 从数组索引获取 LinePosition
    /// </summary>
    /// <param name="arrayIndex">0-5 的数组索引</param>
    /// <returns>对应的 LinePosition</returns>
    public static LinePosition FromArrayIndex(int arrayIndex)
    {
        return arrayIndex switch
        {
            0 => First,
            1 => Second,
            2 => Third,
            3 => Fourth,
            4 => Fifth,
            5 => Sixth,
            _ => throw new ArgumentOutOfRangeException(nameof(arrayIndex), "数组索引必须在 0-5 范围内")
        };
    }
}