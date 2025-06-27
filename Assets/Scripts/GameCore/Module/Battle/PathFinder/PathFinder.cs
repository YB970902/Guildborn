using System.Collections;
using System.Collections.Generic;
using GameCore.Utils.Define;
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
		/// 타일 정보
		/// </summary>
		public struct TileInfo
		{
			/// <summary> 타일의 위치 </summary>
			public Vector2Int Position;
			/// <summary> 이 타일을 점유중인 유닛의 고유 인덱스 </summary>
			public long OccupiedUnitIdx { get; private set; }
			/// <summary> 이 타일로 이동하기 위해 예약한 유닛의 고유 인덱스 </summary>
			public long ReservedUnitIdx { get; private set; }
			/// <summary> 이 타일이 지나갈 수 없는 상태인지 여부 </summary>
			public bool IsObstacle => OccupiedUnitIdx != 0;
			/// <summary> 이 타일로 이동하려는 유닛이 있는지 여부 </summary>
			public bool IsReserved => ReservedUnitIdx != 0;

			public TileInfo(int x, int y)
			{
				Position = new Vector2Int(x, y);
				OccupiedUnitIdx = 0;
				ReservedUnitIdx = 0;
			}
			
			public void Init()
			{
				OccupiedUnitIdx = 0;
				ReservedUnitIdx = 0;
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
		
		/// <summary>
		/// 맵의 타일 정보
		/// </summary>
		private TileInfo[,] tileInfoList;
		
		public PathFinder()
		{
			tileInfoList = new TileInfo[Battle.TileXCount, Battle.TileYCount];
			
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

		/// <summary>
		/// 해당 타일을 예약할 수 있는지 여부
		/// </summary>
		public bool CanReserved(Vector2Int position)
		{
			return tileInfoList[position.x, position.y].IsReserved || tileInfoList[position.x, position.y].IsObstacle;
		}

		/// <summary>
		/// 해당 타일이 장애물인지 여부
		/// </summary>
		public bool IsObstacle(Vector2Int position)
		{
			return tileInfoList[position.x, position.y].IsObstacle;
		}

		/// <summary>
		/// 타일 이동하기 전에 타일을 예약한다.
		/// </summary>
		public void SetReserveTile(long unitIdx, Vector2Int position)
		{
			tileInfoList[position.x, position.y].SetReserve(unitIdx);
		}
	}
}