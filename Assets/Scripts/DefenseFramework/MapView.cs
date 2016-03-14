/**
 * @file MapView.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;
using System.Text;

namespace DefenseFramework
{
    public class MapView : MonoBehaviour
    {
        private MapModel m_cModel;

        private GameObject[,] m_gTileArray = new GameObject[ MapModel.ISizeX, MapModel.ISizeY ];
        private GameObject m_gGridPrefab;
        private GameObject[,] m_gGridArray = new GameObject[ MapModel.ISizeX, MapModel.ISizeY ];
        private GameObject m_gGridDisablePrefab;
        private GameObject[,] m_gGridDisableArray = new GameObject[ MapModel.ISizeX, MapModel.ISizeY ];
        public TextAsset m_tJsonData;
        private GameObject[] m_gTilePrefabArray = new GameObject[ 20 ];
        private GameObject[] m_gTreePrefabArray = new GameObject[ 12 ];
        private GameObject[] m_gRockPrefabArray = new GameObject[ 6 ];
        private string[] m_sTreePrefabNameArray = new string[ 12 ];
        private string[] m_sRockPrefabNameArray = new string[ 6 ];
        private GameObject m_gSpawnPrefab;

        public GameObject[,] GTileArray
        {
            get
            {
                return m_gTileArray;
            }

            set
            {
                m_gTileArray = value;
            }
        }

        public GameObject[] GTilePrefabArray
        {
            get
            {
                return m_gTilePrefabArray;
            }

            set
            {
                m_gTilePrefabArray = value;
            }
        }

        public GameObject[] GTreePrefabArray
        {
            get
            {
                return m_gTreePrefabArray;
            }

            set
            {
                m_gTreePrefabArray = value;
            }
        }

        public string[] STreePrefabNameArray
        {
            get
            {
                return m_sTreePrefabNameArray;
            }

            set
            {
                m_sTreePrefabNameArray = value;
            }
        }

        public GameObject[] GRockPrefabArray
        {
            get
            {
                return m_gRockPrefabArray;
            }

            set
            {
                m_gRockPrefabArray = value;
            }
        }

        public string[] SRockPrefabNameArray
        {
            get
            {
                return m_sRockPrefabNameArray;
            }

            set
            {
                m_sRockPrefabNameArray = value;
            }
        }

        // TODO
        // JSON 파싱 부분 분리

        private void Awake()
        {
            m_cModel = GetComponent<MapModel>();
            m_gGridPrefab = (GameObject) Resources.Load( "Prefabs/GridQuad", typeof( GameObject ) );
            m_gGridDisablePrefab = (GameObject) Resources.Load( "Prefabs/GridDisable", typeof( GameObject ) );

            CreateGrids();
        }

        /// <summary>
        /// 리소스 프리팹들을 불러들인다.
        /// </summary>
        public void LoadResources()
        {
            // 프리팹 미리 불러들인다
            for ( int i = 0; i < 20; i++ )
            {
                string prefabName = "Prefabs/PavementTile" + ( i + 1 );
                GTilePrefabArray[ i ] = (GameObject) Resources.Load( prefabName, typeof( GameObject ) );
                // Debug.Log("Loading prefab: " + prefabName);
            }

            GTreePrefabArray[ 0 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Tree_Leafy_01", typeof( GameObject ) );
            GTreePrefabArray[ 1 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Tree_Leafy_02", typeof( GameObject ) );
            GTreePrefabArray[ 2 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Tree_Leafy_03", typeof( GameObject ) );
            GTreePrefabArray[ 3 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Tree_Leafy_04", typeof( GameObject ) );
            GTreePrefabArray[ 4 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Tree_Leafy_05", typeof( GameObject ) );
            GTreePrefabArray[ 5 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Tree_Short_01", typeof( GameObject ) );
            GTreePrefabArray[ 6 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Tree_Short_02", typeof( GameObject ) );
            GTreePrefabArray[ 7 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Tree_Short_03", typeof( GameObject ) );
            GTreePrefabArray[ 8 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Tree_Tall_01", typeof( GameObject ) );
            GTreePrefabArray[ 9 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Tree_Tall_02", typeof( GameObject ) );
            GTreePrefabArray[ 10 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Tree_Tall_03", typeof( GameObject ) );
            GTreePrefabArray[ 11 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Tree_Tall_04", typeof( GameObject ) );

            GRockPrefabArray[ 0 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Rock_Cone", typeof( GameObject ) );
            GRockPrefabArray[ 1 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Rock_Heavy", typeof( GameObject ) );
            GRockPrefabArray[ 2 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Rock_Large", typeof( GameObject ) );
            GRockPrefabArray[ 3 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Rock_Medium", typeof( GameObject ) );
            GRockPrefabArray[ 4 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Rock_Small_01", typeof( GameObject ) );
            GRockPrefabArray[ 5 ] = (GameObject) Resources.Load( "Models/CactusPack/Prefabs/Rock_Small_02", typeof( GameObject ) );

            STreePrefabNameArray[ 0 ] = "Tree_Leafy_01";
            STreePrefabNameArray[ 1 ] = "Tree_Leafy_02";
            STreePrefabNameArray[ 2 ] = "Tree_Leafy_03";
            STreePrefabNameArray[ 3 ] = "Tree_Leafy_04";
            STreePrefabNameArray[ 4 ] = "Tree_Leafy_05";
            STreePrefabNameArray[ 5 ] = "Tree_Short_01";
            STreePrefabNameArray[ 6 ] = "Tree_Short_02";
            STreePrefabNameArray[ 7 ] = "Tree_Short_03";
            STreePrefabNameArray[ 8 ] = "Tree_Tall_01";
            STreePrefabNameArray[ 9 ] = "Tree_Tall_02";
            STreePrefabNameArray[ 10 ] = "Tree_Tall_03";
            STreePrefabNameArray[ 11 ] = "Tree_Tall_04";

            SRockPrefabNameArray[ 0 ] = "Rock_Cone";
            SRockPrefabNameArray[ 1 ] = "Rock_Heavy";
            SRockPrefabNameArray[ 2 ] = "Rock_Large";
            SRockPrefabNameArray[ 3 ] = "Rock_Medium";
            SRockPrefabNameArray[ 4 ] = "Rock_Small_01";
            SRockPrefabNameArray[ 5 ] = "Rock_Small_02";

            m_gSpawnPrefab = (GameObject) Resources.Load( "Prefabs/SpawnSphere", typeof( GameObject ) );
        }

        /// <summary>
        /// 나무와 바위가 없는 다수의 디폴트 타일 생성
        /// </summary>
        public void CreateDefaultTiles()
        {
            for ( int y = 0; y < MapModel.ISizeY; y++ )
            {
                for ( int x = 0; x < MapModel.ISizeX; x++ )
                {
                    Vector3 pos = new Vector3( x, 0, y );
                    Quaternion rotation = Quaternion.Euler( 90, 0, 0 );
                    GameObject newTile = (GameObject) Instantiate( GTilePrefabArray[ 1 ], pos, rotation );

                    // Map 게임오브젝트를 부모로 둔다
                    newTile.transform.parent = GameObject.Find( "Map" ).transform;

                    m_gTileArray[ x, y ] = newTile;

                    newTile.GetComponent<TileModel>().EType = TileModel.eTileType.walkable;
                    newTile.GetComponent<TileModel>().IIndexX = x;
                    newTile.GetComponent<TileModel>().IIndexY = y;
                    newTile.GetComponent<TileView>().PrefabName = "PavementTile2";
                    newTile.GetComponent<TileModel>().BHasObstacle = false;
                    newTile.GetComponent<TileView>().ObstacleName = "";
                }
            }
        }

        /// <summary>
        /// JSON으로 저장된 맵 데이터를 불러들여 게임 오브젝트를 생성한다.
        /// </summary>
        public void LoadMapJSON()
        {
            LitJson.JsonData data = LitJson.JsonMapper.ToObject( m_tJsonData.text );

            for ( int i = 0; i < data.Count; i++ )
            {
                // JSON 파일에 담긴 데이터들을 가져온다
                int x = (int) data[ i ][ "indexX" ];
                int y = (int) data[ i ][ "indexY" ];

                TileModel.eTileType type;
                if ( data[ i ][ "type" ].ToString().Equals( "walkable" ) == true )
                {
                    type = TileModel.eTileType.walkable;
                }
                else if ( data[ i ][ "type" ].ToString().Equals( "obstacle" ) == true )
                {
                    type = TileModel.eTileType.obstacle;
                }
                else if ( data[ i ][ "type" ].ToString().Equals( "spawn" ) == true )
                {
                    type = TileModel.eTileType.spawn;
                }
                else
                {
                    type = TileModel.eTileType.walkable;
                }

                GameObject prefab = null;
                string prefabName = null;
                for ( int j = 0; j < 20; j++ )
                {
                    if ( data[ i ][ "prefabName" ].ToString().Equals( "PavementTile" + ( j + 1 ) ) == true )
                    {
                        prefab = GTilePrefabArray[ j ];
                        prefabName = "PavementTile" + ( j + 1 );
                        break;
                    }
                }

                // 가져온 정보를 바탕으로 타일을 생성한다
                Vector3 pos = new Vector3( x, 0, y );
                Quaternion rotation = Quaternion.Euler( 90, 0, 0 );
                GameObject newTile = (GameObject) Instantiate( prefab, pos, rotation );

                newTile.transform.parent = GameObject.Find( "Map" ).transform;

                m_gTileArray[ x, y ] = newTile;

                newTile.GetComponent<TileModel>().IIndexY = x;
                newTile.GetComponent<TileModel>().IIndexY = y;
                newTile.GetComponent<TileModel>().EType = type;
                newTile.GetComponent<TileView>().PrefabName = prefabName;
                newTile.GetComponent<TileModel>().BHasObstacle = false;
                newTile.GetComponent<TileView>().SetTileLabelAnchor();

                int iType = (int) type;
                string strType = iType.ToString();
                newTile.GetComponent<TileView>().SetTileLabel( strType );

                // 몬스터 생성 지점을 표시한다.
                if ( type == TileModel.eTileType.spawn )
                {
                    GameObject newSpawn = (GameObject) Instantiate( m_gSpawnPrefab, pos, Quaternion.identity );
                    newSpawn.transform.parent = GameObject.Find( "Map" ).transform;
                    newSpawn.GetComponent<SpawnModel>().IIndexX = x;
                    newSpawn.GetComponent<SpawnModel>().IIndexY = x;

                    GameObject.Find( "SpawnManager" ).GetComponent<SpawnManager>().spawnList.Add( newSpawn );
                }

                // 나무나 돌이 있다면 타일 위에 생성한다.
                GameObject obstacle = null;
                string obstacleName = null;
                bool isEmpty = true;

                if ( data[ i ][ "obstacleName" ].ToString().Equals( "" ) == true )
                {
                    isEmpty = true;
                }
                // 나무
                else if ( data[ i ][ "obstacleName" ].ToString().StartsWith( "Tree" ) == true )
                {
                    for ( int j = 0; j < 12; j++ )
                    {
                        if ( data[ i ][ "obstacleName" ].ToString().Equals( STreePrefabNameArray[ j ] ) == true )
                        {
                            obstacle = GTreePrefabArray[ j ];
                            obstacleName = STreePrefabNameArray[ j ];
                            isEmpty = false;

                            // Debug.Log("match obstacle name: " + treePrefabNameArray[j]);
                            break;
                        }
                    }
                }
                // 바위
                else if ( data[ i ][ "obstacleName" ].ToString().StartsWith( "Rock" ) == true )
                {
                    for ( int k = 0; k < 6; k++ )
                    {
                        if ( data[ i ][ "obstacleName" ].ToString().Equals( SRockPrefabNameArray[ k ] ) == true )
                        {
                            obstacle = GRockPrefabArray[ k ];
                            obstacleName = SRockPrefabNameArray[ k ];
                            isEmpty = false;

                            // Debug.Log("match obstacle name: " + rockPrefabNameArray[k]);
                            break;
                        }
                    }
                }
                else
                {
                    isEmpty = true;
                }

                if ( isEmpty == false )
                {
                    GameObject newObstacle = (GameObject) Instantiate( obstacle, pos, Quaternion.identity );
                    newObstacle.transform.parent = newTile.transform;

                    newTile.GetComponent<TileModel>().BHasObstacle = true;
                    newTile.GetComponent<TileView>().ObstacleName = obstacleName;
                }
            }

            CreateMapData();
        }

        /// <summary>
        /// 맵 데이터를 JSON으로 저장한다.
        /// </summary>
        public void WriteMapJSON()
        {
            StringBuilder sb = new StringBuilder();
            LitJson.JsonWriter writer = new LitJson.JsonWriter( sb );

            writer.WriteArrayStart();
            for ( int y = 0; y < MapModel.ISizeY; y++ )
            {
                for ( int x = 0; x < MapModel.ISizeX; x++ )
                {
                    GameObject tile = m_gTileArray[ x, y ];
                    string typeString = null;
                    TileModel.eTileType type = tile.GetComponent<TileModel>().EType;
                    int indexX = tile.GetComponent<TileModel>().IIndexX;
                    int indexY = tile.GetComponent<TileModel>().IIndexY;
                    if ( type == TileModel.eTileType.walkable )
                    {
                        typeString = "walkable";
                    }
                    else if ( type == TileModel.eTileType.obstacle )
                    {
                        typeString = "obstacle";
                    }
                    else if ( type == TileModel.eTileType.spawn )
                    {
                        typeString = "spawn";
                    }
                    else
                    {
                        typeString = "walkable";
                    }
                    string prefabString = tile.GetComponent<TileView>().PrefabName;
                    string obstacleName = tile.GetComponent<TileView>().ObstacleName;

                    writer.WriteObjectStart();
                    writer.WritePropertyName( "type" );
                    writer.Write( typeString );
                    writer.WritePropertyName( "indexX" );
                    writer.Write( indexX );
                    writer.WritePropertyName( "indexY" );
                    writer.Write( indexY );
                    writer.WritePropertyName( "prefabName" );
                    writer.Write( prefabString );
                    writer.WritePropertyName( "obstacleName" );
                    writer.Write( obstacleName );
                    writer.WriteObjectEnd();
                }
            }
            writer.WriteArrayEnd();

            // Debug.Log(sb.ToString());
            // TODO
            // 절대경로가 아닌 상대경로로 지정
            System.IO.File.WriteAllText( @"C:\Project\unitydefense\Assets\Resources\map01.json", sb.ToString() );
        }

        private void CreateMapData()
        {
            for ( int y = 0; y < MapModel.ISizeY; y++ )
            {
                for ( int x = 0; x < MapModel.ISizeX; x++ )
                {
                    if ( m_gTileArray[ x, y ] == null )
                    {
                        Debug.Log( "tileArray[" + x + ", " + y + "] is NULL" );
                    }

                    m_cModel.IMapDataArray[ x, y ] = (int) m_gTileArray[ x, y ].GetComponent<TileModel>().EType;
                }
            }

            // 임시 코드...
            // 영웅 타워가 최종 목적지이다.
            m_cModel.IMapDataArray[ 3, 12 ] = 111;
            m_gTileArray[ 3, 12 ].GetComponent<TileModel>().EType = TileModel.eTileType.hero;
        }

        /// <summary>
        /// 타일들을 모두 파괴한다.
        /// </summary>
        public void ClearMap()
        {
            for ( int y = 0; y < MapModel.ISizeY; y++ )
            {
                for ( int x = 0; x < MapModel.ISizeX; x++ )
                {
                    Destroy( m_gTileArray[ x, y ].gameObject );
                    m_gTileArray[ x, y ] = null;
                }
            }
            if ( GameObject.Find( "SpawnManager" ).GetComponent<SpawnManager>().spawnList != null )
            {
                foreach ( GameObject spawn in GameObject.Find( "SpawnManager" ).GetComponent<SpawnManager>().spawnList )
                {
                    Destroy( spawn );
                }
                GameObject.Find( "SpawnManager" ).GetComponent<SpawnManager>().spawnList.Clear();
                //GameObject.Find("SpawnManager").GetComponent<SpawnManager>().spawnList = null;
            }
        }

        /// <summary>
        /// 타일 위에 보이는 그리드를 생성한다.
        /// </summary>
        private void CreateGrids()
        {
            for ( int y = 0; y < MapModel.ISizeY; y++ )
            {
                for ( int x = 0; x < MapModel.ISizeX; x++ )
                {
                    Vector3 pos = new Vector3( x, 0.01f, y );
                    Quaternion rotation = Quaternion.Euler( 90, 0, 0 );
                    GameObject newGrid = (GameObject) Instantiate( m_gGridPrefab, pos, rotation );
                    newGrid.SetActive( false );
                    m_gGridArray[ x, y ] = newGrid;

                    // Map 게임오브젝트를 부모로 둔다
                    newGrid.transform.parent = GameObject.Find( "Map" ).transform;

                    // 건설 불가능한 영역의 그리드를 생성한다.
                    GameObject newGridDisable = (GameObject) Instantiate( m_gGridDisablePrefab, pos, rotation );
                    newGridDisable.SetActive( false );
                    m_gGridDisableArray[ x, y ] = newGridDisable;

                    // Map 게임오브젝트를 부모로 둔다
                    newGridDisable.transform.parent = GameObject.Find( "Map" ).transform;
                }
            }
        }

        /// <summary>
        /// 건설 가능한 타일과 불가능한 타일에 그리드를 보여줄지 설정한다.
        /// </summary>
        /// <param name="visible"></param>
        public void DisplayGridBuildable( bool visible )
        {
            for ( int y = 0; y < MapModel.ISizeY; y++ )
            {
                for ( int x = 0; x < MapModel.ISizeX; x++ )
                {
                    // 장애물이 없는 타일에만 그리드 표시
                    if ( m_gTileArray[ x, y ].GetComponent<TileModel>().BHasObstacle == false )
                    {
                        if ( visible == true )
                        {
                            m_gGridArray[ x, y ].SetActive( true );
                        }
                        else if ( visible == false )
                        {
                            m_gGridArray[ x, y ].SetActive( false );
                        }
                    }
                    else if ( m_gTileArray[ x, y ].GetComponent<TileModel>().BHasObstacle == true )
                    {
                        m_gGridArray[ x, y ].SetActive( false );

                        if ( visible == true )
                        {
                            m_gGridDisableArray[ x, y ].SetActive( true );
                        }
                        else if ( visible == false )
                        {
                            m_gGridDisableArray[ x, y ].SetActive( false );
                        }
                    }
                }
            }
        }
    }
}