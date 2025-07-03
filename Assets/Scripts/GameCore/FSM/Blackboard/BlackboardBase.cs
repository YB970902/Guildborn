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
	/// 스테이터스 정보 블랙보드
	/// </summary>
	public interface IStatusBlackboard
	{
		/// <summary>
		/// 원본 스테이터스 정보
		/// </summary>
		public BoStatus OriginalStatus { get; }
	}

	/// <summary>
	/// 피해와 관련된 블랙보드
	/// </summary>
	public interface IDamageableBlackboard
	{
		/// <summary>
		/// 죽었는지 상태. 죽은 상태에선 체력이 회복될 수 없고, 회복된다고 해서 살아나지 않는다.
		/// </summary>
		public bool IsDead { get; }
		/// <summary>
		/// 현재 체력. 최대 체력을 넘을 수 없다.
		/// </summary>
		public int CurrentHealth { get; }
	}

	/// <summary>
	/// 이동과 관련된 블랙보드
	/// </summary>
	public interface IMovableBlackboard
	{
		/// <summary>
		/// 현재 서 있는 타일 정보
		/// </summary>
		public Vector2Int CurrentTile { get; }
		/// <summary>
		/// 목적지 타일
		/// </summary>
		public Vector2Int DestTile { get; }
		/// <summary>
		/// 목적지에 도착했는지 여부
		/// </summary>
		public bool IsArrived { get; }
	}
}