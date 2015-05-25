using UnityEngine;
using System.IO;
using SimpleJSON;


public class TestSimpleJSON_BinarySerialize : MonoBehaviour {

    string jsonStr;

    void Start ( ) {
        string path = Application.streamingAssetsPath + @"/MyJsonFile/GoTransformJson.txt";
        jsonStr = File.ReadAllText(path);
        SimpleJSON.JSONNode node = JSON.Parse(jsonStr);

        #region JSONNode序列化和反序列化

        string dataFile = Application.streamingAssetsPath + @"/SimpleJSON_BinarySerialize.txt";
        using (FileStream fs = new FileStream(dataFile, FileMode.Create)) {
            using (BinaryWriter bw = new BinaryWriter(fs)) {
                bw.Flush( );
                // SimpleJSON序列化
                node.Serialize(bw);
                Debug.LogWarning("序列化OK");
                bw.Close( );
                fs.Close( );
            }
        }
        using (BinaryReader br = new BinaryReader(File.OpenRead(dataFile))) {
            // SimpleJSON反序列化
            JSONNode node2 = JSONNode.Deserialize(br);
            Debug.LogWarning("反序列化->" + node2.ToString(""));
            br.Close( );
        }
        #endregion JSONNode序列化和反序列化
    }

}
