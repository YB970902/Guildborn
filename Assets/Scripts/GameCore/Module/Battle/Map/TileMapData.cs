using System.Collections;
using System.Collections.Generic;
using BC.LocalData;
using GC.Utils.Define;
using UnityEngine;

namespace GC.Module
{
    public class TileMapData : MonoBehaviour
    {
        /// <summary>
        /// 타일을 가지고 있는 루트 트랜스폼.
        /// 여러개의 행을 가지고 있고, 그 행 안에는 열의 개수만큼 타일이 있다.
        /// </summary>
        [SerializeField] Transform trTileRoot;
        
        public int Width { get; private set; }
        public int Height { get; private set; }
        
        public List<Transform> TilePositionList { get; private set; }

        public void Init()
        {
            Width = 0;
            Height = 0;
            
            if(TilePositionList == null) TilePositionList = new List<Transform>();
            else TilePositionList.Clear();
            
            foreach (Transform line in trTileRoot)
            {
                ++Height;
                foreach (Transform trTile in line)
                {
                    TilePositionList.Add(trTile);
                }
            }

            Width = TilePositionList.Count / Height;
        }
    }
}