using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GC.Module
{
    /// <summary>
    /// GameDataModule이 관리할 데이터들이 공통적으로 구현해야할 인터페이스
    /// </summary>
    public interface IGameDataPack
    {
        /// <summary> 초기화 함수. 초기화 시점을 조절하기 위해 존재한다. </summary>
        public void Init();
    }
    
    /// <summary>
    /// 게임에서 사용하는 데이터 모듈.
    /// LocalData로 저장하고 사용할 수 없는 데이터는 여기에 저장된다.
    /// </summary>
    public class GameDataModule : MonoBehaviour
    {
        #region BattleData
        
        /// <summary> 타일의 월드좌표 정보 </summary>
        public TilePositionPack TilePositionPack { get; private set; }
        
        #endregion

        /// <summary>
        /// 전투와 관련된 데이터를 로드한다.
        /// </summary>
        public void LoadBattleData()
        {
            TilePositionPack = FindAnyObjectByType<TilePositionPack>();
            TilePositionPack.Init();
        }

        /// <summary>
        /// 전투와 관련된 데이터를 해제한다.
        /// </summary>
        public void UnloadBattleData()
        {
            TilePositionPack = null;
        }
    }
}