/**
 * @file HeroTowerView.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;

namespace GameClient
{
    public class HeroTowerView : MonoBehaviour
    {
        private HeroTowerModel m_cModel;

        private Vector3 m_vPointHP = Vector3.zero;
        private Rect m_rectHP;
        private Texture m_tHP_Empty;
        private Texture m_tHP_Full;

        private void Awake()
        {
            m_cModel = GetComponent<HeroTowerModel>();
            
            m_cModel.FHP = m_cModel.FHP_Max;
        }

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            // HP가 모두 소모되면 게임 오버 처리
            if ( m_cModel.FHP <= 0 )
            {
                GameManager.isGameOver = true;

                Time.timeScale = 0.0f;
            }
        }

        //private void OnGUI()
        //{
        //    // GUI 영웅 타워 HP
        //    Vector3 pointTransform = Vector3.zero;
        //    pointTransform.x = transform.position.x;
        //    pointTransform.y = transform.position.y + 1.5f;
        //    pointTransform.z = transform.position.z;
        //    m_vPointHP = Camera.main.WorldToScreenPoint( pointTransform );
        //    m_rectHP.width = 100;
        //    m_rectHP.height = 10;
        //    m_rectHP.x = m_vPointHP.x - ( m_rectHP.width / 2 );
        //    m_rectHP.y = Screen.height - m_vPointHP.y - ( m_rectHP.height / 2 );

        //    GUI.DrawTexture( m_rectHP, m_tHP_Empty );
        //    GUI.BeginGroup( m_rectHP, "" );
        //    GUI.DrawTexture( new Rect( 0, 0, 100 * ( m_cModel.FHP / m_cModel.FHP_Max ), m_rectHP.height ), m_tHP_Full );
        //    GUI.EndGroup();
        //}
    }
}

