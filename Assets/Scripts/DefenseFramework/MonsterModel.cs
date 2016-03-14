/**
 * @file MonsterModel.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;

namespace DefenseFramework
{
    public class MonsterModel : MonoBehaviour
    {

        public enum eMonsterState
        {
            idle = 0,
            walk,
            attack,
            gothit,
            die
        };

        private int m_iID = 0;
        private float m_fMoveSpeed = 0.3f;
        private float m_fMaxDistance = 1000.0f;
        private Vector3 m_vEndPoint;
        private eMonsterState m_eState = eMonsterState.walk;
        private float m_fHP = 0.0f;
        private const float m_fHPMax = 30.0f;
        private const int m_iEarnGold = 3;
        private const int m_iEarnScore = 10;
        private Vector3 m_vTargetPosition = Vector3.zero;

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

        public float FMoveSpeed
        {
            get
            {
                return m_fMoveSpeed;
            }

            set
            {
                m_fMoveSpeed = value;
            }
        }

        public float FMaxDistance
        {
            get
            {
                return m_fMaxDistance;
            }

            set
            {
                m_fMaxDistance = value;
            }
        }

        public Vector3 VEndPoint
        {
            get
            {
                return m_vEndPoint;
            }

            set
            {
                m_vEndPoint = value;
            }
        }

        public eMonsterState EState
        {
            get
            {
                return m_eState;
            }

            set
            {
                m_eState = value;
            }
        }

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

        public static float FHPMax
        {
            get
            {
                return m_fHPMax;
            }
        }

        public static int IEarnGold
        {
            get
            {
                return m_iEarnGold;
            }
        }

        public static int IEarnScore
        {
            get
            {
                return m_iEarnScore;
            }
        }

        public Vector3 VTargetPosition
        {
            get
            {
                return m_vTargetPosition;
            }

            set
            {
                m_vTargetPosition = value;
            }
        }
    }
}


