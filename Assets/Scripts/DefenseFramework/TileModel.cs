/**
 * @file TileModel.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;

namespace DefenseFramework
{
    public class TileModel : MonoBehaviour
    {
        public enum eTileType
        {
            walkable = 0,
            obstacle = 1,
            spawn = 11,
            hero = 111
        };

        private eTileType m_eType = eTileType.walkable;
        private int m_iIndexX = 0;
        private int m_iIndexY = 0;
        private bool m_bHasObstacle = false;

        public eTileType EType
        {
            get
            {
                return m_eType;
            }

            set
            {
                m_eType = value;
            }
        }

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

        public bool BHasObstacle
        {
            get
            {
                return m_bHasObstacle;
            }

            set
            {
                m_bHasObstacle = value;
            }
        }
    }
}