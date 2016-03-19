using UnityEngine;
using System.Collections;
using Common;

public class BuildScrollItem : MonoBehaviour
{
    private UIButton m_button;
    private UISprite m_sprite;
    private ResourceManager.ePrefabTile m_eTileIndex;
    private ResourceManager.ePrefabTilePreview m_eTilePreviewIndex;

    public ResourceManager.ePrefabTile ETileIndex
    {
        get
        {
            return m_eTileIndex;
        }

        set
        {
            m_eTileIndex = value;
            ResourceManager.ePrefabTilePreview ePreview = (ResourceManager.ePrefabTilePreview) m_eTileIndex;
            m_eTilePreviewIndex = ePreview;
        }
    }

    public ResourceManager.ePrefabTilePreview ETilePreviewIndex
    {
        get
        {
            return m_eTilePreviewIndex;
        }

        set
        {
            m_eTilePreviewIndex = value;
            m_eTileIndex = (ResourceManager.ePrefabTile) m_eTilePreviewIndex;
        }
    }

    private void Awake()
    {
        m_button = GetComponent<UIButton>();
        m_button.onClick.Add( new EventDelegate( OnClickItem ) );
        m_sprite = GetComponent<UISprite>();
    }

    private void OnClickItem()
    {
        Debug.Log( "OnClickItem() called! " + m_eTileIndex.ToString() );
    }

    public void SetSprite()
    {
        m_sprite.spriteName = m_eTilePreviewIndex.ToString();
        m_button.normalSprite = m_eTilePreviewIndex.ToString();
    }
    
}
