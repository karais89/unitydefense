using UnityEngine;
using System.Collections;
using LitJson;
using System.Text;

public class TileMap : MonoBehaviour {
	public int sizeX = 64;
	public int sizeY = 64;
	public int tileWidth = 1;
	public int tileHeight = 1;
	private int mapWidth = 0;
	private int mapHeight = 0;
    public GameObject[,] tileArray = new GameObject[64, 64];
    private GameObject gridPrefab;
    private GameObject[,] gridArray = new GameObject[64, 64];
    public TextAsset jsonData;
    public GameObject[] tilePrefabArray = new GameObject[20];
    public GameObject[] treePrefabArray = new GameObject[12];
    public GameObject[] rockPrefabArray = new GameObject[6];
    public string[] treePrefabNameArray = new string[12];
    public string[] rockPrefabNameArray = new string[6];

	void Awake() {

        // LoadResources();

        // LoadMapJSON();

		// CreateTestMap ();

        gridPrefab = (GameObject)Resources.Load("Prefabs/GridQuad", typeof(GameObject));

        CreateGrids();

	}

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
    }

    public void CreateDefaultTiles()
    {
        // 나무와 바위가 없는 다수의 디폴트 타일 생성
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

    public void LoadMapJSON()
    {
        LitJson.JsonData data = LitJson.JsonMapper.ToObject(jsonData.text);

        for (int i = 0; i < data.Count; i++)
        {
            // JSON 파일에 담긴 데이터들을 가져온다
            int x = (int)data[i]["indexX"];
            int y = (int)data[i]["indexY"];

            Tile.TileType type;
            if (data[i]["type"].ToString().Equals("empty") == true)
            {
                type = Tile.TileType.empty;
            }
            else if (data[i]["type"].ToString().Equals("walkable") == true)
            {
                type = Tile.TileType.walkable;
            }
            else if (data[i]["type"].ToString().Equals("obstacle") == true)
            {
                type = Tile.TileType.obstacle;
            }
            else
            {
                type = Tile.TileType.empty;
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
    }

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
                if (type == Tile.TileType.empty)
                {
                    typeString = "emtpy";
                }
                else if (type == Tile.TileType.walkable)
                {
                    typeString = "walkable";
                }
                else if (type == Tile.TileType.obstacle)
                {
                    typeString = "obstacle";
                }
                else
                {
                    typeString = "emtpy";
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
    }

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
               
            }
        }
    }

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
                }
            }
        }
    }
    
	// Use this for initialization
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
