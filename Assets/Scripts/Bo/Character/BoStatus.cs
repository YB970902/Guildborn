using System.Collections;
using System.Collections.Generic;
using BC.LocalData;
using FixedMathSharp;
using UnityEngine;

namespace Bo
{
	public class BoStatus
	{
		/// <summary> 공격력 </summary>
		public int Attack { get; private set; }
		/// <summary> 방어력 </summary>
		public int Defence { get; private set; }
		/// <summary> 최대 체력 </summary>
		public int MaxHealth { get; private set; }
		/// <summary> 공격 속도 (1회 공격 시 드는 시간) </summary>
		public Fixed64 AttackSpeed { get; private set; }
		/// <summary> 이동 속도 (1초당 이동량) </summary>
		public Fixed64 MoveSpeed { get; private set; }
		/// <summary> 공격 범위(최소 1) </summary>
		public int AttackRange { get; private set; }

		public BoStatus() { }

		public BoStatus(LDStatus status)
		{
			Attack = status.Attack;
			Defence = status.Defence;
			MaxHealth = status.MaxHealth;
			AttackSpeed = new Fixed64(status.AttackSpeed);
			MoveSpeed = new Fixed64(status.MoveSpeed);
			AttackRange = status.AttackRange;
		}

		public BoStatus(BoStatus status)
		{
			Attack = status.Attack;
			Defence = status.Defence;
			MaxHealth = status.MaxHealth;
			AttackSpeed = status.AttackSpeed;
			MoveSpeed = status.MoveSpeed;
			AttackRange = status.AttackRange;
		}

		public void AddStatus(LDStatus addition)
		{
			Attack += addition.Attack;
			Defence += addition.Defence;
			MaxHealth += addition.MaxHealth;
			AttackSpeed += new Fixed64(addition.AttackSpeed);
			MoveSpeed += new Fixed64(addition.MoveSpeed);
			AttackRange += addition.AttackRange;
		}

		public void AddStatus(BoStatus addition)
		{
			Attack += addition.Attack;
			Defence += addition.Defence;
			MaxHealth += addition.MaxHealth;
			AttackSpeed += addition.AttackSpeed;
			MoveSpeed += addition.MoveSpeed;
			AttackRange += addition.AttackRange;
		}

		public BoStatus Clone()
		{
			return new BoStatus(this);
		}
	}
}