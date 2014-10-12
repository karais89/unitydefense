using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public GameObject gridPrefab;
	public int sizeX = 64;
	public int sizeY = 64;
	private GameObject [,] grid = new GameObject[ 64, 64 ];

	// Use this for initialization
	void Awake () {
	
		for ( int x = 0; x < sizeX; x++ )
		{
			for ( int z = 0; z < sizeY; z++ )
			{
				GameObject gridObject = (GameObject) Instantiate ( gridPrefab );
				gridObject.transform.position = new Vector3( gridObject.transform.position.x + x, 
				                                            gridObject.transform.position.y, gridObject.transform.position.z + z );
				grid[x, z] = gridObject;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
