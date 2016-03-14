/**
 * @file SpawnModel.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;

namespace DefenseFramework
{
    public class SpawnModel : MonoBehaviour
    {
        private int m_iIndexX;
        private int m_iIndexY;

        public int IIndexX
        {
            get
            {
                return m_iIndexX;
            }

            set
            {
                m_iIndexX = value;
            }
        }

        public int IIndexY
        {
            get
            {
                return m_iIndexY;
            }

            set
            {
                m_iIndexY = value;
            }
        }
    }
}