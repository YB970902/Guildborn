using MemoryPack;
using UnityEngine;

[MemoryPackable]
public partial class PlayerData
{
    public string Name;
    public int Score;
}

public class Test : MonoBehaviour
{
    void Start()
    {
        var player = new PlayerData { Name = "Tester", Score = 100 };

        byte[] bytes = MemoryPackSerializer.Serialize(player);
        PlayerData restored = MemoryPackSerializer.Deserialize<PlayerData>(bytes);

        Debug.Log($"Restored: {restored.Name}, {restored.Score}");
    }
}
