using UnityEngine;
using System.Collections;

namespace MapEditor
{
    public class BuildScrollController : MonoBehaviour
    {
        private BuildScrollModel m_cModel = null;
        private BuildScrollView m_cView = null;

        private UIButton m_buttonTile = null;
        private UIButton m_buttonRock = null;
        private UIButton m_buttonTree = null;

        private void Awake()
        {
            m_cModel = GetComponent<BuildScrollModel>();
            m_cModel.EBuildType = BuildScrollModel.eBuildType.Tile;

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
            m_cModel.EBuildType = BuildScrollModel.eBuildType.Tile;

            m_cView.InstantiateItems( m_cModel.EBuildType );
        }

        private void OnClickRock()
        {
            m_cModel.EBuildType = BuildScrollModel.eBuildType.Rock;

            m_cView.InstantiateItems( m_cModel.EBuildType );
        }

        private void OnClickTree()
        {
            m_cModel.EBuildType = BuildScrollModel.eBuildType.Tree;

            m_cView.InstantiateItems( m_cModel.EBuildType );
        }

    }
}

