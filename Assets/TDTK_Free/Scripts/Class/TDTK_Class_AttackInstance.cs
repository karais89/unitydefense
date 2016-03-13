using UnityEngine;
using System.Collections;

using TDTK;

namespace TDTK {
		
	public class AttackInstance {
		
		public bool processed=false;
		
		public Unit srcUnit;
		public Unit tgtUnit;
		
		public Vector3 impactPoint;
		
		public bool critical=false;
		public bool destroy=false;
		
		public bool stunned=false;
		public bool slowed=false;
		public bool dotted=false;
		
		public float damage=0;
		public float damageHP=0;
		public float damageShield=0;
		
		public Stun stun;
		public Slow slow;
		public Dot dot;
		
		
		
		public void Process(){
			if(processed) return;
			
			processed=true;
			
			damage=Random.Range(srcUnit.GetDamageMin(), srcUnit.GetDamageMax());
			damage/=(float)srcUnit.GetShootPointCount();	//divide the damage by number of shootPoint
			
			
			float critChance=srcUnit.GetCritChance();
			if(tgtUnit.immuneToCrit) critChance=-1f;
			if(Random.Range(0f, 1f)<critChance){
				critical=true;
				damage*=srcUnit.GetCritMultiplier();
			}
			
			
			if(damage>=tgtUnit.shield){
				damageShield=tgtUnit.shield;
				damageHP=damage-tgtUnit.shield;
			}
			else{
				damageShield=damage;
				damageHP=0;
			}
			
			if(damageHP>=tgtUnit.HP){
				destroy=true;
				return;
			}
			
			stunned=srcUnit.GetStun().IsApplicable();
			if(tgtUnit.immuneToStun) stunned=false;
			
			slowed=srcUnit.GetSlow().IsValid();
			if(tgtUnit.immuneToSlow) slowed=false;
			
			if(srcUnit.GetDot().GetTotalDamage()>0) dotted=true;
			
			
			if(stunned) stun=srcUnit.GetStun().Clone();
			if(slowed) slow=srcUnit.GetSlow().Clone();
			if(dotted) dot=srcUnit.GetDot().Clone();
			
		}
		
		public AttackInstance Clone(){
			AttackInstance attInstance=new AttackInstance();
			
			attInstance.processed=processed;
			attInstance.srcUnit=srcUnit;
			attInstance.tgtUnit=tgtUnit;
			
			attInstance.critical=critical;
			attInstance.destroy=destroy;
			
			attInstance.stunned=stunned;
			attInstance.slowed=slowed;
			attInstance.dotted=dotted;
			
			attInstance.damage=damage;
			attInstance.damageHP=damageHP;
			attInstance.damageShield=damageShield;
			
			attInstance.stun=stun;
			attInstance.slow=slow;
			attInstance.dot=dot;
			
			return attInstance;
		}
		
	}

}