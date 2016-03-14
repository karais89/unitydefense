/**
 * @file MapEditor.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-11-01
 */

using UnityEngine;
using DefenseFramework;

namespace MapEditor
{
    public class MapEditor : MonoBehaviour
    {
        public GameObject defaultTile;
        private GameObject selectedTile;
        private string selectedTreeName = null;
        private string selectedRockName = null;

        public Texture[] rockButtonTextures = new Texture[ 6 ];
        public Texture[] treeButtonTextures = new Texture[ 12 ];
        public Texture[] tileButtonTextures = new Texture[ 20 ];

        private GameObject selectedTree;
        private GameObject selectedRock;

        private bool isTileBuildMode = false;
        private bool isTreeBuildMode = false;
        private bool isRockBuildMode = false;
        private bool isEraseMode = false;
        private bool isSpawnMode = false;

        private GameObject spawnPrefab = null;

        // Use this for initialization
        private void Awake()
        {
            spawnPrefab = (GameObject) Resources.Load( "Prefabs/SpawnSphere" );

            GameObject.Find( "Map" ).GetComponent<MapView>().LoadResources();

            GameObject.Find( "Map" ).GetComponent<MapView>().LoadMapJSON();
            // GameObject.Find("Map").GetComponent<MapView>().CreateDefaultTiles();
        }

        // Update is called once per frame
        private void Update()
        {
            if ( isTileBuildMode == true )
            {
                // 마우스 클릭한 지점에 타일 생성
                if ( Input.GetMouseButtonDown( 0 ) )
                {
                    Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                    RaycastHit hitInfo;

                    if ( Physics.Raycast( ray, out hitInfo, 100.0f ) )
                    {
                        if ( hitInfo.collider.tag == "TILE" )
                        {
                            // 선택된 타일을 먼저 삭제하고 새로운 타일을 그 자리에 생성
                            int x = hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().IIndexX;
                            int y = hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().IIndexY;

                            Destroy( hitInfo.collider.gameObject );
                            GameObject.Find( "Map" ).GetComponent<MapView>().GTileArray[ x, y ] = null;

                            Debug.Log( "create new tile" );

                            Quaternion rotation = Quaternion.Euler( 90, 0, 0 );
                            GameObject newTile = (GameObject) Instantiate( selectedTile, hitInfo.collider.transform.position, rotation );
                            newTile.transform.parent = GameObject.Find( "Map" ).transform;
                            GameObject.Find( "Map" ).GetComponent<MapView>().GTileArray[ x, y ] = newTile;

                            newTile.GetComponent<DefenseFramework.TileModel>().IIndexX = x;
                            newTile.GetComponent<DefenseFramework.TileModel>().IIndexY = y;
                            newTile.GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.walkable;

                            for ( int i = 0; i < 20; i++ )
                            {
                                if ( selectedTile == GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ i ] )
                                {
                                    newTile.GetComponent<DefenseFramework.TileView>().PrefabName = "PavementTile" + ( i + 1 );
                                    break;
                                }
                            }
                            newTile.GetComponent<DefenseFramework.TileModel>().BHasObstacle = false;
                            newTile.GetComponent<DefenseFramework.TileView>().ObstacleName = "";
                        }
                    }
                }
            }

            if ( isTreeBuildMode == true )
            {
                // 마우스 클릭한 지점에 나무 생성
                if ( Input.GetMouseButtonDown( 0 ) )
                {
                    Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                    RaycastHit hitInfo;

                    if ( Physics.Raycast( ray, out hitInfo, 100.0f ) )
                    {
                        if ( hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().BHasObstacle == false )
                        {
                            Debug.Log( "create new tree" );

                            GameObject newTree = (GameObject) Instantiate( selectedTree, hitInfo.collider.transform.position, Quaternion.identity );

                            newTree.transform.parent = hitInfo.collider.gameObject.transform;

                            hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.obstacle;
                            hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().BHasObstacle = true;
                            hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileView>().ObstacleName = selectedTreeName;
                        }
                    }
                }
            }

