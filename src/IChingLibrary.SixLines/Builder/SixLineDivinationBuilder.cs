namespace IChingLibrary.SixLines.Builder;

/// <summary>
/// 六爻起卦构建器
/// </summary>
public sealed class SixLineDivinationBuilder : ISixLineDivinationBuilder
{
    private static readonly IStructuringStep[] DefaultSteps =
    [
        new NajiaStep(),
        new PositionStep(),
        new SixKinStep(),
        new SixSpiritStep(),
        new HiddenDeityStep(),
        new SymbolicStarStep()
    ];

    private ICastingMethod? _castingMethod;
    
    private readonly List<IStructuringStep> _steps = [];
    private readonly HashSet<Type> _stepTypes = [];
    private bool _requiresSorting;

    /// <inheritdoc />
    public ISixLineDivinationBuilder UseMethod(ICastingMethod castingMethod)
    {
        _castingMethod = castingMethod;
        return this;
    }

    /// <inheritdoc />
    public ISixLineDivinationBuilder WithDefaultSteps()
    {
        var hadExistingSteps = _steps.Count > 0;
        foreach (var step in DefaultSteps)
        {
            AddStep(step, keepOrder: true);
        }

        if (hadExistingSteps)
        {
            _requiresSorting = true;
        }

        return this;
    }

    /// <inheritdoc />
    public ISixLineDivinationBuilder WithStep(IStructuringStep structuringStep)
    {
        AddStep(structuringStep, keepOrder: false);
        return this;
    }

    /// <inheritdoc />
    public SixLineDivination Build()
    {
        if (_castingMethod == null)
            throw new InvalidOperationException($"起卦方式{nameof(ICastingMethod)}尚未指定。");

        // 1. 执行起卦获得种子数据（包含干支时间、原始爻象）
        var seed = _castingMethod.Cast();

        // 2. 创建上下文
        var context = new DivinationContext(seed);

        // 3. 拓扑排序（解决乱序添加问题）
        var sortedSteps = _requiresSorting ? SortSteps() : _steps;

        // 4. 依次执行
        foreach (var step in sortedSteps)
        {
            step.Execute(context);
        }

        return context.SixLineDivination;
    }

    /// <summary>
    /// 对结构化步骤进行拓扑排序
    /// </summary>
    /// <returns>排序后的步骤列表</returns>
    private List<IStructuringStep> SortSteps()
    {
        // 将步骤转换为字典，便于通过类型快速查找
        var stepDict = _steps.ToDictionary(s => s.GetType());
        // 用于记录已访问的类型，防止重复处理和循环依赖
        var visited = new HashSet<Type>();
        // 用于记录当前递归路径上的类型，检测循环依赖
        var visiting = new HashSet<Type>();
        // 存储排序后的结果列表
        var sorted = new List<IStructuringStep>();

        // 遍历所有步骤，对每个步骤进行访问
        foreach (var step in _steps)
        {
            Visit(step);
        }

        return sorted;

        // 深度优先搜索 (DFS) 实现拓扑排序
        void Visit(IStructuringStep step)
        {
            // 获取当前步骤的类型
            var stepType = step.GetType();
            // 检查该步骤类型是否已经被访问过，如果是则直接返回
            if (visited.Contains(stepType)) return;
            if (!visiting.Add(stepType))
            {
                throw new InvalidOperationException($"检测到循环依赖步骤: {stepType}");
            }

            // 遍历当前步骤所需的所有依赖步骤类型
            foreach (var depType in step.RequiredSteps)
            {
                // 尝试从步骤字典中获取依赖步骤
                if (stepDict.TryGetValue(depType, out var depStep))
                {
                    // 递归访问依赖步骤
                    Visit(depStep);
                }
                else
                {
                    throw new InvalidOperationException($"缺少必要的依赖步骤: {depType}");
                }
            }

            visiting.Remove(stepType);
            visited.Add(stepType);
            sorted.Add(step);
        }
    }

    private void AddStep(IStructuringStep structuringStep, bool keepOrder)
    {
        if (!_stepTypes.Add(structuringStep.GetType()))
        {
            return;
        }

        _steps.Add(structuringStep);
        if (!keepOrder)
        {
            _requiresSorting = true;
        }
    }
}
