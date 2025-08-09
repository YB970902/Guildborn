using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace GC
{
    /// <summary>
    /// 에디터에서만 보이는 로그
    /// 빌드하면 보이지 않는다.
    /// </summary>
    public static class ELog
    {
        [Conditional("UNITY_EDITOR")]
        public static void Log(string msg)
        {
            Debug.Log($"[E] {msg}");
        }

        [Conditional("UNITY_EDITOR")]
        public static void LogError(string msg)
        {
            Debug.LogError($"[E] {msg}");
        }

        [Conditional("UNITY_EDITOR")]
        public static void LogWarning(string msg)
        {
            Debug.LogWarning($"[E] {msg}");
        }
    }
}