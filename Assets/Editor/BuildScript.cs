using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class BuildScript
{
    // 命令行入口：Unity -batchmode -quit -executeMethod BuildScript.BuildWindows
    public static void BuildWindows()
    {
        // 收集 EditorBuildSettings 中所有 enabled 的场景
        var scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => s.path)
            .ToArray();

        Debug.Log($"[BuildScript] 共 {scenes.Length} 个场景将被打包：");
        foreach (var s in scenes) Debug.Log("  - " + s);

        // 输出位置：Build/Windows_New/WhereIsMySheep.exe
        string outputDir = System.IO.Path.Combine(
            System.IO.Directory.GetCurrentDirectory(),
            "Build", "Windows_New");
        System.IO.Directory.CreateDirectory(outputDir);
        string outputPath = System.IO.Path.Combine(outputDir, "WhereIsMySheep.exe");

        var buildOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = outputPath,
            target = BuildTarget.StandaloneWindows64,
            targetGroup = BuildTargetGroup.Standalone,
            options = BuildOptions.None
        };

        Debug.Log($"[BuildScript] 开始构建 -> {outputPath}");
        var report = BuildPipeline.BuildPlayer(buildOptions);

        var summary = report.summary;
        Debug.Log($"[BuildScript] 构建完成：result={summary.result}, totalSize={summary.totalSize} bytes, totalTime={summary.totalTime}");

        if (summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log("[BuildScript] ✓ 构建成功！");
            EditorApplication.Exit(0);
        }
        else
        {
            Debug.LogError("[BuildScript] ✗ 构建失败！");
            EditorApplication.Exit(1);
        }
    }
}
