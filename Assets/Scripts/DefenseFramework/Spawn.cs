/**
 * @file Spawn.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-11-12
 */

using UnityEngine;

namespace DefenseFramework
{
    public class Spawn : MonoBehaviour
    {
        public int indexX;
        public int indexY;

        private void Awake()
        {
            Color spawnColor = Color.blue;
            spawnColor.a = 0.5f;
            GetComponent<Renderer>().material.color = spawnColor;
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
}