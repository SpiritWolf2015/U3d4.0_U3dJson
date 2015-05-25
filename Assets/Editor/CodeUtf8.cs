using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// 选中你要选择的文件或文件夹。然后 CodeUTF/SelectFile（可以多选 ）
/// author: luas
/// </summary>
public class CodeUtf8 {
    [MenuItem("CodeUTF/SelectFile")]
    public static void SetUtf8 ( ) {
        foreach (TextAsset o in Selection.GetFiltered(typeof(TextAsset), SelectionMode.DeepAssets)) {
            string oldpath = AssetDatabase.GetAssetPath(o);
            ConvertFileEncoding(oldpath, oldpath, System.Text.Encoding.UTF8);
        }
        Debug.Log("转换为UTF8完成！");
    }

    /// <summary>
    /// 文件编码转换
    /// </summary>
    /// <param name="sourceFile">源文件</param>
    /// <param name="destFile">目标文件，如果为空，则覆盖源文件</param>
    /// <param name="targetEncoding">目标编码</param>
    static void ConvertFileEncoding (string sourceFile, string destFile, System.Text.Encoding targetEncoding) {
        destFile = string.IsNullOrEmpty(destFile) ? sourceFile : destFile;
        System.IO.File.WriteAllText(destFile, System.IO.File.ReadAllText(sourceFile, System.Text.Encoding.Default), targetEncoding);
    }

}