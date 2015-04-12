//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Makes it possible to animate a color of the widget.
/// </summary>

[ExecuteInEditMode]
[RequireComponent( typeof( UIWidget ) )]
public class AnimatedColor : MonoBehaviour
{
    public Color color = Color.white;

    private UIWidget mWidget;

    private void OnEnable()
    {
        mWidget = GetComponent<UIWidget>(); LateUpdate();
    }

    private void LateUpdate()
    {
        mWidget.color = color;
    }
}