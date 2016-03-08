//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright 짤 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Sample script showing how easy it is to implement a standard button that swaps sprites.
/// </summary>

[AddComponentMenu( "NGUI/UI/Image Button" )]
public class UIImageButton : MonoBehaviour
{
    public UISprite target;
    public string normalSprite;
    public string hoverSprite;
    public string pressedSprite;
    public string disabledSprite;
    public bool pixelSnap = true;

    public bool isEnabled
    {
        get
        {
            Collider col = GetComponent<Collider>();
            return col && col.enabled;
        }
        set
        {
            Collider col = GetComponent<Collider>();
            if ( !col ) return;

            if ( col.enabled != value )
            {
                col.enabled = value;
                UpdateImage();
            }
        }
    }

    private void OnEnable()
    {
        if ( target == null ) target = GetComponentInChildren<UISprite>();
        UpdateImage();
    }

    private void OnValidate()
    {
        if ( target != null )
        {
            if ( string.IsNullOrEmpty( normalSprite ) ) normalSprite = target.spriteName;
            if ( string.IsNullOrEmpty( hoverSprite ) ) hoverSprite = target.spriteName;
            if ( string.IsNullOrEmpty( pressedSprite ) ) pressedSprite = target.spriteName;
            if ( string.IsNullOrEmpty( disabledSprite ) ) disabledSprite = target.spriteName;
        }
    }

    private void UpdateImage()
    {
        if ( target != null )
        {
            if ( isEnabled ) SetSprite( UICamera.IsHighlighted( gameObject ) ? hoverSprite : normalSprite );
            else SetSprite( disabledSprite );
        }
    }

    private void OnHover( bool isOver )
    {
        if ( isEnabled && target != null )
            SetSprite( isOver ? hoverSprite : normalSprite );
    }

    private void OnPress( bool pressed )
    {
        if ( pressed ) SetSprite( pressedSprite );
        else UpdateImage();
    }

    private void SetSprite( string sprite )
    {
        if ( target.atlas == null || target.atlas.GetSprite( sprite ) == null ) return;
        target.spriteName = sprite;
        if ( pixelSnap ) target.MakePixelPerfect();
    }
}