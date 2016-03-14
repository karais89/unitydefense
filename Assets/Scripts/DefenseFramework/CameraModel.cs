/**
 * @file CameraModel.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;

namespace DefenseFramework
{
    public class CameraModel : MonoBehaviour
    {
        private float m_fScrollSpeed = 1.0f;
        private int m_iScrollDistance = 10;
        private float m_fZoomSpeed = 1.1f;

        public float FScrollSpeed
        {
            get
            {
                return m_fScrollSpeed;
            }

            set
            {
                m_fScrollSpeed = value;
            }
        }

        public int IScrollDistance
        {
            get
            {
                return m_iScrollDistance;
            }

            set
            {
                m_iScrollDistance = value;
            }
        }

        public float FZoomSpeed
        {
            get
            {
                return m_fZoomSpeed;
            }

            set
            {
                m_fZoomSpeed = value;
            }
        }
    }
}