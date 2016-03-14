/**
 * @file ProjectileController.cs
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
    public class ProjectileController : MonoBehaviour
    {
        private ProjectileModel m_cModel;
        private GameObject m_gNearMonster;
        private GameObject m_gParentObject;

        public GameObject GNearMonster
        {
            get
            {
                return m_gNearMonster;
            }

            set
            {
                m_gNearMonster = value;
            }
        }

        public GameObject GParentObject
        {
            get
            {
                return m_gParentObject;
            }

            set
            {
                m_gParentObject = value;
            }
        }

        // Use this for initialization
        private void Awake()
        {
            m_cModel = GetComponent<ProjectileModel>();
        }

        // Update is called once per frame
        private void Update()
        {
            if ( GNearMonster == null )
            {
                return;
            }
            if ( GNearMonster.activeSelf == true )//해당몹이 액티브일경우만
            {
                // FIXIT

                transform.LookAt( GNearMonster.transform.position );//타겟을 쳐다보게함

                transform.Translate( Vector3.forward * m_cModel.FSpeed * Time.deltaTime );//앞으로전진
            }
            else
            {
                // FIXIT
                //총알 삭제처리 버그픽스
                GameManager.insertObjet( this.gameObject );

                // 우선은 이렇게 처리
                //gameObject.SetActive( false );

                //총알연사력0.1에 총알속도0.3으로 테스트 (엄청빠르게쏘고 느리게날라가게해봄)
                //몹이죽은후 총알이 멈춘채로 대기후에 재사용된몹이 생성되면 그놈한테 날라감
                //가끔 총알이 남아있는경우가 보여서 테스트후 버그처리완료
                //추가후 잘사라짐
            }
        }

        private void OnTriggerEnter( Collider coll )
        {
        }
    }
}