using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Stage : MonoBehaviour
{
    Vector3 accelerationDir;    // 가속도 계산을 위한 좌표
    public float Sensitivity;   // 흔들기 민감도

    public GameObject SFX;
    public GameObject charge;
    public GameObject GoBackWindow;     // 뒤로가기 창
    public GameObject StageManager;     // StageManager
    public GameObject GameManager;      // GameManeger
    public GameObject BulletMaker;      // 포센 제조기
    public int MaxBullet;               // 최대 포션 개수
    public int connection = 0;          // 연결 수
    public Bullet.BulletType start;     // 시작 포션 색
    public List<GameObject> choose = new();     // 선택된 포션
    public int gainedDamage = 0;    // 모인 데미지
    public bool Shaking = false;    // 흔들렸는지 판단
    public bool Damaged = false;            // 데미지를 입었는 지 판단

    public GameObject explode;      // 폭탄
    public GameObject[] same;       // 같은색 특수 포션
    public GameObject BigBang;      // 전부 삭제하는 포션

    public int ABP;     // All Breaked Potion : 총 부순 포션 수
    public int LL;      // Longest Link : 가장 긴 링크
    public int UCP;     // Used Chance Potion : 사용한 특수 포션
      

    private void Awake()
    {
        Sensitivity = SecurityPlayerPrefs.GetFloat("sensitive", 5);
    }

    void Start()
    {
        StageManager.GetComponent<StageManager>().UpdateData();     // 데미지 수치 초기화 업데이트
        BulletMaker.GetComponent<BulletMaker>().MakeBullet(MaxBullet);      // 최대 포션 개수만큼 초기 포션 생성
        ABP = 0;       // 초기 부순 포션 초기화
    }

    void Update()
    {
        TouchUpdate();
    }

    // 특수 포션을 만드는 메서드
    public void MakeChanceBullet(int BulletCount)
    {
        int midValue = (int)choose.Count / 2;   // 선택된 포션 중 가운데 포션 탐색
        Vector3 midVector = choose[midValue].gameObject.transform.position; // 가운데 포션의 좌표값 저장
        Debug.Log("MakingBullet Mid : " + midVector);   // 가운데 포션의 좌표값 생성

        if (BulletCount >= 10)  // 파괴된 포션이 10개 이상일 때
        {
            GameObject No3Bomb = Instantiate(BigBang);
            No3Bomb.transform.position = midVector;
            No3Bomb.transform.parent = BulletMaker.transform;
            return;
            // 모든 포션을 없애는 특수 포션 생성
        }
        if (BulletCount >= 7) // 파괴된 포션이 7개 이상일 때
        {
            GameObject No2Bomb = null;
            if (choose[0].gameObject.GetComponent<Bullet>().type == Bullet.BulletType.Red)
            {
                No2Bomb = Instantiate(same[0]);
            }
            else if (choose[0].gameObject.GetComponent<Bullet>().type == Bullet.BulletType.Green)
            {
                No2Bomb = Instantiate(same[1]);
            }
            else if (choose[0].gameObject.GetComponent<Bullet>().type == Bullet.BulletType.Blue)
            {
                No2Bomb = Instantiate(same[2]);
            }
            else if (choose[0].gameObject.GetComponent<Bullet>().type == Bullet.BulletType.Purple)
            {
                No2Bomb = Instantiate(same[3]);
            }
            else if (choose[0].gameObject.GetComponent<Bullet>().type == Bullet.BulletType.White)
            {
                No2Bomb = Instantiate(same[4]);
            }

            No2Bomb.transform.position = midVector;
            No2Bomb.transform.parent = BulletMaker.transform;
            return;
            // 각 색깔에 맞는 포션 생성
        }
        if (BulletCount >= 5)
        {
            GameObject No1Bomb = Instantiate(explode);
            No1Bomb.transform.position = midVector;
            No1Bomb.transform.parent = BulletMaker.transform;
            return;
            // 주변 포션을 파괴하는 폭탄 생성
        }
    }

    // 상자를 흔드는 메서드
    public void ShakeVertical()
    {
        if (!Shaking)
        {
            SFX.GetComponent<SfxController>().PlaySFX(4);
            Shaking = true;
            Vup();  // 상자를 위로 움직이게 하는 메서드
            Invoke("Vdown", 0.1f);  // 0.1초 후 상자를 내려오게 하는 메서드 실행
        }
    }

    public void ShakeHorizontal()
    {
        if (Damaged)
        {
            Shaking = true;
            Vleft();    // 상자를 왼쪽으로 흔드는 메서드
            Invoke("Vright", 0.1f);  // 0.1초 후 상자를 돌아오게 하는 메서드 실행
        }
    }

    public void Vup()
    {
        GetComponent<Transform>().position = new Vector3(0,0.001f,0);
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.up * 3;
    }

    public void Vdown()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * 3;
    }

    public void Vleft()
    {
        GetComponent<Transform>().position = new Vector3(-0.001f,0, 0);
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.left * 3;
    }

    public void Vright()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.right * 3;
    }

    private void UpdateData2()
    {
        if (StageManager.GetComponent<MainStageManager>() != null)
        {
            StageManager.GetComponent<MainStageManager>().UpdateData();
        }
        else
        {
            //StageManager.GetComponent<TutorialStageManager>().UpdateData();
        }
    }

    protected void MouseUpdate()
    {
        LineRenderer line = this.GetComponent<LineRenderer>();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<Bullet>())
                {
                    Bullet bullet = hit.collider.gameObject.GetComponent<Bullet>();
                    start = bullet.type;
                    bullet.choosed = true;
                    choose.Add(bullet.gameObject);
                    line.positionCount = 1;
                }
                else if (hit.collider.gameObject.GetComponent<ChanceBullet>())
                {
                    hit.collider.GetComponent<ChanceBullet>().whenDestroy();
                    hit.collider.GetComponent<ChanceBullet>().AddDMG();
                    //int destroyCount = hit.collider.GetComponent<ChanceBullet>().DMG;
                    //BulletMaker.GetComponent<BulletMaker>().MakeBullet(destroyCount);
                    hit.collider.GetComponent<ChanceBullet>().DestroyThis();
                    Destroy(hit.collider.gameObject);
                    UCP++;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<Bullet>().type == start &&
                    !hit.collider.gameObject.GetComponent<Bullet>().choosed &&
                    choose[choose.Count - 1].GetComponent<Bullet>().connectedBullet.Contains(hit.collider.gameObject))
                {
                    Bullet bullet = hit.collider.gameObject.GetComponent<Bullet>();
                    bullet.choosed = true;
                    choose.Add(bullet.gameObject);
                    line.positionCount++;
                }
            }

            for (int i = 0; i < choose.Count; i++)
            {
                line.SetPosition(i, choose[i].gameObject.transform.position);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (LL < choose.Count)
            {
                LL = choose.Count;
            }

            if (choose.Count >= 5)
            {
                MakeChanceBullet(choose.Count);
            }

            // Get Damage
            if (choose.Count >= 2)
            {
                gainedDamage += choose.Count;
                foreach (GameObject bullets in choose)
                {
                    Destroy(bullets);
                }
                BulletMaker.GetComponent<BulletMaker>().MakeBullet(choose.Count);
            }
            foreach (GameObject bullets in choose)
            {
                bullets.GetComponent<Bullet>().choosed = false;
            }
            if (choose.Count >= 2)
            {
                ABP += choose.Count;
            }
            choose.Clear();
            line.positionCount = 0;
            UpdateData2();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeVertical();
        }

        if (transform.position.y <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Transform>().position = Vector3.zero;
            Shaking = false;
        }
    }   // 테스트용 마우스 조작 메서드

    

    protected void TouchUpdate()
    {
        LineRenderer line = this.GetComponent<LineRenderer>();
        if (Input.touchCount > 0)   // 터치가 1개 이상 감지되었을 때
        {
            Touch touch = Input.GetTouch(0);    // 가장 먼저 누른 손가락
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(touch.phase == TouchPhase.Began) // 터치가 시작되었을 때
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<Bullet>()) // 터치된 것이 일반 포션일 때
                    {
                        Bullet bullet = hit.collider.gameObject.GetComponent<Bullet>();
                        start = bullet.type;
                        bullet.choosed = true;
                        choose.Add(bullet.gameObject);
                        line.positionCount = 1;
                    }
                    else if (hit.collider.gameObject.GetComponent<ChanceBullet>())  // 터치된 것이 특수 포션일 때
                    {
                        hit.collider.GetComponent<ChanceBullet>().whenDestroy();
                        hit.collider.GetComponent<ChanceBullet>().AddDMG();
                        hit.collider.GetComponent<ChanceBullet>().DestroyThis();
                        Destroy(hit.collider.gameObject);
                        UCP++;
                    }
                }
            }

            if(touch.phase == TouchPhase.Moved) // 터치된 상태로 드래그
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<Bullet>().type == start &&
                        !hit.collider.gameObject.GetComponent<Bullet>().choosed &&
                        choose[choose.Count - 1].GetComponent<Bullet>().connectedBullet.Contains(hit.collider.gameObject))  // 일반 포션이면서 같은 색 포션일 때
                    {
                        Bullet bullet = hit.collider.gameObject.GetComponent<Bullet>();
                        bullet.choosed = true;
                        choose.Add(bullet.gameObject);
                        line.positionCount++;
                        SFX.GetComponent<SfxController>().PlaySFX(0);
                    }
                }

                for (int i = 0; i < choose.Count; i++)
                {
                    line.SetPosition(i, choose[i].gameObject.transform.position);
                }
            }

            if(touch.phase == TouchPhase.Ended) // 터치가 끝났을 때
            {
                if (choose.Count >= 5)  // 5개 이상 선택 시 특수 포션 생성
                {
                    MakeChanceBullet(choose.Count);
                }

                // 데미지 계산
                if (choose.Count >= 2)
                {
                    if (LL < choose.Count)
                    {
                        LL = choose.Count;
                    }


                    gainedDamage += choose.Count;
                    foreach (GameObject bullets in choose)
                    {
                        Destroy(bullets);
                    }
                    BulletMaker.GetComponent<BulletMaker>().MakeBullet(choose.Count);
                    charge.GetComponent<EnergeCharge>().charge();
                }
                foreach (GameObject bullets in choose)
                {
                    bullets.GetComponent<Bullet>().choosed = false;
                }
                if (choose.Count >= 2)
                {
                    ABP += choose.Count;
                }
                choose.Clear();
                line.positionCount = 0;
                UpdateData2();
            }
        }

        isShaked();
    }

    void isShaked()
    {
        accelerationDir = Input.acceleration;

        if (accelerationDir.sqrMagnitude >= Sensitivity && Shaking == false)
        {
            ShakeVertical();
        }
        // TODO : 좌우 흔들릴 때 부들부들 떨리는 버그 발견
        //if (Damaged)
        //{
        //    ShakeHorizontal();
        //}


        if (transform.position.y <= 0 && transform.position.x >= 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Transform>().position = Vector3.zero;
            Shaking = false;
            Damaged = false;
        }
    }

    public void WannaGoBack()
    {
        GoBackWindow.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseGoBackWindow()
    {
        GoBackWindow.SetActive(false);
        Time.timeScale = 1;
    }

    public void WannaGoHome()
    {
        Time.timeScale = 1;
        GameManager.GetComponent<GameManager>().GoHome();
    }
}
