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

        public enum eTilePreview
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

        public enum ePrefabRock
        {
            None = -1,
            Rock_Cone,
            Rock_Heavy,
            Rock_Large,
            Rock_Medium,
            Rock_Small_01,
            Rock_Small_02,
            Max
        }
        
        public enum eRockPreview
        {
            None = -1,
            rock_cone,
            rock_heavy,
            rock_large,
            rock_medium,
            rock_small_1,
            rock_small_2,
            Max
        }
        
        public enum ePrefabTree
        {
            None = -1,
            Tree_Leafy_01,
            Tree_Leafy_02,
            Tree_Leafy_03,
            Tree_Leafy_04,
            Tree_Leafy_05,
            Tree_Short_01,
            Tree_Short_02,
            Tree_Short_03,
            Tree_Tall_01,
            Tree_Tall_02,
            Tree_Tall_03,
            Tree_Tall_04,
            Max
        }
        
        public enum eTreePreview
        {
            None = -1,
            tree_leafy_1,
            tree_leafy_2,
            tree_leafy_3,
            tree_leafy_4,
            tree_leafy_5,
            tree_short_1,
            tree_short_2,
            tree_short_3,
            tree_tall_1,
            tree_tall_2,
            tree_tall_3,
            tree_tall_4,
            Max
        }

        public enum eAtlasName
        {
            None = -1,
            Tile,
            Tree,
            Max
        }
        
        private Dictionary<ePrefabTile, GameObject> m_tileObjectDictionary = new Dictionary<ePrefabTile, GameObject>();
        private Dictionary<eAtlasName, UIAtlas> m_atlasDictionary = new Dictionary<eAtlasName, UIAtlas>();
        
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

        public UIAtlas LoadAtlas( string name )
        {
            UIAtlas atlas = null;

            atlas = Resources.Load<UIAtlas>( "Atlas/" + name );
            if ( atlas == null )
            {
                Debug.LogError( "atlas null, name = " + name );
            }
            return atlas;
        }

        public Dictionary<eAtlasName, UIAtlas> LoadAllAtlas()
        {
            for (int i = 0; i < (int)eAtlasName.Max; i++ )
            {
                eAtlasName name = (eAtlasName) i;
                UIAtlas newAtlas = LoadAtlas(name.ToString());

                m_atlasDictionary.Add( name, newAtlas );
            }
            return m_atlasDictionary;
        }

        public UIAtlas GetAtlas( eAtlasName name )
        {
            return m_atlasDictionary[name];
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
        
        public Dictionary<ePrefabTile, GameObject> LoadAllTiles()
        {
            if ( m_tileObjectDictionary.Count > 0 )
            {
                Debug.Log( "all tiles are already loaded" );
                return m_tileObjectDictionary;
            }

            for ( int i = 0; i < (int) ePrefabTile.Max; i++ )
            {
                ePrefabTile tile = (ePrefabTile) i;
                string prefabName = tile.ToString();
                GameObject newObject = LoadPrefab( prefabName );
                m_tileObjectDictionary.Add( tile, newObject );
            }

            return m_tileObjectDictionary;
        }

        public GameObject GetTile( ePrefabTile eTile )
        {
            if ( m_tileObjectDictionary.Count <= 0 )
            {
                Debug.Log( "Tiles are not loaded yet, so about to load all tiles" );
                LoadAllTiles();
            }

            GameObject tileObj = null;
            if ( m_tileObjectDictionary.TryGetValue(eTile, out tileObj ) == true )
            {
                return tileObj;
            }
            else
            {
                Debug.LogError( "eTile = " + eTile + " does not exist" );
                return null;
            }
        }
        
    }
}

