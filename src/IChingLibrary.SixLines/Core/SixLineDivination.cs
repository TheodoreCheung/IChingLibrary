using System.Text;
using IChingLibrary.Core.Localization;
using IChingLibrary.SixLines.Builder;

namespace IChingLibrary.SixLines;

/// <summary>
/// 六爻占卜，包含起卦时间和卦象信息
/// </summary>
public class SixLineDivination
{
    /// <summary>
    /// 起卦时间
    /// </summary>
    public CastingTime CastingTime { get; }

    /// <summary>
    /// 主卦
    /// </summary>
    public HexagramInstance Original { get; }

    /// <summary>
    /// 变卦
    /// </summary>
    public HexagramInstance? Changed { get; }

    /// <summary>
    /// 神煞集合
    /// </summary>
    public SymbolicStarCollection? SymbolicStars { get; internal set; }

    internal SixLineDivination(CastingTime castingTime, HexagramInstance original, HexagramInstance? changed = null)
    {
        CastingTime = castingTime;
        Original = original;
        Changed = changed;
    }
    
    public static ISixLineDivinationBuilder CreateBuilder()
    {
        return new SixLineDivinationBuilder();
    }

    /// <summary>
    /// 创建六爻占卜（使用默认完整流程：纳甲+世应+六亲+六神）
    /// </summary>
    /// <param name="castingTime">起卦时间</param>
    /// <param name="fourSymbols">六个四象值</param>
    /// <returns>六爻占卜实例</returns>
    public static SixLineDivination Create(DateTimeOffset castingTime, FourSymbol[] fourSymbols)
    {
        if (fourSymbols.Length != 6)
            throw new InvalidOperationException($"Invalid number of four symbol values. Expected 6, got {fourSymbols.Length}");
        
        return CreateBuilder()
            .UseMethod(new CoinCastingMethod(castingTime, fourSymbols))
            .WithDefaultSteps()
            .Build();
    }
    
    /// <summary>
    /// 创建六爻占卜（byte[] 版本，使用默认完整流程）
    /// </summary>
    /// <param name="castingTime">起卦时间</param>
    /// <param name="fourSymbolValues">六个四象值</param>
    /// <returns>六爻占卜实例</returns>
    public static SixLineDivination Create(DateTimeOffset castingTime, byte[] fourSymbolValues)
    {
        if (fourSymbolValues.Length != 6)
            throw new InvalidOperationException($"Invalid number of four symbol values. Expected 6, got {fourSymbolValues.Length}");
        
        return CreateBuilder()
            .UseMethod(new CoinCastingMethod(castingTime, fourSymbolValues.Select(FourSymbol.FromValue).ToArray()))
            .WithDefaultSteps()
            .Build();
    }

    /// <summary>
    /// 时间起卦法（根据年月日时自动起卦）
    /// </summary>
    /// <param name="castingTime">起卦时间</param>
    /// <returns>六爻占卜实例</returns>
    public static SixLineDivination Create(DateTimeOffset castingTime)
    {
        return CreateBuilder()
            .UseMethod(new TimeBasedCastingMethod(castingTime))
            .WithDefaultSteps()
            .Build();
    }

    /// <summary>
    /// 随机数起卦
    /// </summary>
    /// <param name="castingTime">起卦时间</param>
    /// <param name="upperTrigramNumber">上卦随机数</param>
    /// <param name="lowerTrigramNumber">下卦随机数</param>
    /// <param name="changingLineNumber">动爻随机数（可选）</param>
    /// <returns>六爻占卜实例</returns>
    public static SixLineDivination Create(
        DateTimeOffset castingTime,
        int upperTrigramNumber,
        int lowerTrigramNumber,
        int? changingLineNumber = null)
    {
        return CreateBuilder()
            .UseMethod(new NumberBasedCastingMethod(castingTime, upperTrigramNumber, lowerTrigramNumber, changingLineNumber))
            .WithDefaultSteps()
            .Build();
    }

    /// <summary>
    /// 指定主卦和变卦起卦
    /// </summary>
    /// <param name="castingTime">起卦时间</param>
    /// <param name="original">主卦</param>
    /// <param name="changed">变卦（可选）</param>
    /// <returns>六爻占卜实例</returns>
    public static SixLineDivination Create(
        DateTimeOffset castingTime,
        Hexagram original,
        Hexagram? changed = null)
    {
        return CreateBuilder()
            .UseMethod(new SpecifyingHexagramCastingMethod(castingTime, original, changed))
            .WithDefaultSteps()
            .Build();
    }

    /// <summary>
    /// 指定主卦值和变卦值起卦
    /// </summary>
    /// <param name="castingTime">起卦时间</param>
    /// <param name="originalValue">主卦值</param>
    /// <param name="changedValue">变卦值（可选）</param>
    /// <returns>六爻占卜实例</returns>
    public static SixLineDivination Create(
        DateTimeOffset castingTime,
        byte originalValue,
        byte? changedValue = null)
    {
        return CreateBuilder()
            .UseMethod(new SpecifyingHexagramCastingMethod(castingTime, Hexagram.FromValue(originalValue), changedValue is null ? null : Hexagram.FromValue(changedValue.Value)))
            .WithDefaultSteps()
            .Build();
    }

    private static string SafeString(Func<object?> getter)
    {
        try
        {
            return getter()?.ToString() ?? "_";
        }
        catch (InvalidOperationException)
        {
            return "_";
        }
    }
    
