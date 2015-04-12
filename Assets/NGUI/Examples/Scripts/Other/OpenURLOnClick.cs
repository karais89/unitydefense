using UnityEngine;

public class OpenURLOnClick : MonoBehaviour
{
    private void OnClick()
    {
        UILabel lbl = GetComponent<UILabel>();

        if ( lbl != null )
        {
            string url = lbl.GetUrlAtPosition( UICamera.lastWorldPosition );
            if ( !string.IsNullOrEmpty( url ) ) Application.OpenURL( url );
        }
    }
}