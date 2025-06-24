using System.Collections.Generic;
using System.IO;
using BC;
using MemoryPack;
using Newtonsoft.Json;
using UnityEngine;



public class TT : MonoBehaviour
{
    void Start()
    {
        var test = BeanCore.Instance.LD.Test.datas[0];
        Debug.Log(test.Name);
        Debug.Log(test);
    }
}
