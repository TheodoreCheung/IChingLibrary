using Lunar;

namespace IChingLibrary.SixLines;

/// <summary>
/// 起卦时间信息，包含阳历、阴历和干支信息
/// </summary>
public readonly struct CastingTime(DateTimeOffset solar, DateTimeOffset lunar, LunarStemBranch stemBranch)
{
    /// <summary>
    /// 阳历
    /// </summary>
    public DateTimeOffset Solar { get; } = solar;

    /// <summary>
    /// 阴历
    /// </summary>
    public DateTimeOffset Lunar { get; } = lunar;

    /// <summary>
    /// 阴历干支
    /// </summary>
    public LunarStemBranch StemBranch { get; } = stemBranch;

    /// <summary>
    /// 将DateTimeOffset对象转换为CastingTime对象
    /// 该方法将公历日期时间转换为农历日期时间，并计算对应的干支纪年
    /// </summary>
    /// <param name="dateTime">要转换的DateTimeOffset对象</param>
    /// <returns>返回一个CastingTime对象，包含公历时间、农历时间和干支信息</returns>
    public static CastingTime ConvertFrom(DateTimeOffset dateTime)
    {
        // 创建Solar对象，用于公历和农历转换
        var solar = new Solar(dateTime.LocalDateTime);
        // 获取农历日期时间信息
        var lunar = solar.Lunar;

        // 构建农历的DateTimeOffset对象，保持与原时间相同的时区偏移
        var lunarDt = new DateTimeOffset(lunar.Year, Math.Abs(lunar.Month), lunar.Day, lunar.Hour, lunar.Minute,
            lunar.Second, dateTime.Offset);

        // 创建干支对象，包含年、月、日、时的干支信息
        var stemBranch = new LunarStemBranch(
            // 年干支：根据年干支索引创建对应的干支对象
            new StemBranch(HeavenlyStem.FromValue((byte)(lunar.YearGanIndex + 1)),
                EarthlyBranch.FromValue((byte)(lunar.YearZhiIndex + 1))),
            // 月干支：根据月干支索引创建对应的干支对象
            new StemBranch(HeavenlyStem.FromValue((byte)(lunar.MonthGanIndex + 1)),
                EarthlyBranch.FromValue((byte)(lunar.MonthZhiIndex + 1))),
            // 日干支：根据日干支索引创建对应的干支对象
            new StemBranch(HeavenlyStem.FromValue((byte)(lunar.DayGanIndex + 1)),
                EarthlyBranch.FromValue((byte)(lunar.DayZhiIndex + 1))),
            // 时干支：根据时干支索引创建对应的干支对象
            new StemBranch(HeavenlyStem.FromValue((byte)(lunar.TimeGanIndex + 1)),
                EarthlyBranch.FromValue((byte)(lunar.TimeZhiIndex + 1)))
        );

        // 返回包含公历时间、农历时间和干支信息的CastingTime对象
        return new CastingTime(dateTime, lunarDt, stemBranch);
    }
}

/// <summary>
/// 阴历干支，包含年、月、日、时四柱干支
/// </summary>
public readonly struct LunarStemBranch(StemBranch year, StemBranch month, StemBranch day, StemBranch hour)
{
    /// <summary>
    /// 年干支
    /// </summary>
    public StemBranch Year { get; } = year;

    /// <summary>
    /// 月干支
    /// </summary>
    public StemBranch Month { get; } = month;

    /// <summary>
    /// 日干支
    /// </summary>
    public StemBranch Day { get; } = day;

    /// <summary>
    /// 时干支
    /// </summary>
    public StemBranch Hour { get; } = hour;

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Year} {Month} {Day} {Hour}";
    }
}