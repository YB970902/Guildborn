using System.Collections;
using System.Collections.Generic;
using MemoryPack;
using UnityEngine;

namespace BC.LocalData
{
	[MemoryPackable]
	public partial class LDStatus : LocalDataBase
	{
		/// <summary>
		/// 공격력
		/// </summary>
		public int Attack;
		/// <summary>
		/// 방어력
		/// </summary>
		public int Defence;
		/// <summary>
		/// 최대 체력
		/// </summary>
		public int MaxHealth;
		/// <summary>
		/// 공격속도
		/// </summary>
		public double AttackSpeed;
		/// <summary>
		/// 이동속도(타일 한 칸 건너는데 걸리는 시간)
		/// </summary>
		public double MoveSpeed;
		/// <summary>
		/// 공격 범위
		/// </summary>
		public int AttackRange;
	}
}