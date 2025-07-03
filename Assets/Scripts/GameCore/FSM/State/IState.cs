using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC.FSM
{
    public interface IState
    {
        /// <summary> 현재 상태에 진입한다. </summary>
        public void Enter();
        /// <summary> 현재 상태를 실행한다. </summary>
        public void Execute();
        /// <summary> 현재 상태를 종료한다. </summary>
        public void Exit();
    }
}