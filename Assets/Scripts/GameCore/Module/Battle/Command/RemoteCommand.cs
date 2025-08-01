using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using FixedMathSharp;
using GC.Utils.Define;
using UnityEngine;

namespace GC.Module.Command
{
	/// <summary>
	/// 서버로부터 전달받거나, 서버로 보내야하는 명령
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

	public class RemoteSpawnCharacterCommand : RemoteCommand
	{
		/// <summary>
		/// 캐릭터의 고유 인덱스
		/// </summary>
		public long UnitIdx => LongParams[0];
		/// <summary>
		/// 이 캐릭터를 소유하는 플레이어의 아이디
		/// </summary>
		public int OwnerID => IntParams[0];
		/// <summary>
		/// 캐릭터의 아이디
		/// </summary>
		public int CharacterID => IntParams[1];

		public static RemoteSpawnCharacterCommand Set(in RemoteCommand command, long unitIdx, int ownerId, int characterId)
		{
			command.Reset();
			command.LongParams.Add(unitIdx);
			command.IntParams.Add(ownerId);
			command.IntParams.Add(characterId);
			command.CommandType = DefineBattle.RemoteCommandType.SpawnCharacter;

			return command as RemoteSpawnCharacterCommand;
		}
	}
}