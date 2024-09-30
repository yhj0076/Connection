using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject BulletMaker;
    public int MaxBullet;
    public int connection = 0;
    public Bullet.BulletType start;
    public List<GameObject> choose = new();
    public int gainedDamage = 0;
    public bool Shaking = false;

    public GameObject explode;
    public GameObject[] same;
    public GameObject BigBang;
    // Start is called before the first frame update
    void Start()
    {
        BulletMaker.GetComponent<BulletMaker>().MakeBullet(MaxBullet);
    }

    // Update is called once per frame
    void Update()
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
                    int destroyCount = hit.collider.GetComponent<ChanceBullet>().DMG;
                    BulletMaker.GetComponent<BulletMaker>().MakeBullet(destroyCount);
                    hit.collider.GetComponent<ChanceBullet>().DestroyThis();
                    Destroy(hit.collider.gameObject);
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit.collider != null && 
                hit.collider.gameObject.GetComponent<Bullet>().type == start &&
                !hit.collider.gameObject.GetComponent<Bullet>().choosed &&
                choose[choose.Count-1].GetComponent<Bullet>().connectedBullet.Contains(hit.collider.gameObject))
            {
                Bullet bullet = hit.collider.gameObject.GetComponent<Bullet>();
                bullet.choosed = true;
                choose.Add(bullet.gameObject);
                line.positionCount++;           
            }
            for (int i = 0; i < choose.Count; i++) 
            {
                line.SetPosition(i, choose[i].gameObject.transform.position);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
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
            choose.Clear();
            line.positionCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeStage();
        }

        if(transform.position.y <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Transform>().position = Vector3.zero;
            Shaking = false;
        }
    }

    public void MakeChanceBullet(int BulletCount)
    {
        int midValue = (int)choose.Count / 2;
        Vector3 midVector = choose[midValue].gameObject.transform.position;
        Debug.Log("MakingBullet Mid : " + midVector);
        if (BulletCount >= 7)
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
            // Destroy All same Bullet.
        }
        if (BulletCount >= 5)
        {
            GameObject No1Bomb = Instantiate(explode);
            No1Bomb.transform.position = midVector;
            No1Bomb.transform.parent = BulletMaker.transform;
            return;
            // Explode connected Bullet.
        }


        if (BulletCount >= 10)
        {

            return;
            // Destroy All Bullet.
        }
    }

    public void ShakeStage()
    {
        if (!Shaking)
        {
            Shaking = true;
            Vup();
            Invoke("Vdown", 0.1f);
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

}
