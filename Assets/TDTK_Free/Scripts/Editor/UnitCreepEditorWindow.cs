using UnityEngine;
using UnityEditor;

using System;

using System.Collections;
using System.Collections.Generic;

using TDTK;

namespace TDTK {

	public class UnitCreepEditorWindow : UnitEditorWindow {
		
		private static UnitCreepEditorWindow window;
		
		
		public static void Init() {
			 // Get existing open window or if none, make a new one:
			window = (UnitCreepEditorWindow)EditorWindow.GetWindow(typeof (UnitCreepEditorWindow));
			//~ window.minSize=new Vector2(375, 449);
			//~ window.maxSize=new Vector2(375, 800);
			
			EditorDBManager.Init();
			
			InitLabel();
			UpdateObjectHierarchyList();
		}
		
		private static string[] creepTypeLabel;
		private static string[] creepTypeTooltip;
		
		private static string[] animationTypeLabel;
		private static string[] animationTypeTooltip;
		
		private static void InitLabel(){
			int enumLength = Enum.GetValues(typeof(UnitCreepAnimation._AniType)).Length;
			animationTypeLabel=new string[enumLength];
			animationTypeTooltip=new string[enumLength];
			for(int i=0; i<enumLength; i++){
				animationTypeLabel[i]=((UnitCreepAnimation._AniType)i).ToString();
				if((UnitCreepAnimation._AniType)i==UnitCreepAnimation._AniType.None) 		animationTypeTooltip[i]="Disable animation";
				if((UnitCreepAnimation._AniType)i==UnitCreepAnimation._AniType.Mecanim) 	animationTypeTooltip[i]="Use mecanim animation";
				if((UnitCreepAnimation._AniType)i==UnitCreepAnimation._AniType.Legacy) 	animationTypeTooltip[i]="Use legacy animation";
			}
		}
		
		
		
		
		
		private static List<GameObject> objHList=new List<GameObject>();
		private static string[] objHLabelList=new string[0];
		private static void UpdateObjectHierarchyList(){
			List<UnitCreep> creepList=EditorDBManager.GetCreepList();
			if(creepList.Count==0 || selectID>=creepList.Count) return;
			EditorUtilities.GetObjectHierarchyList(creepList[selectID].gameObject, SetObjListCallback);
		}
		public static void SetObjListCallback(List<GameObject> objList, string[] labelList){
			objHList=objList;
			objHLabelList=labelList;
		}
		
		void SelectCreep(int ID){
			selectID=ID;
			UpdateObjectHierarchyList();
			GUI.FocusControl ("");
			
			if(selectID*35<scrollPos1.y) scrollPos1.y=selectID*35;
			if(listVisibleRect.height-35<selectID*35) scrollPos1.y=(selectID+1)*35-listVisibleRect.height+5;
		}
		
		
		
		
		
		private Vector2 scrollPos1;
		private Vector2 scrollPos2;
		
		
		
