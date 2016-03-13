
//This is a dummy version for TDTK_Free
//it doesnt really create or manage any pool 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour{

	public static ObjectPoolManager instance;
	
	void Awake(){
		if(instance!=null) return;
		instance=this;
	}
	
	
	public static int New(Transform objT, int count=0){ return 0; }
	public static int New(GameObject obj, int count=0){ return 0; }
	
	public static Transform Spawn(Transform objT){
		return Spawn(objT.gameObject, Vector3.zero, Quaternion.identity).transform;
	}
	public static Transform Spawn(Transform objT, Vector3 pos, Quaternion rot){
		return Spawn(objT.gameObject, pos, rot).transform;
	}
	
	public static GameObject Spawn(GameObject obj){
		return (GameObject)Instantiate(obj);
	}
	public static GameObject Spawn(GameObject obj, Vector3 pos, Quaternion rot){
		return (GameObject)Instantiate(obj, pos, rot);
	}
	
	
	public static void Unspawn(Transform objT){ Unspawn(objT.gameObject); }
	public static void Unspawn(GameObject obj){ Destroy(obj); }
	
	
	public static void Init(){
		if(instance!=null) return;
		
		GameObject obj=new GameObject();
		obj.name="ObjectPoolManager";
		instance=obj.AddComponent<ObjectPoolManager>();
	}
	public static void ClearAll(){
		
	}
	
	
}

