using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
    public int id = 0;
	public float moveSpeed = 0.3f;
	public float maxDistance = 1000.0f;
	private Transform endPointTransform;
	public enum MonsterState { idle, walk, attack, gothit, die };
	public MonsterState monsterState = MonsterState.walk;

    [HideInInspector]
    public float HP = 0.0f;

    public const float HP_Max = 30.0f;
    private Vector3 pointHP = Vector3.zero;
    private Rect rectHP;
    public Texture HP_EmptyTexture;
    public Texture HP_FullTexture;

	// private NavMeshAgent navAgent;
    public const int earnGold = 3;
    public const int earnScore = 10;
    public GameObject bloodEffectPrefab;
    public GUIText goldText = null;
    [HideInInspector]
    public Vector3 targetPosition = Vector3.zero;

	// Use this for initialization
	void Awake () {
		// navAgent = gameObject.GetComponent<NavMeshAgent>();

		endPointTransform = GameObject.Find ("EndPoint").GetComponent<Transform> ();
        targetPosition = GameObject.Find("HeroTower").GetComponent<Transform>().position;

		// 걷는 애니메이션 바로 시작
		// animation.Play( "Walk" );

        HP = HP_Max;
	}

    void OnEnable()
    {
        // 최초 위치로 이동
        Vector3 initialPos = transform.position;
        initialPos.z = 0;
        transform.position = initialPos;

        // gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;

        monsterState = MonsterState.walk;

        // 걷는 애니메이션 바로 시작
        animation.Play("Walk");

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
                        // 일직선 이동
                        // transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);

                        // hero tower를 타겟으로 이동
                        float distance = (transform.position - LookAtTo(targetPosition)).magnitude;
                        if ( distance >= 1.0f )
                        {
                            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
                        }
                        else
                        {
                            animation.CrossFade("Attack_01");
                            monsterState = MonsterState.attack;
                        }
                        
                    }
                }
                break;

            case MonsterState.attack:
                {

                }
                break;

            case MonsterState.die:
                {
                    if ( animation.isPlaying == false )
                    {
                        GameObjectPool pool = GameObject.Find("GameManager").GetComponent<GameManager>().GetPool();
                        pool.RemoveItem(this.gameObject);

                        /*
                        Vector3 initialPos = transform.position;
                        initialPos.z = 0;
                        transform.position = initialPos;

                        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;
                        monsterState = MonsterState.walk;
                        animation.Play("Walk");
                        HP = HP_Max;
                        */
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

    Vector3 LookAtTo( Vector3 pos )
    {
        Vector3 look = Vector3.zero;
        look.x = pos.x;
        look.y = transform.position.y;
        look.z = pos.z;
        transform.LookAt(look);
        return look;
    }

	// void OnCollisionEnter( Collision coll )
    void OnTriggerEnter( Collider coll )
	{if ( coll.collider.tag == "BULLET" )
		{
            // HP -= Bullet.damage;
            HP -= GameObject.Find("Tower(Clone)").GetComponent<Tower>().bulletDamage;

            CreateBloodEffect(coll.transform.position);

            if ( HP <= 0 )
            {
                Debug.Log("monster died~");

                monsterState = MonsterState.die;
                GameObject.Find("GameManager").GetComponent<GameManager>().score += earnScore;
                GameObject.Find("GameManager").GetComponent<GameManager>().gold += earnGold;

                // 죽는 애니메이션 재생
                animation.CrossFade("Die");

                // gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
                
                // FIXIT
                // 골드 텍스트 이펙트
                // gameObject.GetComponent("GoldText").StartDisplay();
                // Transform goldTransform = transform.Find("GoldText");
                // goldTransform.gameObject.GetComponent<Gold>().StartDisplay();

                // goldText.gameObject.GetComponent<Gold>().StartDisplay();
            }
			
			// 총알 오브젝트를 삭제하지 않고 타워에서 재사용
            coll.gameObject.SetActive(false);

		}
	}

    void CreateBloodEffect( Vector3 position )
    {
        GameObject blood = (GameObject)Instantiate(bloodEffectPrefab, position, Quaternion.identity);
        blood.transform.parent = this.transform;
        Destroy(blood, 2.0f);

    }

    void OnGUI()
    {
        // GUI 몬스터 HP
        Vector3 pointTransform = Vector3.zero;
        pointTransform.x = transform.position.x;
        pointTransform.y = transform.position.y + 1.5f;
        pointTransform.z = transform.position.z;        
        pointHP = Camera.main.WorldToScreenPoint(pointTransform);
        rectHP.width = 100;
        rectHP.height = 10;
        rectHP.x = pointHP.x - (rectHP.width / 2);
        rectHP.y = Screen.height - pointHP.y - (rectHP.height / 2);

        GUI.DrawTexture(rectHP, HP_EmptyTexture);
        GUI.BeginGroup(rectHP, "");
        GUI.DrawTexture(new Rect(0, 0, 100 * (HP / HP_Max), rectHP.height), HP_FullTexture);
        GUI.EndGroup();
    }
}


