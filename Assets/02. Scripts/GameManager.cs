﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	// public GameObject waypoint;

	public GameObject monsterPrefab;
	public float createTime = 2.0f;
	private int monsterCount = 0;
	private Transform[] spawnTransform = new Transform[5];
	public static bool isGameOver = false;
	public static List<GameObject> monsterList = new List<GameObject>();
	public static List<GameObject> bulletList = new List<GameObject>();
	private System.Random rand = new System.Random();
    public int gold = 0;
    public GUIText goldText;
    public int score = 0;
    public GUIText scoreText;
    private bool isGamePaused = false;
    private bool isGameFaster = false;


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

            monsterCount++;

			Debug.Log ( "create new monster id = " + monsterCount );
			

			int random = rand.Next(0, 4);
            GameObject newMonster = (GameObject)Instantiate(monsterPrefab, spawnTransform[random].position, Quaternion.identity);
            newMonster.GetComponent<Monster>().id = monsterCount;

            monsterList.Add(newMonster);
		}
	}
	
	// Update is called once per frame
	void Update () {

        goldText.text = "Gold: " + gold;
        scoreText.text = "Score: " + score;
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
        if (GUI.Button(new Rect(Screen.width-60, 10, 60, 30), "Setting"))
        {

        }
    }

}
