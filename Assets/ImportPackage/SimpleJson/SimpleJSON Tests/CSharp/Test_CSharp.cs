using UnityEngine;
using System.Text;
using SimpleJSON;


class Test_CSharp : MonoBehaviour {

    private StringBuilder m_InGameLog = new StringBuilder( );
    private Vector2 m_Position = Vector2.zero;

    void P (string aText) {
        m_InGameLog.Append(aText + "\n");
    }

    void Test ( ) {
        JSONNode N = JSONNode.Parse("{\"name\":\"test\", \"array\":[1,{\"data\":\"value\"}]}");
        N["array"][1]["Foo"] = "Bar";
        P("'nice formatted' string representation of the JSON tree:");
        P(N.ToString(""));  // 带排版格式
        P("");

        P("'normal' string representation of the JSON tree:");
        P(N.ToString( ));   // 不带排版格式
        P("");

        P("content of member 'name':");
        P(N["name"]);
        P("");

        P("content of member 'array':");
        P(N["array"].ToString(""));
        P("");

        P("first element of member 'array': " + N["array"][0]);
        P("");

        N["array"][0].AsInt = 10;
        P("value of the first element set to: " + N["array"][0]);
        P("The value of the first element as integer: " + N["array"][0].AsInt);
        P("");

        P("N[\"array\"][1][\"data\"] == " + N["array"][1]["data"]);
        P("");

        string data = N.SaveToBase64( );
        string data2 = N.SaveToCompressedBase64( );
        N = null;
        P("序列化 to Base64 string:");
        P(data);
        P("序列化 to Base64 string (压缩):");
        P(data2);
        P("");

        N = JSONNode.LoadFromBase64(data);
        P("反序列化 from Base64 string:");
        P(N.ToString( ));
        P("");

        N = JSONNode.LoadFromCompressedBase64(data2);
        P("反序列化 from压缩过的Base64 string:");
        P(N.ToString( ));
        P("");

        JSONClass I = new JSONClass( );
        I["version"].AsInt = 5;
        I["author"]["name"] = "Bunny83";
        I["author"]["phone"] = "0123456789";
        I["data"][-1] = "First item\twith tab";
        I["data"][-1] = "Second item";
        I["data"][-1]["value"] = "class item";
        I["data"].Add("Forth item");
        I["data"][1] = I["data"][1] + " 'addition to the second item'";
        I.Add("version", "1.0");

        P("Second example:");
        P(I.ToString( ));
        P("");

        P("I[\"data\"][0]            : " + I["data"][0]);
        P("I[\"data\"][0].ToString() : " + I["data"][0].ToString( ));
        P("I[\"data\"][0].Value      : " + I["data"][0].Value);
        P(I.ToString(""));

        string jsonFile = Application.streamingAssetsPath + @"/SecondExample.dat";
        // 保存JSON文件
        SimpleJsonTool.saveCompressedJsonFile(I.ToString(""), jsonFile);
        // 载入JSON文件
        string loadJsonFile = SimpleJsonTool.loadCompressedJsonFile(jsonFile);
        P("");
        P("读取SecondExample.dat");
        P(loadJsonFile);
    }

    void Start ( ) {
        Test( );
        Debug.Log("Test results:\n" + m_InGameLog.ToString( ));
    }
    void OnGUI ( ) {
        m_Position = GUILayout.BeginScrollView(m_Position);
        GUILayout.Label(m_InGameLog.ToString( ));
        GUILayout.EndScrollView( );
    }
}