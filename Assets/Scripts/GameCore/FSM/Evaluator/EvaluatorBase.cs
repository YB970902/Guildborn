using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC.FSM
{
    /// <summary>
    /// 제네릭 클래스를 인스턴스로 갖기위한 인터페이스
    /// </summary>
    public interface IEvaluatorBase
    {
        /// <summary> 지연 초기화를 위한 함수 </summary>
        public void Init();

        /// <summary>
        /// 블랙보드를 기준으로 현재 상태를 평가한다. 
        /// </summary>
        public string Evaluate();
    }
    
    /// <summary>
    /// Blackboard를 통해 현재 상태를 체크하고, 그 상태로 전이시키는 클래스
    /// </summary>
    public class EvaluatorBase<T> : IEvaluatorBase where T : IBlackboard
    {
        /// <summary>
        /// 상태와 관련된 데이터
        /// </summary>
        public T Blackboard { get; private set; }
        
        public EvaluatorBase(T blackboard)
        {
	        Blackboard = blackboard;
        }

        public virtual void Init()
        {
            Blackboard.Init();
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