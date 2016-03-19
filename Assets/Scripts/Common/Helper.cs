/**
 * @file Helper.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public static class Helper
    {
        // FIXIT
        // not working yet
        public static void ClearEx<T>( List<T> _list ) where T : List<T> 
        {
            if (_list.Count > 0)
            {
                _list.Clear();
            }
            else
            {
                Debug.LogError( "_list.Count = " + _list.Count );

            }
        }
        
        public static void SetParentEx(this Transform transform, Transform parent)
        {
            transform.SetParent( parent );
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }
    }
    
}