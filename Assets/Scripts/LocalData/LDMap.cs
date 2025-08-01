using System.Collections;
using System.Collections.Generic;
using MemoryPack;
using UnityEngine;

namespace BC.LocalData
{
	[MemoryPackable]
	public partial class LDMap : LocalDataBase
	{
		/// <summary>
		/// 맵 프리팹 파일의 이름
		/// </summary>
		public string Name;
	}
}