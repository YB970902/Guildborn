using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC.FSM
{
	public class MoveStateBase : StateBase
	{

		public override bool CheckType(IBlackboard blackboard)
		{
			return blackboard is IMovableBlackboard;
		}

		public override void Enter(IBlackboard blackboard)
		{
			
		}

		public override void Execute(IBlackboard blackboard)
		{
			
		}

		public override void Exit(IBlackboard blackboard)
		{
			
		}
	}
}