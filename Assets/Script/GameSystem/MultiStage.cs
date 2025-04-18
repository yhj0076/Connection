using UnityEngine;

namespace Script.GameSystem
{
    public class MultiStage : Stage
    {
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
            TouchUpdate();
        }
        
        protected void TouchUpdate()
        {
            LineRenderer line = this.GetComponent<LineRenderer>();
            if (Input.touchCount > 0)   // ÅÍÄ¡°¡ 1°³ ÀÌ»ó °¨ÁöµÇ¾úÀ» ¶§
            {
                Touch touch = Input.GetTouch(0);    // °¡Àå ¸ÕÀú ´©¸¥ ¼Õ°¡¶ô
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if(touch.phase == TouchPhase.Began) // ÅÍÄ¡°¡ ½ÃÀÛµÇ¾úÀ» ¶§
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.GetComponent<Bullet>()) // ÅÍÄ¡µÈ °ÍÀÌ ÀÏ¹Ý Æ÷¼ÇÀÏ ¶§
                        {
                            Bullet bullet = hit.collider.gameObject.GetComponent<Bullet>();
                            start = bullet.type;
                            bullet.choosed = true;
                            choose.Add(bullet.gameObject);
                            line.positionCount = 1;
                        }
                        else if (hit.collider.gameObject.GetComponent<ChanceBullet>())  // ÅÍÄ¡µÈ °ÍÀÌ Æ¯¼ö Æ÷¼ÇÀÏ ¶§
                        {
                            hit.collider.GetComponent<ChanceBullet>().whenDestroy();
                            hit.collider.GetComponent<ChanceBullet>().AddDMG();
                            hit.collider.GetComponent<ChanceBullet>().DestroyThis();
                            Destroy(hit.collider.gameObject);
                            UCP++;
                        }
                    }
                }
    
                if(touch.phase == TouchPhase.Moved) // ÅÍÄ¡µÈ »óÅÂ·Î µå·¡±×
                {
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.GetComponent<Bullet>().type == start &&
                            !hit.collider.gameObject.GetComponent<Bullet>().choosed &&
                            choose[choose.Count - 1].GetComponent<Bullet>().connectedBullet.Contains(hit.collider.gameObject))  // ÀÏ¹Ý Æ÷¼ÇÀÌ¸é¼­ °°Àº »ö Æ÷¼ÇÀÏ ¶§
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
    
                if(touch.phase == TouchPhase.Ended) // ÅÍÄ¡°¡ ³¡³µÀ» ¶§
                {
                    if (choose.Count >= 5)  // 5°³ ÀÌ»ó ¼±ÅÃ ½Ã Æ¯¼ö Æ÷¼Ç »ý¼º
                    {
                        MakeChanceBullet(choose.Count);
                    }
    
                    // µ¥¹ÌÁö °è»ê
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
                    ABP += choose.Count;
                    choose.Clear();
                    line.positionCount = 0;
                    StageManager.GetComponent<StageManager>().UpdateData();
                }
            }
    
            isShaked();
        }
    }
}