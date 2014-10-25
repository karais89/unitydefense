using UnityEngine;
using System.Collections;
using LitJson;
using System.Text;

public class MapEditor : MonoBehaviour {

	public int sizeX = 64;
	public int sizeY = 64;
    public int tileWidth = 1;
    public int tileHeight = 1;
    private int mapWidth = 0;
    private int mapHeight = 0;

    public TextAsset jsonData;

	public GameObject defaultTile;
	private GameObject selectedTile;
    private GameObject[] tilePrefabArray = new GameObject[20];
    private GameObject[] treePrefabArray = new GameObject[12];
    private GameObject[] rockPrefabArray = new GameObject[6];
    private string[] treePrefabNameArray = new string[12];
    private string[] rockPrefabNameArray = new string[6];
    private string selectedTreeName = null;
    private string selectedRockName = null;


    // private GameObjectPool tilePool = new GameObjectPool();
    public Texture[] rockButtonTexture = new Texture[6];
    public Texture[] treeButtonTexture = new Texture[12];

	public Texture tileButtonTexture0;
	public Texture tileButtonTexture1;
	public Texture tileButtonTexture2;
	public Texture tileButtonTexture3;
	public Texture tileButtonTexture4;
	public Texture tileButtonTexture5;
	public Texture tileButtonTexture6;
	public Texture tileButtonTexture7;
	public Texture tileButtonTexture8;
    public Texture tileButtonTexture9;
    public Texture tileButtonTexture10;
    public Texture tileButtonTexture11;
    public Texture tileButtonTexture12;
    public Texture tileButtonTexture13;
    public Texture tileButtonTexture14;
    public Texture tileButtonTexture15;
    public Texture tileButtonTexture16;
    public Texture tileButtonTexture17;
    public Texture tileButtonTexture18;
    public Texture tileButtonTexture19;
    

	private GameObject selectedTree;
	private GameObject selectedRock;
	/*
	public Texture treeButtonTexture0;
	public Texture treeButtonTexture1;
	public Texture treeButtonTexture2;
	public Texture treeButtonTexture3;
	*/

	private bool isTileBuildMode = false;
	private bool isTreeBuildMode = false;
	private bool isRockBuildMode = false;

    public GameObject[,] tileArray = new GameObject[64, 64];

	// Use this for initialization
	void Awake () {

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



        LoadMapJSON();
	}

