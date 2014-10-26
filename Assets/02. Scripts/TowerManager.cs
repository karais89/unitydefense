using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour {
	public int sizeX = 64;
	public int sizeY = 64;
	public GameObject tower;
	public bool isTileBuildMode = false;
	public Color highlightColor;
	private Color normalColor;
	private GameObject newTowerToBuild;
    private RaycastHit rayCastHit;
    public static List<GameObject> towerList = new List<GameObject>();
    private int towerCount = 0;
    private bool isVisibleMenu = false;

	// Use this for initialization
	void Awake () {
	
		// normalColor = GameObject.FindWithTag ( "TILE" ).GetComponent<Renderer>().material.color;

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

        // 건설할 타워를 미리 생성해두고 비활성화 시켜둔다
        newTowerToBuild = (GameObject)Instantiate(tower, Vector3.zero, Quaternion.identity);
        Color newColor = Color.green;
        newColor.a = 0.1f;
        newTowerToBuild.renderer.material.color = newColor;

        // 건설할 타워를 타워 관리자의 차일드로 추가
        newTowerToBuild.transform.parent = this.transform;

        newTowerToBuild.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {


		if ( isTileBuildMode == true )
        {
            // FIXIT
			// 마우스 커서를 따라 생성할 타워를 위치시킨다
			Vector3 targetPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			
            targetPosition.z = targetPosition.y;
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
                    // 타일을 선택한 경우에만 타워 생성
                    if ( hitInfo.transform.tag == "TILE" )
                    {
                        Debug.Log("create new tower");

                        GameObject newTower = (GameObject)Instantiate(tower, hitInfo.collider.transform.position, Quaternion.identity);
                        newTower.GetComponent<Tower>().id = towerCount;
                        towerCount++;

                        // 타워를 타워 관리자의 차일드로 추가
                        newTower.transform.parent = this.transform;

                        towerList.Add(newTower);

                        GameObject.Find("GameManager").GetComponent<GameManager>().score += newTower.GetComponent<Tower>().GetEarnScore();

                        isTileBuildMode = false;

                        // GameObject.Find("Map").GetComponent<TileMap>().DisplayGridBuildable(false);
                    }
				}
			}
		}

        else if ( isTileBuildMode == false )
        {
            // 타워를 선택하면 메뉴가 열린다
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo1;
                if (Physics.Raycast(ray1, out hitInfo1, 100.0f))
                {
                    if ( hitInfo1.transform.tag == "TOWER" )
                    {
                        rayCastHit = hitInfo1;

                        isVisibleMenu = true;
                    }
                }
            }
        }


	}

	void OnGUI()
	{
		int buttonWidth = 100;
		int buttonHeight = 30;
		
        // 타워 건설 버튼
		if (GUI.Button (new Rect (Screen.width - buttonWidth, Screen.height - buttonHeight, buttonWidth, buttonHeight), "Tower1 gold 30"))
        {
			isTileBuildMode = true;

            newTowerToBuild.SetActive(true);

            // GameObject.Find("Map").GetComponent<TileMap>().DisplayGridBuildable( true );

            // 골드 차감
            int buyGold = GameObject.Find("Tower(Clone)").GetComponent<Tower>().GetBuyGold();
            GameObject.Find("GameManager").GetComponent<GameManager>().gold -= buyGold;
		}

		
        if ( isVisibleMenu == true )
        {
            // TODO
            // 화면 중앙에 보이는 것은 임의
            int centerX = Screen.width / 2;
            int centerY = Screen.height / 2;

            // 타워 팔기 버튼
            if (GUI.Button(new Rect(centerX, centerY, buttonWidth, buttonHeight), "Sell Tower"))
            {
                isVisibleMenu = false;

                // 골드 증가
                int sellGold = GameObject.Find("Tower(Clone)").GetComponent<Tower>().GetBuyGold();
                GameObject.Find("GameManager").GetComponent<GameManager>().gold += sellGold;

                // Destroy(rayCastHit.collider.gameObject);
                rayCastHit.collider.gameObject.SetActive(false);
            }
            // 타워 업그레이드 버튼
            if (GUI.Button(new Rect(centerX, centerY+buttonHeight, buttonWidth, buttonHeight), "Upgrade Tower"))
            {
                isVisibleMenu = false;

                // 레벨 1 증가
                GameObject.Find("Tower(Clone)").GetComponent<Tower>().level += 1;
                rayCastHit.collider.gameObject.GetComponent<Tower>().Upgrade();

                // TODO
                // 
            }
            // 메뉴 취소 버튼
            if (GUI.Button(new Rect(centerX, centerY + buttonHeight * 2, buttonWidth, buttonHeight), "Cancel"))
            {
                isVisibleMenu = false;
            }
        }
        
	}

}
