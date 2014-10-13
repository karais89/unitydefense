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
	private NavMeshAgent navAgent;

	// Use this for initialization
	void Awake () {
		navAgent = gameObject.GetComponent<NavMeshAgent>();

		endPointTransform = GameObject.Find ("EndPoint").GetComponent<Transform> ();

		// 걷는 애니메이션 바로 시작
		animation.Play( "Walk" );


	}

	void Start()
	{
		// navAgent = this.gameObject.GetComponent<NavMeshAgent>();

		// 추적 대상의 위치 설정하면 바로 추적 시작
		// navAgent.destination = endPointTransform.position;
	}
	
	// Update is called once per frame
	void Update () {


		if ( transform.position.z < endPointTransform.position.z )
		{
			transform.Translate ( Vector3.forward * moveSpeed * Time.deltaTime, Space.Self );
		}
	}

	void OnCollisionEnter( Collision coll )
	{
		if ( coll.collider.tag == "BULLET" )
		{
			HP -= Bullet.damage;

			// gameObject.GetComponentInChildren<Background>();

			// TODO
			// 총알 오브젝트를 삭제하지 않고 타워에서 재사용

			Destroy ( coll.gameObject );
			// coll.gameObject.SetActive( false );
		}
	}
}
