using UnityEngine;
using System;
using System.IO;
//JsonDotNet
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;


//用json.net序列化可以满足要求，也支持bson，不过需要额外的读写器。
//json.net，移动平台。支持ios

public class TestJsonDotNet : MonoBehaviour {

    class People {
        public string name;
        public int id;
    }

    void Start ( ) {
        People original = new People( );
        original.name = "乃木板";
        original.id = 46;

        //Placeholder to hold the serialized data so we can deserialize it later
        byte[ ] serializedData = null;   

        // People对象->Json字符串
        string strJson = JsonConvert.SerializeObject(original);
        Debug.LogWarning("序列化People对象->Json字符串为" + strJson);
        //File.WriteAllText(Application.streamingAssetsPath + @"/MyJsonFile/People.txt", strJson, System.Text.Encoding.UTF8);

        //Create a memory stream to hold the serialized bytes
        using (MemoryStream stream = new MemoryStream( )) {
            using (BsonWriter writer = new BsonWriter(stream)) {
                JsonSerializer serializer = new JsonSerializer( );
                // 序列化，输出，写到内存
                serializer.Serialize(writer, original);               
                writer.Flush( );
                writer.Close( );                
            }
            //Read the stream to a byte array.  We could 
            //just as easily output it to a file
            serializedData = stream.ToArray( );  //stream.GetBuffer( );  
            File.WriteAllBytes(Application.streamingAssetsPath + @"/JsonDotNet_BsonSerializer.txt", serializedData);

            //You could write the raw bytes to a file, here we're converting 
            //them to a base-64 string and writing out to the debug log
            string strSerializedBase64 = Convert.ToBase64String(serializedData);
            Debug.Log("People对象序列化为字节数组后，字节数组转成Base64字符串为" + strSerializedBase64);
            stream.Close( );
        }

        using (MemoryStream stream = new MemoryStream(serializedData)) {
            using (BsonReader reader = new BsonReader(stream)) {
                //If you're deserializing a collection, the following option 
                //must be set to instruct the reader that the root object 
                //is actually an array / collection type.             
                //reader.ReadRootValueAsArray = true;
                JsonSerializer serializer = new JsonSerializer( );
                // 读入，反序列化，还原对象
                People newObject = serializer.Deserialize<People>(reader);
                Debug.Log(string.Format("name={0}，id={1}", newObject.name, newObject.id));    

                reader.Close( );
                stream.Close( );
            }
        }

    }

}
