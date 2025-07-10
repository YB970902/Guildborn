// ⚠️ 이 파일은 자동 생성되었습니다. 수정하지 마세요!!
namespace BC.LocalData
{
	public partial class LocalDataModule
	{

		public LocalDataList<LDStatus> LDStatus {get; private set; }
		public LocalDataList<LDCharacter> LDCharacter {get; private set; }

		public static void SaveAllData()
		{
			SaveData<LDStatus>();
			SaveData<LDCharacter>();
		}

		public void LoadAllData()
		{
			LDStatus = LoadData<LDStatus>();
			LDCharacter = LoadData<LDCharacter>();
		}
	}
}
