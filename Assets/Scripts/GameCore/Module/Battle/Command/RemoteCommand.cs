using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using FixedMathSharp;
using GC.Utils.Define;
using UnityEngine;

namespace GC.Module.Command
{
	/// <summary>
	/// 내부적으로 사용하는 명령
	/// </summary>
	public class RemoteCommand : PoolingObject<RemoteCommand>
	{
		public DefineBattle.RemoteCommandType CommandType { get; set; }
		public List<int> IntParams;
		public List<long> LongParams;
		public List<Fixed64> FixedParams;

		public RemoteCommand()
		{
			IntParams = new List<int>();
			LongParams = new List<long>();
			FixedParams = new List<Fixed64>();
		}

		/// <summary>
		/// 보유중인 데이터를 비운다.
		/// </summary>
		public void Reset()
		{
			IntParams.Clear();
			LongParams.Clear();
			FixedParams.Clear();
		}

		public int GetIntParam(DefineBattle.RemoteCommandType commandType, int index)
		{
			if (CommandType != commandType)
			{
				ELog.LogError($"CommandType mismatch: {commandType}");
				return 0;
			}
			
			if (IntParams.Count <= index)
			{
				ELog.LogError($"IntParams[{index}] missing");
				return 0;
			}
			
			return IntParams[index];
		}
		
		public long GetLongParam(DefineBattle.RemoteCommandType commandType, int index)
		{
			if (CommandType != commandType)
			{
				ELog.LogError($"CommandType mismatch: {commandType}");
				return 0;
			}
			
			if (LongParams.Count <= index)
			{
				ELog.LogError($"LongParams[{index}] missing");
				return 0;
			}
			
			return LongParams[index];
		}

		public Fixed64 GetFixedParam(DefineBattle.RemoteCommandType commandType, int index)
		{
			if (CommandType != commandType)
			{
				ELog.LogError($"CommandType mismatch: {commandType}");
				return Fixed64.Zero;
			}
			
			if (FixedParams.Count <= index)
			{
				ELog.LogError($"FixedParams[{index}] missing");
				return Fixed64.Zero;
			}
			
			return FixedParams[index];
		}
	}
	
	/// <summary>
	/// 캐릭터를 필드에 소환한다.
	/// </summary>
	public static class RemoteSpawnCharacterAtFieldCommand
	{
		public static void Set(RemoteCommand command, long unitIdx, int ownerId, int characterId, int fieldId)
		{
			command.Reset();
			command.CommandType = DefineBattle.RemoteCommandType.SpawnCharacterAtField;
			command.LongParams.Add(unitIdx);
			command.IntParams.Add(ownerId);
			command.IntParams.Add(characterId);
			command.IntParams.Add(fieldId);
		}

		public static long UnitIdx(RemoteCommand remoteCommand) => remoteCommand.GetLongParam(DefineBattle.RemoteCommandType.SpawnCharacterAtField, 0);
		public static int OwnerID(RemoteCommand remoteCommand) => remoteCommand.GetIntParam(DefineBattle.RemoteCommandType.SpawnCharacterAtField, 0);
		public static int CharacterID(RemoteCommand remoteCommand) => remoteCommand.GetIntParam(DefineBattle.RemoteCommandType.SpawnCharacterAtField, 1);
		public static int FieldID(RemoteCommand remoteCommand) => remoteCommand.GetIntParam(DefineBattle.RemoteCommandType.SpawnCharacterAtField, 2);
	}
	
	/// <summary>
	/// 캐릭터를 대기석에 소환한다.
	/// </summary>
	public static class RemoteSpawnCharacterAtWaitCommand
	{
		public static void Set(RemoteCommand command, long unitIdx, int ownerId, int characterId, int waitId)
		{
			command.Reset();
			command.CommandType = DefineBattle.RemoteCommandType.SpawnCharacterAtWait;
			command.LongParams.Add(unitIdx);
			command.IntParams.Add(ownerId);
			command.IntParams.Add(characterId);
			command.IntParams.Add(waitId);
		}
		
		public static long UnitIdx(RemoteCommand remoteCommand) => remoteCommand.GetLongParam(DefineBattle.RemoteCommandType.SpawnCharacterAtField, 0);
		public static int OwnerID(RemoteCommand remoteCommand) => remoteCommand.GetIntParam(DefineBattle.RemoteCommandType.SpawnCharacterAtField, 0);
		public static int CharacterID(RemoteCommand remoteCommand) => remoteCommand.GetIntParam(DefineBattle.RemoteCommandType.SpawnCharacterAtField, 1);
		public static int WaitID(RemoteCommand remoteCommand) => remoteCommand.GetIntParam(DefineBattle.RemoteCommandType.SpawnCharacterAtField, 2);
	}
}