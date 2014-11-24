using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour {

    static private SpawnManager spawnManager;

    // private Transform[] spawnTransform = new Transform[5];
    public List<GameObject> spawnList = new List<GameObject>();
    /*
    private int currentWave = 1;
    public int maxWave = 30;
    public int monsterNumPerWave = 32;
    private bool isWaveEnded = false;
    */

    void Awake()
    {
        spawnManager = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
