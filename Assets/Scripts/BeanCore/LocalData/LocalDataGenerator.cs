using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace BC.LocalData
{
	/// <summary>
	/// LocalDataBase를 상속받은 클래스가 생성되면, 자동으로 내용을 LocalDataImplement에 추가해주는 제너레이터
	/// </summary>
	public class LocalDataGenerator : AssetPostprocessor
	{
		// LocalDataBase를 상속받은 클래스인지 확인하기위한 정규식
		private static readonly Regex classRegex = new Regex(@"public\s+partial\s+class\s+(\w+)\s*:\s*LocalDataBase");
		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			foreach (var path in importedAssets)
			{
				// 로컬데이터 경로 내에 생성된 스크립트 파일인경우
				if (path.Replace("\\", "/").StartsWith("Assets/Scripts/LocalData/") &&
				    path.ToLowerInvariant().EndsWith(".cs"))
				{
					string code = File.ReadAllText(path);
					var match = classRegex.Match(code);
					// LocalDataBase를 상속받은 클래스가 있을때만 생성한다. 
					if (!match.Success) continue;

					// 스크립트 파일을 수정한다.
					GenerateLocalDataImplement();
					break;
				}
			}
		}

		/// <summary>
		/// LocalDataImplement.cs 파일을 생성하는 함수
		/// </summary>
		public static void GenerateLocalDataImplement()
		{
			List<string> classNames = FindDerivedTypes<LocalDataBase>();
			
			StringBuilder sb =new StringBuilder();
			
			sb.AppendLine("// \u26a0\ufe0f 이 파일은 자동 생성되었습니다. 수정하지 마세요!!");
			sb.AppendLine("namespace BC.LocalData");
			sb.AppendLine("{");
			sb.AppendLine("\tpublic partial class LocalDataModule");
			sb.AppendLine("\t{");
			
			foreach (string className in classNames)
			{
				string varName = className.Substring(2);
				sb.AppendLine($"\t\tpublic LocalDataList<{className}> {varName} {{get; private set; }}");
			}
			
			sb.AppendLine();
			sb.AppendLine("\t\tpublic static void SaveAllData()");
			sb.AppendLine("\t\t{");
			foreach (string className in classNames)
			{
				sb.AppendLine($"\t\t\tSaveData<{className}>();");
			}
			sb.AppendLine("\t\t}");
			sb.AppendLine();
			sb.AppendLine("\t\tpublic void LoadAllData()");
			sb.AppendLine("\t\t{");
			foreach (string className in classNames)
			{
				string varName = className.Substring(2);
				sb.AppendLine($"\t\t\t{varName} = LoadData<{className}>();");
			}
			sb.AppendLine("\t\t}");
			sb.AppendLine("\t}");
			sb.AppendLine("}");
			
			File.WriteAllText("Assets/Scripts/BeanCore/LocalData/LocalDataImplement.cs", sb.ToString());
			AssetDatabase.Refresh();
		}

		/// <summary>
		/// T 타입을 상속받은 모든 클래스의 이름을 반환한다.
		/// </summary>
		private static List<string> FindDerivedTypes<T>()
		{
			List<string> result = new List<string>();

			foreach (var type in TypeCache.GetTypesDerivedFrom<T>())
			{
				if (!type.IsAbstract && type.IsClass)
				{
					result.Add(type.Name);
				}
			}

			return result;
		}
	}
}
#endif