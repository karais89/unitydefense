using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

namespace MapEditor
{
    public class BuildScrollView : MonoBehaviour
    {
        private List<BuildScrollItem> m_itemsList = new List<BuildScrollItem>();
        private List<GameObject> m_gItemList = new List<GameObject>();
        private UIScrollView m_scrollView = null;
        private Transform m_tGrid = null;
        private UIGrid m_grid = null;

        private void Awake()
        {
            m_tGrid = transform.FindChild( "Grid" );
            m_grid = m_tGrid.GetComponent<UIGrid>();
            m_scrollView = GetComponent<UIScrollView>();

            ResourceManager.Instance.LoadAllAtlas();

            InstantiateItems( MapEditorModel.eBuildMode.Tile );
        }

        public void InstantiateItems( MapEditorModel.eBuildMode type )
        {
            if ( m_gItemList.Count > 0 )
            {
                for ( int i = 0; i < m_gItemList.Count; i++ )
                {
                    Destroy( m_gItemList[ i ] );
                }
                m_gItemList.Clear();
            }

            const string sPrefabName = "MapEditor/Item - MapBuild";
            GameObject itemPrefab = ResourceManager.Instance.LoadPrefab( sPrefabName );

            switch ( type )
            {
                case MapEditorModel.eBuildMode.None:
                    break;
                case MapEditorModel.eBuildMode.Tile:
                    {
                        for ( int i = 0; i < (int) ResourceManager.ePrefabTile.Max; i++ )
                        {
                            GameObject newItem = GameObjectFactory.Instantiate( itemPrefab );

                            ResourceManager.ePrefabTile tile = (ResourceManager.ePrefabTile) i;
                            string sName = sPrefabName + "_" + tile.ToString();
                            newItem.name = sName;
                            newItem.transform.SetParentEx( m_tGrid );

                            newItem.GetComponent<BuildScrollItem>().EBuildMode = type;
                            newItem.GetComponent<BuildScrollItem>().ETilePreview = (ResourceManager.eTilePreview) tile;
                            newItem.GetComponent<BuildScrollItem>().SetSprite();

                            m_gItemList.Add( newItem );
                        }
                    }
                    break;
                case MapEditorModel.eBuildMode.Tree:
                    {
                        for ( int i = 0; i < (int) ResourceManager.ePrefabTree.Max; i++ )
                        {
                            GameObject newItem = GameObjectFactory.Instantiate( itemPrefab );

                            ResourceManager.ePrefabTree tree = (ResourceManager.ePrefabTree) i;
                            string sName = sPrefabName + "_" + tree.ToString();
                            newItem.name = sName;
                            newItem.transform.SetParentEx( m_tGrid );

                            newItem.GetComponent<BuildScrollItem>().EBuildMode = type;
                            newItem.GetComponent<BuildScrollItem>().ETreePreview = (ResourceManager.eTreePreview) tree;
                            newItem.GetComponent<BuildScrollItem>().SetSprite();

                            m_gItemList.Add( newItem );
                        }
                    }
                    break;
                case MapEditorModel.eBuildMode.Rock:
                    {
                        for ( int i = 0; i < (int) ResourceManager.ePrefabRock.Max; i++ )
                        {
                            GameObject newItem = GameObjectFactory.Instantiate( itemPrefab );

                            ResourceManager.ePrefabRock rock = (ResourceManager.ePrefabRock) i;
                            string sName = sPrefabName + "_" + rock.ToString();
                            newItem.name = sName;
                            newItem.transform.SetParentEx( m_tGrid );

                            newItem.GetComponent<BuildScrollItem>().EBuildMode = type;
                            newItem.GetComponent<BuildScrollItem>().ERockPreview = (ResourceManager.eRockPreview) rock;
                            newItem.GetComponent<BuildScrollItem>().SetSprite();

                            m_gItemList.Add( newItem );
                        }
                    }
                    break;
                case MapEditorModel.eBuildMode.Erase:
                    break;
                case MapEditorModel.eBuildMode.Spawn:
                    break;
                case MapEditorModel.eBuildMode.WayPoint:
                    break;
                default:
                    break;
            }
            
            m_grid.repositionNow = true;
        }
    }
}

