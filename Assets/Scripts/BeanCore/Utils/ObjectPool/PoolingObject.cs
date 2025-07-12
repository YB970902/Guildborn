using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC.Utils
{
    /// <summary>
    /// 오브젝트 풀에 들어갈 수 있는 오브젝트
    /// </summary>
    public interface IPoolingObject
    {
        /// <summary>
        /// 초기화
        /// </summary>
        void Init(IObjectPool pool);
        /// <summary>
        /// 풀에 집어넣기 직전에 호출된다.
        /// </summary>
        void Sleep();
        /// <summary>
        /// 풀에서 꺼낸 직후에 호출된다.
        /// </summary>
        void WakeUp();
        /// <summary>
        /// 풀로 집어넣는다.
        /// </summary>
        void ReturnToPool();
    }

    public class PoolingObject<T> : IPoolingObject where T : class, new()
    {
        public ObjectPool<PoolingObject<T>> pool;

        /// <summary>
        /// 현재 오브젝트가 잠들었는지 여부
        /// </summary>
        public bool IsSleep { get; private set; }

        protected virtual void OnInit() { }
        protected virtual void OnSleep() { }
        protected virtual void OnWakeUp() { }

        public void Init(IObjectPool pool)
        {
            this.pool = pool as ObjectPool<PoolingObject<T>>;
            IsSleep = false;
            OnInit();
        }

        public void Sleep()
        {
            if (IsSleep) return;

            IsSleep = true;
            OnSleep();
        }

        public void WakeUp()
        {
            if (!IsSleep) return;
            
            IsSleep = false;
            OnWakeUp();
        }

        public void ReturnToPool()
        {
            pool.Push(this);
        }
    }
}