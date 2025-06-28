using System.Collections;
using System.Collections.Generic;
using GC.Utils.Define;
using UnityEngine;

namespace GC.Module
{
    public class TilePositionPack : MonoBehaviour, IGameDataPack
    {
        /// <summary>
        /// 타일을 가지고 있는 루트 트랜스폼.
        /// 여러개의 행을 가지고 있고, 그 행 안에는 열의 개수만큼 타일이 있다.
        /// </summary>
        [SerializeField] Transform trTileRoot;

        public List<Transform> TilePositionList { get; private set; }

        public void Init()
        {
            if(TilePositionList == null) TilePositionList = new List<Transform>(Battle.TileXCount * Battle.TileYCount);
            else TilePositionList.Clear();
            
            foreach (Transform line in trTileRoot)
            {
                foreach (Transform trTile in line)
                {
                    TilePositionList.Add(trTile);
                }
            }
        }
    }
}