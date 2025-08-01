using System.Collections;
using System.Collections.Generic;
using BC.Addressable;
using BC.LocalData;
using BC.Utils;
using GC.Utils.Define;
using UnityEngine;

namespace GC.Module
{
	/// <summary>
	/// 맵 모듈
	/// 지형 정보를 가지고 있으며, 길찾기도 수행한다.
	/// 유닛이 길찾기를 하기 위해선 이 모듈로부터 PathFindHandler를 받아야 한다.
	/// </summary>
	public class MapModule
	{
		public PathFinder PathFinder { get; private set; }

		private ObjectPool<PathFindHandler> handlerPool;

		private LDMap ldMap;
		private TileMapData tileMapData;

		public MapModule()
		{
			PathFinder = new PathFinder();
			handlerPool = new ObjectPool<PathFindHandler>();
		}

		public void Init()
		{
			handlerPool.Init();
		}

		public void LoadMap(LDMap ldMap)
		{
			this.ldMap = ldMap;
			var prefabMap = AddressableManager.Instance.LoadAssetSync<GameObject>($"Prefabs/Battle/MapDatas/{ldMap.Name}.prefab", DefineAddressable.Group.Battle);
			tileMapData = GameObject.Instantiate(prefabMap).GetComponent<TileMapData>();
		}

		public void UnloadMap()
		{
			GameObject.Destroy(tileMapData);
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