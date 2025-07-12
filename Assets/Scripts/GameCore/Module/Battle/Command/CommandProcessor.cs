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
		
		public CommandProcessor()
		{
			localCommandPool = new ObjectPool<LocalCommand>();
			remoteCommandPool = new ObjectPool<RemoteCommand>();
			localCommandList = new List<LocalCommand>();
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
					case Battle.LocalCommandType.Attack:
						ProcessLocalAttackCommand(localCommandList[i] as LocalAttackCommand);
						break;
					case Battle.LocalCommandType.UseSkill:
						ProcessLocalUseSkillCommand(localCommandList[i] as LocalUseSkillCommand);
						break;
				}
			}
			
			localCommandList.Clear();
		}

		public void ProcessRemoteCommand()
		{
			
		}

		#region AddCommand
		
		public void Attack(long attackerIdx, long targetIdx)
		{
			LocalAttackCommand command = LocalAttackCommand.Set(localCommandPool.Pop(), attackerIdx, targetIdx);
			localCommandPool.Push(command);
		}
		
		#endregion
		
		#region ProcessLocalCommand

		private void ProcessLocalAttackCommand(LocalAttackCommand command)
		{
			
		}

		private void ProcessLocalUseSkillCommand(LocalUseSkillCommand command)
		{
			
		}
		
		#endregion
	}
}