/**
 * @file Monster.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-10-29
 */

using UnityEngine;

public class Monster : MonoBehaviour
{
    //public class Monster : Pathfinding {
    public int id = 0;

    public float moveSpeed = 0.3f;
    public float maxDistance = 1000.0f;
    private Vector3 endPoint;

    public enum MonsterState { idle, walk, attack, gothit, die };

    public MonsterState monsterState = MonsterState.walk;

    [HideInInspector]
    public float HP = 0.0f;

    public const float HP_Max = 30.0f;
    private Rect rectHP;
    public Texture HP_EmptyTexture;
    public Texture HP_FullTexture;

    //private NavMeshAgent navAgent;
    public const int earnGold = 3;

    public const int earnScore = 10;
    public GameObject bloodEffectPrefab;
    public GUIText goldText = null;

    [HideInInspector]
    public Vector3 targetPosition = Vector3.zero;

    ///생성된 HpBar를 담아둘변수
    private GameObject HpBar = null;

    // Use this for initialization
    private void Awake()
    {
        //navAgent = gameObject.GetComponent<NavMeshAgent>();

        //endPoint = GameObject.Find ("EndPoint").GetComponent<Transform> ().position;
        endPoint = new Vector3( 0, 0, 9 );
        targetPosition = GameObject.Find( "HeroTower" ).GetComponent<Transform>().position;

        // 걷는 애니메이션 바로 시작
        // animation.Play( "Walk" );

        HP = HP_Max;

        // 추적 대상의 위치 설정하면 바로 추적 시작
        //navAgent.destination = endPointTransform.position;
    }

    private void OnEnable()
    {
        // 최초 위치로 이동
        Vector3 initialPos = transform.position;
        initialPos.z = 0;
        transform.position = initialPos;

        // 추적 대상의 위치 설정하면 바로 추적 시작
        //navAgent.destination = endPointTransform.position;

        // gameObject.GetComponentInChildren<CapsuleCollider>().enabled = true;

        monsterState = MonsterState.walk;

        // 걷는 애니메이션 바로 시작
        GetComponent<Animation>().Play( "Walk" );

        HP = HP_Max;
    }
    
    // Update is called once per frame
    private void Update()
    {
        switch ( monsterState )
        {
            case MonsterState.walk:
                {
                    // hero tower를 타겟으로 이동

                    float distance = ( transform.position - LookAtTo( targetPosition ) ).magnitude;
                    if ( distance >= 1.0f )
                    {
                        transform.Translate( Vector3.forward * moveSpeed * Time.deltaTime, Space.Self );
                    }
                    else
                    {
                        GetComponent<Animation>().CrossFade( "Attack_01" );
                        monsterState = MonsterState.attack;
                    }
                }
                break;

            case MonsterState.attack:
                {
                }
                break;

            case MonsterState.die:
                {
                    if ( GetComponent<Animation>().isPlaying == false )
                    {
                        //사용이끝난 오브젝트 큐로 반환
                        GameManager.insertObjet( this.gameObject );
                    }
                }
                break;

            default:
                {
                }
                break;
        }
    }
    
    private void ActionState()
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
    private Vector3 LookAtTo( Vector3 pos )
    {
        Vector3 look = Vector3.zero;
        look.x = pos.x;
        look.y = transform.position.y;
        look.z = pos.z;
        transform.LookAt( look );
        return look;
    }

    private void OnTriggerEnter( Collider coll )
    {
        if ( coll.GetComponent<Collider>().tag == "BULLET" )
        {
            // HP -= Bullet.damage;
            HP -= GameObject.Find( "Tower(Clone)" ).GetComponent<Tower>().bulletDamage;

            CreateBloodEffect( coll.transform.position );

            if ( HP <= 0 )
            {
                Debug.Log( "monster died~" );

                monsterState = MonsterState.die;
                GameManager.score += earnScore;
                GameManager.gold += earnGold;

                // 죽는 애니메이션 재생
                GetComponent<Animation>().CrossFade( "Die" );

                GameManager.insertObjet( HpBar );
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
            GameManager.insertObjet( coll.gameObject );
            
        }
    }

    /// <summary>
    /// 지정된 위치에 피 이펙트를 생성한다.
    /// </summary>
    /// <param name="position"></param>
    private void CreateBloodEffect( Vector3 position )
    {
        GameObject blood = (GameObject) Instantiate( bloodEffectPrefab, position, Quaternion.identity );
        blood.transform.parent = this.transform;
        Destroy( blood, 2.0f );
    }

    ///HpBar를 생성해준다
    private void CreateHpBar()
    {
        if ( HpBar == null )
        {
            //피바생성
            HpBar = GameManager.createObjet( GameManager.hpBar_PrefabName );

            //따라갈 대상설정
            HpBar.GetComponent<UIWidget>().SetAnchor( this.transform );

            //약간위에서 따라가도록
            HpBar.GetComponent<UIWidget>().topAnchor.SetHorizontal( this.transform, 20 );
            HpBar.GetComponent<UIWidget>().bottomAnchor.SetHorizontal( this.transform, 20 );
        }
    }

    ///현제체력을 보여준다
    private void CurrentHpView()
    {
        if ( HpBar == null )
        {
            return;
        }

        HpBar.transform.GetChild( 0 ).GetComponent<UISlider>().value = HP / HP_Max;
    }
}