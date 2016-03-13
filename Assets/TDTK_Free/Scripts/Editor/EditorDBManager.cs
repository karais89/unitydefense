using UnityEngine;
using UnityEditor;

using System;

using System.Collections;
using System.Collections.Generic;

using TDTK;

namespace TDTK {

	public class EditorDBManager : EditorWindow {

		private static bool init=false;
		public static void Init(){
			if(init) return;
			
			LoadRsc();
			LoadTower();
			LoadCreep();
		}
		
		
		private static ResourceDB rscPrefabDB;
		private static List<Rsc> rscList=new List<Rsc>();
		private static List<int> rscIDList=new List<int>();
		private static void LoadRsc(){ 
			rscPrefabDB=ResourceDB.LoadDB();
			rscList=rscPrefabDB.rscList;
			
			for(int i=0; i<rscList.Count; i++) rscIDList.Add(rscList[i].ID);
		}
		
		
		
		
		
		
		private static TowerDB towerDBPrefab;
		private static List<UnitTower> towerList=new List<UnitTower>();
		private static List<int> towerIDList=new List<int>();
		private static string[] towerNameList=new string[0];
		private static void LoadTower(){
			towerDBPrefab=TowerDB.LoadDB();
			towerList=towerDBPrefab.towerList;
			
			for(int i=0; i<towerList.Count; i++){
				//towerList[i].prefabID=i;
				if(towerList[i]!=null){
					towerIDList.Add(towerList[i].prefabID);
					if(towerList[i].stats.Count==0) towerList[i].stats.Add(new UnitStat());
				}
				else{
					towerList.RemoveAt(i);
					i-=1;
				}
			}
			
			UpdateTowerNameList();
		}
		private static void UpdateTowerNameList(){
			List<string> tempList=new List<string>();
			tempList.Add(" - ");
			for(int i=0; i<towerList.Count; i++){
				string name=towerList[i].unitName;
				while(tempList.Contains(name)) name+=".";
				tempList.Add(name);
			}
			
			towerNameList=new string[tempList.Count];
			for(int i=0; i<tempList.Count; i++) towerNameList[i]=tempList[i];
		}
		
		
		
		private static CreepDB creepDBPrefab;
		private static List<UnitCreep> creepList=new List<UnitCreep>();
		private static List<int> creepIDList=new List<int>();
		private static string[] creepNameList=new string[0];
		private static void LoadCreep(){
			creepDBPrefab=CreepDB.LoadDB();
			creepList=creepDBPrefab.creepList;
			
			for(int i=0; i<creepList.Count; i++){
				//creepList[i].prefabID=i;
				if(creepList[i]!=null){
					creepIDList.Add(creepList[i].prefabID);
					if(creepList[i].stats.Count==0) creepList[i].stats.Add(new UnitStat());
				}
				else{
					creepList.RemoveAt(i);
					i-=1;
				}
			}
			
			UpdateCreepNameList();
		}
		private static void UpdateCreepNameList(){
			List<string> tempList=new List<string>();
			tempList.Add(" - ");
			for(int i=0; i<creepList.Count; i++){
				string name=creepList[i].unitName;
				while(tempList.Contains(name)) name+=".";
				tempList.Add(name);
			}
			
			creepNameList=new string[tempList.Count];
			for(int i=0; i<tempList.Count; i++) creepNameList[i]=tempList[i];
		}
		
		
		
		
		public static List<Rsc> GetRscList(){ return rscList; }
		public static void SetDirtyRsc(){ EditorUtility.SetDirty(rscPrefabDB); }
		
		
		public static List<UnitTower> GetTowerList(){ return towerList; }
		public static string[] GetTowerNameList(){ return towerNameList; }
		public static List<int> GetTowerIDList(){ return towerIDList; }
		public static void SetDirtyTower(){
			EditorUtility.SetDirty(towerDBPrefab);
			for(int i=0; i<towerList.Count; i++) EditorUtility.SetDirty(towerList[i]);
		}
		
