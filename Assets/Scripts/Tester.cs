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

    private void Update()
    {
        if (GameCore.Instance.Battle.IsBattleStart == false) return;
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameCore.Instance.Battle.Command.SpawnCharacter(0, 0, 1);
        }
    }
}
