# IChingLibrary

一个使用现代 C# (.NET 10) 构建的易学库，提供周易六爻占卜的完整实现。

## 特性

- **多语言国际化支持**：内置中英文翻译，支持运行时切换语言，不影响系统其他本地化
- **纯阳历转阴历转换**：使用 `lunar-csharp` 库实现精确的历法转换
- **京房纳甲法**：标准纳甲法实现，为八卦卦爻绑定天干地支
- **世应位置计算**：根据八宫卦自动计算世爻和应爻位置
- **六亲计算**：根据五行生克关系计算六亲（父母、兄弟、妻财、官鬼、子孙）
- **六神起法**：根据日干自动起六神（青龙、朱雀、勾陈、螣蛇、白虎、玄武）
- **神煞系统（Symbolic Stars）**：支持16 种神煞计算（贵人、禄神、文昌、驿马、桃花等），支持自定义神煞扩展
- **灵活的构建器模式**：可自定义占卜流程，选择需要的计算步骤
- **SOLID 设计原则**：清晰的接口抽象，易于扩展和测试

## 项目结构

```
IChingLibrary/
├── src/
│   ├── IChingLibrary.Core/              # 核心抽象和基础类
│   │   ├── IChingElement<T>              # 易学元素泛型基类
│   │   ├── YinYang                       # 阴阳
│   │   ├── FivePhase                     # 五行（金水木火土）
│   │   ├── FourSymbol                    # 四象（老阴、少阳、少阴、老阳）
│   │   ├── Trigram                       # 三爻（八卦）
│   │   ├── Hexagram                      # 六爻（六十四卦）
│   │   ├── HeavenlyStem                  # 天干
│   │   ├── EarthlyBranch                 # 地支
│   │   ├── StemBranch                    # 干支
│   │   ├── SixKin                        # 六亲
│   │   ├── SixSpirit                     # 六神
│   │   ├── Position                      # 世应位置
│   │   └── SymbolicStar                  # 神煞
│   │
│   ├── IChingLibrary.Generators/         # Roslyn 增量源代码生成器
│   │
│   └── IChingLibrary.SixLines/           # 六爻占卜实现
│       ├── Core/                         # 核心枚举类型
│       │   ├── LinePosition              # 爻位置（初爻到上爻）
│       │   ├── Position                  # 位置（世爻、应爻）
│       │   ├── SixKin                    # 六亲
│       │   ├── SixSpirit                 # 六神
│       │   └── SymbolicStar              # 神煞类型
│       ├── Extensions/                   # 扩展方法
│       │   └── HexagramInstanceExtensions # 卦实例扩展（卦身查找等）
│       ├── SixLineDivination             # 六爻占卜主类
│       ├── SymbolicStarCollection        # 神煞集合
│       ├── SymbolicStarSelection         # 神煞选择器
│       ├── HexagramGenerator             # 卦生成器（internal）
│       ├── HexagramInstance              # 卦实例
│       ├── Line                          # 爻
│       ├── InquiryTime                   # 问时信息
│       ├── Builders/                     # 构建器模式
│       │   ├── SixLineDivinationBuilder  # 构建器
│       │   ├── ISixLineStep              # 步骤接口
│       │   └── DefaultSteps              # 默认步骤实现
│       └── Providers/                    # Provider 模式
│           ├── Abstractions/             # 接口定义
│           │   ├── IInquiryTimeProvider
│           │   ├── INajiaProvider
│           │   ├── IPositionProvider
│           │   ├── ISixKinProvider
│           │   ├── ISixSpiritProvider
│           │   └── IHiddenDeityProvider
│           └── Default*.cs               # 默认实现
└── test/                                 # 测试项目（待实现）
```

## 快速开始

### 安装

```bash
# 克隆仓库
git clone https://github.com/your-repo/IChingLibrary.git
cd IChingLibrary

# 恢复 NuGet 包
dotnet restore
```

### 构建

```bash
# 构建整个解决方案
dotnet build IChingLibrary.slnx

# 构建特定项目
dotnet build src/IChingLibrary.SixLines/IChingLibrary.SixLines.csproj
```

## 使用示例

### 1. 时间起卦法（最简单）

根据年月日时自动起卦，无需手动指定四象：

```csharp
using IChingLibrary.SixLines;

// 根据当前时间自动起卦
var divination = SixLineDivination.Create(DateTimeOffset.Now);

Console.WriteLine($"主卦：{divination.Original.Meta.Label}");
Console.WriteLine($"变卦：{divination.Changed?.Meta.Label ?? "无"}");
```

起卦逻辑：
- **上卦** = (年地支值 + 阴历月份 + 阴历日) % 8
- **下卦** = (年地支值 + 阴历月份 + 阴历日 + 时地支值) % 8
- **动爻** = (年地支值 + 阴历月份 + 阴历日 + 时地支值) % 6

### 2. 随机数起卦法

使用随机数起卦：

```csharp
// 指定上卦和下卦随机数（系统会自动计算动爻）
var divination1 = SixLineDivination.Create(
    DateTimeOffset.Now,
    upperTrigramNumber: 15,
    lowerTrigramNumber: 23
);

// 指定上卦、下卦和动爻随机数
var divination2 = SixLineDivination.Create(
    DateTimeOffset.Now,
    upperTrigramNumber: 15,
    lowerTrigramNumber: 23,
    changingLineNumber: 7
);
```

