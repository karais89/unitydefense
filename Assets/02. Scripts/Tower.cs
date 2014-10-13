using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {
    public int id = 0;
	public GameObject firePos;
	// public GameObject attackRange;
	public float attackRange = 3.0f;
	public float fireTerm = 1.0f;
	public GameObject targetMonster = null;
    private int bulletCount = 0;
	
	// Use this for initialization
	void Awake () {
		
		// InvokeRepeating("GetClosestEnemy", 0, 1.0f);
		
		// 공격 범위 표시
		// attackRange = gameObject.GetComponent( "AttackRange" ) as GameObject;
		// attackRange = gameObject.GetComponentInChildren<AttackRange>;
		// attackRange.renderer.enabled = true;
		// attackRange.renderer.material.color = Color.green;
		/*
		float alpha = 0.5f;
		attackRange.renderer.material.color.a = alpha;
		*/
		
		StartCoroutine("CreateBullet");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator CreateBullet()
	{
		while(true)
		{
			// 일정 대기 시간이 있고 총알이 생성됨
			yield return new WaitForSeconds( fireTerm );
			
			CheckTargetRange();
			
			GetClosestMonster();
			
			if(targetMonster != null)
			{
                bulletCount++;
				Debug.Log ( "create bullet" );

                GameObject bullet = (GameObject)Instantiate(Resources.LoadAssetAtPath("Assets/03. Prefabs/bullet.prefab", typeof(GameObject)), firePos.transform.position, Quaternion.identity);			
				bullet.transform.Rotate( 90, 0, 0 );
                bullet.GetComponent<Bullet>().id = bulletCount;
                bullet.GetComponent<Bullet>().nearMonster = targetMonster;
				
				// 총알을 타워의 차일드로 추가
				bullet.transform.parent = this.transform;
			}
		}
	}
	
	void CheckTargetRange()
	{
		if (targetMonster == null) 
		{
			return;		
		}
		
		if ( Vector3.Distance( targetMonster.transform.position, transform.position ) >= attackRange )
		{
			targetMonster = null;
		}
		
	}
	
	public GameObject GetClosestMonster()
	{
		if ( targetMonster != null )
		{
			return targetMonster;
		}
		
		foreach( GameObject monster in GameManager.monsterList )
		{
			if ( Vector3.Distance( monster.transform.position, transform.position ) < attackRange )
			{
				targetMonster = monster;
			}
		}
        return targetMonster;
	}

    
}
