# README 同步设计（Builder 重构）

日期：2026-03-13

## 背景
Builder 体系完成重构，README 中“项目结构、构建器 API、流程与架构”仍引用旧的 Builders/Providers/BuilderContext 等内容，需要整体同步。

## 目标
- 以当前代码为唯一真源，修正 README 中所有受 Builder 影响的描述。
- 更新项目结构、使用示例、API 参考、构建流程与架构图。
- 统一术语与命名（`InquiryTime` → `CastingTime` 等）。

## 非目标
- 不调整代码实现，仅更新文档。
- 不新增与当前代码不一致的功能说明。

## 变更范围
- 项目结构树：替换为 `Builder/` + `Core/` 的新目录与类说明。
- 使用示例：
  - 维持 `SixLineDivination.Create(...)` 示例并同步说明。
  - 构建器示例改为 `CreateBuilder()` + `UseMethod(...)` + `WithStep(...)`/`WithDefaultSteps()`。
  - 移除 Provider/IBuildStep 示例。
- API 参考：
  - `SixLineDivinationBuilder` 改为 `UseMethod/WithStep/WithDefaultSteps/Build`。
  - 新增 `ICastingMethod`、`IStructuringStep`、`DivinationContext` 说明。
  - 删除 Provider/BuilderContext/IBuildStep 章节。
- 构建流程：改为“起卦方式 Cast → Context → 步骤拓扑排序 → 执行”。
- 架构设计：替换为 ICastingMethod/IStructuringStep/DivinationContext 体系。
- 神煞/变卦说明：基于实际实现修正。

## 关键决策
- `ICastingMethod` 与 `IStructuringStep` 为扩展点，但受内部可见性限制，README 中明确为“库内扩展为主”。
- 自定义神煞与步骤示例仅给出说明，不展示依赖内部构造器的外部代码示例。

## 验收标准
- README 不再出现 Builders/Providers/BuilderContext/IBuildStep 等旧结构。
- 所有示例与 API 描述可与当前代码对齐。
- 构建流程与架构图符合新 Builder 设计。
