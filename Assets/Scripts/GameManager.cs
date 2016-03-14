/**
 * @file GameManager.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2014-10-29
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefenseFramework;

public class GameManager : MonoBehaviour
{
    public float createTime = 2.0f;
    private int monsterCount = 0;
    private Transform[] spawnTransform = new Transform[16];
    public static bool isGameOver = false;

    private System.Random rand = new System.Random();
    
    //큐 선언 - 프리팹을 재활용할 용도
    //개인적으론 static 보단 싱글톤이 좋은뎀 ㅋㅋ static다 달아줘야함 ㅠ
    ///몬스터큐
    public static Queue<GameObject> monster_Queue = new Queue<GameObject>();

    ///총알큐
    public static Queue<GameObject> bullet_Queue = new Queue<GameObject>();

    ///HpBar큐
    public static Queue<GameObject> hpBar_Queue = new Queue<GameObject>();

    ///몬스터 리스트
    public static List<GameObject> monsterList = new List<GameObject>();

    ///몬스터를 담아둘 폴더오브젝트
    private static Transform enemyFolder = null;//파인드는 유니티에 무리를 주는행위라서 어웨이크에서 한번만 실행하기위해 클레스변수에 담음

    //총알을 담아둘 폴더오브젝트
    private static Transform bulletFolder = null;

    //Hpbar를 담아둘 폴더오브젝트
    private static Transform hpBarFolder = null;

    //생성될 프리팹에 이름붙일용도
    private static int monster_Queue_Count;

    private static int bullet_Queue_Count;
    private static int hpBar_Queue_Count;

    //프리팹명칭 const는 내부적으로 static이 된다.
    public const string monster_PrefabName = "Monster";

    public const string bullet_PrefabName = "Bullet";
    public const string hpBar_PrefabName = "HpBar";

    ///시작소지금액
    public int initialGold = 120;

    ///금액
    public static int gold = 0;

    ///점수
    public static int score = 0;

    ///금액라벨
    public UILabel goldText = null;

    ///웨이브라벨
    public UILabel waveText = null;

    ///스피드라벨
    public UILabel speedLabel = null;

    ///스톱버튼UI스플라이트
    public UISprite stopUISprite = null;

    ///스톱버튼UI버튼
    public UIButton stopUIButton = null;

    ///스톱시 팝업창
    public GameObject StopPopup = null;

    ///일지정지
    private bool isGamePaused = false;

    ///게임속도
    private float GameSpeed = 1.0f;

    private int currentWave = 1;
    public int maxWave = 30;
    public int monsterNumPerWave = 32;
    private bool isWaveEnded = false;

    // Use this for initialization
    private void Awake()
    {
        //팝업창 꺼두기
        StopPopup.SetActive( false );
        GameObject.Find( "MapFilled" ).SetActive( false );

        GameObject.Find( "Map" ).GetComponent<TileMap>().LoadResources();

        GameObject.Find( "Map" ).GetComponent<TileMap>().LoadMapJSON();

        //InitPathFinder();

        List<GameObject> list = GameObject.Find( "SpawnManager" ).GetComponent<SpawnManager>().spawnList;

        int i = 0;
        foreach ( GameObject spawn in list )
        {
            spawnTransform[i] = spawn.transform;
            i++;
        }

        // waypoint에 일정 시간 간격으로 캐릭터 생성
        StartCoroutine( this.CreateMonster() );
        gold = initialGold;
        

        //몬스터를담을 폴더지정
        enemyFolder = GameObject.Find( "Enemy" ).transform;
        bulletFolder = GameObject.Find( "Bullet" ).transform;
        hpBarFolder = GameObject.Find( "HpBarUI" ).transform;
        
    }

    public static void reSetList()
    {
        monster_Queue.Clear();
        bullet_Queue.Clear();
        hpBar_Queue.Clear();
        monsterList.Clear();
    }

    ///오브젝트생성 (생성할프리팹명)
    public static GameObject createObjet( string name )//월래 통합해서 만들엇는데 개별적으로 옵션줘야할듯해서 다시 따로만듬
    {
        GameObject obj = null;

        //이름에 따라 메서드선택
        switch ( name )
        {
            case monster_PrefabName:
                obj = createMonster( name );
                break;

            case bullet_PrefabName:
                obj = createBullet( name );
                break;

            case hpBar_PrefabName:
                obj = createHpBar( name );
                break;

            default:
                break;
        }
        return obj;
    }

    ///몬스터생성 혹은 꺼내오기
    public static GameObject createMonster( string name )
    {
        GameObject obj = null;
        if ( monster_Queue.Count == 0 )
        {
            obj = (GameObject) Instantiate( Resources.Load( "Prefabs/" + monster_PrefabName ) as GameObject );
            obj.transform.parent = enemyFolder;//생성시에만 부모설정
            obj.name = name + "_" + monster_Queue_Count++;//이름과 함께 테스트용도로 번호매겨줌 ~_~
        }
        else
        {
            obj = monster_Queue.Dequeue();
            obj.SetActive( true );//새로 생성한건 어차피 트루상태라서 꺼낼때만 실행하도록함
        }
        monsterList.Add( obj );//몬스터만 리스트에 담기
        return obj;
    }

    ///총알생성 혹은 꺼내오기
    public static GameObject createBullet( string name )
    {
        GameObject obj = null;
        if ( bullet_Queue.Count == 0 )
        {
            obj = (GameObject) Instantiate( Resources.Load( "Prefabs/" + bullet_PrefabName ) as GameObject );
            obj.transform.parent = bulletFolder;
            obj.name = name + "_" + bullet_Queue_Count++;
        }
        else
        {
            obj = bullet_Queue.Dequeue();
            obj.SetActive( true );
        }
        return obj;
    }

    ///HpBar생성 혹은 꺼내오기
    public static GameObject createHpBar( string name )
    {
        GameObject obj = null;
        if ( hpBar_Queue.Count == 0 )
        {
            obj = (GameObject) Instantiate( Resources.Load( "Prefabs/" + hpBar_PrefabName ) as GameObject );
            obj.transform.parent = hpBarFolder;
            obj.name = name + "_" + hpBar_Queue_Count++;
        }
        else
        {
            obj = hpBar_Queue.Dequeue();
            obj.SetActive( true );
        }
        return obj;
    }

    ///죽거나 사용이 끝난 오브젝트를 큐로 넣어준다(GameObject 사용된오브젝트)
    public static void insertObjet( GameObject obj )
    {
        if ( obj == null )
        {
            return;
        }
        if ( obj.name.StartsWith( monster_PrefabName ) )//앞글자확인후처리
        {
            monster_Queue.Enqueue( obj );
            monsterList.Remove( obj );//몬스터는 리스트에서 뺀다
        }
        else if ( obj.name.StartsWith( bullet_PrefabName ) )
        {
            bullet_Queue.Enqueue( obj );
        }
        else if ( obj.name.StartsWith( hpBar_PrefabName ) )
        {
            hpBar_Queue.Enqueue( obj );
        }
        obj.SetActive( false );
    }

    /// 일정 시간 간격을 두고 몬스터를 생성한다.
    private IEnumerator CreateMonster()
    {
        while ( isGameOver == false )
        {
            yield return new WaitForSeconds( createTime );
            monsterCount++;

            if ( monsterCount > monsterNumPerWave )
            {
                monsterCount = 0;
                isWaveEnded = true;
                currentWave++;
                //웨이브 표기
                waveText.text = currentWave + " / " + maxWave;
                yield return null;
            }

            Debug.Log( "create new monster id = " + monsterCount );
            //Debug.Log("monsterList.Count : " + monsterList.Count);

            int count = GameObject.Find( "SpawnManager" ).GetComponent<SpawnManager>().spawnList.Count;
            int random = rand.Next( 0, count );

            //몬스터오브젝트생성 혹은 꺼내오기
            GameObject newMonster = createObjet( monster_PrefabName );
            newMonster.transform.position = spawnTransform[random].position;
            //newMonster.transform.position = new Vector3(0, 0, 0);
            newMonster.transform.rotation = Quaternion.identity;
            newMonster.GetComponent<Monster>().id = monsterCount;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        goldText.text = gold.ToString();
    }

    /// 일시 정지 버튼 - ui에서 호출
    public void StopButton()
    {
        if ( isGamePaused == false )
        {
            isGamePaused = true;
            Time.timeScale = 0.0f;

            //ngui의 스플라이트를 교체
            stopUISprite.spriteName = "StartButton";
            //ngui의 버튼쪽 평소의 버튼모양을 교체
            //(노말 롤오버 클릭상태등 거기에 따라 그림이 교체되니 같이처리해야한다.)
            stopUIButton.normalSprite = "StartButton";
            StopPopup.SetActive( true );
        }
        else if ( isGamePaused == true )
        {
            isGamePaused = false;
            Time.timeScale = GameSpeed;//저장된 배속으로 다시시작
            stopUISprite.spriteName = "StopButton";
            stopUIButton.normalSprite = "StopButton";
        }
    }

    ///컨티뉴버튼 - ui에서 호출
    public void ContinueButton()
    {
        StopPopup.SetActive( false );
        isGamePaused = false;
        Time.timeScale = GameSpeed;//저장된 배속으로 다시시작
        stopUISprite.spriteName = "StopButton";
        stopUIButton.normalSprite = "StopButton";
    }

    /// 속도 버튼 - ui에서 호출
    public void SpeedButton()
    {
        if ( isGamePaused == true ) //일시정지일경우는 무시
        {
            return;
        }
        else if ( GameSpeed == 1.0f )
        {
            GameSpeed = 2.0f;
            speedLabel.text = "x4";
        }
        else if ( GameSpeed == 2.0f )
        {
            GameSpeed = 4.0f;
            speedLabel.text = "x1";
        }
        else if ( GameSpeed == 4.0f )
        {
            GameSpeed = 1.0f;
            speedLabel.text = "x2";
        }
        Time.timeScale = GameSpeed;
    }

    /// 종료 버튼 - ui에서 호출
    public void QuitButton()
    {
        reSetList();
        Application.LoadLevel( 0 );
    }
}