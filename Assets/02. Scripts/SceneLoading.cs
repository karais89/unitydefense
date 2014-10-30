using UnityEngine;
using System.Collections;

public class SceneLoading : MonoBehaviour {

	private UISlider _loadingBar;
	public GameObject SceneLoadding;
	public UILabel Persent_Label;
	public UILabel Tip_Label1;
	public UILabel Tip_Label2;
	public UILabel Tip_Label3;


	float	progressRounded;


	private string[,] _tip = {
		{"메인영웅이 죽으면 패배합니다."," "," "},//\r\n
		{"배가고픕니다.","무지","몹시도"},
		{"유니티는 암덩어리입니다","진리입니다"," "},
		{"아 힘들다 짱난다"," ","그러하다."},
		{"늅늅","찡찡","티모찡~"},

	};


	///start버튼에서 호출하도록 설계
	/// 씬을로드한다.
	public void StartLoad()	{
		//신로딩꺼져있는걸 켜준다.
		SceneLoadding.SetActive (true);

		
		_loadingBar = GameObject.Find ("ProgressBar").GetComponent<UISlider> ();

		//tip 렌덤으로 설정
		int index = Random.Range (0, _tip.GetLength (0));
		Tip_Label1.text = _tip[index ,0];
		Tip_Label2.text = _tip[index ,1];
		Tip_Label3.text = _tip[index ,2];

		//로딩코루틴
		StartCoroutine(load());

	}
	public AsyncOperation async = null;
	IEnumerator load(){
		//Application.backgroundLoadingPriority = ThreadPriority.Low;
		async = Application.LoadLevelAsync ("scPlay");
		async.allowSceneActivation = false;//로딩완료후 자동으로넘어가지않도록한다.
		while(!async.isDone){
			float progress = async.progress * 100.0f;
			progressRounded = Mathf.RoundToInt(progress);
			if (async.progress >= 0.9f) {//async.progress는 0.9이상은표현못한다.
				_loadingBar.value = 1.0f;
				Persent_Label.text = "100";
				//yield return new WaitForSeconds(0.1F);//로딩완료시 0.1초 쉬어주고 씬넘긴다.
				//윗부분 현재버그라서 뺌 
				async.allowSceneActivation = true;
				yield return null;
			} else {
				_loadingBar.value = progressRounded * 0.01f;
				Persent_Label.text = progressRounded.ToString();
				yield return null;
			}
		}
		yield return null;
	}


	void UpDate(){

	}





}
