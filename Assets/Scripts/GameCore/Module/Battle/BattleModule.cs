using System.Collections;
using System.Collections.Generic;
using FixedMathSharp;
using UnityEngine;

namespace GC.Module
{
    public class BattleModule
    {
        public PathFindModule PathFind { get; private set; }
        public CharacterModule Character { get; private set; }
        
        public CommandProcessor Command { get; private set; }

        public BattleModule()
        {
            PathFind = new PathFindModule();
            Character = new CharacterModule();
            Command = new CommandProcessor();
        }

        public void Init()
        {
            PathFind.Init();
            Character.Init();
        }

        /// <summary>
        /// 배툴을 시작한다.
        /// </summary>
        public void EnterBattle()
        {
            // 배틀에 필요한 데이터 로드
            GameCore.Instance.GameData.LoadBattleData();
            
            Character.AddCharacter(0, 0, 1);
        }

        /// <summary>
        /// 배틀을 종료한다.
        /// </summary>
        public void ExitBattle()
        {
            // 배틀에 필요한 데이터 정리
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