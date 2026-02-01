namespace IChingLibrary.SixLines;

/// <summary>
/// 卦实例，包含一个卦的完整信息和六个爻的数据
/// </summary>
public class HexagramInstance
{
    /// <summary>
    /// 卦的元数据
    /// </summary>
    public Hexagram Meta { get; }

    /// <summary>
    /// 六个爻的只读列表（从初爻到上爻）
    /// </summary>
    public IReadOnlyList<Line> Lines => _lines;

    /// <summary>
    /// 爻的内部数组（用于 Provider 修改）
    /// </summary>
    private readonly Line[] _lines;

    /// <summary>
    /// 初始化卦实例
    /// </summary>
    /// <param name="meta">卦的元数据</param>
    public HexagramInstance(Hexagram meta)
    {
        Meta = meta;

        var linePositions = LinePosition.GetAll().OrderBy(p => p.Value).ToList();

        _lines = Enumerable.Range(0, 6)
            .Select(i => new Line
            {
                LinePosition = linePositions[i],
                YinYang = ((meta.Value >> i) & 1) == 1 ? YinYang.Yang : YinYang.Yin
            }).ToArray();
    }

    /// <summary>
    /// 获取指定索引位置的爻
    /// </summary>
    /// <param name="index">爻索引（0-5）</param>
    /// <returns>指定位置的爻</returns>
    public Line this[int index] => Lines[index];
}