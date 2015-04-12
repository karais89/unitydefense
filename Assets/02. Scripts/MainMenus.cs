/**
 * @file MainMenus.cs
 * @brief
 * @details
 * @author rejerkake
 * @date 2014-11-01
 */

using UnityEngine;

public class MainMenus : MonoBehaviour
{
    public GameObject stageSelect;

    public void StageSelectOn()
    {
        stageSelect.SetActive( true );
    }

    public void StageSelectOff()
    {
        stageSelect.SetActive( false );
    }
}