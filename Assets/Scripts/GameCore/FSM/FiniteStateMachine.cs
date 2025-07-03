using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC.FSM
{
	/// <summary>
	/// 유한상태머신.
	/// Evaluator와 Blackboard를 가지고 있으며, 상태를 추가할 수 있다.
	/// 자체적인 업데이트는 없고, 외부에서만 업데이트 할 수 있다.
	/// </summary>
	public class FiniteStateMachine
	{
		private IEvaluatorBase evaluator;

		/// <summary> 현재 상태의 키 </summary>
		private string currStateKey;

		private Dictionary<string, IState> states;

		/// <summary>
		/// 블랙보드는 외부에서 수정할 수 있어야 하므로, 외부에서 생성해서 넣어준다.
		/// </summary>
		public FiniteStateMachine(IEvaluatorBase evaluator)
		{
			this.evaluator = evaluator;
			currStateKey = string.Empty;
		}

		public void Init()
		{
			evaluator.Init();
		}

		/// <summary>
		/// 현재 상태를 실행한다.
		/// </summary>
		public void Execute()
		{
			// 정보를 기준으로 상태를 평가한다.
			string stateKey = evaluator.Evaluate();
			IState currState = states[currStateKey];
			
			// 상태가 바뀌었다면 상태 전환을 수행한다. 
			if (stateKey != currStateKey)
			{
				currState.Exit();
				currStateKey = stateKey;
				currState = states[currStateKey];
				currState.Enter();
			}

			// 현재 상태를 수행한다.
			currState.Execute();
		}
	}
}