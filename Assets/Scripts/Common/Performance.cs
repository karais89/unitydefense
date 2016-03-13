﻿/**
 * @file Performance.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-10-29
 */

using UnityEngine;

public class Performance : MonoBehaviour
{
    private LightShadows shadowType = LightShadows.None;

    private void Awake()
    {
        shadowType = LightShadows.Hard;

        GameObject.Find( "Directional light" ).GetComponent<Light>().shadows = shadowType;
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