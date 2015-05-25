using UnityEngine;
using System.Collections;

public static class W3Util {

    public static IEnumerator W3GetRequest (string url, System.Action<WWW, string> w3OkFinishAction) {
        Debug.Log("url::>>>"+url);

        using (WWW w3 = new WWW(url)) {
            while (!w3.isDone) {
                yield return 0;
            }
            if (string.IsNullOrEmpty(w3.error)) {
                if (null != w3OkFinishAction) {
                    // www一切OK，调用Action
                    w3OkFinishAction(w3, null);
                } else {
                    // Action为null
                    Debug.LogError("finishAction is null");
                }
            } else {
                w3OkFinishAction(w3, w3.error);                
            }
        };
    }

}
