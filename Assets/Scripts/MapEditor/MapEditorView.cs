/**
 * @file MapEditorView.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-15
 */

using UnityEngine;
using System.Collections;
using DefenseFramework;

namespace MapEditor
{
    /**
     * @class   MapEditorView
     *
     * @brief   A map editor view.
     *
     * @author  Ddayin
     * @date    2016-03-17
     */

    public class MapEditorView : MonoBehaviour
    {
        /** @brief   The model. */
        private MapEditorModel m_cModel;

        /** @brief   The default tile. */
        public GameObject m_gDefaultTile;
        /** @brief   Array of rock buttons. */
        public Texture[] m_tRockButtonArray = new Texture[ 6 ];
        /** @brief   Array of tree buttons. */
        public Texture[] m_tTreeButtonArray = new Texture[ 12 ];
        /** @brief   Array of tile buttons. */
        public Texture[] m_tTileButtonArray = new Texture[ 20 ];

        /** @brief   The selected tree. */
        private GameObject m_gSelectedTree = null;
        /** @brief   The selected rock. */
        private GameObject m_gSelectedRock = null;
        /** @brief   The selected tile. */
        private GameObject m_gSelectedTile = null;

        /** @brief   The selected tree name. */
        private string m_sSelectedTreeName = null;
        /** @brief   The selected rock name. */
        private string m_sSelectedRockName = null;

        /**
         * @property    public GameObject GSelectedTree
         *
         * @brief   Gets or sets the selected tree.
         *
         * @return  The g selected tree.
         */

        public GameObject GSelectedTree
        {
            get
            {
                return m_gSelectedTree;
            }

            set
            {
                m_gSelectedTree = value;
            }
        }

        /**
         * @property    public GameObject GSelectedRock
         *
         * @brief   Gets or sets the selected rock.
         *
         * @return  The g selected rock.
         */

        public GameObject GSelectedRock
        {
            get
            {
                return m_gSelectedRock;
            }

            set
            {
                m_gSelectedRock = value;
            }
        }

        /**
         * @property    public GameObject GSelectedTile
         *
         * @brief   Gets or sets the selected tile.
         *
         * @return  The g selected tile.
         */

        public GameObject GSelectedTile
        {
            get
            {
                return m_gSelectedTile;
            }

            set
            {
                m_gSelectedTile = value;
            }
        }

        /**
         * @property    public string SSelectedTreeName
         *
         * @brief   Gets or sets the selected tree name.
         *
         * @return  The name of the selected tree.
         */

        public string SSelectedTreeName
        {
            get
            {
                return m_sSelectedTreeName;
            }

            set
            {
                m_sSelectedTreeName = value;
            }
        }

        /**
         * @property    public string SSelectedRockName
         *
         * @brief   Gets or sets the selected rock name.
         *
         * @return  The name of the selected rock.
         */

        public string SSelectedRockName
        {
            get
            {
                return m_sSelectedRockName;
            }

            set
            {
                m_sSelectedRockName = value;
            }
        }

        // Use this for initialization

        /**
         * @fn  private void Awake()
         *
         * @brief   Awakes this object.
         *
         * @author  Ddayin
         * @date    2016-03-17
         */

        private void Awake()
        {
            Debug.Log( "MapEditorView" );
            m_cModel = GetComponent<MapEditorModel>();

            Init();
        }

        /**
         * @fn  private void Init()
         *
         * @brief   S this object.
         *
         * @author  Ddayin
         * @date    2016-03-17
         */

        private void Init()
        {
            

        }

        /**
         * @fn  private void OnGUI()
         *
         * @brief   Executes the graphical user interface action.
         *
         * @author  Ddayin
         * @date    2016-03-17
         */

