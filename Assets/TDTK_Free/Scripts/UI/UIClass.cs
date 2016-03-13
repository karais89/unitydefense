﻿using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;


namespace TDTK {


	[System.Serializable]
	public class UIObject{
		public GameObject rootObj;
		public Transform rootT;
	}


	[System.Serializable]
	public class UIItem{
		public Text label;
		public Image image;
	}

	[System.Serializable]
	public class UnityButton : UIObject{
		public Button button;
		public Text label;
		public Image imageBG;
		public Image imageIcon;
		
		
		public void Init(){
			rootT=rootObj.transform;
			
			button=rootObj.GetComponent<Button>();
			imageBG=rootObj.GetComponent<Image>();
			
			foreach(Transform child in rootT){
				if(child.name=="Image"){
					imageIcon=child.GetComponent<Image>();
				}
				else if(child.name=="Text"){
					label=child.GetComponent<Text>();
				}
			}
		}
		
		public UnityButton Clone(string name, Vector3 posOffset){
			UnityButton newBut=new UnityButton();
			newBut.rootObj=(GameObject)MonoBehaviour.Instantiate(rootObj);
			newBut.rootObj.name=name;//=="" ? srcObj.name+"(Clone)" : name;
			newBut.Init();
			
			newBut.rootT.SetParent(rootT.parent);
			newBut.rootT.localPosition=rootT.localPosition+posOffset;
			newBut.rootT.localScale=new Vector3(1, 1, 1);
			
			return newBut;
		}
	}



	[System.Serializable]
	public class UnitOverlay : UIObject{
		public Slider barHP;
		public Slider barShield;
		
		public void Init(){
			rootT=rootObj.transform;
			
			foreach(Transform child in rootT){
				if(child.name=="ShieldBar"){
					barShield=child.GetComponent<Slider>();
				}
				else if(child.name=="HPBar"){
					barHP=child.GetComponent<Slider>();
				}
			}
		}
		
		public UnitOverlay Clone(string name=""){
			UnitOverlay newOverlay=new UnitOverlay();
			newOverlay.rootObj=(GameObject)MonoBehaviour.Instantiate(rootObj);
			newOverlay.rootObj.name=name=="" ? rootObj.name+"(Clone)" : name;
			newOverlay.Init();
			
			newOverlay.rootT.SetParent(rootT.parent);
			newOverlay.rootT.localScale=rootT.localScale;
			
			return newOverlay;
		}
	}



	//~ public class Tween : MonoBehaviour{
		//~ public static void Pos(GameObject obj, float duration, Vector3 targetPos){
			
		//~ }
	//~ }
		
}