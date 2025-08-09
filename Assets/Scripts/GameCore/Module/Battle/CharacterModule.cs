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

		/// <summary>
		/// 캐릭터를 필드에 소환한다.
		/// </summary>
		public void SpawnCharacterAtField(long unitIdx, int ownerId, int characterId, int fieldId)
		{
			SpawnCharacter(unitIdx, ownerId, characterId);
			var character = characters[unitIdx];
			character.SetFieldTile(fieldId);
		}
		
		/// <summary>
		/// 캐릭터를 대기석에 소환한다.
		/// </summary>
		public void SpawnCharacterAtWait(long unitIdx, int ownerId, int characterId, int waitId)
		{
			SpawnCharacter(unitIdx, ownerId, characterId);
			var character = characters[unitIdx];
			character.SetWaitTile(waitId);
		}

		/// <summary>
		/// 캐릭터를 생성한다.
		/// </summary>
		private void SpawnCharacter(long unitIdx, int ownerId, int characterId)
		{
			if (characters.ContainsKey(unitIdx))
			{
				Debug.LogError("CharacterModule.AddCharacter: Character Already Exists");
				return;
			}
			
			characters[unitIdx] = new BoCharacter(unitIdx, ownerId, BeanCore.Instance.LD.Character[characterId]);
			characters[unitIdx].Init();
		}

		public void Update()
		{
			foreach (KeyValuePair<long, BoCharacter> kvp in characters)
			{
				kvp.Value.Update();
			}
		}
	}
}