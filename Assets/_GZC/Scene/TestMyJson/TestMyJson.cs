using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;


//注意JSON里字符串的表示，用“”包起来的，所以在C#的字符串里直接写JSON字符串要加\等问题，或者粗心把JSON里的字符串当数字解析
//最好用在线JSON字符串合法性网页检查下JSON字符串写的是否合法。
//国内的一个在线检查JSON字符串合法性：
//http://www.bejson.com/


public class TestMyJson : MonoBehaviour {
    
    string jsonStr2;
    string jsonStr = "{\"name\":\"test\", \"array\":[1,{\"data\":\"value\"}]}";
	void Start () {       
        MyJson.JsonNode_Object dic = MyJson.Parse(jsonStr) as MyJson.JsonNode_Object;
        Debug.Log(dic.HaveDictItem("name"));
        Debug.Log(dic["name"].AsString( ));

        StringBuilder sb = new StringBuilder( );
        dic.ConvertToStringWithFormat(sb, 0);   //带排版
        Debug.Log(sb.ToString( ));

        IList<MyJson.IJsonNode> arr = dic["array"].AsList( );
        Debug.Log(arr[0]);
        Debug.Log(arr[1].asDict( )["data"].AsString( ));

        //==================================

        jsonStr2 = File.ReadAllText(Application.streamingAssetsPath + @"/MyJsonFile/AA.txt");
        Debug.Log(jsonStr2);

        MyJson.JsonNode_Object root = MyJson.Parse(jsonStr2) as MyJson.JsonNode_Object;
        MyJson.JsonNode_Object node = root["root"] as MyJson.JsonNode_Object;
        var animSequenceObjVal = node["animSequence"] as MyJson.JsonNode_Object;
        Debug.Log(animSequenceObjVal["startTime"].AsString( ));


        //==============二进制位协议====================
        using (FileStream fs = File.Create(Application.streamingAssetsPath + @"/TestMyJsonCompress.txt")) {
            MyJsonBinary.Write(fs, dic);
            fs.Flush( );
            fs.Close( );
            Debug.Log("使用MyJsonCompress将JSON对象写入二进制流OK");
        }

        byte[ ] bytesJson = File.ReadAllBytes(Application.streamingAssetsPath + @"/TestMyJsonCompress.txt");
        using (MemoryStream ms = new MemoryStream(bytesJson)) {
            MyJson.IJsonNode node2 = MyJsonBinary.Read(ms);
            Debug.Log("使用MyJsonCompress从二进制流读取JSON对象OK");

            node2.ConvertToStringWithFormat(sb, 0);   //带排版
            Debug.Log(sb.ToString( ));

            ms.Close( );            
        }
        
	}

}
