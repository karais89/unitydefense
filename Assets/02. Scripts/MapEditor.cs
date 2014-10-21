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
    // private GameObjectPool tilePool = new GameObjectPool();


	public Texture tileButtonTexture0;
	public Texture tileButtonTexture1;
	public Texture tileButtonTexture2;
	public Texture tileButtonTexture3;
	public Texture tileButtonTexture4;
	public Texture tileButtonTexture5;
	public Texture tileButtonTexture6;
	public Texture tileButtonTexture7;
	public Texture tileButtonTexture8;

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

        tilePrefabArray[0] = (GameObject)Resources.Load("Prefabs/PavementTile01", typeof(GameObject));
        tilePrefabArray[1] = (GameObject)Resources.Load("Prefabs/PavementTile02", typeof(GameObject));

		// 다수의 디폴트 타일 생성
        
		for (int y = 0; y < sizeY; y++) 
		{
			for (int x = 0; x < sizeX; x++) 
			{
				Vector3 pos = new Vector3( x, 0, y );
				Quaternion rotation = Quaternion.Euler(90, 0, 0);
                GameObject newTile = (GameObject)Instantiate(tilePrefabArray[1], pos, rotation);

                newTile.transform.parent = GameObject.Find("Map").transform;

                tileArray[x, y] = newTile;

                newTile.GetComponent<Tile>().type = Tile.TileType.walkable;
                newTile.GetComponent<Tile>().indexX = x;
                newTile.GetComponent<Tile>().indexY = y;
                newTile.GetComponent<Tile>().prefabName = "PavementTile02";
			}
		}
        

        // LoadMapJSON();
	}

    void LoadMapJSON()
    {
        LitJson.JsonData data = LitJson.JsonMapper.ToObject(jsonData.text);

        for ( int i = 0; i < data["Tile"].Count; i++ )
        {
            // JSON 파일에 담긴 데이터들을 가져온다
            int x = (int) data["Tile"][i]["indexX"];
            int y = (int) data["Tile"][i]["indexY"];

            Tile.TileType type;
            if (data["Tile"][i]["type"].ToString().Equals( "empty" ) == true)
            {
                type = Tile.TileType.empty;
            }
            else if (data["Tile"][i]["type"].ToString().Equals("walkable") == true)
            {
                type = Tile.TileType.walkable;
            }
            else if (data["Tile"][i]["type"].ToString().Equals("obstacle") == true)
            {
                type = Tile.TileType.obstacle;
            }
            else
            {
                type = Tile.TileType.empty;
            }

            GameObject prefab = null;
            if (data["Tile"][i]["prefabName"].ToString().Equals("PavementTile01") == true)
            {
                prefab = tilePrefabArray[0];
            }
            else if (data["Tile"][i]["prefabName"].ToString().Equals("PavementTile02") == true)
            {
                prefab = tilePrefabArray[1];
            }
            else 
            {
                prefab = tilePrefabArray[1];
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
                
            	writer.WriteObjectStart();
            	writer.WritePropertyName("type");
                writer.Write(typeString);                
                writer.WritePropertyName("indexX");
                writer.Write(indexX);
                writer.WritePropertyName("indexY");
                writer.Write(indexY);
                writer.WritePropertyName("prefabName");
                writer.Write(prefabString);
                writer.WriteObjectEnd();
            }
        }

        // Debug.Log(sb.ToString());
        System.IO.File.WriteAllText(@"C:\Project\unitydefense\Assets\Resources\map01.json", sb.ToString());
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
                    // 선택된 타일을 먼저 삭제하고 새로운 타일을 그 자리에 생성
                    int x = hitInfo.collider.gameObject.GetComponent<Tile>().indexX;
                    int y = hitInfo.collider.gameObject.GetComponent<Tile>().indexY;

					Destroy ( hitInfo.collider.gameObject );
                    tileArray[x, y] = null;

					Debug.Log ( "create new tile" );

					Quaternion rotation = Quaternion.Euler(90, 0, 0);
					GameObject newTile = (GameObject) Instantiate( selectedTile, hitInfo.collider.transform.position, rotation );
                    newTile.transform.parent = GameObject.Find("Map").transform;
                    tileArray[x, y] = newTile;

                    newTile.GetComponent<Tile>().indexX = x;
                    newTile.GetComponent<Tile>().indexY = y;
                    newTile.GetComponent<Tile>().type = Tile.TileType.walkable;
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
					Debug.Log ( "create new tree" );

					Instantiate( selectedTree, hitInfo.collider.transform.position, Quaternion.identity );
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
					Debug.Log ( "create new rock" );

					Instantiate( selectedRock, hitInfo.collider.transform.position, Quaternion.identity );
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
        // 맵 데이터 저장 버튼
        if (GUI.Button(new Rect(Screen.width-100, Screen.height-30, 100, 30), "Save JSON") )
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

            selectedTile = null;
		}
		if (GUI.Button(new Rect(190, 10, 60, 60), tileButtonTexture3))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = null;
		}
		if (GUI.Button(new Rect(250, 10, 60, 60), tileButtonTexture4))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = null;
		}
		if (GUI.Button(new Rect(310, 10, 60, 60), tileButtonTexture5))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = null;
		}
		if (GUI.Button(new Rect(370, 10, 60, 60), tileButtonTexture6))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = null;
		}
		if (GUI.Button(new Rect(430, 10, 60, 60), tileButtonTexture7))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = null;
		}
		if (GUI.Button(new Rect(490, 10, 60, 60), tileButtonTexture8))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;

            selectedTile = null;
		}

		// 나무 버튼
		if (GUI.Button(new Rect(10, 70, 60, 60), "나무 1"))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
			selectedTree = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Cactus_Leafy_01.prefab", typeof(GameObject) );
		}
		if (GUI.Button(new Rect(70, 70, 60, 60), "나무 2"))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
			selectedTree = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Cactus_Leafy_02.prefab", typeof(GameObject) );
		}
		if (GUI.Button(new Rect(130, 70, 60, 60), "나무 3"))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
			selectedTree = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Cactus_Leafy_03.prefab", typeof(GameObject) );
		}
		if (GUI.Button(new Rect(190, 70, 60, 60), "나무 4"))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
			selectedTree = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Cactus_Leafy_04.prefab", typeof(GameObject) );
		}
		if (GUI.Button(new Rect(250, 70, 60, 60), "나무 5"))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
			selectedTree = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Cactus_Leafy_05.prefab", typeof(GameObject) );
        }
		if (GUI.Button(new Rect(310, 70, 60, 60), "나무 6"))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
			selectedTree = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Cactus_Short_01.prefab", typeof(GameObject) );
        }
		if (GUI.Button(new Rect(370, 70, 60, 60), "나무 7"))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
			selectedTree = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Cactus_Short_02.prefab", typeof(GameObject) );
        }
		if (GUI.Button(new Rect(430, 70, 60, 60), "나무 8"))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
			selectedTree = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Cactus_Short_03.prefab", typeof(GameObject) );
        }
		if (GUI.Button(new Rect(490, 70, 60, 60), "나무 9"))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
			selectedTree = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Cactus_Tall_01.prefab", typeof(GameObject) );
        }
		if (GUI.Button(new Rect(550, 70, 60, 60), "나무 10"))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
			selectedTree = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Cactus_Tall_02.prefab", typeof(GameObject) );
        }
		if (GUI.Button(new Rect(610, 70, 60, 60), "나무 11"))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
			selectedTree = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Cactus_Tall_03.prefab", typeof(GameObject) );
        }
		if (GUI.Button(new Rect(670, 70, 60, 60), "나무 12"))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
			selectedTree = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Cactus_Tall_04.prefab", typeof(GameObject) );
        }
        
		// 바위 버튼
		if (GUI.Button(new Rect(10, 130, 60, 60), "바위 1"))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;
			selectedRock = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Rock_Cone.prefab", typeof(GameObject) );
		}
		if (GUI.Button(new Rect(70, 130, 60, 60), "바위 2"))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;
			selectedRock = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Rock_Heavy.prefab", typeof(GameObject) );
		}
		if (GUI.Button(new Rect(130, 130, 60, 60), "바위 3"))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;
			selectedRock = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Rock_Large.prefab", typeof(GameObject) );
		}
		if (GUI.Button(new Rect(190, 130, 60, 60), "바위 4"))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;
			selectedRock = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Rock_Medium.prefab", typeof(GameObject) );
		}
		if (GUI.Button(new Rect(250, 130, 60, 60), "바위 5"))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;
			selectedRock = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Rock_Small_01.prefab", typeof(GameObject) );
		}
		if (GUI.Button(new Rect(310, 130, 60, 60), "바위 6"))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;
			selectedRock = (GameObject) Resources.LoadAssetAtPath ( "Assets/05. Models/CactusPack/Prefabs/Rock_Small_02.prefab", typeof(GameObject) );
		}

    }
}
