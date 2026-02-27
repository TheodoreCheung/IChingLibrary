namespace IChingLibrary.SixLines;

/// <summary>
/// 构建器上下文
/// </summary>
/// <param name="solarInquiryTime">起卦时间</param>
public sealed class BuilderContext(DateTimeOffset solarInquiryTime)
{
    /// <summary>
    /// 公历起卦时间
    /// </summary>
    public DateTimeOffset SolarInquiryTime { get; } = solarInquiryTime;
    
    /// <summary>
    /// 起卦时间
    /// </summary>
    public InquiryTime? InquiryTime { get; set; }
    
    /// <summary>
    /// 起卦六个爻的四象（从初爻到上爻）
    /// </summary>
    public FourSymbol[]? FourSymbols { get; set; }
    
    /// <summary>
    /// 主卦实例
    /// </summary>
    public HexagramInstance? Original { get; set; }

    /// <summary>
    /// 变卦实例（处理变卦步骤时有值）
    /// </summary>
    public HexagramInstance? Changed { get; set; }
    
    /// <summary>
    /// 神煞
    /// </summary>
    public SymbolicStarCollection? SymbolicStars { get; set; }
}