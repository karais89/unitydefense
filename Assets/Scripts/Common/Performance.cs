/**
 * @file Performance.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-10-29
 */

using UnityEngine;

namespace Common
{
    public class Performance : MonoBehaviour
    {
        private LightShadows shadowType = LightShadows.None;

        private void Awake()
        {
            shadowType = LightShadows.Hard;

            GameObject.Find( "Directional light" ).GetComponent<Light>().shadows = shadowType;
        }
        
    }
}
