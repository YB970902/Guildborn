using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BC.Utils
{
    /// <summary>
    /// 편의를 위한 확장 메서드를 제공한다.
    /// </summary>
    public static class BeanExtensions
    {
        #region Collection

        /// <summary>
        /// 해당 컬렉션이 null이거나 비어있는지 여부
        /// </summary>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }
        #endregion
        
        #region Vector

        public static void Copy(this Vector2 vec, in Vector2 target)
        {
            vec.x = target.x;
            vec.y = target.y;
        }

        public static void Copy(this Vector3 vec, in Vector3 target)
        {
            vec.x = target.x;
            vec.y = target.y;
            vec.z = target.z;
        }
        
        public static void Copy(this Vector2Int vec, in Vector2Int target)
        {
            vec.x = target.x;
            vec.y = target.y;
        }

        public static void Copy(this Vector3Int vec, in Vector3Int target)
        {
            vec.x = target.x;
            vec.y = target.y;
            vec.z = target.z;
        }
        
        
        #endregion
    }
}