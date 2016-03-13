﻿using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections;
using System.Collections.Generic;

using TDTK;

namespace TDTK {

	public class TowerDB : MonoBehaviour {

		public List<UnitTower> towerList=new List<UnitTower>();
	
		public static TowerDB LoadDB(){
			GameObject obj=Resources.Load("DB_TDTK/TowerDB", typeof(GameObject)) as GameObject;
			
			#if UNITY_EDITOR
				if(obj==null) obj=CreatePrefab();
			#endif
			
			return obj.GetComponent<TowerDB>();
		}
		
		public static List<UnitTower> Load(){
			GameObject obj=Resources.Load("DB_TDTK/TowerDB", typeof(GameObject)) as GameObject;
			
			#if UNITY_EDITOR
				if(obj==null) obj=CreatePrefab();
			#endif
			
			TowerDB instance=obj.GetComponent<TowerDB>();
			return instance.towerList;
		}
		
		#if UNITY_EDITOR
			private static GameObject CreatePrefab(){
				GameObject obj=new GameObject();
				obj.AddComponent<TowerDB>();
				GameObject prefab=PrefabUtility.CreatePrefab("Assets/TDTK_Free/Resources/DB_TDTK/TowerDB.prefab", obj, ReplacePrefabOptions.ConnectToPrefab);
				DestroyImmediate(obj);
				AssetDatabase.Refresh ();
				return prefab;
			}
		#endif

	}
	
}
