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
    
	//사용안함
	//private bool isVisibleMenu = false;


	///타워UI
	public GameObject TowerUI = null;

	//select된 tower
	private GameObject selectTower = null;


	// Use this for initialization
	void Awake () {

		//최소안보이게
		TowerUI.SetActive (false);





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
        newTowerToBuild.transform.position = new Vector3(99, 0, 99);

        //newTowerToBuild.SetActive(false);
        newTowerToBuild.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {


		if ( isTileBuildMode == true )
        {
            // FIXIT
            /*
			// 마우스 커서를 따라 생성할 타워를 위치시킨다
			Vector3 targetPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			
            targetPosition.z = targetPosition.y;
            targetPosition.y = 0.0f;
			newTowerToBuild.transform.Translate( targetPosition, Space.World );
            */

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

						GameManager.score += newTower.GetComponent<Tower>().GetEarnScore();

                        isTileBuildMode = false;

                        GameObject.Find("Map").GetComponent<TileMap>().DisplayGridBuildable(false);
                        newTower.GetComponent<Tower>().DisplayAttackRangeSphere( false );

                        hitInfo.collider.gameObject.GetComponent<Tile>().type = Tile.TileType.obstacle;
                        hitInfo.collider.gameObject.GetComponent<Tile>().hasObstacle = true;
                        hitInfo.collider.gameObject.GetComponent<Tile>().obstacleName = "Tower";

                        //GameObject.Find("GameManager").GetComponent<GameManager>().ResearchPathUnits();
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
                RaycastHit hitInfo;
                if (Physics.Raycast(ray1, out hitInfo, 100.0f))
                {
                    if ( hitInfo.transform.tag == "TOWER" )
                    {
                        rayCastHit = hitInfo;

                        
						//사용안함
						//대신 selectTower가 눌인지로 판별
						//isVisibleMenu = true;

						if(selectTower != null){
							//범위표시삭제
							selectTower.GetComponent<Tower>().DisplayAttackRangeSphere(false);
						}

						//TowerUi 활성화
						TowerUI.SetActive(true);

						//selectTower 설정
						selectTower = rayCastHit.collider.gameObject;

						//anchor를 선택된 tower로 설정
						TowerUI.GetComponent<UIWidget>().SetAnchor(rayCastHit.collider.gameObject);


						selectTower.GetComponent<Tower>().DisplayAttackRangeSphere(true);
                    }
					/*else
					{   
						//맨땅선택시 ui제거


						//TowerUi활성화
						towerUI.SetActive(false);

						//범위취소
						selectTower.GetComponent<Tower>().DisplayAttackRangeSphere(false);

						//selectTower 비우기
						selectTower = null;
					}*/
                }
            }
        }


	}



	///타일에 건설모드 - UI에서 호출
	public void TileBuildMode(){
		if (isTileBuildMode == false) {
			//건설모드시작
			isTileBuildMode = true;
			GameObject.Find ("Map").GetComponent<TileMap> ().DisplayGridBuildable (true);

			/////////////골드차감구조변경필요할덧

		} else {
			//건설모드종료
			isTileBuildMode = false;
			GameObject.Find ("Map").GetComponent<TileMap> ().DisplayGridBuildable (false);
		}
	}


	//타워UpGrade - UI에서 호출
	public void UpGrade(){
		selectTower.GetComponent<Tower> ().level += 1;
		selectTower.GetComponent<Tower> ().Upgrade ();
		
		selectTower.GetComponent<Tower> ().DisplayAttackRangeSphere (false);
		selectTower = null;
		TowerUI.SetActive(false);
	}

	//타워Sell - UI에서 호출
	public void Sell(){

		// 골드 증가
		int sellGold = rayCastHit.collider.gameObject.GetComponent<Tower>().GetBuyGold();
		GameManager.gold += sellGold;
		
		// Destroy(rayCastHit.collider.gameObject);
		
		//rayCastHit.collider.gameObject.GetComponent<Tile>().type = Tile.TileType.walkable;
		//rayCastHit.collider.gameObject.GetComponent<Tile>().hasObstacle = false;
		//rayCastHit.collider.gameObject.GetComponent<Tile>().obstacleName = "";
		
		// rayCastHit.collider.gameObject.SetActive(false);
		
		Destroy(rayCastHit.collider.gameObject);

		selectTower.GetComponent<Tower> ().DisplayAttackRangeSphere (false);
		selectTower = null;
		TowerUI.SetActive(false);
	}

	//타워Cancel - UI에서 호출
	public void Cancel(){
		
		selectTower.GetComponent<Tower> ().DisplayAttackRangeSphere (false);
		selectTower = null;
		TowerUI.SetActive(false);
	}

	//그리드 단위로 이동 테스트
	/*Vector3 gridSize = new Vector3(1,1,1);
	Vector3 movementDirection  = new Vector3(0,0,1);
	GameObject newTower = null;
	newTower = (GameObject)Instantiate(tower, this.transform.position, Quaternion.identity);
	InvokeRepeating("UpdatePosition", 1.0f, 1.0f);
	void UpdatePosition () {
		Vector3 newPos = newTower.transform.position + movementDirection;
		newPos = new Vector3(Mathf.Round(newPos.x/gridSize.x)*gridSize.x,
		                 Mathf.Round(newPos.y/gridSize.y)*gridSize.y,
		                 Mathf.Round(newPos.z/gridSize.z)*gridSize.z);

		newTower.transform.position = newPos;
	}*/
	//테스트결과 movementDirection인 z가1 즉 위쪽방향을 1초마다 이동하라고명령내린듯
	//1칸씩 위로 계속이동함
	

}
