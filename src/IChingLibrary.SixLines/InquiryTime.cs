namespace IChingLibrary.SixLines;

/// <summary>
/// 起卦时间信息，包含阳历、阴历和干支信息
/// </summary>
public readonly struct InquiryTime(DateTimeOffset solar, DateTimeOffset lunar, LunarStemBranch stemBranch)
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