using UnityEngine;
using System.Collections;
using LitJson;
using System.Text;
using System.Collections.Generic;

public class TileMap : MonoBehaviour {
	public const int sizeX = 64;
	public const int sizeY = 64;
	public int tileWidth = 1;
	public int tileHeight = 1;
	private int mapWidth = 0;
	private int mapHeight = 0;
    public int[,] mapData = new int[sizeX, sizeY];
    //public UILabel mapLabel;
    public GameObject[,] tileArray = new GameObject[sizeX, sizeY];
    private GameObject gridPrefab;    
    private GameObject[,] gridArray = new GameObject[sizeX, sizeY];
    private GameObject gridDisablePrefab;
    private GameObject[,] gridDisableArray = new GameObject[sizeX, sizeY];
    public TextAsset jsonData;
    public GameObject[] tilePrefabArray = new GameObject[20];
    public GameObject[] treePrefabArray = new GameObject[12];
    public GameObject[] rockPrefabArray = new GameObject[6];
    public string[] treePrefabNameArray = new string[12];
    public string[] rockPrefabNameArray = new string[6];
    // private GameObject[] spawnArray; 
    private List<GameObject> spawnList = new List<GameObject>();
    private GameObject spawnPrefab;

    

	void Awake() {


        gridPrefab = (GameObject)Resources.Load("Prefabs/GridQuad", typeof(GameObject));
        gridDisablePrefab = (GameObject)Resources.Load("Prefabs/GridDisable", typeof(GameObject));

        CreateGrids();

        
	}

    /// <summary>
    /// 리소스 프리팹들을 불러들인다.
    /// </summary>
    public void LoadResources()
    {
        // 프리팹 미리 불러들인다
        for (int i = 0; i < 20; i++)
        {
            string prefabName = "Prefabs/PavementTile" + (i + 1);
            tilePrefabArray[i] = (GameObject)Resources.Load(prefabName, typeof(GameObject));
            // Debug.Log("Loading prefab: " + prefabName);
        }

        treePrefabArray[0] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Tree_Leafy_01", typeof(GameObject));
        treePrefabArray[1] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Tree_Leafy_02", typeof(GameObject));
        treePrefabArray[2] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Tree_Leafy_03", typeof(GameObject));
        treePrefabArray[3] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Tree_Leafy_04", typeof(GameObject));
        treePrefabArray[4] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Tree_Leafy_05", typeof(GameObject));
        treePrefabArray[5] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Tree_Short_01", typeof(GameObject));
        treePrefabArray[6] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Tree_Short_02", typeof(GameObject));
        treePrefabArray[7] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Tree_Short_03", typeof(GameObject));
        treePrefabArray[8] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Tree_Tall_01", typeof(GameObject));
        treePrefabArray[9] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Tree_Tall_02", typeof(GameObject));
        treePrefabArray[10] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Tree_Tall_03", typeof(GameObject));
        treePrefabArray[11] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Tree_Tall_04", typeof(GameObject));

        rockPrefabArray[0] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Rock_Cone", typeof(GameObject));
        rockPrefabArray[1] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Rock_Heavy", typeof(GameObject));
        rockPrefabArray[2] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Rock_Large", typeof(GameObject));
        rockPrefabArray[3] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Rock_Medium", typeof(GameObject));
        rockPrefabArray[4] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Rock_Small_01", typeof(GameObject));
        rockPrefabArray[5] = (GameObject)Resources.Load("Models/CactusPack/Prefabs/Rock_Small_02", typeof(GameObject));

        treePrefabNameArray[0] = "Tree_Leafy_01";
        treePrefabNameArray[1] = "Tree_Leafy_02";
        treePrefabNameArray[2] = "Tree_Leafy_03";
        treePrefabNameArray[3] = "Tree_Leafy_04";
        treePrefabNameArray[4] = "Tree_Leafy_05";
        treePrefabNameArray[5] = "Tree_Short_01";
        treePrefabNameArray[6] = "Tree_Short_02";
        treePrefabNameArray[7] = "Tree_Short_03";
        treePrefabNameArray[8] = "Tree_Tall_01";
        treePrefabNameArray[9] = "Tree_Tall_02";
        treePrefabNameArray[10] = "Tree_Tall_03";
        treePrefabNameArray[11] = "Tree_Tall_04";

        rockPrefabNameArray[0] = "Rock_Cone";
        rockPrefabNameArray[1] = "Rock_Heavy";
        rockPrefabNameArray[2] = "Rock_Large";
        rockPrefabNameArray[3] = "Rock_Medium";
        rockPrefabNameArray[4] = "Rock_Small_01";
        rockPrefabNameArray[5] = "Rock_Small_02";

        spawnPrefab = (GameObject)Resources.Load("Prefabs/SpawnSphere", typeof(GameObject));
    }

