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

        private List<GameObject> m_gTilesList = new List<GameObject>();
        private const string m_sPreviewPath = "Images/PavementTextures/";
        private const string m_sPreviewName = "preview";
        private const string m_sPreviewMaterialName = "MaterialPreview_";
        private List<Sprite> m_sPreviewList = new List<Sprite>();
        private List<Material> m_mPreviewList = new List<Material>();

        public List<GameObject> GTilesList
        {
            get
            {
                return m_gTilesList;
            }
        }
        
        public List<Sprite> SPreviewList
        {
            get
            {
                return m_sPreviewList;
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
        
        public List<Sprite> LoadAllTilesPreviewSprite()
        {
            if (m_sPreviewList.Count > 0)
            {
                Debug.LogWarning( "m_sPreviewList.Count = " + m_sPreviewList.Count );
                m_sPreviewList.Clear();
            }

            for (int i = 0; i < (int)ePrefabTile.Max; i++ )
            {
                ePrefabTile eTile = (ePrefabTile) i;
                string sTileName = eTile.ToString();
                string fullPathName = m_sPreviewPath + sTileName + "/" + m_sPreviewName;
                Sprite newSprite = Resources.Load<Sprite>( fullPathName );
                if (newSprite == null)
                {
                    Debug.LogError( "newSprite == null, fullPathName = " + fullPathName );
                    continue;
                }
                m_sPreviewList.Add( newSprite );
            }

            return m_sPreviewList;
        }

        public Sprite GetTilePreviewSprite(ePrefabTile tile)
        {
            int index = (int) tile;
            if (m_sPreviewList.Count <= 0)
            {
                Debug.LogError( "m_sPreviewList.Count = " + m_sPreviewList.Count );
                return null;
            }
            return m_sPreviewList[ index ];
        }

        public List<Material> LoadAllTilePreviewMaterails()
        {
            if ( m_mPreviewList.Count > 0 )
            {
                Debug.LogWarning( "m_mPreviewList.Count = " + m_mPreviewList.Count );
                m_mPreviewList.Clear();
            }

            for ( int i = 0; i < (int) ePrefabTile.Max; i++ )
            {
                ePrefabTile eTile = (ePrefabTile) i;
                string sTileName = eTile.ToString();
                int index = i + 1;
                string fullPathName = m_sPreviewPath + sTileName + "/" + m_sPreviewMaterialName + index.ToString("D2");
                Material newMaterial = Resources.Load<Material>( fullPathName );
                if ( newMaterial == null )
                {
                    Debug.LogError( "newMaterial == null, fullPathName = " + fullPathName );
                    continue;
                }
                m_mPreviewList.Add( newMaterial );
            }

            return m_mPreviewList;
        }

        public Material GetTilePreviewMaterial(ePrefabTile tile)
        {
            int index = (int) tile;
            if ( m_mPreviewList.Count <= 0 )
            {
                Debug.LogError( "m_mPreviewList.Count = " + m_mPreviewList.Count );
                return null;
            }
            return m_mPreviewList[ index ];
        }
    }
}

