/**
 * @file TileView.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;

namespace DefenseFramework
{
    public class TileView : MonoBehaviour
    {
        private string m_sPrefabName = null;
        private string m_sObstacleName = null;
        private GameObject m_gTileLabel;
        private bool m_bIsVisibleLabel = false;
        private TileModel m_cModel = null;

        public string PrefabName
        {
            get
            {
                return m_sPrefabName;
            }

            set
            {
                m_sPrefabName = value;
            }
        }

        public string ObstacleName
        {
            get
            {
                return m_sObstacleName;
            }

            set
            {
                m_sObstacleName = value;
            }
        }

        // Use this for initialization
        void Awake()
        {
            m_gTileLabel = (GameObject) Instantiate( Resources.Load( "Prefabs/TileWidget" ) as GameObject );
            m_gTileLabel.transform.parent = this.transform;
            m_gTileLabel.SetActive( false );
        }

        // Update is called once per frame
        void Update()
        {
            if ( m_bIsVisibleLabel == true )
            {
                int iType = (int) m_cModel.EType;
                m_gTileLabel.transform.GetChild( 0 ).GetComponent<UILabel>().text = iType.ToString();
            }
        }

        public void SetTileLabelAnchor()
        {
            m_gTileLabel.GetComponent<UIWidget>().SetAnchor( this.transform );

            m_gTileLabel.GetComponent<UIWidget>().topAnchor.SetHorizontal( this.transform, 20 );
        }

        public void SetTileLabel( string s )
        {
            m_gTileLabel.transform.GetChild( 0 ).GetComponent<UILabel>().text = s;
        }

        public void SetVisibleLabel( bool visible )
        {
            m_bIsVisibleLabel = visible;
            if ( m_bIsVisibleLabel == true )
            {
                m_gTileLabel.SetActive( true );
            }
            else if ( m_bIsVisibleLabel == false )
            {
                m_gTileLabel.SetActive( false );
            }
        }
    }
}