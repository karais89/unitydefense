using UnityEngine;
using System.Collections;

public class MapEditor : MonoBehaviour {

	public int sizeX = 64;
	public int sizeY = 64;

	public GameObject defaultTile;
	private GameObject selectedTile;

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

	// Use this for initialization
	void Awake () {
		// 다수의 디폴트 타일 생성
		for (int y = 0; y < sizeY; y++) 
		{
			for (int x = 0; x < sizeX; x++) 
			{
				Vector3 pos = new Vector3( x, 0, y );
				Quaternion rotation = Quaternion.Euler(90, 0, 0);
				Instantiate ( defaultTile, pos, rotation);
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
					Destroy ( hitInfo.collider.gameObject );

					Debug.Log ( "create new tile" );

					Quaternion rotation = Quaternion.Euler(90, 0, 0);
					Instantiate( selectedTile, hitInfo.collider.transform.position, rotation );
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

	void OnGUI()
	{
		// 타일 버튼
		if (GUI.Button(new Rect(10, 10, 60, 60), tileButtonTexture0))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
			selectedTile = (GameObject) Resources.LoadAssetAtPath ( "Assets/03. Prefabs/PavementTile01.prefab", typeof(GameObject) );
		}
		if (GUI.Button(new Rect(70, 10, 60, 60), tileButtonTexture1))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
			selectedTile = (GameObject) Resources.LoadAssetAtPath ( "Assets/03. Prefabs/PavementTile02.prefab", typeof(GameObject) );
		}
		if (GUI.Button(new Rect(130, 10, 60, 60), tileButtonTexture2))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
		}
		if (GUI.Button(new Rect(190, 10, 60, 60), tileButtonTexture3))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
		}
		if (GUI.Button(new Rect(250, 10, 60, 60), tileButtonTexture4))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
		}
		if (GUI.Button(new Rect(310, 10, 60, 60), tileButtonTexture5))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
		}
		if (GUI.Button(new Rect(370, 10, 60, 60), tileButtonTexture6))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
		}
		if (GUI.Button(new Rect(430, 10, 60, 60), tileButtonTexture7))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
		}
		if (GUI.Button(new Rect(490, 10, 60, 60), tileButtonTexture8))
		{
			isTileBuildMode = true;
			isTreeBuildMode = false;
			isRockBuildMode = false;
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
