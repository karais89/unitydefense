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
    public List<GameObject> spawnList = new List<GameObject>();
    
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