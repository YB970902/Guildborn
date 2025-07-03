using System.Collections;
using System.Collections.Generic;
using Bo;
using UnityEngine;

namespace GC.FSM
{
	/// <summary>
	/// 상태에 영향을 줄 수 있는 정보를 담는 인터페이스. 무조건 이 인터페이스를 구현해야 한다.
	/// </summary>
	public interface IBlackboard
	{
		public void Init();
	}

	/// <summary>
	/// 스테이터스 정보가 있는 블랙보드
	/// </summary>
	public interface IStatusBlackboard
	{
		public BoStatus OriginalStatus { get; }
	}

	/// <summary>
	/// 피해를 입을 수 있는 블랙보드
	/// </summary>
	public interface IDamageableBlackboard
	{
		public bool IsDead { get; }
		public int CurrentHealth { get; }
	}
}