### 3. 指定主卦和变卦起卦

直接指定主卦和变卦：

```csharp
using IChingLibrary.Core;

// 只指定主卦（无变卦）
var divination1 = SixLineDivination.Create(
    DateTimeOffset.Now,
    Hexagram.TheCreative  // 乾为天
);

// 指定主卦和变卦
var divination2 = SixLineDivination.Create(
    DateTimeOffset.Now,
    Hexagram.TheCreative,   // 主卦：乾为天
    Hexagram.TheReceptive   // 变卦：坤为地
);

// 使用卦值指定
var divination3 = SixLineDivination.Create(
    DateTimeOffset.Now,
    originalValue: 63,   // 乾为天（0b111111）
    changedValue: 0      // 坤为地（0b000000）
);
```

### 4. 使用四象数组（传统方式）

直接使用四象数组起卦：

```csharp
using IChingLibrary.Core;
using IChingLibrary.SixLines;

// 使用 FourSymbol[] 数组（C# 12 集合表达式）
// 注意：数组顺序必须从初爻到上爻
var divination1 = SixLineDivination.Create(
    DateTimeOffset.Now,
    [
        FourSymbol.YoungYang, FourSymbol.YoungYang, FourSymbol.YoungYang,
        FourSymbol.YoungYang, FourSymbol.YoungYang, FourSymbol.YoungYang
    ]
);

// 使用 byte[] 数组（6=老阴，7=少阳，8=少阴，9=老阳）
// 注意：数组顺序必须从初爻到上爻
var divination2 = SixLineDivination.Create(
    DateTimeOffset.Now,
    [9, 8, 7, 6, 8, 7]  // 索引0=初爻, 索引5=上爻
);

// 访问结果
Console.WriteLine($"主卦：{divination2.Original.Meta.Label}");
Console.WriteLine($"变卦：{divination2.Changed?.Meta.Label ?? "无"}");

// 遍历爻信息
foreach (var line in divination2.Original.Lines)
{
    Console.WriteLine($"{line.LinePosition.Label}: {line.YinYang.Label}, " +
                      $"{line.StemBranch}, {line.SixKin.Label}, " +
                      $"{line.SixSpirit?.Label ?? "无"}, {line.Position?.Label ?? "无"}");
}
```

### 5. 自定义流程（构建器模式）

使用构建器自由选择需要的步骤：

```csharp
// 时间起卦 + 自定义步骤组合
var divination = SixLineDivination
    .CreateBuilder(DateTimeOffset.Now)
    .UseTimeBasedHexagram()
    .WithNajia()           // 纳甲
    .WithPosition()        // 世应位置
    .WithSixKin()          // 六亲
    // 不需要六神
    .Build();

// 随机数起卦 + 只纳甲，不需要其他步骤
var divination = SixLineDivination
    .CreateBuilder(DateTimeOffset.Now)
    .UseRandomHexagram(15, 23)
    .WithNajia()
    .Build();

// 直接指定四象值 + 默认流程
var divination = SixLineDivination
    .CreateBuilder(DateTimeOffset.Now)
    .UseFourSymbols([9, 8, 7, 6, 8, 7])
    .WithDefaultSteps()
    .Build();

// 直接指定卦象 + 自定义流程
var divination = SixLineDivination
    .CreateBuilder(DateTimeOffset.Now)
    .UseHexagram(Hexagram.TheCreative, Hexagram.TheReceptive)
    .WithDefaultSteps()
    .Build();

// 只创建卦实例，不执行任何计算步骤
var divination = SixLineDivination
    .CreateBuilder(DateTimeOffset.Now)
    .UseFourSymbols([7, 7, 7, 7, 7, 7])
    .Build();
```

### 6. 使用自定义 Provider

实现 Provider 接口并注入到构建器中：

```csharp
using IChingLibrary.SixLines.Providers.Abstractions;

// 自定义纳甲法 Provider
public class MyCustomNajiaProvider : INajiaProvider
{
    public void BindStemBranches(HexagramInstance hexagram, InquiryTime inquiryTime)
    {
        // 自定义纳甲法实现
    }

    public StemBranch[] GetNajiaTable(Trigram trigram, bool isInner)
    {
        // 返回自定义纳甲表
    }
}

// 使用自定义 Provider
var divination = SixLineDivination
    .CreateBuilder(DateTimeOffset.Now)
    .UseTimeBasedHexagram()
    .WithNajia(new MyCustomNajiaProvider())
    .WithSixKin()
    .Build();
```

### 7. 完全自定义步骤

实现 `ISixLineStep` 接口创建自定义步骤：

```csharp
using IChingLibrary.SixLines.Builders;

// 自定义步骤
public class MyCustomStep : ISixLineStep
{
    public void Execute(HexagramInstance hexagram, InquiryTime inquiryTime, HexagramInstance? originalHexagram)
    {
        // 自定义处理逻辑
    }
}

// 使用自定义步骤
var divination = SixLineDivination
    .CreateBuilder(DateTimeOffset.Now)
    .UseTimeBasedHexagram()
    .WithNajia()
    .WithCustomStep(new MyCustomStep())
    .Build();
```

