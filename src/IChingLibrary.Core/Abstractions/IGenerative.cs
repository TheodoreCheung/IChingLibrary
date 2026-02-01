namespace IChingLibrary.Core.Abstractions;

/// <summary>
/// 定义相生关系的接口
/// </summary>
/// <typeparam name="TElement">易学元素类型</typeparam>
public interface IGenerative<in TElement> where TElement : IChingElement<TElement>
{
    /// <summary>
    /// 判断是否与另一个元素存在相生关系（双向检查）
    /// </summary>
    bool IsGenerates(TElement other);

    /// <summary>
    /// 判断是否生另一个元素（当前 → 其他）
    /// </summary>
    bool Generates(TElement other);

    /// <summary>
    /// 判断是否被另一个元素生（其他 → 当前）
    /// </summary>
    bool GeneratesBy(TElement other);
}