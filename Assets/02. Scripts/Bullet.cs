using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public int id = 0;
	public static int damage = 10;    
    /// 속도    
	public float speed = 0.5f;
	public GameObject nearMonster;
	public GameObject parentObject;

	// Use this for initialization
	void Awake () {

		// TODO
		// 타워에 가장 가까이에 있는 몬스터를 타겟으로 총알 생성
        // nearMonster = transform.parent.GetComponent<Tower>().GetClosestMonster();
        

		/*
		rigidbody 적용해서 이동
		Debug.Log ( "AddForceAtPosition x = " + nearMonster.transform.position.x + " y = " + nearMonster.transform.position.y + " z = " + nearMonster.transform.position.z );

		// rigidbody.AddForce( nearMonster.transform.position * speed );
		Vector3 direction = rigidbody.transform.position - nearMonster.transform.position;
		rigidbody.AddForceAtPosition( direction.normalized, nearMonster.transform.position );

		Vector3 directionNormalized = direction.normalized;
		Debug.Log ( "direction x = " + directionNormalized.x + " y = " + directionNormalized.y + " z = " + directionNormalized.z );
		transform.Rotate ( transform.rotation.x, directionNormalized.y, directionNormalized.z );
		*/
        
        // Vector3 direction = transform.position - nearMonster.transform.position;
        // transform.rotation = Quaternion.LookRotation( direction );

	}
	    
    // Update is called once per frame
	void Update () {
		if (nearMonster == null) 
		{
			return;
		}
		if (nearMonster.activeSelf == true)//해당몹이 액티브일경우만
		{
			// FIXIT
			// 총알 날아가는 방향이 이상함.....이상해서 고침요ㅋ
			
			transform.LookAt (nearMonster.transform.position);//타겟을 쳐다보게함
			
			transform.Translate (Vector3.forward * speed * Time.deltaTime);//앞으로전진 
			
		} 
		else 
		{
			//총알 삭제처리 버그픽스 
			GameManager.insertObjet(this.gameObject);
			//총알연사력0.1에 총알속도0.3으로 테스트 (엄청빠르게쏘고 느리게날라가게해봄)
			//몹이죽은후 총알이 멈춘채로 대기후에 재사용된몹이 생성되면 그놈한테 날라감
			//가끔 총알이 남아있는경우가 보여서 테스트후 버그처리완료
			//추가후 잘사라짐
		}
	}
	
	void OnTriggerEnter( Collider coll )
    {

    }
}
