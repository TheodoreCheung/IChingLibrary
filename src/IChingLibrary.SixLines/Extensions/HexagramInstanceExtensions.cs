namespace IChingLibrary.SixLines;

/// <summary>
/// HexagramInstance 扩展方法
/// </summary>
public static class HexagramInstanceExtensions
{
    /// <summary>
    /// 查找卦身
    /// 规则：阳世起子阴起午，俱从初爻数到世
    /// </summary>
    /// <param name="hexagram">卦实例</param>
    /// <returns>卦身地支，null 表示无卦身</returns>
    public static EarthlyBranch? FindHexagramBody(this HexagramInstance hexagram)
    {
        // 1. 找到世爻
        var worldLine = hexagram.Lines.FirstOrDefault(l => l.Position == Position.Worldly);
        if (worldLine is null || worldLine.Position != Position.Worldly)
        {
            return null;
        }

        // 2. 根据世爻阴阳确定起始地支
        var startBranch = worldLine.YinYang == YinYang.Yang
            ? EarthlyBranch.Zi  // 阳世起子
            : EarthlyBranch.Wu; // 阴世起午

        // 3. 从初爻数到世爻位置
        var offset = worldLine.LinePosition.ToArrayIndex(); // 0-5
        var hexagramBodyValue = (byte)((startBranch.Value + offset - 1) % 12 + 1);

        return EarthlyBranch.FromValue(hexagramBodyValue);
    }
}
