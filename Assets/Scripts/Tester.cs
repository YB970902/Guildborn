using System;
using System.Collections;
using System.Collections.Generic;
using BC.Utils;
using GC;
using GC.Module;
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
            //GameCore.Instance.Battle.Command.SpawnCharacterAtField(0, 0, 1, 0);
            GameCore.Instance.Battle.Command.SpawnCharacterAtWait(0, 0, 1, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var hits = Physics2D.RaycastAll(mousePos, Vector2.zero);
            if (hits.IsNullOrEmpty()) return;

            foreach (var hit in hits)
            {
                if (hit.transform.CompareTag("Tile") == false) continue;
                
                TileData tileData = hit.transform.GetComponent<TileData>();
                ELog.Log($"TileType: {tileData.TileType}, TileID : {tileData.TileID}");
            }
        }
    }
}
