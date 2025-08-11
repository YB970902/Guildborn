using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 게임 내에서 사용되는 각종 enum, const 값들을 모아둔 스크립트.
// 이 곳에 있는 class 들은 인스턴스화 되지 않는다는 전제로 사용되어야 하기 때문에 static class로 제작한다.
namespace GC.Utils.Define
{
	/// <summary>
	/// 디버깅을 위한 데이터를 관리하는 클래스. 
	/// </summary>
	public static class Debugging
	{
		/// <summary> PathFinder 모듈 디버깅 </summary>
		public static bool DebugPathFinder = false;
	}
	
	/// <summary>
	/// 전투와 관련된 데이터를 관리하는 클래스
	/// </summary>
	public static class DefineBattle
	{
		public enum LocalCommandType
		{
			Attack,		// 일반 공격
			UseSkill,	// 스킬 사용
		}

		public enum RemoteCommandType
		{
			None,
			SpawnCharacterAtWait,	// 대기De석에 캐릭터 소환
			SpawnCharacterAtField,	// 필드에 캐릭터 소환
			CharacterMoveToField,	// 캐릭터를 필드로 이동
			CharacterMoveToWait,	// 캐릭터를 대기석으로 이동
		}

		public enum TileType
		{
			Field,	// 전투에 참여하는 캐릭터가 위치하는 타일.
			Wait,	// 전투에 참여하지 않는 캐릭터가 대기하는 타일
		}
	}

	public static class DefineAddressable
	{
		public enum Group
		{
			None,
			Battle, // 전투에 사용되는 그룹
		}
	}
}