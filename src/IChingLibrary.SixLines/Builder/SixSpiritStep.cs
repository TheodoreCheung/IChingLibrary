namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 绑六神步骤
/// </summary>
public sealed class SixSpiritStep : IStructuringStep
{
    /// <summary>
    /// 六神顺序：青龙、朱雀、勾陈、螣蛇、白虎、玄武
    /// </summary>
    private static readonly SixSpirit[] SpiritsOrder =
    [
        SixSpirit.AzureDragon,
        SixSpirit.VermilionBird,
        SixSpirit.HookChen,
        SixSpirit.CoiledSnake,
        SixSpirit.WhiteTiger,
        SixSpirit.BlackTortoise
    ];

    /// <summary>
    /// 根据日干获取起始六神索引
    /// 规则：甲/乙日起青龙，丙/丁日起朱雀，戊/己日起勾陈，庚/辛日起螣蛇，壬/癸日起白虎
    /// </summary>
    /// <param name="dayStem">日干</param>
    /// <returns>起始六神索引</returns>
    private static int GetStartIndex(HeavenlyStem dayStem)
    {
        return dayStem.Value switch
        {
            1 or 2 => 0, // 甲/乙 -> 青龙
            3 or 4 => 1, // 丙/丁 -> 朱雀
            5 => 2, // 戊 -> 勾陈
            6 => 3, // 己 -> 螣蛇
            7 or 8 => 4, // 庚/辛 -> 白虎
            9 or 10 => 5, // 壬/癸 -> 玄武
            _ => 0
        };
    }
    
    /// <inheritdoc />
    public IEnumerable<Type> RequiredSteps { get; } = [];

    /// <inheritdoc />
    public void Execute(DivinationContext context)
    {
        var dayStem = context.SixLineDivination.CastingTime.StemBranch.Day.Stem;

        // 根据日干确定起始六神（初爻）
        var startIndex = GetStartIndex(dayStem);

        for (var i = 0; i < 6; i++)
        {
            context.SixLineDivination.Original.Lines[i].SixSpirit = SpiritsOrder[(startIndex + i) % 6];
        }
    }
}