### 8. 变卦处理

变卦（有变爻的卦）会自动计算，但变卦只计算纳甲和六亲：

```csharp
// 老阳（9）和老阴（6）为变爻
var divination = SixLineDivination.Create(
    DateTimeOffset.Now,
    [FourSymbol.OldYang, FourSymbol.YoungYang, FourSymbol.YoungYang,
     FourSymbol.YoungYang, FourSymbol.YoungYang, FourSymbol.YoungYang]
);

if (divination.Changed != null)
{
    Console.WriteLine($"主卦：{divination.Original.Meta.Label}");
    Console.WriteLine($"变卦：{divination.Changed.Meta.Label}");

    // 变卦的六亲使用主卦的卦宫五行计算
    foreach (var line in divination.Changed.Lines)
    {
        Console.WriteLine($"{line.LinePosition.Label}: {line.SixKin.Label}");
    }
}
```

### 9. 神煞系统（Symbolic Stars）

神煞系统提供 16 种预定义神煞的计算，支持自定义扩展：

```csharp
using IChingLibrary.Core;
using IChingLibrary.SixLines;

// 默认流程已包含神煞计算
var divination = SixLineDivination.Create(DateTimeOffset.Now);

// 查询卦身
Console.WriteLine($"卦身：{divination.SymbolicStars?.HexagramBody?.Label ?? "无"}");

// 查询贵人（多值神煞）
var nobleman = divination.SymbolicStars?.GetStars(SymbolicStar.Nobleman);
Console.WriteLine($"贵人：{string.Join("、", nobleman?.Select(b => b.Label) ?? [])}");

// 查询禄神（单值神煞）
var salarySpirit = divination.SymbolicStars?.GetStars(SymbolicStar.SalarySpirit);
Console.WriteLine($"禄神：{salarySpirit?[0].Label ?? "无"}");

// 检查某个爻上的神煞
var line = divination.Original.Lines[0];
var lineStars = divination.SymbolicStars?.GetStarsForLine(line) ?? [];
Console.WriteLine($"{line.LinePosition.Label}神煞：{string.Join("、", lineStars.Select(s => s.Label))}");

// 检查某个地支上的所有神煞
var branch = EarthlyBranch.Zi;
var branchStars = divination.SymbolicStars?.GetStarsForBranch(branch) ?? [];
Console.WriteLine($"子神煞：{string.Join("、", branchStars.Select(s => s.Label))}");
```

#### 自定义神煞集合

```csharp
// 移除某些神煞
var divination = SixLineDivination
    .CreateBuilder(DateTimeOffset.Now)
    .WithDefaultSteps()
    .WithSymbolicStars(selection =>
    {
        selection.Remove(SymbolicStar.YangBlade);        // 移除羊刃
        selection.Remove(SymbolicStar.RobberyMalignity); // 移除劫煞
        selection.Remove(SymbolicStar.DeathSpirit);      // 移除亡神
    })
    .Build();

// 只计算特定神煞
var divination = SixLineDivination
    .CreateBuilder(DateTimeOffset.Now)
    .WithDefaultSteps()
    .WithSymbolicStars(selection =>
    {
        selection.Clear();
        selection.Add(SymbolicStar.Nobleman);
        selection.Add(SymbolicStar.SalarySpirit);
        selection.Add(SymbolicStar.PeachBlossom);
    })
    .Build();
```

#### 添加自定义神煞类型

```csharp
using IChingLibrary.Core;
using IChingLibrary.SixLines;
using IChingLibrary.SixLines.Providers;

// 创建自定义神煞类型
var myCustomStar = SymbolicStar.CreateCustom("MyCustomStar");

// 自定义神煞计算委托
EarthlyBranch[]? CalculateMyCustomStar(InquiryTime inquiryTime, HexagramInstance hexagram)
{
    // 示例：根据日支计算自定义神煞
    var dayBranch = inquiryTime.StemBranch.Day.Branch;
    // 返回包含该神煞的地支数组，或 null 表示不适用
    return [dayBranch];
}

// 使用 WithSymbolicStars 配置自定义神煞
var divination = SixLineDivination
    .CreateBuilder(DateTimeOffset.Now)
    .WithDefaultSteps()
    .WithSymbolicStars(provider =>
    {
        // 移除某些默认神煞（可选）
        provider.Remove(SymbolicStar.YangBlade);
        provider.Remove(SymbolicStar.RobberyMalignity);

        // 添加自定义神煞计算器
        provider.Add(myCustomStar, CalculateMyCustomStar);
    })
    .Build();
```

**说明**：
- `DefaultSymbolicStarProvider` 使用委托模式管理神煞计算器
- 通过 `provider.Add(SymbolicStar, SymbolicStarCalculatorDelegate)` 添加自定义神煞
- 通过 `provider.Remove(SymbolicStar)` 移除不需要的神煞
- 默认神煞计算器已自动注册，只需添加或覆盖需要的计算器

