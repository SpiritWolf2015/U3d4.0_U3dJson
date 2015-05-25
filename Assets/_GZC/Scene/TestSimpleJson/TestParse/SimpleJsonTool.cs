using UnityEngine;
using System.IO;
using SimpleJSON;

public static class SimpleJsonTool   {

	public static string formatJsonStr (string jsonStr) {
        return JSON.Parse(jsonStr).ToString("");    // 带排版格式
    }
    public static string formatJsonStr (JSONNode node) {
        return node.ToString("");    // 带排版格式
    }

    public static void saveJsonFile (string jsonStr, string fileNamePath) {
        JSON.Parse(jsonStr).SaveToFile(fileNamePath);
    }

    public static void saveCompressedJsonFile (string jsonStr,string fileNamePath) {
        JSON.Parse(jsonStr).SaveToCompressedFile(fileNamePath);
    }

    public static string loadCompressedJsonFile (string fileNamePath) {
        string json = null;
        if (File.Exists(fileNamePath)) {
            JSONNode node = JSONNode.LoadFromCompressedFile(fileNamePath);
            json = formatJsonStr(node);
            if (string.IsNullOrEmpty(json)) {
                Debug.LogError(fileNamePath + "，读取JSON文件为空！");
            } else {
                return json;
            }
        } else {
            Debug.LogError(fileNamePath + "，文件不存在！");
        }
       
       return null;
    }

}