    void CreateDefaultTiles()
    {
        // 나무와 바위가 없는 다수의 디폴트 타일 생성
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                Vector3 pos = new Vector3(x, 0, y);
                Quaternion rotation = Quaternion.Euler(90, 0, 0);
                GameObject newTile = (GameObject)Instantiate(tilePrefabArray[1], pos, rotation);

                newTile.transform.parent = GameObject.Find("Map").transform;

                tileArray[x, y] = newTile;

                newTile.GetComponent<Tile>().type = Tile.TileType.walkable;
                newTile.GetComponent<Tile>().indexX = x;
                newTile.GetComponent<Tile>().indexY = y;
                newTile.GetComponent<Tile>().prefabName = "PavementTile2";
            }
        }
    }

    void LoadMapJSON()
    {
        LitJson.JsonData data = LitJson.JsonMapper.ToObject(jsonData.text);

        for ( int i = 0; i < data.Count; i++ )
        {
            // JSON 파일에 담긴 데이터들을 가져온다
            int x = (int) data[i]["indexX"];
            int y = (int) data[i]["indexY"];

            Tile.TileType type;
            if (data[i]["type"].ToString().Equals( "empty" ) == true)
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
            for (int j = 0; j < 20; j++ )
            {
                if (data[i]["prefabName"].ToString().Equals("PavementTile"+(j+1)) == true)
                {
                    prefab = tilePrefabArray[j];
                    prefabName = "PavementTile"+(j+1);
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
                    if (data[i]["obstacleName"].ToString().Equals( treePrefabNameArray[j] ) == true)
                    {
                        obstacle = treePrefabArray[j];
                        obstacleName = treePrefabNameArray[j];
                        isEmpty = false;

                        Debug.Log("match obstacle name: " + treePrefabNameArray[j]);
                        break;
                    }
                }
            }
            // 바위
            else if (data[i]["obstacleName"].ToString().StartsWith("Rock") == true)
            {
                for (int k = 0; k < 6; k++)
                {
                    if (data[i]["obstacleName"].ToString().Equals( rockPrefabNameArray[k] ) == true)
                    {
                        obstacle = rockPrefabArray[k];
                        obstacleName = rockPrefabNameArray[k];
                        isEmpty = false;

                        Debug.Log("match obstacle name: " + rockPrefabNameArray[k]);
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

                newTile.GetComponent<Tile>().obstacleName = obstacleName;
            }
        }
    }

    void WriteMapJSON()
    {
    	StringBuilder sb = new StringBuilder();
    	LitJson.JsonWriter writer = new LitJson.JsonWriter(sb);

        writer.WriteArrayStart();
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
            	GameObject tile = tileArray[ x, y ];
            	string typeString = null;
                Tile.TileType type = tile.GetComponent<Tile>().type;
                int indexX = tile.GetComponent<Tile>().indexX;
                int indexY = tile.GetComponent<Tile>().indexY;
                if ( type == Tile.TileType.empty )
                {
                    typeString = "emtpy";
                }
                else if ( type == Tile.TileType.walkable )
                {
                	typeString = "walkable";
                }
                else if ( type == Tile.TileType.obstacle )
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


    void ClearMap()
    {
        for (int y = 0; y < sizeY; y++ )
        {
            for (int x = 0; x < sizeX; x++)
            {
                Destroy(tileArray[x, y].gameObject);
            }
        }
            
    }
	
	// Update is called once per frame
	void Update () {
		if ( isTileBuildMode == true )
		{
			// 마우스 클릭한 지점에 타일 생성
			if (Input.GetMouseButtonDown (0)) 
			{
				Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hitInfo;
				
				if ( Physics.Raycast( ray, out hitInfo, 100.0f ) )
				{
                    if ( hitInfo.collider.tag == "TILE" )
                    {
                        // 선택된 타일을 먼저 삭제하고 새로운 타일을 그 자리에 생성
                        int x = hitInfo.collider.gameObject.GetComponent<Tile>().indexX;
                        int y = hitInfo.collider.gameObject.GetComponent<Tile>().indexY;

                        Destroy(hitInfo.collider.gameObject);
                        tileArray[x, y] = null;

                        Debug.Log("create new tile");

                        Quaternion rotation = Quaternion.Euler(90, 0, 0);
                        GameObject newTile = (GameObject)Instantiate(selectedTile, hitInfo.collider.transform.position, rotation);
                        newTile.transform.parent = GameObject.Find("Map").transform;
                        tileArray[x, y] = newTile;

                        newTile.GetComponent<Tile>().indexX = x;
                        newTile.GetComponent<Tile>().indexY = y;
                        newTile.GetComponent<Tile>().type = Tile.TileType.walkable;

                        for (int i = 0; i < 20; i++)
                        {
                            if (selectedTile == tilePrefabArray[i])
                            {
                                newTile.GetComponent<Tile>().prefabName = "PavementTile" + (i + 1);
                                break;
                            }
                        }   
                    }                    
				}				
			}
		}

		if ( isTreeBuildMode == true )
		{
			// 마우스 클릭한 지점에 나무 생성
			if (Input.GetMouseButtonDown (0)) 
			{
				Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hitInfo;
				
				if ( Physics.Raycast( ray, out hitInfo, 100.0f ) )
				{
                    if ( hitInfo.collider.gameObject.GetComponent<Tile>().hasObstacle == false )
                    {
                        Debug.Log("create new tree");

                        GameObject newTree = (GameObject)Instantiate(selectedTree, hitInfo.collider.transform.position, Quaternion.identity);

                        newTree.transform.parent = hitInfo.collider.gameObject.transform;

                        hitInfo.collider.gameObject.GetComponent<Tile>().hasObstacle = true;
                        hitInfo.collider.gameObject.GetComponent<Tile>().obstacleName = selectedTreeName;
                    }                    
				}
			}
		}

		if ( isRockBuildMode == true )
		{
			// 마우스 클릭한 지점에 바위 생성
			if (Input.GetMouseButtonDown (0)) 
			{
				Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hitInfo;
				
				if ( Physics.Raycast( ray, out hitInfo, 100.0f ) )
				{
                    if (hitInfo.collider.gameObject.GetComponent<Tile>().hasObstacle == false)
                    {
                        Debug.Log("create new rock");

                        GameObject newRock = (GameObject) Instantiate(selectedRock, hitInfo.collider.transform.position, Quaternion.identity);

                        newRock.transform.parent = hitInfo.collider.gameObject.transform;

                        hitInfo.collider.gameObject.GetComponent<Tile>().hasObstacle = true;
                        hitInfo.collider.gameObject.GetComponent<Tile>().obstacleName = selectedRockName;
                    }
				}
			}
		}
	}

    void OnDrawGizmos()
    {
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

	void OnGUI()
	{
        // 맵 리셋 버튼
        if (GUI.Button(new Rect(Screen.width - 300, Screen.height - 30, 100, 30), "Reset"))
        {
            ClearMap();
            CreateDefaultTiles();
        }

        // 맵 데이터 불러오기 버튼
        if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 30, 100, 30), "Load Map"))
        {
            LoadMapJSON();
        }

        // 맵 데이터 저장 버튼
        if (GUI.Button(new Rect(Screen.width-100, Screen.height-30, 100, 30), "Save Map") )
        {
            WriteMapJSON();
        }

		// 타일 버튼
		if (GUI.Button(new Rect(10, 10, 60, 60), tileButtonTexture0))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
			
            selectedTile = tilePrefabArray[0];
		}
		if (GUI.Button(new Rect(70, 10, 60, 60), tileButtonTexture1))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
			
            selectedTile = tilePrefabArray[1];
		}
		if (GUI.Button(new Rect(130, 10, 60, 60), tileButtonTexture2))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = tilePrefabArray[2];
		}
		if (GUI.Button(new Rect(190, 10, 60, 60), tileButtonTexture3))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = tilePrefabArray[3];
		}
		if (GUI.Button(new Rect(250, 10, 60, 60), tileButtonTexture4))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = tilePrefabArray[4];
		}
		if (GUI.Button(new Rect(310, 10, 60, 60), tileButtonTexture5))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = tilePrefabArray[5];
		}
		if (GUI.Button(new Rect(370, 10, 60, 60), tileButtonTexture6))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = tilePrefabArray[6];
		}
		if (GUI.Button(new Rect(430, 10, 60, 60), tileButtonTexture7))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = tilePrefabArray[7];
		}
		if (GUI.Button(new Rect(490, 10, 60, 60), tileButtonTexture8))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = tilePrefabArray[8];
		}
        if (GUI.Button(new Rect(550, 10, 60, 60), tileButtonTexture9))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;

            selectedTile = tilePrefabArray[9];
        }
        if (GUI.Button(new Rect(610, 10, 60, 60), tileButtonTexture10))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;

            selectedTile = tilePrefabArray[10];
        }
        if (GUI.Button(new Rect(670, 10, 60, 60), tileButtonTexture11))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;

            selectedTile = tilePrefabArray[11];
        }
        if (GUI.Button(new Rect(730, 10, 60, 60), tileButtonTexture12))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;

            selectedTile = tilePrefabArray[12];
        }
        if (GUI.Button(new Rect(790, 10, 60, 60), tileButtonTexture13))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;

            selectedTile = tilePrefabArray[13];
        }
        if (GUI.Button(new Rect(850, 10, 60, 60), tileButtonTexture14))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;

            selectedTile = tilePrefabArray[14];
        }
        if (GUI.Button(new Rect(910, 10, 60, 60), tileButtonTexture15))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;

            selectedTile = tilePrefabArray[15];
        }
        if (GUI.Button(new Rect(970, 10, 60, 60), tileButtonTexture16))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;

            selectedTile = tilePrefabArray[16];
        }
        if (GUI.Button(new Rect(1030, 10, 60, 60), tileButtonTexture17))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;

            selectedTile = tilePrefabArray[17];
        }
        if (GUI.Button(new Rect(1090, 10, 60, 60), tileButtonTexture18))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;

            selectedTile = tilePrefabArray[18];
        }
        if (GUI.Button(new Rect(1150, 10, 60, 60), tileButtonTexture19))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;

            selectedTile = tilePrefabArray[19];
        }

		// 나무 버튼
        if (GUI.Button(new Rect(10, 70, 60, 60), treeButtonTexture[0]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;

			selectedTree = treePrefabArray[0];
            selectedTreeName = treePrefabNameArray[0];
		}
        if (GUI.Button(new Rect(70, 70, 60, 60), treeButtonTexture[1]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;

            selectedTree = treePrefabArray[1];
            selectedTreeName = treePrefabNameArray[1];
		}
        if (GUI.Button(new Rect(130, 70, 60, 60), treeButtonTexture[2]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;

            selectedTree = treePrefabArray[2];
            selectedTreeName = treePrefabNameArray[2];
		}
        if (GUI.Button(new Rect(190, 70, 60, 60), treeButtonTexture[3]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;

            selectedTree = treePrefabArray[3];
            selectedTreeName = treePrefabNameArray[3];
		}
        if (GUI.Button(new Rect(250, 70, 60, 60), treeButtonTexture[4]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;

            selectedTree = treePrefabArray[4];
            selectedTreeName = treePrefabNameArray[4];
        }
        if (GUI.Button(new Rect(310, 70, 60, 60), treeButtonTexture[5]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;

            selectedTree = treePrefabArray[5];
            selectedTreeName = treePrefabNameArray[5];
        }
        if (GUI.Button(new Rect(370, 70, 60, 60), treeButtonTexture[6]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;

            selectedTree = treePrefabArray[6];
            selectedTreeName = treePrefabNameArray[6];
        }
        if (GUI.Button(new Rect(430, 70, 60, 60), treeButtonTexture[7]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;

            selectedTree = treePrefabArray[7];
            selectedTreeName = treePrefabNameArray[7];
        }
        if (GUI.Button(new Rect(490, 70, 60, 60), treeButtonTexture[8]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;

            selectedTree = treePrefabArray[8];
            selectedTreeName = treePrefabNameArray[8];
        }
        if (GUI.Button(new Rect(550, 70, 60, 60), treeButtonTexture[9]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;

            selectedTree = treePrefabArray[9];
            selectedTreeName = treePrefabNameArray[9];
        }
        if (GUI.Button(new Rect(610, 70, 60, 60), treeButtonTexture[10]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;

            selectedTree = treePrefabArray[10];
            selectedTreeName = treePrefabNameArray[10];
        }
        if (GUI.Button(new Rect(670, 70, 60, 60), treeButtonTexture[11]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;

            selectedTree = treePrefabArray[11];
            selectedTreeName = treePrefabNameArray[11];
        }
        
		// 바위 버튼
		if (GUI.Button(new Rect(10, 130, 60, 60), rockButtonTexture[0]))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;

            selectedRock = rockPrefabArray[0];
            selectedRockName = rockPrefabNameArray[0];
		}
        if (GUI.Button(new Rect(70, 130, 60, 60), rockButtonTexture[1]))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;

            selectedRock = rockPrefabArray[1];
            selectedRockName = rockPrefabNameArray[1];
		}
        if (GUI.Button(new Rect(130, 130, 60, 60), rockButtonTexture[2]))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;

            selectedRock = rockPrefabArray[2];
            selectedRockName = rockPrefabNameArray[2];
		}
        if (GUI.Button(new Rect(190, 130, 60, 60), rockButtonTexture[3]))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;

            selectedRock = rockPrefabArray[3];
            selectedRockName = rockPrefabNameArray[3];
		}
        if (GUI.Button(new Rect(250, 130, 60, 60), rockButtonTexture[4]))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;

            selectedRock = rockPrefabArray[4];
            selectedRockName = rockPrefabNameArray[4];
		}
        if (GUI.Button(new Rect(310, 130, 60, 60), rockButtonTexture[5]))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;

            selectedRock = rockPrefabArray[5];
            selectedRockName = rockPrefabNameArray[5];
		}

    }
}
