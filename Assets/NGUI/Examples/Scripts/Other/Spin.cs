using UnityEngine;

/// <summary>
/// Want something to spin? Attach this script to it. Works equally well with rigidbodies as without.
/// </summary>

[AddComponentMenu( "NGUI/Examples/Spin" )]
public class Spin : MonoBehaviour
{
    public Vector3 rotationsPerSecond = new Vector3( 0f, 0.1f, 0f );
    public bool ignoreTimeScale = false;

    private Rigidbody mRb;
    private Transform mTrans;

    private void Start()
    {
        mTrans = transform;
        mRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if ( mRb == null )
        {
            ApplyDelta( ignoreTimeScale ? RealTime.deltaTime : Time.deltaTime );
        }
    }

    private void FixedUpdate()
    {
        if ( mRb != null )
        {
            ApplyDelta( Time.deltaTime );
        }
    }

    public void ApplyDelta( float delta )
    {
        delta *= Mathf.Rad2Deg * Mathf.PI * 2f;
        Quaternion offset = Quaternion.Euler( rotationsPerSecond * delta );

        if ( mRb == null )
        {
            mTrans.rotation = mTrans.rotation * offset;
        }
        else
        {
            mRb.MoveRotation( mRb.rotation * offset );
        }
    }
}