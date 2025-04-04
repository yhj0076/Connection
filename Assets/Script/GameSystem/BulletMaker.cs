using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMaker : MonoBehaviour
{
    public GameObject parent;
    public GameObject[] BulletPrefab;
    public GameObject reds;
    public GameObject greens;
    public GameObject blues;
    public GameObject purples;
    public GameObject whites;

    public void MakeBullet(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int RandomIndex = Random.Range(0, BulletPrefab.Length);
            GameObject bullet = Instantiate(BulletPrefab[RandomIndex]);
            bullet.transform.position = new Vector3(Random.Range(-2f, 3f), transform.position.y, 0);
            if (RandomIndex == 0)
            {
                bullet.transform.SetParent(reds.transform);
            }
            else if (RandomIndex == 1)
            {
                bullet.transform.SetParent(greens.transform);
            }
            else if (RandomIndex == 2)
            {
                bullet.transform.SetParent(blues.transform);
            }
            else if (RandomIndex == 3)
            {
                bullet.transform.SetParent(purples.transform);
            }
            else if (RandomIndex == 4)
            {
                bullet.transform.SetParent(whites.transform);
            }
        }
    }
}
