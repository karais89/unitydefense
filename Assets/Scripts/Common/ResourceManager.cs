/**
 * @file ResourceManager.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-11-26
 */

using UnityEngine;

namespace Common
{
    public class ResourceManager : MonoBehaviour
    {
        private static ResourceManager resourceManager;

        private void Awake()
        {
            resourceManager = this;
        }

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public GameObject Load( string prefabName )
        {
            GameObject obj = null;

            obj = (GameObject) Resources.Load( "Prefabs/" + prefabName );

            return obj;
        }
    }
}

