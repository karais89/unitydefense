using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	// public GameObject waypoint;

	public GameObject monsterPrefab;
	public float createTime = 2.0f;
	private int count = 0;
	private Transform[] spawnTransform = new Transform[5];
	public static bool isGameOver = false;
	public static List<GameObject> monsterList = new List<GameObject>();
	public static List<GameObject> bulletList = new List<GameObject>();
	private System.Random rand = new System.Random();
    public int gold = 0;
    public GUIText goldText;
    public int score = 0;
    public GUIText scoreText;


	// Use this for initialization
	void Awake () {
		for ( int i = 0; i < 5; i++ )
		{
			spawnTransform[i] = GameObject.Find( "SpawnPoint0" + i ).GetComponent<Transform>();
		}

		// waypoint에 일정 시간 간격으로 캐릭터 생성
		StartCoroutine( this.CreateMonster () );

        goldText.text = "Gold: " + gold;
        scoreText.text = "Score: " + score;
	
	}

	IEnumerator CreateMonster()
	{
		while ( isGameOver == false )
		{
			yield return new WaitForSeconds( createTime );

			Debug.Log ( "create new monster " + count );
			count++;

			int random = rand.Next(0, 4);
			monsterList.Add( (GameObject)Instantiate( monsterPrefab, spawnTransform[random].position, Quaternion.identity ) );
		}
	}
	
	// Update is called once per frame
	void Update () {
	

	}

    void OnGUI()
    {
        if ( GUI.Button(new Rect(10, Screen.height-30, 60, 30), "Resume" ) )
        {

        }
        if (GUI.Button(new Rect(80, Screen.height-30, 60, 30), "X2"))
        {

        }
    }

}
