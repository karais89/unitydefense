using UnityEngine;
using System.Collections;
using Common;

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

	///생성된 HpBar를 담아둘변수
	GameObject HpBar = null;

    //private bool isMoveAble = false;
    private Point[] pathArr;
    private Point startPoint;
    private Point nextPoint;
    private int pathIndex = 0;

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
								
				// hero tower를 타겟으로 이동
                /*
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
				*/

                float _speed = /*GameManager.instance.cellSize * */Time.deltaTime * moveSpeed;
                int tx = nextPoint.x - startPoint.x;
                int ty = nextPoint.y - startPoint.y;

                float dx = _speed * tx;
                float dy = -_speed * ty;
                float rx = (nextPoint.x /* * GameManager.instance.cellSize + GameManager.instance.cellSize / 2.0f*/) - this.transform.localPosition.x;
                float ry = (-nextPoint.y /* * GameManager.instance.cellSize - GameManager.instance.cellSize / 2.0f*/) - this.transform.localPosition.z;
                bool isCloseX = false;
                bool isCloseY = false;
                if (Mathf.Abs(dx) > Mathf.Abs(rx) || dx == 0) 
                {
                    dx = rx;
                    isCloseX = true;
                }
                if (Mathf.Abs(dy) > Mathf.Abs(ry) || dy == 0) 
                {
                    dy = ry;
                    isCloseY = true;
                }

                this.transform.localPosition += new Vector3(dx, 0, -dy);

                if (isCloseX && isCloseY)
                {
                    if (pathArr.Length <= pathIndex + 1)
                    {
                        return;
                    }
                    SetNextPoint();
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
				
				
				
				
				//사용이끝난 오브젝트 큐로 반환
				GameManager.insertObjet(this.gameObject);
				/*
                GameObjectPool pool = GameObject.Find("GameManager").GetComponent<GameManager>().GetPool();
                pool.RemoveItem(this.gameObject);*/
				
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

    

    public void SetStartPoint(Point p)
    {
        startPoint = p;
        GetPath();
        nextPoint = pathArr[pathIndex];
        // showCharDir();
        // isMoveAble = true;
    }

    public void GetPath()
    {
        Point[] pArr = PathFinder.Instance.GetPath(startPoint, 111);
        if (pArr == null) 
        {
            Debug.Log("NULL path");
            return;
        }
        pathArr = pArr;
        //pathArr = new Point[]{new Point(startPoint.x + 1, startPoint.y), new Point(startPoint.x + 2, startPoint.y), new Point(startPoint.x + 2, startPoint.y+1), new Point(startPoint.x + 2, startPoint.y)};
        pathIndex = 0;
    }

    private void SetNextPoint()
    {
        startPoint = nextPoint;
        pathIndex++;
        nextPoint = pathArr[pathIndex];
        // showCharDir();
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

    /// <summary>
    /// 타겟 pos을 향해 바라본다.
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    Vector3 LookAtTo( Vector3 pos )
    {
        Vector3 look = Vector3.zero;
        look.x = pos.x;
        look.y = transform.position.y;
        look.z = pos.z;
        transform.LookAt(look);
        return look;
    }

	
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
				GameManager.score += earnScore;
				GameManager.gold += earnGold;

                // 죽는 애니메이션 재생
                animation.CrossFade("Die");


				GameManager.insertObjet(HpBar);
				HpBar = null;

                // gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
                
                // FIXIT
                // 골드 텍스트 이펙트
                // gameObject.GetComponent("GoldText").StartDisplay();
                // Transform goldTransform = transform.Find("GoldText");
                // goldTransform.gameObject.GetComponent<Gold>().StartDisplay();

                // goldText.gameObject.GetComponent<Gold>().StartDisplay();
            }
			else
			{
				//HpBar생성
				CreateHpBar();
			}

			//현재체력표시
			CurrentHpView();


			//총알 오브젝트를 삭제하지않고 게임매니저통합에서 재사용
			//사용이끝난 오브젝트 큐로 반환
			GameManager.insertObjet(coll.gameObject);

			//사용안함
			// 총알 오브젝트를 삭제하지 않고 타워에서 재사용
            //coll.gameObject.SetActive(false);

		}
	}


    /// <summary>
    /// 지정된 위치에 피 이펙트를 생성한다.
    /// </summary>
    /// <param name="position"></param>
    void CreateBloodEffect( Vector3 position )
    {
        GameObject blood = (GameObject)Instantiate(bloodEffectPrefab, position, Quaternion.identity);
        blood.transform.parent = this.transform;
        Destroy(blood, 2.0f);

    }



	
	///HpBar를 생성해준다
	private void CreateHpBar()
	{
		if (HpBar == null)
		{
			//피바생성
			HpBar = GameManager.createObjet (GameManager.hpBar_PrefabName);
			
			//따라갈 대상설정
			HpBar.GetComponent<UIWidget>().SetAnchor(this.transform);

			//약간위에서 따라가도록
			HpBar.GetComponent<UIWidget>().topAnchor.SetHorizontal(this.transform , 20);
			HpBar.GetComponent<UIWidget>().bottomAnchor.SetHorizontal(this.transform , 20);

		}
	}

	///현제체력을 보여준다
	private void CurrentHpView()
	{
		if (HpBar == null) 
		{
			return;
		}
		HpBar.transform.GetChild(0).GetComponent<UISlider>().sliderValue = HP / HP_Max;
	}


	//사용안함
    /*void OnGUI()
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
    }*/


}


