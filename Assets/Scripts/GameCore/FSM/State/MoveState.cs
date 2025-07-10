using System.Collections;
using System.Collections.Generic;
using Bo;
using GC.Module;
using UnityEngine;

namespace GC.FSM
{
	/// <summary>
	/// 목표 타일까지 이동한다.
	/// </summary>
	public class MoveState : StateBase
	{
		public override bool CheckType(IBlackboard blackboard)
		{
			return blackboard is IMovableBlackboard and IStatusBlackboard;
		}

		public override void Enter(IBlackboard blackboard)
		{
			// TODO : 이동 모션 시작
		}

		public override void Execute(IBlackboard blackboard)
		{
			// 이동시킨다.
			BoStatus boStatus = ((IStatusBlackboard)blackboard).CurrentStatus;
			PathFindHandler handler = ((IMovableBlackboard)blackboard).PathFindHandler;
			handler.Move(boStatus.MoveSpeed);
		}

		public override void Exit(IBlackboard blackboard)
		{
			// TODO : 이동 모션 종료. Idle모션 수행
		}
	}
}