#### 预定义神煞类型

| 中文名 | 英文名 | 计算依据 |
|--------|--------|----------|
| 贵人 | Nobleman | 日干（甲戊→牛羊，乙己→鼠猴，丙丁→猪鸡，壬癸→兔蛇，庚辛→马虎） |
| 禄神 | SalarySpirit | 日干（甲→寅，乙→卯，丙戊→巳，丁己→午，庚→申，辛→酉，壬→亥，癸→子） |
| 文昌 | CultureFlourish | 日干（甲→巳，乙→午，丙戊→申，丁己→酉，庚→亥，辛→子，壬→寅，癸→卯） |
| 驿马 | PostHorse | 日支（寅午戌→申，亥卯未→巳，巳酉丑→亥，申子辰→寅） |
| 桃花 | PeachBlossom | 日支（寅午戌→卯，亥卯未→辰，巳酉丑→午，申子辰→酉） |
| 羊刃 | YangBlade | 日干（甲→卯，乙→寅，丙戊→午，丁己→巳，庚→酉，辛→申，壬→子，癸→亥） |
| 将星 | GeneralsStar | 日支（寅午戌→午，亥卯未→卯，巳酉丑→酉，申子辰→子） |
| 华盖 | Canopy | 日支（寅午戌→戌，亥卯未→未，巳酉丑→丑，申子辰→辰） |
| 谋星 | StarOfStrategy | 日支（寅午戌→辰，亥卯未→丑，巳酉丑→未，申子辰→戌） |
| 灾煞 | DisasterMalignity | 日支（寅午戌→子，亥卯未→酉，巳酉丑→卯，申子辰→午） |
| 劫煞 | RobberyMalignity | 日支（寅午戌→亥，亥卯未→申，巳酉丑→寅，申子辰→巳） |
| 亡神 | DeathSpirit | 日支（寅午戌→巳，亥卯未→寅，巳酉丑→申，申子辰→亥） |
| 天医 | CelestialPhysician | 月支（退一位） |
| 天喜 | HeavenlyJoy | 月支（寅卯辰→戌，巳午未→丑，申酉戌→辰，亥子丑→未） |
| 床帐 | MarriageBed | 卦身五行（火→辰戌丑未，金→寅卯，水→巳午，木→申酉，土→亥子） |
| 香闺 | BridalChamber | 卦身五行（火→寅卯，金→辰戌丑未，水→申酉，木→亥子，土→巳午） |

### 10. 获取卦辞、彖辞、象辞

IChingLibrary 支持获取六十四卦的卦辞、彖辞、象辞（大象），以及每个爻的爻辞和小象辞。

#### 10.1 基础用法

通过扩展方法获取卦的辞文：

```csharp
using IChingLibrary.Core;
using IChingLibrary.SixLines;

// 创建卦实例
var qian = Hexagram.TheCreative;
var instance = new HexagramInstance(qian);

// 获取卦辞
string statement = qian.GetStatement();
Console.WriteLine($"卦辞：{statement}");
// 输出：卦辞：元，亨，利，贞。

// 获取彖辞
string commentary = qian.GetCommentary();
Console.WriteLine($"彖辞：{commentary}");
// 输出：彖辞：大哉乾元，万物资始...

// 获取象辞（大象）
string image = qian.GetImage();
Console.WriteLine($"象辞：{image}");
// 输出：象辞：天行健，君子以自强不息。
```

#### 11.2 获取爻辞和小象辞

通过扩展方法获取爻的辞文：

```csharp
using IChingLibrary.Core;
using IChingLibrary.SixLines;

var qian = Hexagram.TheCreative;
var instance = new HexagramInstance(qian);

// 遍历所有爻
foreach (var line in instance.Lines)
{
    // 获取爻辞
    string lineStatement = line.GetStatement(qian);

    // 获取小象辞
    string lineImage = line.GetImage(qian);

    Console.WriteLine($"{line.LinePosition.Label}爻：");
    Console.WriteLine($"  爻辞：{lineStatement}");
    Console.WriteLine($"  小象：{lineImage}");
}

// 输出示例：
// First爻：
//   爻辞：潜龙，勿用。
//   小象：潜龙勿用，阳在下也。
// Second爻：
//   爻辞：见龙在田，利见大人。
//   小象：见龙在田，德施普也。
// ...
```

#### 11.3 指定文化参数

所有获取辞文的方法都支持指定文化参数：

```csharp
using System.Globalization;
using IChingLibrary.Core;

var qian = Hexagram.TheCreative;
var zh = new CultureInfo("zh-Hans");
var en = new CultureInfo("en");

// 获取中文卦辞
string statementZh = qian.GetStatement(zh);
Console.WriteLine(statementZh);  // 输出：元，亨，利，贞。

// 获取英文卦辞
string statementEn = qian.GetStatement(en);
Console.WriteLine(statementEn);  // 输出：Sublime success, perseverance furthers.

// 获取中文爻辞
var instance = new HexagramInstance(qian);
var firstLine = instance.Lines[0];
string lineStatementZh = firstLine.GetStatement(qian, zh);
Console.WriteLine(lineStatementZh);  // 输出：潜龙，勿用。

// 获取英文爻辞
string lineStatementEn = firstLine.GetStatement(qian, en);
Console.WriteLine(lineStatementEn);  // 输出：Hidden dragon. Do not act.
```

