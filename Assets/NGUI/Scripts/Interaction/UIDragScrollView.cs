//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Allows dragging of the specified scroll view by mouse or touch.
/// </summary>

[AddComponentMenu( "NGUI/Interaction/Drag Scroll View" )]
public class UIDragScrollView : MonoBehaviour
{
    /// <summary>
    /// Reference to the scroll view that will be dragged by the script.
    /// </summary>

    public UIScrollView scrollView;

    // Legacy functionality, kept for backwards compatibility. Use 'scrollView' instead.
    [HideInInspector]
    [SerializeField]
    private UIScrollView draggablePanel;

    private Transform mTrans;
    private UIScrollView mScroll;
    private bool mAutoFind = false;
    private bool mStarted = false;

    /// <summary>
    /// Automatically find the scroll view if possible.
    /// </summary>

    private void OnEnable()
    {
        mTrans = transform;

        // Auto-upgrade
        if ( scrollView == null && draggablePanel != null )
        {
            scrollView = draggablePanel;
            draggablePanel = null;
        }

        if ( mStarted && ( mAutoFind || mScroll == null ) )
            FindScrollView();
    }

    /// <summary>
    /// Find the scroll view.
    /// </summary>

    private void Start()
    {
        mStarted = true;
        FindScrollView();
    }

    /// <summary>
    /// Find the scroll view to work with.
    /// </summary>

    private void FindScrollView()
    {
        // If the scroll view is on a parent, don't try to remember it (as we want it to be dynamic in case of re-parenting)
        UIScrollView sv = NGUITools.FindInParents<UIScrollView>( mTrans );

        if ( scrollView == null )
        {
            scrollView = sv;
            mAutoFind = true;
        }
        else if ( scrollView == sv )
        {
            mAutoFind = true;
        }
        mScroll = scrollView;
    }

    /// <summary>
    /// Create a plane on which we will be performing the dragging.
    /// </summary>

    private void OnPress( bool pressed )
    {
        // If the scroll view has been set manually, don't try to find it again
        if ( mAutoFind && mScroll != scrollView )
        {
            mScroll = scrollView;
            mAutoFind = false;
        }

        if ( scrollView && enabled && NGUITools.GetActive( gameObject ) )
        {
            scrollView.Press( pressed );

            if ( !pressed && mAutoFind )
            {
                scrollView = NGUITools.FindInParents<UIScrollView>( mTrans );
                mScroll = scrollView;
            }
        }
    }

    /// <summary>
    /// Drag the object along the plane.
    /// </summary>

    private void OnDrag( Vector2 delta )
    {
        if ( scrollView && NGUITools.GetActive( this ) )
            scrollView.Drag();
    }

    /// <summary>
    /// If the object should support the scroll wheel, do it.
    /// </summary>

    private void OnScroll( float delta )
    {
        if ( scrollView && NGUITools.GetActive( this ) )
            scrollView.Scroll( delta );
    }
}