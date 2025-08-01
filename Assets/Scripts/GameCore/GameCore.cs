using System;
using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using GC.Module;
using UnityEngine;

namespace GC
{
    public class GameCore : MonoSingleton<GameCore>
    {
        public BattleModule Battle { get; private set; }

        protected override void OnInit()
        {
            base.OnInit();
            // 모듈 생성
            Battle = new BattleModule();
        }

        public void Init()
        {
            // 모듈 초기화
            Battle.Init();
        }

        private void FixedUpdate()
        {
            Battle.Update();
        }
    }
}