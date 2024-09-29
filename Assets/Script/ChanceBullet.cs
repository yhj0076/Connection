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
            Debug.Log("Bomb!!");
            for (int i = 0; i < destroyList.Count; ++i)
            {
                Destroy(destroyList[0]);
            }
        }
        else if (type == ChanceType.Same)
        {
            GameObject bulletMaker = this.transform.parent.gameObject;
            if (sameType == BulletType.Red)
            {
                foreach (GameObject go in bulletMaker.transform.GetChild(0))
                {
                    Destroy(go);
                }
            }
            if (sameType == BulletType.Green)
            {
                foreach (GameObject go in bulletMaker.transform.GetChild(1))
                {
                    Destroy(go);
                }
            }
            if (sameType == BulletType.Blue)
            {
                foreach (GameObject go in bulletMaker.transform.GetChild(2))
                {
                    Destroy(go);
                }
            }
            if (sameType == BulletType.Purple)
            {
                foreach (GameObject go in bulletMaker.transform.GetChild(3))
                {
                    Destroy(go);
                }
            }
            if (sameType == BulletType.White)
            {
                for (int i = 0; i < bulletMaker.transform.GetChild(4).childCount; i++)
                {
                    Destroy(bulletMaker.transform.GetChild(4).gameObject);
                }
            }

            // search all same bullets
            // clear searched bullets
        }
        else if (type == ChanceType.BigBang)
        {
            // claer all bullets
        }
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
