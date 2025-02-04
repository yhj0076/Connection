using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    public BulletType type;
    public bool connected;
    public bool choosed;
    public List<GameObject> connectedBullet = new();


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!connectedBullet.Contains(collision.gameObject))
        {
            connectedBullet.Add(collision.gameObject);
            if (collision.gameObject.tag == "Bullet")
            {
                collision.gameObject.GetComponent<Bullet>().connectedBullet.Add(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        connectedBullet.Remove(collision.gameObject);
    }
    public enum BulletType
    {
        Red,
        Blue,
        Green,
        Purple,
        White
    }

    public void DestroyThis()
    {
        Destroy(this.gameObject);
    }
}
