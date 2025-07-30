using System.Collections;
using System.Collections.Generic;
using BC;
using BC.Utils;
using UnityEngine;

namespace GC
{
    /// <summary>
    /// 게임을 로딩할 때 순서대로 로딩할 수 있도록 처리해주는 매니저
    /// </summary>
    public class BootstrapManager : MonoSingleton<BootstrapManager>
    {
        private IEnumerator Start()
        {
            // GameCore, BeanCore 모두 초기화될 때까지 대기한다.
            while (GC.GameCore.IsInit == false && BeanCore.IsInit == false) yield return null;

            BeanCore.Instance.Init();
            GC.GameCore.Instance.Init();
            
            // 지금부터 시작
        }
    }
}