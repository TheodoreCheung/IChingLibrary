namespace IChingLibrary.Core.Abstractions;

/// <summary>
/// 定义通用关系的接口（包括冲、合、三合等）
/// </summary>
/// <typeparam name="TElement">易学元素类型</typeparam>
public interface IRelationship<in TElement> where TElement : IChingElement<TElement>
{
    /// <summary>
    /// 判断是否与另一个元素相冲
    /// </summary>
    bool IsClashing(TElement other);

    /// <summary>
    /// 判断是否与另一个元素相合
    /// </summary>
    bool IsCombining(TElement other);

    /// <summary>
    /// 判断是否与另外两个元素形成三合局
    /// </summary>
    bool IsTriangularCombination(TElement other, TElement another);
}