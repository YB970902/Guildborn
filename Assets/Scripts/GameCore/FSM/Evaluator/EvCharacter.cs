using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC.FSM
{
    public class EvCharacter : EvaluatorBase<BbCharacter>
    {
        public EvCharacter(BbCharacter blackboard) : base(blackboard)
        {
            
        }

        public override string Evaluate()
        {
            // 목적지에 도착했다면 대기한다.
            if (Blackboard.PathFindHandler.IsArrived) return "Idle";
            // 목적지를 향해 이동한다.
            return "Move";
        }
    }
}