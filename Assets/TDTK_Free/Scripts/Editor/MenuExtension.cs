using UnityEngine;
using UnityEditor;

using System.Collections;

using TDTK;

namespace TDTK {

	public class MenuExtension : EditorWindow {
		
		[MenuItem ("Tools/TDTK_Free/New Scene - Fixed Path", false, -100)]
		private static void NewScene(){
			CreateEmptyScene();
			
			GameObject obj=(GameObject)Instantiate(Resources.Load("ScenePrefab/TDTK_FixedPath", typeof(GameObject)));
			obj.name="TDTK_FixedPath";
			
			SpawnManager spawnManager=(SpawnManager)FindObjectOfType(typeof(SpawnManager));
			if(spawnManager.waveList[0].subWaveList[0].unit==null)
				spawnManager.waveList[0].subWaveList[0].unit=CreepDB.GetFirstPrefab().gameObject;
		}

		static void CreateEmptyScene(){
			EditorApplication.NewScene();
			GameObject camObj=Camera.main.gameObject; 	DestroyImmediate(camObj);
			Light light=GameObject.FindObjectOfType<Light>();
			if(light!=null) DestroyImmediate(light.gameObject);
			
			RenderSettings.skybox=null;
		}
		
		
		
		
		[MenuItem ("Tools/TDTK_Free/CreepEditor", false, 10)]
		static void OpenCreepEditor () {
			UnitCreepEditorWindow.Init();
		}
		
		[MenuItem ("Tools/TDTK_Free/TowerEditor", false, 10)]
		static void OpenTowerEditor () {
			UnitTowerEditorWindow.Init();
		}
		
		[MenuItem ("Tools/TDTK_Free/SpawnEditor", false, 10)]
		static void OpenSpawnEditor () {
			SpawnEditorWindow.Init();
		}
		
		[MenuItem ("Tools/TDTK_Free/ResourceDBEditor", false, 10)]
		public static void OpenResourceEditor () {
			ResourceDBEditor.Init();
		}
		
		
		[MenuItem ("Tools/TDTK_Free/About TDTK-free", false, 100)]
		static void OpenForumLink () {
			AboutWindow.Init();
		}
		
	}


}