    /// <inheritdoc />
    public override string ToString()
    {
        var sb = new StringBuilder();

        const string tName = nameof(SixLineDivination);
        
        sb.AppendLine($"# {Original.Meta}{(Changed is null ? "" : $" {IChingTranslationManager.GetTranslation(tName, "To")} {Changed.Meta}")} {IChingTranslationManager.GetTranslation(tName, "Hexagram")}\n");
        
        sb.AppendLine($"## {IChingTranslationManager.GetTranslation(tName, "CastingTime")}");
        sb.AppendLine($"**{IChingTranslationManager.GetTranslation(tName, "GregorianCalendar")}**: _{CastingTime.Solar.ToString(IChingTranslationManager.GetTranslation(tName, "DateFormat"))}_  ");
        sb.AppendLine($"**{IChingTranslationManager.GetTranslation(tName, "LunarStemBranch")}**: _{CastingTime.StemBranch}_  ");
        sb.AppendLine($"**{IChingTranslationManager.GetTranslation(tName, "DayEmptiness")}**: _{string.Join("、", CastingTime.StemBranch.Day.EmptyBranches.Select(b => b.ToString()))}_  \n");

        sb.AppendLine($"## {IChingTranslationManager.GetTranslation(tName, "OriginalHexagram")}");
        var originalNature = Original.Meta.GetNature();
        sb.AppendLine($"**{IChingTranslationManager.GetTranslation(tName, "HexagramName")}**: _{Original.Meta}{(originalNature is null ? "" : $"（{originalNature}{IChingTranslationManager.GetTranslation(tName, "Hexagram")}）")}_  ");
        sb.AppendLine($"**{IChingTranslationManager.GetTranslation(tName, "HexagramPalace")}**: _{Original.Meta.Palace}{IChingTranslationManager.GetTranslation(tName, "Palace")}_  ");
        sb.AppendLine($"**{IChingTranslationManager.GetTranslation(tName, "PalaceFivePhases")}**: _{Original.Meta.Palace.FivePhase}_  \n");

        sb.AppendLine($"|{IChingTranslationManager.GetTranslation(tName, "LinePosition")}|{IChingTranslationManager.GetTranslation(tName, "StemBranch")}|{IChingTranslationManager.GetTranslation(tName, "SixKin")}|{IChingTranslationManager.GetTranslation(tName, "FourSymbols")}|{IChingTranslationManager.GetTranslation(tName, "SixSpirits")}|{IChingTranslationManager.GetTranslation(tName, "Position")}|{IChingTranslationManager.GetTranslation(tName, "HiddenDeity")}|{IChingTranslationManager.GetTranslation(tName, "HiddenDeityStemBranch")}|");
        sb.AppendLine("|---|---|---|---|---|---|---|---|");
        for (var i = 5; i >= 0; i--)
        {
            var line = Original[i];

            sb.Append($"|{line.LinePosition}|{SafeString(() => line.StemBranch)}|{SafeString(() => line.SixKin)}|{line.FourSymbol}|{line.SixSpirit?.ToString() ?? "_"}|");
            if (line.Position is not null)
            {
                sb.Append($"{line.Position}|");
            }
            else
            {
                sb.Append("_|");
            }
            
            var hiddenDeity = line.HiddenDeity;
            if (hiddenDeity.HasValue)
            {
                sb.Append($"{hiddenDeity.Value.SixKin}|{hiddenDeity.Value.StemBranch}|  \n");
            }
            else
            {
                sb.Append("_|_|  \n");
            }
        }

        if (Changed is not null)
        {
            sb.AppendLine($"\n## {IChingTranslationManager.GetTranslation(tName, "ChangedHexagram")}");
            var changedNature = Changed.Meta.GetNature();
            sb.AppendLine(
                $"**{IChingTranslationManager.GetTranslation(tName, "HexagramName")}**: _{Changed.Meta}{(changedNature is null ? "" : $"（{changedNature}{IChingTranslationManager.GetTranslation(tName, "Hexagram")}）")}_  ");
            sb.AppendLine(
                $"**{IChingTranslationManager.GetTranslation(tName, "HexagramPalace")}**: _{Changed.Meta.Palace}{IChingTranslationManager.GetTranslation(tName, "Palace")}_  ");
            sb.AppendLine(
                $"**{IChingTranslationManager.GetTranslation(tName, "PalaceFivePhases")}**: _{Changed.Meta.Palace.FivePhase}_  \n");

            sb.AppendLine(
                $"|{IChingTranslationManager.GetTranslation(tName, "LinePosition")}|{IChingTranslationManager.GetTranslation(tName, "StemBranch")}|{IChingTranslationManager.GetTranslation(tName, "SixKin")}|");
            sb.AppendLine("|---|---|---|");
            for (var i = 5; i >= 0; i--)
            {
                sb.AppendLine($"|{Changed[i].LinePosition}|{SafeString(() => Changed[i].StemBranch)}|{SafeString(() => Changed[i].SixKin)}|");
            }
        }

        if (SymbolicStars is null)
            return sb.ToString();
        
        sb.AppendLine($"\n## {IChingTranslationManager.GetTranslation(tName, "SymbolicStar")}");
        sb.AppendLine($"|{IChingTranslationManager.GetTranslation(tName, "SymbolicStarName")}|{IChingTranslationManager.GetTranslation(tName, "SymbolicStarBranch")}|");
        sb.AppendLine("|---|---|");
        
        foreach (var symbolicStar in SymbolicStars.AllStars)
        {
            sb.AppendLine($"|{symbolicStar.Key}|{string.Join("、", symbolicStar.Value)}|");
        }
        
        return sb.ToString();
    }
}
