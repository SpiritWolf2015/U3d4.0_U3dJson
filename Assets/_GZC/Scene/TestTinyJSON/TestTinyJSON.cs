using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TinyJSON;

public class TestTinyJSON : MonoBehaviour {

    // 用记事本把文件编码改成UTF8就能解析了。
    readonly string CHINESE_JSON = @"MyJsonFile/GoTransformJson.txt";

    System.Action<WWW, string> m_Action;
    string m_result = "初始";
    private Vector2 m_Position = Vector2.zero;

	void Start () {
        Debug.LogWarning("TestTinyJSON");

        m_Action = (w3, error) => {
            if (null == error) {
                m_result = w3.text;              
                Debug.Log(m_result);

                TinyJSON.Parser parser = new Parser( );
                Node node = parser.Load(m_result);
                Debug.Log("node.Count=" + node.Count);
                Debug.Log("node.IsTable=" + node.IsTable( ));
                Dictionary<string, Node> gos = (Dictionary<string, Node>)node;
                Debug.Log("gos[gameObject].IsArray( )=" + gos["gameObject"].IsArray( ));
                List<Node> gonodes = (List<Node>)gos["gameObject"];
                Dictionary<string, Node> go = (Dictionary<string, Node>)gonodes[0];
                Debug.Log("go[name]=" + go["name"]);

                Node position = go["position"];
                Dictionary<string, Node> positionDic = (Dictionary<string, Node>)position;
                Debug.Log("positionDic[x]=" + positionDic["x"]);
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
