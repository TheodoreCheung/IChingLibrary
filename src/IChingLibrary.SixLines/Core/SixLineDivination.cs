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

        var fourSymbols = new FourSymbol[6];
        for (var i = 0; i < fourSymbols.Length; i++)
        {
            fourSymbols[i] = FourSymbol.FromValue(fourSymbolValues[i]);
        }
        
        return CreateBuilder()
            .UseMethod(new CoinCastingMethod(castingTime, fourSymbols))
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

    private static string FormatEarthlyBranches(ReadOnlyMemory<EarthlyBranch> branches)
    {
        var span = branches.Span;
        if (span.Length == 0)
        {
            return "_";
        }

        var branchNames = new string[span.Length];
        for (var i = 0; i < span.Length; i++)
        {
            branchNames[i] = span[i].ToString();
        }

        return string.Join("、", branchNames);
    }
    
    /// <inheritdoc />
    public override string ToString()
    {
        var sb = new StringBuilder();

        const string tName = nameof(SixLineDivination);
        var tTo = IChingTranslationManager.GetTranslation(tName, "To");
        var tHexagram = IChingTranslationManager.GetTranslation(tName, "Hexagram");
        var tCastingTime = IChingTranslationManager.GetTranslation(tName, "CastingTime");
        var tGregorianCalendar = IChingTranslationManager.GetTranslation(tName, "GregorianCalendar");
        var tDateFormat = IChingTranslationManager.GetTranslation(tName, "DateFormat");
        var tLunarStemBranch = IChingTranslationManager.GetTranslation(tName, "LunarStemBranch");
        var tDayEmptiness = IChingTranslationManager.GetTranslation(tName, "DayEmptiness");
        var tOriginalHexagram = IChingTranslationManager.GetTranslation(tName, "OriginalHexagram");
        var tHexagramName = IChingTranslationManager.GetTranslation(tName, "HexagramName");
        var tHexagramPalace = IChingTranslationManager.GetTranslation(tName, "HexagramPalace");
        var tPalace = IChingTranslationManager.GetTranslation(tName, "Palace");
        var tPalaceFivePhases = IChingTranslationManager.GetTranslation(tName, "PalaceFivePhases");
        var tLinePosition = IChingTranslationManager.GetTranslation(tName, "LinePosition");
        var tStemBranch = IChingTranslationManager.GetTranslation(tName, "StemBranch");
        var tSixKin = IChingTranslationManager.GetTranslation(tName, "SixKin");
        var tFourSymbols = IChingTranslationManager.GetTranslation(tName, "FourSymbols");
        var tSixSpirits = IChingTranslationManager.GetTranslation(tName, "SixSpirits");
        var tPosition = IChingTranslationManager.GetTranslation(tName, "Position");
        var tHiddenDeity = IChingTranslationManager.GetTranslation(tName, "HiddenDeity");
        var tHiddenDeityStemBranch = IChingTranslationManager.GetTranslation(tName, "HiddenDeityStemBranch");
        var tChangedHexagram = IChingTranslationManager.GetTranslation(tName, "ChangedHexagram");
        var tSymbolicStar = IChingTranslationManager.GetTranslation(tName, "SymbolicStar");
        var tSymbolicStarName = IChingTranslationManager.GetTranslation(tName, "SymbolicStarName");
        var tSymbolicStarBranch = IChingTranslationManager.GetTranslation(tName, "SymbolicStarBranch");
        var dayEmptiness = FormatEarthlyBranches(CastingTime.StemBranch.Day.EmptyBranchesMemory);
        
        sb.AppendLine($"# {Original.Meta}{(Changed is null ? "" : $" {tTo} {Changed.Meta}")} {tHexagram}\n");
        
        sb.AppendLine($"## {tCastingTime}");
        sb.AppendLine($"**{tGregorianCalendar}**: _{CastingTime.Solar.ToString(tDateFormat)}_  ");
        sb.AppendLine($"**{tLunarStemBranch}**: _{CastingTime.StemBranch}_  ");
        sb.AppendLine($"**{tDayEmptiness}**: _{dayEmptiness}_  \n");

        sb.AppendLine($"## {tOriginalHexagram}");
        var originalNature = Original.Meta.GetNature();
        sb.AppendLine($"**{tHexagramName}**: _{Original.Meta}{(originalNature is null ? "" : $"（{originalNature}{tHexagram}）")}_  ");
        sb.AppendLine($"**{tHexagramPalace}**: _{Original.Meta.Palace}{tPalace}_  ");
        sb.AppendLine($"**{tPalaceFivePhases}**: _{Original.Meta.Palace.FivePhase}_  \n");

        sb.AppendLine($"|{tLinePosition}|{tStemBranch}|{tSixKin}|{tFourSymbols}|{tSixSpirits}|{tPosition}|{tHiddenDeity}|{tHiddenDeityStemBranch}|");
        sb.AppendLine("|---|---|---|---|---|---|---|---|");
        for (var i = 5; i >= 0; i--)
        {
            var line = Original[i];
            var stemBranchText = line.TryGetStemBranch(out var lineStemBranch) ? lineStemBranch!.ToString() : "_";
            var sixKinText = line.TryGetSixKin(out var lineSixKin) ? lineSixKin!.ToString() : "_";

            sb.Append($"|{line.LinePosition}|{stemBranchText}|{sixKinText}|{line.FourSymbol}|{line.SixSpirit?.ToString() ?? "_"}|");
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
            sb.AppendLine($"\n## {tChangedHexagram}");
            var changedNature = Changed.Meta.GetNature();
            sb.AppendLine(
                $"**{tHexagramName}**: _{Changed.Meta}{(changedNature is null ? "" : $"（{changedNature}{tHexagram}）")}_  ");
            sb.AppendLine(
                $"**{tHexagramPalace}**: _{Changed.Meta.Palace}{tPalace}_  ");
            sb.AppendLine(
                $"**{tPalaceFivePhases}**: _{Changed.Meta.Palace.FivePhase}_  \n");

            sb.AppendLine(
                $"|{tLinePosition}|{tStemBranch}|{tSixKin}|");
            sb.AppendLine("|---|---|---|");
            for (var i = 5; i >= 0; i--)
            {
                var line = Changed[i];
                var stemBranchText = line.TryGetStemBranch(out var lineStemBranch) ? lineStemBranch!.ToString() : "_";
                var sixKinText = line.TryGetSixKin(out var lineSixKin) ? lineSixKin!.ToString() : "_";
                sb.AppendLine($"|{line.LinePosition}|{stemBranchText}|{sixKinText}|");
            }
        }

        if (SymbolicStars is null)
            return sb.ToString();
        
        sb.AppendLine($"\n## {tSymbolicStar}");
        sb.AppendLine($"|{tSymbolicStarName}|{tSymbolicStarBranch}|");
        sb.AppendLine("|---|---|");
        
        foreach (var symbolicStar in SymbolicStars.AllStarsMemory)
        {
            sb.AppendLine($"|{symbolicStar.Key}|{FormatEarthlyBranches(symbolicStar.Value)}|");
        }
        
        return sb.ToString();
    }
}
