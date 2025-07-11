using System;
using System.Collections;
using System.Collections.Generic;
using GC;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private void Start()
    {
        GameCore.Instance.Battle.EnterBattle();
    }
}