    /// <summary>
    /// 나무와 바위가 없는 다수의 디폴트 타일 생성
    /// </summary>
    public void CreateDefaultTiles()
    {
		for (int y = 0; y < sizeY; y++) 
		{
			for (int x = 0; x < sizeX; x++) 
			{
				Vector3 pos = new Vector3( x, 0, y );
				Quaternion rotation = Quaternion.Euler(90, 0, 0);
                GameObject newTile = (GameObject)Instantiate(tilePrefabArray[1], pos, rotation);
                
                // Map 게임오브젝트를 부모로 둔다
                newTile.transform.parent = GameObject.Find("Map").transform;

                tileArray[x, y] = newTile;

                newTile.GetComponent<Tile>().type = Tile.TileType.walkable;
                newTile.GetComponent<Tile>().indexX = x;
                newTile.GetComponent<Tile>().indexY = y;
                newTile.GetComponent<Tile>().prefabName = "PavementTile2";
                newTile.GetComponent<Tile>().hasObstacle = false;
                newTile.GetComponent<Tile>().obstacleName = "";
			}
		}
	}

    /// <summary>
    /// JSON으로 저장된 맵 데이터를 불러들여 게임 오브젝트를 생성한다.
    /// </summary>
    public void LoadMapJSON()
    {
        LitJson.JsonData data = LitJson.JsonMapper.ToObject(jsonData.text);

        for (int i = 0; i < data.Count; i++)
        {
            // JSON 파일에 담긴 데이터들을 가져온다
            int x = (int)data[i]["indexX"];
            int y = (int)data[i]["indexY"];

            Tile.TileType type;
            if (data[i]["type"].ToString().Equals("walkable") == true)
            {
                type = Tile.TileType.walkable;
            }
            else if (data[i]["type"].ToString().Equals("obstacle") == true)
            {
                type = Tile.TileType.obstacle;
            }
            else if (data[i]["type"].ToString().Equals("spawn") == true)
            {
                type = Tile.TileType.spawn;
            }
            else
            {
                type = Tile.TileType.walkable;
            }

            GameObject prefab = null;
            string prefabName = null;
            for (int j = 0; j < 20; j++)
            {
                if (data[i]["prefabName"].ToString().Equals("PavementTile" + (j + 1)) == true)
                {
                    prefab = tilePrefabArray[j];
                    prefabName = "PavementTile" + (j + 1);
                    break;
                }
            }

            // 가져온 정보를 바탕으로 타일을 생성한다
            Vector3 pos = new Vector3(x, 0, y);
            Quaternion rotation = Quaternion.Euler(90, 0, 0);
            GameObject newTile = (GameObject)Instantiate(prefab, pos, rotation);

            newTile.transform.parent = GameObject.Find("Map").transform;

            tileArray[x, y] = newTile;

            newTile.GetComponent<Tile>().indexX = x;
            newTile.GetComponent<Tile>().indexY = y;
            newTile.GetComponent<Tile>().type = type;
            newTile.GetComponent<Tile>().prefabName = prefabName;
            newTile.GetComponent<Tile>().hasObstacle = false;
            newTile.GetComponent<Tile>().SetTileLabelAnchor();

            int iType = (int)type;
            string strType = iType.ToString();
            newTile.GetComponent<Tile>().SetTileLabel(strType);

            // 몬스터 생성 지점을 표시한다.
            if ( type == Tile.TileType.spawn )
            {
                GameObject newSpawn = (GameObject) Instantiate(spawnPrefab, pos, Quaternion.identity);
                newSpawn.transform.parent = GameObject.Find("Map").transform;
                newSpawn.GetComponent<Spawn>().indexX = x;
                newSpawn.GetComponent<Spawn>().indexY = x;

                GameObject.Find("SpawnManager").GetComponent<SpawnManager>().spawnList.Add(newSpawn);
            }

            // 나무나 돌이 있다면 타일 위에 생성한다.
            GameObject obstacle = null;
            string obstacleName = null;
            bool isEmpty = true;

            if (data[i]["obstacleName"].ToString().Equals("") == true)
            {
                isEmpty = true;
            }
            // 나무
            else if (data[i]["obstacleName"].ToString().StartsWith("Tree") == true)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (data[i]["obstacleName"].ToString().Equals(treePrefabNameArray[j]) == true)
                    {
                        obstacle = treePrefabArray[j];
                        obstacleName = treePrefabNameArray[j];
                        isEmpty = false;

                        // Debug.Log("match obstacle name: " + treePrefabNameArray[j]);
                        break;
                    }
                }
            }
            // 바위
            else if (data[i]["obstacleName"].ToString().StartsWith("Rock") == true)
            {
                for (int k = 0; k < 6; k++)
                {
                    if (data[i]["obstacleName"].ToString().Equals(rockPrefabNameArray[k]) == true)
                    {
                        obstacle = rockPrefabArray[k];
                        obstacleName = rockPrefabNameArray[k];
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

            if (isEmpty == false)
            {
                GameObject newObstacle = (GameObject)Instantiate(obstacle, pos, Quaternion.identity);
                newObstacle.transform.parent = newTile.transform;

                newTile.GetComponent<Tile>().hasObstacle = true;
                newTile.GetComponent<Tile>().obstacleName = obstacleName;
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
        LitJson.JsonWriter writer = new LitJson.JsonWriter(sb);

        writer.WriteArrayStart();
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                GameObject tile = tileArray[x, y];
                string typeString = null;
                Tile.TileType type = tile.GetComponent<Tile>().type;
                int indexX = tile.GetComponent<Tile>().indexX;
                int indexY = tile.GetComponent<Tile>().indexY;
                if (type == Tile.TileType.walkable)
                {
                    typeString = "walkable";
                }
                else if (type == Tile.TileType.obstacle)
                {
                    typeString = "obstacle";
                }
                else if (type == Tile.TileType.spawn)
                {
                    typeString = "spawn";
                }
                else
                {
                    typeString = "walkable";
                }
                string prefabString = tile.GetComponent<Tile>().prefabName;
                string obstacleName = tile.GetComponent<Tile>().obstacleName;

                writer.WriteObjectStart();
                writer.WritePropertyName("type");
                writer.Write(typeString);
                writer.WritePropertyName("indexX");
                writer.Write(indexX);
                writer.WritePropertyName("indexY");
                writer.Write(indexY);
                writer.WritePropertyName("prefabName");
                writer.Write(prefabString);
                writer.WritePropertyName("obstacleName");
                writer.Write(obstacleName);
                writer.WriteObjectEnd();
            }
        }
        writer.WriteArrayEnd();

        // Debug.Log(sb.ToString());
        // TODO
        // 절대경로가 아닌 상대경로로 지정
        System.IO.File.WriteAllText(@"C:\Project\unitydefense\Assets\Resources\map01.json", sb.ToString());
    }

    private void CreateMapData()
    {
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                if ( tileArray[x, y] == null )
                {
                    Debug.Log("tileArray[" + x + ", " + y + "] is NULL");
                }

                mapData[x, y] = (int) tileArray[x, y].GetComponent<Tile>().type;
            }
        }
        