		public static List<UnitCreep> GetCreepList(){ return creepList; }
		public static string[] GetCreepNameList(){ return creepNameList; }
		public static List<int> GetCreepIDList(){ return creepIDList; }
		public static void SetDirtyCreep(){
			EditorUtility.SetDirty(creepDBPrefab);
			for(int i=0; i<creepList.Count; i++) EditorUtility.SetDirty(creepList[i]);
		}
		
		
		
		
		
		
		public static void AddNewRsc(){
			Rsc rsc=new Rsc();
			rsc.ID=GenerateNewID(rscIDList);
			rscIDList.Add(rsc.ID);
			rscList.Add(rsc);
			SetDirtyRsc();
			RefreshRsc();
		}
		public static void RemoveRsc(int listID){
			rscIDList.Remove(rscList[listID].ID);
			rscList.RemoveAt(listID);
			SetDirtyRsc();
			RefreshRsc();
		}
		
		
		
		
		
		
		public static int AddNewTower(UnitTower newTower){
			if(towerList.Contains(newTower)) return -1;
			
			int ID=GenerateNewID(towerIDList);
			newTower.prefabID=ID;
			towerIDList.Add(ID);
			towerList.Add(newTower);
			
			UpdateTowerNameList();
			
			if(newTower.stats.Count==0) newTower.stats.Add(new UnitStat());
			
			SetDirtyTower();
			
			return towerList.Count-1;
		}
		public static void RemoveTower(int listID){
			towerIDList.Remove(towerList[listID].prefabID);
			towerList.RemoveAt(listID);
			UpdateTowerNameList();
			SetDirtyTower();
		}
		
		
		
		public static int AddNewCreep(UnitCreep newCreep){
			if(creepList.Contains(newCreep)) return -1;
			
			int ID=GenerateNewID(creepIDList);
			newCreep.prefabID=ID;
			creepIDList.Add(ID);
			creepList.Add(newCreep);
			
			UpdateCreepNameList();
			
			if(newCreep.stats.Count==0) newCreep.stats.Add(new UnitStat());
			
			while(newCreep.valueRscMin.Count<rscList.Count) newCreep.valueRscMin.Add(0);
			while(newCreep.valueRscMax.Count<rscList.Count) newCreep.valueRscMax.Add(0);
			
			SetDirtyCreep();
			
			return creepList.Count-1;
		}
		public static void RemoveCreep(int listID){
			UnitCreep removedCreep=creepList[listID];
			creepIDList.Remove(creepList[listID].prefabID);
			creepList.RemoveAt(listID);
			UpdateCreepNameList();
			SetDirtyCreep();
			RefreshCreep_Remove(removedCreep);
		}
		
		
		
		
		
		
		
		
		private static int GenerateNewID(List<int> list){
			int ID=0;
			while(list.Contains(ID)) ID+=1;
			return ID;
		}
		
		
		
		
		
		private static void RefreshRsc(){
			for(int i=0; i<towerList.Count; i++){
				UnitTower tower=towerList[i];
				for(int n=0; n<tower.stats.Count; n++){
					UnitStat stat=tower.stats[n];
					
					while(stat.cost.Count<rscList.Count) stat.cost.Add(0);
					while(stat.cost.Count>rscList.Count) stat.cost.RemoveAt(stat.cost.Count-1);
					
					while(stat.rscGain.Count<rscList.Count) stat.rscGain.Add(0);
					while(stat.rscGain.Count>rscList.Count) stat.rscGain.RemoveAt(stat.rscGain.Count-1);
				}
			}
			
			for(int i=0; i<creepList.Count; i++){
				UnitCreep creep=creepList[i];
				
				while(creep.valueRscMin.Count<rscList.Count) creep.valueRscMin.Add(0);
				while(creep.valueRscMin.Count>rscList.Count) creep.valueRscMin.RemoveAt(creep.valueRscMin.Count-1);
				
				while(creep.valueRscMax.Count<rscList.Count) creep.valueRscMax.Add(0);
				while(creep.valueRscMax.Count>rscList.Count) creep.valueRscMax.RemoveAt(creep.valueRscMax.Count-1);
			}
		}
		
		private static void RefreshCreep_Remove(UnitCreep creep){
			
		}
		
	}

}