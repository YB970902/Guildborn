using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using UnityEngine;

namespace GC.Module
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
		private List<Vector2Int> path;
		/// <summary> 현재 위치의 타일 </summary>
		public Vector2Int CurrentTile { get; private set; }
		/// <summary> 유닛이 이동해야 하는 다음 타일 </summary>
		public Vector2Int NextTile { get; private set; }
		/// <summary>
		/// 길찾기 인스턴스
		/// </summary>
		private PathFinder pathFinder;

		public PathFindHandler(PathFinder pathFinder)
		{
			path = new List<Vector2Int>();
			this.pathFinder = pathFinder;
		}

		/// <summary>
		/// 유닛이 이동할경우 여기에 현재 위치를 넣는다.
		/// TODO : 현재는 위치값을 Vector2를 받지만, 추후에 고정소수점을 사용하는 벡터로 수정되어야함.
		/// </summary>
		public void Update(Vector2 position)
		{
			// TODO : 다음 노드에 도착했는지 검사하기. 도착했다면, 경로를 갱신하고 pathFinder에게 요청한다. 
		}
		
		#if UNITY_EDITOR
		/// <summary>
		/// 테스트용 길찾기 요청
		/// </summary>
		public void GetPathForDebug(in Vector2Int start, in Vector2Int end)
		{
			pathFinder.FindPath(start, end, ref path);

			for(int i = 0; i < path.Count; ++i)
			{
				Debug.Log($"Index : {i}, X : {path[i].x}, Y : {path[i].y}");
			}
		}
		#endif
	}
}