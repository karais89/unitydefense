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
        
        public GameObject LoadPrefab( string name )
        {
            GameObject obj = null;

            obj = (GameObject) Resources.Load( "Prefabs/" + name );
            if (obj == null)
            {
                Debug.LogError( "obj null, prefabName = " + name );
            }
            return obj;
        }

        public GameObject Load(string pathAndName)
        {
            GameObject obj = null;
            obj = (GameObject) Resources.Load( pathAndName );
            if ( obj == null )
            {
                Debug.LogError( "obj null, pathAndName = " + pathAndName );
            }
            return obj;
        }

        private List<GameObject> m_gTilesList = new List<GameObject>();

        public List<GameObject> GTilesList
        {
            get
            {
                return m_gTilesList;
            }
        }

        public List<GameObject> LoadAllTiles()
        {
            if (m_gTilesList.Count > 0)
            {
                Debug.LogWarning( "m_gTilesList.Count = " + m_gTilesList.Count );
                m_gTilesList.Clear();
            }

            for (int i = 0; i < (int)ePrefabTile.Max; i++ )
            {
                ePrefabTile tile = (ePrefabTile) i;
                string prefabName = tile.ToString();
                GameObject newObject = Load( prefabName );
                m_gTilesList.Add(newObject);
            }

            return m_gTilesList;
        }


        private const string m_sPath = "Images/Pavement textures pack/";
        //public List<Sprite> LoadAllTilesPreview()
        //{
        
        //    return;
        //}
    }
}

