using System.Collections;
using System.Collections.Generic;
using Bo;
using GC.Module;
using UnityEngine;

namespace GC.FSM
{
	/// <summary>
	/// 대가 상태
	/// </summary>
	public class IdleState : StateBase
	{
		public override bool CheckType(IBlackboard blackboard)
		{
			return true;
		}

		public override void Enter(IBlackboard blackboard)
		{
			// TODO : 대기 모션 시작
		}

		public override void Execute(IBlackboard blackboard)
		{
			
		}

		public override void Exit(IBlackboard blackboard)
		{
			// TODO : 대기 모션 종료
		}
	}
}