using UnityEngine;
using System.Collections;


public class Tower : MonoBehaviour {
    public int id = 0;
	public GameObject firePos;
	// public GameObject attackRangeSphere;
	public float attackRange = 3.0f;
	public float fireTerm = 1.0f;
	public GameObject targetMonster = null;
    private int bulletCount = 0;
    public int earnScore = 20;
    public int buyGold = 30;
    public int sellGold = 10;
    public int level = 1;
    public int bulletDamage = 10;
    private GameObjectPool pool = new GameObjectPool();
    private GameObject bullet = null;
    

    public int GetEarnScore()
    {
        return earnScore;
    }
    public int GetBuyGold()
    {
        return buyGold;
    }
    public int GetSellGold()
    {
        return sellGold;
    }
    public int GetBulletDamage()
    {
        return bulletDamage;
    }

	// Use this for initialization
	void Awake () {
		
        bullet = (GameObject)Resources.Load("Prefabs/bullet", typeof(GameObject));

        pool.Create(bullet, 10);
        pool.SetParent(this.transform);
		
		StartCoroutine("CreateBullet");
		
	}

    void Start()
    {
        /*
        // 공격 범위 표시
        //attackRangeSphere = GameObject.Find("AttackRangeSphere");
        attackRangeSphere.renderer.enabled = false;
        Color rangeColor = Color.green;
        rangeColor.a = 0.5f;
        attackRangeSphere.renderer.material.color = rangeColor;
         */ 
    }

    void OnApplicationQuit()
    {
        pool.Dispose();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisplayAttackRangeSphere( bool visible )
    {
        /*
        if ( visible == true )
        {
            attackRangeSphere.renderer.enabled = true;
        }
        else if ( visible == false )
        {
            attackRangeSphere.renderer.enabled = false;
        }
         */
    }
	
    /// <summary>
    /// 일정 시간 간격으로 총알을 생성한다.
    /// </summary>
    /// <returns></returns>
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
				// Debug.Log ( "create bullet" );

                GameObject newBullet = pool.NewItem();
                newBullet.transform.position = firePos.transform.position;
                newBullet.transform.rotation = Quaternion.identity;
				// bullet.transform.Rotate( 90, 0, 0 );
                // bullet.transform.Rotate(  targetMonster.transform.position - firePos.transform.position );
                newBullet.GetComponent<Bullet>().id = bulletCount;
                newBullet.GetComponent<Bullet>().nearMonster = targetMonster;

                // Vector3 targetDir = targetMonster.transform.position - transform.position;
                // bullet.rigidbody.velocity = transform.TransformDirection(targetDir * 1.0f);
				
				// 총알을 타워의 차일드로 추가
                // newBullet.transform.parent = this.transform;

                // Vector3 force = targetMonster.transform.position - transform.position;
                // bullet.rigidbody.AddForce(targetMonster.transform.position);
			}
		}
	}
	
    /// <summary>
    /// 총알을 발사할 타겟 몬스터를 검사한다.
    /// </summary>
	void CheckTargetRange()
	{
		if (targetMonster == null) 
		{
			return;		
		}
        if ( targetMonster.GetComponent<Monster>().monsterState == Monster.MonsterState.die )
        {
            targetMonster = null;
            return;
        }
		
		if ( Vector3.Distance( targetMonster.transform.position, transform.position ) >= attackRange )
		{
			targetMonster = null;
		}
	}
	
    /// <summary>
    /// 타워에 가장 가까이에 있는 몬스터를 반환한다.
    /// </summary>
    /// <returns></returns>
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
                if ( monster.GetComponent<Monster>().monsterState == Monster.MonsterState.die )
                {
                    continue;
                }
				targetMonster = monster;
			}
		}
        return targetMonster;
	}

    /// <summary>
    /// 타워를 업그레이드 한다.
    /// </summary>
    public void Upgrade()
    {
        // 임시로 총알 데미지를 두배로 올림
        bulletDamage *= 2;
        sellGold *= 2;
        earnScore *= 2;
        attackRange += 1.0f;

        // 임시로 시각적으로 보여주기 위해 크기를 늘린다
        transform.localScale += new Vector3( 0.1f, 0.1f, 0.1f );
    }
}
