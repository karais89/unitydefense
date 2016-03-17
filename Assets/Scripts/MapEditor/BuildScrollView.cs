using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Common;

public class BuildScrollView : MonoBehaviour {

    private List<BuildScrollItem> m_itemsList = new List<BuildScrollItem>();
    private List<GameObject> m_gItemList = new List<GameObject>();
    private Transform m_tContent = null;
    private ScrollRect m_scrollRect = null;

    private void Awake()
    {
        m_tContent = transform.FindChild( "Viewport" ).transform.FindChild( "Content" );
        if(m_tContent ==null)
        {
            Debug.LogError( "m_tContent == null" );
        }
        m_scrollRect = transform.GetComponent<ScrollRect>();


        ResourceManager.Instance.LoadAllTilesPreview();

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

        const string sPrefabName = "Item - MapBuild";
        GameObject itemPrefab = ResourceManager.Instance.LoadPrefab( sPrefabName ) as GameObject;

        for (int i = 0; i < (int)ResourceManager.ePrefabTile.Max; i++ )
        {
            GameObject newItem = GameObjectFactory.Instantite( itemPrefab );

            ResourceManager.ePrefabTile tile = (ResourceManager.ePrefabTile)i;
            string sName = sPrefabName + "_" + tile.ToString();
            newItem.name = sName;
            newItem.transform.parent = m_tContent;

            m_gItemList.Add( newItem );
        }
        
        //m_scrollRect.horizontalNormalizedPosition
    }
}
