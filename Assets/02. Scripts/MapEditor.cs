using UnityEngine;
using System.Collections;
using LitJson;

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

                newTile.GetComponent<Tile>().indexX = x;
                newTile.GetComponent<Tile>().indexY = y;
                newTile.GetComponent<Tile>().type = Tile.TileType.walkable;
			}
		}

        LoadJSON();
	}

    void LoadJSON()
    {
        LitJson.JsonData data = LitJson.JsonMapper.ToObject(jsonData.text);
        Debug.Log(data);
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
