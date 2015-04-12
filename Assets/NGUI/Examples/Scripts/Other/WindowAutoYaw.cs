using UnityEngine;

/// <summary>
/// Attaching this script to an object will make it turn as it gets closer to left/right edges of the screen.
/// Look at how it's used in Example 6.
/// </summary>

[AddComponentMenu( "NGUI/Examples/Window Auto-Yaw" )]
public class WindowAutoYaw : MonoBehaviour
{
    public int updateOrder = 0;
    public Camera uiCamera;
    public float yawAmount = 20f;

    private Transform mTrans;

    private void OnDisable()
    {
        mTrans.localRotation = Quaternion.identity;
    }

    private void OnEnable()
    {
        if ( uiCamera == null ) uiCamera = NGUITools.FindCameraForLayer( gameObject.layer );
        mTrans = transform;
    }

    private void Update()
    {
        if ( uiCamera != null )
        {
            Vector3 pos = uiCamera.WorldToViewportPoint( mTrans.position );
            mTrans.localRotation = Quaternion.Euler( 0f, ( pos.x * 2f - 1f ) * yawAmount, 0f );
        }
    }
}