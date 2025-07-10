using System.Collections;
using System.Collections.Generic;
using FixedMathSharp;
using UnityEngine;

namespace GC.Module
{
    public class BattleModule
    {
        public PathFindModule PathFind { get; private set; }

        public BattleModule()
        {
            PathFind = new PathFindModule();
        }

        public void Init()
        {
            PathFind.Init();
        }

        public void EnterBattle()
        {
            GameCore.Instance.GameData.LoadBattleData();
        }

        public void ExitBattle()
        {
            GameCore.Instance.GameData.UnloadBattleData();
        }

        /// <summary>
        /// 1초동안 Tick 갱신 주기 
        /// </summary>
        public static int TickFrame = 30;

        /// <summary> 1Tick을 처리하는데 걸리는 시간 </summary>
        public static Fixed64 DeltaTime = (Fixed64)1 / (Fixed64)TickFrame;
    }
}