using UnityEngine;
using UnityEditor;

using System;

using System.Collections;

using TDTK;

namespace TDTK {

	[CustomEditor(typeof(UnitCreepAnimation))]
	public class UnitCreepAnimationEditor : Editor {

		
		static private UnitCreepAnimation instance;
		
		private static string[] typeLabel=new string[4];
		private static string[] typeTooltip=new string[4];
		private static bool init=false;
		
		
		private static void Init(){
			init=true;
			
			//public enum _ShootObjectType{Projectile, Missile, Beam, Effect, FPSRaycast, FPSDirect}
			int enumLength = Enum.GetValues(typeof(UnitCreepAnimation._AniType)).Length;
			typeLabel=new string[enumLength];
			typeTooltip=new string[enumLength];
			for(int i=0; i<enumLength; i++){
				typeLabel[i]=((UnitCreepAnimation._AniType)i).ToString();
				if((UnitCreepAnimation._AniType)i==UnitCreepAnimation._AniType.Mecanim) typeTooltip[i]="";
				if((UnitCreepAnimation._AniType)i==UnitCreepAnimation._AniType.Legacy) typeTooltip[i]="";
			}
		}
		
		void Awake(){
			instance = (UnitCreepAnimation)target;
			
			if(!init) Init();
			
			EditorUtility.SetDirty(instance);
		}
		
		
		private static bool showDefaultFlag=false;
		
		private GUIContent cont;
		private GUIContent[] contList;
		
		//private bool showMoveClip=false;
		//private bool showDeadClip=false;
		//private bool showDestClip=false;
		
		public override void OnInspectorGUI(){
			GUI.changed = false;
			
			EditorGUILayout.Space();
			
			int type=(int)instance.type;
			cont=new GUIContent("Type:", "Type of the animation to use");
			contList=new GUIContent[typeLabel.Length];
			for(int i=0; i<contList.Length; i++) contList[i]=new GUIContent(typeLabel[i], typeTooltip[i]);
			type = EditorGUILayout.Popup(cont, type, contList);
			instance.type=(UnitCreepAnimation._AniType)type;
			
			EditorGUILayout.Space();
			
			if(instance.type==UnitCreepAnimation._AniType.Legacy){
				cont=new GUIContent("AniRootObject:", "The Animation component that runs the animation on the unit");
				instance.aniInstance=(Animation)EditorGUILayout.ObjectField(cont, instance.aniInstance, typeof(Animation), true);
				
				cont=new GUIContent("MoveSpeedMultiplier:", "The multiplier used to match the move animation speed to the unit's move speed");
				instance.moveSpeedMultiplier=EditorGUILayout.FloatField(cont, instance.moveSpeedMultiplier);
				
				EditorGUILayout.Space();
				
				
				//~ cont=new GUIContent("Clip Spawn:", "The animation clip to be played when the unit is moving");
				//~ instance.clipSpawn=(AnimationClip)EditorGUILayout.ObjectField(cont, instance.clipSpawn, typeof(AnimationClip), true);
				
				//~ cont=new GUIContent("Clip Move:", "The animation clip to be played when the unit is moving");
				//~ instance.clipMove=(AnimationClip)EditorGUILayout.ObjectField(cont, instance.clipMove, typeof(AnimationClip), true);
				
				//~ cont=new GUIContent("Clip Dead:", "The animation clip to be played when the unit is destroyed");
				//~ instance.clipDead=(AnimationClip)EditorGUILayout.ObjectField(cont, instance.clipDead, typeof(AnimationClip), true);
				
				//~ cont=new GUIContent("Clip Destination:", "The animation clip to be played when the unit reach its destination");
				//~ instance.clipDestination=(AnimationClip)EditorGUILayout.ObjectField(cont, instance.clipDestination, typeof(AnimationClip), true);
				
				
			}
			
			if(instance.type==UnitCreepAnimation._AniType.Mecanim){
				//~ cont=new GUIContent("Max Shoot Elevation:", "The maximum elevation at which the shootObject will be fired. The firing elevation depends on the target distance. The further the target, the higher the elevation. ");
				//~ instance.maxShootAngle=EditorGUILayout.FloatField(cont, instance.maxShootAngle);
				
				//~ cont=new GUIContent("Max Shoot Range:", "The maximum range of the shootObject. This is used to govern the elevation, not the actual range limit. When a target exceed this distance, the shootObject will be fired at the maximum elevation");
				//~ instance.maxShootAngle=EditorGUILayout.FloatField(cont, instance.maxShootAngle);
				
				cont=new GUIContent("Animator:", "The Animator component to be controlled by the script");
				instance.anim=(Animator)EditorGUILayout.ObjectField(cont, instance.anim, typeof(Animator), true);
				
				EditorGUILayout.Space();
				
				//~ cont=new GUIContent("Clip Dead:", "The animation clip to be played by the AnimatorController when the unit is dead. The clip must be similar to the one specified in the AnimatorController. This is required to know the duration of the animation");
				//~ instance.clipDead=(AnimationClip)EditorGUILayout.ObjectField(cont, instance.clipDead, typeof(AnimationClip), true);
				
				//~ cont=new GUIContent("Clip Destination:", "The animation clip to be played by the AnimatorController when the unit reaches destination. The clip must be similar to the one specified in the AnimatorController. This is required to know the duration of the animation");
				//~ instance.clipDestination=(AnimationClip)EditorGUILayout.ObjectField(cont, instance.clipDestination, typeof(AnimationClip), true);
			
			}
			
			cont=new GUIContent("Clip Spawn:", "The animation clip to be played when the unit is moving");
			instance.clipSpawn=(AnimationClip)EditorGUILayout.ObjectField(cont, instance.clipSpawn, typeof(AnimationClip), true);
			
			cont=new GUIContent("Clip Move:", "The animation clip to be played when the unit is moving");
			instance.clipMove=(AnimationClip)EditorGUILayout.ObjectField(cont, instance.clipMove, typeof(AnimationClip), true);
			
			cont=new GUIContent("Clip Dead:", "The animation clip to be played when the unit is destroyed");
			instance.clipDead=(AnimationClip)EditorGUILayout.ObjectField(cont, instance.clipDead, typeof(AnimationClip), true);
			
			cont=new GUIContent("Clip Destination:", "The animation clip to be played when the unit reach its destination");
			instance.clipDestination=(AnimationClip)EditorGUILayout.ObjectField(cont, instance.clipDestination, typeof(AnimationClip), true);
			
			
			EditorGUILayout.Space();
			
			
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.MaxWidth(10));
			showDefaultFlag=EditorGUILayout.Foldout(showDefaultFlag, "Show default editor");
			EditorGUILayout.EndHorizontal();
			if(showDefaultFlag) DrawDefaultInspector();
			
			
			if(GUI.changed) EditorUtility.SetDirty(instance);
			
		}
	}

}