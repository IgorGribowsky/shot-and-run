using System.Collections.Generic;
using UnityEngine;

public class BulletPoolOld : MonoBehaviour
{
    public static BulletPoolOld Instance;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int initialCapacity = 100;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < initialCapacity; i++)
            CreateBullet();
    }

    private GameObject CreateBullet()
    {
        var b = Instantiate(bulletPrefab);
        b.SetActive(false);
        pool.Enqueue(b);
        return b;
    }

    public GameObject Get()
    {
        if (pool.Count == 0)
            CreateBullet();

        var b = pool.Dequeue();
        b.SetActive(true);
        return b;
    }

    public void Return(GameObject bullet)
    {
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }
}

