/**
 * @file MapEditorScene.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-15
 */

using UnityEngine;
using System.Collections;

namespace MapEditor
{
    public class MapEditorScene : MonoBehaviour
    {
        private MapEditorView m_cView;

        private void Awake()
        {
            m_cView = GetComponent<MapEditorView>();
        }
    }
}