		void OnGUI () {
			if(window==null) Init();
			
			List<UnitCreep> creepList=EditorDBManager.GetCreepList();
			
			if(GUI.Button(new Rect(window.position.width-120, 5, 100, 25), "Save")) EditorDBManager.SetDirtyCreep();
			
			EditorGUI.LabelField(new Rect(5, 7, 150, 17), "Add new creep:");
			UnitCreep newCreep=null;
			newCreep=(UnitCreep)EditorGUI.ObjectField(new Rect(100, 7, 140, 17), newCreep, typeof(UnitCreep), false);
			if(newCreep!=null){
				int newSelectID=EditorDBManager.AddNewCreep(newCreep);
				if(newSelectID!=-1) SelectCreep(newSelectID);
			}
			
			
			float startX=5;
			float startY=50;	float cachedY=50;
			
			if(minimiseList){
				if(GUI.Button(new Rect(startX, startY-20, 30, 18), ">>")) minimiseList=false;
			}
			else{
				if(GUI.Button(new Rect(startX, startY-20, 30, 18), "<<")) minimiseList=true;
			}
			Vector2 v2=DrawUnitList(startX, startY, creepList); 	startX=v2.x+25;
			
			if(creepList.Count==0) return;
			
			cont=new GUIContent("Creep Prefab:", "The prefab object of the creep\nClick this to highlight it in the ProjectTab");
			EditorGUI.LabelField(new Rect(startX, startY, width, height), cont);
			EditorGUI.ObjectField(new Rect(startX+90, startY, 185, height), creepList[selectID].gameObject, typeof(GameObject), false);
			startY+=spaceY+10;
			
			
			Rect visibleRect=new Rect(startX, startY, window.position.width-startX-10, window.position.height-startY-5);
			Rect contentRect=new Rect(startX, startY, contentWidth, contentHeight);
			
			//~ GUI.color=new Color(.8f, .8f, .8f, 1f);
			//~ GUI.Box(visibleRect, "");
			//~ GUI.color=Color.white;
			
			scrollPos2 = GUI.BeginScrollView(visibleRect, scrollPos2, contentRect);
				
				v3=DrawUnitConfigurator(startX, startY, creepList);
				contentWidth=v3.z;
				contentHeight=v3.y;
				
				startX+=spaceX+width+45;
				startY+=75;
				
				//~ if(creep.type!=_CreepType.Default){
					//~ v3=DrawStat(creep.stats[0], startX, startY, statContentHeight, creepList[selectID]);
					//~ statContentHeight=v3.z;
					//~ if(contentHeight<v3.y) contentHeight=v3.y;
					//~ if(contentWidth<530) contentWidth=530;
				//~ }
				
				contentHeight-=cachedY;
				
			GUI.EndScrollView();
			
			
			if(GUI.changed) EditorDBManager.SetDirtyCreep();
			
		}
		
		
		
		
		private Rect listVisibleRect;
		private Rect listContentRect;
		
		private int deleteID=-1;
		private bool minimiseList=false;
		Vector2 DrawUnitList(float startX, float startY, List<UnitCreep> creepList){
			
			float width=260;
			if(minimiseList) width=60;
			
			
			if(!minimiseList){
				if(GUI.Button(new Rect(startX+180, startY-20, 40, 18), "up")){
					if(selectID>0){
						UnitCreep creep=creepList[selectID];
						creepList[selectID]=creepList[selectID-1];
						creepList[selectID-1]=creep;
						selectID-=1;
						
						if(selectID*35<scrollPos1.y) scrollPos1.y=selectID*35;
					}
				}
				if(GUI.Button(new Rect(startX+222, startY-20, 40, 18), "down")){
					if(selectID<creepList.Count-1){
						UnitCreep creep=creepList[selectID];
						creepList[selectID]=creepList[selectID+1];
						creepList[selectID+1]=creep;
						selectID+=1;
						
						if(listVisibleRect.height-35<selectID*35) scrollPos1.y=(selectID+1)*35-listVisibleRect.height+5;
					}
				}
			}
			
			
			listVisibleRect=new Rect(startX, startY, width+15, window.position.height-startY-5);
			listContentRect=new Rect(startX, startY, width, creepList.Count*40);
			
			GUI.color=new Color(.8f, .8f, .8f, 1f);
			GUI.Box(listVisibleRect, "");
			GUI.color=Color.white;
			
			scrollPos1 = GUI.BeginScrollView(listVisibleRect, scrollPos1, listContentRect);
			
			
				startY+=5;	startX+=5;
			
				for(int i=0; i<creepList.Count; i++){
					
					EditorUtilities.DrawSprite(new Rect(startX, startY+(i*35), 30, 30), creepList[i].iconSprite);
					
					if(minimiseList){
						if(selectID==i) GUI.color = new Color(0, 1f, 1f, 1f);
						if(GUI.Button(new Rect(startX+35, startY+(i*35), 30, 30), "")) SelectCreep(i);
						GUI.color = Color.white;
						
						continue;
					}
					
					
					
					if(selectID==i) GUI.color = new Color(0, 1f, 1f, 1f);
					if(GUI.Button(new Rect(startX+35, startY+(i*35), 150, 30), creepList[i].unitName)) SelectCreep(i);
					GUI.color = Color.white;
					
					if(deleteID==i){
						
						if(GUI.Button(new Rect(startX+190, startY+(i*35), 60, 15), "cancel")) deleteID=-1;
						
						GUI.color = Color.red;
						if(GUI.Button(new Rect(startX+190, startY+(i*35)+15, 60, 15), "confirm")){
							if(selectID>=deleteID) SelectCreep(Mathf.Max(0, selectID-1));
							EditorDBManager.RemoveCreep(deleteID);
							deleteID=-1;
						}
						GUI.color = Color.white;
					}
					else{
						if(GUI.Button(new Rect(startX+190, startY+(i*35), 60, 15), "remove")) deleteID=i;
					}
				}
			
			GUI.EndScrollView();
			
			return new Vector2(startX+width, startY);
		}
		
		
		private static int selectID=0;
		private float contentHeight=0;
		private float contentWidth=0;
		private Vector3 v3;
		