        private void OnGUI()
        {
            // 타일 버튼
            if ( GUI.Button( new Rect( 10, 10, 60, 60 ), m_tTileButtonArray[ 0 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 0 ];
            }
            if ( GUI.Button( new Rect( 70, 10, 60, 60 ), m_tTileButtonArray[ 1 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 1 ];
            }
            if ( GUI.Button( new Rect( 130, 10, 60, 60 ), m_tTileButtonArray[ 2 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 2 ];
            }
            if ( GUI.Button( new Rect( 190, 10, 60, 60 ), m_tTileButtonArray[ 3 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 3 ];
            }
            if ( GUI.Button( new Rect( 250, 10, 60, 60 ), m_tTileButtonArray[ 4 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 4 ];
            }
            if ( GUI.Button( new Rect( 310, 10, 60, 60 ), m_tTileButtonArray[ 5 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 5 ];
            }
            if ( GUI.Button( new Rect( 370, 10, 60, 60 ), m_tTileButtonArray[ 6 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 6 ];
            }
            if ( GUI.Button( new Rect( 430, 10, 60, 60 ), m_tTileButtonArray[ 7 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 7 ];
            }
            if ( GUI.Button( new Rect( 490, 10, 60, 60 ), m_tTileButtonArray[ 8 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 8 ];
            }
            if ( GUI.Button( new Rect( 550, 10, 60, 60 ), m_tTileButtonArray[ 9 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 9 ];
            }
            if ( GUI.Button( new Rect( 610, 10, 60, 60 ), m_tTileButtonArray[ 10 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 10 ];
            }
            if ( GUI.Button( new Rect( 670, 10, 60, 60 ), m_tTileButtonArray[ 11 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 11 ];
            }
            if ( GUI.Button( new Rect( 730, 10, 60, 60 ), m_tTileButtonArray[ 12 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 12 ];
            }
            if ( GUI.Button( new Rect( 790, 10, 60, 60 ), m_tTileButtonArray[ 13 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 13 ];
            }
            if ( GUI.Button( new Rect( 850, 10, 60, 60 ), m_tTileButtonArray[ 14 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 14 ];
            }
            if ( GUI.Button( new Rect( 910, 10, 60, 60 ), m_tTileButtonArray[ 15 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 15 ];
            }
            if ( GUI.Button( new Rect( 970, 10, 60, 60 ), m_tTileButtonArray[ 16 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 16 ];
            }
            if ( GUI.Button( new Rect( 1030, 10, 60, 60 ), m_tTileButtonArray[ 17 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 17 ];
            }
            if ( GUI.Button( new Rect( 1090, 10, 60, 60 ), m_tTileButtonArray[ 18 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 18 ];
            }
            if ( GUI.Button( new Rect( 1150, 10, 60, 60 ), m_tTileButtonArray[ 19 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tile;

                m_gSelectedTile = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 19 ];
            }

            // 나무 버튼
            if ( GUI.Button( new Rect( 10, 70, 60, 60 ), m_tTreeButtonArray[ 0 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tree;

                m_gSelectedTree = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTreePrefabArray[ 0 ];
                m_sSelectedTreeName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().STreePrefabNameArray[ 0 ];
            }
            if ( GUI.Button( new Rect( 70, 70, 60, 60 ), m_tTreeButtonArray[ 1 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tree;

                m_gSelectedTree = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTreePrefabArray[ 1 ];
                m_sSelectedTreeName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().STreePrefabNameArray[ 1 ];
            }
            if ( GUI.Button( new Rect( 130, 70, 60, 60 ), m_tTreeButtonArray[ 2 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tree;

                m_gSelectedTree = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTreePrefabArray[ 2 ];
                m_sSelectedTreeName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().STreePrefabNameArray[ 2 ];
            }
            if ( GUI.Button( new Rect( 190, 70, 60, 60 ), m_tTreeButtonArray[ 3 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tree;

                m_gSelectedTree = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTreePrefabArray[ 3 ];
                m_sSelectedTreeName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().STreePrefabNameArray[ 3 ];
            }
            if ( GUI.Button( new Rect( 250, 70, 60, 60 ), m_tTreeButtonArray[ 4 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tree;

                m_gSelectedTree = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTreePrefabArray[ 4 ];
                m_sSelectedTreeName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().STreePrefabNameArray[ 4 ];
            }
            if ( GUI.Button( new Rect( 310, 70, 60, 60 ), m_tTreeButtonArray[ 5 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tree;
                m_gSelectedTree = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTreePrefabArray[ 5 ];
                m_sSelectedTreeName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().STreePrefabNameArray[ 5 ];
            }
            if ( GUI.Button( new Rect( 370, 70, 60, 60 ), m_tTreeButtonArray[ 6 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tree;

                m_gSelectedTree = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTreePrefabArray[ 6 ];
                m_sSelectedTreeName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().STreePrefabNameArray[ 6 ];
            }
            if ( GUI.Button( new Rect( 430, 70, 60, 60 ), m_tTreeButtonArray[ 7 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tree;

                m_gSelectedTree = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTreePrefabArray[ 7 ];
                m_sSelectedTreeName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().STreePrefabNameArray[ 7 ];
            }
            if ( GUI.Button( new Rect( 490, 70, 60, 60 ), m_tTreeButtonArray[ 8 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tree;

                m_gSelectedTree = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTreePrefabArray[ 8 ];
                m_sSelectedTreeName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().STreePrefabNameArray[ 8 ];
            }
            if ( GUI.Button( new Rect( 550, 70, 60, 60 ), m_tTreeButtonArray[ 9 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tree;

                m_gSelectedTree = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTreePrefabArray[ 9 ];
                m_sSelectedTreeName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().STreePrefabNameArray[ 9 ];
            }
            if ( GUI.Button( new Rect( 610, 70, 60, 60 ), m_tTreeButtonArray[ 10 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tree;

                m_gSelectedTree = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTreePrefabArray[ 10 ];
                m_sSelectedTreeName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().STreePrefabNameArray[ 10 ];
            }
            if ( GUI.Button( new Rect( 670, 70, 60, 60 ), m_tTreeButtonArray[ 11 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Tree;

                m_gSelectedTree = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTreePrefabArray[ 11 ];
                m_sSelectedTreeName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().STreePrefabNameArray[ 11 ];
            }

            // 바위 버튼
            if ( GUI.Button( new Rect( 10, 130, 60, 60 ), m_tRockButtonArray[ 0 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Rock;

                m_gSelectedRock = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GRockPrefabArray[ 0 ];
                m_sSelectedRockName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().SRockPrefabNameArray[ 0 ];
            }
            if ( GUI.Button( new Rect( 70, 130, 60, 60 ), m_tRockButtonArray[ 1 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Rock;

                m_gSelectedRock = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GRockPrefabArray[ 1 ];
                m_sSelectedRockName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().SRockPrefabNameArray[ 1 ];
            }
            if ( GUI.Button( new Rect( 130, 130, 60, 60 ), m_tRockButtonArray[ 2 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Rock;

                m_gSelectedRock = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GRockPrefabArray[ 2 ];
                m_sSelectedRockName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().SRockPrefabNameArray[ 2 ];
            }
            if ( GUI.Button( new Rect( 190, 130, 60, 60 ), m_tRockButtonArray[ 3 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Rock;

                m_gSelectedRock = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GRockPrefabArray[ 3 ];
                m_sSelectedRockName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().SRockPrefabNameArray[ 3 ];
            }
            if ( GUI.Button( new Rect( 250, 130, 60, 60 ), m_tRockButtonArray[ 4 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Rock;

                m_gSelectedRock = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GRockPrefabArray[ 4 ];
                m_sSelectedRockName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().SRockPrefabNameArray[ 4 ];
            }
            if ( GUI.Button( new Rect( 310, 130, 60, 60 ), m_tRockButtonArray[ 5 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Rock;

                m_gSelectedRock = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GRockPrefabArray[ 5 ];
                m_sSelectedRockName = GameObject.Find( "TileMap" ).GetComponent<TileMapView>().SRockPrefabNameArray[ 5 ];
            }
        }
    }
}