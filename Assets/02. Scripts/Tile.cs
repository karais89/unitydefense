﻿using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	// public Color highlightColor;
	// private Color normalColor;
    public enum TileType { walkable = 0, obstacle = 1, hero = 111 };
    public TileType type = TileType.walkable;
    public int indexX = 0;
    public int indexY = 0;
    public string prefabName = null;
    public bool hasObstacle = false;
    public string obstacleName = null;

	// Use this for initialization
	void Awake () {
		// normalColor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
