using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Color highlightColor;
	private Color normalColor;
    public enum TileType { empty, walkable, obstacle };
    public TileType type = TileType.empty;
    public int indexX = 0;
    public int indexY = 0;

	// Use this for initialization
	void Awake () {
		normalColor = renderer.material.color;
	}
	
	// Update is called once per frame
	void Update () {
	
		// 마우스 커서가 가리키는 타일에 하이라이트 색상 변경
		/*
		Ray ray = Camera.mainCamera.ScreenPointToRay( Input.mousePosition );
		RaycastHit hitInfo;
		
		if( collider.Raycast( ray, out hitInfo, Mathf.Infinity ) ) {
			renderer.material.color = highlightColor;
		}
		else {
			renderer.material.color = normalColor;
		}
		*/
	}
}
