/**
 * @file HeroTowerModel.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;

namespace GameClient
{
    public class HeroTowerModel : MonoBehaviour
    {
        private float m_fHP = 0.0f;     // 현재 체력
        private float m_fHP_Max = 200.0f;

        public float FHP
        {
            get
            {
                return m_fHP;
            }

            set
            {
                m_fHP = value;
            }
        }

        public float FHP_Max
        {
            get
            {
                return m_fHP_Max;
            }

            set
            {
                m_fHP_Max = value;
            }
        }
    }
}