using UnityEngine;
using System.Collections;
using Common;

namespace MapEditor
{
    public class BuildScrollItem : MonoBehaviour
    {
        private UIButton m_button;
        private UISprite m_sprite;
        private MapEditorModel.eBuildMode m_eBuildMode;
        private MapEditorController m_cMapEditorController;

        private ResourceManager.eTilePreview m_eTilePreview = ResourceManager.eTilePreview.None;
        private ResourceManager.eRockPreview m_eRockPreview = ResourceManager.eRockPreview.None;
        private ResourceManager.eTreePreview m_eTreePreview = ResourceManager.eTreePreview.None;
        
        public ResourceManager.eTilePreview ETilePreview
        {
            get
            {
                return m_eTilePreview;
            }

            set
            {
                m_eTilePreview = value;
            }
        }

        public ResourceManager.eRockPreview ERockPreview
        {
            get
            {
                return m_eRockPreview;
            }

            set
            {
                m_eRockPreview = value;
            }
        }

        public ResourceManager.eTreePreview ETreePreview
        {
            get
            {
                return m_eTreePreview;
            }

            set
            {
                m_eTreePreview = value;
            }
        }

        public MapEditorModel.eBuildMode EBuildMode
        {
            get
            {
                return m_eBuildMode;
            }

            set
            {
                m_eBuildMode = value;
            }
        }

        private void Awake()
        {
            m_cMapEditorController = GameObject.Find( "MapEditor" ).GetComponent<MapEditorController>();

            m_button = GetComponent<UIButton>();
            m_button.onClick.Add( new EventDelegate( OnClickItem ) );
            m_button.onClick.Add( new EventDelegate( m_cMapEditorController.OnClickBuildItem ) );

            m_sprite = GetComponent<UISprite>();
        }

        private void OnClickItem()
        {
            ResourceManager.ePrefabTile eTile = (ResourceManager.ePrefabTile) m_eTilePreview;

            m_cMapEditorController.SetSelectedItem( eTile );
        }

        public void SetSprite()
        {
            switch ( m_eBuildMode )
            {
                case MapEditorModel.eBuildMode.None:
                    break;
                case MapEditorModel.eBuildMode.Tile:
                    m_sprite.atlas = ResourceManager.Instance.GetAtlas( ResourceManager.eAtlasName.Tile );
                    m_sprite.spriteName = m_eTilePreview.ToString();
                    m_button.normalSprite = m_eTilePreview.ToString();
                    break;
                case MapEditorModel.eBuildMode.Tree:
                    m_sprite.atlas = ResourceManager.Instance.GetAtlas( ResourceManager.eAtlasName.Tree );
                    m_sprite.spriteName = m_eTreePreview.ToString();
                    m_button.normalSprite = m_eTreePreview.ToString();
                    break;
                case MapEditorModel.eBuildMode.Rock:
                    m_sprite.atlas = ResourceManager.Instance.GetAtlas( ResourceManager.eAtlasName.Tree );
                    m_sprite.spriteName = m_eRockPreview.ToString();
                    m_button.normalSprite = m_eRockPreview.ToString();
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
            
        }

    }

}
