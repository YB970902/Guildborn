using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC.FSM
{
    /// <summary>
    /// Blackboard를 통해 현재 상태를 체크하고, 그 상태로 전이시키는 클래스
    /// </summary>
    public class EvaluatorBase<T> where T : IBlackboard
    {
        /// <summary>
        /// 상태와 관련된 데이터
        /// </summary>
        public T Blackboard { get; private set; }

        public EvaluatorBase(IBlackboard blackboard)
        {
	        Blackboard = (T)blackboard;
        }

        /// <summary>
        /// 블랙보드를 참고하여 현재 상태를 파악한다.
        /// 문자열을 반환하며, 이를 파싱하여 상태 데이터로 쓴다.
        /// </summary>
        public virtual string Evaluate()
        {
            return string.Empty;
        }
    }
}