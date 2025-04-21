using System.Collections.Generic;
using UnityEngine;

namespace Script._ServerControl
{
    public class MultiStage : MonoBehaviour
    {
        protected Vector3 accelerationDir;
        public float Sensitivity;

        public GameObject SFX;
        public GameObject charge;
        public GameObject StageManager;
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

        public int ABP;
        public int LL;
        public int UCP;

        private void Awake()
        {
            Sensitivity = SecurityPlayerPrefs.GetFloat("sensitive", 5);
        }

        void Start()
        {
            StageManager.GetComponent<MultiStageManager>().UpdateData();
            BulletMaker.GetComponent<BulletMaker>().MakeBullet(MaxBullet);
            ABP = 0;
        }

        void Update()
        {
            // TouchUpdate();
            MouseUpdate();  // 컴퓨터 테스트용
        }


        void MakeChanceBullet(int BulletCount)
        {
            int midValue = (int)choose.Count / 2;
            Vector3 midVector = choose[midValue].gameObject.transform.position;
            Debug.Log("MakingBullet Mid : " + midVector);

            if (BulletCount >= 10)
            {
                GameObject No3Bomb = Instantiate(BigBang);
                No3Bomb.transform.position = midVector;
                No3Bomb.transform.parent = BulletMaker.transform;
                return;

            }

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

            }

            if (BulletCount >= 5)
            {
                GameObject No1Bomb = Instantiate(explode);
                No1Bomb.transform.position = midVector;
                No1Bomb.transform.parent = BulletMaker.transform;
                return;

            }
        }


        void ShakeStage()
        {
            if (!Shaking)
            {
                SFX.GetComponent<SfxController>().PlaySFX(4);
                Shaking = true;
                Vup();
                Invoke("Vdown", 0.1f);
            }
        }

        void Vup()
        {
            GetComponent<Transform>().position = new Vector3(0, 0.001f, 0);
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            rigid.velocity = Vector2.up * 3;
        }

        void Vdown()
        {
            Rigidbody2D rigid = GetComponent<Rigidbody2D>();
            rigid.velocity = Vector2.down * 3;
        }

        void MouseUpdate()
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
                        choose[choose.Count - 1].GetComponent<Bullet>().connectedBullet
                            .Contains(hit.collider.gameObject))
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
                if (choose.Count >= 5)
                {
                    MakeChanceBullet(choose.Count);
                }

                // Get Damage
                if (choose.Count >= 2)
                {
                    gainedDamage += choose.Count;
                    C_GainedDmg cGainedDmg = new C_GainedDmg();
                    cGainedDmg.gainedDmg = choose.Count;
                    GameObject nM = GameObject.Find("NetworkManager");
                    nM.GetComponent<NetworkManager>().Send(cGainedDmg.Write());

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
                StageManager.GetComponent<MultiStageManager>().UpdateData();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ShakeStage();
            }

            if (transform.position.y <= 0)
            {
                GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                GetComponent<Transform>().position = Vector3.zero;
                Shaking = false;
            }
        }

        void TouchUpdate()
            {
                LineRenderer line = this.GetComponent<LineRenderer>();
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                    if (touch.phase == TouchPhase.Began)
                    {
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
                                hit.collider.GetComponent<ChanceBullet>().DestroyThis();
                                Destroy(hit.collider.gameObject);
                                UCP++;
                            }
                        }
                    }

                    if (touch.phase == TouchPhase.Moved)
                    {
                        if (hit.collider != null)
                        {
                            if (hit.collider.gameObject.GetComponent<Bullet>().type == start &&
                                !hit.collider.gameObject.GetComponent<Bullet>().choosed &&
                                choose[choose.Count - 1].GetComponent<Bullet>().connectedBullet
                                    .Contains(hit.collider.gameObject))
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

                    if (touch.phase == TouchPhase.Ended)
                    {
                        if (choose.Count >= 5)
                        {
                            MakeChanceBullet(choose.Count);
                        }


                        if (choose.Count >= 2)
                        {
                            if (LL < choose.Count)
                            {
                                LL = choose.Count;
                            }

                            gainedDamage += choose.Count;
                            C_GainedDmg cGainedDmg = new C_GainedDmg();
                            cGainedDmg.gainedDmg = choose.Count;
                            GameObject nM = GameObject.Find("NetworkManager");
                            nM.GetComponent<NetworkManager>().Send(cGainedDmg.Write());
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

                        ABP += choose.Count;
                        choose.Clear();
                        line.positionCount = 0;
                        StageManager.GetComponent<MultiStageManager>().UpdateData();
                    }
                }

                isShaked();
            }

        void isShaked()
            {
                accelerationDir = Input.acceleration;

                if (accelerationDir.sqrMagnitude >= Sensitivity && Shaking == false)
                {
                    ShakeStage();
                }

                if (transform.position.y <= 0)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    GetComponent<Transform>().position = Vector3.zero;
                    Shaking = false;
                }
            }
        }
    }
