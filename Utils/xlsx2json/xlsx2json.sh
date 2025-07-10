#!/bin/bash

# Json 폴더 생성
mkdir -p Json

# 모든 .xlsx 파일 처리
for file in *.xlsx; do
  [ -e "$file" ] || continue
  filename="${file%.xlsx}"
  echo "Processing $file -> Json/$filename.json"

  python3 - <<EOF
import pandas as pd
import json
import re
import os

# 파일 읽기 (1행: 키, 2행: 타입)
df = pd.read_excel("$file", engine="openpyxl", header=[0, 1])
json_list = []

for _, row in df.iterrows():
    item = {}
    array_fields = {}

    for (col_name, col_type) in df.columns:
        value = row[(col_name, col_type)]

        match = re.match(r"(.+?)_(\d{3})$", col_name)
        if match:
            base, idx_str = match.groups()
            idx = int(idx_str)
            if base not in array_fields:
                array_fields[base] = []
            while len(array_fields[base]) <= idx:
                array_fields[base].append(None)
            if col_type == "N":
                array_fields[base][idx] = int(value) if not pd.isna(value) else 0
            else:
                array_fields[base][idx] = str(value)
            continue

        if col_type == "N":
            item[col_name] = int(value) if not pd.isna(value) else 0
        else:
            item[col_name] = str(value)

    for key, arr in array_fields.items():
        item[key] = arr

    json_list.append(item)

# datas 키로 감싸기
output_dict = {
    "datas": json_list
}

# 저장
output_path = os.path.join("Json", f"$filename.json")
with open(output_path, "w", encoding="utf-8") as f:
    json.dump(output_dict, f, indent=2, ensure_ascii=False)

print(f"✅ 변환 완료: Json/$filename.json")
EOF

done
