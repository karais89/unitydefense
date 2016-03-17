using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildScrollItem : MonoBehaviour
{
    private Button m_button;
    private Image m_image;

    private void Awake()
    {
        m_button = GetComponent<Button>();
        m_image = GetComponent<Image>();
    }

    private void SetImage()
    {

    }
}
