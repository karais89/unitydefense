/**
 * @file ResourceManager.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-11-26
 */

using UnityEngine;
using System.Collections.Generic;

namespace Common
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        public enum ePrefabType
        {
            None = 0,
            Tile,
            Object,
            Monster,
            Tower,
            Max
        }
        
        public enum ePrefabTile
        {
            None = -1,
            PavementTile_01,
            PavementTile_02,
            PavementTile_03,
            PavementTile_04,
            PavementTile_05,
            PavementTile_06,
            PavementTile_07,
            PavementTile_08,
            PavementTile_09,
            PavementTile_10,
            PavementTile_11,
            PavementTile_12,
            PavementTile_13,
            PavementTile_14,
            PavementTile_15,
            PavementTile_16,
            PavementTile_17,
            PavementTile_18,
            PavementTile_19,
            PavementTile_20,
            Max
        }

        public enum ePrefabTilePreview
        {
            None = -1,
            PavementTilePreview_01,
            PavementTilePreview_02,
            PavementTilePreview_03,
            PavementTilePreview_04,
            PavementTilePreview_05,
            PavementTilePreview_06,
            PavementTilePreview_07,
            PavementTilePreview_08,
            PavementTilePreview_09,
            PavementTilePreview_10,
            PavementTilePreview_11,
            PavementTilePreview_12,
            PavementTilePreview_13,
            PavementTilePreview_14,
            PavementTilePreview_15,
            PavementTilePreview_16,
            PavementTilePreview_17,
            PavementTilePreview_18,
            PavementTilePreview_19,
            PavementTilePreview_20,
            Max
        }

        private List<GameObject> m_gTilesList = new List<GameObject>();

        
        public List<GameObject> GTilesList
        {
            get
            {
                return m_gTilesList;
            }
        }
        

        public GameObject LoadPrefab( string name )
        {
            GameObject obj = null;

            obj = Resources.Load<GameObject>( "Prefabs/" + name );
            if (obj == null)
            {
                Debug.LogError( "obj null, prefabName = " + name );
            }
            return obj;
        }

        public GameObject Load(string fullPathAndName)
        {
            GameObject obj = null;
            obj = Resources.Load<GameObject>( fullPathAndName );
            if ( obj == null )
            {
                Debug.LogError( "obj null, pathAndName = " + fullPathAndName );
            }
            return obj;
        }
        
        public List<GameObject> LoadAllTiles()
        {
            if (m_gTilesList.Count > 0)
            {
                Debug.Log( "all tiles are already loaded" );
                return m_gTilesList;
            }

            for (int i = 0; i < (int)ePrefabTile.Max; i++ )
            {
                ePrefabTile tile = (ePrefabTile) i;
                string prefabName = tile.ToString();
                GameObject newObject = LoadPrefab( prefabName );
                m_gTilesList.Add(newObject);
            }

            return m_gTilesList;
        }
        
    }
}

