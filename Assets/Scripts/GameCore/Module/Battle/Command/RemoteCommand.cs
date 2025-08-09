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
		public List<Fixed64> FloatParams;

		public RemoteCommand()
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
	
	/// <summary>
	/// 캐릭터를 필드에 소환한다.
	/// </summary>
	public static class RemoteSpawnCharacterAtFieldCommand
	{
		public static void Set(ref RemoteCommand command, long unitIdx, int ownerId, int characterId, int fieldId)
		{
			command.Reset();
			command.CommandType = DefineBattle.RemoteCommandType.SpawnCharacterAtField;
			command.LongParams.Add(unitIdx);
			command.IntParams.Add(ownerId);
			command.IntParams.Add(characterId);
			command.IntParams.Add(fieldId);
		}
		
		public static long UnitIdx(in RemoteCommand remoteCommand) => remoteCommand.LongParams[0];
		public static int OwnerID(in RemoteCommand remoteCommand) => remoteCommand.IntParams[0];
		public static int CharacterID(in RemoteCommand remoteCommand) => remoteCommand.IntParams[1];
		public static int FieldID(in RemoteCommand remoteCommand) => remoteCommand.IntParams[2];
	}
	
	/// <summary>
	/// 캐릭터를 대기석에 소환한다.
	/// </summary>
	public static class RemoteSpawnCharacterAtWaitCommand
	{
		public static void Set(ref RemoteCommand command, long unitIdx, int ownerId, int characterId, int waitId)
		{
			command.Reset();
			command.CommandType = DefineBattle.RemoteCommandType.SpawnCharacterAtWait;
			command.LongParams.Add(unitIdx);
			command.IntParams.Add(ownerId);
			command.IntParams.Add(characterId);
			command.IntParams.Add(waitId);
		}
		
		public static long UnitIdx(in RemoteCommand remoteCommand) => remoteCommand.LongParams[0];
		public static int OwnerID(in RemoteCommand remoteCommand) => remoteCommand.IntParams[0];
		public static int CharacterID(in RemoteCommand remoteCommand) => remoteCommand.IntParams[1];
		public static int WaitID(in RemoteCommand remoteCommand) => remoteCommand.IntParams[2];
	}
}