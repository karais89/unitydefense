using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using TDTK;

namespace TDTK {
	
	public class PlatformTD : MonoBehaviour {
		
		//prefabID of tower available to this platform
		//prior to runtime, this stores the ID of all the unavailable tower on the list, it gets reverse in VerifyTowers (call by BuildManager)
		public List<int> availableTowerIDList=new List<int>();
		
		[HideInInspector] public GameObject thisObj;
		[HideInInspector] public Transform thisT;
		
		public void Init(){
			thisObj=gameObject;
			thisT=transform;
			thisObj.layer=LayerManager.LayerPlatform();
		}
		
		public void VerifyTowers(List<UnitTower> towerList){
			List<int> newList=new List<int>();
			for(int i=0; i<towerList.Count; i++){
				if(!availableTowerIDList.Contains(towerList[i].prefabID)) newList.Add(towerList[i].prefabID);
			}
			availableTowerIDList=newList;
		}
		
	}
	

}