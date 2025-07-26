using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ClosedXML.Excel;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class XLSX2JSON
{
    [MenuItem("BeanCore/LocalData/XLSX2JSON")]
    public static void Process()
    {
        var xlsxDir = Path.Combine(Application.dataPath, "Datas/LocalData");
        var jsonDir = Path.Combine(Application.dataPath, "Datas/LocalData/Json");
        Directory.CreateDirectory(jsonDir);

        foreach (var filePath in Directory.GetFiles(xlsxDir, "*.xlsx"))
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var outputPath = Path.Combine(jsonDir, fileName + ".json");

            Debug.Log($"Processing {filePath} -> {outputPath}");

            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1);

            var headers = new List<string>();
            var types = new List<string>();

            // 첫 두 줄: 헤더 + 타입
            var headerRow = worksheet.Row(1);
            var typeRow = worksheet.Row(2);
            int colCount = worksheet.Row(1).CellsUsed().Count();

            for (int col = 1; col <= colCount; col++)
            {
                headers.Add(headerRow.Cell(col).GetString());
                types.Add(typeRow.Cell(col).GetString());
            }

            var datas = new List<Dictionary<string, object>>();
            var rowIndex = 3;

            while (!worksheet.Row(rowIndex).IsEmpty())
            {
                var rowDict = new Dictionary<string, object>();
                var arrayFields = new Dictionary<string, List<object>>();

                for (int col = 0; col < colCount; col++)
                {
                    var key = headers[col];
                    var type = types[col];
                    var cell = worksheet.Row(rowIndex).Cell(col + 1);
                    var value = cell.Value;

                    var match = Regex.Match(key, @"^(.+?)_(\d{3})$");
                    if (match.Success)
                    {
                        var baseKey = match.Groups[1].Value;
                        var index = int.Parse(match.Groups[2].Value);

                        if (!arrayFields.ContainsKey(baseKey))
                            arrayFields[baseKey] = new List<object>();

                        var list = arrayFields[baseKey];
                        while (list.Count <= index)
                            list.Add(null);

                        list[index] = type == "N" ? Convert.ToInt32(value) : value.ToString();
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        if (type == "N")
                        {
                            try
                            {
                                if (string.IsNullOrWhiteSpace(value.ToString()))
                                {
                                    rowDict[key] = 0;
                                }
                                else
                                {
                                    if (double.TryParse(value.ToString(), out var num))
                                        rowDict[key] = (int)num;
                                    else
                                        rowDict[key] = 0;
                                }
                            }
                            catch
                            {
                                rowDict[key] = 0;
                            }
                        }
                        else
                        {
                            rowDict[key] = value.ToString() ?? "";
                        }
                    }
                    else
                    {
                        // 빈 키를 무시하거나 로깅
                        Debug.Log($"⚠️ 유효하지 않은 키 발견 (열 이름 비어 있음). rowIndex={rowIndex}, col={col}");
                    }

                }

                foreach (var kvp in arrayFields)
                {
                    rowDict[kvp.Key] = kvp.Value;
                }

                datas.Add(rowDict);
                rowIndex++;
            }

            var output = new Dictionary<string, object> { ["datas"] = datas };
            File.WriteAllText(outputPath, JsonConvert.SerializeObject(output, Formatting.Indented));
            Debug.Log($"✅ 변환 완료: {outputPath}");
            AssetDatabase.Refresh();
        }
    }
}
#endif