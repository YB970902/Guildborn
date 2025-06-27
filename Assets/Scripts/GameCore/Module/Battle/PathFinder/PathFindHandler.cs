using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using UnityEngine;

namespace GameCore.BattleModule.PathFinder
{
	/// <summary>
	/// 길찾기를 처리해주는 핸들러.
	/// PathFinder와 통신하는 실질적은 클래스로, 유닛과 경로와 관련된 의존성을 없애기 위해 따로 존재한다. 
	/// </summary>
	public class PathFindHandler
	{
		/// <summary>
		/// 경로를 담고있는 리스트
		/// 경로는 역순으로 들어가 있다.
		/// 다음 타일로 이동했다면 경로를 하나씩 지우기 때문에 마지막 인덱스에 있는 타일은 내가 다음에 이동해야할 타일이다.
		/// 제거되지 않고 계속 재사용되므로, 참조할때 주의
		/// </summary>
		public List<Vector2Int> Path { get; private set; }
		
		public Vector2Int CurrentTile { get; private set; }

		public PathFindHandler()
		{
			Path = new List<Vector2Int>();
		}

		/// <summary>
		/// 다음 경로를 반환한다.
		/// </summary>
		/// <param name="nextPath"> 다음 경로를 반환한다. 다음 경로가 없다면, 현재 경로를 반환한다. </param>
		/// <returns> 다음 경로가 있는지 여부 </returns>
		public bool GetNextPath(out Vector2Int nextPath)
		{
			if (Path.IsNullOrEmpty())
			{
				nextPath = CurrentTile;
				return false;
			}

			nextPath = CurrentTile;
			return true;
		}
	}
}