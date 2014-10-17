﻿using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	// private Transform transform;
	public float scrollSpeed = 1.0f;
	public int scrollDistance = 10;
	public float zoomSpeed = 10.0f;
	private float zoomInMax = 2.0f;
	private float zoomOutMax = 5.0f;

	// Use this for initialization
	void Awake () {
		// transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

		Debug.DrawRay( ray.origin, ray.direction * 100.0f, Color.green );

		// 마우스 상하좌우 스크롤
		float mousePosX = Input.mousePosition.x;
		float mousePosY = Input.mousePosition.y;

		if ( mousePosX < scrollDistance )
		{
			transform.Translate( Vector3.right * -scrollSpeed * Time.deltaTime );
		}

		if ( mousePosX >= Screen.width - scrollDistance )
		{
			transform.Translate( Vector3.right * scrollSpeed * Time.deltaTime );
		}

		if ( mousePosY < scrollDistance )
		{
			transform.Translate( Vector3.up * -scrollSpeed * Time.deltaTime );
		}

		if ( mousePosY >= Screen.height - scrollDistance )
		{
			transform.Translate( Vector3.up * scrollSpeed * Time.deltaTime );
		}

		// 마우스 스크롤 휠로 확대 축소
		if ( Input.GetAxis( "Mouse ScrollWheel" ) > 0 )
		{
			transform.Translate( Vector3.forward * -zoomSpeed * Time.deltaTime );
		}
		if ( Input.GetAxis( "Mouse ScrollWheel" ) < 0 )
		{
			transform.Translate( Vector3.forward * zoomSpeed * Time.deltaTime );
		}
	}
}