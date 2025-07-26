using System;
using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace BC.Addressable
{
	public class AddressableManager : MonoSingleton<AddressableManager>
	{
		interface IAssetData
		{
			public void Unload();
		}

		/// <summary>
		/// 애셋의 정보를 담고있는 클래스
		/// </summary>
		public class AssetData<T> : IAssetData where T : Object
		{
			/// <summary> 애셋의 경로 </summary>
			public string Path { get; private set; }
			/// <summary> 애셋의 로드가 완료되면 호출할 함수 </summary>
			public Action<T> Callback { get; set; }
			/// <summary> 애셋이 로드됐는지 감지하기 위한 핸들 </summary>
			public AsyncOperationHandle<T> Handle { get; private set; }

			public AssetData(string path, AsyncOperationHandle<T> handle)
			{
				Path = path;
				Handle = handle;

				Handle.Completed += OnLoadComplete;
			}

			private void OnLoadComplete(AsyncOperationHandle<T> handle)
			{
				Callback?.Invoke(handle.Result);
				Callback = null;
			}

			/// <summary>
			/// 애셋을 언로드한다.
			/// </summary>
			public void Unload()
			{
				Addressables.Release(Handle);
			}
		}

		/// <summary>
		/// 로드된 모든 애셋을 가지고 있는 딕셔너리.
		/// </summary>
		private Dictionary<string, IAssetData> dictAsset;
		
		/// <summary>
		/// 애셋을 그룹 단위로 가지고 있는 딕셔너리.
		/// </summary>
		private Dictionary<string, List<IAssetData>> dictAssetsGroup;

		protected override void OnInit()
		{
			base.OnInit();
			
			dictAsset = new Dictionary<string, IAssetData>();
			dictAssetsGroup = new Dictionary<string, List<IAssetData>>();
		}

		/// <summary>
		/// 애셋을 비동기로 로드한다.
		/// </summary>
		/// <param name="path"> 애셋의 어드레스 </param>
		/// <param name="callback"> 애셋 로드가 완료되면 호출될 함수 </param>
		/// <param name="groupKey"> 애셋의 그룹 </param>
		/// <typeparam name="T"> 애셋의 타입 </typeparam>
		public void LoadAssetAsync<T>(string path, Action<T> callback, string groupKey = "") where T : Object
		{
			// 이미 애셋을 로드한 적이 있는 경우
			if (dictAsset.TryGetValue(path, out IAssetData assetInterfaceData))
			{
				AssetData<T> assetData = assetInterfaceData as AssetData<T>;
				// 애셋 로드가 완료된경우 즉시 콜백을 호출한다.
				if (assetData.Handle.IsDone) callback?.Invoke(assetData.Handle.Result);
				// 애셋 로드가 아직 완료되지 않은 경우 콜백에 추가한다. 
				else assetData.Callback += callback;
				return;
			}
			
			// 애셋 데이터를 새로 생성한다.
			var handle = Addressables.LoadAssetAsync<T>(path);
			AssetData<T> newAsset = new AssetData<T>(path, handle);
			dictAsset.Add(path, newAsset);
			newAsset.Callback += callback;

			// 애셋을 그룹단위 묶는다.
			if (dictAssetsGroup.TryGetValue(groupKey, out List<IAssetData> list)) list.Add(newAsset);
			else dictAssetsGroup.Add(groupKey, new List<IAssetData>() { newAsset });
		}
		
		/// <summary>
		/// 애셋을 동기로 로드한다.
		/// </summary>
		/// <param name="path"> 애셋의 경로 </param>
		/// <param name="groupKey"> 애셋의 그룹</param>
		/// <typeparam name="T"> 애셋의 타입</typeparam>
		public T LoadAssetSync<T>(string path, string groupKey = "") where T : Object
		{
			// 애셋 데이터가 있다면 즉시 반환한다. 만약 아직 로드가 끝나지 않았다면, 기다렸다가 반환한다.
			if (dictAsset.TryGetValue(path, out IAssetData assetInterfaceData))
			{
				AssetData<T> assetData = assetInterfaceData as AssetData<T>;
				if(assetData.Handle.IsDone) return assetData.Handle.Result;
				assetData.Handle.WaitForCompletion();
				return assetData.Handle.Result;
			}
			
			// 애셋을 추가한다.
			var handle = Addressables.LoadAssetAsync<T>(path);
			AssetData<T> newAsset = new AssetData<T>(path, handle);
			dictAsset.Add(path, newAsset);
			
			// 애셋을 그룹단위 묶는다.
			if (dictAssetsGroup.TryGetValue(groupKey, out List<IAssetData> list)) list.Add(newAsset);
			else dictAssetsGroup.Add(groupKey, new List<IAssetData>() { newAsset });

			// 로드가 끝날때까지 기다렸다가 반환한다.
			handle.WaitForCompletion();
			return handle.Result;
		}

		/// <summary>
		/// 애셋을 그룹단위로 언로드한다.
		/// </summary>
		/// <param name="groupKey"> 그룹의 키 </param>
		public void UnloadAsset(string groupKey)
		{
			if (dictAssetsGroup.TryGetValue(groupKey, out List<IAssetData> list))
			{
				foreach (var assetData in list)
				{
					assetData.Unload();
				}
				dictAssetsGroup.Remove(groupKey);
			}
		}
	}
}