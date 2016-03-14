/**
 * @file CameraControl.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-10-29
 */

using UnityEngine;

namespace DefenseFramework
{
    public class CameraController : MonoBehaviour
    {
        private CameraModel m_cModel;

        private void Awake()
        {
            m_cModel = GetComponent<CameraModel>();
        }

        // Update is called once per frame
        private void Update()
        {
            // 디버그 용으로 마우스 커서가 가리키는 지점에 레이를 녹색 선으로 표시
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

            Debug.DrawRay( ray.origin, ray.direction * 100.0f, Color.green );

            Move();
        }

        private void Move()
        {
            // 마우스 상하좌우 스크롤
            float mousePosX = Input.mousePosition.x;
            float mousePosY = Input.mousePosition.y;

            if ( mousePosX < m_cModel.IScrollDistance )
            {
                transform.Translate( Vector3.right * -m_cModel.FScrollSpeed * Time.deltaTime );
            }

            if ( mousePosX >= Screen.width - m_cModel.IScrollDistance )
            {
                transform.Translate( Vector3.right * m_cModel.FScrollSpeed * Time.deltaTime );
            }

            if ( mousePosY < m_cModel.IScrollDistance )
            {
                transform.Translate( Vector3.up * -m_cModel.FScrollSpeed * Time.deltaTime );
            }

            if ( mousePosY >= Screen.height - m_cModel.IScrollDistance )
            {
                transform.Translate( Vector3.up * m_cModel.FScrollSpeed * Time.deltaTime );
            }

            // 마우스 스크롤 휠로 확대 축소
            if ( Input.GetAxis( "Mouse ScrollWheel" ) > 0 )
            {
                transform.Translate( Vector3.forward * -m_cModel.FZoomSpeed * Time.deltaTime );
            }
            if ( Input.GetAxis( "Mouse ScrollWheel" ) < 0 )
            {
                transform.Translate( Vector3.forward * m_cModel.FZoomSpeed * Time.deltaTime );
            }
        }
    }
}


