using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using DocumentFormat.OpenXml.Spreadsheet;
using FixedMathSharp;
using UnityEngine;
using GC.Utils.Define;

namespace GC.Module.Command
{
	/// <summary>
	/// 내부적으로 사용하는 명령
	/// </summary>
	public class LocalCommand : PoolingObject<LocalCommand>
	{
		public DefineBattle.LocalCommandType CommandType { get; set; }
		public List<int> IntParams;
		public List<long> LongParams;
		public List<Fixed64> FloatParams;

		public LocalCommand()
		{
			IntParams = new List<int>();
			LongParams = new List<long>();
			FloatParams = new List<Fixed64>();
		}

		/// <summary>
		/// 보유중인 데이터를 비운다.
		/// </summary>
		public void Reset()
		{
			IntParams.Clear();
			LongParams.Clear();
			FloatParams.Clear();
		}
	}

	public class LocalAttackCommand : LocalCommand
	{
		/// <summary>
		/// 공격자
		/// </summary>
		public long Attacker => LongParams[0];
		/// <summary>
		/// 피격 대상
		/// </summary>
		public long Target => LongParams[1];

		public static LocalAttackCommand Set(in LocalCommand command, long attackerIdx, long targetIdx)
		{
			command.Reset();
			command.LongParams.Add(attackerIdx);
			command.LongParams.Add(targetIdx);
			command.CommandType = DefineBattle.LocalCommandType.Attack;

			return command as LocalAttackCommand;
		}
	}
	
	public class LocalUseSkillCommand : LocalCommand
	{
		/// <summary>
		/// 공격자
		/// </summary>
		public long Attacker => LongParams[0];
		public int SkillID => IntParams[0];

		public static LocalAttackCommand Set(in LocalCommand command, long attackerIdx, int skillID)
		{
			command.Reset();
			command.LongParams.Add(attackerIdx);
			command.IntParams.Add(skillID);
			command.CommandType = DefineBattle.LocalCommandType.UseSkill;

			return command as LocalAttackCommand;
		}
	}
}