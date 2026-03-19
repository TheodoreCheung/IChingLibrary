namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 基于时间的起卦方式（梅花易数）
/// </summary>
/// <param name="castingTime">起卦时间</param>
public class TimeBasedCastingMethod(DateTimeOffset castingTime) : ICastingMethod
{
    /// <inheritdoc />
    public SixLineDivination Cast()
    {
        var ctime = CastingTime.ConvertFrom(castingTime);
        
        // 获取年地支值、时地支值和阴历月日
        var yearBranchValue = ctime.StemBranch.Year.Branch.Value;
        var hourBranchValue = ctime.StemBranch.Hour.Branch.Value;
        var lunarMonth = ctime.Lunar.Month;
        var lunarDay = ctime.Lunar.Day;

        // 计算上下卦和动爻
        var upperTrigramNumber = yearBranchValue + lunarMonth + lunarDay;
        var lowerTrigramNumber = yearBranchValue + lunarMonth + lunarDay + hourBranchValue;
        var changingLineNumber = yearBranchValue + lunarMonth + lunarDay + hourBranchValue;

        return NumberBasedCastingMethod.CreateDivination(ctime, upperTrigramNumber, lowerTrigramNumber, changingLineNumber);
    }
}

/// <summary>
/// 基于掷钱的起卦方式
/// </summary>
/// <param name="castingTime">起卦时间</param>
/// <param name="fourSymbols">六爻对应的四象序列</param>
public class CoinCastingMethod(DateTimeOffset castingTime, FourSymbol[] fourSymbols) : ICastingMethod
{
    /// <inheritdoc />
    public SixLineDivination Cast() => CreateDivination(CastingTime.ConvertFrom(castingTime), fourSymbols);

    internal static SixLineDivination CreateDivination(CastingTime castingTime, FourSymbol[] fourSymbols)
    {
        if (fourSymbols.Length != 6)
            throw new ArgumentException("必须提供6个四象值", nameof(fourSymbols));
        
        byte originalValue = 0;
        byte changingMask = 0;
        
        for (var i = 0; i < 6; i++)
        {
            var symbol = fourSymbols[i];
            if (symbol.YinYang == YinYang.Yang)
                originalValue |= (byte)(1 << i);
            if (symbol.IsChanging)
                changingMask |= (byte)(1 << i);
        }

        var hasChanging = changingMask != 0;
        var changedValue = (byte)(originalValue ^ changingMask);
        return SpecifyingHexagramCastingMethod.CreateDivination(
            castingTime,
            Hexagram.FromValue(originalValue),
            hasChanging ? Hexagram.FromValue(changedValue) : null);
    }
}

/// <summary>
/// 基于数字的起卦方式
/// </summary>
/// <param name="castingTime">起卦时间</param>
/// <param name="upperTrigramNumber">上卦数字</param>
/// <param name="lowerTrigramNumber">下卦数字</param>
/// <param name="changingLineNumber">动爻数字（可选）</param>
public class NumberBasedCastingMethod(DateTimeOffset castingTime, int upperTrigramNumber, int lowerTrigramNumber, int? changingLineNumber) : ICastingMethod
{
    /// <inheritdoc />
    public SixLineDivination Cast() => CreateDivination(CastingTime.ConvertFrom(castingTime), upperTrigramNumber, lowerTrigramNumber, changingLineNumber);

    internal static SixLineDivination CreateDivination(
        CastingTime castingTime,
        int upperTrigramNumber,
        int lowerTrigramNumber,
        int? changingLineNumber)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(upperTrigramNumber);
        ArgumentOutOfRangeException.ThrowIfNegative(lowerTrigramNumber);
        if (changingLineNumber is < 0)
            throw new ArgumentOutOfRangeException(nameof(changingLineNumber));

        // 通过随机数获取上下卦
        var upperTrigram = GetTrigramByNumber(upperTrigramNumber);
        var lowerTrigram = GetTrigramByNumber(lowerTrigramNumber);

        // 创建主卦
        var original = Hexagram.Create(upperTrigram, lowerTrigram);

        // 计算动爻位置（1-6，1为初爻，6为上爻）
        int changingLinePosition;
        if (changingLineNumber.HasValue)
        {
            changingLinePosition = GetChangingLinePosition(changingLineNumber.Value);
        }
        else
        {
            // 如果没有提供动爻随机数，则使用公式：(上卦数 + 下卦数 + 日支) % 6
            var dayBranchValue = castingTime.StemBranch.Day.Branch.Value;
            changingLinePosition = GetChangingLinePosition(upperTrigramNumber + lowerTrigramNumber + dayBranchValue);
        }

        var changed = (byte)(original.Value ^ (1 << changingLinePosition - 1));
        return SpecifyingHexagramCastingMethod.CreateDivination(castingTime, original, Hexagram.FromValue(changed));
    }
    
    /// <summary>
    /// 根据随机数获取对应的八卦
    /// </summary>
    /// <param name="number">随机数</param>
    /// <returns>对应的卦象</returns>
    private static Trigram GetTrigramByNumber(int number)
    {
        var remainder = number % 8;
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
    /// <param name="number">随机数</param>
    /// <returns>动爻位置（1-6）</returns>
    private static int GetChangingLinePosition(int number)
    {
        var remainder = number % 6;
        return remainder == 0 ? 6 : remainder;
    }
}

/// <summary>
/// 指定卦象的起卦方式
/// </summary>
/// <param name="castingTime">起卦时间</param>
/// <param name="original">本卦</param>
/// <param name="changed">之卦（可选）</param>
public class SpecifyingHexagramCastingMethod(DateTimeOffset castingTime, Hexagram original, Hexagram? changed = null) : ICastingMethod
{
    /// <inheritdoc />
    public SixLineDivination Cast() => CreateDivination(CastingTime.ConvertFrom(castingTime), original, changed);

    internal static SixLineDivination CreateDivination(CastingTime castingTime, Hexagram original, Hexagram? changed)
    {
        var originalInstance = new HexagramInstance(original);

        if (changed is null)
            return new SixLineDivination(castingTime, originalInstance);

        var changingMask = (byte)(original.Value ^ changed.Value);
        if (changingMask == 0)
            return new SixLineDivination(castingTime, originalInstance);

        ApplyChangingLines(originalInstance, changingMask);

        return new SixLineDivination(castingTime, originalInstance, new HexagramInstance(changed));
    }

    private static void ApplyChangingLines(HexagramInstance original, byte changingMask)
    {
        for (var i = 0; i < 6; i++)
        {
            if (((changingMask >> i) & 1) == 1)
            {
                original.Lines[i].IsChanging = true;
            }
        }
    }
}
