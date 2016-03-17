using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Common;

public class BuildScrollItem : MonoBehaviour
{
    private Button m_button;
    private Image m_image;
    private ResourceManager.ePrefabTile m_tileIndex;

    public ResourceManager.ePrefabTile TileIndex
    {
        get
        {
            return m_tileIndex;
        }

        set
        {
            m_tileIndex = value;
        }
    }

    private void Awake()
    {
        m_button = GetComponent<Button>();
        m_image = GetComponent<Image>();

        SetImage();
    }

    private void SetImage()
    {
        m_image.sprite = ResourceManager.Instance.GetTilePreviewSprite( m_tileIndex );
        m_image.material = ResourceManager.Instance.GetTilePreviewMaterial( m_tileIndex );
    }
}
