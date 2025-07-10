using System.Collections;
using System.Collections.Generic;
using MemoryPack;
using UnityEngine;

namespace BC.LocalData
{
	[MemoryPackable]
	public partial class LDCharacter : LocalDataBase
	{
		/// <summary>
		/// 캐릭터 이름
		/// </summary>
		public string Name;
		/// <summary>
		/// 캐릭터 스테이터스. LDStatus와 연결되는 ID
		/// </summary>
		public int StatusID;
	}
}