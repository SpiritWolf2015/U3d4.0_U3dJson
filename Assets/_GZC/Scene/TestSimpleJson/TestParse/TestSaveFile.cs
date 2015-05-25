using UnityEngine;
using System.Collections;

public class TestSaveFile : MonoBehaviour {

    // 用记事本把文件编码改成UTF8就能解析了。
    readonly string CHINESE_JSON = @"MyJsonFile/GoTransformJson.txt";
    
    System.Action<WWW, string> m_Action;
    string m_result = "初始";
    private Vector2 m_Position = Vector2.zero;

    void Start ( ) {
        Debug.LogWarning("TestSaveFile");  

        m_Action = (w3, error) => {
            if (null == error) {
                m_result = w3.text;
                // 带排版格式
                m_result = SimpleJsonTool.formatJsonStr(m_result);  
                // 保存JSON文件
                SimpleJsonTool.saveCompressedJsonFile(m_result, Application.streamingAssetsPath + @"/SimpleJsonCompressedJsonStrSave.txt");
                Debug.Log("保存SimpleJsonCompressedJsonStrSave.txt成功");
            } else {
                m_result = w3.error;
                // www错误
                Debug.LogError("W3错误，URL=" + w3.url + ", ERROR:" + w3.error);
            }            
        };

        string url = U3dPathUtil.U3dStreamPath( ) + CHINESE_JSON;
        StartCoroutine(W3Util.W3GetRequest(url, m_Action));
    }

    
    void OnGUI ( ) {
        m_Position = GUILayout.BeginScrollView(m_Position);
        GUILayout.Label(m_result.ToString( ));
        GUILayout.EndScrollView( );
    }

}