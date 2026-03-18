namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 纳甲步骤
/// </summary>
public sealed class NajiaStep : IStructuringStep
{
    /// <summary>
    /// 纳甲表（内卦/外卦对应的天干）
    /// </summary>
    private static ReadOnlySpan<byte> StemTable =>
    [
        2, 10, // 坤：内乙外癸 
        7, 7, // 震：内外庚 
        5, 5, // 坎：内外戊 
        4, 4, // 兑：内外丁 
        3, 3, // 艮：内外丙 
        6, 6, // 离：内外己 
        8, 8, // 巽：内外辛 
        1, 9 // 乾：内甲外壬
    ];

    /// <summary>
    /// 纳支表（内卦/外卦对应的地支）
    /// </summary>
    private static ReadOnlySpan<byte> BranchTable =>
    [
        8, 6, 4, 2, 12, 10, // 坤：未巳卯丑亥酉
        1, 3, 5, 7, 9, 11, // 震：子寅辰午申戌
        3, 5, 7, 9, 11, 1, // 坎：寅辰午申戌子
        6, 4, 2, 12, 10, 8, // 兑：巳卯丑亥酉未
        5, 7, 9, 11, 1, 3, // 艮：辰午申戌子寅
        4, 2, 12, 10, 8, 6, // 离：卯丑亥酉未巳
        2, 12, 10, 8, 6, 4, // 巽：丑亥酉未巳卯
        1, 3, 5, 7, 9, 11 // 乾：子寅辰午申戌
    ];

    /// <inheritdoc />
    public IEnumerable<Type> RequiredSteps { get; } = [];

    /// <inheritdoc />
    public void Execute(DivinationContext context)
    {
        Bind(context.SixLineDivination.Original);
        
        if (context.SixLineDivination.Changed is not null)
            Bind(context.SixLineDivination.Changed);
    }
    
    private static void Bind(HexagramInstance hexagram)
    {
        // 1. 获取内卦和外卦的元数据索引 (0-7)
        var lowerIdx = hexagram.Meta.Lower.Value;
        var upperIdx = hexagram.Meta.Upper.Value;

        // 2. 获取对应的天干 (内卦用内干，外卦用外干)
        // 假设 NajiaStemTable 存储格式为：[坤内, 坤外, 震内, 震外...]
        var lowerStem = StemTable[lowerIdx * 2];
        var upperStem = StemTable[upperIdx * 2 + 1];

        // 3. 获取地支切片 (Span 切片不产生内存分配)
        var lowerBranches = BranchTable.Slice(lowerIdx * 6, 3); // 内卦取前三支
        var upperBranches = BranchTable.Slice(upperIdx * 6 + 3, 3); // 外卦取后三支

        // 4. 赋值
        var lines = hexagram.Lines;
        for (var i = 0; i < 3; i++)
        {
            lines[i].StemBranch = new StemBranch(HeavenlyStem.FromValue(lowerStem), EarthlyBranch.FromValue(lowerBranches[i]));
            lines[i + 3].StemBranch = new StemBranch(HeavenlyStem.FromValue(upperStem), EarthlyBranch.FromValue(upperBranches[i]));
        }
    }
}
