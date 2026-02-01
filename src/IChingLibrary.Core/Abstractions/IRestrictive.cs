namespace IChingLibrary.Core.Abstractions;

/// <summary>
/// 定义相克关系的接口
/// </summary>
/// <typeparam name="TElement">易学元素类型</typeparam>
public interface IRestrictive<in TElement> where TElement : IChingElement<TElement>
{
    /// <summary>
    /// 判断是否与另一个元素存在相克关系（双向检查）
    /// </summary>
    bool IsRestrains(TElement other);

    /// <summary>
    /// 判断是否克另一个元素（当前 → 其他）
    /// </summary>
    bool Restrains(TElement other);

    /// <summary>
    /// 判断是否被另一个元素克（其他 → 当前）
    /// </summary>
    bool RestrainsBy(TElement other);
}