        // 임시 코드...
        // 영웅 타워가 최종 목적지이다.
        mapData[3, 12] = 111;
        tileArray[3, 12].GetComponent<Tile>().type = Tile.TileType.hero;
    }

    /// <summary>
    /// 타일들을 모두 파괴한다.
    /// </summary>
    public void ClearMap()
    {
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Destroy(tileArray[x, y].gameObject);
                tileArray[x, y] = null;
            }
        }

        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().spawnList.Clear();
        GameObject.Find("SpawnManager").GetComponent<SpawnManager>().spawnList = null;
    }

    /// <summary>
    /// 타일 위에 보이는 그리드를 생성한다.
    /// </summary>
    private void CreateGrids()
    {
        for (int y = 0; y < sizeY; y++)        
        {
            for (int x = 0; x < sizeX; x++)        
            {
                Vector3 pos = new Vector3(x, 0.01f, y);
                Quaternion rotation = Quaternion.Euler(90, 0, 0);
                GameObject newGrid = (GameObject)Instantiate(gridPrefab, pos, rotation);
                newGrid.SetActive(false);
                gridArray[x, y] = newGrid;

                // Map 게임오브젝트를 부모로 둔다
                newGrid.transform.parent = GameObject.Find("Map").transform;

                // 건설 불가능한 영역의 그리드를 생성한다.
                GameObject newGridDisable = (GameObject)Instantiate(gridDisablePrefab, pos, rotation);
                newGridDisable.SetActive(false);
                gridDisableArray[x, y] = newGridDisable;

                // Map 게임오브젝트를 부모로 둔다
                newGridDisable.transform.parent = GameObject.Find("Map").transform;

                
            }
        }

    }

    /// <summary>
    /// 건설 가능한 타일과 불가능한 타일에 그리드를 보여줄지 설정한다.
    /// </summary>
    /// <param name="visible"></param>
    public void DisplayGridBuildable( bool visible )
    {
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                // 장애물이 없는 타일에만 그리드 표시
                if (tileArray[x, y].GetComponent<Tile>().hasObstacle == false)
                {
                    if (visible == true)
                    {
                        gridArray[x, y].SetActive(true);
                    }
                    else if (visible == false)
                    {
                        gridArray[x, y].SetActive(false);
                    }
                }
                else if (tileArray[x, y].GetComponent<Tile>().hasObstacle == true)
                {
                    gridArray[x, y].SetActive(false);

                    if ( visible == true )
                    {                        
                        gridDisableArray[x, y].SetActive(true);
                    }
                    else if ( visible == false )
                    {
                        gridDisableArray[x, y].SetActive(false);
                    }                    
                }
            }
        }


        
    }
    
	/// <summary>
	/// Gizmo 그리드를 그린다.
	/// </summary>
    /*
	void OnDrawGizmos () {
		mapWidth = sizeX * tileWidth;
		mapHeight = sizeY * tileHeight;

		// draw layer border
		Gizmos.color = Color.white;
		Gizmos.DrawLine(Vector3.zero, new Vector3(mapWidth, 0, 0));
		Gizmos.DrawLine(Vector3.zero, new Vector3(0, 0, mapHeight));
		Gizmos.DrawLine(new Vector3(mapWidth, 0, 0), new Vector3(mapWidth, 0, mapHeight));
		Gizmos.DrawLine(new Vector3(0, 0, mapHeight), new Vector3(mapWidth, 0, mapHeight));
	
		// draw tile cells
		Gizmos.color = Color.green;
		for (int i = 1; i < sizeX; i++)
		{
			Gizmos.DrawLine(new Vector3(i * tileWidth, 0, 0), new Vector3(i * tileWidth, 0, mapHeight));
		}
		
		for (int i = 1; i < sizeY; i++)
		{
			Gizmos.DrawLine(new Vector3(0, 0, i * tileHeight), new Vector3(mapWidth, 0, i * tileHeight));
		}
	}
     */
	
	// Update is called once per frame
	void Update () {
	
	}
}
