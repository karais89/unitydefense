/**
 * @file TowerController.cs
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
    public class TowerController : MonoBehaviour
    {
        private TowerModel m_cModel;
        private TowerView m_cView;
        private GameObject m_gFirePos = null;
        private GameObject m_gTargetMonster = null;

        private void Awake()
        {
            m_cModel = GetComponent<TowerModel>();
            m_cView = GetComponent<TowerView>();

            m_gFirePos = transform.FindChild( "FirePos" ).gameObject;

            StartCoroutine( "CreateBullet" );
        }

        /// <summary>
        /// 일정 시간 간격으로 총알을 생성한다.
        /// </summary>
        /// <returns></returns>
        private IEnumerator CreateBullet()
        {
            while ( true )
            {
                // 일정 대기 시간이 있고 총알이 생성됨
                yield return new WaitForSeconds( m_cModel.FFireTerm );

                CheckTargetRange();

                GetClosestMonster();

                if ( m_gTargetMonster != null )
                {
                    m_cModel.IBulletCount++;
                    // Debug.Log ( "create bullet" );

                    //사용안함
                    //GameObject newBullet = pool.NewItem();

                    //총알오브젝트생성 혹은 꺼내오기
                    GameObject newBullet = GameManager.createObjet( GameManager.bullet_PrefabName );
                    newBullet.transform.position = m_gFirePos.transform.position;
                    newBullet.transform.rotation = Quaternion.identity;
                    newBullet.GetComponent<ProjectileModel>().IID = m_cModel.IBulletCount;
                    newBullet.GetComponent<ProjectileController>().GNearMonster = m_gTargetMonster;
                }
            }
        }

        /// <summary>
        /// 총알을 발사할 타겟 몬스터를 검사한다.
        /// </summary>
        private void CheckTargetRange()
        {
            if ( m_gTargetMonster == null )
            {
                return;
            }
            if ( m_gTargetMonster.GetComponent<MonsterModel>().EState == MonsterModel.eMonsterState.die )
            {
                m_gTargetMonster = null;
                return;
            }

            if ( Vector3.Distance( m_gTargetMonster.transform.position, transform.position ) >= m_cModel.FAttackRange )
            {
                m_gTargetMonster = null;
            }
        }

        /// <summary>
        /// 타워에 가장 가까이에 있는 몬스터를 반환한다.
        /// </summary>
        /// <returns></returns>
        public GameObject GetClosestMonster()
        {
            if ( m_gTargetMonster != null )
            {
                return m_gTargetMonster;
            }

            foreach ( GameObject monster in GameManager.monsterList )
            {
                if ( Vector3.Distance( monster.transform.position, transform.position ) < m_cModel.FAttackRange )
                {
                    if ( monster.GetComponent<MonsterModel>().EState == MonsterModel.eMonsterState.die )
                    {
                        continue;
                    }
                    m_gTargetMonster = monster;
                }
            }
            return m_gTargetMonster;
        }

        /// <summary>
        /// 타워를 업그레이드 한다.
        /// </summary>
        public void Upgrade()
        {
            // 임시로 총알 데미지를 두배로 올림
            m_cModel.IBulletDamage *= 2;
            m_cModel.ISellGold *= 2;
            m_cModel.IEarnScore *= 2;
            m_cModel.FAttackRange += 1.0f;

            // 임시로 시각적으로 보여주기 위해 크기를 늘린다
            transform.localScale += new Vector3( 0.1f, 0.1f, 0.1f );
        }
    }
}