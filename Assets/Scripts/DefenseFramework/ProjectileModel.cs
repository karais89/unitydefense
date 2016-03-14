/**
 * @file ProjectileModel.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;

namespace DefenseFramework
{
    public class ProjectileModel : MonoBehaviour
    {
        private int m_iID = 0;
        private static int m_iDamage = 10;
        private float m_fSpeed = 0.5f;

        public int IID
        {
            get
            {
                return m_iID;
            }

            set
            {
                m_iID = value;
            }
        }

        public static int IDamage
        {
            get
            {
                return m_iDamage;
            }

            set
            {
                m_iDamage = value;
            }
        }

        public float FSpeed
        {
            get
            {
                return m_fSpeed;
            }

            set
            {
                m_fSpeed = value;
            }
        }
    }
}