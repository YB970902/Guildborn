using System.Collections;
using System.Collections.Generic;
using BC;
using BC.LocalData;
using GC;
using GC.FSM;
using GC.Module;
using UnityEngine;

namespace Bo
{
	public class BoCharacter
	{
		/// <summary> 캐릭터를 구분할 수 있는 고유 인덱스 </summary>
		public long UnitIdx { get; private set; }
		/// <summary> 캐릭터 소유자의 아이디 </summary>
		public int OwnerID { get; private set; }
		/// <summary> 캐릭터의 상태를 가지고있는 블랙보드 </summary>
		private BbCharacter blackboard;
		/// <summary> 길찾기를 위한 핸들러 </summary>
		private PathFindHandler pathFindHandler;
		
		private FiniteStateMachine stateMachine;

		public BoCharacter(long unitIdx, int ownerID, LDCharacter ldCharacter)
		{
			UnitIdx = unitIdx;
			OwnerID = ownerID;
			LDStatus ldStatus = BeanCore.Instance.LD.Status[ldCharacter.StatusID];
			pathFindHandler = GameCore.Instance.Battle.PathFind.GetHandler();
			blackboard = new BbCharacter(ldStatus, pathFindHandler);
			stateMachine = new FiniteStateMachine(new EvCharacter(blackboard), blackboard);
			stateMachine.AddState("Move", new MoveState());
			stateMachine.AddState("Idle", new IdleState());
		}

		public void Init()
		{
			stateMachine.Init();
		}
		
		public void Update()
		{
			// TODO : 버프 처리하기
			stateMachine.Execute();
		}
	}
}