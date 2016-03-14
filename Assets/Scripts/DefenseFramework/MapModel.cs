/**
 * @file MapModel.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;


namespace DefenseFramework
{
    public class MapModel : MonoBehaviour
    {
        private const int m_iSizeX = 64;
        private const int m_iSizeY = 64;
        private int m_iTileWidth = 1;
        private int m_iTileHeight = 1;
        private int[,] m_iMapDataArray = new int[ ISizeX, ISizeY ];
        

        public static int ISizeX
        {
            get
            {
                return m_iSizeX;
            }
        }
        
        public static int ISizeY
        {
            get
            {
                return m_iSizeY;
            }
        }

        public int ITileWidth
        {
            get
            {
                return m_iTileWidth;
            }

            set
            {
                m_iTileWidth = value;
            }
        }

        public int ITileHeight
        {
            get
            {
                return m_iTileHeight;
            }

            set
            {
                m_iTileHeight = value;
            }
        }

        public int[,] IMapDataArray
        {
            get
            {
                return m_iMapDataArray;
            }

            set
            {
                m_iMapDataArray = value;
            }
        }
    }
}