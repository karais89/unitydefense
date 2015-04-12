//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using System.Collections;
using UnityEngine;

/// <summary>
/// Simple script that shows how to download a remote texture and assign it to be used by a UITexture.
/// </summary>

[RequireComponent( typeof( UITexture ) )]
public class DownloadTexture : MonoBehaviour
{
    public string url = "http://www.yourwebsite.com/logo.png";

    private Texture2D mTex;

    private IEnumerator Start()
    {
        WWW www = new WWW( url );
        yield return www;
        mTex = www.texture;

        if ( mTex != null )
        {
            UITexture ut = GetComponent<UITexture>();
            ut.mainTexture = mTex;
            ut.MakePixelPerfect();
        }
        www.Dispose();
    }

    private void OnDestroy()
    {
        if ( mTex != null ) Destroy( mTex );
    }
}