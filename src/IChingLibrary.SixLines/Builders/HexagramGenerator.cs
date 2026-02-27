using IChingLibrary.SixLines.Providers;

namespace IChingLibrary.SixLines;

/// <summary>
/// 卦生成器，负责通过各种起卦方式生成四象数组
/// </summary>
internal static class HexagramGenerator
{
    /// <summary>
    /// 时间起卦法（根据年月日时自动起卦）
    /// </summary>
    /// <param name="inquiryTime">起卦时间</param>
    /// <returns>四象数组</returns>
    internal static FourSymbol[] FromTime(DateTimeOffset inquiryTime)
    {
        // 获取起卦时间的干支信息
        var inquiryTimeProvider = new DefaultInquiryTimeProvider();
        var convertedInquiryTime = inquiryTimeProvider.ConvertFrom(inquiryTime);

        // 获取年地支值、时地支值和阴历月日
        var yearBranchValue = convertedInquiryTime.StemBranch.Year.Branch.Value;
        var hourBranchValue = convertedInquiryTime.StemBranch.Hour.Branch.Value;
        var lunarMonth = convertedInquiryTime.Lunar.Month;
        var lunarDay = convertedInquiryTime.Lunar.Day;

        // 计算上下卦和动爻
        var upperTrigramNumber = yearBranchValue + lunarMonth + lunarDay;
        var lowerTrigramNumber = yearBranchValue + lunarMonth + lunarDay + hourBranchValue;
        var changingLineNumber = yearBranchValue + lunarMonth + lunarDay + hourBranchValue;

        return FromRandomNumbers(inquiryTime, upperTrigramNumber, lowerTrigramNumber, changingLineNumber);
    }

    /// <summary>
    /// 随机数起卦法
    /// </summary>
    /// <param name="inquiryTime">起卦时间</param>
    /// <param name="upperTrigramNumber">上卦随机数</param>
    /// <param name="lowerTrigramNumber">下卦随机数</param>
    /// <param name="changingLineNumber">动爻随机数（可选）</param>
    /// <returns>四象数组</returns>
    internal static FourSymbol[] FromRandomNumbers(
        DateTimeOffset inquiryTime,
        int upperTrigramNumber,
        int lowerTrigramNumber,
        int? changingLineNumber = null)
    {
        // 通过随机数获取上下卦
        var upperTrigram = GetTrigramByNumber(upperTrigramNumber);
        var lowerTrigram = GetTrigramByNumber(lowerTrigramNumber);

        // 创建主卦
        var originalHexagram = Hexagram.Create(upperTrigram, lowerTrigram);

        // 计算动爻位置（1-6，1为初爻，6为上爻）
        int changingLinePosition;
        if (changingLineNumber.HasValue)
        {
            changingLinePosition = GetChangingLinePosition(changingLineNumber.Value);
        }
        else
        {
            // 如果没有提供动爻随机数，则使用公式：(上卦数 + 下卦数 + 日支) % 6
            var inquiryTimeProvider = new DefaultInquiryTimeProvider();
            var convertedInquiryTime = inquiryTimeProvider.ConvertFrom(inquiryTime);
            var dayBranchValue = convertedInquiryTime.StemBranch.Day.Branch.Value;
            changingLinePosition = GetChangingLinePosition(upperTrigramNumber + lowerTrigramNumber + dayBranchValue);
        }

        return GenerateFourSymbols(originalHexagram, changingLinePosition);
    }

    /// <summary>
    /// 指定主卦和变卦起卦
    /// </summary>
    /// <param name="original">主卦</param>
    /// <param name="changed">变卦（可选）</param>
    /// <returns>四象数组</returns>
    internal static FourSymbol[] FromHexagrams(Hexagram original, Hexagram? changed = null)
    {
        return GenerateFourSymbols(original, changed);
    }

    /// <summary>
    /// 四象起卦（直接使用四象数组）
    /// </summary>
    /// <param name="fourSymbols">四象数组</param>
    /// <returns>四象数组</returns>
    internal static FourSymbol[] FromFourSymbols(FourSymbol[] fourSymbols)
    {
        if (fourSymbols.Length != 6)
            throw new ArgumentException("必须提供6个四象值", nameof(fourSymbols));
        return fourSymbols;
    }

    /// <summary>
    /// 根据随机数获取对应的八卦
    /// </summary>
    private static Trigram GetTrigramByNumber(int number)
    {
        int remainder = number % 8;
        return remainder switch
        {
            1 => Trigram.Qian,   // 乾
            2 => Trigram.Dui,     // 兑
            3 => Trigram.Li,      // 离
            4 => Trigram.Zhen,    // 震
            5 => Trigram.Xun,     // 巽
            6 => Trigram.Kan,     // 坎
            7 => Trigram.Gen,     // 艮
            0 => Trigram.Kun,     // 坤
            _ => throw new InvalidOperationException("无效的余数")
        };
    }

    /// <summary>
    /// 根据随机数获取动爻位置（1-6）
    /// </summary>
    private static int GetChangingLinePosition(int number)
    {
        int remainder = number % 6;
        return remainder == 0 ? 6 : remainder;
    }

    /// <summary>
    /// 生成四象数组（六爻）- 根据动爻位置
    /// </summary>
    private static FourSymbol[] GenerateFourSymbols(Hexagram hexagram, int changingLinePosition)
    {
        var fourSymbols = new FourSymbol[6];
        var linePositions = LinePosition.GetAll().OrderBy(p => p.Value).ToList();

        for (int i = 0; i < 6; i++)
        {
            // 获取当前爻的阴阳（从卦的二进制位获取）
            var yinYang = ((hexagram.Value >> i) & 1) == 1 ? YinYang.Yang : YinYang.Yin;

            // 如果是动爻位置，使用老阴或老阳
            if (linePositions[i].Value == changingLinePosition)
            {
                fourSymbols[i] = yinYang == YinYang.Yang ? FourSymbol.OldYang : FourSymbol.OldYin;
            }
            else
            {
                fourSymbols[i] = yinYang == YinYang.Yang ? FourSymbol.YoungYang : FourSymbol.YoungYin;
            }
        }

        return fourSymbols;
    }

    /// <summary>
    /// 生成四象数组（六爻）- 根据主卦和变卦
    /// </summary>
    private static FourSymbol[] GenerateFourSymbols(Hexagram original, Hexagram? changed)
    {
        var fourSymbols = new FourSymbol[6];

        for (int i = 0; i < 6; i++)
        {
            // 获取主卦当前爻的阴阳
            var originalYinYang = ((original.Value >> i) & 1) == 1 ? YinYang.Yang : YinYang.Yin;

            if (changed != null)
            {
                // 获取变卦当前爻的阴阳
                var changedYinYang = ((changed.Value >> i) & 1) == 1 ? YinYang.Yang : YinYang.Yin;

                // 如果主卦和变卦的阴阳不同，则该爻为动爻
                if (originalYinYang != changedYinYang)
                {
                    fourSymbols[i] = originalYinYang == YinYang.Yang ? FourSymbol.OldYang : FourSymbol.OldYin;
                }
                else
                {
                    fourSymbols[i] = originalYinYang == YinYang.Yang ? FourSymbol.YoungYang : FourSymbol.YoungYin;
                }
            }
            else
            {
                // 没有变卦，全部使用少阴少阳
                fourSymbols[i] = originalYinYang == YinYang.Yang ? FourSymbol.YoungYang : FourSymbol.YoungYin;
            }
        }

        return fourSymbols;
    }
}
