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
	public static class Battle
	{
		/// <summary> 타일 X축 개수 </summary>
		public const int TileXCount = 5;
		/// <summary> 타일 Y축 개수 </summary>
		public const int TileYCount = 5;
	}
}