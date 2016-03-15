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
    static public class Helper
    {
        // FIXIT
        // 현재 제대로 작동 안함...
        static public void ClearEx<T>( List<T> _list ) where T : List<T> 
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
    }
}