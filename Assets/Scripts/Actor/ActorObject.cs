using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC.Actor
{
    /// <summary>
    /// 시각적으로 보여지는 모든 부분을 담당하는 오브젝트
    /// </summary>
    public class ActorObject : MonoBehaviour
    {
        [SerializeField] SPUM_Prefabs spum;
        
        public void Init()
        {
            spum.OverrideControllerInit();
            spum.PlayAnimation(PlayerState.IDLE, 0);
        }
        
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}