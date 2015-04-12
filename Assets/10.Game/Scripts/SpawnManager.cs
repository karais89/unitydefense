/**
 * @file SpawnManager.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-11-12
 */

using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    static private SpawnManager spawnManager;

    // private Transform[] spawnTransform = new Transform[5];
    public List<GameObject> spawnList = new List<GameObject>();

    /*
    private int currentWave = 1;
    public int maxWave = 30;
    public int monsterNumPerWave = 32;
    private bool isWaveEnded = false;
    */

    private void Awake()
    {
        spawnManager = this;
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}