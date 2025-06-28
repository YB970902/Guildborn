using System.Collections;
using System.Collections.Generic;
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

        public void EnterBattle()
        {
            GameCore.Instance.GameData.LoadBattleData();
        }

        public void ExitBattle()
        {
            GameCore.Instance.GameData.UnloadBattleData();
        }
    }
}