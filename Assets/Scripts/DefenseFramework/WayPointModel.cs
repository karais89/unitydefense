/**
 * @file WayPointModel.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-15
 */

using UnityEngine;

namespace DefenseFramework
{
    public class WayPointModel : MonoBehaviour
    {

        public enum eWayPointState
        {
            None = 0,
            Entered,
            Escaped
        }


        public enum eWayPointType
        {
            None = 0,
            Start,
            End,
            Goal,
            Return,
            Pass
        }

        private Vector3 m_vPosition;

        private eWayPointType m_eType;

        private eWayPointState m_eState;
    }
}