using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;

public class BuildScrollView : MonoBehaviour {

    private List<BuildScrollItem> m_itemsList = new List<BuildScrollItem>();
    private List<GameObject> m_gItemList = new List<GameObject>();
    private UIScrollView m_scrollView = null;
    private Transform m_tGrid = null;

    private void Awake()
    {
        m_tGrid = transform.FindChild( "Grid" );
        m_scrollView = GetComponent<UIScrollView>();
        
        InstantiateTileItems();
    }

    private void InstantiateTileItems()
    {
        if (m_gItemList.Count > 0)
        {
            for (int i = 0; i < m_gItemList.Count; i++ )
            {
                Destroy( m_gItemList[ i ] );
            }
            m_gItemList.Clear();
        }

        const string sPrefabName = "MapEditor/Item - MapBuild";
        GameObject itemPrefab = ResourceManager.Instance.LoadPrefab( sPrefabName );
        
        for (int i = 0; i < (int)ResourceManager.ePrefabTile.Max; i++ )
        {
            GameObject newItem = GameObjectFactory.Instantiate( itemPrefab );

            ResourceManager.ePrefabTile tile = (ResourceManager.ePrefabTile)i;
            string sName = sPrefabName + "_" + tile.ToString();
            newItem.name = sName;
            newItem.transform.SetParentEx( m_tGrid );

            newItem.GetComponent<BuildScrollItem>().ETileIndex = tile;
            newItem.GetComponent<BuildScrollItem>().SetSprite();

            m_gItemList.Add( newItem );
        }
        
        
    }
}