            if ( isRockBuildMode == true )
            {
                // 마우스 클릭한 지점에 바위 생성
                if ( Input.GetMouseButtonDown( 0 ) )
                {
                    Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                    RaycastHit hitInfo;

                    if ( Physics.Raycast( ray, out hitInfo, 100.0f ) )
                    {
                        if ( hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().BHasObstacle == false )
                        {
                            Debug.Log( "create new rock" );

                            GameObject newRock = (GameObject) Instantiate( selectedRock, hitInfo.collider.transform.position, Quaternion.identity );

                            newRock.transform.parent = hitInfo.collider.gameObject.transform;

                            hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.obstacle;
                            hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().BHasObstacle = true;
                            hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileView>().ObstacleName = selectedRockName;
                        }
                    }
                }
            }

            if ( isEraseMode == true )
            {
                // 마우스 클릭한 지점의 오브젝트 삭제
                if ( Input.GetMouseButtonDown( 0 ) )
                {
                    Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                    RaycastHit hitInfo;

                    if ( Physics.Raycast( ray, out hitInfo, 100.0f ) )
                    {
                        Debug.Log( "Raycast tag = " + hitInfo.collider.tag );

                        if ( hitInfo.collider.tag == "TREE" || hitInfo.collider.tag == "ROCK" )
                        {
                            Debug.Log( "Erase Tree/Rock" );

                            hitInfo.collider.gameObject.transform.parent.GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.walkable;
                            hitInfo.collider.gameObject.transform.parent.GetComponent<DefenseFramework.TileModel>().BHasObstacle = false;
                            hitInfo.collider.gameObject.transform.parent.GetComponent<DefenseFramework.TileView>().ObstacleName = "";

                            Destroy( hitInfo.collider.gameObject );
                        }
                        else if ( hitInfo.collider.tag == "TILE" )
                        {
                            Debug.Log( "Erase Tile" );

                            int x = hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().IIndexX;
                            int y = hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().IIndexY;

                            Destroy( hitInfo.collider.gameObject );

                            Quaternion rotation = Quaternion.Euler( 90, 0, 0 );
                            GameObject newTile = (GameObject) Instantiate( GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 1 ], hitInfo.collider.transform.position, rotation );

                            newTile.transform.parent = GameObject.Find( "Map" ).transform;

                            GameObject.Find( "Map" ).GetComponent<MapView>().GTileArray[ x, y ] = newTile;

                            newTile.GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.walkable;
                            newTile.GetComponent<DefenseFramework.TileModel>().IIndexX = x;
                            newTile.GetComponent<DefenseFramework.TileModel>().IIndexY = y;
                            newTile.GetComponent<DefenseFramework.TileView>().PrefabName = "PavementTile2";
                            newTile.GetComponent<DefenseFramework.TileModel>().BHasObstacle = false;
                            newTile.GetComponent<DefenseFramework.TileView>().ObstacleName = "";
                        }
                        else if ( hitInfo.collider.tag == "SPAWN" )
                        {
                            Debug.Log( "Erase Spawn" );

                            int x = hitInfo.collider.gameObject.GetComponent<SpawnModel>().IIndexX;
                            int y = hitInfo.collider.gameObject.GetComponent<SpawnModel>().IIndexY;

                            GameObject.Find( "Map" ).GetComponent<MapView>().GTileArray[ x, y ].GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.walkable;

                            Destroy( hitInfo.collider.gameObject );
                        }
                    }
                }
            }

