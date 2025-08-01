// ⚠️ 이 파일은 자동 생성되었습니다. 수정하지 마세요!!
namespace BC.LocalData
{
	public partial class LocalDataModule
	{
		public LocalDataList<LDStatus> Status {get; private set; }
		public LocalDataList<LDCharacter> Character {get; private set; }
		public LocalDataList<LDMap> Map {get; private set; }

		public static void SaveAllData()
		{
			SaveData<LDStatus>();
			SaveData<LDCharacter>();
			SaveData<LDMap>();
		}

		public void LoadAllData()
		{
			Status = LoadData<LDStatus>();
			Character = LoadData<LDCharacter>();
			Map = LoadData<LDMap>();
		}
	}
}
