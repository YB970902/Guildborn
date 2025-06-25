// ⚠️ 이 파일은 자동 생성되었습니다. 수정하지 마세요!!
namespace BC.LocalData
{
	public partial class LocalDataModule
	{

		public LocalDataList<TestData> TestData {get; private set; }
		public LocalDataList<TestDataA> TestDataA {get; private set; }

		public static void SaveAllData()
		{
			SaveData<TestData>();
			SaveData<TestDataA>();
		}

		public void LoadAllData()
		{
			TestData = LoadData<TestData>();
			TestDataA = LoadData<TestDataA>();
		}
	}
}
