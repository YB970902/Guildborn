using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
            }
            
            return instance;
        }
    }

    private static T instance;

    private static bool isInit = false;

    protected void Awake()
    {
        if (isInit)
        {
            Destroy(gameObject);
            return;
        }
        
        OnInit();
    }

    // 초기화 시 1회 호출하는 함수
    protected virtual void OnInit()
    {
        if(instance == null) instance = GetComponent<T>();
        DontDestroyOnLoad(gameObject);
        isInit = true;
    }
}
