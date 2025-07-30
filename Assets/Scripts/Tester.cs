using System;
using System.Collections;
using System.Collections.Generic;
using GC;
using UnityEngine;

public class Tester : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        GameCore.Instance.Battle.EnterBattle();
    }
}