		Vector3 DrawUnitConfigurator(float startX, float startY, List<UnitCreep> creepList){
			UnitCreep creep=creepList[selectID];
			
			float maxWidth=0;
			float cachedY=startY;
			float cachedX=startX;
			startX+=65;	//startY+=20;
			
			startX=cachedX;
			
			v3=DrawIconAndName(creep, startX, startY);	startY=v3.y;	maxWidth=v3.z;
			
			cachedY=startY;
			
			v3=DrawUnitDefensiveSetting(creep, startX, startY, objHList, objHLabelList);	startY=v3.y;	if(maxWidth<v3.z) maxWidth=v3.z;
			
			startY+=20;
			
			
			cont=new GUIContent("Flying:", "Check to mark the creep as flying unit.");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			creep.flying=EditorGUI.Toggle(new Rect(startX+spaceX, startY, 40, height), creep.flying);
			
			cont=new GUIContent("Move Speed:", "Moving speed of the creep");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			creep.moveSpeed=EditorGUI.FloatField(new Rect(startX+spaceX, startY, 40, height), creep.moveSpeed);
			
			cont=new GUIContent("Life Cost:", "The amont of life taken from player when this creep reach it's destination");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			creep.lifeCost=EditorGUI.IntField(new Rect(startX+spaceX, startY, 40, height), creep.lifeCost);
			
			cont=new GUIContent("Life Gain:", "Life awarded to the player when player successfully destroy this creep");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			creep.lifeValue=EditorGUI.IntField(new Rect(startX+spaceX, startY, 40, height), creep.lifeValue);
			
			cont=new GUIContent("Energy Gain:", "Energy awarded to the player when player successfully destroy this creep");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			creep.valueEnergyGain=EditorGUI.IntField(new Rect(startX+spaceX, startY, 40, height), creep.valueEnergyGain);
			
			
			cont=new GUIContent("Resource Gain Upon Destroyed:", "The amont of life taken from player when this creep reach it's destination");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY+5, width+150, height), cont);
			List<Rsc> rscList=EditorDBManager.GetRscList();
			for(int i=0; i<rscList.Count; i++){
				EditorUtilities.DrawSprite(new Rect(startX+25, startY+=spaceY-2, 20, 20), rscList[i].icon);	startY+=2;
				EditorGUI.LabelField(new Rect(startX, startY, width, height), "    -       min/max");//+rscList[i].name);
				creep.valueRscMin[i]=EditorGUI.IntField(new Rect(startX+spaceX, startY, 40, height), creep.valueRscMin[i]);
				creep.valueRscMax[i]=EditorGUI.IntField(new Rect(startX+spaceX+40, startY, 40, height), creep.valueRscMax[i]);
			}
			startY+=5;
			
			
			startY=cachedY+spaceY;	startX+=300;
			
			
			//~ if(creep.type==_CreepType.Offense){
				//~ cont=new GUIContent("Stop To Attack:", "Check to have the creep stop moving when there's target to attack");
				//~ EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				//~ creep.stopToAttack=EditorGUI.Toggle(new Rect(startX+spaceX, startY, 40, height), creep.stopToAttack);
				//~ startY+=spaceY;
			//~ }
			//~ v3=DrawUnitOffensiveSetting(creep, startX, startY, objHList, objHLabelList);		startY=v3.y+20;	if(maxWidth<v3.z) maxWidth=v3.z;
			
			
			//~ if(creep.type==_CreepType.Offense) startY+=30;
			
			UnitCreepAnimation ani=creep.gameObject.GetComponent<UnitCreepAnimation>();
			if(ani==null){
				if(GUI.Button(new Rect(startX, startY, width+50, height+2), "Add animation component"))
					ani=creep.gameObject.AddComponent<UnitCreepAnimation>();
			}
			
