/**
 * @file Bullet.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-10-29
 */

using UnityEngine;

namespace DefenseFramework
{
    public class Bullet : MonoBehaviour
    {
        public int id = 0;
        public static int damage = 10;

        /// 속도
        public float speed = 0.5f;

        public GameObject nearMonster;
        public GameObject parentObject;

        // Use this for initialization
        private void Awake()
        {
        }

        // Update is called once per frame
        private void Update()
        {
            if ( nearMonster == null )
            {
                return;
            }
            if ( nearMonster.activeSelf == true )//해당몹이 액티브일경우만
            {
                // FIXIT

                transform.LookAt( nearMonster.transform.position );//타겟을 쳐다보게함

                transform.Translate( Vector3.forward * speed * Time.deltaTime );//앞으로전진
            }
            else
            {
                // FIXIT
                //총알 삭제처리 버그픽스
                //GameManager.insertObjet( this.gameObject );

                // 우선은 이렇게 처리
                gameObject.SetActive( false );

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


