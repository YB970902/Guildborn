using System.Collections;
using System.Collections.Generic;
using GC.Utils.Define;
using UnityEngine;

namespace GC.Module
{
    /// <summary>
    /// 전투에 필요한 맵의 타일 정보
    /// </summary>
    public class TileData : MonoBehaviour
    {
        public DefineBattle.TileType TileType { get; private set; }

        public int TileID { get; private set; }
        
        public Vector2 Position => transform.position;

        public void Set(DefineBattle.TileType tileType, int tileId)
        {
            TileType = tileType;
            TileID = tileId;
        }
    }
}