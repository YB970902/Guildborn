using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using FixedMathSharp;
using GC.Utils.Define;
using UnityEngine;

namespace GC.Module.Command
{
	/// <summary>
	/// 서버로부터 전달받거나, 서버로 보내야하는 명령의 인터페이스
	/// </summary>
	public interface IRemoteCommand
	{
		public DefineBattle.RemoteCommandType CommandType { get; }
	}

	public class RemoteClassPool
	{
		public ObjectPool<RemoteSpawnCharacterCommand> SpawnCharacterPool { get; private set; }

		public RemoteClassPool()
		{
			SpawnCharacterPool = new ObjectPool<RemoteSpawnCharacterCommand>();
		}

		public void Init()
		{
			SpawnCharacterPool.Init();
		}
	}

	public class RemoteSpawnCharacterCommand : PoolingObject<RemoteSpawnCharacterCommand>, IRemoteCommand
	{
		public DefineBattle.RemoteCommandType CommandType => DefineBattle.RemoteCommandType.SpawnCharacter;
		
		/// <summary>
		/// 캐릭터의 고유 인덱스
		/// </summary>
		public long UnitIdx { get; private set; }

		/// <summary>
		/// 이 캐릭터를 소유하는 플레이어의 아이디
		/// </summary>
		public int OwnerID { get; private set; }

		/// <summary>
		/// 캐릭터의 아이디
		/// </summary>
		public int CharacterID { get; private set; }
		
		public void Set(long unitIdx, int ownerId, int characterId)
		{
			UnitIdx = unitIdx;
			OwnerID = ownerId;
			CharacterID = characterId;
		}
	}
}