using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using GC.Module;
using UnityEngine;

namespace GC
{
    public class GameCore : MonoSingleton<GameCore>
    {
        [SerializeField] GameDataModule gameDataModule;
        public BattleModule Battle { get; private set; }
        public GameDataModule GameData => gameDataModule;

        protected override void OnInit()
        {
            base.OnInit();
            
            // 모듈 생성
            Battle = new BattleModule();
            
            // 모듈 초기화
            Battle.Init();
        }
    }
}