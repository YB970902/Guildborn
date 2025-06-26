using System.Collections;
using System.Collections.Generic;
using BC.LocalData;
using BC.Utils;
using UnityEngine;

namespace BC
{
    /// <summary>
    /// 여러 사이드 프로젝트에서 사용할 공통 기능을 모아둔 클래스
    /// 특정 프로젝트에 종속적이지 않고 여러 곳에서 사용될 기능들이 여기에 포함된다.
    /// </summary>
    public class BeanCore : MonoSingleton<BeanCore>
    {
        public LocalDataModule LD { get; private set; }

        protected override void OnInit()
        {
            base.OnInit();

            LD = new LocalDataModule();
        }
    }
}