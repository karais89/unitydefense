/**
 * @file WayPointManager.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-15
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Common;


namespace DefenseFramework
{
    public class WayPointManager : Singleton<WayPointManager>
    {
        private List<GameObject> m_gWayPointList = new List<GameObject>();

        private void Awake()
        {

        }

        public void Add(GameObject _newWayPoint)
        {
            m_gWayPointList.Add( _newWayPoint );
        }

        public void Remove()
        {

        }

        public void Clear()
        {
            m_gWayPointList.ClearEx();
        }
    }
}