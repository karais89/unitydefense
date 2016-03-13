﻿using UnityEngine;
using UnityEditor;

using System;

using System.Collections;
using System.Collections.Generic;

using TDTK;

namespace TDTK {

	public class UnitEditorWindow : EditorWindow {
		
		
		
		protected static GUIContent cont;
		protected static GUIContent[] contL;
		
		protected static float spaceX=110;
		protected static float spaceY=20;
		protected static float width=150;
		protected static float height=18;
		
		protected static bool shootPointFoldout=false;
		
		
		
		
		public static bool TowerDealDamage(UnitTower tower){
			_TowerType type=tower.type;
			if(type==_TowerType.Turret || type==_TowerType.AOE) return true;
			return false;
		}
		public static bool TowerUseTurret(UnitTower tower){
			_TowerType type=tower.type;
			if(type==_TowerType.Turret) return true;
			return false;
		}
		public static bool TowerTargetHostile(UnitTower tower){
			_TowerType type=tower.type;
			if(type==_TowerType.Turret || type==_TowerType.AOE) return true;
			return false;
		}
		public static bool TowerUseShootObject(UnitTower tower){
			_TowerType type=tower.type;
			if(type==_TowerType.Turret) return true;
			return false;
		}
		public static bool TowerUseShootObjectT(UnitTower tower){
			_TowerType type=tower.type;
			if(type==_TowerType.AOE) return true;
			return false;
		}
		
		
		public static int GetObjectIDFromHList(Transform objT, List<GameObject> objHList){
			if(objT==null) return 0;
			for(int i=1; i<objHList.Count; i++){
				if(objT==objHList[i].transform) return i;
			}
			return 0;
		}
		
		
		
		
		public static Vector3 DrawIconAndName(Unit unit, float startX, float startY){
			float cachedX=startX;
			float cachedY=startY;
			
			EditorUtilities.DrawSprite(new Rect(startX, startY, 60, 60), unit.iconSprite);
			startX+=65;
			
			cont=new GUIContent("Name:", "The unit name to be displayed in game");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			unit.unitName=EditorGUI.TextField(new Rect(startX+spaceX-65, startY, width-5, height), unit.unitName);
			
			cont=new GUIContent("Icon:", "The unit icon to be displayed in game, must be a sprite");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			unit.iconSprite=(Sprite)EditorGUI.ObjectField(new Rect(startX+spaceX-65, startY, width-5, height), unit.iconSprite, typeof(Sprite), false);
			
			startX-=65;
			startY=cachedY;
			startX+=35+spaceX+width;	//startY+=20;
			cont=new GUIContent("HitPoint(HP):", "The unit default's HitPoint.\nThis is the base value to be modified by various in game bonus.");
			EditorGUI.LabelField(new Rect(startX, startY, width, height), cont);
			unit.defaultHP=EditorGUI.FloatField(new Rect(startX+80, startY, 40, height), unit.defaultHP);
			
			cont=new GUIContent(" - Regen:", "HP regeneration rate. The amount of HP to be regenerated per second");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			unit.HPRegenRate=EditorGUI.FloatField(new Rect(startX+80, startY, 40, height), unit.HPRegenRate);
			
			if(unit.HPRegenRate<=0) GUI.color=new Color(0.5f, 0.5f, 0.5f, 1);
			cont=new GUIContent(" - Stagger:", "HP regeneration stagger duration(in second). The duration which the HP regen will be stopped when a unit is hit. Once the duration is passed the HP will start regenerating again");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			unit.HPStaggerDuration=EditorGUI.FloatField(new Rect(startX+80, startY, 40, height), unit.HPStaggerDuration);
			GUI.color=Color.white;
			
			startY=cachedY; 	startX+=145;	
			cont=new GUIContent("Shield:", "The unit default's Shield. Shield can act as a regenerative damage absorber. A unit only be damaged once its shield has been depleted.\nThis is the base value to be modified by various in game bonus.");
			EditorGUI.LabelField(new Rect(startX, startY, width, height), cont);
			unit.defaultShield=EditorGUI.FloatField(new Rect(startX+70, startY, 40, height), unit.defaultShield);
			
			if(unit.defaultShield<=0) GUI.color=new Color(0.5f, 0.5f, 0.5f, 1);
			cont=new GUIContent(" - Regen:", "Shield regeneration rate. The amount of shield to be regenerated per second");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			unit.shieldRegenRate=EditorGUI.FloatField(new Rect(startX+70, startY, 40, height), unit.shieldRegenRate);
			GUI.color=Color.white;
			
			if(unit.defaultShield<=0 || unit.shieldRegenRate<=0) GUI.color=new Color(0.5f, 0.5f, 0.5f, 1);
			cont=new GUIContent(" - Stagger:", "Shield regeneration stagger duration(in second). The duration which the shield regen will be stopped when a unit is hit. Once the duration is passed the shield will start regenerating again");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			unit.shieldStaggerDuration=EditorGUI.FloatField(new Rect(startX+70, startY, 40, height), unit.shieldStaggerDuration);
			
			GUI.color=Color.white;
			
			
			float contentWidth=startX-cachedX+spaceX+25;
			
			//GUI.Box(new Rect(cachedX, cachedY, contentWidth, startY-cachedY+spaceY), "");
			
			return new Vector3(startX, startY+spaceY, contentWidth);
		}
		
