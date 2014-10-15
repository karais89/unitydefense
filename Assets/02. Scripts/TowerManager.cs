using UnityEngine;
using System.Collections;

public class TowerManager : MonoBehaviour {
	public int sizeX = 64;
	public int sizeY = 64;
	public GameObject tower;
	public bool isTileBuildMode = false;
	public Color highlightColor;
	private Color normalColor;
	private GameObject newTower;
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
			// 마우스 커서를 따라 생성할 타워를 위치시킨다
			Vector3 targetPosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			targetPosition.y = 0.0f;
			newTower.transform.Translate( targetPosition, Space.World );

            //
            // 타일 단위로 생성할 타워 위치
            /*
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

			/*
			// 마우스 커서가 가리키는 타일에 하이라이트 색상 변경
			Ray ray1 = Camera.mainCamera.ScreenPointToRay( Input.mousePosition );
			RaycastHit hitInfo1;
			
			if( Physics.Raycast( ray1, out hitInfo1, 100.0f ) ) {
				// normalColor = hitInfo1.collider.renderer.material.color;
				hitInfo1.collider.renderer.material.color = highlightColor;
			}
            else {
				hitInfo1.collider.renderer.material.color = normalColor;
			}
			*/


            // 마우스 클릭한 지점에 타워 생성
            if (Input.GetMouseButtonDown (0)) 
			{
				
				Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
				RaycastHit hitInfo;

				if ( Physics.Raycast( ray, out hitInfo, 100.0f ) )
				{
					Debug.Log ( "create new tower" );

					Instantiate( tower, hitInfo.collider.transform.position, Quaternion.identity );
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

			newTower = (GameObject) Instantiate( tower, Vector3.zero, Quaternion.identity );
			Color newColor = Color.green;
			newColor.a = 0.1f;
			newTower.renderer.material.color = newColor;
		}
		
	}

}
