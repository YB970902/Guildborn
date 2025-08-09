using System.Collections;
using System.Collections.Generic;
using BC;
using BC.LocalData;
using GC.Utils.Define;
using UnityEngine;
using UnityEngine.Serialization;

namespace GC.Module
{
    /// <summary>
    /// 전투에 필요한 맵 타일 정보
    /// </summary>
    public class TileMapData : MonoBehaviour
    {
        /// <summary>
        /// 타일을 가지고 있는 루트 트랜스폼.
        /// 여러개의 행을 가지고 있고, 그 행 안에는 열의 개수만큼 타일이 있다.
        /// </summary>
        [FormerlySerializedAs("trTileRoot")] [SerializeField] Transform trFieldTileRoot;
        /// <summary> 대기 타일을 가지고 있는 루트 트랜스폼 </summary>
        [SerializeField] Transform trWaitTileRoot;
        
        /// <summary> 필드 타일 가로 개수 </summary>
        public int FieldTileWidthCount { get; private set; }
        /// <summary> 필드 타일 세로 개수 </summary>
        public int FieldTileHeightCount { get; private set; }
        /// <summary> 대기석 타일 개수 </summary>
        public int WaitTileCount { get; private set; }

        /// <summary> 필드 타일의 위치를 가지고 있는 리스트 </summary>
        public List<Transform> TilePositionList { get; private set; }
        /// <summary> 대기석 타일의 위치를 가지고 있는 리스트 </summary>
        public List<Transform> WaitTilePositionList { get; private set; }

        public void Init()
        {
            // 맵의 너비와 높이를 계산한다.
            FieldTileWidthCount = 0;
            FieldTileHeightCount = 0;
            
            if(TilePositionList == null) TilePositionList = new List<Transform>();
            else TilePositionList.Clear();
            
            foreach (Transform line in trFieldTileRoot)
            {
                ++FieldTileHeightCount;
                foreach (Transform trTile in line)
                {
                    TilePositionList.Add(trTile);
                }
            }

            FieldTileWidthCount = TilePositionList.Count / FieldTileHeightCount;
            
            // 맵의 대기석을 계산한다.
            if(WaitTilePositionList == null) WaitTilePositionList = new List<Transform>();
            else WaitTilePositionList.Clear();

            foreach (Transform trTile in trWaitTileRoot)
            {
                ++WaitTileCount;
                WaitTilePositionList.Add(trTile);
            }
        }
    }
}