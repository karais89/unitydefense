using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	// public Color highlightColor;
	// private Color normalColor;
    public enum TileType { empty, walkable, obstacle };
    public TileType type = TileType.empty;
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
