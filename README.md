# Seek The Sheep

一款 2D 俯视角益智解谜游戏。玩家操控牧羊人在格子地图上移动，利用推箱子、铺桥、砍树等机制解开谜题，最终找到并接近绵羊即可过关。关卡按顺序逐步解锁，并设有隐藏关与在线排行榜。

仓库地址：[github.com/kkirito16/Seek-The-Sheep](https://github.com/kkirito16/Seek-The-Sheep)

## 游戏玩法

### 基本操作

| 按键 | 作用 |
|------|------|
| `W` / `S` / `A` / `D` | 向上下左右移动一步 |
| 关卡内 UI | 重开本关、返回选关、撤销上一步等 |

移动采用**离散格子步进**（非连续行走），每走一步会计入步数；步数越少，在排行榜中成绩越好。

### 核心目标

- 在多数关卡中，玩家需要**找到绵羊**（`hasFindSheep`）或完成关卡内的机关谜题。
- 经典关卡包含**推石头到目标点**（`Target` + `Rock`）：所有石头就位后门会亮起（`TargetsManager`）。
- 通关后进度写入 `LevelState_ISO`，选关界面会解锁下一关。

### 机关与机制

| 机制 | 说明 | 相关脚本 |
|------|------|----------|
| 推箱子 | 石头推到标靶上触发开门 | `Rock.cs`, `Target.cs`, `TargetsManager.cs` |
| 冰面滑行 | 在冰上移动会沿方向滑到非冰格为止 | `PlayerController.cs` |
| 水面与铺桥 | 携带木材可在水面铺木桥，桥可被撤销 | `Water.cs`, `PlayerController` |
| 合作关 | 玩家移动时，绵羊会向**相反方向**联动 | `SheepController.cs`, `LevelCooperation` |
| 迷惑关 | 方向键与移动方向**相反** | `ConfusedController.cs`, `LevelConfused` |
| 斧头 / 树桩 | 砍树、障碍交互 | `Axe.cs`, `Tree.cs`, `Stump.cs` |
| 撤销 | 可回退玩家、绵羊、铺桥等上一步状态 | `RetractLastController.cs` |
| 隐藏关 | 完成前置关卡后解锁，含剧情与后期效果 | `HiddenLevelManager.cs`, `HiddenLevel` |

## 关卡列表

按 `EditorBuildSettings` 中的加载顺序：

| 序号 | 场景 | 说明 |
|------|------|------|
| 0 | `StartMenu` | 主菜单 |
| 1 | `LevelSelect` | 关卡选择（根据 `LevelState_ISO` 解锁） |
| 2 | `Level1` | 教学 / 入门 |
| 3 | `Level2` | |
| 4 | `Level-ice` | 冰面机制 |
| 5 | `Level3` | |
| 6 | `Level-dark night` | 夜间主题 |
| 7 | `Level-ice-night` | 冰面 + 夜间 |
| 8 | `LevelCooperation` | 与绵羊合作（反向联动） |
| 9 | `LevelConfused` | 反向操作 |
| 10 | `Level-SheepFindMan` | 寻羊主题关 |
| 11 | `HiddenLevel` | 隐藏关（需解锁） |

> `Level4` 已存在于项目中，但未加入 Build Settings，默认不会被打包进游戏流程。

## 技术栈

- **引擎**：Unity `6000.0.30f1`（Unity 6）
- **渲染**：Universal Render Pipeline (URP) `17.0.3`
- **动画**：DOTween
- **UI 文字**：TextMesh Pro
- **排行榜**：LootLocker SDK `2.1.0`（`LeaderBoard.cs` 上传与拉取分数）

## 项目结构

```
Seek5.0/
├── Assets/
│   ├── Scenes/           # 关卡与菜单场景
│   ├── Scripts/          # 游戏逻辑
│   │   ├── UI/           # 界面、选关、按钮
│   │   ├── Audio/        # BGM / 音效
│   │   ├── RankList/     # LootLocker 排行榜
│   │   ├── Dialogs/      # 关卡内对话
│   │   └── Utility/      # 关卡状态、隐藏关、工具类
│   ├── Resources/        # 预制体、LevelState_ISO 等
│   ├── Textures/         # 贴图与 CG
│   └── Audio/            # 音频资源
├── ProjectSettings/      # Unity 项目配置
├── Packages/             # 包依赖（manifest.json）
└── .gitignore            # 已忽略 Build、Library 等生成目录
```

### 主要脚本一览

| 模块 | 脚本 | 职责 |
|------|------|------|
| 玩家 | `PlayerController.cs` | 移动、冰面、铺桥、找羊、步数统计 |
| 绵羊 | `SheepController.cs` | 合作关反向联动 |
| 关卡 | `LevelManager.cs` | 选关界面解锁与按钮状态 |
| 进度 | `LevelState_ISO.cs` | ScriptableObject 持久化关卡解锁 |
| 全局 UI | `UIManager.cs`, `ButtonManager.cs` | 步数/木材显示、场景切换 |
| 排行榜 | `LeaderBoard.cs`, `PlayerManager.cs` | LootLocker 分数提交与展示 |

## 环境要求

- Unity **6000.0.30f1** 或兼容的 Unity 6 版本
- 支持 Windows / macOS 等平台构建（项目内已有 Windows 构建输出目录 `Build/`，该目录已被 `.gitignore` 忽略）

## 快速开始

1. 克隆仓库：

   ```bash
   git clone git@github.com:kkirito16/Seek-The-Sheep.git
   cd Seek-The-Sheep
   ```

2. 使用 Unity Hub 打开项目根目录，等待 `Library/` 与依赖包自动生成。

3. 打开 `Assets/Scenes/StartMenu.unity`，点击 Play 运行。

4. 若需配置排行榜，在 Unity 中检查 LootLocker 相关配置：
   - `Assets/LootLockerSDK/Resources/Config/LootLockerConfig.asset`
   - `LeaderBoard.cs` 中的 `leaderboardKey`（默认 `"16304"`）

## 构建说明

在 Unity 菜单 **File → Build Settings** 中选择目标平台并构建。构建产物会输出到本地 `Build/` 目录，**不会**提交到 Git 仓库。

### 命令行构建 Windows 版

项目提供 `Assets/Editor/BuildScript.cs`，可在无界面模式下打包：

```bash
/Applications/Unity/Hub/Editor/6000.0.30f1/Unity.app/Contents/MacOS/Unity \
  -batchmode -quit -nographics \
  -projectPath "$(pwd)" \
  -executeMethod BuildScript.BuildWindows \
  -logFile Logs/build.log
```

输出路径：`Build/Windows_New/WhereIsMySheep.exe`（已在 `.gitignore` 中忽略）。

## 许可证

未在仓库中声明许可证。如需开源或二次分发，请自行补充 LICENSE 文件。
