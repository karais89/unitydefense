using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using TDTK;

namespace TDTK {
	
	public class PathTD : MonoBehaviour {

		
		public List<Transform> wpList=new List<Transform>();
		
		public bool createPathLine=true;
		
		public float dynamicOffset=1;
		
		public bool loop=false;
		public int loopPoint=0;		
		
		
		public void Init(){
			
			if(loop){
				loopPoint=Mathf.Min(wpList.Count-1, loopPoint); //looping must start 1 waypoint before the destination
			}
			
			//~ if(createPathLine) StartCoroutine(CreatePathLine());
		}
		
		public List<Vector3> GetWaypointList(){
			List<Vector3> list=new List<Vector3>();
			for(int i=0; i<wpList.Count; i++) list.Add(wpList[i].position);
			return list;
		}
		
		public int GetPathWPCount(){ return wpList.Count; }
		public Transform GetSpawnPoint(){ return wpList[0]; }
		
		public int GetLoopPoint(){ return loopPoint; }
		
		
		public float GetPathDistance(int wpID=1){
			if(wpList.Count==0) return 0;
			
			float totalDistance=0;
			
			for(int i=wpID; i<wpList.Count; i++)
				totalDistance+=Vector3.Distance(wpList[i-1].position, wpList[i].position);
			
			return totalDistance;
		}
		
		
		
		
		
		void Start(){
			if(createPathLine) CreatePathLine();
		}
		void CreatePathLine(){
			
			Transform parentT=new GameObject().transform;
			parentT.position=transform.position;
			parentT.parent=transform;
			parentT.gameObject.name="PathLine";
			
			GameObject pathLine=(GameObject)Resources.Load("ScenePrefab/PathLine");
			GameObject pathPoint=(GameObject)Resources.Load("ScenePrefab/PathPoint");
			
			Vector3 startPoint=Vector3.zero;
			Vector3 endPoint=Vector3.zero;
			
			for(int i=0; i<wpList.Count; i++){
				GameObject point=(GameObject)Instantiate(pathPoint, wpList[i].position, Quaternion.identity);
				point.transform.parent=parentT;
				
				endPoint=wpList[i].position;
				
				if(i>0){
					GameObject lineObj=(GameObject)Instantiate(pathLine, startPoint, Quaternion.identity);
					LineRenderer lineRen=lineObj.GetComponent<LineRenderer>();
					lineRen.SetPosition(0, startPoint);
					lineRen.SetPosition(1, endPoint);
					
					lineObj.transform.parent=parentT;
				}
				
				startPoint=wpList[i].position;
			}
		}
		
		
		
		
		
		public bool showGizmo=true;
		public Color gizmoColor=Color.blue;
		void OnDrawGizmos(){
			if(showGizmo){
				Gizmos.color = gizmoColor;
				
				//~ if(Application.isPlaying){
					//~ for(int i=1; i<wpSectionList.Count; i++){
						//~ List<Vector3> subPathO=GetWPSectionPath(i-1);
						//~ List<Vector3> subPath=GetWPSectionPath(i);
						
						//~ //Debug.Log(i+"    "+wpSectionList[i].isPlatform+"    "+subPathO.Count+"   "+subPath.Count);
						
						//~ Gizmos.DrawLine(subPathO[subPathO.Count-1], subPath[0]);
						//~ for(int n=1; n<subPath.Count; n++){
							//~ Gizmos.DrawLine(subPath[n-1], subPath[n]);
						//~ }
					//~ }
				//~ }
				//~ else{
					for(int i=1; i<wpList.Count; i++){
						if(wpList[i-1]!=null && wpList[i]!=null)
							Gizmos.DrawLine(wpList[i-1].position, wpList[i].position);
					}
				//~ }
			}
		}
		
	}
	
}



