using UnityEngine;
using System.Collections;

public class MainMenus : MonoBehaviour {

	public GameObject stageSelect;

	public void StageSelectOn(){
		stageSelect.SetActive (true);
	}

	public void StageSelectOff(){
		stageSelect.SetActive (false);
	}
}
