namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 六爻结构化步骤接口
/// </summary>
public interface IStructuringStep
{
    /// <summary>
    /// 当前步骤依赖的前置步骤类型集合
    /// </summary>
    /// <value>前置步骤的类型列表</value>
    IEnumerable<Type> RequiredSteps { get; }
    
    /// <summary>
    /// 执行结构化步骤
    /// </summary>
    /// <param name="context">起卦上下文</param>
    void Execute(DivinationContext context);
}
