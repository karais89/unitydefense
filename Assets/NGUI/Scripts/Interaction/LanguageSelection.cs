//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Turns the popup list it's attached to into a language selection list.
/// </summary>

[RequireComponent( typeof( UIPopupList ) )]
[AddComponentMenu( "NGUI/Interaction/Language Selection" )]
public class LanguageSelection : MonoBehaviour
{
    private UIPopupList mList;

    private void Start()
    {
        mList = GetComponent<UIPopupList>();

        if ( Localization.knownLanguages != null )
        {
            mList.items.Clear();

            for ( int i = 0, imax = Localization.knownLanguages.Length; i < imax; ++i )
                mList.items.Add( Localization.knownLanguages[i] );

            mList.value = Localization.language;
        }
        EventDelegate.Add( mList.onChange, OnChange );
    }

    private void OnChange()
    {
        Localization.language = UIPopupList.current.value;
    }
}