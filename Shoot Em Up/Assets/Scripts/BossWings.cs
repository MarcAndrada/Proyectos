using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWings : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float offsetBullet;
    public float fireRate;

    private float TimeToShoot;
    private GameObject bullet;

    // Update is called once per frame
    void Update()
    {
        float delta = Time.deltaTime * 1000;

        TimeToShoot += delta;

        if (TimeToShoot > fireRate) {
            Vector3 pos = transform.up * offsetBullet + transform.position;
            bullet = Instantiate(bulletPrefab, pos, transform.rotation);
            Destroy(bullet, 3);
            TimeToShoot = 0;
        }
    }
}
