using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour {
	public int sizeX = 64;
	public int sizeY = 64;
	public GameObject tower;
	public bool isTileBuildMode = false;
	public Color highlightColor;
	private Color normalColor;
	private GameObject newTowerToBuild;
	private Transform newTowerTransform;
    

	// Use this for initialization
	void Awake () {
	
		normalColor = GameObject.FindWithTag ( "TILE" ).GetComponent<Renderer>().material.color;

		/*
		// 다수의 타워 생성
		for (int y = 0; y < sizeY; y++) 
		{
			for (int x = 0; x < sizeX; x++) 
			{
				Vector3 pos = new Vector3( x, 0.08f, y );
				Instantiate ( tower, pos, Quaternion.identity);
			}
		}
		*/

	}
	
	// Update is called once per frame
	void Update () {


		if ( isTileBuildMode == true )
        {
            // FIXIT
			// 마우스 커서를 따라 생성할 타워를 위치시킨다
			Vector3 targetPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			targetPosition.y = 0.0f;
			newTowerToBuild.transform.Translate( targetPosition, Space.World );

            // TODO
            // 타일 단위로 생성할 타워 위치
            /*
             * 
             * var gridSize : Vector3 = new Vector3(1,1,1);
            var movementDirection : Vector3 = new Vector3(0,0,1);

            function Start () {
             InvokeRepeating("UpdatePosition", 1.0, 1.0);
            }

            function UpdatePosition () {
             var newPos : Vector3 = transform.position+movementDirection;
             newPos = Vector3(Mathf.Round(newPos.x/gridSize.x)*gridSize.x,
             Mathf.Round(newPos.y/gridSize.y)*gridSize.y,
             Mathf.Round(newPos.z/gridSize.z)*gridSize.z);
             transform.position = newPos;
            }
            */

            // FIXIT
			// 마우스 커서가 가리키는 타일에 하이라이트 색상 변경
			/*Ray ray1 = Camera.mainCamera.ScreenPointToRay( Input.mousePosition );
			RaycastHit hitInfo1;
			
			if( Physics.Raycast( ray1, out hitInfo1, 100.0f ) ) {
				// normalColor = hitInfo1.collider.renderer.material.color;
				hitInfo1.collider.renderer.material.color = highlightColor;
			}
            else {
				hitInfo1.collider.renderer.material.color = normalColor;
			}
			*/

            // TODO
            // 마우스 클릭한 지점의 바닥 타일 이차원 배열에서 가져오기


            // 마우스 클릭한 지점에 타워 생성
            if (Input.GetMouseButtonDown (0)) 
			{
				
				Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hitInfo;

				if ( Physics.Raycast( ray, out hitInfo, 100.0f ) )
				{
					Debug.Log ( "create new tower" );

					GameObject newTower = (GameObject) Instantiate( tower, hitInfo.collider.transform.position, Quaternion.identity );

                    // GameObject.Find("GameManager").GetComponent<GameManager>().score += newTower.GetComponent<Tower>().earnScore;
				}

			}
		}


	}

	void OnGUI()
	{
		int buttonWidth = 100;
		int buttonHeight = 30;
		
		if (GUI.Button (new Rect (Screen.width - buttonWidth, Screen.height - buttonHeight, buttonWidth, buttonHeight), "타워1")) {
			isTileBuildMode = true;

			newTowerToBuild = (GameObject) Instantiate( tower, Vector3.zero, Quaternion.identity );
			Color newColor = Color.green;
			newColor.a = 0.1f;
			newTowerToBuild.renderer.material.color = newColor;
		}
		
	}

}
