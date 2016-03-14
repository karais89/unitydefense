/**
 * @file SpawnView.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;

namespace DefenseFramework
{
    public class SpawnView : MonoBehaviour
    {
        private void Awake()
        {
            Color spawnColor = Color.blue;
            spawnColor.a = 0.5f;
            GetComponent<Renderer>().material.color = spawnColor;
        }
    }
}