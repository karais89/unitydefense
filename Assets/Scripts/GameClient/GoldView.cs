/**
 * @file GoldModel.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-15
 */
 
 using UnityEngine;
using System.Collections;

namespace GameClient
{
    public class GoldView : MonoBehaviour
    {
        private float m_fSpeed = 0.001f;
        private float m_fDisplayDelay = 1.0f;
        private bool m_bIsVisible = false;

        // Update is called once per frame
        private void Update()
        {
            if ( m_bIsVisible == true )
            {
                Vector3 pos = transform.position;
                pos.y += m_fSpeed;
                transform.position = pos;
            }
        }

        /// <summary>
        /// 골드 텍스트 이펙트를 보여주기 시작한다.
        /// </summary>
        public void StartDisplay()
        {
            m_bIsVisible = true;
            StartCoroutine( "DisplayGold" );
        }

        /// <summary>
        /// 골드 텍스트 이펙트를 보여준다.
        /// </summary>
        /// <returns></returns>
        private IEnumerator DisplayGold()
        {
            yield return new WaitForSeconds( m_fDisplayDelay );

            for ( float a = 1; a >= 0; a -= 0.05f )
            {
                transform.GetComponent<GUIText>().material.color = new Vector4( 1, 1, 1, a );
                yield return new WaitForFixedUpdate();
            }

            m_bIsVisible = false;
            Destroy( gameObject );
        }
    }
}