using System;
using UnityEngine;

public class TextTest : MonoBehaviour {

    public GameObject TextObject;

    private TextMesh _statusText;
    private DateTime _refTime;
    private int _testNum = 5;
    private JsonTestScript _tester;
    private bool _complete;

    float m_waitSecond = 1F;
    float m_delaySecond = 0F;
   
    void Start ( ) {
        //Set the test starting point
        _testNum = 0;
        _statusText = TextObject.GetComponent<TextMesh>( );
        _statusText.text = string.Format("-- SERIALIZATION TESTS 序列化测试 -- \r\n Tests are run with \r\n a {0} second delay \r\n Starting in {1} seconds.", m_delaySecond, m_waitSecond + m_delaySecond);
        _tester = new JsonTestScript(_statusText);
        _refTime = DateTime.Now.AddSeconds(m_waitSecond);
    }
    
    void Update ( ) {
        if (!_complete && (DateTime.Now - _refTime).TotalSeconds >= m_delaySecond) {
            _testNum++;
            RunNextTest( );
            _refTime = DateTime.Now;
        }
    }

    private void RunNextTest ( ) {
        switch (_testNum) {
            case 1:
                _tester.SerializeVector3( );
                break;
            case 2:
                _tester.GenericListSerialization( );
                break;
            case 3:
                _tester.PolymorphicSerialization( );
                break;
            case 4:
                _tester.DictionarySerialization( );
                break;
            case 5:
                _tester.DictionaryObjectValueSerialization( );
                break;
            case 6:
                _tester.DictionaryObjectKeySerialization( );
                break;
            default:
                _complete = true;
                _statusText.text = "Tests Complete 测试完成\r\nSee Console for Log 查看控制台输出";
                break;
        }
    }

}
