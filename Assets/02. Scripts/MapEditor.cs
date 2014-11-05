using UnityEngine;
using System.Collections;
using LitJson;
using System.Text;

public class MapEditor : MonoBehaviour {
    
	public GameObject defaultTile;
	private GameObject selectedTile;   
    private string selectedTreeName = null;
    private string selectedRockName = null;

    public Texture[] rockButtonTextures = new Texture[6];
    public Texture[] treeButtonTextures = new Texture[12];
    public Texture[] tileButtonTextures = new Texture[20];
   
	private GameObject selectedTree;
	private GameObject selectedRock;
	
	private bool isTileBuildMode = false;
	private bool isTreeBuildMode = false;
	private bool isRockBuildMode = false;
    private bool isEraseMode = false;
    
	// Use this for initialization
	void Awake () 
    {
        GameObject.Find("Map").GetComponent<TileMap>().LoadResources();

        GameObject.Find("Map").GetComponent<TileMap>().LoadMapJSON();
        // GameObject.Find("Map").GetComponent<TileMap>().CreateDefaultTiles();
	}
    
	
	// Update is called once per frame
	void Update () {
		if ( isTileBuildMode == true )
		{
			// 마우스 클릭한 지점에 타일 생성
			if (Input.GetMouseButtonDown(0)) 
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
                        GameObject.Find("Map").GetComponent<TileMap>().tileArray[x, y] = null;                        

                        Debug.Log("create new tile");

                        Quaternion rotation = Quaternion.Euler(90, 0, 0);
                        GameObject newTile = (GameObject)Instantiate(selectedTile, hitInfo.collider.transform.position, rotation);
                        newTile.transform.parent = GameObject.Find("Map").transform;
                        GameObject.Find("Map").GetComponent<TileMap>().tileArray[x, y] = newTile;                        

                        newTile.GetComponent<Tile>().indexX = x;
                        newTile.GetComponent<Tile>().indexY = y;
                        newTile.GetComponent<Tile>().type = Tile.TileType.walkable;

                        for (int i = 0; i < 20; i++)
                        {
                            if (selectedTile == GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[i])
                            {
                                newTile.GetComponent<Tile>().prefabName = "PavementTile" + (i + 1);
                                break;
                            }
                        }
                        newTile.GetComponent<Tile>().hasObstacle = false;
                        newTile.GetComponent<Tile>().obstacleName = "";
                    }                    
				}				
			}
		}

		if ( isTreeBuildMode == true )
		{
			// 마우스 클릭한 지점에 나무 생성
			if (Input.GetMouseButtonDown(0)) 
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
			if (Input.GetMouseButtonDown(0)) 
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

        if (isEraseMode == true)
        {
            // 마우스 클릭한 지점의 오브젝트 삭제
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, 100.0f))
                {
                    if ( hitInfo.collider.tag == "TREE" || hitInfo.collider.tag == "ROCK" )
                    {
                        hitInfo.collider.gameObject.transform.parent.GetComponent<Tile>().type = Tile.TileType.walkable;
                        hitInfo.collider.gameObject.transform.parent.GetComponent<Tile>().hasObstacle = false;
                        hitInfo.collider.gameObject.transform.parent.GetComponent<Tile>().obstacleName = "";

                        Destroy(hitInfo.collider.gameObject);
                    }
                    else if ( hitInfo.collider.tag == "TILE" )
                    {
                        int x = hitInfo.collider.gameObject.GetComponent<Tile>().indexX;
                        int y = hitInfo.collider.gameObject.GetComponent<Tile>().indexY;

                        Destroy(hitInfo.collider.gameObject);

                        Quaternion rotation = Quaternion.Euler(90, 0, 0);
                        GameObject newTile = (GameObject)Instantiate(GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[1], hitInfo.collider.transform.position, rotation);

                        newTile.transform.parent = GameObject.Find("Map").transform;

                        GameObject.Find("Map").GetComponent<TileMap>().tileArray[x, y] = newTile;

                        newTile.GetComponent<Tile>().type = Tile.TileType.walkable;
                        newTile.GetComponent<Tile>().indexX = x;
                        newTile.GetComponent<Tile>().indexY = y;
                        newTile.GetComponent<Tile>().prefabName = "PavementTile2";
                        newTile.GetComponent<Tile>().hasObstacle = false;
                        newTile.GetComponent<Tile>().obstacleName = "";
                    }
                }
            }
        }
	}
    

	void OnGUI()
	{
        // 맵 리셋 버튼
        if (GUI.Button(new Rect(Screen.width - 300, Screen.height - 30, 100, 30), "Reset"))
        {
            GameObject.Find("Map").GetComponent<TileMap>().ClearMap();
            GameObject.Find("Map").GetComponent<TileMap>().CreateDefaultTiles();
        }

        // 맵 데이터 불러오기 버튼
        if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 30, 100, 30), "Load Map"))
        {
            GameObject.Find("Map").GetComponent<TileMap>().ClearMap();
            GameObject.Find("Map").GetComponent<TileMap>().LoadMapJSON();
        }

        // 맵 데이터 저장 버튼
        if (GUI.Button(new Rect(Screen.width-100, Screen.height-30, 100, 30), "Save Map") )
        {
            GameObject.Find("Map").GetComponent<TileMap>().WriteMapJSON();            
        }

        // 지우기 버튼
        if (GUI.Button(new Rect(10, Screen.height - 30, 100, 30), "Erase"))
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

		// 타일 버튼
		if (GUI.Button(new Rect(10, 10, 60, 60), tileButtonTextures[0]))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[0];
		}
        if (GUI.Button(new Rect(70, 10, 60, 60), tileButtonTextures[1]))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[1];
		}
        if (GUI.Button(new Rect(130, 10, 60, 60), tileButtonTextures[2]))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[2];
		}
        if (GUI.Button(new Rect(190, 10, 60, 60), tileButtonTextures[3]))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[3];
		}
        if (GUI.Button(new Rect(250, 10, 60, 60), tileButtonTextures[4]))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[4];
		}
        if (GUI.Button(new Rect(310, 10, 60, 60), tileButtonTextures[5]))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[5];
		}
        if (GUI.Button(new Rect(370, 10, 60, 60), tileButtonTextures[6]))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[6];
		}
        if (GUI.Button(new Rect(430, 10, 60, 60), tileButtonTextures[7]))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[7];
		}
        if (GUI.Button(new Rect(490, 10, 60, 60), tileButtonTextures[8]))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[8];
		}
        if (GUI.Button(new Rect(550, 10, 60, 60), tileButtonTextures[9]))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[9];
        }
        if (GUI.Button(new Rect(610, 10, 60, 60), tileButtonTextures[10]))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[10];
        }
        if (GUI.Button(new Rect(670, 10, 60, 60), tileButtonTextures[11]))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[11];
        }
        if (GUI.Button(new Rect(730, 10, 60, 60), tileButtonTextures[12]))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[12];
        }
        if (GUI.Button(new Rect(790, 10, 60, 60), tileButtonTextures[13]))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false; 
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[13];
        }
        if (GUI.Button(new Rect(850, 10, 60, 60), tileButtonTextures[14]))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[14];
        }
        if (GUI.Button(new Rect(910, 10, 60, 60), tileButtonTextures[15]))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[15];
        }
        if (GUI.Button(new Rect(970, 10, 60, 60), tileButtonTextures[16]))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[16];
        }
        if (GUI.Button(new Rect(1030, 10, 60, 60), tileButtonTextures[17]))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[17];
        }
        if (GUI.Button(new Rect(1090, 10, 60, 60), tileButtonTextures[18]))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[18];
        }
        if (GUI.Button(new Rect(1150, 10, 60, 60), tileButtonTextures[19]))
        {
            isTileBuildMode = true;
            isTreeBuildMode = false;
            isRockBuildMode = false;
            isEraseMode = false;

            selectedTile = GameObject.Find("Map").GetComponent<TileMap>().tilePrefabArray[19];
        }

		// 나무 버튼
        if (GUI.Button(new Rect(10, 70, 60, 60), treeButtonTextures[0]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTree = GameObject.Find("Map").GetComponent<TileMap>().treePrefabArray[0];
            selectedTreeName = GameObject.Find("Map").GetComponent<TileMap>().treePrefabNameArray[0];
		}
        if (GUI.Button(new Rect(70, 70, 60, 60), treeButtonTextures[1]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTree = GameObject.Find("Map").GetComponent<TileMap>().treePrefabArray[1];
            selectedTreeName = GameObject.Find("Map").GetComponent<TileMap>().treePrefabNameArray[1];
		}
        if (GUI.Button(new Rect(130, 70, 60, 60), treeButtonTextures[2]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTree = GameObject.Find("Map").GetComponent<TileMap>().treePrefabArray[2];
            selectedTreeName = GameObject.Find("Map").GetComponent<TileMap>().treePrefabNameArray[2];
		}
        if (GUI.Button(new Rect(190, 70, 60, 60), treeButtonTextures[3]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTree = GameObject.Find("Map").GetComponent<TileMap>().treePrefabArray[3];
            selectedTreeName = GameObject.Find("Map").GetComponent<TileMap>().treePrefabNameArray[3];
		}
        if (GUI.Button(new Rect(250, 70, 60, 60), treeButtonTextures[4]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTree = GameObject.Find("Map").GetComponent<TileMap>().treePrefabArray[4];
            selectedTreeName = GameObject.Find("Map").GetComponent<TileMap>().treePrefabNameArray[4];
        }
        if (GUI.Button(new Rect(310, 70, 60, 60), treeButtonTextures[5]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTree = GameObject.Find("Map").GetComponent<TileMap>().treePrefabArray[5];
            selectedTreeName = GameObject.Find("Map").GetComponent<TileMap>().treePrefabNameArray[5];
        }
        if (GUI.Button(new Rect(370, 70, 60, 60), treeButtonTextures[6]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTree = GameObject.Find("Map").GetComponent<TileMap>().treePrefabArray[6];
            selectedTreeName = GameObject.Find("Map").GetComponent<TileMap>().treePrefabNameArray[6];
        }
        if (GUI.Button(new Rect(430, 70, 60, 60), treeButtonTextures[7]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTree = GameObject.Find("Map").GetComponent<TileMap>().treePrefabArray[7];
            selectedTreeName = GameObject.Find("Map").GetComponent<TileMap>().treePrefabNameArray[7];
        }
        if (GUI.Button(new Rect(490, 70, 60, 60), treeButtonTextures[8]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTree = GameObject.Find("Map").GetComponent<TileMap>().treePrefabArray[8];
            selectedTreeName = GameObject.Find("Map").GetComponent<TileMap>().treePrefabNameArray[8];
        }
        if (GUI.Button(new Rect(550, 70, 60, 60), treeButtonTextures[9]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTree = GameObject.Find("Map").GetComponent<TileMap>().treePrefabArray[9];
            selectedTreeName = GameObject.Find("Map").GetComponent<TileMap>().treePrefabNameArray[9];
        }
        if (GUI.Button(new Rect(610, 70, 60, 60), treeButtonTextures[10]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTree = GameObject.Find("Map").GetComponent<TileMap>().treePrefabArray[10];
            selectedTreeName = GameObject.Find("Map").GetComponent<TileMap>().treePrefabNameArray[10];
        }
        if (GUI.Button(new Rect(670, 70, 60, 60), treeButtonTextures[11]))
		{
			isTreeBuildMode = true;
			isTileBuildMode = false;
			isRockBuildMode = false;
            isEraseMode = false;

            selectedTree = GameObject.Find("Map").GetComponent<TileMap>().treePrefabArray[11];
            selectedTreeName = GameObject.Find("Map").GetComponent<TileMap>().treePrefabNameArray[11];
        }
        
		// 바위 버튼
		if (GUI.Button(new Rect(10, 130, 60, 60), rockButtonTextures[0]))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;
            isEraseMode = false;

            selectedRock = GameObject.Find("Map").GetComponent<TileMap>().rockPrefabArray[0];
            selectedRockName = GameObject.Find("Map").GetComponent<TileMap>().rockPrefabNameArray[0];
		}
        if (GUI.Button(new Rect(70, 130, 60, 60), rockButtonTextures[1]))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;
            isEraseMode = false;

            selectedRock = GameObject.Find("Map").GetComponent<TileMap>().rockPrefabArray[1];
            selectedRockName = GameObject.Find("Map").GetComponent<TileMap>().rockPrefabNameArray[1];
		}
        if (GUI.Button(new Rect(130, 130, 60, 60), rockButtonTextures[2]))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;
            isEraseMode = false;

            selectedRock = GameObject.Find("Map").GetComponent<TileMap>().rockPrefabArray[2];
            selectedRockName = GameObject.Find("Map").GetComponent<TileMap>().rockPrefabNameArray[2];
		}
        if (GUI.Button(new Rect(190, 130, 60, 60), rockButtonTextures[3]))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;
            isEraseMode = false;

            selectedRock = GameObject.Find("Map").GetComponent<TileMap>().rockPrefabArray[3];
            selectedRockName = GameObject.Find("Map").GetComponent<TileMap>().rockPrefabNameArray[3];
		}
        if (GUI.Button(new Rect(250, 130, 60, 60), rockButtonTextures[4]))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;
            isEraseMode = false;

            selectedRock = GameObject.Find("Map").GetComponent<TileMap>().rockPrefabArray[4];
            selectedRockName = GameObject.Find("Map").GetComponent<TileMap>().rockPrefabNameArray[4];
		}
        if (GUI.Button(new Rect(310, 130, 60, 60), rockButtonTextures[5]))
		{
			isRockBuildMode = true;
			isTreeBuildMode = false;
			isTileBuildMode = false;
            isEraseMode = false;

            selectedRock = GameObject.Find("Map").GetComponent<TileMap>().rockPrefabArray[5];
            selectedRockName = GameObject.Find("Map").GetComponent<TileMap>().rockPrefabNameArray[5];
		}

    }
}
