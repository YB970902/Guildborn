using System.Collections;
using System.Collections.Generic;
using GameCore.Utils.Define;
using Priority_Queue;
using UnityEngine;

namespace GameCore.BattleModule.PathFinder
{
	/// <summary>
	/// 길찾기를 처리하는 모듈.
	/// 실질적으로 유닛과 통신하지는 않고, PathFindHandler와 통신한다.
	/// </summary>
	public class PathFinder
	{
		/// <summary>
		/// 타일 정보.
		/// 길찾기에서 우선순위 큐를 사용하기 위해 FastPriorityQueueNode 추가
		/// </summary>
		public class TileInfo : FastPriorityQueueNode
		{
			/// <summary> 타일의 인덱스 </summary>
			public Vector2Int Index;
			/// <summary> 이 타일을 점유중인 유닛의 고유 인덱스 </summary>
			public long OccupiedUnitIdx { get; private set; }
			/// <summary> 이 타일로 이동하기 위해 예약한 유닛의 고유 인덱스 </summary>
			public long ReservedUnitIdx { get; private set; }
			/// <summary> 이 타일이 지나갈 수 없는 상태인지 여부 </summary>
			public bool IsObstacle => OccupiedUnitIdx != 0;
			/// <summary> 이 타일로 이동하려는 유닛이 있는지 여부 </summary>
			public bool IsReserved => ReservedUnitIdx != 0;
			
			
			#region ForPathFinding
			/// <summary> 부모 타일의 위치. 길찾기에서만 사용된다. </summary>
			public TileInfo Parent { get; set; }

			/// <summary> 시작 위치에서부터 현지 타일까지의 거리. 길찾기에서만 사용된다. </summary>
			public int G { get; set; }
			/// <summary> 현재 위치에서 도착 타일까지의 거리. 길찾기에서만 사용된다. </summary>
			public int H { get; set; }
			/// <summary> 가중치. 길찾기에서만 사용된다. </summary>
			public int F => G + H;
			
			/// <summary> 열림 리스트에 들어가있는지 여부 </summary>
			public bool IsOpen { get; set; }
			/// <summary> 닫힘 리스트에 들어가있는지 여부 </summary>
			public bool IsClosed { get; set; }
			
			/// <summary>
			/// 길찾기와 관련된 정보를 리셋한다.
			/// </summary>
			public void ResetPathFindingInfo()
			{
				Parent = null;
				G = int.MaxValue; // 초기값은 가장 큰 값이 들어가야한다. 더 작은값으로 갱신해야하기 때문이다.
				H = 0;
				IsOpen = false;
				IsClosed = false;
			}
			
			#endregion

			public TileInfo(int x, int y)
			{
				Index = new Vector2Int(x, y);
			}
			
			public void Init()
			{
				OccupiedUnitIdx = 0;
				ReservedUnitIdx = 0;
				ResetPathFindingInfo();
			}

			/// <summary>
			/// 이 타일로 이동할 것임을 예약한다.
			/// </summary>
			public void SetReserve(long unitIdx)
			{
				#if UNITY_EDITOR
				if(Debugging.DebugPathFinder) Debug.Log($"SetReserve {ReservedUnitIdx} > {unitIdx}");
				#endif
				ReservedUnitIdx = unitIdx;
			}
		}
		
		/// <summary> 맵의 타일 정보 </summary>
		private TileInfo[,] tileInfoList;
		/// <summary> 길찾기 연산에 필요한 오픈 리스트. </summary>
		private FastPriorityQueue<TileInfo> openList;
		/// <summary> 길찾기 연산에 필요한 클로즈 리스트. </summary>
		private List<TileInfo> closeList;
		/// <summary> 길찾기 연산에 필요한 인접 리스트 </summary>
		private List<TileInfo> nearList;
		
		public PathFinder()
		{
			tileInfoList = new TileInfo[Battle.TileXCount, Battle.TileYCount];
			openList = new FastPriorityQueue<TileInfo>(Battle.TileXCount * Battle.TileYCount);
			closeList = new List<TileInfo>(Battle.TileXCount * Battle.TileYCount);
			nearList = new List<TileInfo>(6);
			
			for (int x = 0; x < Battle.TileXCount; ++x)
			{
				for (int y = 0; y < Battle.TileYCount; ++y)
				{
					tileInfoList[x, y] = new TileInfo(x, y);
				}
			}
		}

		/// <summary>
		/// 타일 정보 초기화
		/// </summary>
		public void Init()
		{
			for (int x = 0; x < Battle.TileXCount; ++x)
			{
				for (int y = 0; y < Battle.TileYCount; ++y)
				{
					tileInfoList[x, y].Init();
				}
			}
		}
		
		#region PathFinding
		
