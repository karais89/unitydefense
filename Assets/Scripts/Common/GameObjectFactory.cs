/**
 * @file GameObjectFactory.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-17
 */

using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public static class GameObjectFactory
    {
        public static GameObject Instantiate(GameObject original)
        {
            if (original == null)
            {
                Debug.LogError( "original == null" );
                return null;
            }
            
            GameObject newObj = UnityEngine.Object.Instantiate( original );
            if (newObj == null)
            {
                Debug.LogError( "newObj == null, original name = " + original.name );
                return null;
            }
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localScale = Vector3.one;

            return newObj;
        }

        public static GameObject InstantiateUI(GameObject original)
        {
            if ( original == null )
            {
                Debug.LogError( "original == null" );
                return null;
            }

            GameObject newObj = UnityEngine.Object.Instantiate( original );
            if ( newObj == null )
            {
                Debug.LogError( "newObj == null, original name = " + original.name );
                return null;
            }
            newObj.GetComponent<RectTransform>().localPosition = Vector3.zero;
            newObj.GetComponent<RectTransform>().localScale = Vector3.one;
            
            return newObj;
        }
    }
}
