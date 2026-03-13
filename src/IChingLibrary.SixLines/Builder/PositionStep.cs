namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 安世应步骤
/// </summary>
public sealed class PositionStep : IStructuringStep
{
    /// <summary>
    /// 基于“天同二世天变五，地同四世地变初，本宫六世三世异，人同游魂人变归”口诀的映射表。
    /// 索引为上下卦按位排布相同状态的二进制：相同为1，不同为0。
    /// bit0: 初四同, bit1: 二五同, bit2: 三上同。
    /// </summary>
    private static readonly int[] WorldlyPositions = [2, 3, 3, 4, 1, 2, 0, 5];

    /// <summary>
    /// 获取世爻和应爻的索引位置
    /// </summary>
    /// <param name="hexagram">卦实例</param>
    /// <returns>世爻索引与应爻索引</returns>
    private static (int worldlyIndex, int correspondingIndex) GetPositionIndices(HexagramInstance hexagram)
    {
        var hexagramValue = hexagram.Meta.Value;

        var lower = hexagramValue & 0b111;
        var upper = (hexagramValue >> 3) & 0b111;

        // 计算上下卦对应爻是否相同：相同为1，不同为0
        // bit0: 初爻与四爻相同; bit1: 二爻与五爻相同; bit2: 三爻与上爻相同
        var match = (~(lower ^ upper)) & 0b111;

        var worldlyIndex = WorldlyPositions[match];
        var correspondingIndex = (worldlyIndex + 3) % 6;

        return (worldlyIndex, correspondingIndex);
    }
    
    /// <inheritdoc />
    public IEnumerable<Type> RequiredSteps { get; } = [];

    /// <inheritdoc />
    public void Execute(DivinationContext context)
    {
        var (worldlyIndex, correspondingIndex) = GetPositionIndices(context.SixLineDivination.Original);
        context.SixLineDivination.Original.Lines[worldlyIndex].Position = Position.Worldly;
        context.SixLineDivination.Original.Lines[correspondingIndex].Position = Position.Corresponding;
    }
}