		/// <summary>
		/// 길찾기를 수행한다.
		/// </summary>
		/// <param name="start">시작 위치</param>
		/// <param name="dest">도착 위치</param>
		/// <param name="path">경로</param>
		/// <returns>목표까지의 경로가 있는지 없는지 여부</returns>
		public bool FindPath(in Vector2Int start, in Vector2Int dest, ref List<Vector2Int> path)
		{
			// 초기화
			path.Clear();
			openList.Clear();
			closeList.Clear();
			
			// 길찾기 관련 데이터를 리셋한다.
			for (int x = 0; x < Battle.TileXCount; ++x)
			{
				for (int y = 0; y < Battle.TileYCount; ++y)
				{
					tileInfoList[x, y].ResetPathFindingInfo();
				}
			}
			
			TileInfo currTile = GetTileInfo(start);
			currTile.G = 0;
			currTile.H = CalcHeuristic(currTile, dest);
			openList.Enqueue(currTile, currTile.F);

			// 오픈리스트가 빌때까지 길을 찾는다.
			while (openList.Count > 0)
			{
				// 현재 타일
				currTile = openList.Dequeue();
				currTile.IsOpen = false;
				currTile.IsClosed = true;

				// 경로를 찾았다면, 길찾기를 중단한다.
				if (currTile.Index == dest)
				{
					while (currTile != null)
					{
						path.Add(currTile.Index);
						currTile = currTile.Parent;
					}
					
					return true;
				}
				
				nearList.Clear();
				SearchNearList(currTile.Index);
				if (nearList.Count == 0) continue;

				for (int i = 0, count = nearList.Count; i < count; ++i)
				{
					TileInfo nearTile = nearList[i];
					nearTile.H = CalcHeuristic(nearTile, dest);
					
					// G값이 더 작아질 수 있다면, 부모를 갱신한다.
					if (nearTile.G > currTile.G + 1)
					{
						nearTile.Parent = currTile;
						nearTile.G = currTile.G + 1;
					}

					// 오픈리스트에 있지 않는다면 추가한다.
					if (nearTile.IsOpen == false)
					{
						nearTile.IsOpen = true;
						openList.Enqueue(nearTile, nearTile.F);
					}
				}
			}
			
			// 경로를 찾지 못했다. CloseNode중 가장 목적지와 가까운 곳으로 이동한다.

			// 가장 가까운 거리
			int minDist = int.MaxValue;
			// 가장 가까운 타일
			TileInfo minDistTile = null;
			
			for (int i = 0, count = closeList.Count; i < count; ++i)
			{
				TileInfo tileInfo = closeList[i];
				int dist = CalcHeuristic(tileInfo, dest);
				if (dist < minDist)
				{
					minDist = dist;
					minDistTile = tileInfo;
				}
			}

			while (minDistTile != null)
			{
				path.Add(minDistTile.Index);
				minDistTile = minDistTile.Parent;
			}
			
			return false;
		}

		/// <summary>
		/// 타일 근처에 있는 타일들을 주변 리스트에 넣는다.
		/// </summary>
		private void SearchNearList(in Vector2Int index)
		{
			if ((index.y & 1) == 0)
			{
				// 짝수는 좌하, 하, 좌상, 상, 상상, 하하 순으로 넣는다.
				AddToNearList(index + Vector2Int.down + Vector2Int.left);
				AddToNearList(index + Vector2Int.down);
				AddToNearList(index + Vector2Int.up + Vector2Int.left);
				AddToNearList(index + Vector2Int.up);
				AddToNearList(index + Vector2Int.up + Vector2Int.up);
				AddToNearList(index + Vector2Int.down + Vector2Int.down);
			}
			else
			{
				// 홀수는 하, 우하, 상, 우상, 상상, 하하 순으로 넣는다.
				AddToNearList(index + Vector2Int.down);
				AddToNearList(index + Vector2Int.down + Vector2Int.right);
				AddToNearList(index + Vector2Int.up);
				AddToNearList(index + Vector2Int.up + Vector2Int.right);
				AddToNearList(index + Vector2Int.up + Vector2Int.up);
				AddToNearList(index + Vector2Int.down + Vector2Int.down);
			}
			
			// 인접 리스트에 넣을 수 있다면 넣는다.
			void AddToNearList(in Vector2Int index)
			{
				// 범위를 벗어났다면 넣지 않는다.
				if (index.x < 0 || index.x >= Battle.TileXCount ||
				    index.y < 0 || index.y >= Battle.TileYCount)
				{
					return;
				}
				
				TileInfo tileInfo = GetTileInfo(index);
				// 닫힌 리스트에 있다면 인접 리스트에 넣지 않는다.
				if (tileInfo.IsClosed) return;
				
				nearList.Add(GetTileInfo(index));
			}
		}

		/// <summary>
		/// 휴리스틱 비용을 계산한다.
		/// </summary>
		private int CalcHeuristic(TileInfo tileInfo, in Vector2Int dest)
		{
			Vector3Int tileCubeCoord = GetCubeCoordinate(tileInfo.Index);
			Vector3Int destCubeCoord = GetCubeCoordinate(dest);

			int result = Mathf.Max(Mathf.Abs(tileCubeCoord.x - destCubeCoord.x),
			                       Mathf.Abs(tileCubeCoord.y - destCubeCoord.y));
			result = Mathf.Max(result, Mathf.Abs(tileCubeCoord.z - destCubeCoord.z));

			return result;
		}

		/// <summary>
		/// 큐브 좌표계에서의 값을 반환한다.
		/// </summary>
		private Vector3Int GetCubeCoordinate(in Vector2Int index)
		{
			Vector3Int result = Vector3Int.zero;
			result.x = index.x - (index.y - (index.y & 1));
			result.z = index.y;
			result.y = -result.x - result.z;

			return result;
		}
		
		private TileInfo GetTileInfo(in Vector2Int index)
		{
			return tileInfoList[index.x, index.y];
		}
		
		#endregion
		
		/// <summary>
		/// 해당 타일을 예약할 수 있는지 여부
		/// </summary>
		public bool CanReserved(in Vector2Int index)
		{
			return tileInfoList[index.x, index.y].IsReserved || tileInfoList[index.x, index.y].IsObstacle;
		}

		/// <summary>
		/// 해당 타일이 장애물인지 여부
		/// </summary>
		public bool IsObstacle(in Vector2Int index)
		{
			return tileInfoList[index.x, index.y].IsObstacle;
		}

		/// <summary>
		/// 타일 이동하기 전에 타일을 예약한다.
		/// </summary>
		public void SetReserveTile(long unitIdx, in Vector2Int index)
		{
			tileInfoList[index.x, index.y].SetReserve(unitIdx);
		}
	}
}