#### 11.4 扩展方法列表

**HexagramExtensions** (位于 `IChingLibrary.Core.Extensions`)：

| 方法 | 说明 | 返回类型 |
|------|------|---------|
| `GetStatement(CultureInfo? culture = null)` | 获取卦辞 | `string` |
| `GetCommentary(CultureInfo? culture = null)` | 获取彖辞 | `string` |
| `GetImage(CultureInfo? culture = null)` | 获取象辞（大象） | `string` |

**LineExtensions** (位于 `IChingLibrary.SixLines.Extensions`)：

| 方法 | 说明 | 返回类型 |
|------|------|---------|
| `GetStatement(Hexagram hexagram, CultureInfo? culture = null)` | 获取爻辞 | `string` |
| `GetImage(Hexagram hexagram, CultureInfo? culture = null)` | 获取小象辞 | `string` |

#### 11.5 完整示例

```csharp
using System.Globalization;
using IChingLibrary.Core;
using IChingLibrary.Core.Localization;
using IChingLibrary.SixLines;

// 设置默认语言
IChingTranslationManager.DefaultCulture = new CultureInfo("zh-Hans");

// 创建占卜
var divination = SixLineDivination.Create(DateTimeOffset.Now);
var hexagram = divination.Original.Meta;

// 显示卦信息
Console.WriteLine($"卦名：{hexagram.Label}");
Console.WriteLine($"卦辞：{hexagram.GetStatement()}");
Console.WriteLine($"彖辞：{hexagram.GetCommentary()}");
Console.WriteLine($"象辞：{hexagram.GetImage()}");
Console.WriteLine();

// 显示每个爻的信息
foreach (var line in divination.Original.Lines)
{
    Console.WriteLine($"{line.LinePosition.Label}爻 ({line.YinYang.Label})：");
    Console.WriteLine($"  爻辞：{line.GetStatement(hexagram)}");
    Console.WriteLine($"  小象：{line.GetImage(hexagram)}");
    Console.WriteLine($"  六亲：{line.SixKin?.Label ?? "未计算"}");
    Console.WriteLine($"  世应：{line.Position?.Label ?? "无"}");
    Console.WriteLine();
}
```

### 11. 多语言国际化支持

IChingLibrary 内置了完整的多语言支持，默认提供简体中文和英文两种语言。所有易学元素（YinYang、FivePhase、Trigram、Hexagram、HeavenlyStem、EarthlyBranch 等）都支持本地化显示。

#### 11.1 基础用法

所有易学元素都提供了 `ToString()` 方法，会根据当前文化设置返回对应的翻译：

```csharp
using System.Globalization;
using IChingLibrary.Core;
using IChingLibrary.Core.Localization;

// 设置易学库默认语言为简体中文
IChingTranslationManager.DefaultCulture = new CultureInfo("zh-Hans");

Console.WriteLine(YinYang.Yin);      // 输出：阴
Console.WriteLine(YinYang.Yang);     // 输出：阳
Console.WriteLine(FivePhase.Metal);  // 输出：金
Console.WriteLine(Trigram.Qian);     // 输出：乾
Console.WriteLine(Hexagram.TheCreative);  // 输出：乾为天

// 切换为英文
IChingTranslationManager.DefaultCulture = new CultureInfo("en");

Console.WriteLine(YinYang.Yin);      // 输出：Yin
Console.WriteLine(YinYang.Yang);     // 输出：Yang
Console.WriteLine(FivePhase.Metal);  // 输出：Metal
Console.WriteLine(Trigram.Qian);     // 输出：Qian
Console.WriteLine(Hexagram.TheCreative);  // 输出：The Creative
```

#### 11.2 干支组合翻译

`StemBranch` 类也会根据文化设置返回正确的翻译：

```csharp
// 中文环境
IChingTranslationManager.DefaultCulture = new CultureInfo("zh-Hans");
var stemBranch = new StemBranch(HeavenlyStem.Jia, EarthlyBranch.Zi);
Console.WriteLine(stemBranch);  // 输出：甲子

// 英文环境
IChingTranslationManager.DefaultCulture = new CultureInfo("en");
Console.WriteLine(stemBranch);  // 输出：JiaZi
```

#### 11.3 指定文化参数

如果不想使用全局的 `DefaultCulture`，可以为每个 `ToString()` 调用指定文化参数：

```csharp
var zh = new CultureInfo("zh-Hans");
var en = new CultureInfo("en");

// 即使 DefaultCulture 是英文，也可以指定使用中文
IChingTranslationManager.DefaultCulture = new CultureInfo("en");

Console.WriteLine(YinYang.Yin.ToString(zh));  // 输出：阴
Console.WriteLine(YinYang.Yin.ToString(en));  // 输出：Yin
```

#### 11.4 应用启动时设置语言

在应用程序启动时设置易学库的默认语言：

