using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC.Module
{
    public class BattleModule
    {
        public PathFindModule PathFind { get; private set; }

        public BattleModule(List<Transform> trTileList)
        {
            PathFind = new PathFindModule(trTileList);
        }
    }
}