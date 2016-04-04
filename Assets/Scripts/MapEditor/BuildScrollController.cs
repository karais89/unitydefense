using UnityEngine;
using System.Collections;

namespace MapEditor
{
    public class BuildScrollController : MonoBehaviour
    {
        //private BuildScrollModel m_cModel = null;
        private MapEditorModel m_cModel = null;
        private BuildScrollView m_cView = null;

        private UIButton m_buttonTile = null;
        private UIButton m_buttonRock = null;
        private UIButton m_buttonTree = null;

        private void Awake()
        {
            m_cModel = GameObject.Find( "MapEditor" ).GetComponent<MapEditorModel>();
            
            m_cView = GetComponent<BuildScrollView>();

            m_buttonTile = transform.parent.FindChild( "Button - Tile" ).GetComponent<UIButton>();
            m_buttonTile.onClick.Add( new EventDelegate( OnClickTile ) );

            m_buttonRock = transform.parent.FindChild( "Button - Rock" ).GetComponent<UIButton>();
            m_buttonRock.onClick.Add( new EventDelegate( OnClickRock ) );

            m_buttonTree = transform.parent.FindChild( "Button - Tree" ).GetComponent<UIButton>();
            m_buttonTree.onClick.Add( new EventDelegate( OnClickTree ) );
        }

        private void OnClickTile()
        {
            m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

            m_cView.InstantiateItems( m_cModel.EMode );
        }

        private void OnClickRock()
        {
            m_cModel.EMode = MapEditorModel.eBuildMode.Rock;

            m_cView.InstantiateItems( m_cModel.EMode );
        }

        private void OnClickTree()
        {
            m_cModel.EMode = MapEditorModel.eBuildMode.Tree;

            m_cView.InstantiateItems( m_cModel.EMode );
        }

    }
}

