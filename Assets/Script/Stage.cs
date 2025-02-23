using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Stage : MonoBehaviour
{
    Vector3 accelerationDir;    // ���ӵ� ����� ���� ��ǥ
    public float Sensitivity;   // ���� �ΰ���

    public GameObject SFX;
    public GameObject charge;
    public GameObject GoBackWindow;     // �ڷΰ��� â
    public GameObject StageManager;     // StageManager
    public GameObject GameManager;      // GameManeger
    public GameObject BulletMaker;      // ���� ������
    public int MaxBullet;               // �ִ� ���� ����
    public int connection = 0;          // ���� ��
    public Bullet.BulletType start;     // ���� ���� ��
    public List<GameObject> choose = new();     // ���õ� ����
    public int gainedDamage = 0;    // ���� ������
    public bool Shaking = false;    // ���ȴ��� �Ǵ�
    public bool Damaged = false;            // �������� �Ծ��� �� �Ǵ�

    public GameObject explode;      // ��ź
    public GameObject[] same;       // ������ Ư�� ����
    public GameObject BigBang;      // ���� �����ϴ� ����

    public int ABP;     // All Breaked Potion : �� �μ� ���� ��
    public int LL;      // Longest Link : ���� �� ��ũ
    public int UCP;     // Used Chance Potion : ����� Ư�� ����
      

    private void Awake()
    {
        Sensitivity = SecurityPlayerPrefs.GetFloat("sensitive", 5);
    }

    void Start()
    {
        StageManager.GetComponent<StageManager>().UpdateData();     // ������ ��ġ �ʱ�ȭ ������Ʈ
        BulletMaker.GetComponent<BulletMaker>().MakeBullet(MaxBullet);      // �ִ� ���� ������ŭ �ʱ� ���� ����
        ABP = 0;       // �ʱ� �μ� ���� �ʱ�ȭ
    }

    void Update()
    {
        TouchUpdate();
    }

    // Ư�� ������ ����� �޼���
    public void MakeChanceBullet(int BulletCount)
    {
        int midValue = (int)choose.Count / 2;   // ���õ� ���� �� ��� ���� Ž��
        Vector3 midVector = choose[midValue].gameObject.transform.position; // ��� ������ ��ǥ�� ����
        Debug.Log("MakingBullet Mid : " + midVector);   // ��� ������ ��ǥ�� ����

        if (BulletCount >= 10)  // �ı��� ������ 10�� �̻��� ��
        {
            GameObject No3Bomb = Instantiate(BigBang);
            No3Bomb.transform.position = midVector;
            No3Bomb.transform.parent = BulletMaker.transform;
            return;
            // ��� ������ ���ִ� Ư�� ���� ����
        }
        if (BulletCount >= 7) // �ı��� ������ 7�� �̻��� ��
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
            // �� ���� �´� ���� ����
        }
        if (BulletCount >= 5)
        {
            GameObject No1Bomb = Instantiate(explode);
            No1Bomb.transform.position = midVector;
            No1Bomb.transform.parent = BulletMaker.transform;
            return;
            // �ֺ� ������ �ı��ϴ� ��ź ����
        }
    }

    // ���ڸ� ���� �޼���
    public void ShakeVertical()
    {
        if (!Shaking)
        {
            SFX.GetComponent<SfxController>().PlaySFX(4);
            Shaking = true;
            Vup();  // ���ڸ� ���� �����̰� �ϴ� �޼���
            Invoke("Vdown", 0.1f);  // 0.1�� �� ���ڸ� �������� �ϴ� �޼��� ����
        }
    }

    public void ShakeHorizontal()
    {
        if (Damaged)
        {
            Shaking = true;
            Vleft();    // ���ڸ� �������� ���� �޼���
            Invoke("Vright", 0.1f);  // 0.1�� �� ���ڸ� ���ƿ��� �ϴ� �޼��� ����
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
    }   // �׽�Ʈ�� ���콺 ���� �޼���

    

    protected void TouchUpdate()
    {
        LineRenderer line = this.GetComponent<LineRenderer>();
        if (Input.touchCount > 0)   // ��ġ�� 1�� �̻� �����Ǿ��� ��
        {
            Touch touch = Input.GetTouch(0);    // ���� ���� ���� �հ���
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(touch.phase == TouchPhase.Began) // ��ġ�� ���۵Ǿ��� ��
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<Bullet>()) // ��ġ�� ���� �Ϲ� ������ ��
                    {
                        Bullet bullet = hit.collider.gameObject.GetComponent<Bullet>();
                        start = bullet.type;
                        bullet.choosed = true;
                        choose.Add(bullet.gameObject);
                        line.positionCount = 1;
                    }
                    else if (hit.collider.gameObject.GetComponent<ChanceBullet>())  // ��ġ�� ���� Ư�� ������ ��
                    {
                        hit.collider.GetComponent<ChanceBullet>().whenDestroy();
                        hit.collider.GetComponent<ChanceBullet>().AddDMG();
                        hit.collider.GetComponent<ChanceBullet>().DestroyThis();
                        Destroy(hit.collider.gameObject);
                        UCP++;
                    }
                }
            }

            if(touch.phase == TouchPhase.Moved) // ��ġ�� ���·� �巡��
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.GetComponent<Bullet>().type == start &&
                        !hit.collider.gameObject.GetComponent<Bullet>().choosed &&
                        choose[choose.Count - 1].GetComponent<Bullet>().connectedBullet.Contains(hit.collider.gameObject))  // �Ϲ� �����̸鼭 ���� �� ������ ��
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

            if(touch.phase == TouchPhase.Ended) // ��ġ�� ������ ��
            {
                if (choose.Count >= 5)  // 5�� �̻� ���� �� Ư�� ���� ����
                {
                    MakeChanceBullet(choose.Count);
                }

                // ������ ���
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
        // TODO : �¿� ��鸱 �� �ε�ε� ������ ���� �߰�
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
