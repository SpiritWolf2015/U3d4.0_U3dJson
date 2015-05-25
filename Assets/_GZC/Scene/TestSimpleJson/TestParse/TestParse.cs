using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SimpleJSON;


// SimpleJSON�Ľ���


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
        #region ����JSON�ַ���
        
        SimpleJSON.JSONNode node = JSON.Parse(jsonStr);
        SimpleJSON.JSONClass jsonClass = node.AsObject;
        Debug.Log("�����Ű��ʽ=" + jsonClass.ToString( ));   // �����Ű��ʽ
        Debug.Log("���Ű��ʽ=" + jsonClass.ToString(""));   // ���Ű��ʽ

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
        #endregion ����JSON�ַ���
    }

}
