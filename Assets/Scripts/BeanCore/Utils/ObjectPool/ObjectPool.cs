using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC.Utils
{
	/// <summary>
	/// ObjectPool을 추상화하기 위한 인터페이스 
	/// </summary>
	public interface IObjectPool { }
	
	public class ObjectPool<T> : IObjectPool where T : IPoolingObject, new()
	{
		/// <summary> 풀이 관리하고 있는 모든 오브젝트 리스트 </summary>
		private List<T> allObjects;
		/// <summary> 현재 풀 내에 있는 모든 오브젝트 리스트 </summary>
		private List<T> objects;

		public ObjectPool()
		{
			allObjects = new List<T>();
			objects = new List<T>();
		}

		public void Init(int initializeSize = 0)
		{
			allObjects.Capacity = initializeSize;
			objects.Capacity = initializeSize;
			for (int i = 0; i < initializeSize; ++i)
			{
				CreateObject();
			}
		}
		
		private T CreateObject()
		{
			T newPoolingObject = new T();
			allObjects.Add(newPoolingObject);
			objects.Add(newPoolingObject);
			newPoolingObject.Init(this);
			newPoolingObject.Sleep();
			return newPoolingObject;
		}
		
		public void Push(T poolingObject)
		{
			poolingObject.Sleep();
			objects.Add(poolingObject);
		}

		public T Pop()
		{
			if (objects.IsNullOrEmpty())
			{
				return CreateObject();
			}

			T result = objects[^1];
			objects.RemoveAt(objects.Count - 1);
			result.WakeUp();
			return result;
		}
	}
}