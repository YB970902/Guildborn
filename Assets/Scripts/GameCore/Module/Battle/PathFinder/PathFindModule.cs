using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using UnityEngine;

namespace GC.Module
{
	/// <summary>
	/// 길찾기 모듈
	/// 유닛이 길찾기를 하기 위해선 이 모듈에서 PathFindHandler를 받아야 한다.
	/// </summary>
	public class PathFindModule
	{
		public PathFinder PathFinder { get; private set; }

		private ObjectPool<PathFindHandler> handlerPool;

		public PathFindModule()
		{
			PathFinder = new PathFinder();
			PathFinder.Init();
			handlerPool = new ObjectPool<PathFindHandler>();
		}

		public void Init()
		{
			PathFinder.Init();
			handlerPool.Init();
		}

		/// <summary>
		/// 핸들러를 반환한다.
		/// </summary>
		public PathFindHandler GetHandler()
		{
			return handlerPool.Pop();
		}
	}
}