            if ( isSpawnMode == true )
            {
                // 마우스 클릭한 지점에 몬스터 생성 지점을 심는다
                if ( Input.GetMouseButtonDown( 0 ) )
                {
                    Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                    RaycastHit hitInfo;

                    if ( Physics.Raycast( ray, out hitInfo, 100.0f ) )
                    {
                        if ( hitInfo.collider.tag == "TILE" )
                        {
                            int x = hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().IIndexX;
                            int y = hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().IIndexY;

                            GameObject newSpawn = (GameObject) Instantiate( spawnPrefab, hitInfo.collider.transform.position, Quaternion.identity );
                            newSpawn.transform.parent = GameObject.Find( "Map" ).transform;
                            newSpawn.GetComponent<SpawnModel>().IIndexX = x;
                            newSpawn.GetComponent<SpawnModel>().IIndexY = y;

                            GameObject.Find( "SpawnManager" ).GetComponent<SpawnManager>().spawnList.Add( newSpawn );

                            hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.spawn;
                        }
                    }
                }
            }
        }

        private void OnGUI()
        {
            // 맵 리셋 버튼
            if ( GUI.Button( new Rect( Screen.width - 300, Screen.height - 30, 100, 30 ), "Reset" ) )
            {
                GameObject.Find( "Map" ).GetComponent<MapView>().ClearMap();
                GameObject.Find( "Map" ).GetComponent<MapView>().CreateDefaultTiles();
            }

            // 맵 데이터 불러오기 버튼
            if ( GUI.Button( new Rect( Screen.width - 200, Screen.height - 30, 100, 30 ), "Load Map" ) )
            {
                GameObject.Find( "Map" ).GetComponent<MapView>().ClearMap();
                GameObject.Find( "Map" ).GetComponent<MapView>().LoadMapJSON();
            }

            // 맵 데이터 저장 버튼
            if ( GUI.Button( new Rect( Screen.width - 100, Screen.height - 30, 100, 30 ), "Save Map" ) )
            {
                GameObject.Find( "Map" ).GetComponent<MapView>().WriteMapJSON();
            }

            // 지우기 버튼
            if ( GUI.Button( new Rect( 10, Screen.height - 30, 100, 30 ), "Erase" ) )
            {
                if ( isEraseMode == false )
                {
                    isEraseMode = true;
                }
                else if ( isEraseMode == true )
                {
                    isEraseMode = false;
                }

                isTileBuildMode = false;
                isTreeBuildMode = false;
                isRockBuildMode = false;
            }

            // Spwan 생성 버튼
            if ( GUI.Button( new Rect( 110, Screen.height - 30, 100, 30 ), "Spawn Point" ) )
            {
                if ( isSpawnMode == false )
                {
                    isSpawnMode = true;
                }
                else if ( isSpawnMode == true )
                {
                    isSpawnMode = false;
                }

                isEraseMode = false;
                isTileBuildMode = false;
                isTreeBuildMode = false;
                isRockBuildMode = false;
            }

            // 타일 버튼
            if ( GUI.Button( new Rect( 10, 10, 60, 60 ), tileButtonTextures[ 0 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 0 ];
            }
            if ( GUI.Button( new Rect( 70, 10, 60, 60 ), tileButtonTextures[ 1 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 1 ];
            }
            if ( GUI.Button( new Rect( 130, 10, 60, 60 ), tileButtonTextures[ 2 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 2 ];
            }
            if ( GUI.Button( new Rect( 190, 10, 60, 60 ), tileButtonTextures[ 3 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 3 ];
            }
            if ( GUI.Button( new Rect( 250, 10, 60, 60 ), tileButtonTextures[ 4 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 4 ];
            }
            if ( GUI.Button( new Rect( 310, 10, 60, 60 ), tileButtonTextures[ 5 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 5 ];
            }
            if ( GUI.Button( new Rect( 370, 10, 60, 60 ), tileButtonTextures[ 6 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 6 ];
            }
            if ( GUI.Button( new Rect( 430, 10, 60, 60 ), tileButtonTextures[ 7 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 7 ];
            }
            if ( GUI.Button( new Rect( 490, 10, 60, 60 ), tileButtonTextures[ 8 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 8 ];
            }
            if ( GUI.Button( new Rect( 550, 10, 60, 60 ), tileButtonTextures[ 9 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 9 ];
            }
            if ( GUI.Button( new Rect( 610, 10, 60, 60 ), tileButtonTextures[ 10 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 10 ];
            }
            if ( GUI.Button( new Rect( 670, 10, 60, 60 ), tileButtonTextures[ 11 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 11 ];
            }
            if ( GUI.Button( new Rect( 730, 10, 60, 60 ), tileButtonTextures[ 12 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 12 ];
            }
            if ( GUI.Button( new Rect( 790, 10, 60, 60 ), tileButtonTextures[ 13 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 13 ];
            }
            if ( GUI.Button( new Rect( 850, 10, 60, 60 ), tileButtonTextures[ 14 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 14 ];
            }
            if ( GUI.Button( new Rect( 910, 10, 60, 60 ), tileButtonTextures[ 15 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 15 ];
            }
            if ( GUI.Button( new Rect( 970, 10, 60, 60 ), tileButtonTextures[ 16 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 16 ];
            }
            if ( GUI.Button( new Rect( 1030, 10, 60, 60 ), tileButtonTextures[ 17 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 17 ];
            }
            if ( GUI.Button( new Rect( 1090, 10, 60, 60 ), tileButtonTextures[ 18 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 18 ];
            }
            if ( GUI.Button( new Rect( 1150, 10, 60, 60 ), tileButtonTextures[ 19 ] ) )
            {
                isTileBuildMode = true;
                isTreeBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTile = GameObject.Find( "Map" ).GetComponent<MapView>().GTilePrefabArray[ 19 ];
            }

            // 나무 버튼
            if ( GUI.Button( new Rect( 10, 70, 60, 60 ), treeButtonTextures[ 0 ] ) )
            {
                isTreeBuildMode = true;
                isTileBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTree = GameObject.Find( "Map" ).GetComponent<MapView>().GTreePrefabArray[ 0 ];
                selectedTreeName = GameObject.Find( "Map" ).GetComponent<MapView>().STreePrefabNameArray[ 0 ];
            }
            if ( GUI.Button( new Rect( 70, 70, 60, 60 ), treeButtonTextures[ 1 ] ) )
            {
                isTreeBuildMode = true;
                isTileBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTree = GameObject.Find( "Map" ).GetComponent<MapView>().GTreePrefabArray[ 1 ];
                selectedTreeName = GameObject.Find( "Map" ).GetComponent<MapView>().STreePrefabNameArray[ 1 ];
            }
            if ( GUI.Button( new Rect( 130, 70, 60, 60 ), treeButtonTextures[ 2 ] ) )
            {
                isTreeBuildMode = true;
                isTileBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTree = GameObject.Find( "Map" ).GetComponent<MapView>().GTreePrefabArray[ 2 ];
                selectedTreeName = GameObject.Find( "Map" ).GetComponent<MapView>().STreePrefabNameArray[ 2 ];
            }
            if ( GUI.Button( new Rect( 190, 70, 60, 60 ), treeButtonTextures[ 3 ] ) )
            {
                isTreeBuildMode = true;
                isTileBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTree = GameObject.Find( "Map" ).GetComponent<MapView>().GTreePrefabArray[ 3 ];
                selectedTreeName = GameObject.Find( "Map" ).GetComponent<MapView>().STreePrefabNameArray[ 3 ];
            }
            if ( GUI.Button( new Rect( 250, 70, 60, 60 ), treeButtonTextures[ 4 ] ) )
            {
                isTreeBuildMode = true;
                isTileBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTree = GameObject.Find( "Map" ).GetComponent<MapView>().GTreePrefabArray[ 4 ];
                selectedTreeName = GameObject.Find( "Map" ).GetComponent<MapView>().STreePrefabNameArray[ 4 ];
            }
            if ( GUI.Button( new Rect( 310, 70, 60, 60 ), treeButtonTextures[ 5 ] ) )
            {
                isTreeBuildMode = true;
                isTileBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTree = GameObject.Find( "Map" ).GetComponent<MapView>().GTreePrefabArray[ 5 ];
                selectedTreeName = GameObject.Find( "Map" ).GetComponent<MapView>().STreePrefabNameArray[ 5 ];
            }
            if ( GUI.Button( new Rect( 370, 70, 60, 60 ), treeButtonTextures[ 6 ] ) )
            {
                isTreeBuildMode = true;
                isTileBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTree = GameObject.Find( "Map" ).GetComponent<MapView>().GTreePrefabArray[ 6 ];
                selectedTreeName = GameObject.Find( "Map" ).GetComponent<MapView>().STreePrefabNameArray[ 6 ];
            }
            if ( GUI.Button( new Rect( 430, 70, 60, 60 ), treeButtonTextures[ 7 ] ) )
            {
                isTreeBuildMode = true;
                isTileBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTree = GameObject.Find( "Map" ).GetComponent<MapView>().GTreePrefabArray[ 7 ];
                selectedTreeName = GameObject.Find( "Map" ).GetComponent<MapView>().STreePrefabNameArray[ 7 ];
            }
            if ( GUI.Button( new Rect( 490, 70, 60, 60 ), treeButtonTextures[ 8 ] ) )
            {
                isTreeBuildMode = true;
                isTileBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTree = GameObject.Find( "Map" ).GetComponent<MapView>().GTreePrefabArray[ 8 ];
                selectedTreeName = GameObject.Find( "Map" ).GetComponent<MapView>().STreePrefabNameArray[ 8 ];
            }
            if ( GUI.Button( new Rect( 550, 70, 60, 60 ), treeButtonTextures[ 9 ] ) )
            {
                isTreeBuildMode = true;
                isTileBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTree = GameObject.Find( "Map" ).GetComponent<MapView>().GTreePrefabArray[ 9 ];
                selectedTreeName = GameObject.Find( "Map" ).GetComponent<MapView>().STreePrefabNameArray[ 9 ];
            }
            if ( GUI.Button( new Rect( 610, 70, 60, 60 ), treeButtonTextures[ 10 ] ) )
            {
                isTreeBuildMode = true;
                isTileBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTree = GameObject.Find( "Map" ).GetComponent<MapView>().GTreePrefabArray[ 10 ];
                selectedTreeName = GameObject.Find( "Map" ).GetComponent<MapView>().STreePrefabNameArray[ 10 ];
            }
            if ( GUI.Button( new Rect( 670, 70, 60, 60 ), treeButtonTextures[ 11 ] ) )
            {
                isTreeBuildMode = true;
                isTileBuildMode = false;
                isRockBuildMode = false;
                isEraseMode = false;

                selectedTree = GameObject.Find( "Map" ).GetComponent<MapView>().GTreePrefabArray[ 11 ];
                selectedTreeName = GameObject.Find( "Map" ).GetComponent<MapView>().STreePrefabNameArray[ 11 ];
            }

            // 바위 버튼
            if ( GUI.Button( new Rect( 10, 130, 60, 60 ), rockButtonTextures[ 0 ] ) )
            {
                isRockBuildMode = true;
                isTreeBuildMode = false;
                isTileBuildMode = false;
                isEraseMode = false;

                selectedRock = GameObject.Find( "Map" ).GetComponent<MapView>().GRockPrefabArray[ 0 ];
                selectedRockName = GameObject.Find( "Map" ).GetComponent<MapView>().SRockPrefabNameArray[ 0 ];
            }
            if ( GUI.Button( new Rect( 70, 130, 60, 60 ), rockButtonTextures[ 1 ] ) )
            {
                isRockBuildMode = true;
                isTreeBuildMode = false;
                isTileBuildMode = false;
                isEraseMode = false;

                selectedRock = GameObject.Find( "Map" ).GetComponent<MapView>().GRockPrefabArray[ 1 ];
                selectedRockName = GameObject.Find( "Map" ).GetComponent<MapView>().SRockPrefabNameArray[ 1 ];
            }
            if ( GUI.Button( new Rect( 130, 130, 60, 60 ), rockButtonTextures[ 2 ] ) )
            {
                isRockBuildMode = true;
                isTreeBuildMode = false;
                isTileBuildMode = false;
                isEraseMode = false;

                selectedRock = GameObject.Find( "Map" ).GetComponent<MapView>().GRockPrefabArray[ 2 ];
                selectedRockName = GameObject.Find( "Map" ).GetComponent<MapView>().SRockPrefabNameArray[ 2 ];
            }
            if ( GUI.Button( new Rect( 190, 130, 60, 60 ), rockButtonTextures[ 3 ] ) )
            {
                isRockBuildMode = true;
                isTreeBuildMode = false;
                isTileBuildMode = false;
                isEraseMode = false;

                selectedRock = GameObject.Find( "Map" ).GetComponent<MapView>().GRockPrefabArray[ 3 ];
                selectedRockName = GameObject.Find( "Map" ).GetComponent<MapView>().SRockPrefabNameArray[ 3 ];
            }
            if ( GUI.Button( new Rect( 250, 130, 60, 60 ), rockButtonTextures[ 4 ] ) )
            {
                isRockBuildMode = true;
                isTreeBuildMode = false;
                isTileBuildMode = false;
                isEraseMode = false;

                selectedRock = GameObject.Find( "Map" ).GetComponent<MapView>().GRockPrefabArray[ 4 ];
                selectedRockName = GameObject.Find( "Map" ).GetComponent<MapView>().SRockPrefabNameArray[ 4 ];
            }
            if ( GUI.Button( new Rect( 310, 130, 60, 60 ), rockButtonTextures[ 5 ] ) )
            {
                isRockBuildMode = true;
                isTreeBuildMode = false;
                isTileBuildMode = false;
                isEraseMode = false;

                selectedRock = GameObject.Find( "Map" ).GetComponent<MapView>().GRockPrefabArray[ 5 ];
                selectedRockName = GameObject.Find( "Map" ).GetComponent<MapView>().SRockPrefabNameArray[ 5 ];
            }
        }
    }
}

