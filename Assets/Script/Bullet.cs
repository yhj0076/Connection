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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        connectedBullet.Add(collision.gameObject);
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
}
