using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using UnityEngine;

namespace GC
{
    public class GameCore : MonoSingleton<GameCore>
    {
        /// <summary>
        /// 타일의 실제 월드 좌표를 가지고 있는 리스트
        /// TODO : 고민해봐야함. 이런 리스트 형태 말고 따로 리스트를 만들어야하나?
        /// </summary>
        [SerializeField] List<Transform> trTileList;
        
        public Module.BattleModule Battle { get; private set; }
        private void Awake()
        {
            Battle = new Module.BattleModule(trTileList);
        }
    }
}