using System.Collections.ObjectModel;
using IChingLibrary.SixLines.Builder;

namespace IChingLibrary.SixLines;

/// <summary>
/// 神煞集合（Symbolic Stars Collection，计算结果）
/// </summary>
public sealed class SymbolicStarCollection
{
    private readonly Dictionary<SymbolicStar, EarthlyBranch[]> _symbolicStars;

    /// <summary>
    /// 初始化神煞集合
    /// </summary>
    /// <param name="symbolicStars">神煞类型到地支数组的映射</param>
    internal SymbolicStarCollection(Dictionary<SymbolicStar, EarthlyBranch[]> symbolicStars)
    {
        _symbolicStars = symbolicStars;
    }

    /// <summary>
    /// 获取指定神煞对应的所有地支
    /// </summary>
    /// <param name="symbolicStar">神煞类型</param>
    /// <returns>对应的地支数组，如果神煞不存在返回 null</returns>
    public EarthlyBranch[]? GetStars(SymbolicStar symbolicStar)
    {
        return _symbolicStars.TryGetValue(symbolicStar, out var branches)
            ? branches.ToArray()
            : null;
    }

    /// <summary>
    /// 检查指定地支是否具有某个神煞
    /// </summary>
    /// <param name="branch">地支</param>
    /// <param name="symbolicStar">神煞类型</param>
    /// <returns>如果该地支具有此神煞返回 true，否则返回 false</returns>
    public bool HasStar(EarthlyBranch branch, SymbolicStar symbolicStar)
    {
        return _symbolicStars.TryGetValue(symbolicStar, out var starBranches)
               && starBranches.Contains(branch);
    }

    /// <summary>
    /// 获取指定地支上的所有神煞
    /// </summary>
    /// <param name="branch">地支</param>
    /// <returns>该地支上的所有神煞类型</returns>
    public IEnumerable<SymbolicStar> GetStarsForBranch(EarthlyBranch branch)
    {
        return _symbolicStars
            .Where(kvp => kvp.Value.Contains(branch))
            .Select(kvp => kvp.Key);
    }

    /// <summary>
    /// 获取所有神煞
    /// </summary>
    public IReadOnlyDictionary<SymbolicStar, EarthlyBranch[]> AllStars =>
        new ReadOnlyDictionary<SymbolicStar, EarthlyBranch[]>(
            _symbolicStars.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToArray()));

    /// <summary>
    /// 添加神煞
    /// </summary>
    /// <param name="symbolicStar">可以通过<see cref="SymbolicStar.CreateCustom"/>方法添加自定义神煞</param>
    /// <param name="symbolicStarCalculatorDelegate">计算神煞所临地支</param>
    /// <returns>添加成功与否</returns>
    public bool Add(SymbolicStar symbolicStar, Func<EarthlyBranch[]> symbolicStarCalculatorDelegate) =>
        _symbolicStars.TryAdd(symbolicStar, symbolicStarCalculatorDelegate());
    
    /// <summary>
    /// 移除指定神煞
    /// </summary>
    /// <param name="symbolicStar"></param>
    /// <returns></returns>
    public bool Remove(SymbolicStar symbolicStar) => _symbolicStars.Remove(symbolicStar);
}
