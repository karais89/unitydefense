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
    public int initialGold = 120;
    public int gold = 0;
    public GUIText goldText = null;
    public int score = 0;
    public GUIText scoreText = null;
    public GUIText waveText = null;
    private bool isGamePaused = false;
    private bool isGameFaster = false;
    private bool isVisibleSettingMenu = false;
    private GameObjectPool pool = new GameObjectPool();
    private int currentWave = 0;
    public int maxWave = 30;
    public int monsterNumPerWave = 32;
    private bool isWaveEnded = false;

    public GameObjectPool GetPool()
    {
        return pool;
    }


	// Use this for initialization
	void Awake () {

        GameObject.Find("Map").GetComponent<TileMap>().LoadResources();

        GameObject.Find("Map").GetComponent<TileMap>().LoadMapJSON();

		for ( int i = 0; i < 5; i++ )
		{
			spawnTransform[i] = GameObject.Find( "SpawnPoint0" + i ).GetComponent<Transform>();
		}

		// waypoint에 일정 시간 간격으로 캐릭터 생성
		StartCoroutine( this.CreateMonster () );
        gold = initialGold;
        goldText.text = "Gold: " + gold;
        scoreText.text = "Score: " + score;

        monsterPrefab = (GameObject)Resources.Load("Prefabs/Troll", typeof(GameObject));

        pool.Create(monsterPrefab, monsterNumPerWave);
        pool.SetParent(GameObject.Find("Enemy").transform);
	}


    /// <summary>
    /// 일정 시간 간격을 두고 몬스터를 생성한다.
    /// </summary>
    /// <returns></returns>
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

        goldText.text = "Gold: " + gold;
        scoreText.text = "Score: " + score;
        waveText.text = "Wave " + currentWave + " / " + maxWave;

	}

    void OnGUI()
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

    }

}
