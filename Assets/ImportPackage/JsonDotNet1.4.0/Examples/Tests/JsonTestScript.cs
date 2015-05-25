using Assets.DustinHorne.JsonDotNetUnity.TestCases;
using Assets.DustinHorne.JsonDotNetUnity.TestCases.TestModels;
using Newtonsoft.Json;
using SampleClassLibrary;

using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;


public class JsonTestScript {

    private TextMesh _text;
    private const string BAD_RESULT_MESSAGE = "Incorrect Deserialized Result";

    public JsonTestScript (TextMesh text) {
        _text = text;
    }

    /// <summary>
    /// Simple Vector3 Serialization
    /// </summary>
    public void SerializeVector3 ( ) {
        LogStart("Vector3 Serialization");
        try {

            Vector3 v = new Vector3(2, 4, 6);
            string serialized = JsonConvert.SerializeObject(v);
            Debug.LogWarning("向量序列化为Json字符串->" + serialized);
            LogSerialized(serialized);
            Vector3 v2 = JsonConvert.DeserializeObject<Vector3>(serialized);
            Debug.LogWarning("反序列化向量为" + v2);

            LogResult("4", v2.y);

            if (v2.y != v.y) {
                DisplayFail("Vector3 Serialization", BAD_RESULT_MESSAGE);
            }

            DisplaySuccess("Vector3序列化OK");
        } catch (Exception ex) {
            DisplayFail("Vector3 Serialization", ex.Message);
        }

        LogEnd(1);
    }

    /// <summary>
    /// List<T> serialization
    /// </summary>
    public void GenericListSerialization ( ) {
        LogStart("List<T> Serialization");

        try {
            List<SimpleClassObject> objList = new List<SimpleClassObject>( );

            for (int i = 0; i < 4; i++) {
                objList.Add(TestCaseUtils.GetSimpleClassObject( ));
            }

            string serialized = JsonConvert.SerializeObject(objList);
            Debug.LogWarning("序列化List<SimpleClassObject>为" + serialized);
            LogSerialized(serialized);

            List<SimpleClassObject> newList = JsonConvert.DeserializeObject<List<SimpleClassObject>>(serialized);

            LogResult(objList.Count.ToString( ), newList.Count);
            LogResult(objList[2].TextValue, newList[2].TextValue);

            if ((objList.Count != newList.Count) || (objList[3].TextValue != newList[3].TextValue)) {
                DisplayFail("List<T> Serialization", BAD_RESULT_MESSAGE);
                Debug.LogError("Deserialized List<T> has incorrect count or wrong item value");
            } else {
                DisplaySuccess("List<T>序列化OK");
            }
        } catch (Exception ex) {
            DisplayFail("List<T> Serialization", ex.Message);
            throw;
        }

        LogEnd(2);
    }

