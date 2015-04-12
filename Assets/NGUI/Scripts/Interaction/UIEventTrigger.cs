//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attaching this script to an object will let you trigger remote functions using NGUI events.
/// </summary>

[AddComponentMenu( "NGUI/Interaction/Event Trigger" )]
public class UIEventTrigger : MonoBehaviour
{
    static public UIEventTrigger current;

    public List<EventDelegate> onHoverOver = new List<EventDelegate>();
    public List<EventDelegate> onHoverOut = new List<EventDelegate>();
    public List<EventDelegate> onPress = new List<EventDelegate>();
    public List<EventDelegate> onRelease = new List<EventDelegate>();
    public List<EventDelegate> onSelect = new List<EventDelegate>();
    public List<EventDelegate> onDeselect = new List<EventDelegate>();
    public List<EventDelegate> onClick = new List<EventDelegate>();
    public List<EventDelegate> onDoubleClick = new List<EventDelegate>();
    public List<EventDelegate> onDragOver = new List<EventDelegate>();
    public List<EventDelegate> onDragOut = new List<EventDelegate>();

    private void OnHover( bool isOver )
    {
        if ( current != null ) return;
        current = this;
        if ( isOver ) EventDelegate.Execute( onHoverOver );
        else EventDelegate.Execute( onHoverOut );
        current = null;
    }

    private void OnPress( bool pressed )
    {
        if ( current != null ) return;
        current = this;
        if ( pressed ) EventDelegate.Execute( onPress );
        else EventDelegate.Execute( onRelease );
        current = null;
    }

    private void OnSelect( bool selected )
    {
        if ( current != null ) return;
        current = this;
        if ( selected ) EventDelegate.Execute( onSelect );
        else EventDelegate.Execute( onDeselect );
        current = null;
    }

    private void OnClick()
    {
        if ( current != null ) return;
        current = this;
        EventDelegate.Execute( onClick );
        current = null;
    }

    private void OnDoubleClick()
    {
        if ( current != null ) return;
        current = this;
        EventDelegate.Execute( onDoubleClick );
        current = null;
    }

    private void OnDragOver( GameObject go )
    {
        if ( current != null ) return;
        current = this;
        EventDelegate.Execute( onDragOver );
        current = null;
    }

    private void OnDragOut( GameObject go )
    {
        if ( current != null ) return;
        current = this;
        EventDelegate.Execute( onDragOut );
        current = null;
    }
}