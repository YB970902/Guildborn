using System.Collections.Generic;
using BC.Utils;
using GC.Module.Command;
using GC.Utils.Define;

namespace GC.Module
{
	/// <summary>
	/// 명령을 처리하는 모듈
	/// RemoteCommand : 서버로 보내야하거나 서버로부터 받는 명령
	/// LocalCommand : 내부적으로 사용하는 명령으로, 공격이나 스킬 사용 명령이 포함된다.
	/// </summary>
	public class CommandProcessor
	{
		private ObjectPool<LocalCommand> localCommandPool;
		private ObjectPool<RemoteCommand> remoteCommandPool;
		
		private List<LocalCommand> localCommandList;
		// TODO : 이 명령을 실행할 타이밍도 정보에 포함되어야 한다.
		private List<RemoteCommand> remoteCommandList;
		
		public CommandProcessor()
		{
			localCommandPool = new ObjectPool<LocalCommand>();
			remoteCommandPool = new ObjectPool<RemoteCommand>();
			localCommandList = new List<LocalCommand>();
			remoteCommandList = new List<RemoteCommand>();
		}

		public void Init()
		{
			localCommandPool.Init();
			remoteCommandPool.Init();
		}

		public void ProcessLocalCommand()
		{
			for (int i = 0, count = localCommandList.Count; i < count; ++i)
			{
				switch (localCommandList[i].CommandType)
				{
					case DefineBattle.LocalCommandType.Attack:
						ProcessLocalAttack(localCommandList[i] as LocalAttackCommand);
						break;
					case DefineBattle.LocalCommandType.UseSkill:
						ProcessLocalUseSkill(localCommandList[i] as LocalUseSkillCommand);
						break;
				}
				
				localCommandList[i].ReturnToPool();
			}
			
			localCommandList.Clear();
		}

		public void ProcessRemoteCommand()
		{
			for (int i = 0, count = remoteCommandList.Count; i < count; ++i)
			{
				switch (remoteCommandList[i].CommandType)
				{
					case DefineBattle.RemoteCommandType.SpawnCharacter:
						ProcessRemoteSpawnCharacter(remoteCommandList[i] as RemoteSpawnCharacterCommand);
						break;
				}
			}
		}

		#region AddCommand
		
		public void Attack(long attackerIdx, long targetIdx)
		{
			LocalAttackCommand command = LocalAttackCommand.Set(localCommandPool.Pop(), attackerIdx, targetIdx);
			localCommandPool.Push(command);
		}

		/// <summary>
		/// 캐릭터를 소환한다
		/// </summary>
		/// <param name="unitIdx"> 소환할 캐릭터의 고유 아이디 </param>
		/// <param name="ownerId"> 캐릭터를 소유하는 소유자 아이디</param>
		/// <param name="characterId"> 캐릭터 아이디 </param>
		public void SpawnCharacter(long unitIdx, int ownerId, int characterId)
		{
			RemoteSpawnCharacterCommand command = RemoteSpawnCharacterCommand.Set(remoteCommandPool.Pop(), unitIdx, ownerId, characterId);
			remoteCommandPool.Push(command);
		}
		
		#endregion
		
		#region ProcessLocalCommand

		private void ProcessLocalAttack(LocalAttackCommand command)
		{
			
		}

		private void ProcessLocalUseSkill(LocalUseSkillCommand command)
		{
			
		}
		
		#endregion
		
		#region ProcessRemoteCommand
		
		private void ProcessRemoteSpawnCharacter(RemoteSpawnCharacterCommand command)
		{
			GameCore.Instance.Battle.Character.AddCharacter(command.UnitIdx, command.OwnerID, command.CharacterID);
		}
		
		#endregion
	}
}