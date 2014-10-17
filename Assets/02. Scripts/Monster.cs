using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
    public int id = 0;
	public float moveSpeed = 0.1f;
	public float maxDistance = 1000.0f;
	private Transform endPointTransform;
	public enum MonsterState { walk, gothit, die };
	public MonsterState monsterState = MonsterState.walk;
	public int HP { get; set; }
    public const int HP_Max = 30;
	private NavMeshAgent navAgent;
    public const int earnGold = 3;
    public const int earnScore = 10;
    public GameObject bloodEffectPrefab;

	// Use this for initialization
	void Awake () {
		navAgent = gameObject.GetComponent<NavMeshAgent>();

		endPointTransform = GameObject.Find ("EndPoint").GetComponent<Transform> ();

		// 걷는 애니메이션 바로 시작
		animation.Play( "Walk" );

        HP = HP_Max;
	}

	void Start()
	{
		// navAgent = this.gameObject.GetComponent<NavMeshAgent>();

		// 추적 대상의 위치 설정하면 바로 추적 시작
		// navAgent.destination = endPointTransform.position;
	}
	
	// Update is called once per frame
	void Update () {

        switch ( monsterState )
        {
            case MonsterState.walk:
                {
                    if (transform.position.z < endPointTransform.position.z)
                    {
                        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
                    }
                }
                break;

            case MonsterState.die:
                {
                    if ( animation.isPlaying == false )
                    {
                        // void Blend(string animation, float targetWeight = 1.0F, float fadeLength = 0.3F);
                        // animation.Blend("Die", 1.0f, 0.3f);

                        Vector3 initialPos = transform.position;
                        initialPos.z = 0;
                        transform.position = initialPos;

                        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;
                        monsterState = MonsterState.walk;
                        animation.Play("Walk");
                        HP = HP_Max;
                    }
                }
                break;

            default:
                {

                }
                break;
        }
		
	}

    void ActionState()
    {
        switch ( monsterState )
        {
            case MonsterState.walk:
                {

                }
                break;

            case MonsterState.die:
                {

                }
                break;

            default:
                break;

                    
        }
    }

	// void OnCollisionEnter( Collision coll )
    void OnTriggerEnter( Collider coll )
	{
       

		if ( coll.collider.tag == "BULLET" )
		{
            HP -= Bullet.damage;

            CreateBloodEffect(coll.transform.position);

            if ( HP <= 0 )
            {
                Debug.Log("monster died~");

                monsterState = MonsterState.die;
                GameObject.Find("GameManager").GetComponent<GameManager>().score += earnScore;
                GameObject.Find("GameManager").GetComponent<GameManager>().gold += earnGold;

                // 죽는 애니메이션 재생
                animation.Play("Die");

                gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
                // gameObject.SetActive(false);
                // DestroyImmediate(this.gameObject);
            }
			
			// TODO
			// 총알 오브젝트를 삭제하지 않고 타워에서 재사용

			Destroy ( coll.gameObject );
			// coll.gameObject.SetActive( false );
		}
	}

    void CreateBloodEffect( Vector3 position )
    {
        GameObject blood = (GameObject)Instantiate(bloodEffectPrefab, position, Quaternion.identity);
        Destroy(blood, 2.0f);

    }
}


