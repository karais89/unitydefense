using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;

using TDTK;

namespace TDTK {

	//[CustomEditor(typeof(UnitTower))]
	public class UnitTowerEditor : Editor {

		//private static UnitTower instance;
		
		void Awake(){
			//instance = (UnitTower)target;
			
			//EditorDBManager.Init();
			
			//EditorUtility.SetDirty(instance);
		}
		
		/*
		public override void OnInspectorGUI(){
			
			GUI.changed = false;
			
			List<Rsc> rscList=EditorDBManager.GetRscList();
			for(int n=0; n<instance.stats.Count; n++){
				UnitStat stat=instance.stats[n];
				
				if(stat.cost.Count!=rscList.Count) EditorUtility.SetDirty(instance);
				while(stat.cost.Count<rscList.Count) stat.cost.Add(0);
				while(stat.cost.Count>rscList.Count) stat.cost.RemoveAt(stat.cost.Count-1);
				
				if(stat.rscGain.Count!=rscList.Count) EditorUtility.SetDirty(instance);
				while(stat.rscGain.Count<rscList.Count) stat.rscGain.Add(0);
				while(stat.rscGain.Count>rscList.Count) stat.rscGain.RemoveAt(stat.rscGain.Count-1);
			}
			
			DrawDefaultInspector();
			
			if(GUI.changed) EditorUtility.SetDirty(instance);
			
		}
		*/
		
		
		
		
		
	}

}