﻿/**
 * @file Gold.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-10-29
 */

using System.Collections;
using UnityEngine;

public class Gold : MonoBehaviour
{
    /*
     * using UnityEngine;
using System.Collections;

public class CsScore : MonoBehaviour {
    public float ScoreDelay = 0.5f;
    // Use this for initialization
    void Start () {
        StartCoroutine("DisplayScore");
    }

    // Update is called once per frame
    void Update () {
        Vector3 pos = transform.position;
        pos.y += 0.001f;
        transform.position = pos;
    }
    IEnumerator DisplayScore()
    {
        yield return new WaitForSeconds(ScoreDelay);

        for(float a = 1; a >= 0; a -= 0.05f)
        {
            transform.guiText.material.color = new Vector4(1, 1, 1, a);
            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }
}
     */
    public float speed = 0.001f;
    public float displayDelay = 1.0f;
    public bool isVisible = false;

    private void Awake()
    {
        // StartCoroutine("DisplayGold");
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if ( isVisible == true )
        {
            Vector3 pos = transform.position;
            pos.y += speed;
            transform.position = pos;
        }
    }

    /// <summary>
    /// 골드 텍스트 이펙트를 보여주기 시작한다.
    /// </summary>
    public void StartDisplay()
    {
        isVisible = true;
        StartCoroutine( "DisplayGold" );
    }

    /// <summary>
    /// 골드 텍스트 이펙트를 보여준다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator DisplayGold()
    {
        yield return new WaitForSeconds( displayDelay );

        for ( float a = 1; a >= 0; a -= 0.05f )
        {
            transform.guiText.material.color = new Vector4( 1, 1, 1, a );
            yield return new WaitForFixedUpdate();
        }

        isVisible = false;
        Destroy( gameObject );
    }
}