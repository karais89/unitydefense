/**
 * @file Tile.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-10-29
 */

using UnityEngine;

namespace DefenseFramework
{
    public class Tile : MonoBehaviour
    {
        public enum TileType { walkable = 0, obstacle = 1, spawn = 11, hero = 111 };

        public TileType type = TileType.walkable;
        public int indexX = 0;
        public int indexY = 0;
        public string prefabName = null;
        public bool hasObstacle = false;
        public string obstacleName = null;
        private GameObject tileLabel;
        private bool isVisibleLabel = false;

        // Use this for initialization
        private void Awake()
        {
            tileLabel = (GameObject) Instantiate( Resources.Load( "Prefabs/TileWidget" ) as GameObject );
            tileLabel.transform.parent = this.transform;
            tileLabel.SetActive( false );
        }

        // Update is called once per frame
        private void Update()
        {
            if ( isVisibleLabel == true )
            {
                int iType = (int) type;
                tileLabel.transform.GetChild( 0 ).GetComponent<UILabel>().text = iType.ToString();
            }
        }

        public void SetTileLabelAnchor()
        {
            tileLabel.GetComponent<UIWidget>().SetAnchor( this.transform );

            tileLabel.GetComponent<UIWidget>().topAnchor.SetHorizontal( this.transform, 20 );
        }

        public void SetTileLabel( string s )
        {
            tileLabel.transform.GetChild( 0 ).GetComponent<UILabel>().text = s;
        }

        public void SetVisibleLabel( bool visible )
        {
            isVisibleLabel = visible;
            if ( isVisibleLabel == true )
            {
                tileLabel.SetActive( true );
            }
            else if ( isVisibleLabel == false )
            {
                tileLabel.SetActive( false );
            }
        }
    }
}


