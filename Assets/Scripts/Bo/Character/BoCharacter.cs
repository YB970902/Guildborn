using System.Collections;
using System.Collections.Generic;
using BC.LocalData;
using GC.FSM;
using UnityEngine;

namespace Bo
{
	public class BoCharacter
	{
		/// <summary> 캐릭터를 구분할 수 있는 고유 인덱스 </summary>
		public long CharacterIdx { get; private set; }
		/// <summary> 캐릭터 소유자의 아이디 </summary>
		public int OwnerID { get; private set; }
		/// <summary>
		/// 캐릭터의 상태를 가지고있는 블랙보드
		/// </summary>
		private BbCharacter blackboard;
		
		private FiniteStateMachine stateMachine;

		public BoCharacter(LDStatus ldStatus)
		{
			blackboard = new BbCharacter(ldStatus);
			stateMachine = new FiniteStateMachine(new EvCharacter(blackboard));
		}

		public void Init()
		{
			stateMachine.Init();
		}
	}
}