```csharp
// ASP.NET Core 应用 - Program.cs
var builder = WebApplication.CreateBuilder(args);

// 根据用户配置或请求首选项设置易学库语言
var userLanguage = builder.Configuration["AppSettings:Language"] ?? "zh-Hans";
IChingTranslationManager.DefaultCulture = new CultureInfo(userLanguage);

var app = builder.Build();
app.Run();
```

```csharp
// WPF/WinForms 应用 - App.xaml.cs 或 Program.cs
public static class Program
{
    [STAThread]
    public static void Main()
    {
        // 从用户设置读取语言偏好
        var language = UserService.GetUserLanguagePreference();
        IChingTranslationManager.DefaultCulture = new CultureInfo(language);

        // 启动应用程序
        var app = new MainWindow();
        app.ShowDialog();
    }
}
```

#### 11.5 运行时切换语言

支持在应用程序运行时动态切换语言，不影响系统其他本地化设置：

```csharp
// 用户在应用设置中选择语言
public void ChangeLanguage(string languageCode)
{
    IChingTranslationManager.DefaultCulture = new CultureInfo(languageCode);

    // 重新加载界面或刷新显示
    RefreshDisplay();
}

// 示例：切换为简体中文
ChangeLanguage("zh-Hans");

// 示例：切换为英文
ChangeLanguage("en");
```

#### 11.6 文化解析优先级

当获取翻译时，系统按以下优先级解析文化：

```
1. 显式指定的 CultureInfo 参数（最高优先级）
   ↓
2. IChingTranslationManager.DefaultCulture（如果已设置）
   ↓
3. CultureInfo.CurrentUICulture（系统 UI 文化，默认回退）
```

```csharp
// 示例：优先级演示
IChingTranslationManager.DefaultCulture = new CultureInfo("zh-Hans");
// 假设系统 CurrentUICulture 是 "en"

var element = YinYang.Yin;

// 1. 最高优先级：显式指定
element.ToString(new CultureInfo("en"));  // 输出：Yin

// 2. 次优先级：DefaultCulture
element.ToString();  // 输出：阴

// 3. 默认回退：CurrentUICulture（当 DefaultCulture 为 null 时）
IChingTranslationManager.DefaultCulture = null;
element.ToString();  // 输出：Yin
```

#### 11.7 自定义翻译提供者

如果需要支持更多语言或自定义翻译，可以实现 `IIChingTranslationProvider` 接口：

```csharp
using System.Globalization;
using IChingLibrary.Core.Localization;

// 自定义翻译提供者
public class JsonTranslationProvider : IIChingTranslationProvider
{
    private readonly Dictionary<string, Dictionary<string, string>> _translations;

    public JsonTranslationProvider(string jsonFilePath)
    {
        // 从 JSON 文件加载翻译
        var json = File.ReadAllText(jsonFilePath);
        _translations = JsonSerializer.Deserialize<
            Dictionary<string, Dictionary<string, string>>>(json)!;
    }

    public string? GetTranslation(string typeName, string label, CultureInfo culture)
    {
        var key = $"{typeName}.{label}";
        var cultureName = culture.Name;

        if (_translations.TryGetValue(cultureName, out var langDict))
        {
            return langDict.GetValueOrDefault(key);
        }

        return null;  // 找不到翻译时返回 null，将使用 Label
    }
}

// 使用自定义提供者
IChingTranslationManager.Provider = new JsonTranslationProvider("translations.json");
```

```csharp
// 数据库翻译提供者示例
public class DatabaseTranslationProvider : IIChingTranslationProvider
{
    private readonly IDbConnection _dbConnection;

    public DatabaseTranslationProvider(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public string? GetTranslation(string typeName, string label, CultureInfo culture)
    {
        const string sql = @"
            SELECT TranslationText
            FROM Translations
            WHERE TypeName = @TypeName
              AND Label = @Label
              AND CultureName = @CultureName";

        return _dbConnection.QuerySingleOrDefault<string>(sql, new
        {
            TypeName = typeName,
            Label = label,
            CultureName = culture.Name
        });
    }
}

// 使用数据库提供者
IChingTranslationManager.Provider = new DatabaseTranslationProvider(dbConnection);
```

#### 11.8 重置为默认设置

如果需要恢复到默认的 RESX 翻译提供者和文化设置：

```csharp
// 重置所有自定义设置
IChingTranslationManager.ResetToDefault();

// 之后将使用：
// - Provider: 默认的 ResxTranslationProvider
// - DefaultCulture: null（使用系统 CurrentUICulture）
```

#### 11.9 与系统本地化的隔离

`IChingTranslationManager.DefaultCulture` 只影响易学库的翻译，不会影响应用程序的其他本地化（如日期、货币格式等）：

```csharp
// 设置系统 UI 文化为英语
CultureInfo.CurrentUICulture = new CultureInfo("en");

// 设置易学库语言为简体中文
IChingTranslationManager.DefaultCulture = new CultureInfo("zh-Hans");

// 易学元素显示中文
Console.WriteLine(YinYang.Yin);  // 输出：阴

// 其他本地化仍然使用系统语言
var date = DateTime.Now;
Console.WriteLine(date.ToString("d"));  // 输出：M/D/YYYY（英语格式）
```