		public static Vector3 DrawUnitDefensiveSetting(Unit unit, float startX, float startY, List<GameObject> objHList, string[] objHLabelList){
			//float cachedX=startX;
			//float cachedY=startY;
			
			int objID=GetObjectIDFromHList(unit.targetPoint, objHList);
			cont=new GUIContent("TargetPoint:", "The transform object which indicate the center point of the unit\nThis would be the point where the shootObject and effect will be aiming at");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			objID = EditorGUI.Popup(new Rect(startX+spaceX, startY, width, height), objID, objHLabelList);
			unit.targetPoint = (objHList[objID]==null) ? null : objHList[objID].transform;
			
			cont=new GUIContent("Hit Threshold:", "The range from the targetPoint where a shootObject is considered reached the target");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			unit.hitThreshold=EditorGUI.FloatField(new Rect(startX+spaceX, startY, 40, height), unit.hitThreshold);
			
			cont=new GUIContent("Immuned to Crit:", "Check if the unit is immuned to critical hit");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			unit.immuneToCrit=EditorGUI.Toggle(new Rect(startX+spaceX, startY, 40, height), unit.immuneToCrit);
			
			cont=new GUIContent("Immuned to Slow:", "Check if the unit is immuned to slow");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			unit.immuneToSlow=EditorGUI.Toggle(new Rect(startX+spaceX, startY, 40, height), unit.immuneToSlow);
			
			cont=new GUIContent("Immuned to Stun:", "Check if the unit is immuned to stun");
			EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
			unit.immuneToStun=EditorGUI.Toggle(new Rect(startX+spaceX, startY, 40, height), unit.immuneToStun);
			
			//~ GUI.Box(new Rect(cachedX, cachedY, spaceX+width, startY-cachedY+spaceY), "");
			
			return new Vector3(startX, startY, spaceX+width);
		}
		
		
		