			if(ani!=null){
				if(GUI.Button(new Rect(startX, startY, width+50, height+2), "Remove animation component")){
					DestroyImmediate(ani, true);
					//creep.gameObject.GetComponent<UnitCreepAnimation>();
					return new Vector3(startX, startY, maxWidth);
				}
				
				startY+=5;
				
				int type=(int)ani.type;
				cont=new GUIContent("Type:", "Type of the animation to use");
				contL=new GUIContent[animationTypeLabel.Length];
				for(int i=0; i<contL.Length; i++) contL[i]=new GUIContent(animationTypeLabel[i], animationTypeTooltip[i]);
				//cont=new GUIContent("AnimationRootObj:", "The gameObject that contains the Animation component that runs the animation on the unit");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				type = EditorGUI.Popup(new Rect(startX+spaceX, startY, width, height), type, contL);
				ani.type=(UnitCreepAnimation._AniType)type;
				
				if(ani.type==UnitCreepAnimation._AniType.Legacy){
					int objID=GetObjectIDFromHList(ani.aniRootObj!=null ? ani.aniRootObj.transform : null, objHList);
					cont=new GUIContent("AnimationRootObj:", "The gameObject that contains the Animation component that runs the animation on the unit");
					EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
					objID = EditorGUI.Popup(new Rect(startX+spaceX, startY, width, height), objID, objHLabelList);
					ani.aniRootObj = (objHList[objID]==null) ? null : objHList[objID];
					
					
					cont=new GUIContent("Move Speed Multiplier:", "The multiplier used to match the move animation speed to the unit's move speed");
					EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
					ani.moveSpeedMultiplier=EditorGUI.FloatField(new Rect(startX+spaceX+30, startY, 40, height), ani.moveSpeedMultiplier);
					
					
				}
				else if(ani.type==UnitCreepAnimation._AniType.Mecanim){
					int objID=GetObjectIDFromHList(ani.aniRootObj!=null ? ani.aniRootObj.transform : null, objHList);
					cont=new GUIContent("Animator RootObj:", "The gameObject that contains the Animator component that control the animation on the unit");
					EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
					objID = EditorGUI.Popup(new Rect(startX+spaceX, startY, width, height), objID, objHLabelList);
					ani.aniRootObj = (objHList[objID]==null) ? null : objHList[objID];
					
					//~ cont=new GUIContent("Dead Clip:", "The animation clip played by the animator when the creep is destroyed. This is required to know the duration of the animation");
					//~ EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
					//~ ani.clipDead=(AnimationClip)EditorGUI.ObjectField(new Rect(startX+spaceX, startY, width, height), ani.clipDead, typeof(AnimationClip), false);
					
					//~ cont=new GUIContent("Destination Clip:", "The animation clip played by the animator when the creep reach its destination. This is required to know the duration of the animation");
					//~ EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
					//~ ani.clipDestination=(AnimationClip)EditorGUI.ObjectField(new Rect(startX+spaceX, startY, width, height), ani.clipDestination, typeof(AnimationClip), false);
				}
				
				if(ani.type!=UnitCreepAnimation._AniType.None){
					cont=new GUIContent("Spawn Clip:", "The animation clip to be played when the creep is destroyed");
					EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
					ani.clipSpawn=(AnimationClip)EditorGUI.ObjectField(new Rect(startX+spaceX, startY, width, height), ani.clipSpawn, typeof(AnimationClip), false);
					
					cont=new GUIContent("Move Clip:", "The animation clip to be played when the creep is moving");
					EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
					ani.clipMove=(AnimationClip)EditorGUI.ObjectField(new Rect(startX+spaceX, startY, width, height), ani.clipMove, typeof(AnimationClip), false);
					
					cont=new GUIContent("Dead Clip:", "The animation clip to be played when the creep is destroyed");
					EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
					ani.clipDead=(AnimationClip)EditorGUI.ObjectField(new Rect(startX+spaceX, startY, width, height), ani.clipDead, typeof(AnimationClip), false);
					
					cont=new GUIContent("Destination Clip:", "The animation clip to be played when the creep reach its destination");
					EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
					ani.clipDestination=(AnimationClip)EditorGUI.ObjectField(new Rect(startX+spaceX, startY, width, height), ani.clipDestination, typeof(AnimationClip), false);
				}
			}
			
			
			
			//GUI.Box(new Rect(cachedX, cachedY, maxWidth, 10), "");
			
			return new Vector3(startX, startY, maxWidth);
		}
		
		
	}

}