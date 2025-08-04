using System.Collections;
using System.Collections.Generic;
using BC;
using FixedMathSharp;
using UnityEngine;

namespace GC.Module
{
    public class BattleModule
    {
        public MapModule Map { get; private set; }
        public CharacterModule Character { get; private set; }
        public CommandProcessor Command { get; private set; }

        public bool IsBattleStart { get; private set; }
        
        /// <summary>
        /// 1초동안 Tick 갱신 주기 
        /// </summary>
        public static int TickFrame = 30;

        /// <summary> 1Tick을 처리하는데 걸리는 시간 </summary>
        public static Fixed64 DeltaTime = (Fixed64)1 / (Fixed64)TickFrame;

        public BattleModule()
        {
            Map = new MapModule();
            Character = new CharacterModule();
            Command = new CommandProcessor();
        }

        public void Init()
        {
            Map.Init();
            Character.Init();
            Command.Init();

            // 고정 업데이트 주기를 조절한다. 
            Time.fixedDeltaTime = 1f / TickFrame;
        }

        /// <summary>
        /// 전투를 시작한다.
        /// </summary>
        public void EnterBattle()
        {
            // 전투에 필요한 모든 리소스를 로드한다.
            GameCore.Instance.StartCoroutine(LoadBattleResources());
            
            Map.LoadMap(BeanCore.Instance.LD.Map[1]);
        }

        /// <summary>
        /// 전투에 필요한 모든 애셋을 로드한다.
        /// </summary>
        private IEnumerator LoadBattleResources()
        {
            // TODO : 어드레서블에서 전투에 필요한 캐릭터와 같은 데이터를 미리 체크해서 한꺼번에 로드시킨다.
            yield return null;
            IsBattleStart = true;
        }

        /// <summary>
        /// 배틀을 종료한다.
        /// </summary>
        public void ExitBattle()
        {
            Map.UnloadMap();
            
            // 전투에 필요한 모든 리소스를 제거한다.
            GameCore.Instance.StartCoroutine(LoadBattleResources());
        }

        private IEnumerator UnloadBattleResources()
        {
            yield return null;
            IsBattleStart = false;
        }

        public void Update()
        {
            if (IsBattleStart)
            {
                Command.ProcessLocalCommand();
                Command.ProcessRemoteCommand();
                Character.Update();
            }
        }
    }
}