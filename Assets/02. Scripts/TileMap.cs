using UnityEngine;
using System.Collections;

public class TileMap : MonoBehaviour {
	public int sizeX = 64;
	public int sizeY = 64;
	public int tileWidth = 1;
	public int tileHeight = 1;
	private int mapWidth = 0;
	private int mapHeight = 0;
	public GameObject testTile;
    public GameObject[,] tileArray = new GameObject[64, 64];

	void Awake() {

		CreateTestMap ();
	}

	void CreateTestMap()
	{
		// 다수의 타일 생성
		for (int y = 0; y < sizeY; y++) 
		{
			for (int x = 0; x < sizeX; x++) 
			{
				Vector3 pos = new Vector3( x, 0, y );
				Quaternion rotation = Quaternion.Euler(90, 0, 0);
				GameObject newTile = (GameObject) Instantiate ( testTile, pos, rotation);
                tileArray[x, y] = newTile;
                // Map 게임오브젝트를 부모로 둔다
                newTile.transform.parent = GameObject.Find("Map").transform;                
			}
		}
	}


	// Use this for initialization
	void OnDrawGizmos () {
		mapWidth = sizeX * tileWidth;
		mapHeight = sizeY * tileHeight;

		// draw layer border
		Gizmos.color = Color.white;
		Gizmos.DrawLine(Vector3.zero, new Vector3(mapWidth, 0, 0));
		Gizmos.DrawLine(Vector3.zero, new Vector3(0, 0, mapHeight));
		Gizmos.DrawLine(new Vector3(mapWidth, 0, 0), new Vector3(mapWidth, 0, mapHeight));
		Gizmos.DrawLine(new Vector3(0, 0, mapHeight), new Vector3(mapWidth, 0, mapHeight));
	
		// draw tile cells
		Gizmos.color = Color.green;
		for (int i = 1; i < sizeX; i++)
		{
			Gizmos.DrawLine(new Vector3(i * tileWidth, 0, 0), new Vector3(i * tileWidth, 0, mapHeight));
		}
		
		for (int i = 1; i < sizeY; i++)
		{
			Gizmos.DrawLine(new Vector3(0, 0, i * tileHeight), new Vector3(mapWidth, 0, i * tileHeight));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
