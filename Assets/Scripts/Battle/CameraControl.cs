﻿/**
 * @file CameraControl.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-10-29
 */

using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    public int scrollDistance = 10;
    public float zoomSpeed = 1.1f;
   
    // Use this for initialization
    private void Awake()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // 디버그 용으로 마우스 커서가 가리키는 지점에 레이를 녹색 선으로 표시
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