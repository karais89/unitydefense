using UnityEngine;
using System.Collections;

public class HeroTower : MonoBehaviour {
    [HideInInspector]
    public float HP = 0.0f;

    public float HP_Max = 200.0f;
    private Vector3 pointHP = Vector3.zero;
    private Rect rectHP;
    public Texture HP_EmptyTexture;
    public Texture HP_FullTexture;    

    void Awake()
    {
        
        HP = HP_Max;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        // HP가 모두 소모되면 게임 오버 처리
	    if ( HP <= 0 )
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().isGameOver = true;

            Time.timeScale = 0.0f;
        }
	}

    void OnGUI()
    {
        // GUI 영웅 타워 HP
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