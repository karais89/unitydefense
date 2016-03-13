using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;

using TDTK;

namespace TDTK {

	public class UIHUD : MonoBehaviour {

		public Text txtLife;
		public Text txtWave;
		
		public Text txtTimer;
		public UnityButton buttonSpawn;
		
		public List<UnityButton> rscObjList=new List<UnityButton>();
		
		
		// Use this for initialization
		void Start () {
			buttonSpawn.Init();
			
			txtTimer.text="";
			
			List<Rsc> rscList=ResourceManager.GetResourceList();
			for(int i=0; i<rscList.Count; i++){
				if(i==0) rscObjList[i].Init();
				else rscObjList.Add(rscObjList[0].Clone("RscObj"+i, new Vector3(i*90, 0, 0)));
				rscObjList[i].imageIcon.sprite=rscList[i].icon;
			}
			
			OnLife(0);
			OnNewWave(1);
			OnResourceChanged(new List<int>());
			
			if(SpawnManager.AutoStart()){
				buttonSpawn.rootObj.SetActive(false);
				//StartCoroutine(AutoStartTimer());
				OnSpawnTimer(SpawnManager.GetAutoStartDelay());
			}
		}
		
		void OnEnable(){
			GameControl.onLifeE += OnLife;
			
			SpawnManager.onNewWaveE += OnNewWave;
			SpawnManager.onEnableSpawnE += OnEnableSpawn;
			SpawnManager.onSpawnTimerE += OnSpawnTimer;
			
			ResourceManager.onRscChangedE += OnResourceChanged;
		}
		void OnDisable(){
			GameControl.onLifeE -= OnLife;
			
			SpawnManager.onNewWaveE -= OnNewWave;
			SpawnManager.onEnableSpawnE -= OnEnableSpawn;
			SpawnManager.onSpawnTimerE -= OnSpawnTimer;
			
			ResourceManager.onRscChangedE -= OnResourceChanged;
		}
		
		void OnLife(int changedvalue){
			int cap=GameControl.GetPlayerLifeCap();
			string text=(cap>0) ? "/"+GameControl.GetPlayerLifeCap() : "" ;
			txtLife.text=GameControl.GetPlayerLife()+text;
		}
		
		void OnNewWave(int waveID){
			int totalWaveCount=SpawnManager.GetTotalWaveCount();
			string text=totalWaveCount>0 ? "/"+totalWaveCount : "";
			txtWave.text=waveID+text;
			if(GameControl.IsGameStarted()) buttonSpawn.rootObj.SetActive(false);
		}
		
		void OnResourceChanged(List<int> valueChangedList){
			List<Rsc> rscList=ResourceManager.GetResourceList();
			for(int i=0; i<rscObjList.Count; i++){
				rscObjList[i].label.text=rscList[i].value.ToString();
			}
		}
		
		public void OnSpawnButton(){
			//if(FPSControl.IsOn()) return;
			
			timerDuration=0;
			
			SpawnManager.Spawn();
			buttonSpawn.rootObj.SetActive(false);
			
			buttonSpawn.label.text="Next Wave";
		}
		
		void OnEnableSpawn(){
			buttonSpawn.rootObj.SetActive(true);
		}
		
		private float timerDuration=0;
		void OnSpawnTimer(float duration){ timerDuration=duration; }
		void FixedUpdate(){
			if(timerDuration>0){
				if(timerDuration<60) txtTimer.text="Next Wave in "+(Mathf.Ceil(timerDuration)).ToString("f0")+"s";
				else txtTimer.text="Next Wave in "+(Mathf.Floor(timerDuration/60)).ToString("f0")+"m";
				timerDuration-=Time.fixedDeltaTime;
			}
			else if(txtTimer.text!="") txtTimer.text="";
		}
		
		
		
		public Toggle toggleFastForward;
		public void ToggleFF(){
			if(toggleFastForward.isOn) Time.timeScale=UI.GetFFTime();
			else Time.timeScale=1;
		}
		
		public void OnPauseButton(){
			_GameState gameState=GameControl.GetGameState();
			if(gameState==_GameState.Over) return;
			
			if(toggleFastForward.isOn) toggleFastForward.isOn=false;
			
			if(gameState==_GameState.Pause){
				GameControl.ResumeGame();
				UIPauseMenu.Hide();
			}
			else{
				GameControl.PauseGame();
				UIPauseMenu.Show();
			}
		}
	}

}