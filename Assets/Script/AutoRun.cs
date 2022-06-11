//#define AutoRun

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Editorでのスクリプト起動時に初期化処理を実行
/// </summary>
/// 
#if AutoRun
[UnityEditor.InitializeOnLoad]
public class AutoRun : MonoBehaviour {
    static AutoRun() {
        Debug.Log ("*** Debug.Log hooked by Autorun.LogCallbackHandler()");
        Application.RegisterLogCallback(LogCallbackHandler);
    }

    static void LogCallbackHandler(string condition, string stackTrace, LogType type) {
        if (type == LogType.Exception) {
           // Unity Editor 上で実行中の場合のみ実行を一時停止する
            if (UnityEditor.EditorApplication.isPlaying) {
                UnityEditor.EditorApplication.isPaused = true;
            }
        }
    }
}
#endif
