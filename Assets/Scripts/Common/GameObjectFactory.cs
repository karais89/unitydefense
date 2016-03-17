/**
 * @file GameObjectFactory.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-17
 */

using UnityEngine;

namespace Common
{
    public static class GameObjectFactory
    {
        public static GameObject Instantite(GameObject original)
        {
            GameObject newObj = Instantite( original ) as GameObject;
            if (newObj == null)
            {
                Debug.LogError( "newObj == null, original name = " + original.name );
                return null;
            }
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localScale = Vector3.one;

            return newObj;
        }
    }
}
