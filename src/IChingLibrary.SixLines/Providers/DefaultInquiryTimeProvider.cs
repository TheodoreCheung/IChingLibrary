using IChingLibrary.SixLines.Providers.Abstractions;
using Lunar;

namespace IChingLibrary.SixLines.Providers;

/// <summary>
/// 默认起卦时间信息转换器，使用 Lunar 库进行阳历到阴历的转换
/// </summary>
internal class DefaultInquiryTimeProvider : IInquiryTimeProvider
{
    /// <inheritdoc />
    public InquiryTime ConvertFrom(DateTimeOffset dateTime)
    {
        var solar = new Solar(dateTime.LocalDateTime);
        var lunar = solar.Lunar;

        var lunarDt = new DateTimeOffset(lunar.Year, lunar.Month, lunar.Day, lunar.Hour, lunar.Minute, lunar.Second, dateTime.Offset);
        
        var stemBranch = new LunarStemBranch(
            new StemBranch(HeavenlyStem.FromValue((byte)(lunar.YearGanIndex + 1)),
                EarthlyBranch.FromValue((byte)(lunar.YearZhiIndex + 1))),
            new StemBranch(HeavenlyStem.FromValue((byte)(lunar.MonthGanIndex + 1)),
                EarthlyBranch.FromValue((byte)(lunar.MonthZhiIndex + 1))),
            new StemBranch(HeavenlyStem.FromValue((byte)(lunar.DayGanIndex + 1)),
                EarthlyBranch.FromValue((byte)(lunar.DayZhiIndex + 1))),
            new StemBranch(HeavenlyStem.FromValue((byte)(lunar.TimeGanIndex + 1)), 
                EarthlyBranch.FromValue((byte)(lunar.TimeZhiIndex+ 1)))
        );

        return new InquiryTime(dateTime, lunarDt, stemBranch);
    }
}