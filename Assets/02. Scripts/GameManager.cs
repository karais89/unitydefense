using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	// public GameObject waypoint;

	private GameObject monsterPrefab = null;
	public float createTime = 2.0f;
	private int monsterCount = 0;
	private Transform[] spawnTransform = new Transform[5];
	public bool isGameOver = false;
	public static List<GameObject> monsterList = new List<GameObject>();
	// public static List<GameObject> bulletList = new List<GameObject>();


	private System.Random rand = new System.Random();

	///시작소지금액
    public int initialGold = 120;
	///금액
    public int gold = 0;
	///점수
    public int score = 0;

	///금액라벨
	public UILabel goldText = null;
	///웨이브라벨
	public UILabel waveText = null;
	///스피드라벨
	public UILabel speedLabel = null;
	///스톱버튼UI스플라이트
	public UISprite stopUISprite = null;
	///스톱버튼UI버튼
	public UIButton stopUIButton = null;
	///스톱시 팝업창
	public GameObject StopPopup = null;

	///일지정지
    private bool isGamePaused = false;
	///게임속도
	private float GameSpeed = 1.0f;
	///셋팅메뉴
    private bool isVisibleSettingMenu = false;

	///오브젝트풀
    private GameObjectPool pool = new GameObjectPool();
	///오브젝트풀
	public GameObjectPool GetPool()
	{
		return pool;
	}


    private int currentWave = 1;
    public int maxWave = 30;
    public int monsterNumPerWave = 32;
    private bool isWaveEnded = false;


	///속도
	//private bool isGameFaster = false;
	///스코어텍스트
	//public GUIText scoreText = null;


	// Use this for initialization
	void Awake () {

		//팝업창 꺼두기
		StopPopup.SetActive (false);

        GameObject.Find("Map").GetComponent<TileMap>().LoadResources();

        GameObject.Find("Map").GetComponent<TileMap>().LoadMapJSON();

		for ( int i = 0; i < 5; i++ )
		{
			spawnTransform[i] = GameObject.Find( "SpawnPoint0" + i ).GetComponent<Transform>();
		}

		// waypoint에 일정 시간 간격으로 캐릭터 생성
		StartCoroutine( this.CreateMonster () );
        gold = initialGold;
        //없어도 될듯
		//goldText.text = gold.ToString();
        //scoreText.text = "Score: " + score;

        monsterPrefab = (GameObject)Resources.Load("Prefabs/Troll", typeof(GameObject));

        pool.Create(monsterPrefab, monsterNumPerWave);
        pool.SetParent(GameObject.Find("Enemy").transform);
	}


    /// 일정 시간 간격을 두고 몬스터를 생성한다.
	IEnumerator CreateMonster()
	{
		while ( isGameOver == false )
		{
			yield return new WaitForSeconds( createTime );

            monsterCount++;

            if ( monsterCount > monsterNumPerWave )
            {
                monsterCount = 0;
                isWaveEnded = true;
                currentWave++;
				//웨이브 표기
				waveText.text = currentWave + " / " + maxWave;
                yield return null;
            }

			Debug.Log ( "create new monster id = " + monsterCount );
			

			int random = rand.Next(0, 4);
            // GameObject newMonster = (GameObject)Instantiate(monsterPrefab, spawnTransform[random].position, Quaternion.identity);
            GameObject newMonster = pool.NewItem();
            newMonster.transform.position = spawnTransform[random].position;
            newMonster.transform.rotation = Quaternion.identity;
            newMonster.GetComponent<Monster>().id = monsterCount;

            // 생성된 몬스터들은 Enemy를 부모르 둔다
            // newMonster.transform.parent = GameObject.Find("Enemy").transform;

            monsterList.Add(newMonster);
		}
	}
	
	// Update is called once per frame
	void Update () {
		goldText.text = gold.ToString();
	}


	/// 일시 정지 버튼 - ui에서 호출
	public void StopButton()
	{
		if (isGamePaused == false) 
		{
			isGamePaused = true;
			Time.timeScale = 0.0f;
			
			//ngui의 스플라이트를 교체
			stopUISprite.spriteName = "StartButton";
			//ngui의 버튼쪽 평소의 버튼모양을 교체 
			//(노말 롤오버 클릭상태등 거기에 따라 그림이 교체되니 같이처리해야한다.)
			stopUIButton.normalSprite = "StartButton";
			StopPopup.SetActive (true);
		}
		else if (isGamePaused == true) 
		{
			isGamePaused = false;
			Time.timeScale = GameSpeed;//저장된 배속으로 다시시작 
			stopUISprite.spriteName = "StopButton";
			stopUIButton.normalSprite = "StopButton";
		}
	}

	///컨티뉴버튼 - ui에서 호출
	public void ContinueButton()
	{
		StopPopup.SetActive (false);
		isGamePaused = false;
		Time.timeScale = GameSpeed;//저장된 배속으로 다시시작 
		stopUISprite.spriteName = "StopButton";
		stopUIButton.normalSprite = "StopButton";
	}



	/// 속도 버튼 - ui에서 호출
	public void SpeedButton()
	{
		if (isGamePaused == true) //일시정지일경우는 무시
		{
			return;
		} 
		else if (GameSpeed == 1.0f) 
		{
			GameSpeed = 2.0f;
			speedLabel.text = "x4";
		}
		else if (GameSpeed == 2.0f) 
		{
			GameSpeed = 4.0f;
			speedLabel.text = "x1";
		} 
		else if (GameSpeed == 4.0f) 
		{
			GameSpeed = 1.0f;
			speedLabel.text = "x2";
		}
		Time.timeScale = GameSpeed;
	}

	/// 종료 버튼 - ui에서 호출
	public void QuitButton()
	{
		Application.LoadLevel(0);
	}






    /*void OnGUI()
    {
        // 일시 정지 버튼
        if ( GUI.Button(new Rect(10, Screen.height-30, 60, 30), "Pause" ) )
        {
            if ( isGamePaused == false )
            {
                isGamePaused = true;
                Time.timeScale = 0.0f;
            }
            else if ( isGamePaused == true )
            {
                isGamePaused = false;
                if ( isGameFaster == false )
                {
                    Time.timeScale = 1.0f;
                }
                else if ( isGameFaster == true )
                {
                    Time.timeScale = 2.0f;
                }                
            }            
        }

        // 게임 두배 속도 버튼
        if (GUI.Button(new Rect(80, Screen.height-30, 60, 30), "X2"))
        {
            if (isGameFaster == false )
            {
                Time.timeScale = 2.0f;
                isGameFaster = true;
            }
            else if (isGameFaster == true)
            {
                Time.timeScale = 1.0f;
                isGameFaster = false;
            }
        }

        // 설정 버튼
        if (GUI.Button(new Rect(Screen.width-90, 10, 90, 30), "Setting"))
        {
            if (isVisibleSettingMenu == false )
            {
                isVisibleSettingMenu = true;
            }
            else if (isVisibleSettingMenu == true)
            {
                isVisibleSettingMenu = false;
            }
        }

        // 메인 메뉴 씬으로 이동 버튼
        if ( isVisibleSettingMenu == true )
        {
            if (GUI.Button(new Rect(Screen.width - 90, 40, 90, 30), "Main Menu"))
            {
                Application.LoadLevel(0);
            }

            // TODO
            // 사운드 조정 버튼
        }        

    }*/






}
