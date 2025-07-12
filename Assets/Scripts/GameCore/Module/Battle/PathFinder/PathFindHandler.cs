using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using FixedMathSharp;
using UnityEngine;

namespace GC.Module
{
	/// <summary>
	/// 길찾기를 처리해주는 핸들러.
	/// PathFinder와 통신하는 실질적은 클래스로, 유닛과 경로와 관련된 의존성을 없애기 위해 따로 존재한다. 
	/// </summary>
	public class PathFindHandler : PoolingObject<PathFindHandler>
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
		public Vector2Int NextTile => path[^1];
		/// <summary> 유닛이 최종적으로 도착해야하는 타일 </summary>
		public Vector2Int DestTile { get; private set; }
		/// <summary> 목적지에 도착했는지 여부 </summary>
		public bool IsArrived => path.IsNullOrEmpty();
		/// <summary> 길찾기 인스턴스 </summary>
		private PathFinder pathFinder;

		/// <summary> 지금까지 이동한 비율. 1 이상이면 다음 타일에 도착했다. </summary>
		private Fixed64 durationDist;

		/// <summary> 유닛을 식별하기위한 고유 인덱스 </summary>
		private long unitIdx;
		
		public PathFindHandler()
		{
			path = new List<Vector2Int>();
			pathFinder = GameCore.Instance.Battle.PathFind.PathFinder;
		}

		public void Set(long unitIdx)
		{
			this.unitIdx = unitIdx;
		}

		/// <summary>
		/// 이동을 수행한다.
		/// </summary>
		public void Move(Fixed64 moveSpeed)
		{
			// 이미 목적지에 도착했다면 이동하지 않는다.
			if (IsArrived) return;
			
			// 타일 1칸을 이동하는데 걸리는 시간을, 1Tick당 이동한 거리(비율)로 변환한다.
			Fixed64 delta = BattleModule.DeltaTime / moveSpeed;
			durationDist += delta;
			
			// 중간을 넘어섰다면, 타일 이동 처리를 한다.
			if (durationDist - delta < Fixed64.Half && durationDist >= Fixed64.Half)
			{
				// 현재 위치의 타일을 벗어났으므로 0으로 처리한다.
				pathFinder.SetOccupyTile(0, CurrentTile);
				pathFinder.SetReserveTile(0, NextTile);
				pathFinder.SetOccupyTile(unitIdx, NextTile);
			}
			// 1을 넘었다면, 목적지에 도착했다. 다음 길찾기를 수행한다.
			else if (durationDist > Fixed64.One)
			{
				pathFinder.FindPath(CurrentTile, DestTile, ref path);
				// 경로의 마지막 요소는 현재의 위치이므로 마지막 요소를 지운다.
				CurrentTile = path[^1];
				path.RemoveAt(path.Count - 1);
				
				pathFinder.SetReserveTile(unitIdx, NextTile);

				// 이동한 거리를 초기화한다.
				durationDist = Fixed64.Zero;
			}
		}

		/// <summary>
		/// 월드 좌표계상의 위치를 반환한다.
		/// </summary>
		public Vector2 GetPosition()
		{
			Vector2 currentPosition = pathFinder.GetPosition(CurrentTile);
			Vector2 nextPosition = pathFinder.GetPosition(NextTile);

			return currentPosition + (nextPosition - currentPosition) * (float)durationDist;
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