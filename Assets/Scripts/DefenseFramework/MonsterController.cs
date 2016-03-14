/**
 * @file MonsterController.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;
using GameClient;

namespace DefenseFramework
{
    public class MonsterController : MonoBehaviour
    {
        private MonsterModel m_cModel;

        private Rect m_rectHP;
        private Texture m_tHP_Empty;
        private Texture m_tHP_Full;
        private GameObject m_gBloodEffectPrefab;
        private GUIText m_goldText = null;
        private GameObject m_gHpBar = null; ///생성된 HpBar를 담아둘변수

        // Use this for initialization
        private void Awake()
        {
            m_cModel = GetComponent<MonsterModel>();
            //endPoint = GameObject.Find ("EndPoint").GetComponent<Transform> ().position;
            m_cModel.VEndPoint = new Vector3( 0, 0, 9 );
            m_cModel.VTargetPosition = GameObject.Find( "HeroTower" ).GetComponent<Transform>().position;

            // 걷는 애니메이션 바로 시작
            // animation.Play( "Walk" );

            m_cModel.FHP = MonsterModel.FHPMax;

            m_gBloodEffectPrefab = Resources.Load<GameObject>( "Prefabs/BloodEffect" );
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

            m_cModel.EState = MonsterModel.eMonsterState.walk;

            // 걷는 애니메이션 바로 시작
            GetComponent<Animation>().Play( "Walk" );

            m_cModel.FHP = MonsterModel.FHPMax;
        }

        // Update is called once per frame
        private void Update()
        {
            switch ( m_cModel.EState )
            {
                case MonsterModel.eMonsterState.walk:
                    {
                        // hero tower를 타겟으로 이동

                        float distance = ( transform.position - LookAtTo( m_cModel.VTargetPosition ) ).magnitude;
                        if ( distance >= 1.0f )
                        {
                            transform.Translate( Vector3.forward * m_cModel.FMoveSpeed * Time.deltaTime, Space.Self );
                        }
                        else
                        {
                            GetComponent<Animation>().CrossFade( "Attack_01" );
                            m_cModel.EState = MonsterModel.eMonsterState.attack;
                        }
                    }
                    break;

                case MonsterModel.eMonsterState.attack:
                    {
                    }
                    break;

                case MonsterModel.eMonsterState.die:
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
            switch ( m_cModel.EState )
            {
                case MonsterModel.eMonsterState.walk:
                    {
                    }
                    break;

                case MonsterModel.eMonsterState.die:
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
                m_cModel.FHP -= GameObject.Find( "Tower(Clone)" ).GetComponent<Tower>().m_iBulletDamage;

                CreateBloodEffect( coll.transform.position );

                if ( m_cModel.FHP <= 0 )
                {
                    Debug.Log( "monster died~" );

                    m_cModel.EState = MonsterModel.eMonsterState.die;
                    GameManager.score += MonsterModel.IEarnScore;
                    GameManager.gold += MonsterModel.IEarnGold;

                    // 죽는 애니메이션 재생
                    GetComponent<Animation>().CrossFade( "Die" );

                    GameManager.insertObjet( m_gHpBar );
                    m_gHpBar = null;

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
            GameObject blood = (GameObject) Instantiate( m_gBloodEffectPrefab, position, Quaternion.identity );
            blood.transform.parent = this.transform;
            Destroy( blood, 2.0f );
        }

        ///HpBar를 생성해준다
        private void CreateHpBar()
        {
            if ( m_gHpBar == null )
            {
                //피바생성
                m_gHpBar = GameManager.createObjet( GameManager.hpBar_PrefabName );

                //따라갈 대상설정
                m_gHpBar.GetComponent<UIWidget>().SetAnchor( this.transform );

                //약간위에서 따라가도록
                m_gHpBar.GetComponent<UIWidget>().topAnchor.SetHorizontal( this.transform, 20 );
                m_gHpBar.GetComponent<UIWidget>().bottomAnchor.SetHorizontal( this.transform, 20 );
            }
        }

        ///현제체력을 보여준다
        private void CurrentHpView()
        {
            if ( m_gHpBar == null )
            {
                return;
            }

            m_gHpBar.transform.GetChild( 0 ).GetComponent<UISlider>().value = m_cModel.FHP / MonsterModel.FHPMax;
        }
    }

}

