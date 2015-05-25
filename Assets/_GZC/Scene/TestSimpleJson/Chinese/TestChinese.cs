using UnityEngine;
using System.Collections;

public class TestChinese : MonoBehaviour {

    // 用记事本把文件编码改成UTF8就能解析了。
    readonly string CHINESE_JSON = @"MyJsonFile/B.txt";
    
    System.Action<WWW, string> m_Action;
    string m_result = "初始";
    private Vector2 m_Position = Vector2.zero;

    void Start ( ) {
        Debug.LogWarning("TestChinese");  

        m_Action = (w3, error) => {
            if (null == error) {
                m_result = w3.text;
                Debug.Log(m_result);
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