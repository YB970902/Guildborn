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
		public ObjectPool<RemoteSpawnCharacterAtFieldCommand> SpawnCharacterAtFieldPool { get; private set; }
		public ObjectPool<RemoteSpawnCharacterAtWaitCommand> SpawnCharacterAtWaitPool { get; private set; }

		public RemoteClassPool()
		{
			SpawnCharacterAtFieldPool = new ObjectPool<RemoteSpawnCharacterAtFieldCommand>();
		}

		public void Init()
		{
			SpawnCharacterAtFieldPool.Init();
		}
	}

	/// <summary>
	/// 캐릭터를 필드에 소환한다.
	/// </summary>
	public class RemoteSpawnCharacterAtFieldCommand : PoolingObject<RemoteSpawnCharacterAtFieldCommand>, IRemoteCommand
	{
		public DefineBattle.RemoteCommandType CommandType => DefineBattle.RemoteCommandType.SpawnCharacterAtField;
		
		/// <summary> 캐릭터의 고유 인덱스 </summary>
		public long UnitIdx { get; private set; }

		/// <summary> 이 캐릭터를 소유하는 플레이어의 아이디 </summary>
		public int OwnerID { get; private set; }

		/// <summary> 캐릭터의 아이디 </summary>
		public int CharacterID { get; private set; }
		
		/// <summary> 필드의 아이디 </summary>
		public int FieldID { get; private set; }
		
		public void Set(long unitIdx, int ownerId, int characterId, int fieldId)
		{
			UnitIdx = unitIdx;
			OwnerID = ownerId;
			CharacterID = characterId;
			FieldID = fieldId;
		}
	}
	
	/// <summary>
	/// 캐릭터를 대기석에 소환한다.
	/// </summary>
	public class RemoteSpawnCharacterAtWaitCommand : PoolingObject<RemoteSpawnCharacterAtWaitCommand>, IRemoteCommand
	{
		public DefineBattle.RemoteCommandType CommandType => DefineBattle.RemoteCommandType.SpawnCharacterAtField;
		
		/// <summary> 캐릭터의 고유 인덱스 </summary>
		public long UnitIdx { get; private set; }

		/// <summary> 이 캐릭터를 소유하는 플레이어의 아이디 </summary>
		public int OwnerID { get; private set; }

		/// <summary> 캐릭터의 아이디 </summary>
		public int CharacterID { get; private set; }
		
		/// <summary> 대기석의 아이디 </summary>
		public int WaitID { get; private set; }
		
		public void Set(long unitIdx, int ownerId, int characterId, int waitId)
		{
			UnitIdx = unitIdx;
			OwnerID = ownerId;
			CharacterID = characterId;
			WaitID = waitId;
		}
	}
}