		public static Vector3 DrawUnitOffensiveSetting(UnitTower unit, float startX, float startY, List<GameObject> objHList, string[] objHLabelList){
			return DrawUnitOffensiveSetting(unit, null, startX, startY, objHList, objHLabelList);
		}
		public static Vector3 DrawUnitOffensiveSetting(UnitCreep unit, float startX, float startY, List<GameObject> objHList, string[] objHLabelList){
			return DrawUnitOffensiveSetting(null, unit, startX, startY, objHList, objHLabelList);
		}
		public static Vector3 DrawUnitOffensiveSetting(UnitTower tower, UnitCreep creep, float startX, float startY, List<GameObject> objHList, string[] objHLabelList){
			float cachedX=startX;
			//float cachedY=startY;
			
			Unit unit=null;
			if(tower!=null) unit=tower;
			if(creep!=null) unit=creep;
			
			
			if(tower && TowerUseShootObject(tower)){
				cont=new GUIContent("ShootPoint:", "The transform which indicate the position where the shootObject will be fired from (Optional)\nEach shootPoint assigned will fire a shootObject instance in each attack\nIf left empty, the unit transform itself will be use as the shootPoint\nThe orientation of the shootPoint matter as they dictate the orientation of the shootObject starting orientation.\n");
				shootPointFoldout=EditorGUI.Foldout(new Rect(startX, startY+=spaceY, spaceX, height), shootPointFoldout, cont);
				int shootPointCount=unit.shootPoints.Count;
				shootPointCount=EditorGUI.IntField(new Rect(startX+spaceX, startY, 40, height), shootPointCount);
				
				//if(!EditorGUIUtility.editingTextField && shootPointCount!=unit.shootPoints.Count){
				if(shootPointCount!=unit.shootPoints.Count){
					while(unit.shootPoints.Count<shootPointCount) unit.shootPoints.Add(null);
					while(unit.shootPoints.Count>shootPointCount) unit.shootPoints.RemoveAt(unit.shootPoints.Count-1);
				}
					
				if(shootPointFoldout){
					for(int i=0; i<unit.shootPoints.Count; i++){
						//GetObjectID(unit.shootPoints[i]);
						int objID=GetObjectIDFromHList(unit.shootPoints[i], objHList);
						EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), "    - Element "+(i+1));
						objID = EditorGUI.Popup(new Rect(startX+spaceX, startY, width, height), objID, objHLabelList);
						unit.shootPoints[i] = (objHList[objID]==null) ? null : objHList[objID].transform;
					}
				}
				
