using System.Collections;
using System.Collections.Generic;
using BC;
using Bo;
using UnityEngine;

namespace GC.Module
{
	/// <summary>
	/// 유닛을 관리하는 모듈
	/// </summary>
	public class CharacterModule
	{
		private Dictionary<long, BoCharacter> characters;
		
		public CharacterModule()
		{
			characters = new Dictionary<long, BoCharacter>();
		}

		public void Init()
		{
			
		}

		/// <summary>
		/// 데이터를 모두 지운다.
		/// </summary>
		public void Clear()
		{
			characters.Clear();		
		}

		public void AddCharacter(long unitIdx, int ownerId, int characterId)
		{
			if (characters.ContainsKey(unitIdx))
			{
				Debug.LogError("CharacterModule.AddCharacter: Character Already Exists");
				return;
			}
			
			characters[unitIdx] = new BoCharacter(unitIdx, ownerId, BeanCore.Instance.LD.Character[characterId]);
		}
	}
}