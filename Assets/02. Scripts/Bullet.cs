using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public int id = 0;
	public static int damage = 10;
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
		if(nearMonster != null){
			
            // FIXIT
            // 총알 날아가는 방향이 이상함..
            
            transform.LookAt(nearMonster.transform.position);//타겟을 쳐다보게함
           
			transform.Translate (Vector3.forward  * speed * Time.deltaTime );//앞으로전진 
            
		}
	}
}
