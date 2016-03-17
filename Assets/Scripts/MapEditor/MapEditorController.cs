﻿/**
 * @file MapEditorController.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-15
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DefenseFramework;

namespace MapEditor
{
    public class MapEditorController : MonoBehaviour
    {
        private MapEditorModel m_cModel;
        private MapEditorView m_cView = null;
        private GameObject m_gSpawnPrefab = null;
        private GameObject m_gMap = null;
        private Button m_buttonReset = null;
        private Button m_buttonLoad = null;
        private Button m_buttonSave = null;

        private Button m_buttonSpawn = null;
        private Button m_buttonWayPoint = null;
        private Button m_buttonErase = null;


        private void Awake()
        {
            Debug.Log( "MapEditorController" );
            m_cModel = GetComponent<MapEditorModel>();
            m_cView = GetComponent<MapEditorView>();

            m_gMap = GameObject.Find( "TileMap" );

            AddListenerToButtons();

            Init();
        }

        private void AddListenerToButtons()
        {
            // add button handler
            GameObject objCanvas = GameObject.Find( "Canvas" );
            m_buttonReset = objCanvas.transform.FindChild( "Button - Reset" ).GetComponent<Button>();
            m_buttonReset.onClick.AddListener( OnClickReset );
            m_buttonLoad = objCanvas.transform.FindChild( "Button - Load" ).GetComponent<Button>();
            m_buttonLoad.onClick.AddListener( OnClickLoad );
            m_buttonSave = objCanvas.transform.FindChild( "Button - Save" ).GetComponent<Button>();
            m_buttonSave.onClick.AddListener( OnClickSave );

            m_buttonSpawn = objCanvas.transform.FindChild( "Button - Spawn" ).GetComponent<Button>();
            m_buttonSpawn.onClick.AddListener( OnClickSpawn );
            m_buttonWayPoint = objCanvas.transform.FindChild( "Button - WayPoint" ).GetComponent<Button>();
            m_buttonWayPoint.onClick.AddListener( OnClickWayPoint );
            m_buttonErase = objCanvas.transform.FindChild( "Button - Erase" ).GetComponent<Button>();
            m_buttonErase.onClick.AddListener( OnClickErase );
        }

        private void Init()
        {
            m_gSpawnPrefab = (GameObject) Resources.Load( "Prefabs/SpawnSphere" );

        }

        private void OnClickReset()
        {
            m_gMap.GetComponent<TileMapView>().ClearMap();
            m_gMap.GetComponent<TileMapView>().CreateDefaultTiles();
        }

        private void OnClickLoad()
        {
            m_gMap.GetComponent<TileMapView>().ClearMap();
            m_gMap.GetComponent<TileMapView>().LoadMapJSON();
        }

        private void OnClickSave()
        {
            m_gMap.GetComponent<TileMapView>().WriteMapJSON();
        }

        private void OnClickSpawn()
        {
            if ( m_cModel.EMode == MapEditorModel.eBuildMode.Spawn )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.None;
            }
            else if ( m_cModel.EMode != MapEditorModel.eBuildMode.Spawn )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Spawn;
            }
        }

        private void OnClickWayPoint()
        {
            if ( m_cModel.EMode == MapEditorModel.eBuildMode.WayPoint )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.None;
            }
            else if ( m_cModel.EMode != MapEditorModel.eBuildMode.WayPoint )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.WayPoint;
            }
        }

        private void OnClickErase()
        {
            if ( m_cModel.EMode == MapEditorModel.eBuildMode.Erase )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.None;
            }
            else if ( m_cModel.EMode != MapEditorModel.eBuildMode.Erase )
            {
                m_cModel.EMode = MapEditorModel.eBuildMode.Erase;
            }
        }

        
        // Update is called once per frame
        private void Update()
        {
            CheckButtonDown();
        }

        private void CheckButtonDown()
        {
            switch ( m_cModel.EMode )
            {
                case MapEditorModel.eBuildMode.None:
                    break;
                case MapEditorModel.eBuildMode.Tile:
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
                                GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTileArray[ x, y ] = null;

                                Debug.Log( "create new tile" );

                                Quaternion rotation = Quaternion.Euler( 90, 0, 0 );
                                GameObject newTile = (GameObject) Instantiate( m_cView.GSelectedTile, hitInfo.collider.transform.position, rotation );
                                newTile.transform.parent = GameObject.Find( "TileMap" ).transform;
                                GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTileArray[ x, y ] = newTile;

                                newTile.GetComponent<DefenseFramework.TileModel>().IIndexX = x;
                                newTile.GetComponent<DefenseFramework.TileModel>().IIndexY = y;
                                newTile.GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.Walkable;

                                for ( int i = 0; i < 20; i++ )
                                {
                                    if ( m_cView.GSelectedTile == GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ i ] )
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
                    break;
                case MapEditorModel.eBuildMode.Tree:
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

                                GameObject newTree = (GameObject) Instantiate( m_cView.GSelectedTree, hitInfo.collider.transform.position, Quaternion.identity );

                                newTree.transform.parent = hitInfo.collider.gameObject.transform;

                                hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.Obstacle;
                                hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().BHasObstacle = true;
                                hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileView>().ObstacleName = m_cView.SSelectedTreeName;
                            }
                        }
                    }
                    break;
                case MapEditorModel.eBuildMode.Rock:
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

                                GameObject newRock = (GameObject) Instantiate( m_cView.GSelectedRock, hitInfo.collider.transform.position, Quaternion.identity );

                                newRock.transform.parent = hitInfo.collider.gameObject.transform;

                                hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.Obstacle;
                                hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().BHasObstacle = true;
                                hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileView>().ObstacleName = m_cView.SSelectedRockName;
                            }
                        }
                    }
                    break;
                case MapEditorModel.eBuildMode.Erase:
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

                                hitInfo.collider.gameObject.transform.parent.GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.Walkable;
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
                                GameObject newTile = (GameObject) Instantiate( GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTilePrefabArray[ 1 ], hitInfo.collider.transform.position, rotation );

                                newTile.transform.parent = GameObject.Find( "TileMap" ).transform;

                                GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTileArray[ x, y ] = newTile;

                                newTile.GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.Walkable;
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

                                GameObject.Find( "TileMap" ).GetComponent<TileMapView>().GTileArray[ x, y ].GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.Walkable;

                                Destroy( hitInfo.collider.gameObject );
                            }
                        }
                    }
                    break;
                case MapEditorModel.eBuildMode.Spawn:
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

                                GameObject newSpawn = (GameObject) Instantiate( m_gSpawnPrefab, hitInfo.collider.transform.position, Quaternion.identity );
                                newSpawn.transform.parent = GameObject.Find( "TileMap" ).transform;
                                newSpawn.GetComponent<SpawnModel>().IIndexX = x;
                                newSpawn.GetComponent<SpawnModel>().IIndexY = y;

                                GameObject.Find( "SpawnManager" ).GetComponent<SpawnManager>().spawnList.Add( newSpawn );

                                hitInfo.collider.gameObject.GetComponent<DefenseFramework.TileModel>().EType = DefenseFramework.TileModel.eTileType.Spawn;
                            }
                        }
                    }
                    break;
                case MapEditorModel.eBuildMode.WayPoint:
                    break;
                default:
                    Debug.LogError( "m_cModel.EMode = " + m_cModel.EMode );
                    break;
            }
        }
    }
}