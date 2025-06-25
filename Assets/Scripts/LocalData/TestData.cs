using System.Collections.Generic;
using BC.LocalData;
using MemoryPack;

[MemoryPackable]
public partial class TestData : LocalDataBase
{
	public string Name;
	public int HP;
	public int MP;
	public List<string> Param;
}