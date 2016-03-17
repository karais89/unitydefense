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


        ResourceManager.Instance.LoadAllTilesPreviewSprite();
        ResourceManager.Instance.LoadAllTilePreviewMaterails();

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

        float initialX = -355f;
        for (int i = 0; i < (int)ResourceManager.ePrefabTile.Max; i++ )
        {
            GameObject newItem = GameObjectFactory.InstantiateUI( itemPrefab );

            ResourceManager.ePrefabTile tile = (ResourceManager.ePrefabTile)i;
            string sName = sPrefabName + "_" + tile.ToString();
            newItem.name = sName;
            newItem.transform.SetParentEx( m_tContent );

            float width = newItem.GetComponent<RectTransform>().rect.width;
            Vector3 newPos = Vector3.zero;
            float x = initialX + (float)(i * width);
            newPos.x = x;
            newPos.y = 0;
            newItem.GetComponent<RectTransform>().localPosition = newPos;
            
            m_gItemList.Add( newItem );
        }

        //for (int i = 0; i < m_gItemList.Count; i++ )
        //{
        //    Vector3 newPos = m_gItemList[ i ].GetComponent<RectTransform>().localPosition;
        //    newPos.y = 0f;
        //    m_gItemList[ i ].GetComponent<RectTransform>().localPosition = newPos;
        //}
    }
}
