﻿/**
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
    public class MapEditorView : MonoBehaviour
    {
        private MapEditorModel m_cModel;

        public GameObject m_gDefaultTile;
        public Texture[] m_tRockButtonArray = new Texture[ 6 ];
        public Texture[] m_tTreeButtonArray = new Texture[ 12 ];
        public Texture[] m_tTileButtonArray = new Texture[ 20 ];

        private GameObject m_gSelectedTree = null;
        private GameObject m_gSelectedRock = null;
        private GameObject m_gSelectedTile = null;

        private string m_sSelectedTreeName = null;
        private string m_sSelectedRockName = null;
        
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
        private void Awake()
        {
            m_cModel = GetComponent<MapEditorModel>();

            GameObject.Find( "Map" ).GetComponent<TileMapView>().LoadResources();

            GameObject.Find( "Map" ).GetComponent<TileMapView>().LoadMapJSON();
            // GameObject.Find("Map").GetComponent<TileMapView>().CreateDefaultTiles();
        }

        private void OnGUI()
        {
            // 맵 리셋 버튼
            if ( GUI.Button( new Rect( Screen.width - 300, Screen.height - 30, 100, 30 ), "Reset" ) )
            {
                GameObject.Find( "Map" ).GetComponent<TileMapView>().ClearMap();
                GameObject.Find( "Map" ).GetComponent<TileMapView>().CreateDefaultTiles();
            }

            // 맵 데이터 불러오기 버튼
            if ( GUI.Button( new Rect( Screen.width - 200, Screen.height - 30, 100, 30 ), "Load Map" ) )
            {
                GameObject.Find( "Map" ).GetComponent<TileMapView>().ClearMap();
                GameObject.Find( "Map" ).GetComponent<TileMapView>().LoadMapJSON();
            }

            // 맵 데이터 저장 버튼
            if ( GUI.Button( new Rect( Screen.width - 100, Screen.height - 30, 100, 30 ), "Save Map" ) )
            {
                GameObject.Find( "Map" ).GetComponent<TileMapView>().WriteMapJSON();
            }

            // 지우기 버튼
            if ( GUI.Button( new Rect( 10, Screen.height - 30, 100, 30 ), "Erase" ) )
            {
                if ( m_cModel.EMode == MapEditorModel.eEditorMode.Erase )
                {
                    m_cModel.EMode = MapEditorModel.eEditorMode.None;
                }
                else if ( m_cModel.EMode != MapEditorModel.eEditorMode.Erase )
                {
                    m_cModel.EMode = MapEditorModel.eEditorMode.Erase;
                }
            }

            // Spwan 생성 버튼
            if ( GUI.Button( new Rect( 110, Screen.height - 30, 100, 30 ), "Spawn Point" ) )
            {
                if ( m_cModel.EMode == MapEditorModel.eEditorMode.Spawn )
                {
                    m_cModel.EMode = MapEditorModel.eEditorMode.None;
                }
                else if ( m_cModel.EMode != MapEditorModel.eEditorMode.Spawn )
                {
                    m_cModel.EMode = MapEditorModel.eEditorMode.Spawn;
                }
            }

            // 타일 버튼
            if ( GUI.Button( new Rect( 10, 10, 60, 60 ), m_tTileButtonArray[ 0 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 0 ];
            }
            if ( GUI.Button( new Rect( 70, 10, 60, 60 ), m_tTileButtonArray[ 1 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 1 ];
            }
            if ( GUI.Button( new Rect( 130, 10, 60, 60 ), m_tTileButtonArray[ 2 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 2 ];
            }
            if ( GUI.Button( new Rect( 190, 10, 60, 60 ), m_tTileButtonArray[ 3 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 3 ];
            }
            if ( GUI.Button( new Rect( 250, 10, 60, 60 ), m_tTileButtonArray[ 4 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 4 ];
            }
            if ( GUI.Button( new Rect( 310, 10, 60, 60 ), m_tTileButtonArray[ 5 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 5 ];
            }
            if ( GUI.Button( new Rect( 370, 10, 60, 60 ), m_tTileButtonArray[ 6 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 6 ];
            }
            if ( GUI.Button( new Rect( 430, 10, 60, 60 ), m_tTileButtonArray[ 7 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 7 ];
            }
            if ( GUI.Button( new Rect( 490, 10, 60, 60 ), m_tTileButtonArray[ 8 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 8 ];
            }
            if ( GUI.Button( new Rect( 550, 10, 60, 60 ), m_tTileButtonArray[ 9 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 9 ];
            }
            if ( GUI.Button( new Rect( 610, 10, 60, 60 ), m_tTileButtonArray[ 10 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 10 ];
            }
            if ( GUI.Button( new Rect( 670, 10, 60, 60 ), m_tTileButtonArray[ 11 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 11 ];
            }
            if ( GUI.Button( new Rect( 730, 10, 60, 60 ), m_tTileButtonArray[ 12 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 12 ];
            }
            if ( GUI.Button( new Rect( 790, 10, 60, 60 ), m_tTileButtonArray[ 13 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 13 ];
            }
            if ( GUI.Button( new Rect( 850, 10, 60, 60 ), m_tTileButtonArray[ 14 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 14 ];
            }
            if ( GUI.Button( new Rect( 910, 10, 60, 60 ), m_tTileButtonArray[ 15 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 15 ];
            }
            if ( GUI.Button( new Rect( 970, 10, 60, 60 ), m_tTileButtonArray[ 16 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 16 ];
            }
            if ( GUI.Button( new Rect( 1030, 10, 60, 60 ), m_tTileButtonArray[ 17 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 17 ];
            }
            if ( GUI.Button( new Rect( 1090, 10, 60, 60 ), m_tTileButtonArray[ 18 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 18 ];
            }
            if ( GUI.Button( new Rect( 1150, 10, 60, 60 ), m_tTileButtonArray[ 19 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tile;

                m_gSelectedTile = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTilePrefabArray[ 19 ];
            }

            // 나무 버튼
            if ( GUI.Button( new Rect( 10, 70, 60, 60 ), m_tTreeButtonArray[ 0 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tree;

                m_gSelectedTree = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTreePrefabArray[ 0 ];
                m_sSelectedTreeName = GameObject.Find( "Map" ).GetComponent<TileMapView>().STreePrefabNameArray[ 0 ];
            }
            if ( GUI.Button( new Rect( 70, 70, 60, 60 ), m_tTreeButtonArray[ 1 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tree;

                m_gSelectedTree = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTreePrefabArray[ 1 ];
                m_sSelectedTreeName = GameObject.Find( "Map" ).GetComponent<TileMapView>().STreePrefabNameArray[ 1 ];
            }
            if ( GUI.Button( new Rect( 130, 70, 60, 60 ), m_tTreeButtonArray[ 2 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tree;

                m_gSelectedTree = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTreePrefabArray[ 2 ];
                m_sSelectedTreeName = GameObject.Find( "Map" ).GetComponent<TileMapView>().STreePrefabNameArray[ 2 ];
            }
            if ( GUI.Button( new Rect( 190, 70, 60, 60 ), m_tTreeButtonArray[ 3 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tree;

                m_gSelectedTree = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTreePrefabArray[ 3 ];
                m_sSelectedTreeName = GameObject.Find( "Map" ).GetComponent<TileMapView>().STreePrefabNameArray[ 3 ];
            }
            if ( GUI.Button( new Rect( 250, 70, 60, 60 ), m_tTreeButtonArray[ 4 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tree;

                m_gSelectedTree = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTreePrefabArray[ 4 ];
                m_sSelectedTreeName = GameObject.Find( "Map" ).GetComponent<TileMapView>().STreePrefabNameArray[ 4 ];
            }
            if ( GUI.Button( new Rect( 310, 70, 60, 60 ), m_tTreeButtonArray[ 5 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tree;
                m_gSelectedTree = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTreePrefabArray[ 5 ];
                m_sSelectedTreeName = GameObject.Find( "Map" ).GetComponent<TileMapView>().STreePrefabNameArray[ 5 ];
            }
            if ( GUI.Button( new Rect( 370, 70, 60, 60 ), m_tTreeButtonArray[ 6 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tree;

                m_gSelectedTree = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTreePrefabArray[ 6 ];
                m_sSelectedTreeName = GameObject.Find( "Map" ).GetComponent<TileMapView>().STreePrefabNameArray[ 6 ];
            }
            if ( GUI.Button( new Rect( 430, 70, 60, 60 ), m_tTreeButtonArray[ 7 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tree;

                m_gSelectedTree = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTreePrefabArray[ 7 ];
                m_sSelectedTreeName = GameObject.Find( "Map" ).GetComponent<TileMapView>().STreePrefabNameArray[ 7 ];
            }
            if ( GUI.Button( new Rect( 490, 70, 60, 60 ), m_tTreeButtonArray[ 8 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tree;

                m_gSelectedTree = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTreePrefabArray[ 8 ];
                m_sSelectedTreeName = GameObject.Find( "Map" ).GetComponent<TileMapView>().STreePrefabNameArray[ 8 ];
            }
            if ( GUI.Button( new Rect( 550, 70, 60, 60 ), m_tTreeButtonArray[ 9 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tree;

                m_gSelectedTree = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTreePrefabArray[ 9 ];
                m_sSelectedTreeName = GameObject.Find( "Map" ).GetComponent<TileMapView>().STreePrefabNameArray[ 9 ];
            }
            if ( GUI.Button( new Rect( 610, 70, 60, 60 ), m_tTreeButtonArray[ 10 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tree;

                m_gSelectedTree = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTreePrefabArray[ 10 ];
                m_sSelectedTreeName = GameObject.Find( "Map" ).GetComponent<TileMapView>().STreePrefabNameArray[ 10 ];
            }
            if ( GUI.Button( new Rect( 670, 70, 60, 60 ), m_tTreeButtonArray[ 11 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Tree;

                m_gSelectedTree = GameObject.Find( "Map" ).GetComponent<TileMapView>().GTreePrefabArray[ 11 ];
                m_sSelectedTreeName = GameObject.Find( "Map" ).GetComponent<TileMapView>().STreePrefabNameArray[ 11 ];
            }

            // 바위 버튼
            if ( GUI.Button( new Rect( 10, 130, 60, 60 ), m_tRockButtonArray[ 0 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Rock;

                m_gSelectedRock = GameObject.Find( "Map" ).GetComponent<TileMapView>().GRockPrefabArray[ 0 ];
                m_sSelectedRockName = GameObject.Find( "Map" ).GetComponent<TileMapView>().SRockPrefabNameArray[ 0 ];
            }
            if ( GUI.Button( new Rect( 70, 130, 60, 60 ), m_tRockButtonArray[ 1 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Rock;

                m_gSelectedRock = GameObject.Find( "Map" ).GetComponent<TileMapView>().GRockPrefabArray[ 1 ];
                m_sSelectedRockName = GameObject.Find( "Map" ).GetComponent<TileMapView>().SRockPrefabNameArray[ 1 ];
            }
            if ( GUI.Button( new Rect( 130, 130, 60, 60 ), m_tRockButtonArray[ 2 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Rock;

                m_gSelectedRock = GameObject.Find( "Map" ).GetComponent<TileMapView>().GRockPrefabArray[ 2 ];
                m_sSelectedRockName = GameObject.Find( "Map" ).GetComponent<TileMapView>().SRockPrefabNameArray[ 2 ];
            }
            if ( GUI.Button( new Rect( 190, 130, 60, 60 ), m_tRockButtonArray[ 3 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Rock;

                m_gSelectedRock = GameObject.Find( "Map" ).GetComponent<TileMapView>().GRockPrefabArray[ 3 ];
                m_sSelectedRockName = GameObject.Find( "Map" ).GetComponent<TileMapView>().SRockPrefabNameArray[ 3 ];
            }
            if ( GUI.Button( new Rect( 250, 130, 60, 60 ), m_tRockButtonArray[ 4 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Rock;

                m_gSelectedRock = GameObject.Find( "Map" ).GetComponent<TileMapView>().GRockPrefabArray[ 4 ];
                m_sSelectedRockName = GameObject.Find( "Map" ).GetComponent<TileMapView>().SRockPrefabNameArray[ 4 ];
            }
            if ( GUI.Button( new Rect( 310, 130, 60, 60 ), m_tRockButtonArray[ 5 ] ) )
            {
                m_cModel.EMode = MapEditorModel.eEditorMode.Rock;

                m_gSelectedRock = GameObject.Find( "Map" ).GetComponent<TileMapView>().GRockPrefabArray[ 5 ];
                m_sSelectedRockName = GameObject.Find( "Map" ).GetComponent<TileMapView>().SRockPrefabNameArray[ 5 ];
            }
        }
    }
}