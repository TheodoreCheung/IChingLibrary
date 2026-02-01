using IChingLibrary.SixLines.Providers.Abstractions;

namespace IChingLibrary.SixLines.Providers;

/// <summary>
/// 默认六神提供器
/// </summary>
internal class DefaultSixSpiritProvider : ISixSpiritProvider
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

    /// <inheritdoc />
    public void BindSixSpirits(HexagramInstance hexagram, InquiryTime inquiryTime)
    {
        var dayStem = inquiryTime.StemBranch.Day.Stem;

        // 根据日干确定起始六神（初爻）
        var startIndex = GetStartIndex(dayStem);

        for (int i = 0; i < 6; i++)
        {
            hexagram.Lines[i].SixSpirit = SpiritsOrder[(startIndex + i) % 6];
        }
    }

    /// <summary>
    /// 根据日干获取起始六神索引
    /// 规则：甲/乙日起青龙，丙/丁日起朱雀，戊/己日起勾陈，庚/辛日起螣蛇，壬/癸日起白虎
    /// </summary>
    private static int GetStartIndex(HeavenlyStem dayStem)
    {
        return dayStem.Value switch
        {
            1 or 2 => 0, // 甲/乙 -> 青龙
            3 or 4 => 1, // 丙/丁 -> 朱雀
            5 or 6 => 2, // 戊/己 -> 勾陈
            7 or 8 => 3, // 庚/辛 -> 螣蛇
            9 or 10 => 4, // 壬/癸 -> 白虎
            _ => 0
        };
    }
}
