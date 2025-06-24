using System.Collections;
using System.Collections.Generic;
using System.IO;
using MemoryPack;
using Newtonsoft.Json;
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
	public partial class Test : LocalDataBase
	{
		public string Name;
		public int HP;
		public int MP;
		public List<string> Param;
	}

	[MemoryPackable]
	public partial class LocalDataList<T> where T : LocalDataBase
	{
		public List<T> datas = new List<T>();
	}

	public class LocalDataModule
	{
		public LocalDataList<Test> Test { get; private set; }
		public LocalDataModule()
		{
			LoadAllData();
		}

		/// <summary>
		/// 모든 로컬데이터를 Bytes파일로 변환하여 저장한다.
		/// </summary>
		public void SaveAllData()
		{
			SaveData<Test>();
		}

		public void LoadAllData()
		{
			Test = LoadData<Test>();
		}

		/// <summary>
		/// bytes 파일을 읽어서 역직렬화하여 불러온다.
		/// </summary>
		public LocalDataList<T> LoadData<T>() where T : LocalDataBase
		{
			string bytesPath = Path.Combine(Application.dataPath, $"Datas/LocalData/Bytes/{typeof(T).Name}.bytes");
			byte[] bytes = File.ReadAllBytes(bytesPath);
			return MemoryPackSerializer.Deserialize<LocalDataList<T>>(bytes);
		}

		/// <summary>
		/// json 파일을 읽어서 bytes파일로 변환하여 저장한다.
		/// </summary>
		public void SaveData<T>() where T : LocalDataBase
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
}