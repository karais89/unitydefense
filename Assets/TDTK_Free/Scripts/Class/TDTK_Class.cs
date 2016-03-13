using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TDTK {

	//Use in BuildManager, the status return when CheckBuildPoint() is called
	public enum _TileStatus{
		NoPlatform, 	//no platform at detected
		Available, 		//there's a valid build point
		Unavailable, 	//the build point is invalid (occupied)
		Blocked			//building on the spot will block the only available path
	}
	
	//Use in BuildManager, contain all the infomation of the specific select build spot
	[System.Serializable]
	public class BuildInfo{
		public Vector3 position=Vector3.zero;		//the position of the build point
		public PlatformTD platform;					//the platform the build point belongs to
		
		//the prefabIDs of the towers available to be build
		public List<int> availableTowerIDList=new List<int>();	
	}
	
	
	
	
	[System.Serializable]
	public class TDTKItem{
		public int ID=0;
		public string name="";
		
		public Sprite icon;
		//public Texture icon;
		//public string iconName;
	}
	
	
	[System.Serializable]
	public class Rsc : TDTKItem{
		public int value;
		
		public Rsc Clone(){
			Rsc rsc=new Rsc();
			rsc.ID=ID;
			rsc.name=name;
			rsc.icon=icon;
			//rsc.iconName=iconName;
			rsc.value=value;
			return rsc;
		}
		
		public bool IsMatch(Rsc rsc){
			if(rsc.ID!=ID) return false;
			if(rsc.name!=name) return false;
			if(rsc.icon!=icon) return false;
			return true;
		}
	}
	
	
	
	[System.Serializable]
	public class DAType : TDTKItem{
		public string desp="";
	}
	[System.Serializable]
	public class DamageType : DAType{
		
	}
	[System.Serializable]
	public class ArmorType : DAType{
		public List<float> modifiers=new List<float>();
	}
	
	
	
	
	
	
	

	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	/*
	[System.Serializable]
	public class Weapon : TDTKItem{
		
		public int currentActiveStat=0;
		public List<UnitStat> stats=new List<UnitStat>(){ new UnitStat() };
		public int damageType=0;
		
		private float currentCD=0;
		public int currentAmmo=10;
		
		public float recoil=1;
		
		public int GetShootPointCount(){ return 1; }
		
		public bool ReadyToFire(){
			if(IsOnCooldown()) return false;
			if(OutOfAmmo()) return false;
			return true;
		}
		
		public bool IsOnCooldown(){ return currentCD>0 ? true : false;}
		
		public IEnumerator CooldownRoutine(){
			currentCD=GetCooldown();
			while(currentCD>0){
				currentCD-=Time.deltaTime;
				yield return null;
			}
		}
		
		public bool OutOfAmmo(){ return currentAmmo<=0 ? true : false; }
		
		public bool IsReloading(){ return reloadDuration>0 ? true : false; }
		
		private float reloadDuration=0;
		public IEnumerator ReloadRoutine(){
			reloadDuration=GetReloadDuration();
			while(reloadDuration>0){
				reloadDuration-=Time.deltaTime;
				yield return null;
			}
			currentAmmo=GetClipSize();
			
			FPSControl.ReloadComplete(null);
		}
		
		
		
		public float GetCooldown(){ return Mathf.Max(0.05f, stats[currentActiveStat].cooldown * (1)); }
		public float GetDamageMin(){ return Mathf.Max(0, stats[currentActiveStat].damageMin * (1)); }
		public float GetDamageMax(){ return Mathf.Max(0, stats[currentActiveStat].damageMax * (1)); }
		
		//public int GetCurrentAmmo(){ return currentAmmo; }
		public int GetClipSize(){ return stats[currentActiveStat].clipSize; }
		public float GetReloadDuration(){ return Mathf.Max(0.05f, stats[currentActiveStat].reloadDuration * (1)); }
		
		public float GetCritChance(){ return stats[currentActiveStat].crit.chance; }
		public float GetCritModifier(){ return stats[currentActiveStat].crit.dmgMultiplier; }
		
		public Stun GetStun(){ return stats[currentActiveStat].stun; }
		public Slow GetSlow(){ return stats[currentActiveStat].slow; }
		public Dot GetDot(){ return stats[currentActiveStat].dot; }
		
		public bool DamageShieldOnly(){ return stats[currentActiveStat].damageShieldOnly; }
		public float GetShieldBreak(){ return stats[currentActiveStat].shieldBreak; }
		public float GetShieldPierce(){ return stats[currentActiveStat].shieldPierce; }
		public InstantKill GetInstantKill(){ return stats[currentActiveStat].instantKill; }
	}*/
	
	
}