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
    /**
     * @class   MapEditorModel
     *
     * @brief   A data Model for the map editor.
     *
     * @author  Ddayin
     * @date    2016-03-17
     */

    public class MapEditorModel : MonoBehaviour
    {
        /**
         * @enum    eBuildMode
         *
         * @brief   Values that represent build modes.
         */

        public enum eBuildMode
        {
            ///< An enum constant representing the none option
            None = 0,
            ///< An enum constant representing the tile option
            Tile,
            ///< An enum constant representing the tree option
            Tree,
            ///< An enum constant representing the rock option
            Rock,
            ///< An enum constant representing the erase option
            Erase,
            ///< An enum constant representing the spawn option
            Spawn,
            ///< An enum constant representing the way point option
            WayPoint
        }

        /** @brief   The mode. */
        private eBuildMode m_eMode = eBuildMode.None;

        /**
         * @property    public eBuildMode EMode
         *
         * @brief   Gets or sets the mode.
         *
         * @return  The e mode.
         */

        public eBuildMode EMode
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
    }
}