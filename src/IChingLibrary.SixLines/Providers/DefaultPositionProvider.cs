using IChingLibrary.SixLines.Providers.Abstractions;

namespace IChingLibrary.SixLines.Providers;

/// <summary>
/// 默认世应位置提供器
/// </summary>
internal class DefaultPositionProvider : IPositionProvider
{
    /// <summary>
    /// 世应位置查找表：键为(变化爻数,首个未变化爻索引)，值为(世爻索引,应爻索引)
    /// </summary>
    private static readonly Dictionary<(int changingCount, int firstUnchangedIndex), (int worldly, int corresponding)> PositionLookupTable = new()
    {
        // 一世卦：世在初爻，应在四爻
        [(1, 1)] = (0, 3),
        // 二世卦：世在二爻，应在五爻
        [(2, 2)] = (1, 4),
        // 三世卦：世在三爻，应在上爻
        [(3, 3)] = (2, 5),
        // 四世卦：世在四爻，应在初爻
        [(4, 4)] = (3, 0),
        // 五世卦：世在五爻，应在二爻
        [(5, 5)] = (4, 1),
        // 游魂卦：世在四爻，应在初爻
        [(4, 2)] = (3, 0),
        // 归魂卦：世在三爻，应在上爻
        [(3, 1)] = (2, 5),
    };

    /// <summary>
    /// 纯卦（本宫卦）的世应位置：世在上爻，应在三爻
    /// </summary>
    private static readonly (int worldly, int corresponding) PureHexagramPosition = (5, 2);

    /// <inheritdoc />
    public void BindPositions(HexagramInstance hexagram)
    {
        var (worldlyIndex, correspondingIndex) = GetPositionIndices(hexagram);
        hexagram.Lines[worldlyIndex].Position = Position.Worldly;
        hexagram.Lines[correspondingIndex].Position = Position.Corresponding;
    }

    /// <summary>
    /// 获取世爻和应爻的索引位置
    /// </summary>
    private static (int worldlyIndex, int correspondingIndex) GetPositionIndices(HexagramInstance hexagram)
    {
        var hexagramValue = hexagram.Meta.Value;
        var palace = hexagram.Meta.Palace;
        var pureHexagramValue = (byte)((byte)palace << 3 | (byte)palace);

        // 纯卦（本宫卦）
        if (hexagramValue == pureHexagramValue)
        {
            return PureHexagramPosition;
        }

        // 计算变化爻数和首个未变化爻索引
        var (changingCount, firstUnchangedIndex) = AnalyzeHexagramChanges(hexagramValue, pureHexagramValue);

        // 从查找表获取世应位置
        var key = (changingCount, firstUnchangedIndex);
        return PositionLookupTable.GetValueOrDefault(key, PureHexagramPosition);
    }

    /// <summary>
    /// 分析卦与纯卦的差异，返回变化爻数和首个未变化爻索引
    /// </summary>
    /// <param name="hexagramValue">卦值</param>
    /// <param name="pureHexagramValue">纯卦值</param>
    /// <returns>(变化爻数, 首个未变化爻索引)</returns>
    private static (int changingCount, int firstUnchangedIndex) AnalyzeHexagramChanges(byte hexagramValue, byte pureHexagramValue)
    {
        int changingCount = 0;
        int firstUnchangedIndex = -1;

        for (int i = 0; i < 6; i++)
        {
            var hexBit = (hexagramValue >> i) & 1;
            var pureBit = (pureHexagramValue >> i) & 1;

            if (hexBit == pureBit)
            {
                if (firstUnchangedIndex == -1)
                {
                    firstUnchangedIndex = i;
                }
            }
            else
            {
                changingCount++;
            }
        }

        return (changingCount, firstUnchangedIndex);
    }
}
