using System.Collections;
using System.Collections.Generic;
using BC.LocalData;
using Bo;
using GC.Module;
using UnityEngine;

namespace GC.FSM
{
    /// <summary>
    /// Character의 정보 중 상태에 영향을 줄 수 있는 정보의 모음
    /// </summary>
    public class BbCharacter : IBlackboard, IStatusBlackboard, IDamageableBlackboard, IMovableBlackboard
    {
        /// <summary>
        /// 원본 스테이터스.
        /// 스테이터스가 중간에 변할수도 있기 때문에 원본 스테이터스를 가지고 있는다.
        /// </summary>
        public BoStatus OriginalStatus { get; private set; }
        public BoStatus CurrentStatus { get; private set; }
        public bool IsDead => CurrentHealth <= 0;
        public int CurrentHealth { get; private set; }
        
        public PathFindHandler PathFindHandler { get; private set; }
        
        public Vector2Int CurrentTile => PathFindHandler.CurrentTile;
        public Vector2Int DestTile => PathFindHandler.DestTile;
        public bool IsArrived => PathFindHandler.IsArrived;

        public BbCharacter(LDStatus ldStatus, PathFindHandler pathFindHandler)
        {
            OriginalStatus = new BoStatus(ldStatus);
            CurrentStatus = OriginalStatus.Clone();
            PathFindHandler = pathFindHandler;
        }
        
        public void Init()
        {
            Reset();
        }

        /// <summary>
        /// 초기상태로 돌린다.
        /// </summary>
        public void Reset()
        {
            // 원본 스테이터스로 되돌린다.
            CurrentStatus.Copy(OriginalStatus);
            CurrentHealth = OriginalStatus.MaxHealth;
        }
    }
}