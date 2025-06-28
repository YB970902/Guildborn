using System.Collections.Generic;
using System.IO;
using BC;
using MemoryPack;
using Newtonsoft.Json;
using UnityEngine;
using GC;
using Unity.VisualScripting;


public class TT : MonoBehaviour
{
    [SerializeField] private Vector2Int start;
    [SerializeField] private Vector2Int dest;
    void Start()
    {
        var handler = GameCore.Instance.Battle.PathFind.GetHandler();
        
        handler.GetPathForDebug(start, dest);
    }
}
