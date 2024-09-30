using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChanceBullet : MonoBehaviour
{
    public ChanceType type;
    public BulletType sameType;
    bool bomb;
    public List<GameObject> destroyList = new();
    public GameObject BulletMaker;
    public int DMG;

    private void Start()
    {
        BulletMaker = this.transform.parent.gameObject;
    }

    public void AddDMG()
    {
        GameObject.FindObjectOfType<Stage>().GetComponent<Stage>().gainedDamage += DMG;
    }

    public  void DestroyThis()
    {
        Destroy(this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!destroyList.Contains(collision.gameObject) && collision.gameObject.tag == "Bullet")
        {
            destroyList.Add(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!destroyList.Contains(collision.gameObject) && collision.gameObject.tag == "Bullet")
        {
            destroyList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        destroyList.Remove(collision.gameObject);
    }

    public void whenDestroy()
    {
        if (type == ChanceType.Explode)
        {
            int destroyCount = destroyList.Count;
            DMG = destroyCount;
            Debug.Log("Bomb!!");
            for (int i = 0; i < destroyCount; ++i)
            {
                Destroy(destroyList[0]);
            }
        }
        else if (type == ChanceType.Same)
        {
            if (sameType == BulletType.Red)
            {
                DestroySameBullets(0);
            }
            else if (sameType == BulletType.Green)
            {
                DestroySameBullets(1);
            }
            else if (sameType == BulletType.Blue)
            {
                DestroySameBullets(2);
            }
            else if (sameType == BulletType.Purple)
            {
                DestroySameBullets(3);
            }
            else if (sameType == BulletType.White)
            {
                DestroySameBullets(4);
            }

            // search all same bullets
            // clear searched bullets
        }
        else if (type == ChanceType.BigBang)
        {
            // claer all bullets
        }
    }

    private void DestroySameBullets(int index)
    {
        //Debug.Log(bulletMaker.transform.GetChild(0).name);
        //bulletMaker.transform.GetChild(0).GetComponent<BulletParent>().DestroyThis();
        //bulletMaker.GetComponent<BulletMaker>().MakeParent(0);
        int allNum = BulletMaker.transform.GetChild(index).childCount;
        DMG = allNum;
        for (int i = 0; i < allNum; i++)
        {
            BulletMaker.transform.GetChild(index).GetChild(i).GetComponent<Bullet>().DestroyThis();
            Debug.Log("destroyed!");
        }
        BulletMaker.GetComponent<BulletMaker>().MakeBullet(allNum);
    }

    public enum ChanceType
    {
        BigBang,
        Same,
        Explode
    }

    public enum BulletType
    {
        Red,
        Blue,
        Green,
        Purple,
        White,
        None
    }
}
