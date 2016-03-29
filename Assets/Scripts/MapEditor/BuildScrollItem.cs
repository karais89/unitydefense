using UnityEngine;
using System.Collections;
using Common;

namespace MapEditor
{
    public class BuildScrollItem : MonoBehaviour
    {
        private UIButton m_button;
        private UISprite m_sprite;
        private BuildScrollModel.eBuildType m_eBuildType;
        private MapEditorView m_cMapEditorView;

        private ResourceManager.eTilePreview m_eTilePreview = ResourceManager.eTilePreview.None;
        private ResourceManager.eRockPreview m_eRockPreview = ResourceManager.eRockPreview.None;
        private ResourceManager.eTreePreview m_eTreePreview = ResourceManager.eTreePreview.None;

        public BuildScrollModel.eBuildType EBuildType
        {
            get
            {
                return m_eBuildType;
            }

            set
            {
                m_eBuildType = value;
            }
        }

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

        private void Awake()
        {
            m_cMapEditorView = GameObject.Find( "MapEditor" ).GetComponent<MapEditorView>();

            m_button = GetComponent<UIButton>();
            m_button.onClick.Add( new EventDelegate( OnClickItem ) );
            m_button.onClick.Add( new EventDelegate( m_cMapEditorView.OnClickBuilItem ) );

            m_sprite = GetComponent<UISprite>();
        }

        private void OnClickItem()
        {
            //m_cMapEditorView
        }

        public void SetSprite()
        {
            switch ( m_eBuildType )
            {
                case BuildScrollModel.eBuildType.None:
                    break;

                case BuildScrollModel.eBuildType.Tile:
                    m_sprite.atlas = ResourceManager.Instance.GetAtlas( ResourceManager.eAtlasName.Tile );
                    m_sprite.spriteName = m_eTilePreview.ToString();
                    m_button.normalSprite = m_eTilePreview.ToString();
                    break;

                case BuildScrollModel.eBuildType.Rock:
                    m_sprite.atlas = ResourceManager.Instance.GetAtlas( ResourceManager.eAtlasName.Tree );
                    m_sprite.spriteName = m_eRockPreview.ToString();
                    m_button.normalSprite = m_eRockPreview.ToString();
                    break;

                case BuildScrollModel.eBuildType.Tree:
                    m_sprite.atlas = ResourceManager.Instance.GetAtlas( ResourceManager.eAtlasName.Tree );
                    m_sprite.spriteName = m_eTreePreview.ToString();
                    m_button.normalSprite = m_eTreePreview.ToString();
                    break;

                case BuildScrollModel.eBuildType.Max:
                    break;
                default:
                    break;
            }


        }

    }

}