#### 11.10 支持的语言

目前内置支持的语言：

| 语言名称 | 语言代码 | 说明 |
|----------|----------|------|
| English | `en` | 默认语言（回退语言） |
| 简体中文 | `zh-Hans` | 简体中文翻译 |

如需添加其他语言，可以：

1. 创建新的 RESX 文件（如 `IChingResources.ja.resx` 用于日语）
2. 或实现自定义 `IIChingTranslationProvider`

#### 11.11 唯一键（UniqueKey）

每个易学元素都有 `UniqueKey` 属性，格式为 `{TypeName}.{Label}`，用于资源查找和调试：

```csharp
Console.WriteLine(YinYang.Yin.UniqueKey);      // 输出：YinYang.Yin
Console.WriteLine(FivePhase.Metal.UniqueKey);  // 输出：FivePhase.Metal
Console.WriteLine(Trigram.Qian.UniqueKey);     // 输出：Trigram.Qian
Console.WriteLine(Hexagram.TheCreative.UniqueKey);  // 输出：Hexagram.TheCreative
```

## API 参考

### SixLineDivination

主类，提供静态方法创建占卜实例。

#### Create 方法（起卦）

| 方法 | 说明 |
|------|------|
| `Create(DateTimeOffset)` | 时间起卦法，根据年月日时自动起卦 |
| `Create(DateTimeOffset, int, int, int?)` | 随机数起卦法（上卦数、下卦数、动爻数可选） |
| `Create(DateTimeOffset, Hexagram, Hexagram?)` | 指定主卦和变卦起卦 |
| `Create(DateTimeOffset, byte, byte?)` | 指定主卦值和变卦值起卦 |
| `Create(DateTimeOffset, FourSymbol[])` | 使用四象数组起卦（数组顺序：从初爻到上爻） |
| `Create(DateTimeOffset, byte[])` | 使用四象值数组起卦（数组顺序：从初爻到上爻） |

#### CreateBuilder 方法（自定义流程）

| 方法 | 说明 |
|------|------|
| `CreateBuilder(DateTimeOffset)` | 创建构建器（需配合 Use 系列方法选择起卦方式） |

#### 属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `InquiryTime` | `InquiryTime` | 问时信息 |
| `Original` | `HexagramInstance` | 主卦 |
| `Changed` | `HexagramInstance?` | 变卦（如有） |
| `SymbolicStars` | `SymbolicStarCollection?` | 神煞集合 |

### SixLineDivinationBuilder

构建器类，用于自定义占卜流程。

#### 起卦方法（Use 系列方法，必须调用其中一个）

| 方法 | 说明 |
|------|------|
| `UseTimeBasedHexagram()` | 使用时间起卦法 |
| `UseRandomHexagram(int, int, int?)` | 使用随机数起卦法（上卦数、下卦数、动爻数可选） |
| `UseFourSymbols(FourSymbol[])` | 使用指定的四象数组（数组顺序：从初爻到上爻） |
| `UseFourSymbols(byte[])` | 使用指定的四象值数组（数组顺序：从初爻到上爻） |
| `UseHexagram(Hexagram, Hexagram?)` | 使用指定的卦象（主卦、变卦可选） |

#### 流程配置方法（With 系列方法）

| 方法 | 说明 |
|------|------|
| `WithInquiryTimeProvider(IInquiryTimeProvider)` | 设置问时转换器 |
| `WithNajia(INajiaProvider?)` | 添加纳甲步骤（主卦） |
| `WithPosition(IPositionProvider?)` | 添加世应位置步骤（主卦） |
| `WithSixKin(ISixKinProvider?)` | 添加六亲步骤（主卦） |
| `WithSixSpirit(ISixSpiritProvider?)` | 添加六神步骤（主卦） |
| `WithHiddenDeity(IHiddenDeityProvider?)` | 添加伏神步骤（主卦） |
| `WithNajiaForChanged(INajiaProvider?)` | 添加纳甲步骤（变卦） |
| `WithSixKinForChanged(ISixKinProvider?)` | 添加六亲步骤（变卦） |
| `WithSymbolicStars(Action<DefaultSymbolicStarProvider>?)` | 配置神煞计算器 |
| `WithDefaultSteps()` | 添加默认完整流程 |
| `WithCustomStep(ISixLineStep, bool)` | 添加自定义步骤 |
| `Build()` | 构建占卜实例 |

### IChingTranslationManager

易学翻译管理器，提供多语言支持的核心 API。

#### 属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Provider` | `IIChingTranslationProvider` | 获取或设置当前翻译提供者 |
| `DefaultCulture` | `CultureInfo?` | 获取或设置易学库的默认文化 |

#### 方法

| 方法 | 说明 |
|------|------|
| `GetTranslation(string typeName, string label, CultureInfo? culture = null)` | 获取指定元素的翻译文本 |
| `ResetToDefault()` | 重置为默认翻译提供者和文化设置 |

#### 使用示例

