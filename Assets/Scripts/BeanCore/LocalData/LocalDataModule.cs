using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MemoryPack;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace BC.LocalData
{
	[MemoryPackable]
	public partial class LocalDataBase
	{
		public int ID;
		public string Key;
	}

	[MemoryPackable]
	public partial class LocalDataList<T> where T : LocalDataBase
	{
		public List<T> datas = new List<T>();
	}

	public partial class LocalDataModule
	{
		public LocalDataModule()
		{
			LoadAllData();
		}

		/// <summary>
		/// bytes 파일을 읽어서 역직렬화하여 불러온다.
		/// </summary>
		private LocalDataList<T> LoadData<T>() where T : LocalDataBase
		{
			string bytesPath = Path.Combine(Application.dataPath, $"Datas/LocalData/Bytes/{typeof(T).Name}.bytes");
			byte[] bytes = File.ReadAllBytes(bytesPath);
			return MemoryPackSerializer.Deserialize<LocalDataList<T>>(bytes);
		}

		/// <summary>
		/// json 파일을 읽어서 bytes파일로 변환하여 저장한다.
		/// </summary>
		private static void SaveData<T>() where T : LocalDataBase
		{
			string bytesPath = Path.Combine(Application.dataPath, $"Datas/LocalData/Bytes/{typeof(T).Name}.bytes");
			string jsonPath = Path.Combine(Application.dataPath, $"Datas/LocalData/Json/{typeof(T).Name}.json");
			string json = File.ReadAllText(jsonPath);

			LocalDataList<T> dataList = JsonConvert.DeserializeObject<LocalDataList<T>>(json);

			byte[] bytes = MemoryPackSerializer.Serialize(dataList);
			File.WriteAllBytes(bytesPath, bytes);
			Debug.Log($"SaveData : {typeof(T).Name}");
		}
	}

	/// <summary>
	/// 로컬데이터가 변경되면 새로 bytes파일을 만들어서 저장하는 기능을 위한 클래스
	/// </summary>
	public class LocalDataReimporter : AssetPostprocessor
	{
		/// <summary>
		/// 데이터가 변경된것을 감지한다. 로컬데이터가 변경되었다면, bytes파일을 새로 만든다.
		/// </summary>
		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			foreach(string path in importedAssets)
			{
				if (IsLocalData(path))
				{
					// 로컬데이터가 하나라도 새로 추가되거나 변경되었다면, bytes파일을 새로 만든다.
					LocalDataModule.SaveAllData();
					Debug.Log($"LocalData Reimported!");
					break;
				}
			}
		}

		/// <summary>
		/// 로컬데이터 내의 json파일인지 여부
		/// </summary>
		private static bool IsLocalData(string path)
		{
			// 확장자가 .JSON일 수도 있어서 ToLowerInvariant로 소문자로 맞춘 뒤 비교
			return path.ToLowerInvariant().EndsWith(".json") &&
			       path.Replace("\\", "/").StartsWith("Assets/Datas/LocalData/Json/");
		}
	}
}