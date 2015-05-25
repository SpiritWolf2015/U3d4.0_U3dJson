using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SimpleJSON;


// SimpleJSON的解析


public class TestParse : MonoBehaviour {

    string jsonStr =@"{name: nmb, sex: male,age:25}";
    string jsonStr2 = @"{kecheng: [c,c++,java,c#,mysql,oracle]}";

    class Person {
        public string name;
        public string sex;
        public int age;

        public override string ToString ( ) {
            return "name=" + name + ", sex=" + sex + ", age=" + age;
        }
    }
	
	void Start () {
        #region 解析JSON字符串
        
        SimpleJSON.JSONNode node = JSON.Parse(jsonStr);
        SimpleJSON.JSONClass jsonClass = node.AsObject;
        Debug.Log("不带排版格式=" + jsonClass.ToString( ));   // 不带排版格式
        Debug.Log("带排版格式=" + jsonClass.ToString(""));   // 带排版格式

        Person p = new Person( );
        p.name = jsonClass[0];  // jsonClass["name"];
        p.sex = jsonClass[1];
        p.age = node["age"].AsInt;
        Debug.Log(p.ToString( ));

        string formatJsonStr_ = SimpleJsonTool.formatJsonStr(jsonStr2);
        Debug.Log(formatJsonStr_);
        SimpleJSON.JSONNode node2 = JSON.Parse(jsonStr2);
        SimpleJSON.JSONArray jsonArray = node2["kecheng"].AsArray;
        foreach (SimpleJSON.JSONNode node3 in jsonArray) {
            Debug.Log(node3);
        }
        #endregion 解析JSON字符串
    }

}
