namespace IChingLibrary.SixLines.Providers.Abstractions;

/// <summary>
/// 起卦时间信息转换器接口
/// </summary>
public interface IInquiryTimeProvider
{
    /// <summary>
    /// 将日期时间转换为起卦时间信息
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>起卦时间信息</returns>
    InquiryTime ConvertFrom(DateTimeOffset dateTime);
}