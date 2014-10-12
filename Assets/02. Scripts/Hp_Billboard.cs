using UnityEngine;
using System.Collections;

public class Hp_Billboard : MonoBehaviour {

	public Transform _TargetCamera;

	void Start () {
		_TargetCamera = Camera.main.gameObject.transform;
	
	}

	void LateUpdate () {
		this.transform.rotation = Quaternion.LookRotation(_TargetCamera.forward , _TargetCamera.up);
		//업데이트에 넣어도 기능은하긴하는데
		//모든스크립트의 업데이트는 자제척인순서로행하다보니 
		//순서에따라서 약간씩케릭터에 엇나가는경우가생길수있어서
		//모든업데이트함수후에 실행되는 이함수를쓰면 최대한부드럽게실행된다고하긔 

	}
}
