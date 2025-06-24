using System.Collections;
using System.Collections.Generic;
using BC.LocalData;
using UnityEngine;

namespace BC
{
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