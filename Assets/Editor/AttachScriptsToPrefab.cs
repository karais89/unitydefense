/**
 * @file AttachScriptsToPrefab.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using UnityEditor;
using System.Collections;
using DefenseFramework;

public class AttachScriptsToPrefab : Editor
{
    [MenuItem( "Tools/Attach Tile Scripts To Prefab" )]
    public static void AttachTileScriptsToPrefab()
    {
        bool isSelected = false;
        if (null != Selection.activeGameObject)
        {
            isSelected = true;
        }
        if (isSelected == false)
        {
            Debug.LogError( "isSelected = false" );
            return;
        }

        GameObject obj = Selection.activeGameObject;
        obj.AddComponent<TileModel>();
        obj.AddComponent<TileView>();
        obj.AddComponent<TileController>();
    }
}