```csharp
using IChingLibrary.Core.Localization;

// 设置默认语言
IChingTranslationManager.DefaultCulture = new CultureInfo("zh-Hans");

// 获取翻译
var translation = IChingTranslationManager.GetTranslation("YinYang", "Yin");
Console.WriteLine(translation);  // 输出：阴

// 使用自定义提供者
IChingTranslationManager.Provider = new MyCustomProvider();

// 重置为默认设置
IChingTranslationManager.ResetToDefault();
```

### IIChingTranslationProvider

翻译提供者接口，用于实现自定义翻译逻辑。

#### 方法

| 方法 | 返回类型 | 说明 |
|------|---------|------|
| `GetTranslation(string typeName, string label, CultureInfo culture)` | `string?` | 获取指定元素和文化的翻译，找不到时返回 null |

#### 实现示例

```csharp
public class CustomTranslationProvider : IIChingTranslationProvider
{
    public string? GetTranslation(string typeName, string label, CultureInfo culture)
    {
        // 自定义翻译逻辑
        // 返回翻译文本，或 null 表示使用默认的 Label
        return null;
    }
}
```

### IChingElement<T>

所有易学元素的抽象基类，提供多语言支持的通用功能。

#### 属性

| 属性 | 类型 | 说明 |
|------|------|------|
| `Value` | `byte` | 元素的唯一标识值 |
| `Label` | `string` | 元素的标签名称 |
| `UniqueKey` | `string` | 元素的唯一键，格式为 `{TypeName}.{Label}` |

#### 方法

| 方法 | 返回类型 | 说明 |
|------|---------|------|
| `ToString()` | `string` | 返回元素的翻译文本（使用 DefaultCulture 或 CurrentUICulture） |
| `ToString(CultureInfo culture)` | `string` | 返回指定文化的翻译文本 |

#### 文化解析优先级

```
1. ToString(CultureInfo culture) 中的 culture 参数（最高优先级）
   ↓
2. IChingTranslationManager.DefaultCulture（如果已设置）
   ↓
3. CultureInfo.CurrentUICulture（系统 UI 文化，默认回退）
```

## 构建流程

默认完整流程包含以下步骤：

```
1. 选择起卦方式（Use 系列方法）
   ├── UseTimeBasedHexagram()     - 时间起卦法
   ├── UseRandomHexagram()        - 随机数起卦法
   ├── UseFourSymbols()           - 指定四象值
   └── UseHexagram()              - 指定卦象
   ↓
2. 参数验证
   ↓
3. 转换问时信息（阳历 → 阴历 → 干支）
   ↓
4. 从四象值计算卦值（确定变爻）
   ↓
5. 创建主卦实例
   ↓
6. 纳甲（绑定干支）
   ↓
7. 计算世应位置
   ↓
8. 计算六亲
   ↓
9. 计算伏神
   ↓
10. 计算六神
   ↓
11. 计算神煞（包含卦身）
   ↓
12. 生成变卦（如有变爻）
   ↓
13. 返回占卜实例
```

## 设计原则

本项目遵循以下设计原则：

### SOLID 原则

- **单一职责原则（SRP）**：每个类只负责一个功能
  - `HexagramGenerator`：负责从各种方式生成四象数组
  - `SixLineDivinationBuilder`：负责步骤配置和实例构建
  - `SixLineDivination`：负责提供统一的 API 入口
  - 每个 Provider：负责单一的计算功能

- **开闭原则（OCP）**：通过接口扩展，无需修改现有代码
  - 添加新起卦方式：只需扩展 `HexagramGenerator`
  - 添加新计算步骤：实现 `ISixLineStep` 接口
  - 添加新 Provider：实现相应的 Provider 接口

- **里氏替换原则（LSP）**：所有 Provider 实现可互相替换

- **接口隔离原则（ISP）**：接口职责明确，不强迫实现不需要的方法

- **依赖倒置原则（DIP）**：依赖抽象接口而非具体实现

### 架构设计

```
┌─────────────────────────────────────────────────────────────────┐
│                     SixLineDivination                           │
│  - 统一的 API 入口                                               │
│  - 委托给 HexagramGenerator 生成四象                             │
│  - 委托给 SixLineDivinationBuilder 构建                         │
└─────────────────────────────────────────────────────────────────┘
                              │
                ┌─────────────┴─────────────┐
                ▼                           ▼
┌──────────────────────────────────┐  ┌──────────────────────────────┐
│     HexagramGenerator (internal) │  │  SixLineDivinationBuilder   │
│  - FromTime()                    │  │  - WithNajia()               │
│  - FromRandomNumbers()           │  │  - WithPosition()            │
│  - FromHexagrams()               │  │  - WithSixKin()              │
│  - FromFourSymbols()             │  │  - WithSixSpirit()           │
│  → 返回 FourSymbol[]             │  │  - Build()                   │
└──────────────────────────────────┘  └──────────────────────────────┘
```

## 技术栈

- **目标框架**: .NET 10.0
- **语言版本**: C# 12.0
- **外部依赖**: lunar-csharp 1.6.8（阴历转换）
- **代码生成器**: Microsoft.CodeAnalysis.CSharp 5.0.0

## 贡献

欢迎提交 Issue 和 Pull Request！

## 许可证

MIT License

## 致谢

- [lunar-csharp](https://github.com/623537395/lunar-csharp) - 农历转换库
