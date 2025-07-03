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
    }
}