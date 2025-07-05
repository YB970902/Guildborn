using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC.FSM
{

    /// <summary>
    /// 상태의 기본 클래스
    /// 블랙보드를 파라미터로 넘겨받으면, 그 파라미터를 통해서 상태를 실행한다.
    /// 파라미터에 필요한 정보가 없을수도 있으므로, 이를 Init에서 감별한다.
    /// </summary>
    public abstract class StateBase
    {
        /// <summary>
        /// Blackboard가 필요한 정보를 담은 인터페이스를 구현했는지를 체크한다.
        /// </summary>
        public abstract bool CheckType(IBlackboard blackboard);
        /// <summary>
        /// 현재 상태에 진입한다.
        /// </summary>
        public abstract void Enter(IBlackboard blackboard);
        /// <summary>
        /// 현재 상태를 실행한다.
        /// </summary>
        public abstract void Execute(IBlackboard blackboard);
        /// <summary>
        /// 현재 상태를 종료한다.
        /// </summary>
        public abstract void Exit(IBlackboard blackboard);
    }
}