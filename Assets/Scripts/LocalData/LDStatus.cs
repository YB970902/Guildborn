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
		/// 체력
		/// </summary>
		public int Health;
		/// <summary>
		/// 공격속도
		/// </summary>
		public double AttackSpeed;
		/// <summary>
		/// 이동속도
		/// </summary>
		public double MoveSpeed;
		/// <summary>
		/// 공격 범위
		/// </summary>
		public int AttackRange;
	}
}