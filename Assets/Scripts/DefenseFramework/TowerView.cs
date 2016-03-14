/**
 * @file TowerView.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;

namespace DefenseFramework
{
    public class TowerView : MonoBehaviour
    {
        private GameObject m_gAttackRangeSphere = null;

        public GameObject GAttackRangeSphere
        {
            get
            {
                return m_gAttackRangeSphere;
            }

            set
            {
                m_gAttackRangeSphere = value;
            }
        }
        
        // Use this for initialization
        private void Awake()
        {
            m_gAttackRangeSphere = transform.FindChild( "AttackRangeSphere" ).gameObject;

            // 공격 범위 표시
            m_gAttackRangeSphere.GetComponent<Renderer>().enabled = false;
            Color rangeColor = Color.green;
            rangeColor.a = 0.3f;
            m_gAttackRangeSphere.GetComponent<Renderer>().material.color = rangeColor;
        }
        
        public void DisplayAttackRangeSphere( bool visible )
        {
            if ( visible == true )
            {
                m_gAttackRangeSphere.GetComponent<Renderer>().enabled = true;
            }
            else if ( visible == false )
            {
                m_gAttackRangeSphere.GetComponent<Renderer>().enabled = false;
            }
        }

    }
}