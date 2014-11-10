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
    private GameObject tileLabel;

	// Use this for initialization
	void Awake () {
		// normalColor = renderer.material.color;

        tileLabel = (GameObject)Instantiate(Resources.Load("Prefabs/TileWidget") as GameObject);
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        int iType = (int)type;
        tileLabel.transform.GetChild(0).GetComponent<UILabel>().text = iType.ToString();
	}

    public void SetTileLabelAnchor()
    {
        tileLabel.GetComponent<UIWidget>().SetAnchor(this.transform);

        tileLabel.GetComponent<UIWidget>().topAnchor.SetHorizontal(this.transform, 20);
    }

    public void SetTileLabel( string s )
    {
        tileLabel.transform.GetChild(0).GetComponent<UILabel>().text = s;
    }
}