				if(unit.shootPoints.Count>1){
					cont=new GUIContent("Shots delay Between ShootPoint:", "Delay in second between shot fired at each shootPoint. When set to zero all shootPoint fire simulteneously");
					EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width+60, height), cont);
					unit.delayBetweenShootPoint=EditorGUI.FloatField(new Rect(startX+spaceX+90, startY-1, 55, height-1), unit.delayBetweenShootPoint);
				}
			}
			
			
			if(tower && TowerUseTurret(tower)){
				//GetObjectID(unit.turretObject);
				int objID=GetObjectIDFromHList(unit.turretObject, objHList);
				cont=new GUIContent("TurretObject:", "The object under unit's hierarchy which is used to aim toward target (Optional). When left unassigned, no aiming will be done.");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				objID = EditorGUI.Popup(new Rect(startX+spaceX, startY, width, height), objID, objHLabelList);
				unit.turretObject = (objHList[objID]==null) ? null : objHList[objID].transform;
				
				//GetObjectID(unit.barrelObject);
				objID=GetObjectIDFromHList(unit.barrelObject, objHList);
				cont=new GUIContent("BarrelObject:", "The object under unit's hierarchy which is used to aim toward target (Optional). This is only required if the unit barrel and turret rotates independently on different axis");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				objID = EditorGUI.Popup(new Rect(startX+spaceX, startY, width, height), objID, objHLabelList);
				unit.barrelObject = (objHList[objID]==null) ? null : objHList[objID].transform;
				
				cont=new GUIContent("Aim Rotate In x-axis:", "Check if the unit turret/barrel can rotate in x-axis (elevation)");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				unit.rotateTurretAimInXAxis=EditorGUI.Toggle(new Rect(startX+spaceX+20, startY, 40, height), unit.rotateTurretAimInXAxis);
			}
			
			
			//~ GUI.Box(new Rect(cachedX, cachedY, spaceX+width, startY-cachedY+spaceY), "");
			
			return new Vector3(cachedX, startY, spaceX+width);
		}
		
		
		
		
		
		
		
		
		public static Vector3 DrawStat(UnitStat stat, float startX, float startY, float statContentHeight, UnitTower tower){
			return DrawStat(stat, startX, startY, statContentHeight, tower, null);
		}
		public static Vector3 DrawStat(UnitStat stat, float startX, float startY, float statContentHeight,  UnitCreep creep){
			return DrawStat(stat, startX, startY, statContentHeight, null, creep);
		}
		public static Vector3 DrawStat(UnitStat stat, float startX, float startY, float statContentHeight, UnitTower tower, UnitCreep creep){
			
			List<Rsc> rscList=EditorDBManager.GetRscList();
			
			float width=150;
			float fWidth=35;
			float spaceX=130;
			float height=18;
			float spaceY=height+2;
			
			//startY-=spaceY;
			
			GUI.Box(new Rect(startX, startY, 220, statContentHeight-startY), "");
			
			startX+=10;	startY+=10;
			
			if(tower!=null){
				
				cont=new GUIContent("Construct Duration:", "The time in second it takes to construct (if this is the first level)/upgrade (if this is not the first level)");
				EditorGUI.LabelField(new Rect(startX, startY, width, height), cont);
				stat.buildDuration=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.buildDuration);
				
				cont=new GUIContent("Deconstruct Duration:", "The time in second it takes to deconstruct if the unit is in this level");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.unBuildDuration=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.unBuildDuration);
			
				
				if(stat.cost.Count!=rscList.Count){
					while(stat.cost.Count>rscList.Count) stat.cost.RemoveAt(stat.cost.Count-1);
					while(stat.cost.Count<rscList.Count) stat.cost.Add(0);
				}
				cont=new GUIContent("Build/Upgrade Cost:", "The resource required to build/upgrade to this level");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				int count=0;	startY+=spaceY; 	float cachedX=startX;
				for(int i=0; i<rscList.Count; i++){
					EditorUtilities.DrawSprite(new Rect(startX+10, startY-1, 20, 20), rscList[i].icon);
					stat.cost[i]=EditorGUI.IntField(new Rect(startX+30, startY, fWidth, height), stat.cost[i]);
					count+=1; 	startX+=65;
					if(count==3){ startY+=spaceY; startX=cachedX; }
				}
				startX=cachedX;	startY+=5;
				
				startY+=spaceY+5;
			}
			
			
			
			if((tower && TowerUseShootObject(tower))){
				cont=new GUIContent("ShootObject:", "The shootObject used by the unit.\nUnit that intended to shoot at the target will not function correctly if this is left unassigned.");
				EditorGUI.LabelField(new Rect(startX, startY, width, height), cont);
				stat.shootObject=(ShootObject)EditorGUI.ObjectField(new Rect(startX+spaceX-50, startY, 4*fWidth-20, height), stat.shootObject, typeof(ShootObject), false);
				startY+=5;
			}
			
			if(tower && TowerUseShootObjectT(tower)){
				cont=new GUIContent("ShootObject:", "The shootObject used by the unit.\nUnit that intended to shoot at the target will not function correctly if this is left unassigned.");
				EditorGUI.LabelField(new Rect(startX, startY, width, height), cont);
				stat.shootObjectT=(Transform)EditorGUI.ObjectField(new Rect(startX+spaceX-50, startY, 4*fWidth-20, height), stat.shootObjectT, typeof(Transform), false);
				startY+=5;
			}
				
			if((tower && TowerDealDamage(tower))){
				cont=new GUIContent("Damage(Min/Max):", "");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.damageMin=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.damageMin);
				stat.damageMax=EditorGUI.FloatField(new Rect(startX+spaceX+fWidth, startY, fWidth, height), stat.damageMax);
				
				cont=new GUIContent("Cooldown:", "Duration between each attack");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.cooldown=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.cooldown);
				
				/*
				cont=new GUIContent("Clip Size:", "The amount of attack the unit can do before the unit needs to reload\nWhen set to <0 the unit will never need any reload");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.clipSize=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.clipSize);
				stat.clipSize=Mathf.Round(stat.clipSize);
				
				
				cont=new GUIContent("Reload Duration:", "");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.reloadDuration=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.reloadDuration);
				*/
				
				
				cont=new GUIContent("Range:", "Effect range of the unit");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.range=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.range);
				
				cont=new GUIContent("AOE Radius:", "Area-of-Effective radius. When the shootObject hits it's target, any other hostile unit within the area from the impact position will suffer the same target as the target.\nSet value to >0 to enable. ");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.aoeRadius=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.aoeRadius);
				
				
				
				cont=new GUIContent("Stun", "");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);	startY-=spaceY;
				
				cont=new GUIContent("        - Chance:", "Chance to stun the target in each successful attack. Takes value from 0-1 with 0 being 0% and 1 being 100%");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.stun.chance=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.stun.chance);
				
				cont=new GUIContent("        - Duration:", "The stun duration in second");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.stun.duration=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.stun.duration);
				
				
				
				cont=new GUIContent("Critical", "");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);	startY-=spaceY;
				
				cont=new GUIContent("            - Chance:", "Chance to score critical hit in attack. Takes value from 0-1 with 0 being 0% and 1 being 100%");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.crit.chance=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.crit.chance);
				
				cont=new GUIContent("            - Multiplier:", "Damage multiplier for successful critical hit. Takes value from 0 and above with with 0.5 being 50% of normal damage as bonus");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.crit.dmgMultiplier=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.crit.dmgMultiplier);
				
				
				
				cont=new GUIContent("Slow", "");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);	startY-=spaceY;
				
				cont=new GUIContent("         - Duration:", "The effect duration in second");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.slow.duration=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.slow.duration);
				
				cont=new GUIContent("         - Multiplier:", "Move speed multiplier. Takes value from 0-1 with with 0.7 being decrese default speed by 30%");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.slow.slowMultiplier=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.slow.slowMultiplier);
				
				
				
				cont=new GUIContent("Dot", "Damage over time");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);	startY-=spaceY;
				
				cont=new GUIContent("        - Duration:", "The effect duration in second");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.dot.duration=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.dot.duration);
				
				cont=new GUIContent("        - Interval:", "Duration between each tick. Damage is applied at each tick.");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.dot.interval=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.dot.interval);
				
				cont=new GUIContent("        - Damage:", "Damage applied at each tick");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.dot.value=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.dot.value);
				
				
			}
			
			
			
			if((tower && tower.type==_TowerType.Support)){
				cont=new GUIContent("Range:", "Effect range of the unit");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.range=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.range);
				startY+=5;
				
				cont=new GUIContent("Buff:", "Note: Buffs from multple tower doesnt stack, however when there's difference in the buff strength, the stronger buff applies. A tower can gain maximum dmage buff from one source and maximum range buff from another");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);	startY-=spaceY;
				
				cont=new GUIContent("        - Damage:", "Damage buff multiplier. Takes value from 0 and above with 0.5 being 50% increase in damage");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.buff.damageBuff=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.buff.damageBuff);
				
				cont=new GUIContent("        - Cooldown:", "Dooldown buff multiplier. Takes value from 0-1 with 0.2 being reduce cooldown by 20%");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.buff.cooldownBuff=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.buff.cooldownBuff);
				
				cont=new GUIContent("        - Range:", "Range buff multiplier. Takes value from 0 and above with 0.5 being 50% increase in range");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.buff.rangeBuff=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.buff.rangeBuff);
				
				cont=new GUIContent("        - Critical:", "Critical hit chance buff modifier. Takes value from 0 and above with 0.25 being 25% increase in critical hit chance");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.buff.criticalBuff=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.buff.criticalBuff);
				
				cont=new GUIContent("        - HP Regen:", "HP Regeneration Buff. Takes value from 0 and above with 2 being gain 2HP second ");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.buff.regenHP=EditorGUI.FloatField(new Rect(startX+spaceX, startY, fWidth, height), stat.buff.regenHP);
			}
			
			
			
			if(tower){
				startY+=10;
				cont=new GUIContent("Custom Description:", "Check to use use custom description. If not, the default one (generated based on the effect) will be used");
				EditorGUI.LabelField(new Rect(startX, startY+=spaceY, width, height), cont);
				stat.useCustomDesp=EditorGUI.Toggle(new Rect(startX+spaceX, startY, 40, height), stat.useCustomDesp);
				if(stat.useCustomDesp){
					GUIStyle style=new GUIStyle("TextArea");
					style.wordWrap=true;
					//~ cont=new GUIContent("Description(to be used in runtime): ", "");
					//~ EditorGUI.LabelField(new Rect(startX, startY+=spaceY, 200, 20), cont);
					stat.desp=EditorGUI.TextArea(new Rect(startX, startY+spaceY-3, 200, 90), stat.desp, style);
					startY+=90;
				}
			}
			
			
			
			
			statContentHeight=startY+spaceY+5;
			
			return new Vector3(startX+220, startY, statContentHeight);
		}
		
		
	}

}