/**
 * @file WayPointModel.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-15
 */

using UnityEngine;
using System.Collections;

namespace MapEditor
{
    public class MapEditorModel : MonoBehaviour
    {
        public enum eEditorMode
        {
            None = 0,
            Tile,
            Tree,
            Rock,
            Erase,
            Spawn,
            WayPoint
        }

        private eEditorMode m_eMode = eEditorMode.None;

        public eEditorMode EMode
        {
            get
            {
                return m_eMode;
            }

            set
            {
                m_eMode = value;
            }
        }

        private void Awake()
        {
            Debug.Log( "MapEditorModel" );
        }
    }
}