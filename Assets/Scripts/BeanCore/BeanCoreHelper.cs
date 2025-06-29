using System.Collections;
using System.Collections.Generic;
using BC.LocalData;
using UnityEditor;
using UnityEngine;

namespace BC
{
	/// <summary>
	/// BeanCore의 기능을 편리하게 사용하기 위한 기능을 제공하는 클래스
	/// </summary>
	public class BeanCoreHelper
	{
		[MenuItem("BeanCore/LocalData/ReimportLocalData")]
		private static void ReimportLocalData()
		{
			LocalDataModule.SaveAllData();
		}

		[MenuItem("BeanCore/LocalData/ReimportLocalDataClass")]
		private static void ReimportLocalDataClass()
		{
			LocalDataGenerator.GenerateLocalDataImplement();
		}
	}
}