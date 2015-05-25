using UnityEngine;

public static class U3dPathUtil {    

    public static string U3dStreamPath ( ) {
        string u3dStreamPath = null;

        switch (Application.platform) { 
            case RuntimePlatform.WindowsEditor:
                u3dStreamPath = FileStreamingAssetsPath( );
                break;
            case RuntimePlatform.WindowsPlayer:
                u3dStreamPath = FileStreamingAssetsPath( );
                break;
            case RuntimePlatform.IPhonePlayer:
                u3dStreamPath = FileStreamingAssetsPath( );
                break;
            case RuntimePlatform.Android:
                u3dStreamPath = AndroidStreamingAssetsPath( );
                break;
            case RuntimePlatform.WindowsWebPlayer:
                u3dStreamPath = WebStreamingAssetsPath( );
                break;
            default:
                Debug.LogError("U3dStreamPath不支持的平台");
                u3dStreamPath = "不支持的平台";
                break;
        }
        Debug.Log("运行的平台：" + Application.platform + ", StreamPath=" + u3dStreamPath);
        return u3dStreamPath;
    }

    // 此路径可以在PC, IOS上使用
    static string FileStreamingAssetsPath ( ) {
        string path = @"file://" + Application.streamingAssetsPath + @"/";        
        return path;
    }
    // 此路径可以在Android上使用
    static string AndroidStreamingAssetsPath ( ) {
        string path = Application.streamingAssetsPath + @"/";
        return path;
    }
    // 此路径可以在WEB上使用
    static string WebStreamingAssetsPath ( ) {
        string path = Application.dataPath + @"/StreamingAssets/";       
        return path;
    }

}
