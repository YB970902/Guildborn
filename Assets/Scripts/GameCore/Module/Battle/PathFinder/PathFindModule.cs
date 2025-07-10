using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC.Module
{
	/// <summary>
	/// 길찾기 모듈
	/// 유닛이 길찾기를 하기 위해선 이 모듈에서 PathFindHandler를 받아야 한다.
	/// </summary>
	public class PathFindModule
	{
		private PathFinder pathFinder;

		private List<PathFindHandler> handler;

		public PathFindModule()
		{
			pathFinder = new PathFinder();
			pathFinder.Init();
			// TODO : 임시 코드. 추후에 풀을 생성하는 방식으로 수정
			handler = new List<PathFindHandler>();
		}

		public void Init()
		{
			pathFinder.Init();
		}

		/// <summary>
		/// 핸들러를 반환한다.
		/// </summary>
		public PathFindHandler GetHandler()
		{
			// TODO : 지금은 임시 코드. 오브젝트 풀로 관리하기.
			return new PathFindHandler(pathFinder);
		}
	}
}