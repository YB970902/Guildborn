using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bo
{
	public class BoCharacter
	{
		/// <summary> 캐릭터를 구분할 수 있는 고유 인덱스 </summary>
		public long CharacterIdx { get; private set; }
		/// <summary> 캐릭터 소유자의 아이디 </summary>
		public int OwnerID { get; private set; }
		/// <summary> 캐릭터의 스텟 </summary>
		public BoStatus Status { get; private set; }
		/// <summary> 캐릭터의 현재 체력 </summary>
		public int CurrentHealth { get; private set; }
	}
}