    /// <summary>
    /// Polymorphism
    /// </summary>
    public void PolymorphicSerialization ( ) {
        LogStart("Polymorphic Serialization");

        try {
            List<SampleBase> list = new List<SampleBase>( );

            for (int i = 0; i < 4; i++) {
                list.Add(TestCaseUtils.GetSampleChid( ));
            }

            // Formatting.Indented：带排版
            string serialized = JsonConvert.SerializeObject(list, Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            Debug.LogWarning("多态序列化，序列化List<SampleBase>为" + serialized);
            LogSerialized(serialized);

            List<SampleBase> newList = JsonConvert.DeserializeObject<List<SampleBase>>(serialized, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

            SampleChild deserializedObject = newList[2] as SampleChild;

            if (deserializedObject == null) {
                DisplayFail("Polymorphic Serialization", BAD_RESULT_MESSAGE);
            } else {
                LogResult(list[2].TextValue, newList[2].TextValue);

                if (list[2].TextValue != newList[2].TextValue) {
                    DisplayFail("Polymorphic Serialization", BAD_RESULT_MESSAGE);
                } else {
                    DisplaySuccess("多态序列化OK");
                }
            }

        } catch (Exception ex) {
            DisplayFail("Polymorphic Serialization", ex.Message);
            throw;
        }


        LogEnd(3);
    }

    /// <summary>
    /// Dictionary Serialization
    /// </summary>
    public void DictionarySerialization ( ) {
        LogStart("Dictionary & Other DLL");

        try {
            SampleExternalClass o = new SampleExternalClass { SampleString = Guid.NewGuid( ).ToString( ) };

            o.SampleDictionary.Add(1, "A");
            o.SampleDictionary.Add(2, "B");
            o.SampleDictionary.Add(3, "C");
            o.SampleDictionary.Add(4, "D");

            string serialized = JsonConvert.SerializeObject(o, Formatting.Indented);
            Debug.LogWarning("字典序列化->"+serialized );
            LogSerialized(serialized);

            SampleExternalClass newObj = JsonConvert.DeserializeObject<SampleExternalClass>(serialized);

            LogResult(o.SampleString, newObj.SampleString);
            LogResult(o.SampleDictionary.Count.ToString( ), newObj.SampleDictionary.Count);

            var keys = new StringBuilder(4);
            var vals = new StringBuilder(4);

            foreach (KeyValuePair<int, string> kvp in o.SampleDictionary) {
                keys.Append(kvp.Key.ToString( ));
                vals.Append(kvp.Value);
            }

            LogResult("1234", keys.ToString( ));
            LogResult("ABCD", vals.ToString( ));

            if ((o.SampleString != newObj.SampleString) || (o.SampleDictionary.Count != newObj.SampleDictionary.Count) ||
                (keys.ToString( ) != "1234") || (vals.ToString( ) != "ABCD")) {
                DisplayFail("Dictionary & Other DLL", BAD_RESULT_MESSAGE);
            } else {
                DisplaySuccess("Dictionary & Other DLL");
            }
        } catch (Exception ex) {
            DisplayFail("Dictionary & Other DLL", ex.Message);
            throw;
        }

    }

    /// <summary>
    /// Serialize a dictionary with an object as the value
    /// </summary>
    public void DictionaryObjectValueSerialization ( ) {
        LogStart("Dictionary (Object Value)");

        try {
            Dictionary<int, SampleBase> dict = new Dictionary<int, SampleBase>( );

            for (int i = 0; i < 4; i++) {
                dict.Add(i, TestCaseUtils.GetSampleBase( ));
            }

            string serialized = JsonConvert.SerializeObject(dict, Formatting.Indented);
            Debug.LogWarning("DictionaryObjectValueSerialization->" + serialized);
            LogSerialized(serialized);

            Dictionary<int, SampleBase> newDict = JsonConvert.DeserializeObject<Dictionary<int, SampleBase>>(serialized);

            LogResult(dict[1].TextValue, newDict[1].TextValue);

            if (dict[1].TextValue != newDict[1].TextValue) {
                DisplayFail("Dictionary (Object Value)", BAD_RESULT_MESSAGE);
            } else {
                DisplaySuccess("Dictionary (Object Value)");
            }
        } catch (Exception ex) {
            DisplayFail("Dictionary (Object Value)", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// Serialize a dictionary with an object as the key
    /// </summary>
    public void DictionaryObjectKeySerialization ( ) {
        LogStart("Dictionary (Object As Key)");

        try {
            var dict = new Dictionary<SampleBase, int>( );

            for (var i = 0; i < 4; i++) {
                dict.Add(TestCaseUtils.GetSampleBase( ), i);
            }

            string serialized = JsonConvert.SerializeObject(dict, Formatting.Indented);
            Debug.LogWarning("Dictionary (Object As Key) serialized->" + serialized);
            LogSerialized(serialized);

            _text.text = serialized;
            Dictionary<SampleBase, int> newDict = JsonConvert.DeserializeObject<Dictionary<SampleBase, int>>(serialized);


            List<SampleBase> oldKeys = new List<SampleBase>( );
            List<SampleBase> newKeys = new List<SampleBase>( );

            foreach (SampleBase k in dict.Keys) {
                oldKeys.Add(k);
            }

            foreach (SampleBase k in newDict.Keys) {
                newKeys.Add(k);
            }

            LogResult(oldKeys[1].TextValue, newKeys[1].TextValue);

            if (oldKeys[1].TextValue != newKeys[1].TextValue) {
                DisplayFail("Dictionary (Object As Key)", BAD_RESULT_MESSAGE);
            } else {
                DisplaySuccess("Dictionary (Object As Key)");
            }
        } catch (Exception ex) {
            DisplayFail("Dictionary (Object As Key)", ex.Message);
            throw;
        }
    }


    #region Private Helper Methods

    private void DisplaySuccess (string testName) {
        _text.text = testName + "\r\nSuccessful";
    }

    private void DisplayFail (string testName, string reason) {
        try {
            _text.text = testName + "\r\nFailed :( \r\n" + reason ?? string.Empty;
        } catch {
            Debug.Log("%%%%%%%%%%%" + testName);
        }

    }

    private void LogStart (string testName) {
        Log(string.Empty);
        Log(string.Format("======= SERIALIZATION TEST: {0} ==========", testName));
    }

    private void LogEnd (int testNum) {
        //Log(string.Format("====== SERIALIZATION TEST #{0} COMPLETE", testNum));
    }

    private void Log (object message) {
        Debug.Log(message);
    }

    private void LogSerialized (string message) {

        Debug.Log(string.Format("#### Serialized Object: {0}", message));
    }

    private void LogResult (string shouldEqual, object actual) {
        Log("--------------------");
        Log(string.Format("*** Original Test value: {0}", shouldEqual));
        Log(string.Format("*** Deserialized Test Value: {0}", actual));
        Log("--------------------");
    }
    #endregion

}
