using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        // 플레이 버튼을 누르면 플레이 씬으로 넘어간다
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height - 100, 60, 30), "Play"))
        {
            Application.LoadLevel(1);
        }
    }
}
