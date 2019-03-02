using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    public float bulletSpeed = 400;
    public int clipSize = 30;
    public double fireRate = 0.12;
    public GameObject bulletObject;
    public int damage = 40;
    private int clip;
    private double nextFire = 0;

    // Start is called before the first frame update
    void Start()
    {
        clip = clipSize;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButton("Fire1") || Input.GetAxis("Fire1") > 0) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            Shoot();
        }
        if (Input.GetButton("Reload") && clip != clipSize) {
            Reload();
        }
    }

    void Shoot()
    {
        if (clip > 0) {
            clip -= 1;
            bulletObject.GetComponent<DestroyBullet>().damage = damage;
            Rigidbody bullet = Instantiate(bulletObject.GetComponent<Rigidbody>(), transform.position, transform.rotation);
            bullet.velocity = transform.TransformDirection(Vector3.left * bulletSpeed);
        }
    }

    void Reload()
    {
        clip = clipSize;
    }
}
